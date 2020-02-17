using SFDGameScriptInterface;
using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.Mocks.MockObjects;
using static BotExtended.GameScript;

namespace BotExtended
{
    public static class BotHelper
    {
        private static IScriptStorage _storage;
        public static IScriptStorage Storage
        {
            get
            {
                if (_storage == null)
                    _storage = Game.LocalStorage; return _storage;
            }
        }

        private class InfectedCorpse
        {
            public static int TimeToTurnIntoZombie = 5000;
            public IPlayer Body { get; set; }
            public float DeathTime { get; private set; }
            public bool IsTurningIntoZombie { get; private set; }
            public bool CanTurnIntoZombie { get; private set; }
            public bool IsZombie { get; private set; }

            private bool TurnIntoZombie()
            {
                if (Body.IsRemoved || Body.IsBurnedCorpse) return false;

                var player = Game.CreatePlayer(Body.GetWorldPosition());
                var zombie = SpawnBot(GetZombieType(Body), player, equipWeapons: false, setProfile: false);
                var zombieBody = zombie.Player;

                var modifiers = Body.GetModifiers();
                // Marauder has fake MaxHealth to have blood effect on the face
                if (Enum.GetName(typeof(BotType), GetExtendedBot(Body).Type).StartsWith("Marauder"))
                    modifiers.CurrentHealth = modifiers.MaxHealth = 75;
                else
                    modifiers.CurrentHealth = modifiers.MaxHealth * 0.75f;
                zombieBody.SetModifiers(modifiers);

                var profile = Body.GetProfile();
                zombieBody.SetProfile(ToZombieProfile(profile));
                zombieBody.SetBotName(Body.Name);

                Body.Remove();
                Body = zombieBody;
                Body.SetBotBehaivorActive(false);
                Body.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));
                IsTurningIntoZombie = true;
                return true;
            }

            public InfectedCorpse(IPlayer player)
            {
                Body = player;
                IsTurningIntoZombie = false;
                IsZombie = false;
                CanTurnIntoZombie = true;
                DeathTime = Game.TotalElapsedGameTime;
            }

            public void Update()
            {
                if (ScriptHelper.IsElapsed(DeathTime, TimeToTurnIntoZombie))
                {
                    if (!IsTurningIntoZombie)
                    {
                        CanTurnIntoZombie = TurnIntoZombie();
                    }
                    if (!IsZombie)
                    {
                        UpdateTurningIntoZombieAnimation();
                    }
                }
            }

