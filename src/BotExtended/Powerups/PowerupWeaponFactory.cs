using BotExtended.Library;
using BotExtended.Powerups.MeleeWeapons;
using BotExtended.Powerups.RangeWeapons;
using SFDGameScriptInterface;
using System;

namespace BotExtended.Powerups
{
    static class PowerupWeaponFactory
    {
        public static RangeWpn Create(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup)
        {
            try
            {
                var type = Mapper.GetWeaponItemType(name);
                if (type != WeaponItemType.Rifle && type != WeaponItemType.Handgun)
                    return new RangeWpn(owner, name, RangedWeaponPowerup.None);

                switch (powerup)
                {
                    case RangedWeaponPowerup.Delay:
                        return new DelayGun(owner, name, powerup);
                    case RangedWeaponPowerup.Gauss:
                        return new GaussGun(owner, name, powerup);
                    case RangedWeaponPowerup.Gravity:
                    case RangedWeaponPowerup.GravityDE:
                        return new GravityGun(owner, name, powerup);
                    case RangedWeaponPowerup.Minigun:
                        return new MiniGun(owner, name, powerup);
                    case RangedWeaponPowerup.Object:
                        return new ObjectGun(owner, name, powerup);
                    case RangedWeaponPowerup.Scattershot:
                        return new Scattershot(owner, name, powerup);
                    case RangedWeaponPowerup.Taser:
                        return new TaserGun(owner, name, powerup);
                    default:
                        return new RangeWpn(owner, name, powerup);
                }
            }
            catch
            {
                return new RangeWpn(owner, name, RangedWeaponPowerup.None);
            }
        }

        public static MeleeWpn Create(IPlayer owner, WeaponItem name, MeleeWeaponPowerup powerup)
        {
            try
            {
                var type = Mapper.GetWeaponItemType(name);
                if (type != WeaponItemType.Melee && type != WeaponItemType.NONE)
                    return new MeleeWpn(owner, name, MeleeWeaponPowerup.None);

                switch (powerup)
                {
                    case MeleeWeaponPowerup.Breaking:
                        return new BreakingPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Earthquake:
                        return new EarthquakePowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.FireTrail:
                        return new FireTrailPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Gib:
                        return new GibPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.GroundBreaker:
                        return new GroundBreakerPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.GroundSlam:
                        return new GroundSlamPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Hurling:
                        return new HurlingPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Megaton:
                        return new MegatonPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Pushback:
                        return new PushbackPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Serious:
                        return new SeriousPowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Slide:
                        return new SlidePowerup(owner, name, powerup);
                    case MeleeWeaponPowerup.Splitting:
                        return new SplittingPowerup(owner, name, powerup);
                    default:
                        return new MeleeWpn(owner, name, powerup);
                }
            }
            catch
            {
                return new MeleeWpn(owner, name, MeleeWeaponPowerup.None);
            }
        }
    }
}
