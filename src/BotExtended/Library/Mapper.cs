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

        public static ProjectileItem GetProjectile(WeaponItem weaponItem)
        {
            switch (weaponItem)
            {
                case WeaponItem.ASSAULT:
                    return ProjectileItem.ASSAULT;
                case WeaponItem.BAZOOKA:
                    return ProjectileItem.BAZOOKA;
                case WeaponItem.BOW:
                    return ProjectileItem.BOW;
                case WeaponItem.CARBINE:
                    return ProjectileItem.CARBINE;
                case WeaponItem.DARK_SHOTGUN:
                    return ProjectileItem.DARK_SHOTGUN;
                case WeaponItem.GRENADE_LAUNCHER:
                    return ProjectileItem.GRENADE_LAUNCHER;
                case WeaponItem.M60:
                    return ProjectileItem.M60;
                case WeaponItem.MP50:
                    return ProjectileItem.MP50;
                case WeaponItem.SAWED_OFF:
                    return ProjectileItem.SAWED_OFF;
                case WeaponItem.SHOTGUN:
                    return ProjectileItem.SHOTGUN;
                case WeaponItem.SMG:
                    return ProjectileItem.SMG;
                case WeaponItem.SNIPER:
                    return ProjectileItem.SNIPER;
                case WeaponItem.TOMMYGUN:
                    return ProjectileItem.TOMMYGUN;

                case WeaponItem.FLAREGUN:
                    return ProjectileItem.FLAREGUN;
                case WeaponItem.MACHINE_PISTOL:
                    return ProjectileItem.MACHINE_PISTOL;
                case WeaponItem.MAGNUM:
                    return ProjectileItem.MAGNUM;
                case WeaponItem.PISTOL:
                    return ProjectileItem.PISTOL;
                case WeaponItem.PISTOL45:
                    return ProjectileItem.PISTOL45;
                case WeaponItem.REVOLVER:
                    return ProjectileItem.REVOLVER;
                case WeaponItem.SILENCEDPISTOL:
                    return ProjectileItem.SILENCEDPISTOL;
                case WeaponItem.SILENCEDUZI:
                    return ProjectileItem.SILENCEDUZI;
                case WeaponItem.UZI:
                    return ProjectileItem.UZI;

                default:
                    return ProjectileItem.NONE;
            }
        }

        public static WeaponItem GetWeaponItem(ProjectileItem projectileItem)
        {
            if (projectileItem == ProjectileItem.FLAKCANNON)
                return WeaponItem.NONE;
            // Game bug: https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&t=4333
            if (projectileItem == ProjectileItem.SUB_MACHINEGUN)
                return WeaponItem.TOMMYGUN;
            return SharpHelper.StringToEnum<WeaponItem>(SharpHelper.EnumToString<ProjectileItem>(projectileItem));
        }
    }
}
