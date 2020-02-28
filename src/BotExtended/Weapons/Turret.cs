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
        
        private IObject m_rotationPoint;
        private Vector2 RotationCenter { get { return m_rotationPoint.GetWorldPosition(); } }
        private IObjectWeldJoint m_bodyJoint;

        public static readonly int TotalAmmo = Game.IsEditorTest ? 1000 : 300; // TODO
        private int m_currentAmmo = TotalAmmo;
        public int CurrentAmmo { get { return m_currentAmmo == -1 ? 0 : m_currentAmmo; } }

        private int m_direction = -1;
        public int Direction
        {
            get { return m_direction; }
            private set { m_direction = value; }
        }

        public Vector2 AimVector { get { return ScriptHelper.GetDirection(Angle); } }

        private float LowerAngle
        {
            get
            {
                var _deg10 = -0.175f; // -10deg
                return Direction > 0 ? _deg10 : MathHelper.PI - _deg10;
            }
        }
        private float UpperAngle
        {
            get
            {
                var deg45 = 0.785f; // 45deg
                return Direction > 0 ? deg45 : MathHelper.PI - deg45;
            }
        }
        public float Angle
        {
            get
            {
                if (Direction > 0)
                    return m_components.Last().GetAngle();
                return (m_components.Last().GetAngle() + MathHelper.PI);
            }
            set
            {
                var lowerCompAngle = Direction > 0 ? LowerAngle : LowerAngle - MathHelper.PI;
                var upperCompAngle = Direction > 0 ? UpperAngle : UpperAngle - MathHelper.PI;
                var angle = Direction > 0 ? value : value - MathHelper.PI;

                angle = MathHelper.Clamp(angle,
                    Math.Min(lowerCompAngle, upperCompAngle),
                    Math.Max(lowerCompAngle, upperCompAngle));
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

            // {X:110 Y:-204} Player

            // x:175 y:-180 TurretRotationPoint
            // x:172 y:-179 TurretLegLeft1
            // x:169 y:-185 TurretLegLeft2
            // x:176 y:-185 TurretLegRight1
            // x:180 y:-193 TurretLegRight2
            // x:173,2 y:-183 TurretLegMiddle1
            // x:173,2 y:-186 TurretLegMiddle2
            // x:178 y:-182 TurretAmmo
            // x:171 y:-178 TurretComp1
            // x:176 y:-179 TurretComp2
            // x:181 y:-178 TurretComp3
            // x:176 y:-180 TurretComp4
            // x:158 y:-183 TurretBarrel1
            // x:161 y:-183 TurretBarrel2
            // x:169 y:-183 TurretBarrel3

            m_rotationPoint = Game.CreateObject("RevoluteJoint", rotationPointPosition);
            UniqueID = m_rotationPoint.UniqueID;

            // Object creation order is important. It will determine the z-layer the object will be located to
            var legMiddle1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legMiddle1Position, -Direction * 0.41f);
            var legMiddle2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legMiddle2Position, -Direction * 0.41f);
            var legLeft1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legLeft1Position);
            var legLeft2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legLeft2Position);
            var legRight1 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legRight1Position,
                (float)MathExtension.ToRadians(Direction * 180));
            var legRight2 = (IObjectActivateTrigger)Game.CreateObject("Lever01", legRight2Position,
                (float)MathExtension.ToRadians(Direction * 180));
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

            // TODO: remove
            //Events.PlayerKeyInputCallback.Start(OnKeyInput);
        }

        private void OnKeyInput(IPlayer player, VirtualKeyInfo[] keyEvents)
        {
            for (var i = 0; i < keyEvents.Length; i++)
            {
                if (player.KeyPressed(VirtualKey.JUMP))
                {
                    Angle += (float)MathExtension.ToRadians(-1);
                }
                if (player.KeyPressed(VirtualKey.CROUCH_ROLL_DIVE))
                {
                    Angle += (float)MathExtension.ToRadians(1);
                }
                if (player.KeyPressed(VirtualKey.RELOAD))
                {
                    Fire();
                }
            }
        }

        private float m_fireCooldown = 0;
        public void Update(float elapsed)
        {
            ScanTargets();

            Game.DrawText(string.Format("{0}/{1}", CurrentAmmo, TotalAmmo), RotationCenter - Vector2.UnitY * 15);

            switch (m_state)
            {
                case TurretState.Idle:
                    break;
                case TurretState.Firing:
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
        private Vector2[] GetLineOfSight()
        {
            return new Vector2[]
            {
                FirePosition,
                FirePosition + AimVector * Range,
            };
        }

        private void ScanTargets()
        {
            var los = GetLineOfSight();
            var targets = ScriptHelper.RayCastPlayers(los[0], los[1], true, Owner);
            var hasTargets = false;
            var scanRange = Range + 22;

            foreach (var target in targets)
            {
                if (!target.IsDead && !target.IsRemoved)
                {
                    hasTargets = true; break;
                }
            }

            Game.DrawLine(los[0], los[1], hasTargets ? Color.Green : Color.Red);
            Game.DrawLine(RotationCenter, RotationCenter + ScriptHelper.GetDirection(LowerAngle) * scanRange, Color.Cyan);
            Game.DrawLine(RotationCenter, RotationCenter + ScriptHelper.GetDirection(UpperAngle) * scanRange, Color.Cyan);

            var area = new Area(
                RotationCenter + Vector2.UnitX * scanRange * Direction + Vector2.UnitY * (Range - 130),
                RotationCenter - Vector2.UnitY * 91);

            Game.DrawArea(area);

            if (hasTargets)
            {
                if (m_state != TurretState.Firing)
                    m_state = TurretState.Firing;
            }
            else
            {
                if (m_state != TurretState.Idle)
                    m_state = TurretState.Idle;
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
    }
}
