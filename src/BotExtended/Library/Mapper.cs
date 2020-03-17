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
            switch (projectileItem)
            {
                case ProjectileItem.ASSAULT:
                    return WeaponItem.ASSAULT;
                case ProjectileItem.BAZOOKA:
                    return WeaponItem.BAZOOKA;
                case ProjectileItem.BOW:
                    return WeaponItem.BOW;
                case ProjectileItem.CARBINE:
                    return WeaponItem.CARBINE;
                case ProjectileItem.DARK_SHOTGUN:
                    return WeaponItem.DARK_SHOTGUN;
                case ProjectileItem.FLAKCANNON:
                    return WeaponItem.NONE;
                case ProjectileItem.FLAREGUN:
                    return WeaponItem.FLAREGUN;
                case ProjectileItem.GRENADE_LAUNCHER:
                    return WeaponItem.GRENADE_LAUNCHER;
                case ProjectileItem.M60:
                    return WeaponItem.M60;
                case ProjectileItem.MACHINE_PISTOL:
                    return WeaponItem.MACHINE_PISTOL;
                case ProjectileItem.MAGNUM:
                    return WeaponItem.MAGNUM;
                case ProjectileItem.MP50:
                    return WeaponItem.MP50;
                case ProjectileItem.PISTOL:
                    return WeaponItem.PISTOL;
                case ProjectileItem.PISTOL45:
                    return WeaponItem.PISTOL45;
                case ProjectileItem.REVOLVER:
                    return WeaponItem.REVOLVER;
                case ProjectileItem.SAWED_OFF:
                    return WeaponItem.SAWED_OFF;
                case ProjectileItem.SHOTGUN:
                    return WeaponItem.SHOTGUN;
                case ProjectileItem.SILENCEDPISTOL:
                    return WeaponItem.SILENCEDPISTOL;
                case ProjectileItem.SILENCEDUZI:
                    return WeaponItem.SILENCEDUZI;
                case ProjectileItem.SMG:
                    return WeaponItem.SMG;
                case ProjectileItem.SNIPER:
                    return WeaponItem.SNIPER;
                case ProjectileItem.TOMMYGUN:
                    return WeaponItem.TOMMYGUN;
                case ProjectileItem.UZI:
                    return WeaponItem.UZI;
                default:
                    return WeaponItem.NONE;
            }
        }

        public static string ObjectID(WeaponItem weaponItem, bool activated = false)
        {
            switch (weaponItem)
            {
                case WeaponItem.ASSAULT:
                    return "WpnAssaultRifle";
                case WeaponItem.BAZOOKA:
                    return "WpnBazooka";
                case WeaponItem.BOW:
                    return "WpnBow";
                case WeaponItem.CARBINE:
                    return "WpnCarbine";
                case WeaponItem.DARK_SHOTGUN:
                    return "WpnDarkShotgun";
                case WeaponItem.FLAREGUN:
                    return "WpnFlareGun";
                case WeaponItem.FLAMETHROWER:
                    return "WpnFlamethrower";
                case WeaponItem.GRENADE_LAUNCHER:
                    return "WpnGrenadeLauncher";
                case WeaponItem.M60:
                    return "WpnM60";
                case WeaponItem.MACHINE_PISTOL:
                    return "WpnMachinePistol";
                case WeaponItem.MAGNUM:
                    return "WpnMagnum";
                case WeaponItem.MP50:
                    return "WpnMP50";
                case WeaponItem.PISTOL:
                    return "WpnPistol";
                case WeaponItem.PISTOL45:
                    return "WpnPistol45";
                case WeaponItem.REVOLVER:
                    return "WpnRevolver";
                case WeaponItem.SAWED_OFF:
                    return "WpnSawedoff";
                case WeaponItem.SHOTGUN:
                    return "WpnPumpShotgun";
                case WeaponItem.SILENCEDPISTOL:
                    return "WpnSilencedPistol";
                case WeaponItem.SILENCEDUZI:
                    return "WpnSilencedUzi";
                case WeaponItem.SMG:
                    return "WpnSMG";
                case WeaponItem.SNIPER:
                    return "WpnSniperRifle";
                case WeaponItem.TOMMYGUN:
                    return "WpnTommygun";
                case WeaponItem.UZI:
                    return "WpnUzi";
                case WeaponItem.PIPE:
                    return "WpnPipeWrench";
                case WeaponItem.CHAIN:
                    return "WpnChain";
                case WeaponItem.WHIP:
                    return "WpnWhip";
                case WeaponItem.HAMMER:
                    return "WpnHammer";
                case WeaponItem.KATANA:
                    return "WpnKatana";
                case WeaponItem.MACHETE:
                    return "WpnMachete";
                case WeaponItem.CHAINSAW:
                    return "WpnChainsaw";
                case WeaponItem.KNIFE:
                    return "WpnKnife";
                case WeaponItem.BAT:
                    return "WpnBat";
                case WeaponItem.BATON:
                    return "WpnBaton";
                case WeaponItem.SHOCK_BATON:
                    return "WpnShockBaton";
                case WeaponItem.LEAD_PIPE:
                    return "WpnLeadPipe";
                case WeaponItem.AXE:
                    return "WpnAxe";
                case WeaponItem.GRENADES:
                    return activated ? "WpnGrenadesThrown" : "WpnGrenades";
                case WeaponItem.MOLOTOVS:
                    return activated ? "WpnMolotovsThrown" : "WpnMolotovs";
                case WeaponItem.MINES:
                    return activated ? "WpnMineThrown" : "WpnMines";
                case WeaponItem.C4:
                    return activated ? "WpnC4Thrown" : "WpnC4";
                case WeaponItem.SHURIKEN:
                    return "WpnShuriken";
                case WeaponItem.BASEBALL:
                    return "Baseball";
                case WeaponItem.BOTTLE:
                    return "Bottle00";
                case WeaponItem.BROKEN_BOTTLE:
                    return "Bottle00Broken";
                case WeaponItem.CHAIR:
                    return "Chair00";
                case WeaponItem.CUESTICK:
                    return "CueStick00";
                case WeaponItem.CUESTICK_SHAFT:
                    return "CueStick00Shaft";
                case WeaponItem.FLAGPOLE:
                    return "FlagpoleUS";
                case WeaponItem.PILLOW:
                    return "Pillow00";
                case WeaponItem.SUITCASE:
                    return "Suitcase00";
                case WeaponItem.TEAPOT:
                    return "Teapot00";
                case WeaponItem.TRASH_BAG:
                    return "Trashbag00";
                case WeaponItem.TRASHCAN_LID:
                    return "Trashcan00Lid";
                case WeaponItem.CHAIR_LEG:
                    return "ChairLeg";
                default:
                    return "";
            }
        }
    }
}
