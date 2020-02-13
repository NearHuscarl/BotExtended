using SFDGameScriptInterface;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    public class IncineratorBot : Bot
    {
        public override void OnSpawn(List<Bot> bots)
        {
            var behavior = Player.GetBotBehaviorSet();
            behavior.SearchForItems = false;
            behavior.RangedWeaponPrecisionInterpolateTime = 0f;
            Player.SetBotBehaviorSet(behavior);
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            var playerPosition = Player.GetWorldPosition();

            if (Player.CurrentPrimaryWeapon.WeaponItem == WeaponItem.FLAMETHROWER)
            {
                Game.TriggerExplosion(playerPosition);
                Game.SpawnFireNodes(playerPosition, 20, 5f, FireNodeType.Flamethrower);
                Game.TriggerFireplosion(playerPosition, 60f);
            }
        }
    }
}
