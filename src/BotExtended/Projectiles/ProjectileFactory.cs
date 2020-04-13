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
                case RangedWeaponPowerup.Blackhole:
                    return new BlackholeProjectile(projectile);
                case RangedWeaponPowerup.Blast:
                    return new BlastBullet(projectile);
                case RangedWeaponPowerup.DoubleTrouble:
                    return new DoubleTroubleProjectile(projectile);
                case RangedWeaponPowerup.Fatigue:
                    return new FatigueProjectile(projectile);
                case RangedWeaponPowerup.Helium:
                    return new HeliumProjectile(projectile);
                case RangedWeaponPowerup.Homing:
                    return new HomingProjectile(projectile);
                case RangedWeaponPowerup.Lightning:
                    return new LightningProjectile(projectile);
                case RangedWeaponPowerup.Present:
                    return new PresentBullet(projectile);
                case RangedWeaponPowerup.Spinner:
                    return new SpinnerBullet(projectile);
                case RangedWeaponPowerup.StickyBomb:
                    return new StickyBombProjectile(projectile);
                case RangedWeaponPowerup.Stun:
                    return new StunBullet(projectile);
                case RangedWeaponPowerup.SuicideDove:
                    return new SuicideDoveProjectile(projectile);
                case RangedWeaponPowerup.Tearing:
                    return new TearingBullet(projectile);
                case RangedWeaponPowerup.Telsa:
                    return new TelsaProjectile(projectile);
                default:
                    return null;
            }
        }
    }
}
