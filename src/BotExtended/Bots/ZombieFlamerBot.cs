using System.Collections.Generic;

namespace BotExtended.Bots
{
    public class ZombieFlamerBot : Bot
    {
        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            base.OnSpawn(bots);
            Player.SetMaxFire();
        }
    }
}
