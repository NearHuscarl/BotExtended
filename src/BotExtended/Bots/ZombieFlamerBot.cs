using System.Collections.Generic;

namespace BotExtended.Bots
{
    public class ZombieFlamerBot : Bot
    {
        public ZombieFlamerBot(BotArgs args) : base(args) { }

        public override void OnSpawn()
        {
            base.OnSpawn();
            Player.SetMaxFire();
        }
    }
}
