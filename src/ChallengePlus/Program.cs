using SFDGameScriptInterface;
using System;

namespace ChallengePlus
{
    public partial class Program : GameScriptInterface
    {
        /// <summary>
        /// Placeholder constructor that's not to be included in the ScriptWindow!
        /// </summary>
        public Program() : base(null) { }

        public void OnStartup()
        {
            // invoke the static contructor to create the null instance IPlayer
            var _ = Player.None;
        }

        public void AfterStartup()
        {
            // Initialize in AfterStartup instead of in OnStartup because we need to wait until the null IPlayer instance is removed from the world.
            // otherwise, IPlayer.IsRemoved is not updated yet after calling Remove() and Game.GetPlayers() still returns the null IPlayer
            Initialize();
        }

        private static void Initialize()
        {
            if (Game.IsEditorTest)
            {
                for (var i = 0; i < 5; i++)
                {
                    var bot = ScriptHelper.SpawnBot(BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotB));
                    if (bot == null) continue;
                    if (i < 3) bot.SetTeam(PlayerTeam.Team2);
                    else bot.SetTeam(PlayerTeam.Team3);
                }
            }

            Storage.Initialize();
            PlayerManager.Initialize();
            ChallengeManager.Initialize();
        }

        public void OnShutdown()
        {
        }
    }
}