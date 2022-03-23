using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups
{
    class PowerupManager
    {
        public class PowerupInfo
        {
            public RangedWeaponPowerup RangedPowerup = RangedWeaponPowerup.None;
            public MeleeWeaponPowerup MeleePowerup = MeleeWeaponPowerup.None;
            public bool HasPowerup
            {
                get { return RangedPowerup != RangedWeaponPowerup.None || MeleePowerup != MeleeWeaponPowerup.None; }
            }
        }
        private class WeaponInfo : PowerupInfo
        {
            public WeaponItem Weapon = WeaponItem.NONE;
        }
        private class WeaponObjectInfo : PowerupInfo
        {
            public IObjectWeaponItem Weapon = null;
        }

        private class Weapon
        {
            public Weapon(WeaponObjectInfo info) { WeaponInfo = info; }
            public WeaponObjectInfo WeaponInfo;
            public float EffectTime = 0f;
        }

        private static Dictionary<int, List<WeaponInfo>> m_queuedPowerups = new Dictionary<int, List<WeaponInfo>>();
        private static Dictionary<int, CustomProjectile> m_customProjectiles = new Dictionary<int, CustomProjectile>();
        private static Dictionary<int, ProjectileBase> m_projectiles = new Dictionary<int, ProjectileBase>();
        private static Dictionary<int, Weapon> m_weapons = new Dictionary<int, Weapon>(); // weapons laying on the ground
        private static Dictionary<int, PlayerWeapon> m_owners = new Dictionary<int, PlayerWeapon>(); // equipped weapons

        public static void Initialize()
        {
            Events.UpdateCallback.Start(OnUpdate);
            Events.PlayerWeaponAddedActionCallback.Start(OnPlayerPickedUpWeapon);
            Events.PlayerWeaponRemovedActionCallback.Start(OnPlayerDroppedWeapon);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.PlayerKeyInputCallback.Start(OnPlayerKeyInput);
            Events.ProjectileCreatedCallback.Start(OnProjectileCreated);
            Events.ProjectileHitCallback.Start(OnProjectileHit);
            Events.PlayerMeleeActionCallback.Start(OnMeleeAction);
            Events.ObjectTerminatedCallback.Start(OnObjectTerminated);

            //Events.UpdateCallback.Start((e) =>
            //{
            //    ScriptHelper.LogDebug(
            //        m_owners.Count,
            //        m_queuedPowerups.Count,
            //        m_projectiles.Count,
            //        m_customProjectiles.Count,
            //        m_weapons.Count);
            //}, 30, 0);
        }

        private static float m_lastUpdateTime = 0f;
        private static void OnUpdate(float _)
        {
            var elapsed = Game.TotalElapsedGameTime - m_lastUpdateTime;

            foreach (var item in m_weapons)
            {
                var weapon = item.Value;
                if (weapon.WeaponInfo.HasPowerup)
                    PlayMoreShinyEffect(weapon, elapsed);
            }

            foreach (var projectileID in m_projectiles.Keys.ToList())
            {
                var projectile = m_projectiles[projectileID];
                projectile.OnUpdate(elapsed);
                if (projectile.IsRemoved)
                {
                    projectile.OnRemove();
                    m_projectiles.Remove(projectileID); // Projectile.ID was already reset to 0 at this point
                }
            }

            foreach (var projectileID in m_customProjectiles.Keys.ToList())
            {
                var projectile = m_customProjectiles[projectileID];
                projectile.OnUpdate(elapsed);
                if (projectile.IsRemoved)
                {
                    projectile.OnRemove();
                    m_customProjectiles.Remove(projectileID);
                }
            }

            // need to create a new list before iterating since Update() can trigger a kill which leads to the collection being modified
            foreach (var o in m_owners.ToList())
            {
                var playerWpn = o.Value;
                var currentRangeWpn = playerWpn.CurrentRangeWeapon;

                if (currentRangeWpn != null)
                    currentRangeWpn.Update(elapsed);

                var currentMeleeWpn = GetMeleeWpn(Game.GetPlayer(o.Key));

                if (currentMeleeWpn != null)
                    currentMeleeWpn.Update(elapsed);
            }

            m_lastUpdateTime = Game.TotalElapsedGameTime;
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

                if (m_queuedPowerups.ContainsKey(player.UniqueID))
                {
                    m_queuedPowerups.Remove(player.UniqueID);
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

        public static PowerupInfo GetPowerupInfo(int weaponObjectID)
        {
            Weapon weapon;

            if (m_weapons.TryGetValue(weaponObjectID, out weapon))
            {
                return weapon.WeaponInfo;
            }
            return null;
        }

        private static WeaponInfo CreateWeaponInfo(WeaponItem weaponItem)
        {
            return new WeaponInfo { Weapon = weaponItem };
        }
        private static WeaponInfo CreateWeaponInfo(WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            return new WeaponInfo { Weapon = weaponItem, RangedPowerup = powerup };
        }
        private static WeaponInfo CreateWeaponInfo(WeaponItem weaponItem, MeleeWeaponPowerup powerup)
        {
            return new WeaponInfo { Weapon = weaponItem, MeleePowerup = powerup };
        }

        internal static void SetPowerup(IPlayer player, WeaponItem weaponItem, MeleeWeaponPowerup powerup)
        {
            if (!m_queuedPowerups.ContainsKey(player.UniqueID))
                m_queuedPowerups[player.UniqueID] = new List<WeaponInfo>();

            // Barehand: OnPlayerPickupWeapon is never invoked in this case so we need to assign hand weapon here.
            if (weaponItem == WeaponItem.NONE)
            {
                GetOrCreatePlayerWeapon(player).MeleeHand = PowerupWeaponFactory.Create(player, weaponItem, powerup);
                return;
            }
            m_queuedPowerups[player.UniqueID].Add(CreateWeaponInfo(weaponItem, powerup));
            player.GiveWeaponItem(weaponItem);
        }

        internal static void SetPowerup(IPlayer player, WeaponItem weaponItem, RangedWeaponPowerup powerup)
        {
            if (!m_queuedPowerups.ContainsKey(player.UniqueID))
                m_queuedPowerups[player.UniqueID] = new List<WeaponInfo>();

            m_queuedPowerups[player.UniqueID].Add(CreateWeaponInfo(weaponItem, powerup));
            player.GiveWeaponItem(weaponItem);
        }

        internal static IObjectWeaponItem CreateWeapon(string objectID, MeleeWeaponPowerup powerup)
        {
            var weaponObject = (IObjectWeaponItem)Game.CreateObject(objectID);
            var newWeaponInfo = new WeaponObjectInfo()
            {
                Weapon = weaponObject,
                MeleePowerup = powerup,
            };
            m_weapons.Add(weaponObject.UniqueID, new Weapon(newWeaponInfo));
            return weaponObject;
        }

        internal static IObjectWeaponItem CreateWeapon(string objectID, RangedWeaponPowerup powerup)
        {
            var weaponObject = (IObjectWeaponItem)Game.CreateObject(objectID);
            var newWeaponInfo = new WeaponObjectInfo()
            {
                Weapon = weaponObject,
                RangedPowerup = powerup,
            };
            m_weapons.Add(weaponObject.UniqueID, new Weapon(newWeaponInfo));
            return weaponObject;
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
            var newWeaponInfo = new WeaponObjectInfo() { Weapon = weaponObject };

            switch (weaponObject.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    if (Mapper.IsMakeshiftWeapon(weaponObject.WeaponItem))
                    {
                        newWeaponInfo.MeleePowerup = oldPlayerWpn.MeleeMakeshift.Powerup;
                        m_owners[player.UniqueID].MeleeMakeshift.Remove();
                        m_owners[player.UniqueID].MeleeMakeshift = PowerupWeaponFactory.Create(player, WeaponItem.NONE, MeleeWeaponPowerup.None);
                        break;
                    }
                    newWeaponInfo.MeleePowerup = oldPlayerWpn.Melee.Powerup;
                    m_owners[player.UniqueID].Melee.Remove();
                    m_owners[player.UniqueID].Melee = PowerupWeaponFactory.Create(player, WeaponItem.NONE, MeleeWeaponPowerup.None);
                    break;
                case WeaponItemType.Rifle:
                    newWeaponInfo.RangedPowerup = oldPlayerWpn.Primary.Powerup;
                    m_owners[player.UniqueID].Primary.Remove();
                    m_owners[player.UniqueID].Primary = PowerupWeaponFactory.Create(player, WeaponItem.NONE, RangedWeaponPowerup.None);
                    break;
                case WeaponItemType.Handgun:
                    newWeaponInfo.RangedPowerup = oldPlayerWpn.Secondary.Powerup;
                    m_owners[player.UniqueID].Secondary.Remove();
                    m_owners[player.UniqueID].Secondary = PowerupWeaponFactory.Create(player, WeaponItem.NONE, RangedWeaponPowerup.None);
                    break;
                case WeaponItemType.Thrown:
                    newWeaponInfo.RangedPowerup = oldPlayerWpn.Throwable.Powerup;
                    m_owners[player.UniqueID].Throwable.Remove();
                    // TODO: create null object for thrown weapon
                    m_owners[player.UniqueID].Throwable = PowerupWeaponFactory.Create(player, WeaponItem.NONE, RangedWeaponPowerup.None);
                    break;
            }

            m_weapons.Add(weaponObject.UniqueID, new Weapon(newWeaponInfo));
        }

        private static void OnPlayerPickedUpWeapon(IPlayer player, PlayerWeaponAddedArg arg)
        {
            if (!m_weapons.ContainsKey(arg.SourceObjectID) && !m_queuedPowerups.ContainsKey(player.UniqueID))
                return;

            GetOrCreatePlayerWeapon(player);

            // TODO: gibbed player doesn't fire OnPlayerDropped, so calling m_weapons.GetItem() will throw
            // wait for gurt to fix and remove this line:
            // https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&t=3999&p=23441#p23441
            if (arg.SourceObjectID != 0 && !m_weapons.ContainsKey(arg.SourceObjectID))
                return;

            var weaponInfo = (PowerupInfo)null;

            // get weapon via commands
            if (arg.SourceObjectID == 0)
            {
                // get weapon via BE command or from LocalStorage (e.g. /be sw near m60 minigun)
                weaponInfo = m_queuedPowerups[player.UniqueID].Where(wi => wi.Weapon == arg.WeaponItem).FirstOrDefault();
                // get weapon via vanilla command (e.g. /give 0 m60)
                //if (weaponInfo == null)
                //    weaponInfo = CreateWeaponInfo(arg.WeaponItem);
            }
            else
                weaponInfo = m_weapons[arg.SourceObjectID].WeaponInfo;

            if (weaponInfo == null) return;

            var createRangedWeapon = new Func<RangeWpn>(
                () => PowerupWeaponFactory.Create(player, arg.WeaponItem, weaponInfo.RangedPowerup));
            var createMeleeWeapon = new Func<MeleeWpn>(
                () => PowerupWeaponFactory.Create(player, arg.WeaponItem, weaponInfo.MeleePowerup));

            switch (arg.WeaponItemType)
            {
                case WeaponItemType.Melee:
                    if (Mapper.IsMakeshiftWeapon(arg.WeaponItem))
                    {
                        m_owners[player.UniqueID].MeleeMakeshift = createMeleeWeapon();
                        break;
                    }
                    m_owners[player.UniqueID].Melee = createMeleeWeapon();
                    break;
                case WeaponItemType.Rifle:
                    m_owners[player.UniqueID].Primary = createRangedWeapon();
                    break;
                case WeaponItemType.Handgun:
                    m_owners[player.UniqueID].Secondary = createRangedWeapon();
                    break;
                // TODO: create thrown weapon with factory if implement one
                case WeaponItemType.Thrown:
                    m_owners[player.UniqueID].Throwable = createRangedWeapon();
                    break;
            }

            if (arg.SourceObjectID == 0)
                m_queuedPowerups[player.UniqueID].Remove((WeaponInfo)weaponInfo);
            else
                m_weapons.Remove(arg.SourceObjectID);
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
                            m_customProjectiles.Add(proj.ID, (CustomProjectile)proj);
                        else
                            m_projectiles.Add(proj.ID, proj);
                    }

                    var currentRangeWpn = playerWpn.CurrentRangeWeapon;
                    if (currentRangeWpn != null)
                        playerWpn.CurrentRangeWeapon.OnProjectileCreated(projectile);
                }
            }
        }

        public static void UpdateProjectile(IProjectile oldP, IProjectile newP)
        {
            if (!m_projectiles.ContainsKey(oldP.InstanceID)) return;
            var oldProjectile = m_projectiles[oldP.InstanceID];
            m_projectiles.Remove(oldP.InstanceID);
            m_projectiles.Add(newP.InstanceID, oldProjectile);
        }

        private static void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            if (m_projectiles.ContainsKey(projectile.InstanceID))
            {
                var proj = m_projectiles[projectile.InstanceID];
                var ownerID = proj.InitialOwnerPlayerID;

                // Projectile is not fired from IPlayer, custom weapon with custom powerup is not supported
                if (ownerID == 0) return;

                proj.OnProjectileHit(args);

                // TODO: powerup is not activated if the player is dead
                PlayerWeapon playerWpn;
                if (m_owners.TryGetValue(ownerID, out playerWpn))
                {
                    var currentRangeWpn = playerWpn.CurrentRangeWeapon;
                    if (currentRangeWpn != null)
                    {
                        currentRangeWpn.OnProjectileHit(projectile, args);
                    }
                }

                // NOTE: the reason I dont remove projectile when RemoveFlag = true is because some projectiles
                // like Spinner have longer lifecycle than the original projectile itself
            }
        }

        private static MeleeWpn GetMeleeWpn(IPlayer player)
        {
            if (player == null) return null;

            var playerWpn = GetOrCreatePlayerWeapon(player);
            var weaponItem = BotManager.GetBot(player).CurrentWeapon;

            // barehand powerup is always available and is only overridden when another melee weapon has powerup
            if (weaponItem == playerWpn.Melee.Name && weaponItem != WeaponItem.NONE && playerWpn.Melee.Powerup != MeleeWeaponPowerup.None)
            {
                return playerWpn.Melee;
            }
            return playerWpn.MeleeHand;
        }

        private static void OnMeleeAction(IPlayer owner, PlayerMeleeHitArg[] args)
        {
            var meleeWpn = GetMeleeWpn(owner);

            if (meleeWpn == null || meleeWpn.Powerup == MeleeWeaponPowerup.None) return;

            // OnMeleeAction is invoked a bit early, before MeleeAction is updated
            // https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&p=24824&sid=80ecb190dfe9c7febc1f3ede990a83c6#p24824
            ScriptHelper.Timeout(() =>
            {
                foreach (var arg in args) meleeWpn.OnMeleeAction(args);
            }, 0);
        }

        private static void OnObjectTerminated(IObject[] objs)
        {
            foreach (var obj in objs)
            {
                if (m_customProjectiles.ContainsKey(obj.UniqueID))
                {
                    var proj = m_customProjectiles[obj.UniqueID];
                    proj.OnProjectileTerminated();
                }

                var uniqueID = obj.UniqueID;

                // wait and see if the picked-up callback fires and removes first. this callback should only remove m_weapons here
                // if it's despawned
                // because picked-up callback is fired at the end of the update, removing the object now will lead to null
                // exception later on in the picked-up callback
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
