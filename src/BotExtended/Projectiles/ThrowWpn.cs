using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    // Placeholder for now
    class ThrowWpn
    {
        public WeaponItem Name { get; set; }
        public RangedWeaponPowerup Powerup { get; set; }

        public ThrowWpn()
        {
            Name = WeaponItem.NONE;
            Powerup = RangedWeaponPowerup.None;
        }
    }
}
