using SFDGameScriptInterface;

namespace BotExtended.Library
{
    public static class Mapper
    {
        public static WeaponItemType GetWeaponItemType(WeaponItem weaponItem)
        {
            // UPDATE: SFD.Weapons.WeaponDatabase 1.3.4
            switch (weaponItem)
            {
                case WeaponItem.ASSAULT:
                case WeaponItem.BAZOOKA:
                case WeaponItem.BOW:
                case WeaponItem.CARBINE:
                case WeaponItem.DARK_SHOTGUN:
                case WeaponItem.FLAMETHROWER:
                case WeaponItem.GRENADE_LAUNCHER:
                case WeaponItem.M60:
                case WeaponItem.MP50:
                case WeaponItem.SAWED_OFF:
                case WeaponItem.SHOTGUN:
                case WeaponItem.SMG:
                case WeaponItem.SNIPER:
                case WeaponItem.TOMMYGUN:
                    return WeaponItemType.Rifle;

                case WeaponItem.FLAREGUN:
                case WeaponItem.MACHINE_PISTOL:
                case WeaponItem.MAGNUM:
                case WeaponItem.PISTOL:
                case WeaponItem.PISTOL45:
                case WeaponItem.REVOLVER:
                case WeaponItem.SILENCEDPISTOL:
                case WeaponItem.SILENCEDUZI:
                case WeaponItem.UZI:
                    return WeaponItemType.Handgun;

                case WeaponItem.PIPE:
                case WeaponItem.CHAIN:
                case WeaponItem.WHIP:
                case WeaponItem.HAMMER:
                case WeaponItem.KATANA:
                case WeaponItem.MACHETE:
                case WeaponItem.CHAINSAW:
                case WeaponItem.KNIFE:
                case WeaponItem.BAT:
                case WeaponItem.BATON:
                case WeaponItem.SHOCK_BATON:
                case WeaponItem.LEAD_PIPE:
                case WeaponItem.AXE:
                case WeaponItem.BASEBALL:
                    return WeaponItemType.Melee;

                case WeaponItem.BOTTLE:
                case WeaponItem.BROKEN_BOTTLE:
                case WeaponItem.CHAIR:
                case WeaponItem.CUESTICK:
                case WeaponItem.CUESTICK_SHAFT:
                case WeaponItem.FLAGPOLE:
                case WeaponItem.PILLOW:
                case WeaponItem.SUITCASE:
                case WeaponItem.TEAPOT:
                case WeaponItem.TRASH_BAG:
                case WeaponItem.TRASHCAN_LID:
                case WeaponItem.CHAIR_LEG:
                    return WeaponItemType.Melee;

                case WeaponItem.GRENADES:
                case WeaponItem.MOLOTOVS:
                case WeaponItem.MINES:
                case WeaponItem.C4:
                case WeaponItem.C4DETONATOR:
                case WeaponItem.SHURIKEN:
                    return WeaponItemType.Thrown;

                case WeaponItem.STRENGTHBOOST:
                case WeaponItem.SPEEDBOOST:
                case WeaponItem.SLOWMO_5:
                case WeaponItem.SLOWMO_10:
                    return WeaponItemType.Powerup;

                case WeaponItem.PILLS:
                case WeaponItem.MEDKIT:
                case WeaponItem.LAZER:
                case WeaponItem.BOUNCINGAMMO:
                case WeaponItem.FIREAMMO:
                case WeaponItem.STREETSWEEPER:
                    return WeaponItemType.InstantPickup;

                default:
                    return WeaponItemType.NONE;
            }
        }

        public static WeaponItem GetWeaponItem(ProjectileItem projectileItem)
        {
            if (projectileItem == ProjectileItem.FLAKCANNON)
                return WeaponItem.NONE;
            return SharpHelper.StringToEnum<WeaponItem>(SharpHelper.EnumToString<ProjectileItem>(projectileItem));
        }
    }
}
