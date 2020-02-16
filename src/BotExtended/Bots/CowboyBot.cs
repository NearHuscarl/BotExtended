using System.Collections.Generic;

namespace BotExtended.Bots
{
    public class CowboyBot : Bot
    {
        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            // The fastest fighters in sfd universe
            var behavior = Player.GetBotBehaviorSet();
            behavior.RangedWeaponAimShootDelayMin = 0;
            behavior.RangedWeaponAimShootDelayMax = 50;
            behavior.RangedWeaponHipFireAimShootDelayMin = 0;
            behavior.RangedWeaponHipFireAimShootDelayMax = 25;
            behavior.RangedWeaponPrecisionInterpolateTime = 50;
            Player.SetBotBehaviorSet(behavior);
        }
    }
}
