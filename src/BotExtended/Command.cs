using SFDGameScriptInterface;
using BotExtended.Factions;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.Mocks.MockObjects;
using BotExtended.Library;

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
                    ListBotFaction();
                    break;

                case "lb":
                case "listbot":
                    ListBotType();
                    break;

                case "ff":
                case "findfaction":
                    FindFaction(arguments);
                    break;

                case "s":
                case "setting":
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

                case "st":
                case "stats":
                    PrintStatistics();
                    break;

                case "cst":
                case "clearstats":
                    ClearStatistics();
                    break;

                case "ka":
                    KillAll(); // For debugging purpose only
                    break;
                case "gm":
                    ToggleGodMode();
                    break;

                default:
                    ScriptHelper.PrintMessage("Invalid command", ScriptHelper.ERROR_COLOR);
                    break;
            }
        }

        private static void PrintHelp()
        {
            ScriptHelper.PrintMessage("--BotExtended help--", ScriptHelper.ERROR_COLOR);
            ScriptHelper.PrintMessage("/<botextended|be> [help|h|?]: Print this help");
            ScriptHelper.PrintMessage("/<botextended|be> [version|v]: Print the current version");
            ScriptHelper.PrintMessage("/<botextended|be> [listfaction|lf]: List all bot factions");
            ScriptHelper.PrintMessage("/<botextended|be> [listbot|lb]: List all bot types");
            ScriptHelper.PrintMessage("/<botextended|be> [findfaction|ff] <query>: Find all bot factions that match query");
            ScriptHelper.PrintMessage("/<botextended|be> [settings|s]: Display current script settings");
            ScriptHelper.PrintMessage("/<botextended|be> [create|c] <BotType> [Team|_] [Count]: Create new bot");
            ScriptHelper.PrintMessage("/<botextended|be> [botcount|bc] <1-10>: Set maximum bot count");
            ScriptHelper.PrintMessage("/<botextended|be> [faction|f] [-e] <names|indexes|all>: Choose a list of faction by either name or index to randomly spawn on startup");
            ScriptHelper.PrintMessage("/<botextended|be> [factionrotation|fr] <1-10>: Set faction rotation interval for every n rounds");
            ScriptHelper.PrintMessage("/<botextended|be> [nextfaction|nf]: Change the faction in the currrent faction rotation to the next faction");
            ScriptHelper.PrintMessage("/<botextended|be> [setplayer|sp] <player> <BotType>: Set <player> outfit, weapons and modifiers to <BotType>");
            ScriptHelper.PrintMessage("/<botextended|be> [stats|st]: List all bot types and bot factions stats");
            ScriptHelper.PrintMessage("/<botextended|be> [clearstats|cst]: Clear all bot types and bot factions stats");
        }

        private static void PrintVersion()
        {
            ScriptHelper.PrintMessage("--BotExtended version--", ScriptHelper.ERROR_COLOR);
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

        private static void ListBotFaction()
        {
            ScriptHelper.PrintMessage("--BotExtended list faction--", ScriptHelper.ERROR_COLOR);

            foreach (var factionName in GetFactionNames())
            {
                ScriptHelper.PrintMessage(factionName, ScriptHelper.WARNING_COLOR);
            }
        }

        private static void ListBotType()
        {
            ScriptHelper.PrintMessage("--BotExtended list bot type--", ScriptHelper.ERROR_COLOR);

            foreach (var botType in SharpHelper.EnumToList<BotType>())
            {
                ScriptHelper.PrintMessage((int)botType + ": " + SharpHelper.EnumToString(botType), ScriptHelper.WARNING_COLOR);
            }
        }

        private static void FindFaction(IEnumerable<string> arguments)
        {
            var query = arguments.FirstOrDefault();
            if (query == null) return;

            ScriptHelper.PrintMessage("--BotExtended find results--", ScriptHelper.ERROR_COLOR);

            foreach (var factionName in GetFactionNames())
            {
                var name = factionName.ToLowerInvariant();
                if (name.Contains(query))
                    ScriptHelper.PrintMessage(factionName, ScriptHelper.WARNING_COLOR);
            }
        }

        private static void ShowCurrentSettings()
        {
            ScriptHelper.PrintMessage("--BotExtended settings--", ScriptHelper.ERROR_COLOR);

            var settings = Settings.Get();

            ScriptHelper.PrintMessage("-Factions", ScriptHelper.WARNING_COLOR);
            foreach (var botFaction in settings.BotFactions)
            {
                var index = (int)botFaction;
                ScriptHelper.PrintMessage(index + ": " + botFaction);
            }

            var rotationInterval = settings.FactionRotationEnabled ? settings.FactionRotationInterval.ToString() : "Disabled";
            var roundsUntilRotation = settings.FactionRotationEnabled ? settings.RoundsUntilFactionRotation.ToString() : "N/a";

            ScriptHelper.PrintMessage("-Faction rotation interval: " + rotationInterval, ScriptHelper.WARNING_COLOR);
            ScriptHelper.PrintMessage("-Rounds until rotation: " + roundsUntilRotation, ScriptHelper.WARNING_COLOR);
            ScriptHelper.PrintMessage("-Current faction: " + settings.CurrentFaction, ScriptHelper.WARNING_COLOR);
            ScriptHelper.PrintMessage("-Max bot count: " + settings.BotCount, ScriptHelper.WARNING_COLOR);
        }

        private static void CreateNewBot(IEnumerable<string> arguments)
        {
            var query = arguments.FirstOrDefault();
            if (query == null) return;
            var argList = arguments.ToList();

            var team = PlayerTeam.Independent;
            if (arguments.Count() >= 2 && argList[1] != "_")
            {
                if (!Enum.TryParse(argList[1], ignoreCase: true, result: out team))
                    team = PlayerTeam.Independent;
            }

            var count = 1;
            if (arguments.Count() >= 3)
            {
                if (int.TryParse(argList[2], out count))
                    count = (int)MathHelper.Clamp(count, 1, 15);
                else
                    count = 1;
            }

            var botType = BotType.None;

            if (SharpHelper.TryParseEnum(query, out botType))
            {
                for (var i = 0; i < count; i++)
                {
                    BotManager.SpawnBot(botType, player: null,
                        equipWeapons: true,
                        setProfile: true,
                        team: team,
                        ignoreFullSpawner: true);
                }

                // Dont use the string name in case it just an index
                var bot = count > 1 ? " bots" : " bot";
                ScriptHelper.PrintMessage("Spawned " + count + " " + SharpHelper.EnumToString(botType) + bot + " as " + team);
            }
            else
            {
                ScriptHelper.PrintMessage("--BotExtended spawn bot--", ScriptHelper.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid query: " + query, ScriptHelper.WARNING_COLOR);
            }
        }

        private static void SetBotCount(IEnumerable<string> arguments)
        {
            var firstArg = arguments.FirstOrDefault();
            if (firstArg == null) return;
            int value = -1;

            if (int.TryParse(firstArg, out value))
            {
                BotHelper.Storage.SetItem(BotHelper.StorageKey("BOT_COUNT"), value);
                ScriptHelper.PrintMessage("[Botextended] Update successfully");
            }
            else
                ScriptHelper.PrintMessage("[Botextended] Invalid query: " + firstArg, ScriptHelper.WARNING_COLOR);
        }

        private static void SetFactions(IEnumerable<string> arguments)
        {
            var allBotFactions = BotHelper.GetAvailableBotFactions()
                .Select((f) => SharpHelper.EnumToString(f))
                .ToList();
            var botFactions = new List<string>();
            var excludeFlag = false;
            BotFaction botFaction;

            if (arguments.Count() == 0)
            {
                ScriptHelper.PrintMessage("--BotExtended setfaction--", ScriptHelper.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid command: Argument is empty", ScriptHelper.WARNING_COLOR);
                return;
            }
            if (arguments.Count() == 1 && arguments.Single() == "all")
            {
                botFactions = allBotFactions;
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
                    if (SharpHelper.TryParseEnum(arg, out botFaction))
                    {
                        botFactions.Add(SharpHelper.EnumToString(botFaction));
                    }
                    else
                    {
                        ScriptHelper.PrintMessage("--BotExtended setfaction--", ScriptHelper.ERROR_COLOR);
                        ScriptHelper.PrintMessage("Invalid argument: " + arg, ScriptHelper.WARNING_COLOR);
                        return;
                    }
                }
            }

            if (excludeFlag)
            {
                botFactions = allBotFactions.Where((f) => !botFactions.Contains(f)).ToList();
            }

            BotHelper.Storage.SetItem(BotHelper.StorageKey("BOT_FACTIONS"), botFactions.Distinct().ToArray());
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
                ScriptHelper.PrintMessage("[Botextended] Invalid query: " + firstArg, ScriptHelper.WARNING_COLOR);
        }

        private static void SkipCurrentFaction()
        {
            BotHelper.Storage.SetItem(BotHelper.StorageKey("ROUNDS_UNTIL_FACTION_ROTATION"), 1);
            ScriptHelper.PrintMessage("[Botextended] Update successfully");
        }

        private static void CreateBot(IPlayer player, BotType bt)
        {
            var bot = BotManager.SpawnBot(bt, BotFaction.None, player, true, true, player.GetTeam());
        }
        public static void SetPlayer(IEnumerable<string> arguments)
        {
            if (arguments.Count() < 2)
            {
                ScriptHelper.PrintMessage("--BotExtended decorate--", ScriptHelper.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid arguments: " + string.Join(" ", arguments), ScriptHelper.WARNING_COLOR);
                return;
            }
            var playerArg = string.Join(" ", arguments.Take(arguments.Count() - 1));
            var botTypeArg = arguments.Last();
            BotType botType;

            if (!SharpHelper.TryParseEnum(botTypeArg, out botType))
            {
                ScriptHelper.PrintMessage("--BotExtended decorate--", ScriptHelper.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid BotType: " + botTypeArg, ScriptHelper.WARNING_COLOR);
                return;
            }

            foreach (var player in Game.GetPlayers())
            {
                if (!player.IsUser || player.IsRemoved) continue;

                var playerIndex = -1;
                var playerSlotIndex = player.GetUser().GameSlotIndex;

                if (int.TryParse(playerArg, out playerIndex))
                {
                    if (playerSlotIndex == playerIndex)
                    {
                        CreateBot(player, botType);return;
                    }
                }
                else
                {
                    if (player.Name.ToLower() == playerArg)
                    {
                        CreateBot(player, botType);return;
                    }
                }
            }
            ScriptHelper.PrintMessage("--BotExtended decorate--", ScriptHelper.ERROR_COLOR);
            ScriptHelper.PrintMessage("There is no player " + playerArg, ScriptHelper.WARNING_COLOR);
        }

        private static void PrintStatistics()
        {
            ScriptHelper.PrintMessage("--BotExtended statistics--", ScriptHelper.ERROR_COLOR);

            var botFactions = BotHelper.GetAvailableBotFactions();
            ScriptHelper.PrintMessage("[WinCount] [TotalMatch] [SurvivalRate]", ScriptHelper.WARNING_COLOR);
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

        private static void KillAll()
        {
            if (!Game.IsEditorTest) return;
            var players = Game.GetPlayers();
            foreach (var player in players)
            {
                if (player.GetUser() == null || !player.GetUser().IsHost)
                    player.Kill();
            }
        }

        private static bool m_godMode = (Game.IsEditorTest ? true : false);
        private static void ToggleGodMode()
        {
            m_godMode = !m_godMode;
            var modifiers = Game.GetPlayers()[0].GetModifiers();

            if (m_godMode)
            {
                modifiers.MaxHealth = 5000;
                modifiers.CurrentHealth = 5000;
                modifiers.InfiniteAmmo = 1;
                modifiers.MeleeStunImmunity = 1;
                ScriptHelper.PrintMessage("[Botextended] God mode - ON");
            }
            else
            {
                modifiers.MaxHealth = 100;
                modifiers.CurrentHealth = 100;
                modifiers.InfiniteAmmo = 0;
                modifiers.MeleeStunImmunity = 0;
                ScriptHelper.PrintMessage("[Botextended] God mode - OFF");
            }
            Game.GetPlayers()[0].SetModifiers(modifiers);
        }
    }
}
