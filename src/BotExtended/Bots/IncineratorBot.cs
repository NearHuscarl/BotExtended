using SFDGameScriptInterface;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    public class IncineratorBot : Bot
    {
        public IncineratorBot(BotArgs args) : base(args) { }

        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            base.OnSpawn(bots);

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
