using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Projectiles
{
    class ProjectileFactory
    {
        public static ProjectileBase Create(IProjectile projectile, RangedWeaponPowerup powerup)
        {
            switch (powerup)
            {
                case RangedWeaponPowerup.Present:
                    return new PresentBullet(projectile);
                case RangedWeaponPowerup.Stun:
                    return new StunBullet(projectile);
                case RangedWeaponPowerup.Spinner:
                    return new SpinnerBullet(projectile);
                default:
                    return null;
            }
        }
    }
}
