using System;
using System.Linq;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class RidingProjectile : Projectile
    {
        public RidingProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override void OnProjectileCreated()
        {
            Instance.PowerupBounceActive = true;
            //Instance.Velocity /= 2; // slowest possible

            _isElapsedSwapInstance = ScriptHelper.WithIsElapsed(2500, isElapsedFirstTime: false);
            _isElapsedReflectCooldown = ScriptHelper.WithIsElapsed(100);
        }

        private Func<bool> _isElapsedSwapInstance;
        private Func<bool> _isElapsedReflectCooldown;
        private int _lastBoundedObjectID;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Instance.IsRemoved) return;
            if (Instance.BounceCount > 0) Instance.BounceCount = 0;

            // swap instance because otherwise the rocket will be removed if it travels long enough without bouncing
            if (_isElapsedSwapInstance())
            {
                SwapInstance();
            }

            var rider = GetRocketRider();
            if (rider != null)
            {
                var distanceMovedNextFrame = Instance.Velocity / 55;
                var end = Instance.Position + distanceMovedNextFrame;
                var corner = ScriptHelper.GetCorner(ScriptHelper.GetAngle(Instance.Direction));
                var riderBoxNext = rider.GetAABB();
                var oFilterArea = rider.GetAABB();
                var filterLength = 15f;
                var start1 = Vector2.Zero;
                var start2 = Vector2.Zero;

                if (corner == Corner.TopLeft) { oFilterArea.Left -= filterLength; oFilterArea.Top += filterLength; }
                if (corner == Corner.TopRight) { oFilterArea.Right += filterLength; oFilterArea.Top += filterLength; }
                if (corner == Corner.BottomLeft) { oFilterArea.Left -= filterLength; oFilterArea.Bottom -= filterLength; }
                if (corner == Corner.BottomRight) { oFilterArea.Right += filterLength; oFilterArea.Bottom -= filterLength; }
                if (corner == Corner.TopLeft || corner == Corner.BottomRight) { start1 = riderBoxNext.TopRight; start2 = riderBoxNext.BottomLeft; }
                if (corner == Corner.TopRight || corner == Corner.BottomLeft) { start1 = riderBoxNext.TopLeft; start2 = riderBoxNext.BottomRight; }

                //Game.DrawArea(rider.GetAABB(), Color.Red);
                //Game.DrawArea(riderBoxNext, Color.Blue);
                riderBoxNext.Move(distanceMovedNextFrame);

                foreach (var o in Game.GetObjectsByArea(oFilterArea))
                {
                    if (o.UniqueID == rider.UniqueID || !ScriptHelper.IsInteractiveObject(o) && !ScriptHelper.IsHardStaticGround(o)) continue;
                    if (!riderBoxNext.Intersects(o.GetAABB())) continue;

                    var result1 = GetBouncedObject(start1, start1 + distanceMovedNextFrame, rider);
                    var result2 = GetBouncedObject(start2, start2 + distanceMovedNextFrame, rider);
                    var result = result1;
                    if (result.HitObject == null) result = result2;
                    if (result1.HitObject != null && result2.HitObject != null) result = result1.Fraction < result2.Fraction ? result1 : result2;

                    if (result.HitObject != null)
                    {
                        if (result.ObjectID == _lastBoundedObjectID && !_isElapsedReflectCooldown())
                            break;
                        
                        var reflectedVector = MathExtension.Reflect(Instance.Direction, result.Normal);

                        _lastBoundedObjectID = result.ObjectID;
                        var angle = ScriptHelper.GetAngle(Instance.Direction);
                        var bouncedVec = result.IsPlayer
                            ? RandomHelper.Direction(angle - MathExtension.PIOver8, angle + MathExtension.PIOver8, true) * 20 + Vector2.UnitY * 6
                            : Instance.Direction * 7;

                        result.HitObject.SetLinearVelocity(bouncedVec);
                        result.HitObject.DealDamage(2);
                        Instance.Direction = reflectedVector;

                        var hitPlayer = ScriptHelper.AsPlayer(result.HitObject);
                        if (hitPlayer != null) ScriptHelper.Fall(hitPlayer);

                        Game.PlayEffect(EffectName.BulletHit, result.Position);
                        Game.PlaySound("BulletHitDefault", result.Position, 1);
                        break;
                    }
                }
            }
        }

        private void SwapInstance()
        {
            var pos = Instance.Position - Instance.Direction * 2;
            var newInstance = Game.SpawnProjectile(Instance.ProjectileItem, pos, Instance.Direction, ScriptHelper.GetPowerup(Instance));
            newInstance.Velocity = Instance.Velocity;
            Instance.FlagForRemoval();
            Instance = newInstance;
        }

        private RayCastResult GetBouncedObject(Vector2 start, Vector2 end, IPlayer rider)
        {
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                FilterOnMaskBits = true,
                MaskBits = CategoryBits.Dynamic + CategoryBits.Player + CategoryBits.StaticGround,
                IncludeOverlap = true,
            });

            // allow to go through portals
            var firstResult = results.FirstOrDefault();
            if (firstResult.HitObject != null && firstResult.HitObject.Name.StartsWith("Portal"))
                return default(RayCastResult);
            
            return results.Where(r => r.HitObject != null).FirstOrDefault(r => r.ObjectID != rider.UniqueID
            && (ScriptHelper.IsHardStaticGround(r.HitObject)
            || ScriptHelper.IsDynamicG1(r.HitObject)
            || ScriptHelper.IsPlayer(r.HitObject)
            || ScriptHelper.IsDynamicG2(r.HitObject) && r.HitObject.Name.Contains("Table")
            ));
        }

        public IPlayer GetRocketRider()
        {
            return Game.GetPlayers().Where(p => p.IsRocketRiding && p.RocketRidingProjectileInstanceID == ID).FirstOrDefault();
        }
    }
}
