using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using BotExtended.Factions;
using System;
using BotExtended.Powerups;
using System.Linq;
using static BotExtended.Library.SFD;
using BotExtended.Powerups.RangeWeapons;

namespace BotExtended.Bots
{
    public enum MeleeAction { None, One, Two, Three }

    public class Bot
    {
        public static readonly Bot None;
        public static readonly string NoneCustomID = "BeNonePlayer";
        static Bot()
        {
            var nonePlayer = Game.CreatePlayer(Vector2.Zero);
            nonePlayer.Remove();
            nonePlayer.CustomID = NoneCustomID;
            None = new Bot(nonePlayer);
        }

        public Color DialogueColor
        {
            get { return ScriptHelper.GetTeamColor(Player == null ? PlayerTeam.Independent: Player.GetTeam()); }
        }
        public BotBehaviorSet BotBehaviorSet { get; private set; }
        public IPlayer Player { get; set; }
        public BotType Type { get; set; }
        public BotFaction Faction { get; set; }
        public BotInfo Info { get; set; }
        public int UpdateDelay { get; set; }
        // IsRemoved from BotManager database, NOT from SFD Gameworld
        // You should use Bot.IsRemoved and Bot.Player.IsDead together because hte IPlayer in Bot.Player can be swapped via command.
        // Bot.IsRemoved doesn't necessarily mean Bos.Player.IsDead
        public bool IsRemoved { get; set; }
        public Vector2 Position
        {
            get { return Player.GetWorldPosition(); }
            set { Player.SetWorldPosition(value); }
        }

        private Bot(IPlayer player = null)
        {
            Player = player;
            InfectTeam = player != null ? player.GetTeam() : BotManager.BotTeam;
            UpdateDelay = 0;
            BotBehaviorSet = player != null ? player.GetBotBehaviorSet() : null;
            Info = new BotInfo(player);
        }
        public Bot(IPlayer player, BotType type, BotFaction faction) : this(player)
        {
            Type = type;
            Faction = faction;
            UpdateDelay = 100;
        }
        public Bot(BotArgs args) : this(args.Player)
        {
            Type = args.BotType;
            Faction = args.BotFaction;
            Info = args.Info;
        }

        private void SaySpawnLine()
        {
            if (Info == null) return;

            var spawnLine = Info.SpawnLine;
            var spawnLineChance = Info.SpawnLineChance;

            if (!string.IsNullOrWhiteSpace(spawnLine) && RandomHelper.Percentage(spawnLineChance))
                SayLine(spawnLine);
        }

        public void SayDeathLine()
        {
            if (Info == null) return;

            var deathLine = Info.DeathLine;
            var deathLineChance = Info.DeathLineChance;

            if (!string.IsNullOrWhiteSpace(deathLine) && RandomHelper.Percentage(deathLineChance))
                SayLine(deathLine);
        }

