using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;
using BotExtended.Factions;
using System;
using BotExtended.Projectiles;

namespace BotExtended.Bots
{
    public class Bot
    {
        private static readonly Bot none = new Bot();
        public static Bot None
        {
            get { return none;  }
        }

        public static Color DialogueColor
        {
            get { return new Color(128, 32, 32); }
        }
        public IPlayer Player { get; set; }
        public BotType Type { get; set; }
        public BotFaction Faction { get; set; }
        public BotInfo Info { get; set; }
        public int UpdateInterval { get; set; }
        public Vector2 Position
        {
            get { return Player.GetWorldPosition(); }
            set { Player.SetWorldPosition(value); }
        }

        // TODO: remove if Gurt add in ScriptAPI. https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3963
        public bool IsThrowableActivated { get; private set; }

        public delegate void PlayerDropWeaponCallback(IPlayer previousOwner, IObjectWeaponItem weaponObj, float totalAmmo);
        public event PlayerDropWeaponCallback PlayerDropWeaponEvent;

        public delegate void PlayerPickUpWeaponCallback(IPlayer newOwner, IObjectWeaponItem weaponObj, float totalAmmo);
        public event PlayerPickUpWeaponCallback PlayerPickUpWeaponEvent;

        public Bot()
        {
            Player = null;
            Type = BotType.None;
            Faction = BotFaction.None;
            Info = new BotInfo();
            UpdateInterval = 100;
            IsThrowableActivated = false;
        }
        public Bot(BotArgs args)
        {
            Player = args.Player;
            Type = args.BotType;
            Faction = args.BotFaction;
            Info = args.Info;
        }
        public Bot(IPlayer player, BotFaction faction = BotFaction.None) : this()
        {
            Player = player;
            Faction = faction;
            Info = new BotInfo(player);
        }

        public void SaySpawnLine()
        {
            if (Info == null) return;

            var spawnLine = Info.SpawnLine;
            var spawnLineChance = Info.SpawnLineChance;

            if (!string.IsNullOrWhiteSpace(spawnLine) && RandomHelper.Between(0f, 1f) < spawnLineChance)
                GameScriptInterface.Game.CreateDialogue(spawnLine, DialogueColor, Player, duration: 3000f);
        }

        public void SayDeathLine()
        {
            if (Info == null) return;

            var deathLine = Info.DeathLine;
            var deathLineChance = Info.DeathLineChance;

            if (!string.IsNullOrWhiteSpace(deathLine) && RandomHelper.Between(0f, 1f) < deathLineChance)
                Game.CreateDialogue(deathLine, DialogueColor, Player, duration: 3000f);
        }

        private List<WeaponItem> m_prevWeapons = new List<WeaponItem>()
        {
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
        };

        private List<float> m_prevAmmo = new List<float>()
        {
            0, // makeshift
            0, // melee 'ammo' is durability of melee weapon
            0, // primary
            0, // secondary
            0, // throwable
            0, // powerup - should always be 0
        };

        private float m_lastUpdateElapsed;
        public void Update(float elapsed)
        {
            m_lastUpdateElapsed += elapsed;

            if (m_lastUpdateElapsed >= UpdateInterval)
            {
                OnUpdate(m_lastUpdateElapsed);
                m_lastUpdateElapsed = 0;
            }
            UpdateWeaponStatus();
        }

        private float m_bloodEffectElapsed = 0;
        protected virtual void OnUpdate(float elapsed)
        {
            if (Info.ZombieStatus == ZombieStatus.Infected && !Player.IsRemoved && !Player.IsBurnedCorpse)
            {
                m_bloodEffectElapsed += elapsed;

                if (m_bloodEffectElapsed > 300)
                {
                    Game.PlayEffect(EffectName.BloodTrail, Position);
                    m_bloodEffectElapsed = 0;
                }
            }
        }

        private int CurrentWeaponIndex
        {
            get
            {
                switch (Player.CurrentWeaponDrawn)
                {
                    case WeaponItemType.Melee:
                        if (Player.CurrentMeleeMakeshiftWeapon.WeaponItem != WeaponItem.NONE)
                            return 0;
                        return 1;
                    case WeaponItemType.Rifle:
                        return 2;
                    case WeaponItemType.Handgun:
                        return 3;
                    case WeaponItemType.Thrown:
                        return 4;
                    case WeaponItemType.Powerup:
                        return 5;
                }
                return -1;
            }
        }
        private WeaponItem CurrentWeapon() { return CurrentWeapon(CurrentWeaponIndex); }
        private float CurrentAmmo() { return CurrentAmmo(CurrentWeaponIndex); }
        private WeaponItem CurrentWeapon(int index)
        {
            switch (index)
            {
                case 0:
                    return Player.CurrentMeleeMakeshiftWeapon.WeaponItem;
                case 1:
                    return Player.CurrentMeleeWeapon.WeaponItem;
                case 2:
                    return Player.CurrentPrimaryWeapon.WeaponItem;
                case 3:
                    return Player.CurrentSecondaryWeapon.WeaponItem;
                case 4:
                    return Player.CurrentThrownItem.WeaponItem;
                case 5:
                    return Player.CurrentPowerupItem.WeaponItem;
            }
            return WeaponItem.NONE;
        }
        private float CurrentAmmo(int index)
        {
            switch (index)
            {
                case 0:
                    return Player.CurrentMeleeMakeshiftWeapon.Durability;
                case 1:
                    return Player.CurrentMeleeWeapon.Durability;
                case 2:
                    return Player.CurrentPrimaryWeapon.TotalAmmo;
                case 3:
                    return Player.CurrentSecondaryWeapon.TotalAmmo;
                case 4:
                    return Player.CurrentThrownItem.CurrentAmmo;
            }
            return 0;
        }

