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
    class GaussGun : RangeWpn
    {
        public float ChargeModifier { get; private set; }

        public override float MaxRange { get { return Projectile.IsShotgun(Mapper.GetProjectile(Name)) ? 150 : 300; } }
        public override bool IsValidPowerup()
        {
            return !Projectile.IsSlowProjectile(Mapper.GetProjectile(Name));
        }

        public GaussGun(IPlayer owner, WeaponItem name) : base(owner, name, RangedWeaponPowerup.Gauss) { }

        private bool m_prevManualAiming = false;
        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Owner.IsManualAiming && !m_prevManualAiming)
            {
                m_prevManualAiming = true;
            }
            if (!Owner.IsManualAiming && m_prevManualAiming)
            {
                ChargeModifier = 0f;
                m_prevManualAiming = false;
            }

            if (Owner.IsManualAiming)
            {
                if (ChargeModifier <= 5000)
                    ChargeModifier += 1 * elapsed;
                Game.DrawText(ChargeModifier.ToString(), Owner.GetWorldPosition());
            }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            var start = projectile.Position;
            var end = start + projectile.Direction * MaxRange;
            var maxHitCount = Game.IsEditorTest ? 5000 : (int)(ChargeModifier / 1000 + 1);
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                ProjectileHit = RayCastFilterMode.True,
                IncludeOverlap = true,
                ClosestHitOnly = maxHitCount == 1,
            }).Where(r => r.HitObject != null);
            var props = projectile.GetProperties();

            end = results.Count() == 0 ? end : results.Last().Position;

            var hitCount = 0;
            foreach (var result in results)
            {
                var hitObject = result.HitObject;
                var projectileItem = projectile.ProjectileItem;
                var direction = projectile.Direction;
                var powerup = ScriptHelper.GetPowerup(projectile);
                var cf = hitObject.GetCollisionFilter();

                Game.PlayEffect(EffectName.Electric, result.Position);
                Game.PlaySound("ElectricSparks", result.Position);

                if (cf.AbsorbProjectile)
                {
                    hitCount++;
                    if (cf.CategoryBits == CategoryBits.StaticGround) { end = result.Position; break; }
                }
                hitObject.DealDamage(result.IsPlayer ? props.PlayerDamage : props.ObjectDamage);
                if (hitCount >= maxHitCount) break;
            }

            var distance = Vector2.Distance(start, end);
            for (var i = 0f; i <= distance; i += 1.5f)
                Game.PlayEffect(EffectName.ItemGleam, start + projectile.Direction * i);

            projectile.FlagForRemoval();
        }
    }
}
