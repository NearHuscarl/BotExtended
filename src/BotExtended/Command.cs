using SFDGameScriptInterface;
using BotExtended.Factions;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;
using BotExtended.Library;
using BotExtended.Powerups;

namespace BotExtended
{
    public static class Command
    {
        public static void OnUserMessage(UserMessageCallbackArgs args)
        {
            if (!args.User.IsHost || !args.IsCommand || (args.Command != "BOTEXTENDED" && args.Command != "BE"))
            {
                return;
            }

            var message = args.CommandArguments.ToLowerInvariant();
            var words = message.Split(' ');
            var command = words.FirstOrDefault();
            var arguments = words.Skip(1);

            switch (command)
            {
                case "?":
                case "h":
                case "help":
                    PrintHelp();
                    break;

                case "v":
                case "version":
                    PrintVersion();
                    break;

                case "lf":
                case "listfaction":
                    ListBotFactions();
                    break;

                case "lb":
                case "listbot":
                    ListBotTypes();
                    break;

                case "lrp":
                case "listrangedpowerup":
                    ListRangedPowerups();
                    break;

                case "lmp":
                case "listmeleepowerup":
                    ListMeleePowerups();
                    break;

                case "ff":
                case "findfaction":
                    FindFaction(arguments);
                    break;

                case "s":
                case "settings":
                    ShowCurrentSettings();
                    break;

                case "bc":
                case "botcount":
                    SetBotCount(arguments);
                    break;

                case "c":
                case "create":
                    CreateNewBot(arguments);
                    break;

                case "f":
                case "faction":
                    SetFactions(arguments);
                    break;

                case "fr":
                case "factionrotation":
                    SetFactionRotationInterval(arguments);
                    break;

                case "nf":
                case "nextfaction":
                    SkipCurrentFaction();
                    break;

                case "sp":
                case "setplayer":
                    SetPlayer(arguments);
                    break;

                case "sw":
                case "setweapon":
                    SetWeapon(arguments);
                    break;

                case "cp":
                case "clearplsettings":
                    ClearPlayerSettings();
                    break;

                case "st":
                case "stats":
                    PrintStatistics();
                    break;

                case "cst":
                case "clearstats":
                    ClearStatistics();
                    break;

                default:
                    ScriptHelper.PrintMessage("Invalid command: " + command, BeColors.ERROR_COLOR);
                    break;
            }
        }

        private static void PrintHelp()
        {
            ScriptHelper.PrintMessage("--BotExtended help--", BeColors.ERROR_COLOR);
            ScriptHelper.PrintMessage("/<botextended|be> [help|h|?]: Print this help");
            ScriptHelper.PrintMessage("/<botextended|be> [version|v]: Print the current version");
            ScriptHelper.PrintMessage("/<botextended|be> [listfaction|lf]: List all bot factions");
            ScriptHelper.PrintMessage("/<botextended|be> [listbot|lb]: List all bot types");
            ScriptHelper.PrintMessage("/<botextended|be> [listrangedpowerup|lrp]: List all ranged powerups");
            ScriptHelper.PrintMessage("/<botextended|be> [listmeleepowerup|lmp]: List all melee powerups");
            ScriptHelper.PrintMessage("/<botextended|be> [findfaction|ff] <query>: Find all bot factions that match query");
            ScriptHelper.PrintMessage("/<botextended|be> [settings|s]: Display current script settings");
            ScriptHelper.PrintMessage("/<botextended|be> [create|c] <BotType> [Team|_] [Count]: Create new bot");
            ScriptHelper.PrintMessage("/<botextended|be> [botcount|bc] <0-10>: Set maximum bot count");
            ScriptHelper.PrintMessage("/<botextended|be> [faction|f] [Team] [-e] <names|indexes|all>: Choose a list of faction by either name or index to randomly spawn on startup");
            ScriptHelper.PrintMessage("/<botextended|be> [factionrotation|fr] <1-10>: Set faction rotation interval for every n rounds");
            ScriptHelper.PrintMessage("/<botextended|be> [nextfaction|nf]: Change the faction in the currrent faction rotation to the next faction");
            ScriptHelper.PrintMessage("/<botextended|be> [setplayer|sp] <player> <BotType>: Set <player> outfit, weapons and modifiers to <BotType>");
            ScriptHelper.PrintMessage("/<botextended|be> [setweapon|sw] <player> <WeaponItem> <Powerup>: Give <player> powerup weapon");
            ScriptHelper.PrintMessage("/<botextended|be> [clearplsettings|cp]: Clear all player settings");
            ScriptHelper.PrintMessage("/<botextended|be> [stats|st]: List all bot types and bot factions stats");
            ScriptHelper.PrintMessage("/<botextended|be> [clearstats|cst]: Clear all bot types and bot factions stats");
        }

