using SFDGameScriptInterface;

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

            ChallengeManager.Initialize();
        }

        public void OnShutdown()
        {
        }
    }
}