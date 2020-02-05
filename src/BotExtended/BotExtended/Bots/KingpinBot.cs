using System.Collections.Generic;
using System.Linq;

namespace SFDScript.BotExtended.Bots
{
    public class KingpinBot : Bot
    {
        public override void OnSpawn(List<Bot> others)
        {
            var bodyguards = others.Where(Q => Q.Type == BotType.Bodyguard || Q.Type == BotType.GangsterHulk).Take(2);
            var bodyguardMaxCount = 2;
            var bodyguardCount = bodyguards.Count();
            var bodyguardMissing = bodyguardMaxCount - bodyguardCount;
            if (bodyguardCount < bodyguardMaxCount)
                bodyguards.Concat(others.Where(Q => Q.Type == BotType.Bodyguard2).Take(bodyguardMissing));

            foreach (var bodyguard in bodyguards)
            {
                bodyguard.Player.SetGuardTarget(Player);
            }
        }
    }
}
