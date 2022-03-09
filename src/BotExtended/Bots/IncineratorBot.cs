using SFDGameScriptInterface;
using System.Collections.Generic;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class IncineratorBot : PyromaniacBot
    {
        public IncineratorBot(BotArgs args) : base(args)
        {
            BoostOnBurnLevel = 2;
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (Player.CurrentPrimaryWeapon.WeaponItem == WeaponItem.GRENADE_LAUNCHER)
            {
                Game.SpawnFireNodes(Position, 20, 5f, FireNodeType.Flamethrower);
                Game.TriggerFireplosion(Position, 60f);
                Game.TriggerExplosion(Position);
            }
        }
    }
}
