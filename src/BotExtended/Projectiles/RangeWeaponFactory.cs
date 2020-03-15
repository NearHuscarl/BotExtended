using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    static class RangeWeaponFactory
    {
        public static RangeWpn Create(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup)
        {
            switch (powerup)
            {
                case RangedWeaponPowerup.Gravity:
                    return new GravityGun(owner, name);
                default:
                    return new RangeWpn(owner);
            }
        }
    }
}
