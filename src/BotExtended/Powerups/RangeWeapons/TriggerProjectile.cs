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
    class TriggerProjectile : CustomProjectile
    {
        public List<IObject> _slowmoProjectiles = new List<IObject>();

        public TriggerProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            _isElapsedScan = ScriptHelper.WithIsElapsed(35);
            _isElapsedPlayEffect = ScriptHelper.WithIsElapsed(300);
        }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            var p = CreateCustomProjectile(projectile, "BulletCommonSlowmo", projectile.Direction);
            p.SetAngle(ScriptHelper.GetAngle(projectile.Direction));
            p.SetBodyType(BodyType.Static);
            return p;
        }

        private Func<bool> _isElapsedScan;
        private Func<bool> _isElapsedPlayEffect;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            var position = Instance.GetWorldPosition();
            var direction = ScriptHelper.GetDirection(Instance.GetAngle());
            Instance.SetWorldPosition(position + direction * .1f);

            if (_isElapsedPlayEffect())
                Game.PlayEffect(EffectName.BulletSlowmoTrace, position - direction * 3, direction.X, direction.Y);

            if (ScriptHelper.IsElapsed(CreatedTime, 1000) && _isElapsedScan())
            {
                var start = position;
                var end = start + direction * ScriptHelper.GetDistanceToEdge(start, direction);
                var result = Game.RayCast(start, end, new RayCastInput
                {
                    ProjectileHit = RayCastFilterMode.True,
                    AbsorbProjectile = RayCastFilterMode.True,
                    ClosestHitOnly = true,
                }).FirstOrDefault(x => x.HitObject != null);

                if (result.IsPlayer || result.Fraction <= 0.01f)
                    AccelerateProjectile();
            }
        }

        private void AccelerateProjectile()
        {
            var proj2 = Game.SpawnProjectile(Mapper.GetProjectile(WeaponItem), Instance.GetWorldPosition(), ScriptHelper.GetDirection(Instance.GetAngle()));
            proj2.Velocity /= 1.5f;
            Instance.Remove();
        }
    }
}
