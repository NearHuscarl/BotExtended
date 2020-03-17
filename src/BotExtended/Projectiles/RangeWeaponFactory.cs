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
                    return new GravityGun(owner, name);
                default:
                    return new RangeWpn(owner, name, powerup);
            }
        }
    }
}
