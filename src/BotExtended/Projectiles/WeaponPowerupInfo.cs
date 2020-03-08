using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class WeaponPowerupInfo
    {
        public WeaponItem Melee;
        public MeleeWeaponPowerup MeleePowerup;

        public WeaponItem Primary;
        public RangedWeaponPowerup PrimaryPowerup;

        public WeaponItem Secondary;
        public RangedWeaponPowerup SecondaryPowerup;

        public WeaponItem Throwable;
        public RangedWeaponPowerup ThrowablePowerup;

        public WeaponItem Powerup;
    }
}
