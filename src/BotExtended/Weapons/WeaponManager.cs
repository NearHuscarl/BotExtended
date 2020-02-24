using BotExtended.Library;
using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended.Weapons
{
    class WeaponManager
    {
        private class ProjectileInfo
        {
            public IObject Projectile;
            public RangedWeaponPowerup Powerup;
        }
        private class WeaponInfo
        {
            public IObjectWeaponItem Weapon;
            public RangedWeaponPowerup Powerup;
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

        internal static void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weapon)
        {
            WeaponPowerupInfo weaponInfo = GetWeaponPowerupInfo(previousOwner.UniqueID);

            if (weapon.WeaponItemType == WeaponItemType.Rifle && weaponInfo.PrimaryPowerup != RangedWeaponPowerup.None)
            {
                m_weapons.Add(weapon.UniqueID, new WeaponInfo()
                {
                    Powerup = weaponInfo.PrimaryPowerup,
                    Weapon = weapon,
                });
                m_owners[previousOwner.UniqueID].PrimaryPowerup = RangedWeaponPowerup.None;
                m_owners[previousOwner.UniqueID].Primary = WeaponItem.NONE;
            }
            if (weapon.WeaponItemType == WeaponItemType.Handgun && weaponInfo.SecondaryPowerup != RangedWeaponPowerup.None)
            {
                m_weapons.Add(weapon.UniqueID, new WeaponInfo()
                {
                    Powerup = weaponInfo.SecondaryPowerup,
                    Weapon = weapon,
                });
                m_owners[previousOwner.UniqueID].SecondaryPowerup = RangedWeaponPowerup.None;
                m_owners[previousOwner.UniqueID].Secondary = WeaponItem.NONE;
            }
        }

        internal static void OnPlayerPickUpWeapon(IPlayer newOwner, IObjectWeaponItem weapon)
        {
            if (!m_weapons.ContainsKey(weapon.UniqueID)) return;

            WeaponPowerupInfo weaponInfo = GetWeaponPowerupInfo(newOwner.UniqueID);

            if (weapon.WeaponItemType == WeaponItemType.Rifle)
            {
                m_owners[newOwner.UniqueID].PrimaryPowerup = m_weapons[weapon.UniqueID].Powerup;
                m_owners[newOwner.UniqueID].Primary = weapon.WeaponItem;
                m_weapons.Remove(weapon.UniqueID);
            }
            if (weapon.WeaponItemType == WeaponItemType.Handgun)
            {
                m_owners[newOwner.UniqueID].SecondaryPowerup = m_weapons[weapon.UniqueID].Powerup;
                m_owners[newOwner.UniqueID].Secondary = weapon.WeaponItem;
                m_weapons.Remove(weapon.UniqueID);
            }
        }

        private static void OnProjectileCreated(IProjectile[] projectiles)
        {
            foreach (var projectile in projectiles)
            {
                foreach (var owner in m_owners.Keys)
                {
                    if (projectile.InitialOwnerPlayerID == owner)
                    {
                        var weaponItem = ScriptHelper.GetWeaponItem(projectile.ProjectileItem);
                        var powerup = RangedWeaponPowerup.None;

                        if (weaponItem == m_owners[owner].Primary)
                            powerup = m_owners[owner].PrimaryPowerup;
                        if (weaponItem == m_owners[owner].Secondary)
                            powerup = m_owners[owner].SecondaryPowerup;

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
