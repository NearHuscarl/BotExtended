using SFDGameScriptInterface;
using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;
using BotExtended.Projectiles;
using BotExtended.Weapons;

namespace BotExtended
{
    public static class BotManager
    {
        private static Dictionary<PlayerTeam, BotFaction> CurrentBotFaction = new Dictionary<PlayerTeam, BotFaction>();
        public static int CurrentFactionSetIndex { get; private set; }
        public const PlayerTeam BotTeam = PlayerTeam.Team4;

        // Player corpses waiting to be transformed into zombies
        private static Dictionary<int, InfectedCorpse> m_infectedCorpses = new Dictionary<int, InfectedCorpse>();
        private static List<PlayerSpawner> m_playerSpawners;
        private static Dictionary<string, Bot> m_bots = new Dictionary<string, Bot>();

        public static void Initialize()
        {
            ProjectileManager.Initialize();
            WeaponManager.Initialize();

            m_playerSpawners = BotHelper.GetEmptyPlayerSpawners();

            Events.PlayerWeaponAddedActionCallback.Start(OnPlayerPickedupWeapon);
            Events.PlayerWeaponRemovedActionCallback.Start(OnPlayerDroppedWeapon);
            Events.PlayerMeleeActionCallback.Start(OnPlayerMeleeAction);
            Events.PlayerDamageCallback.Start(OnPlayerDamage);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.ProjectileHitCallback.Start(OnProjectileHit);
            Events.UpdateCallback.Start(OnUpdate);
            Events.PlayerKeyInputCallback.Start(OnPlayerKeyInput);
            Events.UserMessageCallback.Start(Command.OnUserMessage);

            var settings = Settings.Get();
            if (settings.RoundsUntilFactionRotation == 1 || settings.CurrentFaction[BotTeam] == BotFaction.None)
            {
                foreach (var team in SharpHelper.EnumToList<PlayerTeam>())
                {
                    if (team == PlayerTeam.Independent)
                        continue;

                    List<BotFaction> botFactions;

                    if (settings.BotFactions[team].Count > 1)
                        botFactions = settings.BotFactions[team]
                            .Where((f) => f != settings.CurrentFaction[team])
                            .ToList();
                    else
                        botFactions = settings.BotFactions[team];

                    // TODO: disregard spawning only boss or not when count < 3 if team != BotTeam
                    var faction = BotHelper.RandomFaction(botFactions, settings.BotCount);

                    if (team == BotTeam)
                        ScriptHelper.PrintMessage("Change faction to " + faction);
                    CurrentBotFaction[team] = faction;
                }
            }
            else
            {
                CurrentBotFaction = settings.CurrentFaction;
            }
            BotHelper.Storage.SetItem(BotHelper.StorageKey("CURRENT_FACTION"), CurrentBotFaction.Values.Select(f => f.ToString()).ToArray());

            if (settings.FactionRotationEnabled)
            {
                var roundTillNextFactionRotation = settings.RoundsUntilFactionRotation == 1 ?
                    settings.FactionRotationInterval
                    :
                    settings.RoundsUntilFactionRotation - 1;
                BotHelper.Storage.SetItem(BotHelper.StorageKey("ROUNDS_UNTIL_FACTION_ROTATION"), roundTillNextFactionRotation);
            }

            var botSpawnCount = Math.Min(settings.BotCount, m_playerSpawners.Count);

            foreach (var item in CurrentBotFaction)
            {
                var team = item.Key;
                var faction = item.Value;

                if (faction == BotFaction.None)
                    continue;

                if (team == BotTeam)
                {
                    SpawnRandomFaction(faction, botSpawnCount, team);
                }
                else
                {
                    SpawnRandomFaction(faction, 0, team);
                }
            }

            var activeUsers = ScriptHelper.GetActiveUsersByAccountID();

            foreach (var ps in settings.PlayerSettings)
            {
                var pst = PlayerSettings.Parse(ps);

                if (activeUsers.ContainsKey(pst.AccountID))
                {
                    var userID = activeUsers[pst.AccountID].UserIdentifier;
                    var player = Game.GetActiveUser(userID).GetPlayer();

                    if (pst.BotType != "None")
                    {
                        var botType = SharpHelper.StringToEnum<BotType>(pst.BotType);
                        BotHelper.SetPlayer(player, botType);
                    }

                    foreach (var w in pst.Weapons)
                    {
                        BotHelper.SetWeapon(player, w[0], w[1]);
                    }
                }
            }
        }

        private static void SpawnRandomFaction(BotFaction botFaction, int botCount, PlayerTeam team)
        {
            var factionSet = GetFactionSet(botFaction);
            var rndFactionIndex = RandomHelper.Rnd.Next(factionSet.Factions.Count);
            var faction = factionSet.Factions[rndFactionIndex];

            CurrentFactionSetIndex = rndFactionIndex;

            var bots = botCount == 0
                ? faction.Spawn(team)
                : faction.Spawn(botCount, team);

            foreach (var bot in bots)
            {
                TriggerOnSpawn(bot);
            }
        }

        public static void TriggerOnSpawn(Bot bot) { bot.OnSpawn(m_bots.Values); }

        private static void OnPlayerPickedupWeapon(IPlayer player, PlayerWeaponAddedArg arg)
        {
            if (player == null) return;
            var bot = GetBot(player);
            if (bot == Bot.None) return;
            bot.OnPickedupWeapon(arg);
        }

        private static void OnPlayerDroppedWeapon(IPlayer player, PlayerWeaponRemovedArg arg)
        {
            if (player == null) return;
            var bot = GetBot(player);
            if (bot == Bot.None) return;
            bot.OnDroppedWeapon(arg);
        }

