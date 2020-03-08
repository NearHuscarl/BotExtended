using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Bots
{
    public class KriegbärBot : Bot
    {
        public KriegbärBot(BotArgs args) : base(args) { }

        public override void OnSpawn(IEnumerable<Bot> others)
        {
            base.OnSpawn(others);

            var behavior = Player.GetBotBehaviorSet();
            behavior.RangedWeaponUsage = false;
            behavior.SearchForItems = false;
            behavior.GuardRange = 32;
            behavior.ChaseRange = 32;
            Player.SetBotBehaviorSet(behavior);

            var fritzliebe = others.FirstOrDefault(Q => Q.Type == BotType.Fritzliebe);
            if (fritzliebe.Player == null) return;

            Player.SetGuardTarget(fritzliebe.Player);
        }
    }
}
