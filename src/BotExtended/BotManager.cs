using SFDGameScriptInterface;
using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.Mocks.MockObjects;
using static BotExtended.GameScript;
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
        private static List<InfectedCorpse> m_infectedCorpses = new List<InfectedCorpse>();
        private static List<PlayerSpawner> m_playerSpawners;
        private static Dictionary<string, Bot> m_bots = new Dictionary<string, Bot>();

        public static void Initialize()
        {
            ProjectileManager.Initialize();
            WeaponManager.Initialize();

            m_playerSpawners = BotHelper.GetEmptyPlayerSpawners();

            Events.PlayerMeleeActionCallback.Start(OnPlayerMeleeAction);
            Events.PlayerDamageCallback.Start(OnPlayerDamage);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.ProjectileHitCallback.Start(OnProjectileHit);
            Events.UpdateCallback.Start(OnUpdate);
            Events.PlayerKeyInputCallback.Start(OnPlayerKeyInput);
            Events.UserMessageCallback.Start(Command.OnUserMessage);

            InitRandomSeed();

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

            var activeUsers = Game.GetActiveUsers()
                .Where((u) => !u.IsBot && u.IsUser)
                .ToDictionary(u => u.AccountID, u => u);

            foreach (var ps in settings.PlayerSettings)
            {
                var pieces = ps.Split('.');
                var accountID = pieces.First();

                if (activeUsers.ContainsKey(accountID))
                {
                    var botType = SharpHelper.StringToEnum<BotType>(pieces.Last());
                    var userID = activeUsers[accountID].UserIdentifier;
                    BotHelper.SetPlayer(Game.GetActiveUser(userID).GetPlayer(), botType);
                }
            }
        }

        private static void InitRandomSeed()
        {
            int[] botFactionRndState;

            if (BotHelper.Storage.TryGetItemIntArr(BotHelper.StorageKey("BOT_FACTION_RND_STATE"), out botFactionRndState))
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

        public static void TriggerOnSpawn(Bot bot)
        {
            bot.OnSpawn(m_bots.Values);
            bot.PlayerDropWeaponEvent += OnPlayerDropWeapon;
            bot.PlayerPickUpWeaponEvent += OnPlayerPickUpWeapon;
        }

        private static void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weaponObj, float totalAmmo)
        {
            if (Game.IsEditorTest)
            {
                ScriptHelper.LogDebugF("Drop Event: {0} {1} {2}", previousOwner.Name, weaponObj.WeaponItem, weaponObj.UniqueID);
                Events.UpdateCallback.Start((e) =>
                {
                    Game.DrawArea(weaponObj.GetAABB(), Color.Yellow);
                }, 0, (ushort)(60 * 2));
            }

            ProjectileManager.OnPlayerDropWeapon(previousOwner, weaponObj, totalAmmo);
        }

        private static void OnPlayerPickUpWeapon(IPlayer newOwner, IObjectWeaponItem weaponObj, float totalAmmo)
        {
            ProjectileManager.OnPlayerPickUpWeapon(newOwner, weaponObj, totalAmmo);
            ScriptHelper.LogDebugF("Pickup Event: {0} {1} {2}", newOwner.Name, weaponObj.WeaponItem, weaponObj.UniqueID);

            // The reason I need to keep track of all weapons's ammo on map and set the ammo when players pickup weapons manually is
            // because there is no API to get the current ammo for weapons laying around the map. Which I need to make
            // things like custom disarm where the player will drop weapon on command but the current ammo of dropped weapon is lost
            if (totalAmmo != -1)
            {
                switch (weaponObj.WeaponItemType)
                {
                    case WeaponItemType.Melee:
                        if (newOwner.CurrentMeleeMakeshiftWeapon.WeaponItem != WeaponItem.NONE)
                            newOwner.SetCurrentMeleeMakeshiftDurability(totalAmmo);
                        else
                            newOwner.SetCurrentMeleeDurability(totalAmmo);
                        break;
                    case WeaponItemType.Rifle:
                        newOwner.SetCurrentPrimaryWeaponAmmo((int)totalAmmo);
                        break;
                    case WeaponItemType.Handgun:
                        newOwner.SetCurrentSecondaryWeaponAmmo((int)totalAmmo);
                        break;
                    case WeaponItemType.Thrown:
                        newOwner.SetCurrentThrownItemAmmo((int)totalAmmo);
                        break;
                }
            }
        }

        private static float m_lastUpdateTime = 0f;
        public static void OnUpdate(float _)
        {
            var elapsed = Game.TotalElapsedGameTime - m_lastUpdateTime;

            // Turning corpses killed by zombie into another one after some time
            foreach (var corpse in m_infectedCorpses.ToList())
            {
                corpse.Update();

                if (corpse.IsZombie || !corpse.CanTurnIntoZombie)
                {
                    m_infectedCorpses.Remove(corpse);
                }
            }

            foreach (var player in Game.GetPlayers())
            {
                var bot = GetExtendedBot(player);

                if (bot != Bot.None)
                    bot.Update(elapsed);
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
                var bot = GetExtendedBot(maybePlayer);

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

            var bot = GetExtendedBot(player);
            if (bot != Bot.None)
            {
                bot.OnDamage(attacker, args);
            }

            UpdateInfectedStatus(player, attacker, args);
        }

        private static void UpdateInfectedStatus(IPlayer player, IPlayer attacker, PlayerDamageArgs args)
        {
            if (!CanInfectFrom(player) && !player.IsBurnedCorpse && attacker != null)
            {
                var directContact = args.DamageType == PlayerDamageEventType.Melee
                    && attacker.CurrentWeaponDrawn == WeaponItemType.NONE
                    && !attacker.IsKicking && !attacker.IsJumpKicking;

                if (CanInfectFrom(attacker) && directContact)
                {
                    var extendedBot = GetExtendedBot(player);

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

            var bot = GetExtendedBot(player);
            if (bot == Bot.None) return;

            if (!args.Removed)
            {
                bot.SayDeathLine();
            }

            bot.OnDeath(args);

            if (args.Removed)
            {
                bot.PlayerDropWeaponEvent -= OnPlayerDropWeapon;
                bot.PlayerPickUpWeaponEvent -= OnPlayerPickUpWeapon;
            }
            else
            {
                if (bot.Info.ZombieStatus == ZombieStatus.Infected)
                {
                    m_infectedCorpses.Add(new InfectedCorpse(player, bot.Type, bot.Faction));
                }
            }
        }

        private static void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            if (!args.IsPlayer) return;

            var player = Game.GetPlayer(args.HitObjectID);
            var bot = GetExtendedBot(player);
            if (bot == Bot.None) return;

            // I use this instead of PlayerDamage callback because this one include additional
            // info like normal vector
            bot.OnProjectileHit(projectile, args);
        }

        private static void OnPlayerKeyInput(IPlayer player, VirtualKeyInfo[] keyInfos)
        {
            var bot = GetExtendedBot(player);
            if (bot == Bot.None) return;

            bot.OnPlayerKeyInput(keyInfos);
        }

        public static Bot GetExtendedBot(IObject player)
        {
            Bot bot;
            if (m_bots.TryGetValue(player.CustomID, out bot)) return bot;
            return Bot.None;
        }

        public static bool CanInfectFrom(IPlayer player)
        {
            var extendedBot = GetExtendedBot(player);

            return extendedBot != Bot.None
                    && extendedBot.Info.ZombieStatus != ZombieStatus.Human;
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
            TriggerOnSpawn(bot);

            return bot;
        }

        public static Bot SpawnBot(
            BotType botType,
            BotFaction faction = BotFaction.None,
            IPlayer player = null,
            bool equipWeapons = true,
            bool setProfile = true,
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

            var bot = BotFactory.Create(player, botType, faction);
            var info = bot.Info;
            var weaponSet = WeaponSet.Empty;

            if (equipWeapons)
            {
                if (RandomHelper.Between(0f, 1f) < info.EquipWeaponChance)
                {
                    weaponSet = RandomHelper.GetItem(GetWeapons(botType));
                }
                BotHelper.Equip(player, weaponSet);
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
            StoreRandomSeed();
        }

        private static void StoreRandomSeed()
        {
            var rnd = RandomHelper.GetRandomGenerator("BOT_FACTION");
            var rndState = new int[] { rnd.inext, rnd.inextp }.Concat(rnd.SeedArray).ToArray();

            BotHelper.Storage.SetItem(BotHelper.StorageKey("BOT_FACTION_RND_STATE"), rndState);
        }

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
