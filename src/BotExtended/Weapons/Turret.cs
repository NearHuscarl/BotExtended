using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Weapons
{
    enum TurretDirection
    {
        Left,
        Right,
    }

    //var turrentComps = Game.GetObjectsByArea(new Area(-186, 354f, -218, 427f));
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

    class Turret : IWeapon
    {
        public readonly int UniqueID;
        public IPlayer Owner { get; private set; }

        private List<IObject> m_components = new List<IObject>();
        public List<IObject> Components { get { return m_components; } }

        // TODO
        public bool Broken { get { return false; } }

        public Vector2 Position { get { return m_rotationPoint.GetWorldPosition(); } }
        
        private IObject m_rotationPoint;
        private Vector2 RotationCenter { get { return m_rotationPoint.GetWorldPosition(); } }
        private IObjectWeldJoint m_bodyJoint;

        public static readonly int TotalAmmo = 300;
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
                    //var direction =
                    //Angle = ScriptHelper.GetAngle(targetDirection);
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
                    return m_components.Last().GetAngle();
                return m_components.Last().GetAngle() + MathHelper.PI;
            }
            private set
            {
                var minCompAngle = Direction > 0 ? MinAngle : MinAngle - MathHelper.PI;
                var maxCompAngle = Direction > 0 ? MaxAngle : MaxAngle - MathHelper.PI;
                var minMax = NormalizeMinMaxAngle(minCompAngle, maxCompAngle);
                var angle = NormalizeAngle(Direction > 0 ? value : value - MathHelper.PI);

                //ScriptHelper.LogDebug(string.Format("{0:0} {1:0} {2:0} {3:0}",
                //    angle * 180 / Math.PI,
                //    minMax[0] * 180 / Math.PI,
                //    minMax[1] * 180 / Math.PI,
                //    m_targetAngle * 180 / Math.PI
                //    ));

                angle = MathHelper.Clamp(angle, minMax[0], minMax[1]);
                m_components.Last().SetAngle(angle, updateConnectedObjects: true);
            }
        }

        private Vector2 FirePosition
        {
            get { return RotationCenter + AimVector * 22; }
        }

        public float Health = 50;
        public PlayerTeam Team = PlayerTeam.Team4;

        private TurretState m_state = TurretState.Idle;

        enum TurretState
        {
            Idle,
            Firing,
            Broken,
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

            var ux = Vector2.UnitX * -Direction;
            var uy = Vector2.UnitY;
            
            // worldPosition works best when get from player.GetWorldPosition()
            var rotationPointPosition = worldPosition - ux * 10 + uy * 9;
            var legLeft1Position = rotationPointPosition - ux * 3 + uy * 1;
            var legLeft2Position = rotationPointPosition - ux * 6 - uy * 5;
            var legRight1Position = rotationPointPosition + ux * 1 - uy * 5;
            var legRight2Position = rotationPointPosition + ux * 5 - uy * 13;
            var legMiddle1Position = rotationPointPosition - ux * 1.8f - uy * 3;
            var legMiddle2Position = rotationPointPosition - ux * 1.8f - uy * 6;
            var ammoPosition = rotationPointPosition + ux * 3 - uy * 2;
            var comp1Position = rotationPointPosition - ux * 4 + uy * 2;
            var comp2Position = rotationPointPosition + ux * 1 + uy * 1;
            var comp3Position = rotationPointPosition + ux * 6 + uy * 2;
            var comp4Position = rotationPointPosition + ux * 1 + uy * 0;
            var barrel1Position = rotationPointPosition - ux * 17 - uy * 3;
            var barrel2Position = rotationPointPosition - ux * 14 - uy * 3;
            var barrel3Position = rotationPointPosition - ux * 6 - uy * 3;
            var teamIndicatorPosition = rotationPointPosition - ux * 9.8f - uy * 1;

            m_rotationPoint = Game.CreateObject("RevoluteJoint", rotationPointPosition);
            UniqueID = m_rotationPoint.UniqueID;

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
            var comp2 = Game.CreateObject("ItemDebrisShiny00", comp2Position);
            var comp3 = Game.CreateObject("ItemDebrisDark00", comp3Position);
            var comp4 = Game.CreateObject("MetalDebris00A", comp4Position);

            var barrel1 = Game.CreateObject("Rug00A_D", barrel1Position);
            var barrel2 = Game.CreateObject("Rug00A", barrel2Position);
            var barrel3 = Game.CreateObject("Rug00A", barrel3Position);

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
            barrel3.SetFaceDirection(-Direction);
            comp1.SetFaceDirection(-Direction);
            comp2.SetFaceDirection(-Direction);
            comp3.SetFaceDirection(-Direction);
            comp4.SetFaceDirection(-Direction);

            comp1.SetBodyType(BodyType.Static);
            comp2.SetBodyType(BodyType.Static);
            comp3.SetBodyType(BodyType.Static);
            comp4.SetBodyType(BodyType.Static);

            m_bodyJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
            var alterCollisionTile = (IObjectAlterCollisionTile)Game.CreateObject("AlterCollisionTile");

            m_components.Add(legMiddle1); legMiddle1.CustomID = "LegMiddle1";
            m_components.Add(legMiddle2); legMiddle2.CustomID = "LegMiddle2";
            m_components.Add(legLeft1); legLeft1.CustomID = "LegLeft1";
            m_components.Add(legLeft2); legLeft2.CustomID = "LegLeft2";
            m_components.Add(legRight1); legRight1.CustomID = "LegRight1";
            m_components.Add(legRight2); legRight2.CustomID = "LegRight2";
            m_components.Add(ammo); ammo.CustomID = "Ammo";
            m_components.Add(barrel1); barrel1.CustomID = "Barrel1";
            m_components.Add(barrel2); barrel2.CustomID = "Barrel2";
            m_components.Add(barrel3); barrel3.CustomID = "Barrel3";
            m_components.Add(teamIndicator); teamIndicator.CustomID = "TeamIndicator";
            m_components.Add(comp1); comp1.CustomID = "Comp1";
            m_components.Add(comp2); comp2.CustomID = "Comp2";
            m_components.Add(comp3); comp3.CustomID = "Comp3";
            m_components.Add(comp4); comp4.CustomID = "Comp4";

            foreach (var component in m_components)
            {
                if (!component.CustomID.StartsWith("Leg"))
                    m_bodyJoint.AddTargetObject(component);

                alterCollisionTile.AddTargetObject(component);
            }
            m_bodyJoint.AddTargetObject(m_rotationPoint);
            alterCollisionTile.SetDisableCollisionTargetObjects(true);

            Angle = Direction > 0 ? 0 : MathHelper.PI;
        }

        private float m_fireCooldown = 0;
        public void Update(float elapsed)
        {
            Game.DrawText(string.Format("{0}/{1}", CurrentAmmo, TotalAmmo), RotationCenter - Vector2.UnitY * 15);

            UpdateRotation(elapsed);

            switch (m_state)
            {
                case TurretState.Idle:
                    ScanTargets(elapsed);
                    break;
                case TurretState.Firing:
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
                        }
                        m_fireCooldown = 0;
                    }
                    break;
                case TurretState.Broken:
                    break;
            }

        }

        public void OnDamage(IObject component)
        {
            ScriptHelper.LogDebug(string.Format("{0} comp is damaged", component.CustomID));
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
                var targets = ScriptHelper.RayCastPlayers(los[0], los[1], true, Owner);

                if (targets.Count() > 0)
                {
                    targetDirection = direction;
                    CurrentTarget = player;
                    target = player; break;
                }
            }

            m_changeTargetCooldown += elasped;

            if (target != null
                && (CurrentTarget == null || CurrentTarget.UniqueID != target.UniqueID)
                && m_changeTargetCooldown > 500)
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
            Game.DrawLine(los[0], los[1], Color.Green);
            var players = ScriptHelper.RayCastPlayers(los[0], los[1], true, Owner);

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
            m_targetAngle = NormalizeAngle(angle);
        }

        private void UpdateRotation(float elapsed)
        {
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
            var aimVector = AimVector;
            var oneDeg = .0174f;

            aimVector.X += RandomHelper.Between(-oneDeg, oneDeg);
            aimVector.Y += RandomHelper.Between(-oneDeg, oneDeg);

            Game.SpawnProjectile(ProjectileItem.M60, FirePosition, aimVector);
            Game.PlaySound("Magnum", FirePosition);
            m_currentAmmo--;
        }

        private void StartFiring() { if (m_state != TurretState.Firing) m_state = TurretState.Firing; }
        private void StopFiring() { if (m_state != TurretState.Idle) m_state = TurretState.Idle; }
    }
}
