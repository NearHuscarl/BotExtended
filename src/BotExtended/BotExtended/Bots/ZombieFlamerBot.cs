using System.Collections.Generic;

namespace SFDScript.BotExtended.Bots
{
    public class ZombieFlamerBot : Bot
    {
        public override void OnSpawn(List<Bot> bots)
        {
            Player.SetMaxFire();
        }
    }
}
