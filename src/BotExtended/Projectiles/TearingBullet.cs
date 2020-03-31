using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class TearingBullet : Projectile
    {
        public TearingBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Tearing)
        {
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (!args.IsPlayer)
                return;
        }
    }
}
