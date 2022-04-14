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
    class PreciseBouncingProjectile : Projectile
    {
        public PreciseBouncingProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            Instance.PowerupBounceActive = true;
            Instance.Velocity *= 0.7f; // 70% speed 
        }

        public override bool IsRemoved { get { return _bounceCount >= 3; } }

        private int _bounceCount = 0;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);
            
            if (Instance.BounceCount > 0)
            {
                _bounceCount++;
                Instance.BounceCount = 0;
                TargetEnemy();
            }
        }

        private void TargetEnemy()
        {
            var potentialTargets = Game.GetPlayers()
                .Where(p => !p.IsDead && !ScriptHelper.SameTeam(p, Team) && Vector2.Distance(p.GetWorldPosition(), Instance.Position) < 260)
                .Take(5);

            foreach (var player in potentialTargets)
            {
                var pPos = player.GetWorldPosition();
                var start = Instance.Position - Instance.Direction * 2;
                var result = Game.RayCast(start, pPos, new RayCastInput
                {
                    ProjectileHit = RayCastFilterMode.True,
                    AbsorbProjectile = RayCastFilterMode.True,
                    ClosestHitOnly = true,
                }).FirstOrDefault(x => x.HitObject != null);

                if (result.IsPlayer)
                {
                    Instance.Direction = Vector2.Normalize(pPos - Instance.Position);
                    _bounceCount = int.MaxValue; // trigger remove
                    break;
                }
            }
        }
    }
}
