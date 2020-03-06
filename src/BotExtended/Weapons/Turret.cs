using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Weapons
{
    enum TurretDirection
    {
        Left,
        Right,
    }

    enum TurretDamage
    {
        None = 0,
        SensorDamaged = 1, // Shoot everyone
        ControllerDamaged = 2, // Rotate randomly (if rotor is not damaged)
        RotorDamaged = 4, // Cant rotate
        BarrelDamaged = 8, // Cant shoot
    }


    //var area = new Area(-140, 280f, -170, 330f);
    //Events.UpdateCallback.Start((e) => Game.DrawArea(area));
    //var turrentComps = Game.GetObjectsByArea(area);
    //var rotationPointPos = turrentComps.Where(c => c.CustomID == "TurretRotationPoint").Single().GetWorldPosition();
    //foreach (var comp in turrentComps)
    //{
    //    if (comp.CustomID == "") continue;
    //    var pos = comp.GetWorldPosition();
    //    var xDecimal = pos.X - Math.Truncate(pos.X);
    //    var x = xDecimal >= .9 || xDecimal <= .1 ? Math.Round(pos.X) : pos.X;
    //    var yDecimal = pos.Y - Math.Truncate(pos.Y);
    //    var y = yDecimal >= .9 || yDecimal <= .1 ? Math.Round(pos.Y) : pos.Y;

    //    Game.WriteToConsole(string.Format("x:{0:0.0} y:{1:0.0} {2:0.0}",
    //        x - rotationPointPos.X, y - rotationPointPos.Y, comp.CustomID));
    //}

    class Turret : Weapon
    {
        public int UniqueID { get { return m_tip.UniqueID; } }
        public IPlayer Owner { get; private set; }

        public PlayerTeam Team { get; private set; }

        private Dictionary<string, IObject> m_components = new Dictionary<string, IObject>();
        public override IEnumerable<IObject> Components { get { return m_components.Values; } }

        private List<Component> m_damageableComponents = new List<Component>();

        private IObject m_tip;
        private Component m_rotor;
        private Component m_controller;
        private Component m_barrel;
        private Component m_sensor;
        private TurretDamage m_damage = TurretDamage.None;
        public bool Broken { get { return m_damage != TurretDamage.None; } }

        public override Vector2 Position { get { return RotationCenter; } }

        private Vector2 RotationCenter { get; set; }
        private IObjectWeldJoint m_bodyJoint;
        private IObjectAlterCollisionTile m_alterCollisionTile;

        private Dictionary<float, IObject> m_ejectedCasings = new Dictionary<float, IObject>();

        public static readonly int TotalAmmo = Game.IsEditorTest ? 1000 : 300; // TODO: Remove
        private int m_currentAmmo = TotalAmmo;
        public int CurrentAmmo { get { return m_currentAmmo == -1 ? 0 : m_currentAmmo; } }

        private IPlayer m_currentTarget;
        public IPlayer CurrentTarget
        {
            get { return m_currentTarget; }
            private set
            {
                if (CurrentTarget == null || CurrentTarget.UniqueID != value.UniqueID)
                {
                    m_currentTarget = value;
                }
            }
        }

        private int m_direction = -1;
        public int Direction
        {
            get { return m_direction; }
            private set { m_direction = value; }
        }

        public Vector2 AimVector { get { return ScriptHelper.GetDirection(Angle); } }

        private static readonly float _Deg10 = -0.175f; // -10deg
        private float MinAngle
        {
            get
            {
                return Direction > 0 ?
                    _Deg10
                    :
                    MathHelper.PI - MathHelper.PIOver4;
            }
        }
        private float MaxAngle
        {
            get
            {
                return Direction > 0 ?
                    MathHelper.PIOver4
                    :
                    MathHelper.PI - _Deg10;
            }
        }

        private float[] NormalizeMinMaxAngle(float min, float max)
        {
            // make sure it's in -45deg -> 45deg range instead of 320deg -> 45deg
            if (Math.Abs(max - min) > MathHelper.PI)
            {
                min = (float)MathExtension.NormalizeAngle(min);
                max = (float)MathExtension.NormalizeAngle(max);

                if (Math.Abs(max - min) > MathHelper.PI)
                    return new float[] { max - MathHelper.TwoPI, min };
            }
            return new float[] { min, max };
        }
        private float NormalizeAngle(float angle)
        {
            angle = (float)MathExtension.NormalizeAngle(angle);

            if (angle <= MathHelper.TwoPI && angle >= MathExtension.PI_3Over2)
                angle -= MathHelper.TwoPI;

            return angle;
        }
        public float Angle
        {
            get
            {
                if (Direction > 0)
                    return m_rotor.Object.GetAngle();
                return m_rotor.Object.GetAngle() + MathHelper.PI;
            }
            private set
            {
                var minCompAngle = Direction > 0 ? MinAngle : MinAngle - MathHelper.PI;
                var maxCompAngle = Direction > 0 ? MaxAngle : MaxAngle - MathHelper.PI;
                var minMax = NormalizeMinMaxAngle(minCompAngle, maxCompAngle);
                var angle = NormalizeAngle(Direction > 0 ? value : value - MathHelper.PI);

                //ScriptHelper.LogDebugF("{0:0} {1:0} {2:0} {3:0}, {4}",
                //    minMax[0] * 180 / Math.PI,
                //    minMax[1] * 180 / Math.PI,
                //    angle * 180 / Math.PI,
                //    m_targetAngle * 180 / Math.PI,
                //    CurrentTarget != null ? CurrentTarget.Name : "None"
                //    );

                angle = MathHelper.Clamp(angle, minMax[0], minMax[1]);
                m_rotor.Object.SetAngle(angle, updateConnectedObjects: true);
                // muzzle effect's angle is m_tip's Angle. I blame Gurt for this
                m_tip.SetAngle(Direction > 0 ? angle : angle - MathHelper.PI);
            }
        }

        private Vector2 FirePosition
        {
            get { return RotationCenter + AimVector * 22; }
        }

        private TurretState m_state = TurretState.Idle;

        enum TurretState
        {
            Idle,
            Firing,
        }

        private string GetColor(PlayerTeam team)
        {
            switch (team)
            {
                case PlayerTeam.Team1:
                    return "BgBlue";
                case PlayerTeam.Team2:
                    return "BgRed";
                case PlayerTeam.Team3:
                    return "BgGreen";
                case PlayerTeam.Team4:
                    return "BgYellow";
                default:
                    return "BgLightGray";
            }
        }

        public Turret(Vector2 worldPosition, TurretDirection direction, IPlayer owner = null)
        {
            Direction = (direction == TurretDirection.Left) ? -1 : 1;
            Owner = owner;
            Team = owner.GetTeam();

            var ux = Vector2.UnitX * -Direction;
            var uy = Vector2.UnitY;
            
            // worldPosition works best when get from player.GetWorldPosition()
            RotationCenter = worldPosition - ux * 10 + uy * 9;
            var legLeft1Position = RotationCenter - ux * 4 + uy * 1;
            var legLeft2Position = RotationCenter - ux * 7 - uy * 5;
            var legRight1Position = RotationCenter + ux * 0 - uy * 5;
            var legRight2Position = RotationCenter + ux * 4 - uy * 13;
            var legMiddle1Position = RotationCenter - ux * 2.8f - uy * 3;
            var legMiddle2Position = RotationCenter - ux * 2.8f - uy * 6;
            var ammoPosition = RotationCenter + ux * 2 - uy * 2;
            var comp1Position = RotationCenter - ux * 5 + uy * 2;
            var controllerPosition = RotationCenter + ux * 0 + uy * 1;
            var sensorPosition = RotationCenter + ux * 5 + uy * 2;
            var rotorPosition = RotationCenter + ux * 0 - uy * 1;
            var barrelSolid1Position = RotationCenter - ux * 8 - uy * 2;
            var barrelSolid2Position = RotationCenter - ux * 16 - uy * 2;
            var barrelPosition = RotationCenter - ux * 6 + uy * 2;
            var tipPosition = RotationCenter - ux * 24 + uy * 0;
            var teamIndicatorPosition = RotationCenter - ux * 11f - uy * 1;

            // Object creation order is important. It will determine the z-layer the object will be located to
            // TODO: teamIndicator rotation does not work: https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&t=3954
            var teamIndicator = Game.CreateObject("BgBottle00D", teamIndicatorPosition, -Direction * MathHelper.PIOver2);
            teamIndicator.SetColor1(GetColor(owner.GetTeam()));

            var legMiddle1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legMiddle1Position, -Direction * 0.41f);
            var legMiddle2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legMiddle2Position, -Direction * 0.41f);
            var legLeft1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legLeft1Position);
            var legLeft2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legLeft2Position);
            var legRight1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legRight1Position, MathHelper.PI);
            var legRight2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legRight2Position, MathHelper.PI);
            var ammo = (IObjectActivateTrigger)Game.CreateObject("AmmoStash00", ammoPosition);

            var comp1 = Game.CreateObject("ItemDebrisDark00", comp1Position);
            m_controller = new Component("ItemDebrisShiny00", controllerPosition);
            m_sensor = new Component("ItemDebrisDark00", sensorPosition);
            m_rotor = new Component("MetalDebris00A", rotorPosition);

            var barrel1 = Game.CreateObject("MetalPlat01A", barrelSolid1Position);
            var barrel2 = Game.CreateObject("MetalPlat01B", barrelSolid2Position);
            m_barrel = new Component("JusticeStatue00Scales", barrelPosition);
            m_tip = Game.CreateObject("LedgeGrab", tipPosition);

            legMiddle1.SetEnabled(false);
            legMiddle2.SetEnabled(false);
            legLeft1.SetEnabled(false);
            legLeft2.SetEnabled(false);
            legRight1.SetEnabled(false);
            legRight2.SetEnabled(false);
            ammo.SetEnabled(false);

            legLeft1.SetFaceDirection(-Direction);
            legLeft2.SetFaceDirection(-Direction);
            legRight1.SetFaceDirection(Direction);
            legRight2.SetFaceDirection(Direction);
            legMiddle1.SetFaceDirection(-Direction);
            legMiddle2.SetFaceDirection(-Direction);

            ammo.SetFaceDirection(-Direction);
            barrel1.SetFaceDirection(-Direction);
            barrel2.SetFaceDirection(-Direction);
            m_barrel.Object.SetFaceDirection(-Direction);
            m_tip.SetFaceDirection(-Direction);
            comp1.SetFaceDirection(-Direction);
            m_controller.Object.SetFaceDirection(-Direction);
            m_sensor.Object.SetFaceDirection(-Direction);
            m_rotor.Object.SetFaceDirection(-Direction);

            m_rotor.Object.SetBodyType(BodyType.Static); // Using WeldJoint, one static obj is enough

            m_bodyJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
            m_alterCollisionTile = (IObjectAlterCollisionTile)Game.CreateObject("AlterCollisionTile");

            legMiddle1.CustomID = "TurretLegMiddle1";
            legMiddle2.CustomID = "TurretLegMiddle2";
            legLeft1.CustomID = "TurretLegLeft1";
            legLeft2.CustomID = "TurretLegLeft2";
            legRight1.CustomID = "TurretLegRight1";
            legRight2.CustomID = "TurretLegRight2";
            ammo.CustomID = "TurretAmmo";
            barrel1.CustomID = "TurretBarrel1";
            barrel2.CustomID = "TurretBarrel2";
            m_barrel.CustomID = "TurretBarrel3";
            m_tip.CustomID = "TurretTip";
            teamIndicator.CustomID = "TurretTeamIndicator";
            comp1.CustomID = "TurretComp1";
            m_controller.CustomID = "TurretController";
            m_sensor.CustomID = "TurretSensor";
            m_rotor.CustomID = "TurretRotor";

            m_components.Add(legMiddle1.CustomID, legMiddle1); 
            m_components.Add(legMiddle2.CustomID, legMiddle2);
            m_components.Add(legLeft1.CustomID, legLeft1);
            m_components.Add(legLeft2.CustomID, legLeft2);
            m_components.Add(legRight1.CustomID, legRight1);
            m_components.Add(legRight2.CustomID, legRight2);
            m_components.Add(ammo.CustomID, ammo);
            m_components.Add(barrel1.CustomID, barrel1);
            m_components.Add(barrel2.CustomID, barrel2);
            m_components.Add(m_barrel.CustomID, m_barrel.Object);
            m_components.Add(m_tip.CustomID, m_tip);
            m_components.Add(teamIndicator.CustomID, teamIndicator);
            m_components.Add(comp1.CustomID, comp1);
            m_components.Add(m_controller.CustomID, m_controller.Object);
            m_components.Add(m_sensor.CustomID, m_sensor.Object);
            m_components.Add(m_rotor.CustomID, m_rotor.Object);

            // The reason I randomize health for each part is because bullets will likely shoot
            // through all the parts at once, so part with lowest hp will be destroyed first,
            // which is predictable, which is bad
            var healths = RandomHelper.Shuffle(new List<float>() { 50, 150, 250, 300 });
            m_barrel.MaxHealth = healths[0];
            m_rotor.MaxHealth = healths[1];
            m_sensor.MaxHealth = healths[2];
            m_controller.MaxHealth = healths[3];

            m_damageableComponents.Add(m_barrel);
            m_damageableComponents.Add(m_rotor);
            m_damageableComponents.Add(m_sensor);
            m_damageableComponents.Add(m_controller);

            foreach (var dc in m_damageableComponents)
                dc.RemoveWhenDestroyed = false;

            foreach (var component in m_components.Values)
                RegisterComponent(component);

            m_alterCollisionTile.SetDisableCollisionTargetObjects(true);
            RotateTo(Direction > 0 ? 0 : MathHelper.PI);
        }

        private void RegisterComponent(IObject obj)
        {
            if (!obj.CustomID.StartsWith("TurretLeg"))
            {
                m_bodyJoint.AddTargetObject(obj);
            }

            m_alterCollisionTile.AddTargetObject(obj);
        }

        private float m_fireCooldown = 0;
        public override void Update(float elapsed)
        {
            Game.DrawText(string.Format("{0}/{1}", CurrentAmmo, TotalAmmo), RotationCenter - Vector2.UnitY * 15);

            UpdateRotation(elapsed);
            UpdateCasings();
            UpdateBrokenEffects(elapsed);

            switch (m_state)
            {
                case TurretState.Idle:
                    if (CurrentAmmo > 0)
                        ScanTargets(elapsed);
                    break;
                case TurretState.Firing:
                    if (CurrentAmmo > 0)
                        ScanTargets(elapsed);

                    m_fireCooldown += elapsed;
                    if (m_fireCooldown >= 75)
                    {
                        if (m_currentAmmo > 0)
                            Fire();
                        if (m_currentAmmo == 0)
                        {
                            m_currentAmmo--;
                            Game.PlayEffect(EffectName.CustomFloatText, FirePosition, "Out of ammo");
                            Game.PlaySound("OutOfAmmoHeavy", FirePosition);

                            for (uint i = 1; i <= 6; i++)
                            {
                                ScriptHelper.Timeout(() => Game.PlaySound("OutOfAmmoHeavy", FirePosition), 75 * i);
                            }
                            StopFiring();
                        }
                        m_fireCooldown = 0;
                    }
                    break;
            }

        }

        private float m_sparkEffectTime = 0f;
        private float m_smokeEffectTime = 0f;
        private void UpdateBrokenEffects(float elapsed)
        {
            if (HasDamage(TurretDamage.SensorDamaged)
                || HasDamage(TurretDamage.RotorDamaged)
                || HasDamage(TurretDamage.ControllerDamaged))
            {
                var effectTime = 1500;
                if (HasDamage(TurretDamage.SensorDamaged)) effectTime -= 200;
                if (HasDamage(TurretDamage.RotorDamaged)) effectTime -= 200;
                if (HasDamage(TurretDamage.ControllerDamaged)) effectTime -= 200;

                m_sparkEffectTime += elapsed;
                if (m_sparkEffectTime >= effectTime)
                {
                    if (RandomHelper.Boolean()) m_sparkEffectTime -= RandomHelper.Between(0, 1500);
                    else
                    {
                        m_sparkEffectTime = 0f;
                        Game.PlayEffect(EffectName.ItemGleam, m_rotor.Object.GetWorldPosition());
                        Game.PlayEffect(EffectName.DestroyMetal, m_rotor.Object.GetWorldPosition());
                        Game.PlaySound("ElectricSparks", m_rotor.Object.GetWorldPosition());
                    }
                }
            }

            if (HasDamage(TurretDamage.BarrelDamaged))
            {
                m_smokeEffectTime += elapsed;

                if (m_smokeEffectTime >= 500)
                {
                    Game.PlayEffect(EffectName.Dig, FirePosition);
                    m_smokeEffectTime = 0f;
                }
            }
        }

        private void UpdateCasings()
        {
            var removedList = new List<float>();
            foreach (var casing in m_ejectedCasings)
            {
                var timeAdded = casing.Key;
                if (ScriptHelper.IsElapsed(timeAdded, 700))
                {
                    removedList.Add(timeAdded);
                }
            }
            foreach (var i in removedList)
            {
                m_ejectedCasings[i].Remove();
                m_ejectedCasings.Remove(i);
            }
        }

        public bool HasDamage(TurretDamage damage) { return (m_damage & damage) == damage; }

        private void OnBodyDamage(TurretDamage damage, string displayText)
        {
            Game.PlayEffect(EffectName.CustomFloatText, RotationCenter + Vector2.UnitY * 5, displayText);
            Game.PlayEffect(EffectName.Electric, RotationCenter);
            Game.PlayEffect(EffectName.Electric, RotationCenter);
            Game.PlaySound("ElectricSparks", RotationCenter);
            m_damage = m_damage | damage;
        }
        public override void OnDamage(IObject obj, ObjectDamageArgs args)
        {
            foreach (var dc in m_damageableComponents)
            {
                dc.OnDamage(args);
            }

            // https://www.alanzucconi.com/2015/07/26/enum-flags-and-bitwise-operators/
            if (m_barrel.Health == 0 && !HasDamage(TurretDamage.BarrelDamaged))
            {
                Game.PlayEffect(EffectName.CustomFloatText, RotationCenter + Vector2.UnitY * 5, "Barrel Damaged");
                m_damage = m_damage | TurretDamage.BarrelDamaged;
            }
            if (m_rotor.Health == 0 && !HasDamage(TurretDamage.RotorDamaged))
            {
                OnBodyDamage(TurretDamage.RotorDamaged, "Rotor Damaged");
            }
            if (m_sensor.Health == 0 && !HasDamage(TurretDamage.SensorDamaged))
            {
                OnBodyDamage(TurretDamage.SensorDamaged, "Sensor Damaged");
            }
            if (m_controller.Health == 0 && !HasDamage(TurretDamage.ControllerDamaged))
            {
                OnBodyDamage(TurretDamage.ControllerDamaged, "Controller Damaged");
            }
        }

        public static readonly float Range = 500;
        private Vector2[] GetLineOfSight(Vector2 direction)
        {
            direction.Normalize();
            return new Vector2[]
            {
                RotationCenter + direction * 22,
                RotationCenter + direction * (Range + 22),
            };
        }

        private List<IPlayer> GetPlayersInRange()
        {
            var scanRange = Range + 22;
            // TODO: remove hardcode numbers
            var area = new Area(
                RotationCenter - Vector2.UnitY * 91,
                RotationCenter + Vector2.UnitX * scanRange * Direction + Vector2.UnitY * (Range - 130));
            area.Normalize();

            Game.DrawLine(RotationCenter, RotationCenter + ScriptHelper.GetDirection(MinAngle) * scanRange, Color.Cyan);
            Game.DrawLine(RotationCenter, RotationCenter + ScriptHelper.GetDirection(MaxAngle) * scanRange, Color.Cyan);

            //Game.DrawArea(area);
            var players = Game.GetObjectsByArea<IPlayer>(area)
                .Where((p) => ScriptHelper.IsTouchingCircle(p.GetAABB(), RotationCenter, scanRange, MinAngle, MaxAngle)
                && !p.IsDead)
                .ToList();

            // nearest player first
            players.Sort((p1, p2) =>
            {
                var p1Distance = Vector2.Distance(p1.GetWorldPosition(), RotationCenter);
                var p2Distance = Vector2.Distance(p2.GetWorldPosition(), RotationCenter);
                if (p1Distance < p2Distance)
                    return -1;
                return 1;
            });

            return players;
        }

        private IEnumerable<IPlayer> RayCast(Vector2 start, Vector2 end)
        {
            if (HasDamage(TurretDamage.SensorDamaged))
            {
                foreach (var result in RayCastHelper.PlayersInSight(start, end))
                    yield return Game.GetPlayer(result.ObjectID);
            }
            else
            {
                foreach (var result in RayCastHelper.PlayersInSight(start, end, true, Team, Owner))
                    yield return Game.GetPlayer(result.ObjectID);
            }
        }

        private float m_changeTargetCooldown = 0f;
        private void ScanTargets(float elasped)
        {
            var players = GetPlayersInRange();
            var targetDirection = Vector2.Zero;
            IPlayer target = null;

            foreach (var player in players)
            {
                var direction = player.GetWorldPosition() - RotationCenter;
                var los = GetLineOfSight(direction);
                var targets = RayCast(los[0], los[1]);

                if (targets.Count() > 0)
                {
                    targetDirection = direction;
                    target = player; break;
                }
            }

            m_changeTargetCooldown += elasped;

            if (target != null && m_changeTargetCooldown > 500)
            {
                m_changeTargetCooldown = 0;
                CurrentTarget = target;
                RotateTo(ScriptHelper.GetAngle(targetDirection));
            }

            CheckFire(target);
        }

        private void CheckFire(IPlayer target)
        {
            if (target == null)
            {
                StopFiring();
                return;
            }

            var los = GetLineOfSight(AimVector);
            var players = RayCast(los[0], los[1]);
            Game.DrawLine(los[0], los[1], Color.Green);

            if (!players.Any())
            {
                StopFiring();
                return;
            }

            foreach (var player in players)
            {
                if (player.UniqueID == target.UniqueID)
                {
                    StartFiring();
                    break;
                }
            }
        }

        private float m_rotateTimer = 0;
        private float m_targetAngle = 0;
        private void RotateTo(float angle)
        {
            if (HasDamage(TurretDamage.RotorDamaged))
                return;
            m_targetAngle = NormalizeAngle(angle);
        }

        private float m_rndRotationTime = 0f;
        private void UpdateRotation(float elapsed)
        {
            if (HasDamage(TurretDamage.ControllerDamaged))
            {
                m_rndRotationTime += elapsed;
                if (m_rndRotationTime >= 1000)
                {
                    if (RandomHelper.Boolean())
                    {
                        RotateTo(RandomHelper.Between(MinAngle, MaxAngle));
                        m_rndRotationTime = 0f;
                    }
                    else
                        m_rndRotationTime -= RandomHelper.Between(0, 500);
                }
            }

            if (Math.Abs(Angle - m_targetAngle) > 0.0174f)
            {
                m_rotateTimer += elapsed;
                if (m_rotateTimer >= 1/60)
                {
                    if (m_targetAngle > Angle)
                        Angle+= .0174f;
                    else
                        Angle-= .0174f;
                    m_rotateTimer = 0;
                }
            }
        }

        private void Fire()
        {
            if (HasDamage(TurretDamage.BarrelDamaged))
                return;

            var aimVector = AimVector;
            var oneDeg = .0174f;

            aimVector.X += RandomHelper.Between(-oneDeg, oneDeg);
            aimVector.Y += RandomHelper.Between(-oneDeg, oneDeg);

            Game.SpawnProjectile(ProjectileItem.M60, FirePosition, aimVector);
            // More info about muzzle effect
            // https://www.mythologicinteractiveforums.com/viewtopic.php?p=23313#p23313
            Game.PlayEffect("MZLED", Vector2.Zero, m_tip.UniqueID, "MuzzleFlashAssaultRifle");
            Game.PlaySound("Magnum", FirePosition);
            m_currentAmmo--;

            var emittedAngle = (Direction > 0 ? 90 + 45 : 45) + RandomHelper.Between(-.3f, .3f);
            var casing = Game.CreateObject("WpnC4Detonator", RotationCenter, MathHelper.PIOver2,
                ScriptHelper.GetDirection(emittedAngle) * 4, RandomHelper.Between(-5, 5));
            m_ejectedCasings.Add(Game.TotalElapsedGameTime, casing);
        }

        private void StartFiring() { if (m_state != TurretState.Firing) m_state = TurretState.Firing; }
        private void StopFiring() { if (m_state != TurretState.Idle) m_state = TurretState.Idle; }
    }
}
