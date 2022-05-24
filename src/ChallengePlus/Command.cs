using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public enum CommandPermission { Public, Admin }

    public static class Command
    {
        private static readonly string NAME = Constants.SCRIPT_NAME;

        public static void Execute(Action command, CommandPermission permission, UserMessageCallbackArgs args)
        {
            if (permission == CommandPermission.Admin && !args.User.IsHost)
            {
                ScriptHelper.PrintMessage(string.Format("/whisper {0} [{1}] Only host can run this command.", args.User.GameSlotIndex, NAME), ScriptColors.WARNING_COLOR);
                return;
            }
            command();
        }

        public static void OnUserMessage(UserMessageCallbackArgs args)
        {
            if (!args.IsCommand || (args.Command != NAME.ToUpperInvariant() && args.Command != "CP"))
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
                    Execute(PrintHelp, CommandPermission.Public, args);
                    break;

                case "v":
                case "version":
                    Execute(PrintVersion, CommandPermission.Public, args);
                    break;

                case "s":
                case "settings":
                    Execute(ShowCurrentSettings, CommandPermission.Public, args);
                    break;

                case "lc":
                case "listchallenges":
                    Execute(PrintChallenges, CommandPermission.Public, args);
                    break;

                case "ec":
                case "enabledchallenges":
                    Execute(() => SetEnabledChallenges(arguments), CommandPermission.Admin, args);
                    break;

                case "ri":
                case "rotationinterval":
                    Execute(() => SetChallengeRotationInterval(arguments), CommandPermission.Admin, args);
                    break;

                case "nc":
                case "nextchallenge":
                    Execute(SkipCurrentChallenge, CommandPermission.Admin, args);
                    break;

                default:
                    ScriptHelper.PrintMessage("Invalid command: " + command, ScriptColors.ERROR_COLOR);
                    break;
            }
        }

        private static void PrintHelp()
        {
            var command = NAME.ToLowerInvariant();

            ScriptHelper.PrintMessage(string.Format("--{0} help--", NAME), ScriptColors.ERROR_COLOR);
            ScriptHelper.PrintMessage(string.Format("/<{0}|cp> [help|h|?]: Print this help", command));
            ScriptHelper.PrintMessage(string.Format("/<{0}|cp> [version|v]: Print the current version", command));
            ScriptHelper.PrintMessage(string.Format("/<{0}|cp> [listchallenges|lc]: List all challenges", command));
            ScriptHelper.PrintMessage(string.Format("/<{0}|cp> [settings|s]: Display the current script's settings", command));
            ScriptHelper.PrintMessage(string.Format("/<{0}|cp> [enabledchallenges|ec] [-e] <names|indexes|all>: Set enabled challenges to play with", command));
            ScriptHelper.PrintMessage(string.Format("/<{0}|cp> [rotationinterval|ri] <1-10>: Set challenge rotation interval for every n rounds", command));
            ScriptHelper.PrintMessage(string.Format("/<{0}|cp> [nextchallenge|nc]: Reset the rotation interval and skip to the next challenge", command));
        }

        private static void PrintVersion()
        {
            ScriptHelper.PrintMessage(string.Format("--{0} version--", NAME), ScriptColors.ERROR_COLOR);
            ScriptHelper.PrintMessage("v" + Constants.CURRENT_VERSION);
        }

        private static IEnumerable<string> GetChallenges()
        {
            var challenges = ChallengeManager.GetChallengeNames();

            foreach (var challenge in challenges)
            {
                yield return ((int)challenge).ToString() + ": " + ScriptHelper.EnumToString(challenge);
            }
        }

        private static void PrintChallenges()
        {
            ScriptHelper.PrintMessage(string.Format("--{0} list challenges--", NAME), ScriptColors.ERROR_COLOR);

            foreach (var challenge in GetChallenges())
            {
                ScriptHelper.PrintMessage(challenge, ScriptColors.WARNING_COLOR);
            }
        }

        private static void ShowCurrentSettings()
        {
            ScriptHelper.PrintMessage(string.Format("--{0} settings--", NAME), ScriptColors.ERROR_COLOR);

            var settings = Settings.Get();

            var challengeNames = settings.EnabledChallenges;
            var currentChallenge = settings.CurrentChallenge;
            var isAllChallenges = challengeNames.Count == ChallengeManager.GetChallengeNames().Count;
            var challengesValue = isAllChallenges ? "All" : string.Join(",", challengeNames);

            ScriptHelper.PrintMessage(string.Format("-Current Challenge: {0}", currentChallenge));
            ScriptHelper.PrintMessage(string.Format("-Enabled Challenges: {0}", challengesValue));

            var rotationInterval = settings.RotationEnabled ? settings.RotationInterval.ToString() : "Disabled";
            var roundsUntilRotation = settings.RotationEnabled ? settings.RoundsUntilRotation.ToString() : "N/a";

            ScriptHelper.PrintMessage("-Challenge rotation interval: " + rotationInterval, ScriptColors.WARNING_COLOR);
            ScriptHelper.PrintMessage("-Rounds until rotation: " + roundsUntilRotation, ScriptColors.WARNING_COLOR);
        }

        private static void SetEnabledChallenges(IEnumerable<string> arguments)
        {
            var allChallenges = ChallengeManager.GetChallengeNames().Select(ScriptHelper.EnumToString).ToList();
            var challenges = new List<string>();
            var excludeFlag = false;
            ChallengeName challenge;

            if (arguments.Count() == 0)
            {
                ScriptHelper.PrintMessage(string.Format("--{0} SetEnabledChallenges--", NAME), ScriptColors.ERROR_COLOR);
                ScriptHelper.PrintMessage("Invalid command: Argument is empty", ScriptColors.WARNING_COLOR);
                return;
            }

            if (arguments.Count() == 1 && (arguments.Single() == "all" || arguments.Single() == "none"))
            {
                if (arguments.Single() == "all")
                    challenges = new List<string> { "All" };
                if (arguments.Single() == "none")
                    challenges = new List<string> { "None" };
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
                        ScriptHelper.PrintMessage(string.Format("--{0} SetEnabledChallenges--", NAME), ScriptColors.ERROR_COLOR);
                        ScriptHelper.PrintMessage("Invalid argument: Cannot mix None with other options", ScriptColors.WARNING_COLOR);
                        return;
                    }

                    if (ScriptHelper.TryParseEnum(arg, out challenge))
                    {
                        challenges.Add(ScriptHelper.EnumToString(challenge));
                    }
                    else
                    {
                        ScriptHelper.PrintMessage(string.Format("--{0} SetEnabledChallenges--", NAME), ScriptColors.ERROR_COLOR);
                        ScriptHelper.PrintMessage("Invalid argument: " + arg, ScriptColors.WARNING_COLOR);
                        return;
                    }
                }
            }

            if (excludeFlag)
            {
                challenges = allChallenges.Where((f) => !challenges.Contains(f)).ToList();
            }

            Storage.SetItem("ENABLED_CHALLENGES", challenges.Distinct().ToArray());
            ScriptHelper.PrintMessage(string.Format("[{0}] Update successfully", NAME));
        }

        private static void SetChallengeRotationInterval(IEnumerable<string> arguments)
        {
            var firstArg = arguments.FirstOrDefault();
            if (firstArg == null) return;
            int value = -1;

            if (int.TryParse(firstArg, out value))
            {
                value = (int)MathHelper.Clamp(value, 0, 10);
                Storage.SetItem("ROTATION_INTERVAL", value);
                Storage.SetItem("ROUNDS_UNTIL_ROTATION", value);
                ScriptHelper.PrintMessage(string.Format("[{0}] Update successfully", NAME));
            }
            else
                ScriptHelper.PrintMessage(string.Format("[{0}] Invalid query: {1}", NAME, firstArg), ScriptColors.WARNING_COLOR);
        }

        private static void SkipCurrentChallenge()
        {
            Storage.SetItem("ROUNDS_UNTIL_ROTATION", 1);
            ScriptHelper.PrintMessage(string.Format("[{0}] Update successfully", NAME));
        }
    }
}
