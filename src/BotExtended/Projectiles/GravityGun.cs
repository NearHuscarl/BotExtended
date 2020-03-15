using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Projectiles
{
    class GravityGun : RangeWpn
    {
        private static readonly Vector2 FarAwayPosition = Game.GetCameraMaxArea().BottomLeft - Vector2.UnitX * 10;

        public GravityGun(IPlayer owner, WeaponItem name) : base(owner)
        {
            Powerup = RangedWeaponPowerup.Gravity;
            Name = name;

            m_invisibleMagnet = Game.CreateObject("BgCarnivalLight03A");
            m_invisibleMagnet.SetBodyType(BodyType.Static);
            var farBg = Game.CreateObject("FarBgBlimp00");
            m_invisibleMagnet.SetCollisionFilter(farBg.GetCollisionFilter());
            m_invisibleMagnet.SetWorldPosition(FarAwayPosition);

            m_magnetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");
            m_magnetJoint.SetTargetObject(m_invisibleMagnet);

            m_pullJoint = CreatePullJointObject();
        }

        private static readonly float Range = 80;

        private IObject m_invisibleMagnet;
        private IObjectTargetObjectJoint m_magnetJoint;

        private IObjectPullJoint m_pullJoint;

        private IObject m_distanceJointObject;
        private IObjectDistanceJoint m_distanceJoint;
        private IObjectTargetObjectJoint m_targetedObjectJoint;

        private IObject m_targetedObject;
        public bool IsTargetedObjectStabilized { get; private set; }

        private Vector2 GetHoldPosition()
        {
            var aimAngle = ScriptHelper.GetAngle(Owner.AimVector);
            var neckPosition = Owner.GetWorldPosition() + Vector2.UnitY * 10 - Vector2.UnitX * Owner.FacingDirection * 2;
            var crosshairCenter = neckPosition + ScriptHelper.GetDirection(aimAngle + MathHelper.PIOver2 * Owner.FacingDirection)
                * 2 /*3 -> more aligned with crosshair but not very aligned with the gun barrel*/;

            return crosshairCenter + Owner.AimVector * 25;
        }

        private Vector2[] GetScanLine()
        {
            var holdPosition = GetHoldPosition();
            var end = holdPosition + Owner.AimVector * Range;
            Game.DrawLine(holdPosition, end);

            return new Vector2[] { holdPosition, end };
        }

        public override void Update(float elapsed, WeaponItem weapon, float currentAmmo)
        {
            base.Update(elapsed, weapon, currentAmmo);

            if (Owner.IsManualAiming)
            {
                var holdPosition = GetHoldPosition();
                m_invisibleMagnet.SetWorldPosition(holdPosition);
                // m_invisibleMagnet is a static object so the corresponding TargetObjectJoint need to be moved manually too
                m_magnetJoint.SetWorldPosition(holdPosition);

                Game.DrawArea(m_pullJoint.GetAABB(), Color.Cyan);
                Game.DrawArea(m_magnetJoint.GetAABB(), Color.Magenta);
                if (m_distanceJointObject != null)
                {
                    Game.DrawArea(m_distanceJointObject.GetAABB(), Color.Green);
                }

                if (m_targetedObject != null)
                {
                    TryStabilizeTargetObject(holdPosition);
                }

                if (Game.IsEditorTest)
                {
                    if (m_targetedObject != null)
                        Game.DrawArea(m_targetedObject.GetAABB());
                    Game.DrawCircle(GetHoldPosition(), .5f, Color.Red);

                    var to = m_pullJoint.GetTargetObject();
                    if (to != null)
                        Game.DrawArea(to.GetAABB(), Color.Yellow);
                }
            }
            else
            {
                if (IsTargetedObjectStabilized)
                    StopStabilizingTargetedObject();

                m_invisibleMagnet.SetWorldPosition(FarAwayPosition);
                m_magnetJoint.SetWorldPosition(FarAwayPosition);
            }
        }

        private void TryStabilizeTargetObject(Vector2 holdPosition)
        {
            var results = RayCastTargetObject(false);
            var stablizeZone = new Area(
                holdPosition.Y + 10,
                holdPosition.X - 10,
                holdPosition.Y - 10,
                holdPosition.X + 10
                );

            Game.DrawArea(stablizeZone);

            var targetedObjectFound = false;

            if (stablizeZone.Intersects(m_targetedObject.GetAABB()))
                targetedObjectFound = true;

            foreach (var result in results)
            {
                if (result.HitObject == null) continue;

                if (result.HitObject.UniqueID == m_targetedObject.UniqueID)
                {
                    targetedObjectFound = true;

                    if (stablizeZone.Intersects(m_targetedObject.GetAABB()))
                    {
                        if (!IsTargetedObjectStabilized)
                        {
                            StabilizeTargetedObject();
                            IsTargetedObjectStabilized = true;
                        }
                    }
                    break;
                }
            }

            if (!targetedObjectFound)
            {
                StopStabilizingTargetedObject();
            }
        }

        private void StopStabilizingTargetedObject()
        {
            if (m_distanceJointObject != null)
            {
                m_distanceJointObject.Remove();
                m_distanceJoint.Remove();
                m_targetedObjectJoint.Remove();
            }

            m_pullJoint.SetTargetObject(null);
            m_targetedObject = null;
            IsTargetedObjectStabilized = false;
        }

        private void StabilizeTargetedObject()
        {
            m_targetedObject.SetLinearVelocity(Vector2.Zero);

            m_distanceJointObject = Game.CreateObject("BgCarnivalLight03A");
            m_distanceJointObject.SetBodyType(BodyType.Dynamic);

            m_distanceJoint = (IObjectDistanceJoint)Game.CreateObject("DistanceJoint");
            m_distanceJoint.SetLineVisual(LineVisual.None);
            m_distanceJoint.SetLengthType(DistanceJointLengthType.Fixed);

            m_targetedObjectJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");

            var targetedObjPosition = m_targetedObject.GetAABB().Center;
            m_distanceJointObject.SetWorldPosition(targetedObjPosition);
            m_targetedObject.SetWorldPosition(targetedObjPosition);
            m_pullJoint.SetWorldPosition(targetedObjPosition);
            m_distanceJoint.SetWorldPosition(targetedObjPosition);
            // if DistanceJoint and TargetObjectJoint is at the same position, weird things may happen
            // uncomment the part below to stop it
            m_targetedObjectJoint.SetWorldPosition(m_distanceJointObject.GetWorldPosition()/* - Vector2.UnitY*/);

            m_pullJoint.SetTargetObject(m_distanceJointObject);
            m_distanceJoint.SetTargetObject(m_distanceJointObject);
            m_distanceJoint.SetTargetObjectJoint(m_targetedObjectJoint);
            m_targetedObjectJoint.SetTargetObject(m_targetedObject);

            IsTargetedObjectStabilized = true;
        }

        public override void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos)
        {
            base.OnPlayerKeyInput(keyInfos);

            if (!Owner.IsManualAiming)
                return;

            foreach (var keyInfo in keyInfos)
            {
                if (keyInfo.Event == VirtualKeyEvent.Pressed && keyInfo.Key == VirtualKey.SPRINT)
                {
                    PickupObject();
                }
                if (keyInfo.Event == VirtualKeyEvent.Pressed && keyInfo.Key == VirtualKey.ATTACK)
                {
                    Release();
                }
            }
        }

        private IEnumerable<RayCastResult> RayCastTargetObject(bool isSearching)
        {
            var scanLine = GetScanLine();
            var rcInput = new RayCastInput()
            {
                FilterOnMaskBits = true,
                MaskBits = 0x0018, // dynamics_g1, dynamics_g2
                ClosestHitOnly = isSearching,
            };
            var results = Game.RayCast(scanLine[0], scanLine[1], rcInput);

            foreach (var result in results)
            {
                if (result.HitObject == null || result.IsPlayer)
                    continue;

                yield return result;

                if (isSearching) break;
            }
        }

        private IObjectPullJoint CreatePullJointObject()
        {
            var pullJoint = (IObjectPullJoint)Game.CreateObject("PullJoint");

            if (m_targetedObject != null)
                pullJoint.SetWorldPosition(m_targetedObject.GetWorldPosition());

            pullJoint.SetTargetObject(m_targetedObject);
            pullJoint.SetTargetObjectJoint(m_magnetJoint);
            pullJoint.SetForce(2);

            return pullJoint;
        }

        private void PickupObject()
        {
            if (m_targetedObject == null)
            {
                var result = RayCastTargetObject(true).FirstOrDefault();

                if (result.HitObject != null)
                {
                    m_targetedObject = result.HitObject;

                    // m_targetObjectJoint.Position is fucked up if key input event fires. idk why
                    m_magnetJoint.SetWorldPosition(GetHoldPosition());
                    m_pullJoint.Remove();
                    m_pullJoint = CreatePullJointObject();
                }
            }
        }

        private void Release()
        {
            if (m_targetedObject != null)
            {
                if (m_targetedObject.GetCollisionFilter().CategoryBits == 0x10) // dynamics_g2
                {
                    m_targetedObject.SetLinearVelocity(Owner.AimVector * 1f / m_targetedObject.GetMass());
                    m_targetedObject.TrackAsMissile(true);
                }
                else
                {
                    m_targetedObject.SetLinearVelocity(Owner.AimVector * .7f / m_targetedObject.GetMass());
                }

                StopStabilizingTargetedObject();
            }
        }
    }
}