        private float m_lastUpdateElapsed = 0f;
        public void Update(float elapsed)
        {
            m_lastUpdateElapsed += elapsed;

            if (m_lastUpdateElapsed >= UpdateDelay)
            {
                OnUpdate(m_lastUpdateElapsed);
                m_lastUpdateElapsed = 0;
            }
            UpdateInfectedEffect(elapsed);
            UpdateMeleeAttackPhrases();

            if (IsStunned && !Player.IsDeathKneeling)
                Player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));
        }

        public void LogDebug(params object[] messages)
        {
            if (Game.IsEditorTest)
            {
                Game.DrawText(ScriptHelper.ToDisplayString(messages), Position);
            }
        }

        public void SayLine(string line, Color? color = null, float duration = 0)
        {
            var dialogueColor = color == null ? DialogueColor : (Color)color;
            // reading speed: 3 words per second
            duration = duration > 0 ? duration : Math.Max(line.Split(' ').Length * 1000 / 3, 1000);
            if (Game.TotalElapsedGameTime < 1000 && Game.StartupSequenceEnabled)
                duration += 1000;
            Game.CreateDialogue(line, dialogueColor, Player, duration: duration);
        }

        protected virtual void OnUpdate(float elapsed) { }

        public virtual void OnPickedupWeapon(PlayerWeaponAddedArg arg) { }
        public virtual void OnDroppedWeapon(PlayerWeaponRemovedArg arg) { }

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
        }
        public float CurrentTotalAmmo
        {
            get { return GetCurrentTotalAmmo(CurrentWeaponIndex); }
            set { SetCurrentTotalAmmo(CurrentWeaponIndex, value); }
        }

        // TODO: remove
        public WeaponItem CurrentMeleeWeapon
        {
            get { return Player.CurrentMeleeMakeshiftWeapon.WeaponItem != WeaponItem.NONE ?
                    Player.CurrentMeleeMakeshiftWeapon.WeaponItem : Player.CurrentMeleeWeapon.WeaponItem; }
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
                    return Player.CurrentPrimaryWeapon.CurrentAmmo;
                case 3:
                    return Player.CurrentSecondaryWeapon.CurrentAmmo;
                case 4:
                    return Player.CurrentThrownItem.CurrentAmmo;
            }
            return 0;
        }
        public float GetCurrentTotalAmmo(int index)
        {
            switch (index)
            {
                case 0:
                    return Player.CurrentMeleeMakeshiftWeapon.MaxValue;
                case 1:
                    return Player.CurrentMeleeWeapon.MaxValue;
                case 2:
                    return Player.CurrentPrimaryWeapon.TotalAmmo;
                case 3:
                    return Player.CurrentSecondaryWeapon.TotalAmmo;
                case 4:
                    return Player.CurrentThrownItem.CurrentAmmo;
            }
            return 0;
        }
        public void SetCurrentTotalAmmo(int currentWeaponIndex, float value)
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

        public void Decorate(IPlayer existingPlayer)
        {
            existingPlayer.SetProfile(GetProfile());

            existingPlayer.GiveWeaponItem(Player.CurrentMeleeWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentMeleeMakeshiftWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentPrimaryWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentSecondaryWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentThrownItem.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentPowerupItem.WeaponItem);

            existingPlayer.SetTeam(Player.GetTeam());
            existingPlayer.SetModifiers(Player.GetModifiers());
            existingPlayer.SetHitEffect(Player.GetHitEffect());
        }

        public static IPlayer Clone(IPlayer player)
        {
            var clone = Game.CreatePlayer(player.GetWorldPosition());

            clone.SetProfile(player.GetProfile());

            clone.GiveWeaponItem(player.CurrentMeleeWeapon.WeaponItem);
            clone.GiveWeaponItem(player.CurrentMeleeMakeshiftWeapon.WeaponItem);
            clone.GiveWeaponItem(player.CurrentPrimaryWeapon.WeaponItem);
            clone.GiveWeaponItem(player.CurrentSecondaryWeapon.WeaponItem);
            clone.GiveWeaponItem(player.CurrentThrownItem.WeaponItem);
            clone.GiveWeaponItem(player.CurrentPowerupItem.WeaponItem);

            clone.SetTeam(player.GetTeam());
            clone.SetModifiers(player.GetModifiers());
            clone.SetHitEffect(player.GetHitEffect());
            clone.SetBotBehaviorSet(player.GetBotBehaviorSet());
            clone.SetBotName(player.Name);

            return clone;
        }

        // null safety getter. IPlayer.GetProfile() returns null if the Player is removed
        public IProfile GetProfile() { return Player.GetProfile() ?? ScriptHelper.GetEmptyProfile(); }

        private float m_bloodEffectElapsed = 0;
        private void UpdateInfectedEffect(float elapsed)
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

        private bool _lastIdle = false;
        private int _lastSwing = 0;
        public MeleeAction CurrentMeleeAction { get; private set; }
        private void UpdateMeleeAttackPhrases()
        {
            if (Player.IsDead) return;

            if (Player.Statistics.TotalMeleeAttackSwings != _lastSwing)
            {
                if (CurrentMeleeAction == MeleeAction.None) CurrentMeleeAction = MeleeAction.One;
                else if (CurrentMeleeAction == MeleeAction.One) CurrentMeleeAction = MeleeAction.Two;
                else if (CurrentMeleeAction == MeleeAction.Two) CurrentMeleeAction = MeleeAction.Three;
                else if (CurrentMeleeAction == MeleeAction.Three) CurrentMeleeAction = MeleeAction.One;
            }
            if (Player.IsIdle && !_lastIdle)
                CurrentMeleeAction = MeleeAction.None;

            _lastIdle = Player.IsIdle;
            _lastSwing = Player.Statistics.TotalMeleeAttackSwings;
        }

        protected Area DangerArea
        {
            get { return new Area(Position + new Vector2(-30, -5), Position + new Vector2(30, 18)); }
        }
        public bool AreEnemiesNearby()
        {
            return BotManager.GetBots().Any(x => !x.Player.IsDead && !ScriptHelper.SameTeam(Player, x.Player) && DangerArea.Intersects(x.Player.GetAABB()));
        }
        public bool IsHurtRecently { get { return !ScriptHelper.IsElapsed(_lastDamageTime, 2500); } }
        public bool IsVulnerable()
        {
            return IsHurtRecently && AreEnemiesNearby();
        }

        public virtual void OnSpawn() { SaySpawnLine(); }
        public virtual void OnDealDamage(IPlayer victim, PlayerDamageArgs arg) { }
        public virtual void OnMeleeDamage(IPlayer attacker, PlayerMeleeHitArg arg) { }
        public virtual void OnMeleeAction(PlayerMeleeHitArg[] args) { }

        private float _lastDamageTime = 0f;
        public virtual void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            UpdateInfectedStatus(attacker, args);
            _lastDamageTime = Game.TotalElapsedGameTime;
        }

        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            var player = Game.GetPlayer(projectile.InitialOwnerPlayerID);

            if (player == null) return;

            var bot = BotManager.GetBot(player);
            var cowboyBot = bot as CowboyBot;

            // TODO: not apply if the bot is dead before this event runs
            if (cowboyBot != null)
            {
                if (args.IsCrit)
                {
                    var destroyWeapon = RandomHelper.Percentage(cowboyBot.DestroyWeaponWhenCritDisarmChance);
                    if (RandomHelper.Percentage(cowboyBot.CritDisarmChance))
                        Disarm(projectile.Direction, destroyWeapon: destroyWeapon);
                }
                else
                {
                    var destroyWeapon = RandomHelper.Percentage(cowboyBot.DestroyWeaponWhenDisarmChance);
                    if (RandomHelper.Percentage(cowboyBot.DisarmChance))
                        Disarm(projectile.Direction, destroyWeapon: destroyWeapon);
                }
            }
            if (bot.Type == BotType.Hunter && !Player.IsRemoved && ScriptHelper.IsBear(Player))
            {
                Player.DealDamage(projectile.GetProperties().PlayerDamage);
            }
        }

        public static Dictionary<PlayerTeam, int> GetAlivePlayers()
        {
            var result = new Dictionary<PlayerTeam, int>()
            {
                { PlayerTeam.Team1, 0 },
                { PlayerTeam.Team2, 0 },
                { PlayerTeam.Team3, 0 },
                { PlayerTeam.Team4, 0 },
                { PlayerTeam.Independent, 0 },
            };

            foreach (var p in Game.GetPlayers())
            {
                if (!p.IsDead)
                    result[p.GetTeam()]++;
            }

            return result;
        }

        public static List<PlayerTeam> TeamsLeft()
        {
            var results = new List<PlayerTeam>();
            var alivePlayers = GetAlivePlayers();

            foreach (var kv in alivePlayers)
            {
                if (alivePlayers[kv.Key] > 0)
                    results.Add(kv.Key);
            }
            return results;
        }
        public virtual void OnDeath(PlayerDeathArgs args)
        {
            if (args.Killed) SayDeathLine();

            // delay a bit to make sure the spy state is updated
            ScriptHelper.Timeout(() =>
            {
                // there are spy bots, the game doesn't know how many teams actually are. Check gameover manually
                if (!Game.AutoVictoryConditionEnabled)
                {
                    var spyBots = BotManager.GetBots<SpyBot>().Where(x => !x.Player.IsDead).ToList();
                    var isSpyBotsActive = spyBots.Count > 0 && spyBots.Any(x => x.IsDisguising);

                    if (!isSpyBotsActive && TeamsLeft().Count == 1)
                        Game.SetGameOver();
                }
            }, 100);
        }

        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos) { }

        public bool CanInfect { get { return Info.ZombieStatus != ZombieStatus.Human; } }
        public bool CanBeInfected { get { return !CanInfect && !Info.ImmuneToInfect && !Player.IsBurnedCorpse; } }
        public bool IsInfectedByZombie { get { return Info.ZombieStatus == ZombieStatus.Infected; } }
        private void UpdateInfectedStatus(IPlayer attacker, PlayerDamageArgs args)
        {
            if (CanBeInfected && attacker != null)
            {
                var directContact = args.DamageType == PlayerDamageEventType.Melee
                    && attacker.CurrentWeaponDrawn == WeaponItemType.NONE
                    && !attacker.IsKicking && !attacker.IsJumpKicking;
                var attackerBot = BotManager.GetBot(attacker);

                if (attackerBot.CanInfect && directContact)
                {
                    Infect(attackerBot.InfectTeam);
                    Game.ShowChatMessage(attacker.Name + " infected " + Player.Name);
                }
            }
        }

        public PlayerTeam InfectTeam { get; private set; }
        public void Infect(PlayerTeam team)
        {
            if (CanBeInfected)
            {
                InfectTeam = team;
                Game.PlayEffect(EffectName.CustomFloatText, Position, "infected");
                Info.ZombieStatus = ZombieStatus.Infected;
            }
        }

        public void Disarm(Vector2 projDirection, WeaponItemType type = WeaponItemType.NONE, bool destroyWeapon = false, bool playEffect = true)
        {
            type = type == WeaponItemType.NONE ? Player.CurrentWeaponDrawn : type;
            if (type == WeaponItemType.NONE) return;

            var velocity =
                Vector2.UnitX * RandomHelper.Between(.25f, 2.5f) * Math.Sign(projDirection.X) +
                Vector2.UnitY * RandomHelper.Between(.25f, 2.5f);

            if (Math.Sign(projDirection.X) == Math.Sign(Player.GetLinearVelocity().X))
                velocity += Vector2.UnitX * (Player.GetLinearVelocity().X / 2);

            if (playEffect)
                Game.PlayEffect(EffectName.CustomFloatText, Position + Vector2.UnitY * 15, "Disarmed");

            var weapon = Player.Disarm(type, velocity, false);
            if (weapon == null) return;

            weapon.SetAngularVelocity(RandomHelper.Between(-10, 10));
            if (destroyWeapon)
                weapon.SetHealth(0);
        }

        public void DisarmAll()
        {
            foreach (var weaponItemType in Constants.WeaponItemTypes)
            {
                Disarm(Vector2.Zero, weaponItemType, playEffect: false);
            }
        }

        public bool IsStunned { get; private set; }
        private Events.UpdateCallback m_effect;
        public System.Threading.Tasks.Task<bool> Stun(uint stunnedTime)
        {
            var promise = new System.Threading.Tasks.TaskCompletionSource<bool>();

            IsStunned = true;
            Player.SetInputEnabled(false);
            Player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));
            Game.PlayEffect(EffectName.CustomFloatText, Player.GetWorldPosition(), "stunned");

            m_effect = Events.UpdateCallback.Start((e) =>
            {
                var position = RandomHelper.WithinArea(Player.GetAABB());
                Game.PlayEffect(EffectName.Electric, position);
            }, 400);

            ScriptHelper.Timeout(() =>
            {
                Player.AddCommand(new PlayerCommand(PlayerCommandType.StopDeathKneel));
                Player.SetInputEnabled(true);
                IsStunned = false;
                Events.UpdateCallback.Stop(m_effect);
                m_effect = null;
                promise.TrySetResult(true);
            }, stunnedTime);

            return promise.Task;
        }

        public void UseRangeWeapon(bool value, bool shealthRangeWpn = false)
        {
            var bs = Player.GetBotBehaviorSet();
            if (bs.RangedWeaponUsage == value) return;
            
            bs.RangedWeaponUsage = value;
            Player.SetBotBehaviorSet(bs);

            if (shealthRangeWpn)
                ScriptHelper.Command(Player, PlayerCommandType.Sheath);
        }

        public void GoTo(IObject o, float guardRange = 1f, float chaseRange = 2f)
        {
            var bs = Player.GetBotBehaviorSet();
            bs.GuardRange = guardRange;
            bs.ChaseRange = chaseRange;
            SetBotBehaviorSet(bs, true);
            Player.SetGuardTarget(o);
        }

        public float Distance(Vector2 position) { return Vector2.Distance(Position, position); }
        public float Distance(IObject o) { return Distance(o.GetWorldPosition()); }

        public void SetHealth(float health, bool permanent = false)
        {
            if (health <= 0)
            {
                Player.SetHealth(0);
                return;
            }
            var modifiers = Player.GetModifiers();
            if (health > modifiers.MaxHealth)
                modifiers.MaxHealth = (int)health;
            modifiers.CurrentHealth = health;
            Player.SetModifiers(modifiers);
            if (permanent) Info.Modifiers = modifiers;
        }
        // set modifiers without changing current health and energy
        public void SetModifiers(PlayerModifiers modifiers, bool permanent = false)
        {
            modifiers.CurrentHealth = Player.GetHealth();
            modifiers.CurrentEnergy = Player.GetEnergy();
            Player.SetModifiers(modifiers);
            if (permanent) Info.Modifiers = modifiers;
        }
        public void ResetModifiers() { SetModifiers(Info.Modifiers); }

        public void SetBotBehaviorSet(BotBehaviorSet botBehaviorSet, bool permanent = false)
        {
            Player.SetBotBehaviorSet(botBehaviorSet);
            if (permanent) BotBehaviorSet = botBehaviorSet;
        }
        public void ResetBotBehaviorSet() { Player.SetBotBehaviorSet(BotBehaviorSet); }
    }
}
