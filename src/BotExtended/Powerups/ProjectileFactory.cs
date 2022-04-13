using BotExtended.Powerups.RangeWeapons;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Powerups
{
    class ProjectileFactory
    {
        public static ProjectileBase Create(IProjectile projectile, RangedWeaponPowerup powerup)
        {
            switch (powerup)
            {
                case RangedWeaponPowerup.Blackhole:
                    return new BlackholeProjectile(projectile, powerup);
                case RangedWeaponPowerup.Blast:
                    return new BlastProjectile(projectile, powerup);
                case RangedWeaponPowerup.BouncingLaser:
                    return new BouncingLaser(projectile, powerup);
                case RangedWeaponPowerup.Bow:
                    return new Bow(projectile, powerup);
                case RangedWeaponPowerup.Dormant:
                    return new DormantProjectile(projectile, powerup);
                case RangedWeaponPowerup.DoublePenetration:
                    return new DoublePenetrationProjectile(projectile, powerup);
                case RangedWeaponPowerup.DoubleTrouble:
                    return new DoubleTroubleProjectile(projectile, powerup);
                case RangedWeaponPowerup.Fatigue:
                    return new FatigueProjectile(projectile, powerup);
                case RangedWeaponPowerup.Fire:
                    return new FireProjectile(projectile, powerup);
                case RangedWeaponPowerup.Grapeshot:
                    return new GrapeshotProjectile(projectile, powerup);
                case RangedWeaponPowerup.Helium:
                    return new HeliumProjectile(projectile, powerup);
                case RangedWeaponPowerup.Homing:
                    return new HomingProjectile(projectile, powerup);
                case RangedWeaponPowerup.Hunting:
                    return new HuntingProjectile(projectile, powerup);
                case RangedWeaponPowerup.InfiniteBouncing:
                    return new InfiniteBouncingProjectile(projectile, powerup);
                case RangedWeaponPowerup.Knockback:
                    return new KnockbackProjectile(projectile, powerup);
                case RangedWeaponPowerup.Lightning:
                    return new LightningProjectile(projectile, powerup);
                case RangedWeaponPowerup.Molotov:
                    return new MolotovProjectile(projectile, powerup);
                case RangedWeaponPowerup.Penetration:
                    return new PenetrationProjectile(projectile, powerup);
                case RangedWeaponPowerup.Poison:
                    return new PoisonProjectile(projectile, powerup);
                case RangedWeaponPowerup.Precision:
                    return new PrecisionProjectile(projectile, powerup);
                case RangedWeaponPowerup.Present:
                    return new PresentBullet(projectile, powerup);
                case RangedWeaponPowerup.Riding:
                    return new RidingProjectile(projectile, powerup);
                case RangedWeaponPowerup.Shrapnel:
                    return new ShrapnelProjectile(projectile, powerup);
                case RangedWeaponPowerup.Shrinking:
                    return new ShrinkingProjectile(projectile, powerup);
                case RangedWeaponPowerup.Smoke:
                    return new SmokeProjectile(projectile, powerup);
                case RangedWeaponPowerup.Spinner:
                    return new SpinnerProjectile(projectile, powerup);
                case RangedWeaponPowerup.Steak:
                    return new SteakProjectile(projectile, powerup);
                case RangedWeaponPowerup.StickyBomb:
                    return new StickyBombProjectile(projectile, powerup);
                case RangedWeaponPowerup.Stun:
                    return new StunBullet(projectile, powerup);
                case RangedWeaponPowerup.SuicideDove:
                    return new SuicideDoveProjectile(projectile, powerup);
                case RangedWeaponPowerup.SuicideFighter:
                    return new SuicideFighterProjectile(projectile, powerup);
                case RangedWeaponPowerup.Tearing:
                    return new TearingProjectile(projectile, powerup);
                case RangedWeaponPowerup.Termite:
                    return new TermiteProjectile(projectile, powerup);
                case RangedWeaponPowerup.Welding:
                    return new WeldingBullet(projectile, powerup);
                default:
                    return null;
            }
        }
    }
}