        private static bool TryParseTeam(string arg, out PlayerTeam result, PlayerTeam defaultValue = PlayerTeam.Independent)
        {
            switch (arg)
            {
                case "t1":
                    result = PlayerTeam.Team1;
                    return true;
                case "t2":
                    result = PlayerTeam.Team2;
                    return true;
                case "t3":
                    result = PlayerTeam.Team3;
                    return true;
                case "t4":
                    result = PlayerTeam.Team4;
                    return true;
                case "t0":
                    result = PlayerTeam.Independent;
                    return true;
                default:
                    result = defaultValue;
                    return false;
            }
        }

        private static bool TryParsePlayer(string args, out IPlayer result)
        {
            foreach (var player in Game.GetPlayers())
            {
                if (player.IsRemoved) continue;

                if (player.IsUser)
                {
                    var playerIndex = -1;
                    var playerSlotIndex = player.GetUser().GameSlotIndex;

                    if (int.TryParse(args, out playerIndex))
                    {
                        if (playerSlotIndex == playerIndex)
                        {
                            result = player;
                            return true;
                        }
                    }
                    else
                    {
                        if (player.Name.ToLower() == args)
                        {
                            result = player;
                            return true;
                        }
                    }
                }
                else
                {
                    if (player.Name.ToLower() == args)
                    {
                        result = player;
                        return true;
                    }
                }
            }
            result = null;
            return false;
        }

        private static void PrintVersion()
        {
            ScriptHelper.PrintMessage("--BotExtended version--", BeColors.ERROR_COLOR);
            ScriptHelper.PrintMessage("v" + Constants.CURRENT_VERSION);
        }

        private static IEnumerable<string> GetFactionNames()
        {
            var factions = BotHelper.GetAvailableBotFactions();

            foreach (var faction in factions)
            {
                yield return ((int)faction).ToString() + ": " + SharpHelper.EnumToString(faction);
            }
        }

        private static void ListBotFactions()
        {
            ScriptHelper.PrintMessage("--BotExtended list faction--", BeColors.ERROR_COLOR);

            foreach (var factionName in GetFactionNames())
            {
                ScriptHelper.PrintMessage(factionName, BeColors.WARNING_COLOR);
            }
        }

        private static void ListBotTypes()
        {
            ScriptHelper.PrintMessage("--BotExtended list bot type--", BeColors.ERROR_COLOR);

            foreach (var botType in SharpHelper.EnumToList<BotType>())
            {
                ScriptHelper.PrintMessage((int)botType + ": " + SharpHelper.EnumToString(botType), BeColors.WARNING_COLOR);
            }
        }

        private static void ListRangedPowerups()
        {
            ScriptHelper.PrintMessage("--BotExtended list ranged powerup--", BeColors.ERROR_COLOR);

            foreach (var powerup in SharpHelper.EnumToList<RangedWeaponPowerup>())
            {
                ScriptHelper.PrintMessage((int)powerup + ": " + SharpHelper.EnumToString(powerup), BeColors.WARNING_COLOR);
            }
        }

        private static void ListMeleePowerups()
        {
            ScriptHelper.PrintMessage("--BotExtended list melee powerup--", BeColors.ERROR_COLOR);

            foreach (var powerup in SharpHelper.EnumToList<MeleeWeaponPowerup>())
            {
                ScriptHelper.PrintMessage((int)powerup + ": " + SharpHelper.EnumToString(powerup), BeColors.WARNING_COLOR);
            }
        }

        private static void FindFaction(IEnumerable<string> arguments)
        {
            var query = arguments.FirstOrDefault();
            if (query == null) return;

            ScriptHelper.PrintMessage("--BotExtended find results--", BeColors.ERROR_COLOR);

            foreach (var factionName in GetFactionNames())
            {
                var name = factionName.ToLowerInvariant();
                if (name.Contains(query))
                    ScriptHelper.PrintMessage(factionName, BeColors.WARNING_COLOR);
            }
        }

