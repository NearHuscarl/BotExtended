using BotExtended.Powerups;
using SFDGameScriptInterface;

namespace BotExtended
{
    public class WeaponSet
    {
        public WeaponSet()
        {
            Melee = WeaponItem.NONE;
            Primary = WeaponItem.NONE;
            Secondary = WeaponItem.NONE;
            Throwable = WeaponItem.NONE;
            Powerup = WeaponItem.NONE;
            PrimaryPowerup = RangedWeaponPowerup.None;
            SecondaryPowerup = RangedWeaponPowerup.None;
            UseLazer = false;
        }

        static WeaponSet()
        {
            Empty = new WeaponSet();
        }

        public static WeaponSet Empty { get; private set; }

        public WeaponItem Melee { get; set; }
        public WeaponItem Primary { get; set; }
        public WeaponItem Secondary { get; set; }
        public WeaponItem Throwable { get; set; }
        public WeaponItem Powerup { get; set; }
        public RangedWeaponPowerup PrimaryPowerup { get; set; }
        public RangedWeaponPowerup SecondaryPowerup { get; set; }
        public bool UseLazer { get; set; }
        public bool IsEmpty
        {
            get
            {
                return Melee == WeaponItem.NONE
                  && Primary == WeaponItem.NONE
                  && Secondary == WeaponItem.NONE
                  && Throwable == WeaponItem.NONE
                  && Powerup == WeaponItem.NONE
                  && PrimaryPowerup == RangedWeaponPowerup.None
                  && SecondaryPowerup == RangedWeaponPowerup.None
                  && UseLazer == false;
            }
        }
    }
}
