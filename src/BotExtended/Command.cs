using SFDGameScriptInterface;
using BotExtended.Group;
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

                case "lg":
                case "listgroup":
                    ListBotGroup();
                    break;

                case "lb":
                case "listbot":
                    ListBotType();
                    break;

                case "/":
                case "f":
                case "find":
                    FindGroup(arguments);
                    break;

                case "s":
                case "setting":
                    ShowCurrentSettings();
                    break;

                case "bc":
                case "botcount":
                    SetBotCount(arguments);
                    break;

                case "sp":
                case "spawn":
                    SpawnNewBot(arguments);
                    break;

                case "r":
                case "random":
                    SetRandomGroup(arguments);
                    break;

                case "g":
                case "group":
                    SelectGroup(arguments);
                    break;

                case "d":
                case "decorate":
                    DecoratePlayer(arguments);
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
            ScriptHelper.PrintMessage("/<botextended|be> [listgroup|lg]: List all bot groups");
            ScriptHelper.PrintMessage("/<botextended|be> [listbot|lb]: List all bot types");
            ScriptHelper.PrintMessage("/<botextended|be> [find|f|/] <query>: Find all bot groups that match query");
            ScriptHelper.PrintMessage("/<botextended|be> [settings|s]: Display current script settings");
            ScriptHelper.PrintMessage("/<botextended|be> [spawn|sp] <BotType> [Team|_] [Count]: Spawn bot");
            ScriptHelper.PrintMessage("/<botextended|be> [botcount|bc] <1-10>: Set maximum bot count");
            ScriptHelper.PrintMessage("/<botextended|be> [random|r] <0|1>: Random all groups at startup if set to 1. This option will disregard the current group list");
            ScriptHelper.PrintMessage("/<botextended|be> [group|g] <group names|indexes>: Choose a list of group by either name or index to randomly spawn on startup");
            ScriptHelper.PrintMessage("/<botextended|be> [decorate|d] <player> <BotType>: Change <player> outfit, weapons and modifiers to <BotType>");
            ScriptHelper.PrintMessage("/<botextended|be> [stats|st]: List all bot types and bot groups stats");
            ScriptHelper.PrintMessage("/<botextended|be> [clearstats|cst]: Clear all bot types and bot groups stats");
        }

        private static void PrintVersion()
        {
            ScriptHelper.PrintMessage("--BotExtended version--", ScriptHelper.ERROR_COLOR);
            ScriptHelper.PrintMessage("v" + Constants.CURRENT_VERSION);
        }

        private static IEnumerable<string> GetGroupNames()
        {
            var groups = SharpHelper.GetArrayFromEnum<BotGroup>();

            foreach (var group in groups)
            {
                yield return ((int)group).ToString() + ": " + SharpHelper.EnumToString(group);
            }
        }

        private static void ListBotGroup()
        {
            ScriptHelper.PrintMessage("--BotExtended list group--", ScriptHelper.ERROR_COLOR);

            foreach (var groupName in GetGroupNames())
            {
                ScriptHelper.PrintMessage(groupName, ScriptHelper.WARNING_COLOR);
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

        private static void FindGroup(IEnumerable<string> arguments)
        {
            var query = arguments.FirstOrDefault();
            if (query == null) return;

            ScriptHelper.PrintMessage("--BotExtended find results--", ScriptHelper.ERROR_COLOR);

            foreach (var groupName in GetGroupNames())
            {
                var name = groupName.ToLowerInvariant();
                if (name.Contains(query))
                    ScriptHelper.PrintMessage(groupName, ScriptHelper.WARNING_COLOR);
            }
        }

        private static void ShowCurrentSettings()
        {
            ScriptHelper.PrintMessage("--BotExtended settings--", ScriptHelper.ERROR_COLOR);

            string[] groups = null;
            if (BotHelper.Storage.TryGetItemStringArr(BotHelper.StorageKey("BOT_GROUPS"), out groups))
            {
                ScriptHelper.PrintMessage("-Current groups", ScriptHelper.WARNING_COLOR);
                for (var i = 0; i < groups.Length; i++)
                {
                    var botGroup = SharpHelper.StringToEnum<BotGroup>(groups[i]);
                    var index = (int)botGroup;
                    ScriptHelper.PrintMessage(index + ": " + groups[i]);
                }
            }

            bool randomGroup;
            if (!BotHelper.Storage.TryGetItemBool(BotHelper.StorageKey("RANDOM_GROUP"), out randomGroup))
            {
                randomGroup = Constants.RANDOM_GROUP_DEFAULT_VALUE;
            }
            ScriptHelper.PrintMessage("-Random ALL groups: " + randomGroup, ScriptHelper.WARNING_COLOR);

            int botCount;
            if (!BotHelper.Storage.TryGetItemInt(BotHelper.StorageKey("BOT_COUNT"), out botCount))
            {
                botCount = Constants.MAX_BOT_COUNT_DEFAULT_VALUE;
            }
            ScriptHelper.PrintMessage("-Max bot count: " + botCount, ScriptHelper.WARNING_COLOR);
        }

        private static void SpawnNewBot(IEnumerable<string> arguments)
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
                    BotHelper.SpawnBot(botType, player: null,
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

        private static void SetRandomGroup(IEnumerable<string> arguments)
        {
            var firstArg = arguments.FirstOrDefault();
            if (firstArg == null) return;
            int value = -1;

            if (firstArg != "0" && firstArg != "1")
            {
                ScriptHelper.PrintMessage("--BotExtended random group--", ScriptHelper.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid value: " + value + "Value is either 1 (true) or 0 (false): ", ScriptHelper.WARNING_COLOR);
                return;
            }

            if (int.TryParse(firstArg, out value))
            {
                if (value == 1)
                    BotHelper.Storage.SetItem(BotHelper.StorageKey("RANDOM_GROUP"), true);
                if (value == 0)
                    BotHelper.Storage.SetItem(BotHelper.StorageKey("RANDOM_GROUP"), false);
                ScriptHelper.PrintMessage("[Botextended] Update successfully");
            }
            else
                ScriptHelper.PrintMessage("[Botextended] Invalid query: " + firstArg, ScriptHelper.WARNING_COLOR);
        }

        private static void SelectGroup(IEnumerable<string> arguments)
        {
            var botGroups = new List<string>();
            BotGroup botGroup;

            foreach (var query in arguments)
            {
                if (SharpHelper.TryParseEnum(query, out botGroup))
                {
                    botGroups.Add(SharpHelper.EnumToString(botGroup));
                }
                else
                {
                    ScriptHelper.PrintMessage("--BotExtended select--", ScriptHelper.ERROR_COLOR);
                    ScriptHelper.PrintMessage("Invalid query: " + query, ScriptHelper.WARNING_COLOR);
                    return;
                }
            }

            botGroups.Sort();
            BotHelper.Storage.SetItem(BotHelper.StorageKey("BOT_GROUPS"), botGroups.Distinct().ToArray());
            ScriptHelper.PrintMessage("[Botextended] Update successfully");
        }

        public static void DecoratePlayer(IEnumerable<string> arguments)
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
                        BotHelper.Decorate(player, botType);return;
                    }
                }
                else
                {
                    if (player.Name.ToLower() == playerArg)
                    {
                        BotHelper.Decorate(player, botType);return;
                    }
                }
            }
            ScriptHelper.PrintMessage("--BotExtended decorate--", ScriptHelper.ERROR_COLOR);
            ScriptHelper.PrintMessage("There is no player " + playerArg, ScriptHelper.WARNING_COLOR);
        }

        private static void PrintStatistics()
        {
            ScriptHelper.PrintMessage("--BotExtended statistics--", ScriptHelper.ERROR_COLOR);

            var botTypes = SharpHelper.EnumToList<BotType>();
            ScriptHelper.PrintMessage("-[BotType]: [WinCount] [TotalMatch] [SurvivalRate]", ScriptHelper.WARNING_COLOR);
            foreach (var botType in botTypes)
            {
                var botTypeKeyPrefix = BotHelper.StorageKey(botType);
                int winCount;
                var getWinCountAttempt = BotHelper.Storage.TryGetItemInt(botTypeKeyPrefix + "_WIN_COUNT", out winCount);
                int totalMatch;
                var getTotalMatchAttempt = BotHelper.Storage.TryGetItemInt(botTypeKeyPrefix + "_TOTAL_MATCH", out totalMatch);

                if (getWinCountAttempt && getTotalMatchAttempt)
                {
                    var survivalRate = (float)winCount / totalMatch;
                    var survivalRateStr = survivalRate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);

                    ScriptHelper.PrintMessage(SharpHelper.EnumToString(botType) + ": "
                        + " " + winCount + " " + totalMatch + " " + survivalRateStr);
                }
            }

            var botGroups = SharpHelper.EnumToList<BotGroup>();
            ScriptHelper.PrintMessage("-[BotGroup] [Index]: [WinCount] [TotalMatch] [SurvivalRate]", ScriptHelper.WARNING_COLOR);
            foreach (var botGroup in botGroups)
            {
                var groupSet = GetGroupSet(botGroup);
                for (var i = 0; i < groupSet.Groups.Count; i++)
                {
                    var groupKeyPrefix = BotHelper.StorageKey(botGroup, i);
                    int winCount;
                    var getWinCountAttempt = BotHelper.Storage.TryGetItemInt(groupKeyPrefix + "_WIN_COUNT", out winCount);
                    int totalMatch;
                    var getTotalMatchAttempt = BotHelper.Storage.TryGetItemInt(groupKeyPrefix + "_TOTAL_MATCH", out totalMatch);

                    if (getWinCountAttempt && getTotalMatchAttempt)
                    {
                        var survivalRate = (float)winCount / totalMatch;
                        var survivalRateStr = survivalRate.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);

                        ScriptHelper.PrintMessage(SharpHelper.EnumToString(botGroup) + " " + i + ": "
                            + " " + winCount + " " + totalMatch + " " + survivalRateStr);
                    }
                }
            }
        }

        private static void ClearStatistics()
        {
            var botTypes = SharpHelper.EnumToList<BotType>();
            foreach (var botType in botTypes)
            {
                var botTypeKeyPrefix = BotHelper.StorageKey(botType);

                BotHelper.Storage.RemoveItem(botTypeKeyPrefix + "_WIN_COUNT");
                BotHelper.Storage.RemoveItem(botTypeKeyPrefix + "_TOTAL_MATCH");
            }

            var botGroups = SharpHelper.EnumToList<BotGroup>();
            foreach (var botGroup in botGroups)
            {
                var groupSet = GetGroupSet(botGroup);
                for (var i = 0; i < groupSet.Groups.Count; i++)
                {
                    var groupKeyPrefix = BotHelper.StorageKey(botGroup, i);
                    BotHelper.Storage.RemoveItem(groupKeyPrefix + "_WIN_COUNT");
                    BotHelper.Storage.RemoveItem(groupKeyPrefix + "_TOTAL_MATCH");
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
