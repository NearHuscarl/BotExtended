using SFDGameScriptInterface;
using static SFDScript.Library.Mocks.MockObjects;

namespace SFDScript.BotExtended.Bots
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
