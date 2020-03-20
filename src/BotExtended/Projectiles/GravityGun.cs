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

        public GravityGun(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner, name, powerup)
        {
            if (powerup == RangedWeaponPowerup.GravityDE)
                IsSupercharged = true;
            else if (powerup == RangedWeaponPowerup.Gravity)
                IsSupercharged = false;
            else
                throw new Exception("Unknown powerup for gravity gun: " + powerup);

            m_invisibleMagnet = Game.CreateObject("InvisibleBlockSmall");
            m_invisibleMagnet.SetBodyType(BodyType.Static);
            var farBg = Game.CreateObject("FarBgBlimp00");
            m_invisibleMagnet.SetCollisionFilter(farBg.GetCollisionFilter());
            m_invisibleMagnet.SetWorldPosition(FarAwayPosition);

            m_magnetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");
            m_magnetJoint.SetTargetObject(m_invisibleMagnet);

            m_pullJoint = CreatePullJointObject();
        }

        public static readonly float Range = 80;

        public bool IsSupercharged { get; private set; }
        public Area GetStabilizedZone()
        {
            return GetStabilizedZone(GetHoldPosition(true));
        }
        private Area GetStabilizedZone(Vector2 holdPosition)
        {
            return new Area(
                holdPosition.Y + 10,
                holdPosition.X - 10,
                holdPosition.Y - 10,
                holdPosition.X + 10);
        }

        private IObject m_invisibleMagnet;
        private IObjectTargetObjectJoint m_magnetJoint;

        private IObjectPullJoint m_pullJoint;

        private IObject m_distanceJointObject;
        private IObjectDistanceJoint m_distanceJoint;
        private IObjectTargetObjectJoint m_targetedObjectJoint;

        private IObject m_releasedObject;
        private IObject m_targetedObject;
        public IObject TargetedObject { get { return m_targetedObject; } }

        public bool IsTargetedObjectStabilized { get; private set; }

        public Vector2 GetHoldPosition(bool useOffset)
        {
            var offset = 0f;

            if (m_targetedObject != null && useOffset)
            {
                var hitbox = m_targetedObject.GetAABB();
                var length = Math.Max(hitbox.Width, hitbox.Height);
                offset = length / 2f;
            }

            var aimAngle = ScriptHelper.GetAngle(Owner.AimVector);
            var neckPosition = Owner.GetWorldPosition() + Vector2.UnitY * 10 - Vector2.UnitX * Owner.FacingDirection * 2;
            var crosshairCenter = neckPosition + ScriptHelper.GetDirection(aimAngle + MathHelper.PIOver2 * Owner.FacingDirection)
                * 2 /*3 -> more aligned with crosshair but not very aligned with the gun barrel*/;

            return crosshairCenter + Owner.AimVector * (15 + offset);
        }

        private Vector2[] GetScanLine()
        {
            var holdPosition = GetHoldPosition(false);
            var end = holdPosition + Owner.AimVector * Range;

            return new Vector2[] { holdPosition, end };
        }

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Owner.IsManualAiming)
            {
                var holdPosition = GetHoldPosition(true);
                m_invisibleMagnet.SetWorldPosition(holdPosition);
                // m_invisibleMagnet is a static object so the corresponding TargetObjectJoint need to be moved manually too
                m_magnetJoint.SetWorldPosition(holdPosition);

                if (m_targetedObject != null)
                {
                    TryStabilizeTargetedObject(holdPosition);
                }

                if (Game.IsEditorTest)
                {
                    var scanLine = GetScanLine();

                    Game.DrawLine(scanLine[0], scanLine[1]);
                    Game.DrawCircle(holdPosition, .5f, Color.Red);
                    //Game.DrawArea(m_pullJoint.GetAABB(), Color.Cyan);
                    //Game.DrawArea(m_magnetJoint.GetAABB(), Color.Magenta);

                    if (m_targetedObject != null)
                        Game.DrawArea(m_targetedObject.GetAABB(), Color.Blue);

                    //if (m_distanceJointObject != null)
                    //    Game.DrawArea(m_distanceJointObject.GetAABB(), Color.Green);

                    var to = m_pullJoint.GetTargetObject();
                    if (to != null)
                        Game.DrawArea(to.GetAABB(), Color.Yellow);
                }
            }
            else
            {
                if (IsTargetedObjectStabilized || m_targetedObject != null)
                    StopStabilizingTargetedObject();

                m_invisibleMagnet.SetWorldPosition(FarAwayPosition);
                m_magnetJoint.SetWorldPosition(FarAwayPosition);
            }
        }

        private void TryStabilizeTargetedObject(Vector2 holdPosition)
        {
            var results = RayCastTargetedObject(false);
            var stabilizedZone = GetStabilizedZone(holdPosition);

            Game.DrawArea(stabilizedZone, Color.Green);

            var targetedObjectFound = false;
            var targetHitbox = m_targetedObject.GetAABB();

            if (stabilizedZone.Intersects(targetHitbox))
                targetedObjectFound = true;

            foreach (var result in results)
            {
                if (result.HitObject == null) continue;

                if (result.HitObject.UniqueID == m_targetedObject.UniqueID)
                {
                    targetedObjectFound = true;

                    if (stabilizedZone.Intersects(targetHitbox))
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
            else
            {
                var player = m_targetedObject as IPlayer;
                if (player != null && !player.IsStaggering)
                {
                    // Not sure why StaggerInfinite is not infinite!
                    player.AddCommand(new PlayerCommand(PlayerCommandType.StaggerInfinite));
                }
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

            var player = m_targetedObject as IPlayer;
            if (player != null) player.SetInputEnabled(true);

            m_pullJoint.SetTargetObject(null);
            m_targetedObject = null;
            IsTargetedObjectStabilized = false;
        }

        private void StabilizeTargetedObject()
        {
            m_targetedObject.SetLinearVelocity(Vector2.Zero);

            m_distanceJointObject = Game.CreateObject("InvisibleBlockSmall");
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
            }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            // Remove projectile completely since gravity gun only use objects laying around the map as ammunation
            projectile.FlagForRemoval();

            // Cannot use ia 1 because we only want this particular gun to have indefinite ammo
            if (BotManager.GetBot(Owner).CurrentAmmo == 0)
            {
                if (Type == WeaponItemType.Rifle)
                    Owner.SetCurrentPrimaryWeaponAmmo(Owner.CurrentPrimaryWeapon.MaxTotalAmmo - 1);
                if (Type == WeaponItemType.Handgun)
                    Owner.SetCurrentSecondaryWeaponAmmo(Owner.CurrentSecondaryWeapon.MaxTotalAmmo - 1);
            }

            Release();
        }

        private IEnumerable<RayCastResult> RayCastTargetedObject(bool isSearching)
        {
            var scanLine = GetScanLine();
            var rcInput = new RayCastInput()
            {
                FilterOnMaskBits = true,
                MaskBits = (ushort)(IsSupercharged ? CategoryBits.Dynamic + CategoryBits.Player : CategoryBits.Dynamic),
                ClosestHitOnly = isSearching,
            };
            var results = Game.RayCast(scanLine[0], scanLine[1], rcInput);

            foreach (var result in results)
            {
                if (result.HitObject == null)
                    continue;

                yield return result;

                if (isSearching) break;
            }
        }

        private IObjectPullJoint CreatePullJointObject()
        {
            var pullJoint = (IObjectPullJoint)Game.CreateObject("PullJoint");

            if (m_targetedObject != null)
            {
                var mass = m_targetedObject.GetMass();
                pullJoint.SetWorldPosition(m_targetedObject.GetWorldPosition());
                // TODO: cannot lift very heavy objects (piano)
                var force = (float)Math.Pow(10000 * mass, 1.1) / 50;
                if (m_targetedObject is IPlayer)
                    force = 10;

                //ScriptHelper.LogDebug(m_targetedObject.Name, mass, force);
                pullJoint.SetForce(force);
            }

            pullJoint.SetTargetObject(m_targetedObject);
            pullJoint.SetTargetObjectJoint(m_magnetJoint);

            return pullJoint;
        }

        public void PickupObject()
        {
            if (m_targetedObject == null)
            {
                var results = RayCastTargetedObject(true);

                if (results.Count() > 0)
                {
                    var result = results.First();
                    m_targetedObject = result.HitObject;
                    var targetHitbox = m_targetedObject.GetAABB();

                    // if is player, make them staggering
                    if (result.IsPlayer)
                    {
                        var player = (IPlayer)m_targetedObject;
                        player.SetInputEnabled(false);
                        player.AddCommand(new PlayerCommand(PlayerCommandType.StaggerInfinite));
                    }

                    // destroy TargetObjectJoint so hanging stuff can be pulled
                    foreach (var j in Game.GetObjectsByArea<IObjectTargetObjectJoint>(targetHitbox))
                    {
                        var to = j.GetTargetObject();
                        if (to == null) continue;
                        if (to.UniqueID == m_targetedObject.UniqueID)
                        {
                            m_targetedObject.SetLinearVelocity(Vector2.Zero);
                            j.SetTargetObject(null);
                            j.Remove();
                        }
                    }
                    foreach (var j in Game.GetObjectsByArea<IObjectWeldJoint>(targetHitbox))
                    {
                        j.RemoveTargetObject(m_targetedObject);
                    }

                    // some objects that are in dynamic collision group but is static (SurveillanceCamera)
                    if (m_targetedObject.GetBodyType() == BodyType.Static)
                        m_targetedObject.SetBodyType(BodyType.Dynamic);

                    // m_targetObjectJoint.Position is fucked up if key input event fires. idk why
                    m_magnetJoint.SetWorldPosition(GetHoldPosition(true));
                    m_pullJoint.Remove();
                    m_pullJoint = CreatePullJointObject();
                }
            }
        }

        // Don't make heavy objects fly too slow or light objects fly too fast
        private Vector2 ClampVelocity(Vector2 velocity)
        {
            if (velocity.Length() < 20)
                velocity = Owner.AimVector * 20f;
            if (velocity.Length() > 1500)
                velocity = Owner.AimVector * 1500;

            return velocity;
        }

        public override void Remove()
        {
            base.Remove();
            StopStabilizingTargetedObject();
        }

        private bool m_stopPlayingReleaseEffect = false;
        private void Release()
        {
            if (m_targetedObject == null)
            {
                var results = RayCastTargetedObject(true);
                if (results.Count() > 0)
                {
                    m_targetedObject = results.First().HitObject;
                }
            }

            if (m_targetedObject != null)
            {
                var mass = m_targetedObject.GetMass();
                var velocity = Owner.AimVector * 45 / (float)Math.Pow(1 - mass, .6);

                m_targetedObject.SetLinearVelocity(ClampVelocity(velocity));

                if (m_targetedObject.GetCollisionFilter().CategoryBits == 0x10) // dynamics_g2
                {
                    m_targetedObject.TrackAsMissile(true);
                }

                // PS: I dont like the effects, uncomment if you want to see it
                //m_releasedObject = m_targetedObject;
                //m_stopPlayingReleaseEffect = true;
                //ScriptHelper.RunIn(() =>
                //{
                //    if (m_releasedObject.IsRemoved) return;

                //    Game.PlayEffect(EffectName.BulletSlowmoTrace, m_releasedObject.GetWorldPosition());
                //    for (var i = 0; i < 1; i++)
                //    {
                //        var hitbox = m_releasedObject.GetAABB();
                //        var center = hitbox.Center;
                //        var halfWidth = hitbox.Width / 2;
                //        var halfHeight = hitbox.Height / 2;
                //        var effectPosition = new Vector2()
                //        {
                //            X = RandomHelper.Between(center.X - halfWidth, center.X + halfWidth),
                //            Y = RandomHelper.Between(center.Y - halfHeight, center.Y + halfHeight),
                //        };
                //        Game.PlayEffect(EffectName.ItemGleam, effectPosition);
                //    }
                //}, 1);

                StopStabilizingTargetedObject();
            }
        }
    }
}