        private static void ShowCurrentSettings()
        {
            ScriptHelper.PrintMessage("--BotExtended settings--", BeColors.ERROR_COLOR);

            var settings = Settings.Get();

            ScriptHelper.PrintMessage("-Player settings", BeColors.WARNING_COLOR);

            var activeUsers = ScriptHelper.GetActiveUsersByAccountID();

            if (settings.PlayerSettings.Count() == 0)
            {
                ScriptHelper.PrintMessage("<Empty>");
            }
            else
            {
                foreach (var ps in settings.PlayerSettings)
                {
                    var playerSettings = PlayerSettings.Parse(ps);
                    var accountID = playerSettings.AccountID;
                    var name = activeUsers.ContainsKey(accountID) ? activeUsers[accountID].Name : accountID;
                    var weaponValues = string.Join(",", playerSettings.Weapons.Select(w => w[0] + "[" + w[1] + "]"));

                    ScriptHelper.PrintMessage(string.Format("{0} [{1}]: {2}", name, playerSettings.BotType, weaponValues));
                }
            }

            ScriptHelper.PrintMessage("-Factions", BeColors.WARNING_COLOR);

            // TODO: show except for all faction minus a small amount of others
            foreach (var team in Constants.Teams)
            {
                var factions = settings.BotFactions[team];
                var currentFaction = settings.CurrentFaction[team];
                var isAllFactions = factions.Count == SharpHelper.EnumToArray<BotFaction>().Count() - 1 /* minus BotFaction.None */;
                var factionsValue = isAllFactions ? "ALL" : string.Join(",", factions);

                ScriptHelper.PrintMessage(string.Format(" - {0}[{1}]: {2}", team, currentFaction, factionsValue));
            }

            var rotationInterval = settings.FactionRotationEnabled ? settings.FactionRotationInterval.ToString() : "Disabled";
            var roundsUntilRotation = settings.FactionRotationEnabled ? settings.RoundsUntilFactionRotation.ToString() : "N/a";

            ScriptHelper.PrintMessage("-Faction rotation interval: " + rotationInterval, BeColors.WARNING_COLOR);
            ScriptHelper.PrintMessage("-Rounds until rotation: " + roundsUntilRotation, BeColors.WARNING_COLOR);
            ScriptHelper.PrintMessage("-Max bot count: " + settings.BotCount, BeColors.WARNING_COLOR);
        }

        public static void CreateNewBot(IEnumerable<string> arguments)
        {
            if (arguments.Count() < 1)
                return;

            var botTypeStr = arguments.First();
            var botType = BotType.None;

            if (SharpHelper.TryParseEnum(botTypeStr, out botType))
            {
                arguments = arguments.Skip(1);
            }
            else
            {
                ScriptHelper.PrintMessage("--BotExtended spawn bot--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid query: " + botTypeStr, BeColors.WARNING_COLOR);
                return;
            }

            var team = PlayerTeam.Independent;
            if (arguments.Any())
            {
                if (TryParseTeam(arguments.First(), out team))
                    arguments = arguments.Skip(1);
            }

            var count = 1;
            if (arguments.Any())
            {
                if (int.TryParse(arguments.First(), out count))
                    count = (int)MathHelper.Clamp(count, Constants.BOT_SPAWN_COUNT_MIN, Constants.BOT_SPAWN_COUNT_MAX);
                else
                    count = 1;
            }

            for (var i = 0; i < count; i++)
            {
                BotManager.SpawnBot(botType, player: null, team: team, ignoreFullSpawner: true);
            }

            // Dont use the string name in case it just an index
            var bot = count > 1 ? " bots" : " bot";
            ScriptHelper.PrintMessage("Spawned " + count + " " + SharpHelper.EnumToString(botType) + bot + " to " + team);
        }

        private static void SetBotCount(IEnumerable<string> arguments)
        {
            if (arguments.Count() != 1)
                return;

            var countStr = arguments.First();
            var count = 1;
            if (arguments.Any())
            {
                if (int.TryParse(countStr, out count))
                    count = (int)MathHelper.Clamp(count, Constants.BOT_COUNT_MIN, Constants.BOT_COUNT_MAX);
                else
                {
                    ScriptHelper.PrintMessage("[Botextended] Invalid query: " + countStr, BeColors.WARNING_COLOR);
                    return;
                }
            }

            BotHelper.Storage.SetItem(BotHelper.StorageKey("BOT_COUNT"), count);
            ScriptHelper.PrintMessage("[Botextended] Update successfully");
        }

