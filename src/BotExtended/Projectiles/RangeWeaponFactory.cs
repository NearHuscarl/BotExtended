using BotExtended.Library;
using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    static class RangeWeaponFactory
    {
        public static RangeWpn Create(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup)
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
    }
}
