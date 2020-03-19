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
                case RangedWeaponPowerup.Gravity:
                    return new GravityGun(owner, name, RangedWeaponPowerup.Gravity);
                case RangedWeaponPowerup.GravityDE:
                    return new GravityGun(owner, name, RangedWeaponPowerup.GravityDE);
                default:
                    return new RangeWpn(owner, name, powerup);
            }
        }
    }
}