        private static float m_lastUpdateTime = 0f;
        public static void OnUpdate(float _)
        {
            var elapsed = Game.TotalElapsedGameTime - m_lastUpdateTime;

            // Turning corpses killed by zombie into another one after some time
            foreach (var corpse in m_infectedCorpses.Values.ToList())
            {
                corpse.Update();

                if (corpse.IsZombie || !corpse.CanTurnIntoZombie)
                {
                    m_infectedCorpses.Remove(corpse.UniqueID);
                }
            }

            foreach (var player in Game.GetPlayers())
            {
                var bot = GetBot(player);

                if (bot != Bot.None)
                {
                    if (bot.Player.IsDead && bot.IsInfectedByZombie
                        && !m_infectedCorpses.ContainsKey(bot.Player.UniqueID))
                    {
                        AddInfectedCorpse(bot);
                    }
                    bot.Update(elapsed);
                }
                else
                    Wrap(player); // Normal players that are not extended bots
            }

            m_lastUpdateTime = Game.TotalElapsedGameTime;
        }

        private static void OnPlayerMeleeAction(IPlayer attacker, PlayerMeleeHitArg[] args)
        {
            if (attacker == null) return;

            foreach (var arg in args)
            {
                if (!arg.IsPlayer) continue;

                var maybePlayer = arg.HitObject;
                var bot = GetBot(maybePlayer);

                if (bot != Bot.None)
                {
                    bot.OnMeleeDamage(attacker, arg);
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

            var bot = GetBot(player);
            if (bot != Bot.None)
            {
                bot.OnDamage(attacker, args);
            }
        }

        private static void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (player == null) return;

            var bot = GetBot(player);
            if (bot == Bot.None) return;

            bot.OnDeath(args);

            if (args.Removed)
            {
                m_bots.Remove(bot.Player.CustomID);
            }
            if (args.Killed)
            {
                AddInfectedCorpse(bot);
            }
        }

        private static void AddInfectedCorpse(Bot bot)
        {
            if (bot.Info.ZombieStatus == ZombieStatus.Infected)
            {
                var player = bot.Player;
                m_infectedCorpses.Add(player.UniqueID, new InfectedCorpse(player, bot.Type, bot.Faction));
            }
        }

        private static void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            if (!args.IsPlayer) return;

            var player = Game.GetPlayer(args.HitObjectID);
            var bot = GetBot(player);
            if (bot == Bot.None) return;

            // I use this instead of PlayerDamage callback because this one include additional
            // info like normal vector
            bot.OnProjectileHit(projectile, args);
        }

        private static void OnPlayerKeyInput(IPlayer player, VirtualKeyInfo[] keyInfos)
        {
            var bot = GetBot(player);
            if (bot == Bot.None) return;

            bot.OnPlayerKeyInput(keyInfos);
        }

        public static Bot GetBot(IObject player)
        {
            Bot bot;
            if (m_bots.TryGetValue(player.CustomID, out bot)) return bot;
            return Bot.None;
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
            var bot = new Bot(player, BotType.None, BotFaction.None);

            if (string.IsNullOrEmpty(player.CustomID))
            {
                player.CustomID = Guid.NewGuid().ToString("N");
            }

            m_bots.Add(player.CustomID, bot);
            TriggerOnSpawn(bot);

            return bot;
        }

        public static Bot SpawnBot(
            BotType botType,
            BotFaction faction = BotFaction.None,
            IPlayer player = null,
            PlayerTeam team = BotTeam,
            bool ignoreFullSpawner = false,
            bool triggerOnSpawn = true)
        {
            if (player == null) player = SpawnPlayer(ignoreFullSpawner);
            if (player == null) return null;
            // player.UniqueID is unique but seems like it can change value during
            // the script lifetime. Use custom id + guid() to get the const unique id
            if (string.IsNullOrEmpty(player.CustomID))
            {
                player.CustomID = Guid.NewGuid().ToString("N");
            }

            player.SetTeam(team);

            var bot = BotFactory.Create(player, botType, faction);
            var info = bot.Info;
            var weaponSet = WeaponSet.Empty;

            if (RandomHelper.Percentage(info.EquipWeaponChance))
            {
                weaponSet = RandomHelper.GetItem(GetWeapons(botType));
            }
            BotHelper.Equip(player, weaponSet);

            var profile = RandomHelper.GetItem(GetProfiles(botType));
            player.SetProfile(profile);
            if (player.Name == "COM")
                player.SetBotName(profile.Name);

            player.SetModifiers(info.Modifiers);
            player.SetBotBehaviorSet(GetBehaviorSet(info.AIType, info.SearchItems));
            player.SetBotBehaviorActive(true);

            m_bots[player.CustomID] = bot; // This may be updated if using setplayer command

            if (triggerOnSpawn)
                TriggerOnSpawn(bot);

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

            var factionWinStatsKey = BotHelper.StorageKey(CurrentBotFaction[BotTeam], CurrentFactionSetIndex) + "_WIN_STATS";
            int[] factionOldWinStats;
            int winCount, totalMatch;

            if (BotHelper.Storage.TryGetItemIntArr(factionWinStatsKey, out factionOldWinStats))
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

            BotHelper.Storage.SetItem(factionWinStatsKey, new int[] { winCount, totalMatch });
        }

        public static IEnumerable<Bot> GetBots() { return GetBots<Bot>(); }
        public static IEnumerable<T> GetBots<T>() where T : Bot
        {
            foreach (var bot in m_bots.Values)
            {
                var b = bot as T;
                if (b != null) yield return b;
            }
        }
    }
}
