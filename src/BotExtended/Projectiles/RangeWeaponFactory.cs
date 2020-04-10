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
                case RangedWeaponPowerup.Gauss:
                    return new GaussGun(owner, name);
                case RangedWeaponPowerup.Gravity:
                case RangedWeaponPowerup.GravityDE:
                    return new GravityGun(owner, name, powerup);
                default:
                    return new RangeWpn(owner, name, powerup);
            }
        }
    }
}
