﻿using System.Collections.Generic;

namespace BotExtended.Bots
{
    public class AssassinBot : Bot
    {
        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            var behavior = Player.GetBotBehaviorSet();
            behavior.OffensiveClimbingLevel = 0.9f;
            behavior.OffensiveSprintLevel = 0.9f;
            Player.SetBotBehaviorSet(behavior);
        }
    }
}
