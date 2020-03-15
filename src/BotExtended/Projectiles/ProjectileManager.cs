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
        private static Dictionary<int, PlayerWeapon> m_owners = new Dictionary<int, PlayerWeapon>();

        private static readonly Dictionary<RangedWeaponPowerup, ProjectileHooks> m_projHooks =
            new Dictionary<RangedWeaponPowerup, ProjectileHooks>()
            {
                { RangedWeaponPowerup.Present, new PresentBullet() },
                { RangedWeaponPowerup.Stun, new StunBullet() },
                { RangedWeaponPowerup.Gravity, new GravityGunHook() },
            };

        public static void Initialize()
        {
            Events.UpdateCallback.Start(OnUpdate);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.ProjectileCreatedCallback.Start(OnProjectileCreated);
            Events.ProjectileHitCallback.Start(OnProjectileHit);
            Events.ObjectTerminatedCallback.Start(OnObjectTerminated);
            Events.PlayerKeyInputCallback.Start(OnPlayerKeyInput);
        }

        private static void OnUpdate(float elapsed)
        {
            foreach (var item in m_weapons)
            {
                var weapon = item.Value;
                if (weapon.WeaponInfo.HasPowerup)
                {
                    PlayMoreShinyEffect(weapon, elapsed);
                }
            }

            foreach (var o in m_owners)
            {
                var player = o.Value.Primary.Owner;
                // Custom event drop weapon fires first and set the owner to null before
                // the player dead event actually remove the player from m_owner
                if (player == null) continue;

                var playerWpn = o.Value;

                if (player.CurrentWeaponDrawn == WeaponItemType.Rifle)
                {
                    playerWpn.Primary.Update(elapsed,
                         player.CurrentPrimaryWeapon.WeaponItem,
                         player.CurrentPrimaryWeapon.TotalAmmo);
                }
                if (player.CurrentWeaponDrawn == WeaponItemType.Handgun)
                {
                    playerWpn.Secondary.Update(elapsed,
                         player.CurrentPrimaryWeapon.WeaponItem,
                         player.CurrentPrimaryWeapon.TotalAmmo);
                }
            }
        }

        private static void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (args.Removed)
            {
                if (m_owners.ContainsKey(player.UniqueID))
                {
                    m_owners.Remove(player.UniqueID);
                }
            }
        }

        private static void OnPlayerKeyInput(IPlayer player, VirtualKeyInfo[] keyInfos)
        {
            PlayerWeapon playerWpn;

            if (m_owners.TryGetValue(player.UniqueID, out playerWpn))
            {
                if (player.CurrentWeaponDrawn == WeaponItemType.Rifle)
                {
                    playerWpn.Primary.OnPlayerKeyInput(keyInfos);
                }
                if (player.CurrentWeaponDrawn == WeaponItemType.Handgun)
                {
                    playerWpn.Secondary.OnPlayerKeyInput(keyInfos);
                }
            }
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

        private static PlayerWeapon GetOrCreatePlayerWeapon(int playerUniqueID)
        {
            PlayerWeapon playerWpn;
            if (!m_owners.TryGetValue(playerUniqueID, out playerWpn))
            {
                playerWpn = PlayerWeapon.Empty;
                m_owners.Add(playerUniqueID, playerWpn);
            }
            return playerWpn;
        }

        internal static void SetPrimaryPowerup(IPlayer player, WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            if (powerup == RangedWeaponPowerup.None) return;
            var playerWpn = GetOrCreatePlayerWeapon(player.UniqueID);

            playerWpn.Primary = RangeWeaponFactory.Create(player, weaponItem, powerup);
            m_owners[player.UniqueID] = playerWpn;
        }

        internal static void SetSecondaryPowerup(IPlayer player, WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            if (powerup == RangedWeaponPowerup.None) return;
            var playerWpn = GetOrCreatePlayerWeapon(player.UniqueID);

            playerWpn.Secondary = RangeWeaponFactory.Create(player, weaponItem, powerup);
            m_owners[player.UniqueID] = playerWpn;
        }

        internal static void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weapon, float totalAmmo)
        {
            var oldPlayerWpn = GetOrCreatePlayerWeapon(previousOwner.UniqueID);
            var newWeaponInfo = new WeaponInfo()
            {
                Weapon = weapon,
                TotalAmmo = totalAmmo,
            };

            switch (weapon.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    newWeaponInfo.MeleePowerup = oldPlayerWpn.Melee.Powerup;
                    m_owners[previousOwner.UniqueID].Melee.Remove();
                    break;
                case WeaponItemType.Rifle:
                    newWeaponInfo.RangePowerup = oldPlayerWpn.Primary.Powerup;
                    m_owners[previousOwner.UniqueID].Primary.Remove();
                    break;
                case WeaponItemType.Handgun:
                    newWeaponInfo.RangePowerup = oldPlayerWpn.Secondary.Powerup;
                    m_owners[previousOwner.UniqueID].Secondary.Remove();
                    break;
                case WeaponItemType.Thrown:
                    newWeaponInfo.RangePowerup = oldPlayerWpn.Throwable.Powerup;
                    m_owners[previousOwner.UniqueID].Throwable.Remove();
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

            GetOrCreatePlayerWeapon(newOwner.UniqueID);
            var weaponInfo = m_weapons[weapon.UniqueID].WeaponInfo;
            var createRangeWeapon = new Func<RangeWpn>(
                () => RangeWeaponFactory.Create(newOwner, weapon.WeaponItem, weaponInfo.RangePowerup));
            var aa = new Action<int>((int a) => { });

            switch (weapon.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    // TODO: create power melee weapon with factory if implement one
                    m_owners[newOwner.UniqueID].Melee.Add(weapon.WeaponItem, weaponInfo.MeleePowerup);
                    break;
                case WeaponItemType.Rifle:
                    m_owners[newOwner.UniqueID].Primary = createRangeWeapon();
                    break;
                case WeaponItemType.Handgun:
                    m_owners[newOwner.UniqueID].Secondary = createRangeWeapon();
                    break;
                case WeaponItemType.Thrown:
                    m_owners[newOwner.UniqueID].Throwable = createRangeWeapon();
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
                var ownerID = projectile.InitialOwnerPlayerID;

                // Projectile is not fired from IPlayer, custom weapon with custom powerup is not supported
                if (ownerID == 0) continue;

                var playerWpn = GetOrCreatePlayerWeapon(ownerID);
                var powerup = RangedWeaponPowerup.None;
                var weaponItem = ScriptHelper.GetWeaponItem(projectile.ProjectileItem);

                if (weaponItem == playerWpn.Primary.Name)
                    powerup = playerWpn.Primary.Powerup;
                if (weaponItem == playerWpn.Secondary.Name)
                    powerup = playerWpn.Secondary.Powerup;

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

                if (m_weapons.ContainsKey(obj.UniqueID))
                {
                    m_weapons.Remove(obj.UniqueID);
                }
            }
        }
    }
}
