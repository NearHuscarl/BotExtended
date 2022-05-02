using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RandomBotProfiles.Helpers;

namespace RandomBotProfiles
{
    public static class Command
    {
        public static readonly Color MESSAGE_COLOR = new Color(24, 238, 200);
        public static readonly Color ERROR_COLOR = new Color(244, 77, 77);
        public static readonly Color WARNING_COLOR = new Color(249, 191, 11);

        public static void OnUserMessage(UserMessageCallbackArgs args)
        {
            if (!args.User.IsHost || !args.IsCommand || (args.Command != "RANDOMBOTPROFILES" && args.Command != "RBP"))
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

                case "setbots":
                case "sb":
                    SetBots(arguments);
                    break;

                case "clearbot":
                case "cb":
                    ClearBots(arguments);
                    break;

                case "togglerandom":
                case "tr":
                    ToggleRandom(arguments);
                    break;
            }
        }

        private static void ToggleRandom(IEnumerable<string> arguments)
        {
            var isRandom = ScriptHelper.GetIsRandom();
            var newValue = !isRandom;
            ScriptHelper.SaveIsRandom(newValue);
            Game.ShowChatMessage("Set isRandom to " + newValue, MESSAGE_COLOR);
        }

        private static void PrintHelp()
        {
            Game.ShowChatMessage("--RandomBotProfiles help--", ERROR_COLOR);
            Game.ShowChatMessage("/<RandomBotProfiles|rbp> [help|h|?]: Print this help", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBotProfiles|rbp> [SetBots|sb] <Team> <AI-COUNT>: Set bots to play with e.g. /rbp sb 1 expert-2 medium-1", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBotProfiles|rbp> [ClearBot|cb]: Remove all bots from the game (applied in the next round)", MESSAGE_COLOR);
            Game.ShowChatMessage("/<RandomBotProfiles|rbp> [ToggleRandom|tr]: Toggle randomizing bot profiles in every match", MESSAGE_COLOR);
        }

        private static void SetBots(IEnumerable<string> arguments)
        {
            var team = arguments.First();
            arguments = arguments.Skip(1);
            
            var botsData = new List<BotData>();
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

            ScriptHelper.SaveBotsData(botsData);
        }

        private static void ClearBots(IEnumerable<string> arguments)
        {
            ScriptHelper.SaveBotsData(new List<BotData>());
            Game.ShowChatMessage("Bots are cleared in the next match.", MESSAGE_COLOR);
        }
    }
}
