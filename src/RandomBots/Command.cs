using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RandomBots.Helpers;

namespace RandomBots
{
    public static class Command
    {
        public static readonly Color MESSAGE_COLOR = new Color(24, 238, 200);
        public static readonly Color ERROR_COLOR = new Color(244, 77, 77);
        public static readonly Color WARNING_COLOR = new Color(249, 191, 11);

        public static void OnUserMessage(UserMessageCallbackArgs args)
        {
            if (!args.User.IsHost || !args.IsCommand || (args.Command != "RANDOMBOTS" && args.Command != "RB"))
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
                case "settings":
                case "s":
                    PrintSettings(arguments);
                    break;
                case "setbots":
                case "sb":
                    SetBots(arguments);
                    break;
                case "clearbots":
                case "cb":
                    ClearBots(arguments);
                    break;
                case "random":
                case "r":
                    ToggleRandom(arguments);
                    break;
                case "allowonlybots":
                case "aob":
                    ToggleAllowOnlyBot(arguments);
                    break;
            }
        }

        private static void PrintSettings(IEnumerable<string> arguments)
        {
            var isRandom = Storage.GetIsRandom();
            var allowOnlyBots = Storage.GetAllowOnlyBots();

            Game.ShowChatMessage("--RandomBots Settings--", ERROR_COLOR);
            Game.ShowChatMessage("isRandom: " + isRandom, MESSAGE_COLOR);
            Game.ShowChatMessage("allowOnlyBots: " + allowOnlyBots, MESSAGE_COLOR);
        }

        private static void ToggleAllowOnlyBot(IEnumerable<string> arguments)
        {
            var value = Storage.GetAllowOnlyBots();
            var newValue = !value;
            Storage.SaveAllowOnlyBots(newValue);
            Game.ShowChatMessage("Set AllowOnlyBots to " + newValue, MESSAGE_COLOR);
        }

        private static void ToggleRandom(IEnumerable<string> arguments)
        {
            var isRandom = Storage.GetIsRandom();
            var newValue = !isRandom;
            Storage.SaveIsRandom(newValue);
            Game.ShowChatMessage("Set isRandom to " + newValue, MESSAGE_COLOR);
        }

        private static void PrintHelp()
        {
            Game.ShowChatMessage("--RandomBots Help--", ERROR_COLOR);
            Game.ShowChatMessage("/<RandomBots|rb> [Help|h|?]: Print this panel.", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBots|rb> [Settings|s]: Show script settings.", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBots|rb> [SetBots|sb] <Team> <AI-COUNT>: Set script bots to play with, e.g. /rb sb 0 expert-4.", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBots|rb> [ClearBots|cb]: Remove script bots.", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBots|rb> [Random|r]: Toggle randomized bot profiles every match on or off.", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBots|rb> [AllowOnlyBot|aob]: Toggle bot-exclusive gameover on or off.", MESSAGE_COLOR);
        }

        private static void SetBots(IEnumerable<string> arguments)
        {
            var team = arguments.First();
            arguments = arguments.Skip(1);

            var botsData = Storage.GetBotsData();
            foreach (var arg in arguments)
            {
                var parts = arg.Split('-');
                var count = int.Parse(parts[1]);

                for (var i = 0; i < count; i++)
                {
                    botsData.Add(new BotData
                    {
                        Name = RandomHelper.RandomizeName(),
                        Team = BotData.ParseTeam(team),
                        AI = BotData.ParseAI(parts[0]),
                        Profile = RandomHelper.RandomizeProfile(),
                    });
                }
            }

            foreach (var botData in botsData)
            {
                var spawners = ScriptHelper.GetPlayerSpawners();
                var p = Game.CreatePlayer(RandomHelper.GetRandomItem(spawners).GetWorldPosition());

                p.SetProfile(botData.Profile);
                p.SetBotName(botData.Name);
                p.SetTeam(botData.Team);
                p.SetBotBehaviorSet(BotBehaviorSet.GetBotBehaviorPredefinedSet(botData.AI));
                p.SetBotBehaviorActive(true);
            }

            Storage.SaveBotsData(botsData);
        }

        private static void ClearBots(IEnumerable<string> arguments)
        {
            Storage.SaveBotsData(new List<BotData>());
            Game.ShowChatMessage("Bots are cleared in the next match.", MESSAGE_COLOR);
        }
    }
}
