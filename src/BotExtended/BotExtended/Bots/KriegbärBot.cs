using System.Collections.Generic;

namespace SFDScript.BotExtended.Bots
{
    public class KriegbärBot : Bot
    {
        public override void OnSpawn(List<Bot> others)
        {
            var behavior = Player.GetBotBehaviorSet();
            behavior.RangedWeaponUsage = false;
            behavior.SearchForItems = false;
            behavior.GuardRange = 32;
            behavior.ChaseRange = 32;
            Player.SetBotBehaviorSet(behavior);

            var fritzliebe = others.Find(Q => Q.Type == BotType.Fritzliebe);
            if (fritzliebe.Player == null) return;

            Player.SetGuardTarget(fritzliebe.Player);
        }
    }
}
