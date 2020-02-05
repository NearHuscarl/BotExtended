using System.Collections.Generic;

namespace SFDScript.BotExtended.Bots
{
    public class AssassinBot : Bot
    {
        public override void OnSpawn(List<Bot> bots)
        {
            var behavior = Player.GetBotBehaviorSet();
            behavior.OffensiveClimbingLevel = 0.9f;
            behavior.OffensiveSprintLevel = 0.9f;
            Player.SetBotBehaviorSet(behavior);
        }
    }
}
