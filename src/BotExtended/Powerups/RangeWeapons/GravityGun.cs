using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class GravityGun : RangeWpn
    {
        public GravityGun(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner, name, powerup)
        {
            if (powerup == RangedWeaponPowerup.GravityDE)
                IsSupercharged = true;
            else if (powerup == RangedWeaponPowerup.Gravity)
                IsSupercharged = false;
            else
                throw new Exception("Unknown powerup for gravity gun: " + powerup);

            m_pullJoint = CreatePullJointObject();
        }

        public override float MaxRange { get { return 160; } }

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

            var muzzleInfo = GetMuzleInfo();

            return muzzleInfo.Position + muzzleInfo.Direction * (6 + offset);
        }

        private Vector2[] GetScanLine()
        {
            var holdPosition = GetHoldPosition(false);
            var end = holdPosition + Owner.AimVector * MaxRange;

            return new Vector2[] { holdPosition, end };
        }

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (!Owner.IsManualAiming || !IsEquipping) return;

            var holdPosition = GetHoldPosition(true);
            m_invisibleMagnet.SetWorldPosition(holdPosition);
            // m_invisibleMagnet is a static object so the corresponding TargetObjectJoint need to be moved manually too
            m_magnetJoint.SetWorldPosition(holdPosition);

            if (TargetedObject != null)
                TryStabilizeTargetedObject(holdPosition);

            if (Game.IsEditorTest)
            {
                var scanLine = GetScanLine();
                
                Game.DrawLine(scanLine[0], scanLine[1]);
                Game.DrawCircle(holdPosition, .5f, Color.Red);

                Game.DrawArea(m_pullJoint.GetAABB(), Color.Cyan);
                //Game.DrawCircle(m_magnetJoint.GetWorldPosition(), 5, Color.Magenta);
                //Game.DrawCircle(m_invisibleMagnet.GetWorldPosition(), 6, Color.Red);

                if (TargetedObject != null)
                    Game.DrawArea(TargetedObject.GetAABB(), Color.Blue);

                //if (m_distanceJointObject != null)
                //    Game.DrawArea(m_distanceJointObject.GetAABB(), Color.Green);

                var to = m_pullJoint.GetTargetObject();
                if (to != null)
                    Game.DrawArea(ScriptHelper.GrowFromCenter(to.GetAABB().Center, 30), Color.Yellow);
            }
        }

        protected override void OnStopManualAim()
        {
            StopStabilizingTargetedObject();
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

                    if (stabilizedZone.Intersects(targetHitbox) && !IsTargetedObjectStabilized)
                        StabilizeTargetedObject();
                    break;
                }
            }

            if (!targetedObjectFound)
            {
                StopStabilizingTargetedObject();
            }
            else
            {
                var player = ScriptHelper.CastPlayer(TargetedObject);
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
                // Markers to make target object hovered
                m_distanceJointObject.Remove();
                m_distanceJoint.Remove();
                m_targetedObjectJoint.Remove();
            }
            // Markers to pull the target object
            if (m_magnetJoint != null)
            {
                m_magnetJoint.Remove();
                m_invisibleMagnet.Remove();
            }

            var player = ScriptHelper.CastPlayer(TargetedObject);
            if (player != null)
            {
                player.AddCommand(new PlayerCommand(PlayerCommandType.StopStagger));
                player.SetInputEnabled(true);
            }

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
            m_pullJoint.SetForce(15);

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
            if (BotManager.GetBot(Owner).CurrentTotalAmmo == 0)
            {
                if (Type == WeaponItemType.Rifle)
                    Owner.SetCurrentPrimaryWeaponAmmo(Owner.CurrentPrimaryWeapon.MaxTotalAmmo - 1);
                if (Type == WeaponItemType.Handgun)
                    Owner.SetCurrentSecondaryWeaponAmmo(Owner.CurrentSecondaryWeapon.MaxTotalAmmo - 1);
            }

            Release();
        }

        // List of objects that are in dynamic collision group but not really interact with other dynamic objects (try for yourself)
        public static readonly HashSet<string> Blacklist = new HashSet<string>()
        {
            "Lamp00",
        };
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
                if (result.HitObject == null || Blacklist.Contains(result.HitObject.Name))
                    continue;

                yield return result;

                if (isSearching) break;
            }
        }

        private IObjectPullJoint CreatePullJointObject()
        {
            var holdPosition = GetHoldPosition(true);

            m_invisibleMagnet = Game.CreateObject("InvisibleBlockSmall", holdPosition);
            m_invisibleMagnet.SetBodyType(BodyType.Static);
            m_invisibleMagnet.SetCollisionFilter(Constants.NoCollision);

            m_magnetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint", holdPosition);
            m_magnetJoint.SetTargetObject(m_invisibleMagnet);

            var pullJoint = (IObjectPullJoint)Game.CreateObject("PullJoint");

            if (TargetedObject != null)
            {
                TargetedObject.SetMass(.004f);
                pullJoint.SetWorldPosition(TargetedObject.GetWorldPosition());
                pullJoint.SetForce(ScriptHelper.IsPlayer(TargetedObject) ? 15 : 4); // IPlayer doesn't have mass, maybe a bit heavier than normal
                pullJoint.SetForcePerDistance(0);
            }

            pullJoint.SetTargetObject(TargetedObject);
            pullJoint.SetTargetObjectJoint(m_magnetJoint);

            return pullJoint;
        }

        private void MakePlayer(IPlayer player, PlayerCommandType CommandType)
        {
            var faceDirection = player.GetWorldPosition().X > GetHoldPosition(false).X
                ? PlayerCommandFaceDirection.Right : PlayerCommandFaceDirection.Left;
            player.SetInputEnabled(false);
            // some command like Stagger not working without this line
            player.AddCommand(new PlayerCommand(PlayerCommandType.FaceAt, faceDirection));
            ScriptHelper.Timeout(() => player.AddCommand(new PlayerCommand(CommandType)), 2);
        }

        private CollisionFilter m_oldCollisionFilter;
        private float m_oldMass;
        public bool PickupObject()
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
                        MakePlayer(player, PlayerCommandType.StaggerInfinite);
                    }

                    // destroy Joints so hanging stuff can be pulled
                    ScriptHelper.Unscrew(TargetedObject);

                    // some objects that are in dynamic collision group but is static (SurveillanceCamera)
                    if (TargetedObject.GetBodyType() == BodyType.Static)
                        TargetedObject.SetBodyType(BodyType.Dynamic);

                    m_pullJoint.Remove();
                    m_pullJoint = CreatePullJointObject();

                    // The AI when using GravityGun is not very good so I give the bots a little edge advantage
                    if (Owner.IsBot)
                    {
                        m_oldCollisionFilter = TargetedObject.GetCollisionFilter();
                        var noStaticCollision = TargetedObject.GetCollisionFilter();
                        // https://www.mythologicinteractiveforums.com/viewtopic.php?t=1012
                        noStaticCollision.CategoryBits = 0x1010; // marker or something
                        noStaticCollision.MaskBits = (ushort)(noStaticCollision.MaskBits & 0x11);
                        TargetedObject.SetCollisionFilter(noStaticCollision);
                    }
                    return true;
                }
            }
            return false;
        }

        public override void Remove()
        {
            base.Remove();
            StopStabilizingTargetedObject();
        }

        private void Release()
        {
            if (TargetedObject == null)
            {
                var results = RayCastTargetedObject(true);
                if (results.Count() > 0)
                {
                    var result = results.First();
                    TargetedObject = result.HitObject;
                    if (result.IsPlayer) MakePlayer((IPlayer)result.HitObject, PlayerCommandType.Fall);
                }
            }

            if (TargetedObject != null)
            {
                var velocity = Owner.AimVector * 40;

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
                //        var effectPosition = ScriptHelerp.WithinArea(m_releasedObject.GetAABB());
                //        Game.PlayEffect(EffectName.ItemGleam, effectPosition);
                //    }
                //}, 1);

                StopStabilizingTargetedObject();
            }
        }
    }
}
