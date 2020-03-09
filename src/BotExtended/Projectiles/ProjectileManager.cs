using BotExtended.Library;
using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended.Projectiles
{
    class ProjectileManager
    {
        private class ProjectileInfo
        {
            public IObject Projectile;
            public RangedWeaponPowerup Powerup;
        }
        public class WeaponInfo
        {
            public IObjectWeaponItem Weapon = null;
            public float TotalAmmo = -1;
            public RangedWeaponPowerup RangePowerup = RangedWeaponPowerup.None;
            public MeleeWeaponPowerup MeleePowerup = MeleeWeaponPowerup.None;
        }

        private static Dictionary<int, ProjectileInfo> m_extendedProjs = new Dictionary<int, ProjectileInfo>();
        private static Dictionary<int, WeaponInfo> m_weapons = new Dictionary<int, WeaponInfo>();
        private static Dictionary<int, WeaponPowerupInfo> m_owners = new Dictionary<int, WeaponPowerupInfo>();
        private static readonly Dictionary<RangedWeaponPowerup, ProjectileHooks> m_projHooks =
            new Dictionary<RangedWeaponPowerup, ProjectileHooks>()
            {
                { RangedWeaponPowerup.Present, new PresentBullet() }
            };

        public static void Initialize()
        {
            Events.ProjectileCreatedCallback.Start(OnProjectileCreated);
            Events.ObjectTerminatedCallback.Start(OnObjectTerminated);
        }

        private static WeaponPowerupInfo GetWeaponPowerupInfo(int playerUniqueID)
        {
            WeaponPowerupInfo weaponInfo;
            if (!m_owners.TryGetValue(playerUniqueID, out weaponInfo))
            {
                weaponInfo = new WeaponPowerupInfo();
                m_owners.Add(playerUniqueID, weaponInfo);
            }
            return weaponInfo;
        }

        internal static void SetPrimaryPowerup(IPlayer player, WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            if (powerup == RangedWeaponPowerup.None) return;
            WeaponPowerupInfo weaponInfo = GetWeaponPowerupInfo(player.UniqueID);

            weaponInfo.Primary = weaponItem;
            weaponInfo.PrimaryPowerup = powerup;
            m_owners[player.UniqueID] = weaponInfo;
        }

        internal static void SetSecondaryPowerup(IPlayer player, WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            if (powerup == RangedWeaponPowerup.None) return;
            WeaponPowerupInfo weaponInfo = GetWeaponPowerupInfo(player.UniqueID);

            weaponInfo.Secondary = weaponItem;
            weaponInfo.SecondaryPowerup = powerup;
            m_owners[player.UniqueID] = weaponInfo;
        }

        internal static void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weapon, float totalAmmo)
        {
            var oldWeaponInfo = GetWeaponPowerupInfo(previousOwner.UniqueID);
            var newWeaponInfo = new WeaponInfo()
            {
                Weapon = weapon,
                TotalAmmo = totalAmmo,
            };

            switch (weapon.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    newWeaponInfo.MeleePowerup = oldWeaponInfo.MeleePowerup;
                    m_owners[previousOwner.UniqueID].Melee = WeaponItem.NONE;
                    m_owners[previousOwner.UniqueID].MeleePowerup = MeleeWeaponPowerup.None;
                    break;
                case WeaponItemType.Rifle:
                    newWeaponInfo.RangePowerup = oldWeaponInfo.PrimaryPowerup;
                    m_owners[previousOwner.UniqueID].Primary = WeaponItem.NONE;
                    m_owners[previousOwner.UniqueID].PrimaryPowerup = RangedWeaponPowerup.None;
                    break;
                case WeaponItemType.Handgun:
                    newWeaponInfo.RangePowerup = oldWeaponInfo.SecondaryPowerup;
                    m_owners[previousOwner.UniqueID].Secondary = WeaponItem.NONE;
                    m_owners[previousOwner.UniqueID].SecondaryPowerup = RangedWeaponPowerup.None;
                    break;
                case WeaponItemType.Thrown:
                    newWeaponInfo.RangePowerup = oldWeaponInfo.ThrowablePowerup;
                    m_owners[previousOwner.UniqueID].Throwable = WeaponItem.NONE;
                    m_owners[previousOwner.UniqueID].ThrowablePowerup = RangedWeaponPowerup.None;
                    break;
            }

            m_weapons.Add(weapon.UniqueID, newWeaponInfo);
        }

        public static bool IsAlreadyTracked(IObject weapon)
        {
            return m_weapons.ContainsKey(weapon.UniqueID);
        }

        internal static void OnPlayerPickUpWeapon(IPlayer newOwner, IObjectWeaponItem weapon, float totalAmmo)
        {
            if (!m_weapons.ContainsKey(weapon.UniqueID)) return;

            var weaponInfo = GetWeaponPowerupInfo(newOwner.UniqueID);

            switch (weapon.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    m_owners[newOwner.UniqueID].Melee = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].MeleePowerup = m_weapons[weapon.UniqueID].MeleePowerup;
                    break;
                case WeaponItemType.Rifle:
                    m_owners[newOwner.UniqueID].Primary = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].PrimaryPowerup = m_weapons[weapon.UniqueID].RangePowerup;
                    break;
                case WeaponItemType.Handgun:
                    m_owners[newOwner.UniqueID].Secondary = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].SecondaryPowerup = m_weapons[weapon.UniqueID].RangePowerup;
                    break;
                case WeaponItemType.Thrown:
                    m_owners[newOwner.UniqueID].Throwable = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].ThrowablePowerup = m_weapons[weapon.UniqueID].RangePowerup;
                    break;
            }

            m_weapons.Remove(weapon.UniqueID);
        }

        public static WeaponInfo GetWeaponInfo(int objectID)
        {
            WeaponInfo weaponInfo;
            if (!m_weapons.TryGetValue(objectID, out weaponInfo))
                return new WeaponInfo();
            return weaponInfo;
        }

        private static void OnProjectileCreated(IProjectile[] projectiles)
        {
            foreach (var projectile in projectiles)
            {
                var powerupInfo = GetWeaponPowerupInfo(projectile.InitialOwnerPlayerID);
                var powerup = RangedWeaponPowerup.None;
                var weaponItem = ScriptHelper.GetWeaponItem(projectile.ProjectileItem);

                if (weaponItem == powerupInfo.Primary)
                    powerup = powerupInfo.PrimaryPowerup;
                if (weaponItem == powerupInfo.Secondary)
                    powerup = powerupInfo.SecondaryPowerup;

                if (powerup != RangedWeaponPowerup.None)
                {
                    var proj = m_projHooks[powerup].OnProjectileCreated(projectile);
                    m_extendedProjs.Add(proj.UniqueID, new ProjectileInfo()
                    {
                        Projectile = proj,
                        Powerup = powerup,
                    });
                }
            }
        }

        private static void OnObjectTerminated(IObject[] objs)
        {
            foreach (var obj in objs)
            {
                if (m_extendedProjs.ContainsKey(obj.UniqueID))
                {
                    var projInfo = m_extendedProjs[obj.UniqueID];

                    m_projHooks[projInfo.Powerup].OnProjectileHit(projInfo.Projectile);
                    m_extendedProjs.Remove(obj.UniqueID);
                }
            }
        }
    }
}
