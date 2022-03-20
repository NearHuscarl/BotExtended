using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using BotExtended.Powerups;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class SmokerBot : Bot
    {
        private float m_smokeDelay = -25000;

        public SmokerBot(BotArgs args) : base(args)
        {
        }

        public override void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            base.OnDroppedWeapon(arg);

            if (arg.WeaponItemType == WeaponItemType.Rifle)
            {
                PowerupManager.SetPowerup(Player, WeaponItem.GRENADE_LAUNCHER, RangedWeaponPowerup.Smoke);
            }
        }
    }
}
