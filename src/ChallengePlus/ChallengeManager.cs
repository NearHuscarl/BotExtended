using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public static class ChallengeManager
    {
        private static Challenge _challenge;

        public static void Initialize()
        {
            var name = GetCurrentChallenge();
            if (name == ChallengeName.None) return;
            _challenge = ChallengeFactory.Create(name);

            Events.UserMessageCallback.Start(Command.OnUserMessage);
            Events.UpdateCallback.Start(_challenge.Update);
            Events.PlayerDeathCallback.Start(_challenge.OnPlayerDealth);
            
            _challenge.OnSpawn(Game.GetPlayers());
        }

        private static ChallengeName GetCurrentChallenge()
        {
            var settings = Settings.Get();
            var currentChallenge = settings.CurrentChallenge;

            if (settings.RoundsUntilRotation == 1)
            {
                List<ChallengeName> challenges;

                if (settings.EnabledChallenges.Count > 1)
                    challenges = settings.EnabledChallenges.Where((f) => f != settings.CurrentChallenge).ToList();
                else
                    challenges = settings.EnabledChallenges;

                currentChallenge = RandomHelper.GetItem(challenges);
                ScriptHelper.PrintMessage("Change challenge to " + currentChallenge);
            }
            Storage.SetItem("CURRENT_CHALLENGE", currentChallenge.ToString());

            if (settings.RotationEnabled)
            {
                var roundTillNextRotation = settings.RoundsUntilRotation == 1 ?
                    settings.RotationInterval
                    :
                    settings.RoundsUntilRotation - 1;
                Storage.SetItem("ROUNDS_UNTIL_ROTATION", roundTillNextRotation);
            }

            return currentChallenge;
        }
    }
}
