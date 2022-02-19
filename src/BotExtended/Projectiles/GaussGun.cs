using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class GaussGun: RangeWpn
    {
        public float ChargeModifier { get; private set; }

        public GaussGun(IPlayer owner, WeaponItem name) : base(owner, name, RangedWeaponPowerup.Gauss)
        {
        }

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

            if (Projectile.IsSlowProjectile(projectile))
                return;

            var range = 300;

            if (!Projectile.IsShotgun(projectile))
                range *= 2;

            var start = projectile.Position;
            var end = start + projectile.Direction * range;
            var maxHitCount = (int)(ChargeModifier / 1000 + 1);
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                ProjectileHit = RayCastFilterMode.True,
                IncludeOverlap = true,
                ClosestHitOnly = maxHitCount == 1,
            }).Where(r => r.HitObject != null);

            end = results.Count() == 0 ? end : results.First().Position;

            var distance = Vector2.Distance(start, end);
            for (var i = 0f; i <= distance; i+=1.5f)
                Game.PlayEffect(EffectName.ItemGleam, start + projectile.Direction * i);

            var hitCount = 0;
            foreach (var result in results)
            {
                var hitObject = result.HitObject;
                var projectileItem = projectile.ProjectileItem;
                var direction = projectile.Direction;
                var powerup = ScriptHelper.GetPowerup(projectile);

                Game.PlayEffect(EffectName.Electric, result.Position);
                Game.PlaySound("ElectricSparks", result.Position);
                ScriptHelper.Timeout(() =>
                {
                    var p = Game.SpawnProjectile(projectileItem, result.Position, direction, powerup);
                    p.CritChanceDealtModifier *= 3;
                    if (!hitObject.GetCollisionFilter().AbsorbProjectile)
                        ScriptHelper.Timeout(() => p.FlagForRemoval(), 0);
                }, 0);
                //if (Game.IsEditorTest)
                //{
                //    ScriptHelper.RunIn(() =>
                //    {
                //        if (hitObject != null)
                //            Game.DrawArea(hitObject.GetAABB());
                //        Game.DrawLine(start, end, Color.Cyan);
                //    }, 500);
                //}
                if (hitObject.GetCollisionFilter().AbsorbProjectile)
                    hitCount++;
                if (hitCount >= maxHitCount) break;
            }

            projectile.FlagForRemoval();
        }
    }
}
