using System.Collections.Generic;

namespace BotExtended.Bots
{
    public class ZombieFlamerBot : Bot
    {
        public override void OnSpawn(List<Bot> bots)
        {
            Player.SetMaxFire();
        }
    }
}
