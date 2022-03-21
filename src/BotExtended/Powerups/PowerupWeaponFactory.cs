﻿using BotExtended.Library;
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
                switch (powerup)
                {
                    case RangedWeaponPowerup.Delay:
                        return new DelayGun(owner, name);
                    case RangedWeaponPowerup.Gauss:
                        return new GaussGun(owner, name);
                    case RangedWeaponPowerup.Gravity:
                    case RangedWeaponPowerup.GravityDE:
                        return new GravityGun(owner, name, powerup);
                    case RangedWeaponPowerup.Minigun:
                        return new MiniGun(owner, name);
                    case RangedWeaponPowerup.Taser:
                        return new TaserGun(owner, name);
                    default:
                        return new RangeWpn(owner, name, powerup);
                }
            }
            catch
            {
                return new RangeWpn(owner, name, powerup);
            }
        }
        public static MeleeWpn Create(IPlayer owner, WeaponItem name, MeleeWeaponPowerup powerup)
        {
            try
            {
                switch (powerup)
                {
                    case MeleeWeaponPowerup.Hurling:
                        return new HurlingPowerup(owner, name);
                    case MeleeWeaponPowerup.Pushback:
                        return new PushbackPowerup(owner, name);
                    case MeleeWeaponPowerup.Splitting:
                        return new SplittingPowerup(owner, name);
                    default:
                        return new MeleeWpn(owner, name, powerup);
                }
            }
            catch
            {
                return new MeleeWpn(owner, name, powerup);
            }
        }
    }
}
