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
            public BotFaction Faction { get; set; }
            public IPlayer Body { get; set; }
            public float DeathTime { get; private set; }
            public bool IsTurningIntoZombie { get; private set; }
            public bool CanTurnIntoZombie { get; private set; }
            public bool IsZombie { get; private set; }

            public InfectedCorpse(IPlayer player, BotFaction faction)
            {
                Faction = faction;
                Body = player;
                IsTurningIntoZombie = false;
                IsZombie = false;
                CanTurnIntoZombie = true;
                DeathTime = Game.TotalElapsedGameTime;
            }

            private bool TurnIntoZombie()
            {
                if (Body.IsRemoved || Body.IsBurnedCorpse) return false;

                var player = Game.CreatePlayer(Body.GetWorldPosition());
                var zombie = SpawnBot(GetZombieType(Body), Faction, player, equipWeapons: false, setProfile: false);
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

            if (settings.RoundsUntilFactionRotation == 1 || settings.CurrentFaction == BotFaction.None)
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

            if (settings.FactionRotationEnabled)
            {
                var roundTillNextFactionRotation = settings.RoundsUntilFactionRotation == 1 ?
                    settings.FactionRotationInterval
                    :
                    settings.RoundsUntilFactionRotation - 1;
                Storage.SetItem(StorageKey("ROUNDS_UNTIL_FACTION_ROTATION"), roundTillNextFactionRotation);
            }

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
            int[] botFactionRndState;

            if (Storage.TryGetItemIntArr(StorageKey("BOT_FACTION_RND_STATE"), out botFactionRndState))
            {
                RandomHelper.AddRandomGenerator("BOT_FACTION", new Rnd(
                    botFactionRndState.Skip(2).ToArray(),
                    botFactionRndState[0],
                    botFactionRndState[1])
                );
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
                m_infectedCorpses.Add(new InfectedCorpse(player, bot.Faction));
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
            BotFaction faction = BotFaction.None,
            IPlayer player = null,
            bool equipWeapons = true,
            bool setProfile = true,
            PlayerTeam team = BotTeam,
            bool ignoreFullSpawner = false)
        {
            if (player == null) player = SpawnPlayer(ignoreFullSpawner);
            if (player == null) return null;
            // player.UniqueID is unique but seems like it can change value during
            // the script lifetime. Use custom id + guid() to get the const unique id
            if (string.IsNullOrEmpty(player.CustomID))
            {
                player.CustomID = Guid.NewGuid().ToString("N");
            }

            var bot = BotFactory.Create(player, botType, faction);
            var info = bot.Info;
            var weaponSet = WeaponSet.Empty;

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

            bot.SaySpawnLine();
            m_bots[player.CustomID] = bot;

            return bot;
        }

        public static void OnShutdown() { StoreStatistics(); }

        private static void StoreStatistics()
        {
            if (!Game.IsGameOver) return; // User exits in the middle of the round
            var factionDead = true;

            foreach (var player in Game.GetPlayers())
            {
                if (!player.IsDead && player.GetTeam() == PlayerTeam.Team4)
                {
                    factionDead = false;
                    break;
                }
            }

            var factionWinStatsKey = StorageKey(CurrentBotFaction, CurrentFactionSetIndex) + "_WIN_STATS";
            int[] factionOldWinStats;
            int winCount, totalMatch;

            if (Storage.TryGetItemIntArr(factionWinStatsKey, out factionOldWinStats))
            {
                if (factionDead)
                    winCount = factionOldWinStats[0];
                else
                    winCount = factionOldWinStats[0] + 1;
                totalMatch = factionOldWinStats[1] + 1;
            }
            else
            {
                winCount = factionDead ? 0 : 1;
                totalMatch = 1;
            }

            Storage.SetItem(factionWinStatsKey, new int[] { winCount, totalMatch });
            StoreRandomSeed();
        }

        private static void StoreRandomSeed()
        {
            var rnd = RandomHelper.GetRandomGenerator("BOT_FACTION");
            var rndState = new int[] { rnd.inext, rnd.inextp }.Concat(rnd.SeedArray).ToArray();

            Storage.SetItem(StorageKey("BOT_FACTION_RND_STATE"), rndState);
        }
    }
}