            private bool isKneeling;
            private float kneelingTime;
            private void UpdateTurningIntoZombieAnimation()
            {
                if (!isKneeling)
                {
                    kneelingTime = Game.TotalElapsedGameTime;
                    isKneeling = true;
                }
                else
                {
                    if (ScriptHelper.IsElapsed(kneelingTime, 700))
                    {
                        Body.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
                        Body.SetBotBehaivorActive(true);
                        IsZombie = true;
                    }
                }
            }
        }

        internal static string StorageKey(string key)
        {
            return Constants.STORAGE_KEY_PREFIX + key;
        }
        internal static string StorageKey(BotType botType)
        {
            return Constants.STORAGE_KEY_PREFIX + SharpHelper.EnumToString(botType).ToUpperInvariant();
        }
        internal static string StorageKey(BotFaction botFaction, int factionIndex)
        {
            return Constants.STORAGE_KEY_PREFIX + SharpHelper.EnumToString(botFaction).ToUpperInvariant() + "_" + factionIndex;
        }

        public static BotFaction CurrentBotFaction { get; private set; }
        public static int CurrentFactionSetIndex { get; private set; }
        public const PlayerTeam BotTeam = PlayerTeam.Team4;

        // Player corpses waiting to be transformed into zombies
        private static List<InfectedCorpse> m_infectedCorpses = new List<InfectedCorpse>();
        private static List<PlayerSpawner> m_playerSpawners;
        private static Dictionary<string, Bot> m_bots = new Dictionary<string, Bot>();

        public static void Initialize()
        {
            m_playerSpawners = GetEmptyPlayerSpawners();

            Events.PlayerMeleeActionCallback.Start(OnPlayerMeleeAction);
            Events.PlayerDamageCallback.Start(OnPlayerDamage);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.UpdateCallback.Start(OnUpdate);
            Events.UserMessageCallback.Start(Command.OnUserMessage);

            InitRandomSeed();

            var settings = Settings.Get();

            if (settings.RoundsUntilFactionRotation == 1 || settings.UnknownCurrentFaction)
            {
                List<BotFaction> botFactions;

                if (settings.BotFactions.Count > 1)
                    botFactions = settings.BotFactions
                        .Where((f) => f != settings.CurrentFaction)
                        .ToList();
                else
                    botFactions = settings.BotFactions;

                CurrentBotFaction = RandomFaction(botFactions, settings.BotCount);
                Storage.SetItem(StorageKey("CURRENT_FACTION"), SharpHelper.EnumToString(CurrentBotFaction));
                ScriptHelper.PrintMessage("Change faction to " + CurrentBotFaction);
            }
            else
            {
                CurrentBotFaction = settings.CurrentFaction;
            }

            var roundTillNextFactionRotation = settings.RoundsUntilFactionRotation <= 1 ?
                settings.FactionRotationInterval
                :
                settings.RoundsUntilFactionRotation - 1;
            Storage.SetItem(StorageKey("ROUNDS_UNTIL_FACTION_ROTATION"), roundTillNextFactionRotation);

            var botSpawnCount = Math.Min(settings.BotCount, m_playerSpawners.Count);

            if (!Game.IsEditorTest)
            {
                SpawnRandomFaction(CurrentBotFaction, botSpawnCount);
            }
            else
            {
                SpawnRandomFaction(CurrentBotFaction, botSpawnCount);
                return;

                //SpawnRandomFaction(botSpawnCount, botFactions);
                //IPlayer player = null;
                //Game.GetPlayers()[0].SetProfile(GetProfiles(BotType.Mecha).First());
                Game.GetPlayers()[0].SetModifiers(new PlayerModifiers()
                {
                    RunSpeedModifier = 4f,
                    SprintSpeedModifier = 4f,
                });
                //Game.RunCommand("ih 1");
                var p = Game.GetPlayers().Last();
                var mod = p.GetModifiers();
                //mod.CurrentHealth = 1;
                p.SetModifiers(mod);
                //m_bots.First().Value.Player.SetHealth(1);
                //SpawnBot(BotType.Bandido);
            }
        }

        private static void InitRandomSeed()
        {
            int[] botFactionSeed;
            int inext;
            int inextp;

            var getBotFactionSeedAttempt = Storage.TryGetItemIntArr(StorageKey("BOT_FACTION_SEED"), out botFactionSeed);
            var getBotFactionInextAttempt = Storage.TryGetItemInt(StorageKey("BOT_FACTION_INEXT"), out inext);
            var getBotFactionInextpAttempt = Storage.TryGetItemInt(StorageKey("BOT_FACTION_INEXTP"), out inextp);

            if (getBotFactionSeedAttempt && getBotFactionInextAttempt && getBotFactionInextpAttempt)
            {
                RandomHelper.AddRandomGenerator("BOT_FACTION", new Rnd(botFactionSeed, inext, inextp));
            }
            else
            {
                RandomHelper.AddRandomGenerator("BOT_FACTION", new Rnd());
            }
        }

        private static BotFaction RandomFaction(List<BotFaction> botFactions, int botCount)
        {
            List<BotFaction> filteredBotFactions = null;
            if (botCount < 3) // Too few for a faction, spawn boss instead
            {
                filteredBotFactions = botFactions.Select(g => g).Where(g => (int)g >= Constants.BOSS_FACTION_START_INDEX).ToList();
                if (!filteredBotFactions.Any())
                    filteredBotFactions = botFactions;
            }
            else
                filteredBotFactions = botFactions;

            var rndBotFaction = RandomHelper.GetItem(filteredBotFactions, "BOT_FACTION");

            return rndBotFaction;
        }

        private static void SpawnRandomFaction(BotFaction botFaction, int botCount)
        {
            var factionSet = GetFactionSet(botFaction);
            var rndFactionIndex = RandomHelper.Rnd.Next(factionSet.Factions.Count);
            var faction = factionSet.Factions[rndFactionIndex];

            CurrentFactionSetIndex = rndFactionIndex;
            faction.Spawn(botCount);

            foreach (var bot in m_bots.Values.ToList())
            {
                TriggerOnSpawn(bot);
            }
        }

        public static void TriggerOnSpawn(Bot bot) { bot.OnSpawn(m_bots.Values); }

        public static void OnUpdate(float elapsed)
        {
            // Turning corpses killed by zombie into another one after some time
            foreach (var corpse in m_infectedCorpses.ToList())
            {
                corpse.Update();

                if (corpse.IsZombie || !corpse.CanTurnIntoZombie)
                {
                    m_infectedCorpses.Remove(corpse);
                }
            }

            foreach (var bot in m_bots.Values)
            {
                bot.Update(elapsed);
            }
        }

        private static void OnPlayerMeleeAction(IPlayer attacker, PlayerMeleeHitArg[] args)
        {
            if (attacker == null) return;

            foreach (var arg in args)
            {
                if (!arg.IsPlayer) continue;

                Bot enemy;
                if (m_bots.TryGetValue(arg.HitObject.CustomID, out enemy))
                {
                    enemy.OnMeleeDamage(attacker, arg);
                }
            }
        }

        private static void OnPlayerDamage(IPlayer player, PlayerDamageArgs args)
        {
            if (player == null) return;

            IPlayer attacker = null;
            if (args.DamageType == PlayerDamageEventType.Melee)
            {
                attacker = Game.GetPlayer(args.SourceID);
            }
            if (args.DamageType == PlayerDamageEventType.Projectile)
            {
                var projectile = Game.GetProjectile(args.SourceID);
                attacker = Game.GetPlayer(projectile.OwnerPlayerID);
            }

            Bot enemy;
            if (m_bots.TryGetValue(player.CustomID, out enemy))
            {
                enemy.OnDamage(attacker, args);
            }

            UpdateInfectedStatus(player, attacker, args);
        }

        private static void UpdateInfectedStatus(IPlayer player, IPlayer attacker, PlayerDamageArgs args)
        {
            if (!CanInfectFrom(player) && !player.IsBurnedCorpse && attacker != null)
            {
                var attackerPunching = args.DamageType == PlayerDamageEventType.Melee
                    && attacker.CurrentWeaponDrawn == WeaponItemType.NONE
                    && !attacker.IsKicking && !attacker.IsJumpKicking;

                if (CanInfectFrom(attacker) && attackerPunching)
                {
                    var extendedBot = GetExtendedBot(player);

                    // Normal players that are not extended bots
                    if (extendedBot == Bot.None)
                    {
                        extendedBot = Wrap(player);
                    }

                    if (!extendedBot.Info.ImmuneToInfect)
                    {
                        Game.PlayEffect(EffectName.CustomFloatText, player.GetWorldPosition(), "infected");
                        Game.ShowChatMessage(attacker.Name + " infected " + player.Name);
                        extendedBot.Info.ZombieStatus = ZombieStatus.Infected;

                        if (player.IsDead)
                        {
                            m_infectedCorpses.Add(new InfectedCorpse(player));
                        }
                    }
                }
            }
        }

        private static void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (player == null) return;

            Bot enemy;
            if (m_bots.TryGetValue(player.CustomID, out enemy))
            {
                if (!args.Removed)
                {
                    enemy.SayDeathLine();
                }
                enemy.OnDeath(args);
            }

            var bot = GetExtendedBot(player);

            if (bot != Bot.None && bot.Info.ZombieStatus == ZombieStatus.Infected)
            {
                m_infectedCorpses.Add(new InfectedCorpse(player));
            }
        }

        public static Bot GetExtendedBot(IPlayer player)
        {
            return m_bots.ContainsKey(player.CustomID) ? m_bots[player.CustomID] : Bot.None;
        }

        private static BotType GetZombieType(IPlayer player)
        {
            if (player == null)
            {
                throw new Exception("Player cannot be null");
            }
            var botType = GetExtendedBot(player).Type;

            if (botType == BotType.None)
            {
                var playerAI = player.GetBotBehavior().PredefinedAI;

                switch (playerAI)
                {
                    // Expert, Hard
                    case PredefinedAIType.BotA:
                    case PredefinedAIType.BotB:
                        return BotType.ZombieFighter;

                    default: // Player is user or something else
                        return BotType.Zombie;
                }
            }
            else
            {
                var botInfo = GetInfo(botType);
                var aiType = botInfo.AIType;

                switch (aiType)
                {
                    case BotAI.Hacker:
                    case BotAI.Expert:
                    case BotAI.Hard:
                    case BotAI.MeleeHard:
                    case BotAI.MeleeExpert:
                        return BotType.ZombieFighter;

                    case BotAI.Ninja:
                        return BotType.ZombieChild;

                    case BotAI.Hulk:
                        return BotType.ZombieBruiser;
                }

                var modifiers = botInfo.Modifiers;

                if (modifiers.SprintSpeedModifier >= 1.1f)
                    return BotType.ZombieChild;

                if (modifiers.SizeModifier == 1.25f)
                    return BotType.ZombieFat;

                return BotType.Zombie;
            }
        }

        public static IProfile ToZombieProfile(IProfile profile)
        {
            switch (profile.Skin.Name)
            {
                case "Normal":
                case "Tattoos":
                    profile.Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", "");
                    break;

                case "Normal_fem":
                case "Tattoos_fem":
                    profile.Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", "");
                    break;

                case "BearSkin":
                    profile.Skin = new IProfileClothingItem("FrankenbearSkin", "ClothingDarkGray", "ClothingLightBlue", "");
                    break;
            }

            return profile;
        }

        public static bool CanInfectFrom(IPlayer player)
        {
            var extendedBot = GetExtendedBot(player);

            return extendedBot != Bot.None
                    && extendedBot.Info.ZombieStatus != ZombieStatus.Human;
        }

        private static List<PlayerSpawner> GetEmptyPlayerSpawners()
        {
            var spawners = Game.GetObjectsByName("SpawnPlayer");
            var emptySpawners = new List<PlayerSpawner>();
            var players = Game.GetPlayers();

            foreach (var spawner in spawners)
            {
                if (!ScriptHelper.SpawnerHasPlayer(spawner, players))
                {
                    emptySpawners.Add(new PlayerSpawner
                    {
                        Position = spawner.GetWorldPosition(),
                        HasSpawned = false,
                    });
                }
            }

            return emptySpawners;
        }

        private static IPlayer SpawnPlayer(bool ignoreFullSpawner = false)
        {
            List<PlayerSpawner> emptySpawners = null;

            if (ignoreFullSpawner)
            {
                emptySpawners = m_playerSpawners;
            }
            else
            {
                emptySpawners = m_playerSpawners
                    .Select(Q => Q)
                    .Where(Q => Q.HasSpawned == false)
                    .ToList();
            }

            if (!emptySpawners.Any())
            {
                return null;
            }

            var rndSpawner = RandomHelper.GetItem(emptySpawners);
            var player = Game.CreatePlayer(rndSpawner.Position);

            rndSpawner.HasSpawned = true;

            return player;
        }

        private static Bot Wrap(IPlayer player)
        {
            var bot = new Bot(player);

            if (string.IsNullOrEmpty(player.CustomID))
            {
                player.CustomID = Guid.NewGuid().ToString("N");
            }

            m_bots.Add(player.CustomID, bot);
            return bot;
        }

        public static Bot SpawnBot(
            BotType botType,
            IPlayer player = null,
            bool equipWeapons = true,
            bool setProfile = true,
            PlayerTeam team = BotTeam,
            bool ignoreFullSpawner = false)
        {
            var info = GetInfo(botType);
            var weaponSet = WeaponSet.Empty;

            if (player == null) player = SpawnPlayer(ignoreFullSpawner);
            if (player == null) return null;
            // player.UniqueID is unique but seems like it can change value during
            // the script lifetime. Use custom id + guid() to get the const unique id
            if (string.IsNullOrEmpty(player.CustomID))
            {
                player.CustomID = Guid.NewGuid().ToString("N");
            }

            if (equipWeapons)
            {
                if (RandomHelper.Between(0f, 1f) < info.EquipWeaponChance)
                {
                    weaponSet = RandomHelper.GetItem(GetWeapons(botType));
                }
                weaponSet.Equip(player);
            }

            if (setProfile)
            {
                var profile = RandomHelper.GetItem(GetProfiles(botType));
                player.SetProfile(profile);
                player.SetBotName(profile.Name);
            }

            player.SetModifiers(info.Modifiers);
            player.SetBotBehaviorSet(GetBehaviorSet(info.AIType, info.SearchItems));
            player.SetBotBehaviorActive(true);
            player.SetTeam(team);

            var bot = BotFactory.Create(player, botType, info);
            bot.SaySpawnLine();
            m_bots[player.CustomID] = bot;

            return bot;
        }

        public static void OnShutdown() { StoreStatistics(); }

        private static void StoreStatistics()
        {
            var factionDead = true;

            foreach (var player in Game.GetPlayers())
            {
                if (!player.IsDead)
                {
                    factionDead = false;
                    break;
                }
            }

            var botFactionKeyPrefix = StorageKey(CurrentBotFaction, CurrentFactionSetIndex);

            var factionWinCountKey = botFactionKeyPrefix + "_WIN_COUNT";
            int factionOldWinCount;
            var getFactionWinCountAttempt = Storage.TryGetItemInt(factionWinCountKey, out factionOldWinCount);

            var factionTotalMatchKey = botFactionKeyPrefix + "_TOTAL_MATCH";
            int factionOldTotalMatch;
            var getFactionTotalMatchAttempt = Storage.TryGetItemInt(factionTotalMatchKey, out factionOldTotalMatch);

            if (getFactionWinCountAttempt && getFactionTotalMatchAttempt)
            {
                if (!factionDead)
                    Storage.SetItem(factionWinCountKey, factionOldWinCount + 1);
                Storage.SetItem(factionTotalMatchKey, factionOldTotalMatch + 1);
            }
            else
            {
                if (!factionDead)
                    Storage.SetItem(factionWinCountKey, 1);
                else
                    Storage.SetItem(factionWinCountKey, 0);
                Storage.SetItem(factionTotalMatchKey, 1);
            }

            StoreRandomSeed();
        }

        private static void StoreRandomSeed()
        {
            var rnd = RandomHelper.GetRandomGenerator("BOT_FACTION");

            Storage.SetItem(StorageKey("BOT_FACTION_SEED"), rnd.SeedArray);
            Storage.SetItem(StorageKey("BOT_FACTION_INEXT"), rnd.inext);
            Storage.SetItem(StorageKey("BOT_FACTION_INEXTP"), rnd.inextp);
        }
    }
}
