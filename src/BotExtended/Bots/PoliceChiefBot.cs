using BotExtended.Library;
using BotExtended.Powerups;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Bots
{
    class PoliceChiefBot : Bot
    {
        public PoliceChiefBot(BotArgs args) : base(args)
        {
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            foreach (var bot in BotManager.GetBots())
            {
                if (bot.Type == BotType.Police)
                {
                    var player = bot.Player;
                    var weaponItem = player.CurrentPrimaryWeapon.WeaponItem;

                    if (player.CurrentPrimaryWeapon.WeaponItem != WeaponItem.NONE)
                        ProjectileManager.SetPowerup(player, weaponItem, RangedWeaponPowerup.Fatigue);
                    weaponItem = player.CurrentSecondaryWeapon.WeaponItem;
                    if (player.CurrentSecondaryWeapon.WeaponItem != WeaponItem.NONE)
                        ProjectileManager.SetPowerup(player, weaponItem, RangedWeaponPowerup.Fatigue);
                }
            }
        }
    }
}
