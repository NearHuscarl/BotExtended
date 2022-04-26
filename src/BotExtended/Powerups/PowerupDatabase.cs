using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Powerups
{
    public static class MeleeWpns
    {
        public static bool IsMeleeWpns(WeaponItem weaponItem)
        {
            var type = Mapper.GetWeaponItemType(weaponItem);
            return type == WeaponItemType.NONE || type == WeaponItemType.Melee;
        }

        public static bool IsDefaultWpns(WeaponItem weaponItem)
        {
            return IsMeleeWpns(weaponItem) && !Mapper.IsMakeshiftWeapon(weaponItem) && weaponItem != WeaponItem.CHAINSAW;
        }

        public static bool IsBarehand(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.NONE;
        }

        // weapons that have the beat-the-ground animation on third attack
        public static bool IsWpnsWithHandle(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.MACHETE
                || weaponItem == WeaponItem.AXE
                || weaponItem == WeaponItem.BAT
                || weaponItem == WeaponItem.BATON
                || weaponItem == WeaponItem.SHOCK_BATON
                || weaponItem == WeaponItem.PIPE
                || weaponItem == WeaponItem.HAMMER
                || weaponItem == WeaponItem.LEAD_PIPE
                || weaponItem == WeaponItem.KATANA;
        }

        public static bool IsSharpWpns(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.AXE
                || weaponItem == WeaponItem.KATANA
                || weaponItem == WeaponItem.KNIFE
                || weaponItem == WeaponItem.MACHETE;
        }
    }
    public static class RangedWpns
    {
        // TODO: refactor other places
        public static bool IsRangedWpns(WeaponItem weaponItem)
        {
            var type = Mapper.GetWeaponItemType(weaponItem);
            return type == WeaponItemType.Rifle || type == WeaponItemType.Handgun;
        }

        public static bool IsDefaultWpns(WeaponItem weaponItem)
        {
            return IsRangedWpns(weaponItem);
        }

        public static bool IsExplosiveWpns(WeaponItem weaponItem)
        {
            // TODO: test Flak Cannon
            return weaponItem == WeaponItem.BAZOOKA
                || weaponItem == WeaponItem.GRENADE_LAUNCHER;
        }

        public static bool IsNonExplosiveWpns(WeaponItem weaponItem)
        {
            return IsRangedWpns(weaponItem) && !IsExplosiveWpns(weaponItem);
        }

        public static bool IsSmallArm(WeaponItem weaponItem)
        {
            // weapons that shoot bullets
            return IsRangedWpns(weaponItem)
                && weaponItem != WeaponItem.BOW
                && weaponItem != WeaponItem.BAZOOKA
                && weaponItem != WeaponItem.GRENADE_LAUNCHER
                && weaponItem != WeaponItem.FLAMETHROWER
                && weaponItem != WeaponItem.FLAREGUN;
        }
        public static bool IsSlowSmallArm(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.SNIPER
                || weaponItem == WeaponItem.MAGNUM
                || weaponItem == WeaponItem.REVOLVER
                || IsShotgunWpns(weaponItem);
        }

        public static bool IsSlowWpns(WeaponItem weaponItem)
        {
            // TODO: maybe add sniper or magnum?
            return weaponItem == WeaponItem.BOW
                || weaponItem == WeaponItem.BAZOOKA
                || weaponItem == WeaponItem.GRENADE_LAUNCHER
                || weaponItem == WeaponItem.FLAREGUN;
        }

        public static bool IsShotgunWpns(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.SHOTGUN
                || weaponItem == WeaponItem.DARK_SHOTGUN
                || weaponItem == WeaponItem.SAWED_OFF;
        }

        public static bool IsAutomaticWpns(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.M60
                || weaponItem == WeaponItem.MACHINE_PISTOL
                || weaponItem == WeaponItem.ASSAULT
                || weaponItem == WeaponItem.UZI
                || weaponItem == WeaponItem.SILENCEDUZI
                || weaponItem == WeaponItem.MP50
                || weaponItem == WeaponItem.SMG
                || weaponItem == WeaponItem.TOMMYGUN;
        }

        public static bool IsWeakPistols(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.PISTOL
                || weaponItem == WeaponItem.PISTOL45
                || weaponItem == WeaponItem.REVOLVER;
        }
    }

    public static class PowerupDatabase
    {
        public static bool IsWpns(WeaponItem weaponItem) { return true; }

        public static Dictionary<MeleeWeaponPowerup, Func<WeaponItem, bool>> MeleePowerupWpns = new Dictionary<MeleeWeaponPowerup, Func<WeaponItem, bool>>
        {
            { MeleeWeaponPowerup.None, IsWpns },
            { MeleeWeaponPowerup.Breaking, MeleeWpns.IsDefaultWpns },
            { MeleeWeaponPowerup.Earthquake, MeleeWpns.IsWpnsWithHandle },
            { MeleeWeaponPowerup.FireTrail, MeleeWpns.IsWpnsWithHandle },
            { MeleeWeaponPowerup.Gib, MeleeWpns.IsDefaultWpns },
            { MeleeWeaponPowerup.GroundBreaker, MeleeWpns.IsWpnsWithHandle },
            { MeleeWeaponPowerup.GroundSlam, MeleeWpns.IsBarehand },
            { MeleeWeaponPowerup.Hurling, MeleeWpns.IsDefaultWpns },
            { MeleeWeaponPowerup.Megaton, MeleeWpns.IsDefaultWpns },
            { MeleeWeaponPowerup.Pushback, MeleeWpns.IsDefaultWpns },
            { MeleeWeaponPowerup.Serious, MeleeWpns.IsDefaultWpns },
            { MeleeWeaponPowerup.Slide, MeleeWpns.IsDefaultWpns },
            { MeleeWeaponPowerup.Splitting, MeleeWpns.IsSharpWpns },
        };

        public static Dictionary<RangedWeaponPowerup, Func<WeaponItem, bool>> RangedPowerupWpns = new Dictionary<RangedWeaponPowerup, Func<WeaponItem, bool>>
        {
            { RangedWeaponPowerup.None, IsWpns },
            { RangedWeaponPowerup.Blackhole, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Blast, RangedWpns.IsNonExplosiveWpns },
            { RangedWeaponPowerup.BouncingLaser, RangedWpns.IsSmallArm },
            { RangedWeaponPowerup.Bow, w => w == WeaponItem.BOW },
            { RangedWeaponPowerup.Dormant, w => w == WeaponItem.GRENADE_LAUNCHER },
            { RangedWeaponPowerup.DoublePenetration, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.DoubleTrouble, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Fatigue, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Fire, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Grapeshot, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Helium, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Homing, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Hunting, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.InfiniteBouncing, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Knockback, RangedWpns.IsSmallArm },
            { RangedWeaponPowerup.Lightning, w => RangedWpns.IsSlowSmallArm(w) && !RangedWpns.IsShotgunWpns(w) },
            { RangedWeaponPowerup.Mine, w => w == WeaponItem.GRENADE_LAUNCHER },
            { RangedWeaponPowerup.Molotov, RangedWpns.IsSlowWpns },
            { RangedWeaponPowerup.Penetration, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Poison, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.PreciseBouncing, RangedWpns.IsSmallArm },
            { RangedWeaponPowerup.Precision, w => RangedWpns.IsSmallArm(w) && !RangedWpns.IsShotgunWpns(w) },
            { RangedWeaponPowerup.Present, RangedWpns.IsNonExplosiveWpns },
            { RangedWeaponPowerup.Riding, w => w == WeaponItem.BAZOOKA },
            { RangedWeaponPowerup.Shrapnel, RangedWpns.IsExplosiveWpns },
            { RangedWeaponPowerup.Shrinking, RangedWpns.IsExplosiveWpns },
            { RangedWeaponPowerup.Smoke, RangedWpns.IsSlowWpns },
            { RangedWeaponPowerup.Spinner, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Steak, RangedWpns.IsSlowWpns },
            { RangedWeaponPowerup.StickyBomb, RangedWpns.IsExplosiveWpns },
            { RangedWeaponPowerup.Stun, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.SuicideDove, RangedWpns.IsSlowWpns },
            { RangedWeaponPowerup.SuicideFighter, RangedWpns.IsSlowWpns },
            { RangedWeaponPowerup.Taco, w => w == WeaponItem.BAZOOKA },
            { RangedWeaponPowerup.Tearing, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Trigger, RangedWpns.IsSlowSmallArm },
            { RangedWeaponPowerup.Termite, RangedWpns.IsDefaultWpns },
            { RangedWeaponPowerup.Welding, RangedWpns.IsDefaultWpns },

            { RangedWeaponPowerup.Delay, RangedWpns.IsSmallArm },
            { RangedWeaponPowerup.Gauss, RangedWpns.IsSmallArm },
            { RangedWeaponPowerup.Gravity, RangedWpns.IsExplosiveWpns },
            { RangedWeaponPowerup.GravityDE, RangedWpns.IsExplosiveWpns },
            { RangedWeaponPowerup.Minigun, RangedWpns.IsAutomaticWpns },
            { RangedWeaponPowerup.Object, RangedWpns.IsSlowWpns },
            { RangedWeaponPowerup.Scattershot, RangedWpns.IsNonExplosiveWpns },
            { RangedWeaponPowerup.Taser, RangedWpns.IsWeakPistols },
        };

        // TODO: thrown weapons
        public static bool IsValidPowerup(MeleeWeaponPowerup powerup, WeaponItem weaponItem)
        {
            return MeleePowerupWpns[powerup].Invoke(weaponItem);
        }
        public static bool IsValidPowerup(RangedWeaponPowerup powerup, WeaponItem weaponItem)
        {
            return RangedPowerupWpns[powerup].Invoke(weaponItem);
        }

        public static IEnumerable<WeaponItem> GetValidWpns(MeleeWeaponPowerup powerup)
        {
            return SharpHelper.EnumToArray<WeaponItem>().Where(w => IsValidPowerup(powerup, w)).OrderBy(x => x.ToString());
        }
        public static IEnumerable<WeaponItem> GetValidWpns(RangedWeaponPowerup powerup)
        {
            return SharpHelper.EnumToArray<WeaponItem>().Where(w => IsValidPowerup(powerup, w)).OrderBy(x => x.ToString());
        }
    }
}