        private static void SetFactions(IEnumerable<string> arguments)
        {
            var allBotFactions = SharpHelper.EnumToList<BotFaction>()
                .Select((f) => SharpHelper.EnumToString(f))
                .ToList();
            var botFactions = new List<string>();
            var excludeFlag = false;
            BotFaction botFaction;

            if (arguments.Count() == 0)
            {
                ScriptHelper.PrintMessage("--BotExtended setfaction--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid command: Argument is empty", BeColors.WARNING_COLOR);
                return;
            }

            var team = PlayerTeam.Team4;
            if (TryParseTeam(arguments.First(), out team, PlayerTeam.Team4))
            {
                arguments = arguments.Skip(1);
            }

            if (arguments.Count() == 1 && (arguments.Single() == "all" || arguments.Single() == "none"))
            {
                if (arguments.Single() == "all")
                    botFactions = new List<string> { "All" };
                if (arguments.Single() == "none")
                    botFactions = new List<string> { "None" };
            }
            else
            {
                if (arguments.First() == "-e")
                {
                    excludeFlag = true;
                    arguments = arguments.Skip(1);
                }
                foreach (var arg in arguments)
                {
                    if (arg == "none")
                    {
                        ScriptHelper.PrintMessage("--BotExtended setfaction--", BeColors.ERROR_COLOR);
                        ScriptHelper.PrintMessage("Invalid argument: Cannot mix None with other options", BeColors.WARNING_COLOR);
                        return;
                    }

                    if (SharpHelper.TryParseEnum(arg, out botFaction))
                    {
                        botFactions.Add(SharpHelper.EnumToString(botFaction));
                    }
                    else
                    {
                        ScriptHelper.PrintMessage("--BotExtended setfaction--", BeColors.ERROR_COLOR);
                        ScriptHelper.PrintMessage("Invalid argument: " + arg, BeColors.WARNING_COLOR);
                        return;
                    }
                }
            }

            if (excludeFlag)
            {
                botFactions = allBotFactions.Where((f) => !botFactions.Contains(f)).ToList();
            }

            BotHelper.Storage.SetItem(BotHelper.StorageKey("BOT_FACTIONS_" + team), botFactions.Distinct().ToArray());
            ScriptHelper.PrintMessage("[Botextended] Update successfully");
        }

        private static void SetFactionRotationInterval(IEnumerable<string> arguments)
        {
            var firstArg = arguments.FirstOrDefault();
            if (firstArg == null) return;
            int value = -1;

            if (int.TryParse(firstArg, out value))
            {
                value = (int)MathHelper.Clamp(value, 0, 10);
                BotHelper.Storage.SetItem(BotHelper.StorageKey("FACTION_ROTATION_INTERVAL"), value);
                BotHelper.Storage.SetItem(BotHelper.StorageKey("ROUNDS_UNTIL_FACTION_ROTATION"), value);
                ScriptHelper.PrintMessage("[Botextended] Update successfully");
            }
            else
                ScriptHelper.PrintMessage("[Botextended] Invalid query: " + firstArg, BeColors.WARNING_COLOR);
        }

        private static void SkipCurrentFaction()
        {
            BotHelper.Storage.SetItem(BotHelper.StorageKey("ROUNDS_UNTIL_FACTION_ROTATION"), 1);
            ScriptHelper.PrintMessage("[Botextended] Update successfully");
        }

