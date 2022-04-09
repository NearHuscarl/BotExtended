using System;
using System.Collections.Generic;
using System.Linq;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class ShrapnelProjectile : Projectile
    {
        private class Shrapnel
        {
            public float MaxDistance;
            public IProjectile Projectile;
        }

        public ShrapnelProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        public override bool IsRemoved { get { return Instance.IsRemoved && _shrapnels.Count == 0; } }

        private List<Shrapnel> _shrapnels = new List<Shrapnel>();
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            foreach (var s in _shrapnels.ToList())
            {
                if (s.Projectile.TotalDistanceTraveled > s.MaxDistance || s.Projectile.IsRemoved)
                {
                    if (!s.Projectile.IsRemoved)
                    {
                        var shrapnel = Game.CreateObject("BulletCommonSlowmo", s.Projectile.Position, ScriptHelper.GetAngle(s.Projectile.Direction));
                        var cf = shrapnel.GetCollisionFilter();
                        cf.CategoryBits = CategoryBits.DynamicG1;

                        shrapnel.SetMass(0.02f);
                        shrapnel.SetCollisionFilter(cf);
                        shrapnel.SetLinearVelocity(s.Projectile.Direction * 14);
                        ScriptHelper.Timeout(() => shrapnel.Remove(), 3000);
                        ScriptHelper.RunIf(() => shrapnel.Remove(), () => shrapnel.GetLinearVelocity() == Vector2.Zero, 5000);
                    }

                    s.Projectile.FlagForRemoval();
                    _shrapnels.Remove(s);
                }
                else
                {
                    s.Projectile.Velocity -= Vector2.UnitY * 18;
                }
            }
        }

        protected override void OnProjectileCreated()
        {
            Instance.DamageDealtModifier /= 3;
            Instance.PowerupBounceActive = true;
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            // hit platform
            if (!args.RemoveFlag && !args.IsDeflection) return;
            if (!args.RemoveFlag)
            {
                Game.PlayEffect(EffectName.Explosion, args.HitPosition);
                Game.PlaySound("Explosion", args.HitPosition);
                Instance.FlagForRemoval();
            }

            var shrapnelCount = RandomHelper.BetweenInt(15, 25);
            var normalAngle = ScriptHelper.GetAngle(args.HitNormal);
            var normalDir = ScriptHelper.GetDir(normalAngle);
            var offset = Vector2.Zero;

            if (normalDir == Direction.Left) offset = -Vector2.UnitX;
            else if (normalDir == Direction.Right) offset = Vector2.UnitX;
            else if (normalDir == Direction.Top) offset = Vector2.UnitY;
            else if (normalDir == Direction.Bottom) offset = -Vector2.UnitY;
            var position = Instance.Position + offset * 3;
            var deg80 = MathExtension.ToRadians(80);

            for (var i = 0; i < shrapnelCount; i++)
            {
                var direction = RandomHelper.Direction(normalAngle - deg80, normalAngle + deg80, true);
                var p = Game.SpawnProjectile(ProjectileItem.REVOLVER, position, direction);
                p.Velocity /= 3;
                _shrapnels.Add(new Shrapnel
                {
                    Projectile = p,
                    MaxDistance = RandomHelper.Between(65, 100),
                });
            }
        }
    }
}