        private bool IsHoldingActivateableThrowable()
        {
            var currentWpn = CurrentWeapon();
            return currentWpn == WeaponItem.GRENADES
                || currentWpn == WeaponItem.C4
                || currentWpn == WeaponItem.MOLOTOVS
                || currentWpn == WeaponItem.MINES;
        }

        private IObjectWeaponItem[] m_nearbyWeapons;
        private void UpdateWeaponStatus()
        {
            m_nearbyWeapons = Game.GetObjectsByArea<IObjectWeaponItem>(Player.GetAABB());
            var eventHasFired = false;

            for (var i = 0; i < m_prevWeapons.Count; i++)
            {
                if (CurrentWeapon(i) != m_prevWeapons[i])
                {
                    if (m_prevWeapons[i] == WeaponItem.NONE && CurrentWeapon(i) != WeaponItem.NONE && !eventHasFired)
                        eventHasFired = CheckFireWeaponEvent(i, WeaponEvent.Pickup);
                    if (m_prevWeapons[i] != WeaponItem.NONE && CurrentWeapon(i) == WeaponItem.NONE && !eventHasFired)
                        eventHasFired = CheckFireWeaponEvent(i, WeaponEvent.Drop);
                    if (m_prevWeapons[i] != WeaponItem.NONE && CurrentWeapon(i) != WeaponItem.NONE && !eventHasFired)
                        eventHasFired = CheckFireWeaponEvent(i, WeaponEvent.Swap);
                    m_prevWeapons[i] = CurrentWeapon(i);
                }
            }

            if (!eventHasFired)
            {
                for (var i = 0; i < m_prevAmmo.Count; i++)
                {
                    if (CurrentAmmo(i) != m_prevAmmo[i])
                    {
                        if (m_prevWeapons[i] == CurrentWeapon(i))
                            CheckFireWeaponEvent(i, WeaponEvent.Refill);
                    }
                    // this can only be updated after calling CheckFireWeaponEvent()
                    m_prevAmmo[i] = CurrentAmmo(i);
                }
            }

            var currentThrowableAmmo = Player.CurrentThrownItem.CurrentAmmo;
            if (IsHoldingActivateableThrowable() && currentThrowableAmmo + 1 == m_lastThrowableAmmo || currentThrowableAmmo == 0)
            {
                // something just had been thrown
                IsThrowableActivated = false;
                m_lastThrowableAmmo = currentThrowableAmmo;
            }
        }

        private enum WeaponEvent
        {
            Pickup,
            Drop,
            Swap,
            Refill,
        }
        private bool CheckFireWeaponEvent(int weaponIndex, WeaponEvent weaponEvent)
        {
            var eventFired = false;
            // TODO: event will not fire if player pick up the same weapon when have max ammo!
            // https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3946
            IObjectWeaponItem droppedWeaponObj = null;
            IObjectWeaponItem pickedupWeaponObj = null;

            // max velocity first OR min diff pos.X first
            Array.Sort(m_nearbyWeapons, (a, b) =>
            {
                if (a.GetLinearVelocity().LengthSquared() > b.GetLinearVelocity().LengthSquared()
                || Math.Abs(a.GetWorldPosition().X - Player.GetWorldPosition().X) < 1
                && Math.Abs(b.GetWorldPosition().X - Player.GetWorldPosition().X) > 1)
                    return -1;

                return 1;
            });

            foreach (var nearbyWeapon in m_nearbyWeapons)
            {
                // Checking if the weapon is already tracked to filter is a necessary workaround because dropped weapon cannot
                // be tracked reliably due to the lack of API. Without this check, if the wrong weapon is added to the weapon
                // pool twice when the player dropped again, it will throw
                if (droppedWeaponObj == null &&
                    nearbyWeapon.WeaponItem == m_prevWeapons[weaponIndex] && !ProjectileManager.IsAlreadyTracked(nearbyWeapon))
                {
                    if (weaponEvent == WeaponEvent.Drop || weaponEvent == WeaponEvent.Swap)
                    {
                        droppedWeaponObj = nearbyWeapon;
                    }
                }
                if (pickedupWeaponObj == null &&
                    nearbyWeapon.WeaponItem == CurrentWeapon(weaponIndex))
                {
                    if (weaponEvent == WeaponEvent.Pickup || weaponEvent == WeaponEvent.Swap || weaponEvent == WeaponEvent.Refill)
                    {
                        pickedupWeaponObj = nearbyWeapon;
                    }
                }
            }

            // defer firing events until now to make sure drop event always fired before pickup event
            if (droppedWeaponObj != null && PlayerDropWeaponEvent != null)
            {
                PlayerDropWeaponEvent.Invoke(Player, droppedWeaponObj, m_prevAmmo[weaponIndex]);
                eventFired = true;
            }

            if (pickedupWeaponObj != null && PlayerPickUpWeaponEvent != null)
            {
                PlayerPickUpWeaponEvent.Invoke(Player, pickedupWeaponObj,
                    ProjectileManager.GetWeaponInfo(pickedupWeaponObj.UniqueID).TotalAmmo);
                eventFired = true;
            }

            return eventFired;
        }

