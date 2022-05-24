using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;

namespace ChallengePlus
{
    class Settings
    {
        public readonly int RotationInterval;
        public bool RotationEnabled { get { return RotationInterval != 0; } }
        public readonly int RoundsUntilRotation;
        public readonly List<ChallengeName> EnabledChallenges;
        public readonly ChallengeName CurrentChallenge;

        public Settings(
            int rotationInterval,
            int roundsUntilRotation,
            List<ChallengeName> enabledChallenges,
            ChallengeName currentChallenge
            )
        {
            RotationInterval = rotationInterval;
            RoundsUntilRotation = roundsUntilRotation;
            EnabledChallenges = enabledChallenges;
            CurrentChallenge = currentChallenge;
        }

        public static Settings Get()
        {
            var rotationIntervalResult = Storage.GetInt("ROTATION_INTERVAL");
            if (!rotationIntervalResult.Success)
            {
                rotationIntervalResult.Data = Constants.DEFAULT_CHALLENGE_ROTATION_INTERVAL;
                Storage.SetItem("ROTATION_INTERVAL", Constants.DEFAULT_CHALLENGE_ROTATION_INTERVAL);
            }

            var roundsUntilRotationResult = Storage.GetInt("ROUNDS_UNTIL_ROTATION");
            if (!roundsUntilRotationResult.Success)
            {
                roundsUntilRotationResult.Data = 1;
                Storage.SetItem("ROUNDS_UNTIL_ROTATION", roundsUntilRotationResult.Data);
            }

            var currentChallengeResult = Storage.GetString("CURRENT_CHALLENGE");
            if (!currentChallengeResult.Success)
            {
                currentChallengeResult.Data = "None";
                Storage.SetItem("CURRENT_CHALLENGE", currentChallengeResult.Data);
            }
            var currentChallenge = ScriptHelper.StringToEnum<ChallengeName>(currentChallengeResult.Data);

            var enabledChallengesResult = Storage.GetStringArr("ENABLED_CHALLENGES");
            if (!enabledChallengesResult.Success)
            {
                enabledChallengesResult.Data = Constants.DEFAULT_ENABLED_CHALLENGES;
                Storage.SetItem("ENABLED_CHALLENGES", Constants.DEFAULT_ENABLED_CHALLENGES);
            }

            var enabledChallenges = new List<ChallengeName>();
            if (enabledChallengesResult.Data.Count() == 1 && enabledChallengesResult.Data.Single() == "All")
            {
                enabledChallenges = ChallengeManager.GetChallengeNames();
            }
            else
            {
                foreach (var c in enabledChallengesResult.Data)
                {
                    enabledChallenges.Add(ScriptHelper.StringToEnum<ChallengeName>(c));
                }
            }

            return new Settings(
                rotationIntervalResult.Data,
                roundsUntilRotationResult.Data,
                enabledChallenges,
                currentChallenge
            );
        }
    }
}
