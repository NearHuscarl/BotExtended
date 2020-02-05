using SFDGameScriptInterface;

namespace SFDScript.BotExtended
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
            UseLazer = false;
        }

        public void Equip(IPlayer player)
        {
            if (player == null || IsEmpty) return;

            player.GiveWeaponItem(Melee);
            player.GiveWeaponItem(Primary);
            player.GiveWeaponItem(Secondary);
            player.GiveWeaponItem(Throwable);
            player.GiveWeaponItem(Powerup);

            if (UseLazer) player.GiveWeaponItem(WeaponItem.LAZER);
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
                  && UseLazer == false;
            }
        }
    }
}
