using SFDGameScriptInterface;
using BotExtended.Bots;
using BotExtended.Group;
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
        internal static string StorageKey(BotGroup botGroup, int groupIndex)
        {
            return Constants.STORAGE_KEY_PREFIX + SharpHelper.EnumToString(botGroup).ToUpperInvariant() + "_" + groupIndex;
        }

        public static BotGroup CurrentBotGroup { get; private set; }
        public static int CurrentGroupSetIndex { get; private set; }
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

            bool randomGroup;
            if (!Storage.TryGetItemBool(StorageKey("RANDOM_GROUP"), out randomGroup))
            {
                randomGroup = Constants.RANDOM_GROUP_DEFAULT_VALUE;
            }

            int botCount;
            if (!Storage.TryGetItemInt(StorageKey("BOT_COUNT"), out botCount))
            {
                botCount = Constants.MAX_BOT_COUNT_DEFAULT_VALUE;
            }

            botCount = (int)MathHelper.Clamp(botCount, 1, 10);
            var botSpawnCount = Math.Min(botCount, m_playerSpawners.Count);
            var botGroups = new List<BotGroup>();

            if (randomGroup) // Random all bot groups
            {
                botGroups = SharpHelper.GetArrayFromEnum<BotGroup>().ToList();
            }
            else // Random selected bot groups from user settings
            {
                string[] selectedGroups = null;
                if (!Storage.TryGetItemStringArr(StorageKey("BOT_GROUPS"), out selectedGroups))
                {
                    ScriptHelper.PrintMessage(
                        "Error when retrieving bot groups to spawn. Default to randomize all bot groups",
                        ScriptHelper.ERROR_COLOR);
                    botGroups = SharpHelper.GetArrayFromEnum<BotGroup>().ToList();
                }
                else
                {
                    foreach (var groupName in selectedGroups)
                        botGroups.Add(SharpHelper.StringToEnum<BotGroup>(groupName));
                }
            }

            if (!Game.IsEditorTest)
            {
                SpawnRandomGroup(botSpawnCount, botGroups);
            }
            else
            {
                //SpawnRandomGroup(botSpawnCount, botGroups);
                //IPlayer player = null;
                SpawnGroup(BotGroup.Boss_Ninja, botSpawnCount, 1);
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
            int[] botGroupSeed;
            int inext;
            int inextp;

            var getBotGroupSeedAttempt = Storage.TryGetItemIntArr(StorageKey("BOT_GROUP_SEED"), out botGroupSeed);
            var getBotGroupInextAttempt = Storage.TryGetItemInt(StorageKey("BOT_GROUP_INEXT"), out inext);
            var getBotGroupInextpAttempt = Storage.TryGetItemInt(StorageKey("BOT_GROUP_INEXTP"), out inextp);

            if (getBotGroupSeedAttempt && getBotGroupInextAttempt && getBotGroupInextpAttempt)
            {
                RandomHelper.AddRandomGenerator("BOT_GROUP", new Rnd(botGroupSeed, inext, inextp));
            }
            else
            {
                RandomHelper.AddRandomGenerator("BOT_GROUP", new Rnd());
            }
        }

        private static void SpawnRandomGroup(int botCount, List<BotGroup> botGroups)
        {
            List<BotGroup> filteredBotGroups = null;
            if (botCount < 3) // Too few for a group, spawn boss instead
            {
                filteredBotGroups = botGroups.Select(g => g).Where(g => (int)g >= Constants.BOSS_GROUP_START_INDEX).ToList();
                if (!filteredBotGroups.Any())
                    filteredBotGroups = botGroups;
            }
            else
                filteredBotGroups = botGroups;

            var rndBotGroup = RandomHelper.GetItem(filteredBotGroups, "BOT_GROUP");
            var groupSet = GetGroupSet(rndBotGroup);
            var rndGroupIndex = RandomHelper.Rnd.Next(groupSet.Groups.Count);
            var group = groupSet.Groups[rndGroupIndex];

            group.Spawn(botCount);

            foreach (var bot in m_bots.Values.ToList())
            {
                TriggerOnSpawn(bot);
            }
            CurrentBotGroup = rndBotGroup;
            CurrentGroupSetIndex = rndGroupIndex;
        }

        public static void TriggerOnSpawn(Bot bot)
        {
            bot.OnSpawn(m_bots.Values);
        }

        // Spawn exact group for debugging purpose. Usually you random the group before every match
        private static void SpawnGroup(BotGroup botGroup, int botCount, int groupIndex = -1)
        {
            SpawnRandomGroup(botCount, new List<BotGroup>() { botGroup });
        }

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

        public static void StoreStatistics()
        {
            var groupDead = true;

            foreach (var player in Game.GetPlayers())
            {
                if (!player.IsDead)
                    groupDead = false;
            }

            var botGroupKeyPrefix = StorageKey(CurrentBotGroup, CurrentGroupSetIndex);

            var groupWinCountKey = botGroupKeyPrefix + "_WIN_COUNT";
            int groupOldWinCount;
            var getGroupWinCountAttempt = Storage.TryGetItemInt(groupWinCountKey, out groupOldWinCount);

            var groupTotalMatchKey = botGroupKeyPrefix + "_TOTAL_MATCH";
            int groupOldTotalMatch;
            var getGroupTotalMatchAttempt = Storage.TryGetItemInt(groupTotalMatchKey, out groupOldTotalMatch);

            if (getGroupWinCountAttempt && getGroupTotalMatchAttempt)
            {
                if (!groupDead)
                    Storage.SetItem(groupWinCountKey, groupOldWinCount + 1);
                Storage.SetItem(groupTotalMatchKey, groupOldTotalMatch + 1);
            }
            else
            {
                if (!groupDead)
                    Storage.SetItem(groupWinCountKey, 1);
                else
                    Storage.SetItem(groupWinCountKey, 0);
                Storage.SetItem(groupTotalMatchKey, 1);
            }

            StoreRandomSeed();
        }

        private static void StoreRandomSeed()
        {
            var rnd = RandomHelper.GetRandomGenerator("BOT_GROUP");

            Storage.SetItem(StorageKey("BOT_GROUP_SEED"), rnd.SeedArray);
            Storage.SetItem(StorageKey("BOT_GROUP_INEXT"), rnd.inext);
            Storage.SetItem(StorageKey("BOT_GROUP_INEXTP"), rnd.inextp);
        }
    }
}
