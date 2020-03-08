using System.Collections.Generic;

namespace BotExtended.Bots
{
    public class ZombieFlamerBot : Bot
    {
        public ZombieFlamerBot(BotArgs args) : base(args) { }

        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            base.OnSpawn(bots);
            Player.SetMaxFire();
        }
    }
}
