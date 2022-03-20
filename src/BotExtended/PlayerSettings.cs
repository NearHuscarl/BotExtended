using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended
{
    public class PlayerSettings
    {
        public string AccountID = null;
        public string BotType = null;
        public List<string[]> Weapons;

        private static List<string[]> EmptyWeapon
        {
            get
            {
                return new List<string[]>()
                {
                    new string[] { "NONE", "None" },
                    new string[] { "NONE", "None" },
                    new string[] { "NONE", "None" },
                    new string[] { "NONE", "None" },
                    new string[] { "NONE", "None" },
                };
            }
        }
        public static PlayerSettings Empty(string userID)
        {
            return new PlayerSettings()
            {
                AccountID = userID,
                BotType = "None",
                Weapons = EmptyWeapon
            };
        }

        public bool IsEmpty()
        {
            if (BotType != "None") return false;

            foreach (var w in Weapons)
            {
                if (w[0] != "NONE" && w[1] != "None")
                    return false;
            }

            return true;
        }

        public static PlayerSettings Parse(string str)
        {
            var pieces = str.Split('.');

            return new PlayerSettings()
            {
                AccountID = pieces[0],
                BotType = pieces[1],
                Weapons = new List<string[]>()
                {
                    new string[] { pieces[2], pieces[3] },
                    new string[] { pieces[4], pieces[5] },
                    new string[] { pieces[6], pieces[7] },
                    new string[] { pieces[8], pieces[9] },
                    new string[] { pieces[10], pieces[11] },
                },
            };
        }

        public PlayerSettings Update(string botType)
        {
            BotType = botType;
            return this;
        }

        public PlayerSettings Update(WeaponItemType type, string weaponItem, string powerup)
        {
            switch (type)
            {
                case WeaponItemType.NONE:
                case WeaponItemType.Melee:
                    if (powerup.ToLowerInvariant() == "none")
                    {
                        Weapons = EmptyWeapon;
                        break;
                    }
                    Weapons[0][0] = weaponItem;
                    Weapons[0][1] = powerup;
                    break;
                case WeaponItemType.Rifle:
                    Weapons[1][0] = weaponItem;
                    Weapons[1][1] = powerup;
                    break;
                case WeaponItemType.Handgun:
                    Weapons[2][0] = weaponItem;
                    Weapons[2][1] = powerup;
                    break;
                case WeaponItemType.Thrown:
                    Weapons[3][0] = weaponItem;
                    Weapons[3][1] = powerup;
                    break;
                case WeaponItemType.Powerup:
                    Weapons[4][0] = weaponItem;
                    Weapons[4][1] = powerup;
                    break;
            }
            return this;
        }

        public override string ToString()
        {
            return AccountID + "." + BotType + "."
                + Weapons[0][0] + "." + Weapons[0][1] + "."
                + Weapons[1][0] + "." + Weapons[1][1] + "."
                + Weapons[2][0] + "." + Weapons[2][1] + "."
                + Weapons[3][0] + "." + Weapons[3][1] + "."
                + Weapons[4][0] + "." + Weapons[4][1];
        }
    }
}