        private static void UpdatePlayerSettings(IPlayer player, Func<PlayerSettings, PlayerSettings> update)
        {
            if (!player.IsUser) return;
            var accountID = player.GetUser().AccountID;
            if (string.IsNullOrEmpty(accountID)) return;

            var key = BotHelper.StorageKey("PLAYER_SETTINGS");
            string[] allPlayerSettings;

            if (BotHelper.Storage.TryGetItemStringArr(key, out allPlayerSettings))
            {
                var isUpdate = false;
                for (var i = 0; i < allPlayerSettings.Length; i++)
                {
                    if (allPlayerSettings[i].StartsWith(accountID))
                    {
                        var oldPlayerSettings = PlayerSettings.Parse(allPlayerSettings[i]);
                        var newPlayerSettings = update(oldPlayerSettings);

                        isUpdate = true;

                        if (newPlayerSettings.IsEmpty())
                        {
                            var r = allPlayerSettings.ToList();
                            r.RemoveAt(i);
                            allPlayerSettings = r.ToArray();
                        }
                        else
                            allPlayerSettings[i] = newPlayerSettings.ToString();
                        break;
                    }
                }

                if (!isUpdate)
                {
                    var a = allPlayerSettings.ToList();
                    var newPlayerSettings = update(PlayerSettings.Empty(accountID));

                    if (!newPlayerSettings.IsEmpty())
                    {
                        a.Add(newPlayerSettings.ToString());
                        allPlayerSettings = a.ToArray();
                    }
                }
            }
            else
            {
                var newPlayerSettings = update(PlayerSettings.Empty(accountID));

                if (!newPlayerSettings.IsEmpty())
                {
                    allPlayerSettings = new string[] { newPlayerSettings.ToString() };
                }
            }

            BotHelper.Storage.SetItem(key, allPlayerSettings);
        }

