using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class ProjectileManager
    {
        public class WeaponInfo
        {
            public IObjectWeaponItem Weapon = null;
            public RangedWeaponPowerup RangedPowerup = RangedWeaponPowerup.None;
            public MeleeWeaponPowerup MeleePowerup = MeleeWeaponPowerup.None;
            public bool HasPowerup
            {
                get { return RangedPowerup != RangedWeaponPowerup.None || MeleePowerup != MeleeWeaponPowerup.None; }
            }
        }

        private class Weapon
        {
            public Weapon(WeaponInfo info) { WeaponInfo = info; }
            public WeaponInfo WeaponInfo;
            public float EffectTime = 0f;
        }

        private static Dictionary<int, ProjectileBase> m_customProjectiles = new Dictionary<int, ProjectileBase>();
        private static Dictionary<int, ProjectileBase> m_projectiles = new Dictionary<int, ProjectileBase>();
        private static Dictionary<int, Weapon> m_weapons = new Dictionary<int, Weapon>();
        private static Dictionary<int, PlayerWeapon> m_owners = new Dictionary<int, PlayerWeapon>();

        public static void Initialize()
        {
            Events.UpdateCallback.Start(OnUpdate);
            Events.PlayerWeaponAddedActionCallback.Start(OnPlayerPickedUpWeapon);
            Events.PlayerWeaponRemovedActionCallback.Start(OnPlayerDroppedWeapon);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.PlayerKeyInputCallback.Start(OnPlayerKeyInput);
            Events.ProjectileCreatedCallback.Start(OnProjectileCreated);
            Events.ProjectileHitCallback.Start(OnProjectileHit);
            Events.ObjectTerminatedCallback.Start(OnObjectTerminated);

            //Events.UpdateCallback.Start((e) =>
            //{
            //    ScriptHelper.LogDebug(m_owners.Count, m_projectiles.Count, m_customProjectiles.Count, m_weapons.Count);
            //}, 30, 0);
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

            var removeList = new List<int>();
            foreach (var kv in m_projectiles)
            {
                var projectile = kv.Value;
                projectile.Update(elapsed);
                if (projectile.IsRemoved)
                    removeList.Add(kv.Key); // Projectile.ID is already reset to 0
            }
            // Projectiles dont have OnProjectileTerminated like how IObjects have OnObjectTerminated
            // So when the projectiles go outside of the map and dont hit anything, it will be removed here
            foreach (var r in removeList)
                m_projectiles.Remove(r);

            foreach (var projectile in m_customProjectiles.Values)
            {
                projectile.Update(elapsed);
            }

            foreach (var o in m_owners)
            {
                var playerWpn = o.Value;
                var currentRangeWpn = playerWpn.CurrentRangeWeapon;

                if (currentRangeWpn != null)
                    currentRangeWpn.Update(elapsed);
            }
        }

        private static void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (args.Removed)
            {
                PlayerWeapon playerWpn;

                if (m_owners.TryGetValue(player.UniqueID, out playerWpn))
                {
                    playerWpn.Melee.Remove();
                    playerWpn.Primary.Remove();
                    playerWpn.Secondary.Remove();
                    playerWpn.Throwable.Remove();
                    playerWpn.Powerup.Remove();
                    m_owners.Remove(player.UniqueID);
                }
            }
        }

        private static void OnPlayerKeyInput(IPlayer player, VirtualKeyInfo[] keyInfos)
        {
            PlayerWeapon playerWpn;

            if (m_owners.TryGetValue(player.UniqueID, out playerWpn))
            {
                var currentRangeWpn = playerWpn.CurrentRangeWeapon;
                if (currentRangeWpn != null)
                    currentRangeWpn.OnPlayerKeyInput(keyInfos);
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

        public static PlayerWeapon GetOrCreatePlayerWeapon(IPlayer owner)
        {
            if (owner.UniqueID == 0) return null;

            PlayerWeapon playerWpn;
            if (!m_owners.TryGetValue(owner.UniqueID, out playerWpn))
            {
                playerWpn = PlayerWeapon.Empty(owner);
                m_owners.Add(owner.UniqueID, playerWpn);
            }
            return playerWpn;
        }

        internal static void SetPowerup(IPlayer player, WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            var playerWpn = GetOrCreatePlayerWeapon(player);
            var type = Mapper.GetWeaponItemType(weaponItem);

            switch (type)
            {
                // TODO: Melee powerup
                case WeaponItemType.Melee:
                    break;
                case WeaponItemType.Rifle:
                {
                    if (playerWpn.Primary != null)
                        playerWpn.Primary.Remove();
                    playerWpn.Primary = RangeWeaponFactory.Create(player, weaponItem, powerup);
                    break;
                }
                case WeaponItemType.Handgun:
                {
                    if (playerWpn.Secondary != null)
                        playerWpn.Secondary.Remove();
                    playerWpn.Secondary = RangeWeaponFactory.Create(player, weaponItem, powerup);
                    break;
                }
            }

            player.GiveWeaponItem(weaponItem);
            m_owners[player.UniqueID] = playerWpn;
        }

        private static void OnPlayerDroppedWeapon(IPlayer player, PlayerWeaponRemovedArg arg)
        {
            // ID == 0 means no weapon was dropped. For example: Activating instant powerup will make it disappeared, not dropped
            if (arg.TargetObjectID == 0) return;

            // player argument may be a null object if the weapon drops right after the player was gibbed
            if (player.UniqueID == 0) return;

            // dropped weapons dont not always have IObjectWeaponItem type. For example thrown grenades have IObject type
            var weaponObject = Game.GetObject(arg.TargetObjectID) as IObjectWeaponItem;
            if (weaponObject == null) return;

            var oldPlayerWpn = GetOrCreatePlayerWeapon(player);
            var newWeaponInfo = new WeaponInfo() { Weapon = weaponObject };

            switch (weaponObject.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    newWeaponInfo.MeleePowerup = oldPlayerWpn.Melee.Powerup;
                    m_owners[player.UniqueID].Melee.Remove();
                    break;
                case WeaponItemType.Rifle:
                    newWeaponInfo.RangedPowerup = oldPlayerWpn.Primary.Powerup;
                    m_owners[player.UniqueID].Primary.Remove();
                    break;
                case WeaponItemType.Handgun:
                    newWeaponInfo.RangedPowerup = oldPlayerWpn.Secondary.Powerup;
                    m_owners[player.UniqueID].Secondary.Remove();
                    break;
                case WeaponItemType.Thrown:
                    newWeaponInfo.RangedPowerup = oldPlayerWpn.Throwable.Powerup;
                    m_owners[player.UniqueID].Throwable.Remove();
                    break;
            }

            m_weapons.Add(weaponObject.UniqueID, new Weapon(newWeaponInfo));
        }

        private static void OnPlayerPickedUpWeapon(IPlayer player, PlayerWeaponAddedArg arg)
        {
            if (!m_weapons.ContainsKey(arg.SourceObjectID) || arg.SourceObjectID == 0) return;

            GetOrCreatePlayerWeapon(player);
            var weaponInfo = m_weapons[arg.SourceObjectID].WeaponInfo;
            var createRangedWeapon = new Func<RangeWpn>(
                () => RangeWeaponFactory.Create(player, arg.WeaponItem, weaponInfo.RangedPowerup));

            switch (arg.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    // TODO: create power melee weapon with factory if implement one
                    m_owners[player.UniqueID].Melee.Add(arg.WeaponItem, weaponInfo.MeleePowerup);
                    break;
                case WeaponItemType.Rifle:
                    m_owners[player.UniqueID].Primary = createRangedWeapon();
                    break;
                case WeaponItemType.Handgun:
                    m_owners[player.UniqueID].Secondary = createRangedWeapon();
                    break;
                case WeaponItemType.Thrown:
                    m_owners[player.UniqueID].Throwable = createRangedWeapon();
                    break;
            }

            m_weapons.Remove(arg.SourceObjectID);
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

                var owner = Game.GetPlayer(ownerID);
                var playerWpn = GetOrCreatePlayerWeapon(owner);
                var powerup = RangedWeaponPowerup.None;
                var weaponItem = Mapper.GetWeaponItem(projectile.ProjectileItem);

                if (weaponItem == playerWpn.Primary.Name)
                    powerup = playerWpn.Primary.Powerup;
                if (weaponItem == playerWpn.Secondary.Name)
                    powerup = playerWpn.Secondary.Powerup;

                if (powerup != RangedWeaponPowerup.None)
                {
                    var proj = ProjectileFactory.Create(projectile, powerup);
                    if (proj != null && proj.Powerup != RangedWeaponPowerup.None)
                    {
                        if (proj.IsCustomProjectile)
                            m_customProjectiles.Add(proj.ID, proj);
                        else
                            m_projectiles.Add(proj.ID, proj);
                    }

                    var currentRangeWpn = playerWpn.CurrentRangeWeapon;
                    if (currentRangeWpn != null)
                        playerWpn.CurrentRangeWeapon.OnProjectileCreated(projectile);
                }
            }
        }

        private static void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            if (m_projectiles.ContainsKey(projectile.InstanceID))
            {
                var ownerID = projectile.InitialOwnerPlayerID;

                // Projectile is not fired from IPlayer, custom weapon with custom powerup is not supported
                if (ownerID == 0) return;

                var owner = Game.GetPlayer(ownerID);
                var playerWpn = GetOrCreatePlayerWeapon(owner);
                var proj = m_projectiles[projectile.InstanceID];

                proj.OnProjectileHit(args);

                var currentRangeWpn = playerWpn.CurrentRangeWeapon;
                if (currentRangeWpn != null)
                    playerWpn.CurrentRangeWeapon.OnProjectileHit(projectile, args);

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
                    var proj = m_customProjectiles[obj.UniqueID];

                    proj.OnProjectileHit();
                    m_customProjectiles.Remove(obj.UniqueID);
                }

                var uniqueID = obj.UniqueID;

                // wait until the next frame to remove in case weapon picked-up event fire and remove at the end of the last frame
                ScriptHelper.Timeout(() =>
                {
                    if (m_weapons.ContainsKey(uniqueID))
                    {
                        m_weapons.Remove(uniqueID);
                    }
                }, 0);
            }
        }
    }
}
