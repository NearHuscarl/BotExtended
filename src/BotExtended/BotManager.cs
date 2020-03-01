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

namespace BotExtended
{
    public static class BotManager
    {
        public static BotFaction CurrentBotFaction { get; private set; }
        public static int CurrentFactionSetIndex { get; private set; }
        public const PlayerTeam BotTeam = PlayerTeam.Team4;

        // Player corpses waiting to be transformed into zombies
        private static List<InfectedCorpse> m_infectedCorpses = new List<InfectedCorpse>();
        private static List<PlayerSpawner> m_playerSpawners;
        private static Dictionary<string, Bot> m_bots = new Dictionary<string, Bot>();

        public static void Initialize()
        {
            ProjectileManager.Initialize();

            m_playerSpawners = BotHelper.GetEmptyPlayerSpawners();

            Events.PlayerMeleeActionCallback.Start(OnPlayerMeleeAction);
            Events.PlayerDamageCallback.Start(OnPlayerDamage);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.ProjectileHitCallback.Start(OnProjectileHit);
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

                CurrentBotFaction = BotHelper.RandomFaction(botFactions, settings.BotCount);
                BotHelper.Storage.SetItem(BotHelper.StorageKey("CURRENT_FACTION"), SharpHelper.EnumToString(CurrentBotFaction));
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
                BotHelper.Storage.SetItem(BotHelper.StorageKey("ROUNDS_UNTIL_FACTION_ROTATION"), roundTillNextFactionRotation);
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

        private static void SpawnRandomFaction(BotFaction botFaction, int botCount)
        {
            var factionSet = GetFactionSet(botFaction);
            var rndFactionIndex = RandomHelper.Rnd.Next(factionSet.Factions.Count);
            var faction = factionSet.Factions[rndFactionIndex];

            CurrentFactionSetIndex = rndFactionIndex;
            var bots = faction.Spawn(botCount);

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

        private static void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weaponObj)
        {
            ProjectileManager.OnPlayerDropWeapon(previousOwner, weaponObj);
            ScriptHelper.LogDebug(string.Format("Drop Event: {0} {1} {2}", previousOwner.Name, weaponObj.WeaponItem, weaponObj.UniqueID));
        }

        private static void OnPlayerPickUpWeapon(IPlayer newOwner, IObjectWeaponItem weaponObj)
        {
            ProjectileManager.OnPlayerPickUpWeapon(newOwner, weaponObj);
            ScriptHelper.LogDebug(string.Format("Pickup Event: {0} {1} {2}", newOwner.Name, weaponObj.WeaponItem, weaponObj.UniqueID));
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

            foreach (var player in Game.GetPlayers())
            {
                var bot = GetExtendedBot(player);

                if (bot != Bot.None)
                    bot.Update(elapsed);
                else
                    Wrap(player); // Normal players that are not extended bots
            }
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
            bot.PlayerDropWeaponEvent -= OnPlayerDropWeapon;
            bot.PlayerPickUpWeaponEvent -= OnPlayerPickUpWeapon;

            if (!args.Removed)
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

            var factionWinStatsKey = BotHelper.StorageKey(CurrentBotFaction, CurrentFactionSetIndex) + "_WIN_STATS";
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