        public virtual void OnSpawn(IEnumerable<Bot> bots)
        {
            SaySpawnLine();
        }
        public virtual void OnMeleeDamage(IPlayer attacker, PlayerMeleeHitArg arg) { }
        public virtual void OnDamage(IPlayer attacker, PlayerDamageArgs args) { }

        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            var player = Game.GetPlayer(projectile.InitialOwnerPlayerID);

            if (player == null) return;

            var bot = BotManager.GetExtendedBot(player) as CowboyBot;

            if (bot != null)
            {
                if (args.IsCrit)
                {
                    var destroyWeapon = RandomHelper.Percentage(bot.DestroyWeaponWhenCritDisarmChance);
                    if (RandomHelper.Percentage(bot.CritDisarmChance))
                        Disarm(projectile.Direction, destroyWeapon);
                }
                else
                {
                    var destroyWeapon = RandomHelper.Percentage(bot.DestroyWeaponWhenDisarmChance);
                    if (RandomHelper.Percentage(bot.DisarmChance))
                        Disarm(projectile.Direction, destroyWeapon);
                }
            }
        }
        public virtual void OnDeath(PlayerDeathArgs args) { }

        private int m_lastThrowableAmmo = 0;
        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos)
        {
            foreach (var keyInfo in keyInfos)
            {
                if (keyInfo.Event == VirtualKeyEvent.Pressed && keyInfo.Key == VirtualKey.ATTACK)
                {
                    if (IsHoldingActivateableThrowable())
                    {
                        IsThrowableActivated = true;
                        m_lastThrowableAmmo = Player.CurrentThrownItem.CurrentAmmo;
                    }
                    break;
                }
            }
        }

        protected IPlayer FindClosestTarget()
        {
            IPlayer target = null;

            foreach (var player in Game.GetPlayers())
            {
                var result = ScriptHelper.IsDifferentTeam(player, Player);
                if (player.IsDead || player.IsRemoved || !ScriptHelper.IsDifferentTeam(player, Player))
                    continue;

                if (target == null) target = player;

                var targetDistance = Vector2.Distance(target.GetWorldPosition(), Position);
                var potentialTargetDistance = Vector2.Distance(player.GetWorldPosition(), Position);

                if (potentialTargetDistance < targetDistance)
                {
                    target = player;
                }
            }

            return target;
        }

        public void Disarm(Vector2 dropDirection, bool destroyWeapon)
        {
            if (Player.CurrentWeaponDrawn == WeaponItemType.Melee
                || Player.CurrentWeaponDrawn == WeaponItemType.Rifle
                || Player.CurrentWeaponDrawn == WeaponItemType.Handgun
                || Player.CurrentWeaponDrawn == WeaponItemType.Thrown && !IsThrowableActivated)
            {
                var weapon = Game.CreateObject(
                    ScriptHelper.ObjectID(CurrentWeapon(), IsThrowableActivated),
                    Player.GetWorldPosition(), 0,
                    Vector2.UnitX * RandomHelper.Between(2, 6) * -Player.FacingDirection +
                    Vector2.UnitY * RandomHelper.Between(1, 7) + dropDirection * 3,
                    RandomHelper.Between(0, MathHelper.TwoPI), Player.FacingDirection);

                if (destroyWeapon)
                    weapon.SetHealth(0);
                Game.PlayEffect(EffectName.CustomFloatText, Position + Vector2.UnitY * 15, "Disarmed");
                Player.RemoveWeaponItemType(Player.CurrentWeaponDrawn);
            }
        }
    }
}