        public static void SetPlayer(IEnumerable<string> arguments)
        {
            if (arguments.Count() < 2)
            {
                ScriptHelper.PrintMessage("--BotExtended setplayer--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid arguments: " + string.Join(" ", arguments), BeColors.WARNING_COLOR);
                return;
            }

            var playerArg = string.Join(" ", arguments.Take(arguments.Count() - 1));
            IPlayer player;
            if (!TryParsePlayer(playerArg, out player))
            {
                ScriptHelper.PrintMessage("--BotExtended setplayer--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("There is no player " + playerArg, BeColors.WARNING_COLOR);
                return;
            }
            else
                arguments = arguments.Skip(arguments.Count() - 1);

            var botTypeArg = arguments.First();
            BotType botType;
            if (!SharpHelper.TryParseEnum(botTypeArg, out botType))
            {
                ScriptHelper.PrintMessage("--BotExtended setplayer--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid BotType: " + botTypeArg, BeColors.WARNING_COLOR);
                return;
            }

            UpdatePlayerSettings(player, (old) => old.Update(botType.ToString()));

            if (botType == BotType.None)
                ScriptHelper.PrintMessage("Player " + player.Name + " will be reset next round");
            else
                BotHelper.SetPlayer(player, botType);
        }

        public static void SetWeapon(IEnumerable<string> arguments)
        {
            if (arguments.Count() == 2)
                arguments = arguments.Concat(new string[] { "None" });

            if (arguments.Count() < 3)
            {
                ScriptHelper.PrintMessage("--BotExtended setweapon--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid arguments: " + string.Join(" ", arguments), BeColors.WARNING_COLOR);
                return;
            }

            var playerArg = string.Join(" ", arguments.Take(arguments.Count() - 2));
            IPlayer player;
            if (!TryParsePlayer(playerArg, out player))
            {
                ScriptHelper.PrintMessage("--BotExtended setweapon--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("There is no player " + playerArg, BeColors.WARNING_COLOR);
                return;
            }
            else
                arguments = arguments.Skip(arguments.Count() - 2);

            var weaponItemArg = arguments.First();
            WeaponItem weaponItem;
            if (!SharpHelper.TryParseEnum(weaponItemArg, out weaponItem))
            {
                ScriptHelper.PrintMessage("--BotExtended setweapon--", BeColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid WeaponItem: " + weaponItemArg, BeColors.WARNING_COLOR);
                return;
            }
            else
            {
                weaponItemArg = weaponItem.ToString();
                arguments = arguments.Skip(1);
            }

            var powerupArg = arguments.First();
            var type = Mapper.GetWeaponItemType(weaponItem);
            if (type == WeaponItemType.Rifle || type == WeaponItemType.Handgun || type == WeaponItemType.Thrown)
            {
                RangedWeaponPowerup powerup;
                if (!SharpHelper.TryParseEnum(powerupArg, out powerup))
                {
                    ScriptHelper.PrintMessage("--BotExtended setweapon--", BeColors.ERROR_COLOR);
                    ScriptHelper.PrintMessage("There is no such ranged powerup: " + powerupArg, BeColors.WARNING_COLOR);
                    return;
                }
                if (powerup != RangedWeaponPowerup.None)
                {
                    if (!PowerupDatabase.IsValidPowerup(powerup, weaponItem))
                    {
                        ScriptHelper.PrintMessage("--BotExtended setweapon--", BeColors.ERROR_COLOR);
                        ScriptHelper.PrintMessage(string.Format("Ranged powerup {0} can only be used with these weapons:", powerupArg), BeColors.WARNING_COLOR);
                        ScriptHelper.PrintMessage(string.Join(" ", PowerupDatabase.GetValidWpns(powerup)), BeColors.MESSAGE_COLOR);
                        return;
                    }
                }

                powerupArg = powerup.ToString();
            }
            if (type == WeaponItemType.Melee || type == WeaponItemType.NONE)
            {
                MeleeWeaponPowerup powerup;
                if (!SharpHelper.TryParseEnum(powerupArg, out powerup))
                {
                    ScriptHelper.PrintMessage("--BotExtended setweapon--", BeColors.ERROR_COLOR);
                    ScriptHelper.PrintMessage("There is no such melee powerup: " + powerupArg, BeColors.WARNING_COLOR);
                    return;
                }
                if (powerup != MeleeWeaponPowerup.None)
                {
                    if (!PowerupDatabase.IsValidPowerup(powerup, weaponItem))
                    {
                        ScriptHelper.PrintMessage("--BotExtended setweapon--", BeColors.ERROR_COLOR);
                        ScriptHelper.PrintMessage(string.Format("Melee powerup {0} can only be used with these weapons:", powerupArg), BeColors.WARNING_COLOR);
                        ScriptHelper.PrintMessage(string.Join(" ", PowerupDatabase.GetValidWpns(powerup)), BeColors.MESSAGE_COLOR);
                        return;
                    }
                }

                powerupArg = powerup.ToString();
            }

            if (weaponItemArg == "NONE" && powerupArg == "None")
                ScriptHelper.PrintMessage("Player " + player.Name + "'s weapon will be reset next round");
            UpdatePlayerSettings(player, (old) => old.Update(type, weaponItemArg, powerupArg));
            BotHelper.SetWeapon(player, weaponItemArg, powerupArg);
        }

        private static void ClearPlayerSettings()
        {
            BotHelper.Storage.RemoveItem(BotHelper.StorageKey("PLAYER_SETTINGS"));
            ScriptHelper.PrintMessage("[Botextended] Update successfully");
        }

        private static void PrintStatistics()
        {
            ScriptHelper.PrintMessage("--BotExtended statistics--", BeColors.ERROR_COLOR);

            var botFactions = BotHelper.GetAvailableBotFactions();
            ScriptHelper.PrintMessage("[WinCount] [TotalMatch] [SurvivalRate]", BeColors.WARNING_COLOR);
            foreach (var botFaction in botFactions)
            {
                var factionSet = GetFactionSet(botFaction);
                for (var i = 0; i < factionSet.Factions.Count; i++)
                {
                    var factionKey = BotHelper.StorageKey(botFaction, i) + "_WIN_STATS";
                    int[] winStats;

                    if (BotHelper.Storage.TryGetItemIntArr(factionKey, out winStats))
                    {
                        var winCount = winStats[0];
                        var totalMatch = winStats[1];
                        var survivalRate = (float)winCount / totalMatch;
                        var survivalRateStr = survivalRate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);

                        ScriptHelper.PrintMessage(SharpHelper.EnumToString(botFaction) + " " + i + ": "
                            + " " + winCount + " " + totalMatch + " " + survivalRateStr);
                    }
                }
            }
        }

        private static void ClearStatistics()
        {
            var botFactions = BotHelper.GetAvailableBotFactions();
            foreach (var botFaction in botFactions)
            {
                var factionSet = GetFactionSet(botFaction);
                for (var i = 0; i < factionSet.Factions.Count; i++)
                {
                    var factionKey = BotHelper.StorageKey(botFaction, i) + "_WIN_STATS";
                    BotHelper.Storage.RemoveItem(factionKey);
                }
            }

            ScriptHelper.PrintMessage("[Botextended] Clear successfully");
        }
    }
}
