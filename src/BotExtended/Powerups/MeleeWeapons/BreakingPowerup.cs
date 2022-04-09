using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.MeleeWeapons
{
    class BreakingPowerup : MeleeWpn
    {
        public BreakingPowerup(IPlayer owner, WeaponItem name, MeleeWeaponPowerup powerup) : base(owner, name, powerup) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three) return;

            foreach (var arg in args)
            {
                if (!arg.IsPlayer) continue;
             
                var enemy = BotManager.GetBot(arg.ObjectID);
                if (enemy.Info.IsBoss || ScriptHelper.SameTeam(enemy.Player, Owner)) continue;

                foreach (var weapon in Constants.WeaponItemTypes)
                {
                    var weaponObj = enemy.Player.Disarm(weapon);
                    if (weaponObj != null)
                    {
                        weaponObj.Destroy();
                        break;
                    }
                }
            }
        }
    }
}
