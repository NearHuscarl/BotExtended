using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Projectiles
{
    class ProjectileManager
    {
        private class ProjectileInfo
        {
            public IProjectile Projectile;
            public RangedWeaponPowerup Powerup;
        }
        private class CustomProjectileInfo
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
            public bool HasPowerup
            {
                get { return RangePowerup != RangedWeaponPowerup.None || MeleePowerup != MeleeWeaponPowerup.None; }
            }
        }

        private class Weapon
        {
            public Weapon(WeaponInfo info) { WeaponInfo = info; }
            public WeaponInfo WeaponInfo;
            public float EffectTime = 0f;
        }

        private static Dictionary<int, CustomProjectileInfo> m_customProjectiles = new Dictionary<int, CustomProjectileInfo>();
        private static Dictionary<int, ProjectileInfo> m_projectiles = new Dictionary<int, ProjectileInfo>();
        private static Dictionary<int, Weapon> m_weapons = new Dictionary<int, Weapon>();
        private static Dictionary<int, WeaponPowerupInfo> m_owners = new Dictionary<int, WeaponPowerupInfo>();
        private static readonly Dictionary<RangedWeaponPowerup, ProjectileHooks> m_projHooks =
            new Dictionary<RangedWeaponPowerup, ProjectileHooks>()
            {
                { RangedWeaponPowerup.Present, new PresentBullet() },
                { RangedWeaponPowerup.Stun, new StunBullet() }
            };

        public static void Initialize()
        {
            Events.UpdateCallback.Start(OnUpdate);
            Events.ProjectileCreatedCallback.Start(OnProjectileCreated);
            Events.ProjectileHitCallback.Start(OnProjectileHit);
            Events.ObjectTerminatedCallback.Start(OnObjectTerminated);
        }

        private static void OnUpdate(float elapsed)
        {
            var removedKeys = new List<int>();

            foreach (var item in m_weapons)
            {
                var weapon = item.Value;
                Game.DrawArea(weapon.WeaponInfo.Weapon.GetAABB());
                Game.DrawCircle(weapon.WeaponInfo.Weapon.GetWorldPosition(), 1, Color.Red);

                var weaponObject = weapon.WeaponInfo.Weapon;

                if (weaponObject.IsRemoved)
                {
                    removedKeys.Add(item.Key);
                    continue;
                }
                if (weapon.WeaponInfo.HasPowerup)
                {
                    PlayMoreShinyEffect(weapon, elapsed);
                }
            }

            foreach (var key in removedKeys)
                m_weapons.Remove(key);
        }

        private static void PlayMoreShinyEffect(Weapon weapon, float elapsed)
        {
            var weaponObject = weapon.WeaponInfo.Weapon;
            var hitBox = weaponObject.GetAABB();
            weapon.EffectTime += elapsed;

            if (weapon.EffectTime >= 400)
            {
                Game.PlayEffect(EffectName.ItemGleam, new Vector2()
                {
                    X = RandomHelper.Between(hitBox.Left, hitBox.Right),
                    Y = RandomHelper.Between(hitBox.Bottom, hitBox.Top),
                });
                weapon.EffectTime = 0f;
            }
        }

        private static WeaponPowerupInfo GetOrCreateWeaponPowerupInfo(int playerUniqueID)
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
            WeaponPowerupInfo weaponInfo = GetOrCreateWeaponPowerupInfo(player.UniqueID);

            weaponInfo.Primary = weaponItem;
            weaponInfo.PrimaryPowerup = powerup;
            m_owners[player.UniqueID] = weaponInfo;
        }

        internal static void SetSecondaryPowerup(IPlayer player, WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            if (powerup == RangedWeaponPowerup.None) return;
            WeaponPowerupInfo weaponInfo = GetOrCreateWeaponPowerupInfo(player.UniqueID);

            weaponInfo.Secondary = weaponItem;
            weaponInfo.SecondaryPowerup = powerup;
            m_owners[player.UniqueID] = weaponInfo;
        }

        internal static void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weapon, float totalAmmo)
        {
            var oldWeaponInfo = GetOrCreateWeaponPowerupInfo(previousOwner.UniqueID);
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

            m_weapons.Add(weapon.UniqueID, new Weapon(newWeaponInfo));
        }

        public static bool IsAlreadyTracked(IObject weapon)
        {
            return m_weapons.ContainsKey(weapon.UniqueID);
        }

        internal static void OnPlayerPickUpWeapon(IPlayer newOwner, IObjectWeaponItem weapon, float totalAmmo)
        {
            if (!m_weapons.ContainsKey(weapon.UniqueID)) return;

            GetOrCreateWeaponPowerupInfo(newOwner.UniqueID);
            var weaponInfo = m_weapons[weapon.UniqueID].WeaponInfo;

            switch (weapon.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    m_owners[newOwner.UniqueID].Melee = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].MeleePowerup = weaponInfo.MeleePowerup;
                    break;
                case WeaponItemType.Rifle:
                    m_owners[newOwner.UniqueID].Primary = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].PrimaryPowerup = weaponInfo.RangePowerup;
                    break;
                case WeaponItemType.Handgun:
                    m_owners[newOwner.UniqueID].Secondary = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].SecondaryPowerup = weaponInfo.RangePowerup;
                    break;
                case WeaponItemType.Thrown:
                    m_owners[newOwner.UniqueID].Throwable = weapon.WeaponItem;
                    m_owners[newOwner.UniqueID].ThrowablePowerup = weaponInfo.RangePowerup;
                    break;
            }

            m_weapons.Remove(weapon.UniqueID);
        }

        public static WeaponInfo GetWeaponInfo(int objectID)
        {
            Weapon weapon;
            if (!m_weapons.TryGetValue(objectID, out weapon))
                return new WeaponInfo();
            return weapon.WeaponInfo;
        }

        private static void OnProjectileCreated(IProjectile[] projectiles)
        {
            foreach (var projectile in projectiles)
            {
                var powerupInfo = GetOrCreateWeaponPowerupInfo(projectile.InitialOwnerPlayerID);
                var powerup = RangedWeaponPowerup.None;
                var weaponItem = ScriptHelper.GetWeaponItem(projectile.ProjectileItem);

                if (weaponItem == powerupInfo.Primary)
                    powerup = powerupInfo.PrimaryPowerup;
                if (weaponItem == powerupInfo.Secondary)
                    powerup = powerupInfo.SecondaryPowerup;

                if (powerup != RangedWeaponPowerup.None)
                {
                    var customProjectile = m_projHooks[powerup].OnCustomProjectileCreated(projectile);
                    var normalProjectile = m_projHooks[powerup].OnProjectileCreated(projectile);

                    if (customProjectile != null)
                    {
                        m_customProjectiles.Add(customProjectile.UniqueID, new CustomProjectileInfo()
                        {
                            Projectile = customProjectile,
                            Powerup = powerup,
                        });
                    }
                    if (normalProjectile != null)
                    {
                        m_projectiles.Add(normalProjectile.InstanceID, new ProjectileInfo()
                        {
                            Projectile = normalProjectile,
                            Powerup = powerup,
                        });
                    }
                }
            }
        }

        private static void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            if (m_projectiles.ContainsKey(projectile.InstanceID))
            {
                var projInfo = m_projectiles[projectile.InstanceID];

                m_projHooks[projInfo.Powerup].OnProjectileHit(projInfo.Projectile, args);

                if (args.RemoveFlag)
                    m_projectiles.Remove(projectile.InstanceID);
            }
        }

        private static void OnObjectTerminated(IObject[] objs)
        {
            foreach (var obj in objs)
            {
                if (m_customProjectiles.ContainsKey(obj.UniqueID))
                {
                    var projInfo = m_customProjectiles[obj.UniqueID];

                    m_projHooks[projInfo.Powerup].OnCustomProjectileHit(projInfo.Projectile);
                    m_customProjectiles.Remove(obj.UniqueID);
                }
            }
        }
    }
}
