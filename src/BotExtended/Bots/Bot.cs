using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;
using BotExtended.Factions;
using System;
using BotExtended.Projectiles;
using System.Linq;

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

        // TODO: remove if Gurt add this in ScriptAPI. https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3963
        public bool IsThrowableActivated { get; private set; }

        public delegate void PlayerDropWeaponCallback(IPlayer previousOwner, IObjectWeaponItem weaponObj, float totalAmmo);
        public event PlayerDropWeaponCallback PlayerDropWeaponEvent;

        public delegate void PlayerPickUpWeaponCallback(IPlayer newOwner, IObjectWeaponItem weaponObj, float totalAmmo);
        public event PlayerPickUpWeaponCallback PlayerPickUpWeaponEvent;

        private Bot_GravityGunAI m_botGravityGunAI;

        private Bot()
        {
            m_botGravityGunAI = new Bot_GravityGunAI(this);
            PlayerDropWeaponEvent += m_botGravityGunAI.OnPlayerDropWeapon;
        }
        public Bot(IPlayer player = null, BotFaction faction = BotFaction.None) : this()
        {
            Player = player;
            Type = BotType.None;
            Faction = faction;
            Info = new BotInfo(player);
            UpdateInterval = 100;
            IsThrowableActivated = false;
        }
        public Bot(BotArgs args) : this()
        {
            Player = args.Player;
            Type = args.BotType;
            Faction = args.BotFaction;
            Info = args.Info;
        }

        public void SaySpawnLine()
        {
            if (Info == null) return;

            var spawnLine = Info.SpawnLine;
            var spawnLineChance = Info.SpawnLineChance;

            if (!string.IsNullOrWhiteSpace(spawnLine) && RandomHelper.Percentage(spawnLineChance))
                Game.CreateDialogue(spawnLine, DialogueColor, Player, duration: 3000f);
        }

        public void SayDeathLine()
        {
            if (Info == null) return;

            var deathLine = Info.DeathLine;
            var deathLineChance = Info.DeathLineChance;

            if (!string.IsNullOrWhiteSpace(deathLine) && RandomHelper.Percentage(deathLineChance))
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
            UpdateCustomWeaponAI(elapsed);
        }

        public void LogDebug(params object[] messages)
        {
            if (Game.IsEditorTest)
            {
                var ph = ScriptHelper.GetDefaultPlaceholder(messages);
                Game.DrawText(string.Format(ph, messages), Position);
            }
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
        public WeaponItem CurrentWeapon
        {
            get { return GetCurrentWeapon(CurrentWeaponIndex); }
        }
        public float CurrentAmmo
        {
            get { return GetCurrentAmmo(CurrentWeaponIndex); }
            set { SetCurrentAmmo(CurrentWeaponIndex, value); }
        }

        private WeaponItem GetCurrentWeapon(int index)
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
        public float GetCurrentAmmo(int index)
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
        public void SetCurrentAmmo(int currentWeaponIndex, float value)
        {
            switch (currentWeaponIndex)
            {
                case 0:
                    Player.SetCurrentMeleeMakeshiftDurability(value);
                    break;
                case 1:
                    Player.SetCurrentMeleeDurability(value);
                    break;
                case 2:
                    Player.SetCurrentPrimaryWeaponAmmo((int)value);
                    break;
                case 3:
                    Player.SetCurrentSecondaryWeaponAmmo((int)value);
                    break;
                case 4:
                    Player.SetCurrentThrownItemAmmo((int)value);
                    break;
            }
        }

        private bool IsHoldingActivateableThrowable()
        {
            var currentWpn = CurrentWeapon;
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
                if (GetCurrentWeapon(i) != m_prevWeapons[i])
                {
                    // NOTE: multiple weapons can be dropped in 1 frame if the player dies
                    if (m_prevWeapons[i] == WeaponItem.NONE && GetCurrentWeapon(i) != WeaponItem.NONE)
                        eventHasFired = CheckFireWeaponEvent(i, WeaponEvent.Pickup);
                    if (m_prevWeapons[i] != WeaponItem.NONE && GetCurrentWeapon(i) == WeaponItem.NONE)
                        eventHasFired = CheckFireWeaponEvent(i, WeaponEvent.Drop);
                    if (m_prevWeapons[i] != WeaponItem.NONE && GetCurrentWeapon(i) != WeaponItem.NONE)
                        eventHasFired = CheckFireWeaponEvent(i, WeaponEvent.Swap);
                    m_prevWeapons[i] = GetCurrentWeapon(i);
                }
            }

            if (!eventHasFired)
            {
                for (var i = 0; i < m_prevAmmo.Count; i++)
                {
                    if (GetCurrentAmmo(i) != m_prevAmmo[i])
                    {
                        if (m_prevWeapons[i] == GetCurrentWeapon(i))
                            CheckFireWeaponEvent(i, WeaponEvent.Refill);
                    }
                    // this can only be updated after calling CheckFireWeaponEvent()
                    m_prevAmmo[i] = GetCurrentAmmo(i);
                }
            }

            // TODO: cannot diff ammo -> returns wrong result when using ia 1
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
                // pool twice when the player dropped again, it will throw somewhere else
                if (droppedWeaponObj == null &&
                    nearbyWeapon.WeaponItem == m_prevWeapons[weaponIndex] && !ProjectileManager.IsAlreadyTracked(nearbyWeapon))
                {
                    if (weaponEvent == WeaponEvent.Drop || weaponEvent == WeaponEvent.Swap)
                    {
                        droppedWeaponObj = nearbyWeapon;
                    }
                }
                if (pickedupWeaponObj == null &&
                    nearbyWeapon.WeaponItem == GetCurrentWeapon(weaponIndex))
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

        private void UpdateCustomWeaponAI(float elapsed)
        {
            if (Player.IsUser) return;

            foreach (var nearbyWeapon in m_nearbyWeapons)
            {
                Game.DrawArea(nearbyWeapon.GetAABB(), Color.Grey);
                if (Info.SpecificSearchItems.Contains(nearbyWeapon.WeaponItem)
                    && !Player.IsStaggering && !Player.IsStunned && Player.IsOnGround)
                {
                    Player.SetInputEnabled(false);
                    Player.AddCommand(new PlayerCommand(PlayerCommandType.Activate));
                    ScriptHelper.Timeout(() =>
                    {
                        Player.SetInputEnabled(true);
                    }, 1);
                    break;
                }
            }

            var botBehaviorSet = Player.GetBotBehaviorSet();

            if (botBehaviorSet.RangedWeaponMode != BotBehaviorRangedWeaponMode.HipFire)
            {
                var playerWeapon = ProjectileManager.GetOrCreatePlayerWeapon(Player);
                GravityGun gun = null;

                if (playerWeapon.Primary.Powerup == RangedWeaponPowerup.Gravity)
                    gun = (GravityGun)playerWeapon.Primary;
                if (playerWeapon.Secondary.Powerup == RangedWeaponPowerup.Gravity)
                    gun = (GravityGun)playerWeapon.Secondary;

                if (gun != null)
                {
                    m_botGravityGunAI.Update(elapsed, gun);
                }
            }
        }

        public virtual void OnSpawn(IEnumerable<Bot> bots)
        {
            SaySpawnLine();
        }
        public virtual void OnMeleeDamage(IPlayer attacker, PlayerMeleeHitArg arg) { }
        public virtual void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            UpdateInfectedStatus(attacker, args);
        }

        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            var player = Game.GetPlayer(projectile.InitialOwnerPlayerID);

            if (player == null) return;

            var bot = BotManager.GetBot(player) as CowboyBot;

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
        public virtual void OnDeath(PlayerDeathArgs args)
        {
            if (args.Killed)
            {
                SayDeathLine();
                PlayerDropWeaponEvent -= m_botGravityGunAI.OnPlayerDropWeapon;
            }

            if (args.Removed)
            {
                m_nearbyWeapons = Game.GetObjectsByArea<IObjectWeaponItem>(Player.GetAABB());

                for (var i = 0; i < m_prevWeapons.Count; i++)
                {
                    if (GetCurrentWeapon(i) != m_prevWeapons[i])
                    {
                        // NOTE: multiple weapons can be dropped in 1 frame if the player dies
                        if (m_prevWeapons[i] != WeaponItem.NONE && GetCurrentWeapon(i) == WeaponItem.NONE)
                            CheckFireWeaponEvent(i, WeaponEvent.Drop);
                        m_prevWeapons[i] = GetCurrentWeapon(i);
                    }
                }
            }
        }

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

        public bool CanInfect { get { return Info.ZombieStatus != ZombieStatus.Human; } }
        public bool IsInfectedByZombie { get { return Info.ZombieStatus == ZombieStatus.Infected; } }
        private void UpdateInfectedStatus(IPlayer attacker, PlayerDamageArgs args)
        {
            if (!CanInfect && !Player.IsBurnedCorpse && attacker != null)
            {
                var directContact = args.DamageType == PlayerDamageEventType.Melee
                    && attacker.CurrentWeaponDrawn == WeaponItemType.NONE
                    && !attacker.IsKicking && !attacker.IsJumpKicking;
                var attackerBot = BotManager.GetBot(attacker);

                if (attackerBot.CanInfect && directContact)
                {
                    if (!Info.ImmuneToInfect)
                    {
                        Game.PlayEffect(EffectName.CustomFloatText, Position, "infected");
                        Game.ShowChatMessage(attacker.Name + " infected " + Player.Name);
                        Info.ZombieStatus = ZombieStatus.Infected;
                    }
                }
            }
        }

        public void Disarm(Vector2 dropDirection, bool destroyWeapon = false)
        {
            if (Player.CurrentWeaponDrawn == WeaponItemType.Melee
                || Player.CurrentWeaponDrawn == WeaponItemType.Rifle
                || Player.CurrentWeaponDrawn == WeaponItemType.Handgun
                || Player.CurrentWeaponDrawn == WeaponItemType.Thrown && !IsThrowableActivated)
            {
                var weapon = Game.CreateObject(
                    Mapper.ObjectID(CurrentWeapon, IsThrowableActivated),
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

        public bool IsStunned { get; private set; }
        private Events.UpdateCallback m_effect;
        public void Stun(uint stunnedTime, Action<float> effect, uint effectTime = 0)
        {
            IsStunned = true;
            Player.SetInputEnabled(false);
            Player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));

            m_effect = Events.UpdateCallback.Start(effect, effectTime);

            ScriptHelper.Timeout(() =>
            {
                Player.AddCommand(new PlayerCommand(PlayerCommandType.StopDeathKneel));
                Player.SetInputEnabled(true);
                IsStunned = false;
                Events.UpdateCallback.Stop(m_effect);
                m_effect = null;
            }, stunnedTime);
        }
    }
}
