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
                case RangedWeaponPowerup.Grapeshot:
                    return new GrapeshotProjectile(projectile);
                case RangedWeaponPowerup.Helium:
                    return new HeliumProjectile(projectile);
                case RangedWeaponPowerup.Hunting:
                    return new HuntingProjectile(projectile);
                case RangedWeaponPowerup.Homing:
                    return new HomingProjectile(projectile);
                case RangedWeaponPowerup.InfiniteBouncing:
                    return new InfiniteBouncingProjectile(projectile);
                case RangedWeaponPowerup.Lightning:
                    return new LightningProjectile(projectile);
                case RangedWeaponPowerup.Molotov:
                    return new MolotovProjectile(projectile);
                case RangedWeaponPowerup.Poison:
                    return new PoisonProjectile(projectile);
                case RangedWeaponPowerup.Present:
                    return new PresentBullet(projectile);
                case RangedWeaponPowerup.Riding:
                    return new RidingProjectile(projectile);
                case RangedWeaponPowerup.Smoke:
                    return new SmokeProjectile(projectile);
                case RangedWeaponPowerup.Spinner:
                    return new SpinnerBullet(projectile);
                case RangedWeaponPowerup.StickyBomb:
                    return new StickyBombProjectile(projectile);
                case RangedWeaponPowerup.Stun:
                    return new StunBullet(projectile);
                case RangedWeaponPowerup.SuicideDove:
                    return new SuicideDoveProjectile(projectile);
                case RangedWeaponPowerup.SuicideFighter:
                    return new SuicideFighterProjectile(projectile);
                case RangedWeaponPowerup.Tearing:
                    return new TearingBullet(projectile);
                case RangedWeaponPowerup.Termite:
                    return new TermiteProjectile(projectile);
                case RangedWeaponPowerup.Welding:
                    return new WeldingBullet(projectile);
                default:
                    return null;
            }
        }
    }
}
