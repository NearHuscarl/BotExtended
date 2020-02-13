using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    public class ZombieFatBot : Bot
    {
        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);
            Game.TriggerExplosion(Player.GetWorldPosition());
        }
    }
}
