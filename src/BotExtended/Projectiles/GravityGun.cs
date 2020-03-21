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

        public static readonly float Range = 160;

        public bool IsSupercharged { get; private set; }
        public Area GetStabilizedZone()
        {
            return GetStabilizedZone(GetHoldPosition(true));
        }
        private Area GetStabilizedZone(Vector2 holdPosition)
        {
            return ScriptHelper.GrowFromCenter(holdPosition, 20);
        }

        private IObject m_invisibleMagnet;
        private IObjectTargetObjectJoint m_magnetJoint;

        private IObjectPullJoint m_pullJoint;

        private IObject m_distanceJointObject;
        private IObjectDistanceJoint m_distanceJoint;
        private IObjectTargetObjectJoint m_targetedObjectJoint;

        private IObject m_releasedObject;
        public IObject TargetedObject { get; private set; }

        public bool IsTargetedObjectStabilized { get; private set; }

        public Vector2 GetHoldPosition(bool useOffset)
        {
            var offset = 0f;

            if (TargetedObject != null && useOffset)
            {
                var hitbox = TargetedObject.GetAABB();
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

                if (TargetedObject != null)
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

                    if (TargetedObject != null)
                        Game.DrawArea(TargetedObject.GetAABB(), Color.Blue);

                    //if (m_distanceJointObject != null)
                    //    Game.DrawArea(m_distanceJointObject.GetAABB(), Color.Green);

                    var to = m_pullJoint.GetTargetObject();
                    if (to != null)
                        Game.DrawArea(to.GetAABB(), Color.Yellow);
                }
            }
            else
            {
                if (IsTargetedObjectStabilized || TargetedObject != null)
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
            var targetHitbox = TargetedObject.GetAABB();

            if (stabilizedZone.Intersects(targetHitbox))
                targetedObjectFound = true;

            foreach (var result in results)
            {
                if (result.HitObject == null) continue;

                if (result.HitObject.UniqueID == TargetedObject.UniqueID)
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
                var player = TargetedObject as IPlayer;
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

            var player = TargetedObject as IPlayer;
            if (player != null) player.SetInputEnabled(true);

            m_pullJoint.SetTargetObject(null);

            if (TargetedObject != null)
            {
                if (Owner.IsBot)
                    TargetedObject.SetCollisionFilter(m_oldCollisionFilter);
                if (TargetedObject.GetCollisionFilter().CategoryBits == CategoryBits.DynamicG2)
                    TargetedObject.TrackAsMissile(true); // must be called after updating CollisionFilter
                TargetedObject.SetMass(m_oldMass);
                TargetedObject = null;
            }
            IsTargetedObjectStabilized = false;
        }

        private void StabilizeTargetedObject()
        {
            TargetedObject.SetLinearVelocity(Vector2.Zero);

            m_distanceJointObject = Game.CreateObject("InvisibleBlockSmall");
            m_distanceJointObject.SetBodyType(BodyType.Dynamic);

            m_distanceJoint = (IObjectDistanceJoint)Game.CreateObject("DistanceJoint");
            m_distanceJoint.SetLineVisual(LineVisual.None);
            m_distanceJoint.SetLengthType(DistanceJointLengthType.Fixed);

            m_targetedObjectJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");

            var targetedObjPosition = TargetedObject.GetAABB().Center;
            m_distanceJointObject.SetWorldPosition(targetedObjPosition);
            TargetedObject.SetWorldPosition(targetedObjPosition);
            m_pullJoint.SetWorldPosition(targetedObjPosition);
            m_distanceJoint.SetWorldPosition(targetedObjPosition);
            // if DistanceJoint and TargetObjectJoint is at the same position, weird things may happen
            // uncomment the part below to stop it
            m_targetedObjectJoint.SetWorldPosition(m_distanceJointObject.GetWorldPosition()/* - Vector2.UnitY*/);

            m_pullJoint.SetTargetObject(m_distanceJointObject);
            m_distanceJoint.SetTargetObject(m_distanceJointObject);
            m_distanceJoint.SetTargetObjectJoint(m_targetedObjectJoint);
            m_targetedObjectJoint.SetTargetObject(TargetedObject);

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

            if (TargetedObject != null)
            {
                var mass = TargetedObject.GetMass();

                TargetedObject.SetMass(.01f);
                pullJoint.SetWorldPosition(TargetedObject.GetWorldPosition());
                pullJoint.SetForce(5);
                pullJoint.SetForcePerDistance(0);
            }

            pullJoint.SetTargetObject(TargetedObject);
            pullJoint.SetTargetObjectJoint(m_magnetJoint);

            return pullJoint;
        }

        private CollisionFilter m_oldCollisionFilter;
        private float m_oldMass;
        public void PickupObject()
        {
            if (TargetedObject == null)
            {
                var results = RayCastTargetedObject(true);

                if (results.Count() > 0)
                {
                    var result = results.First();
                    TargetedObject = result.HitObject;
                    m_oldMass = TargetedObject.GetMass();

                    // if is player, make them staggering
                    if (result.IsPlayer)
                    {
                        var player = (IPlayer)TargetedObject;
                        player.SetInputEnabled(false);
                        player.AddCommand(new PlayerCommand(PlayerCommandType.StaggerInfinite));
                    }

                    // destroy TargetObjectJoint so hanging stuff can be pulled
                    foreach (var j in Game.GetObjectsByArea<IObjectTargetObjectJoint>(TargetedObject.GetAABB()))
                    {
                        var to = j.GetTargetObject();
                        if (to == null) continue;
                        if (to.UniqueID == TargetedObject.UniqueID)
                        {
                            TargetedObject.SetLinearVelocity(Vector2.Zero);
                            j.SetTargetObject(null);
                            j.Remove();
                        }
                    }
                    foreach (var j in Game.GetObjectsByArea<IObjectWeldJoint>(TargetedObject.GetAABB()))
                    {
                        j.RemoveTargetObject(TargetedObject);
                    }

                    // some objects that are in dynamic collision group but is static (SurveillanceCamera)
                    if (TargetedObject.GetBodyType() == BodyType.Static)
                        TargetedObject.SetBodyType(BodyType.Dynamic);

                    // m_targetObjectJoint.Position is fucked up if key input event fires. idk why
                    m_magnetJoint.SetWorldPosition(GetHoldPosition(true));
                    m_pullJoint.Remove();
                    m_pullJoint = CreatePullJointObject();

                    // The AI when using GravityGun is not very good so I give the bots a little edge advantage
                    if (Owner.IsBot)
                    {
                        m_oldCollisionFilter = TargetedObject.GetCollisionFilter();
                        var noStaticCollision = TargetedObject.GetCollisionFilter();
                        // https://www.mythologicinteractiveforums.com/viewtopic.php?t=1012
                        noStaticCollision.CategoryBits = 0x1010; // marker or something
                        noStaticCollision.MaskBits = (ushort)(noStaticCollision.MaskBits % 2 == 1 ?
                            noStaticCollision.MaskBits - 1 : noStaticCollision.MaskBits);
                        TargetedObject.SetCollisionFilter(noStaticCollision);
                    }
                }
            }
        }

        public override void Remove()
        {
            base.Remove();
            StopStabilizingTargetedObject();
        }

        private bool m_stopPlayingReleaseEffect = false;
        private void Release()
        {
            if (TargetedObject == null)
            {
                var results = RayCastTargetedObject(true);
                if (results.Count() > 0)
                {
                    TargetedObject = results.First().HitObject;
                }
            }

            if (TargetedObject != null)
            {
                var mass = TargetedObject.GetMass();
                var velocity = Owner.AimVector * 50;

                TargetedObject.SetLinearVelocity(velocity);

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
