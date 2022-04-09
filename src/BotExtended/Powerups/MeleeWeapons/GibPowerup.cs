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
    class GibPowerup : MeleeWpn
    {
        public GibPowerup(IPlayer owner, WeaponItem name, MeleeWeaponPowerup powerup) : base(owner, name, powerup) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three) return;

            foreach (var arg in args)
            {
                if (!arg.IsPlayer) continue;
             
                var enemy = BotManager.GetBot(arg.ObjectID);
                if (enemy.Info.IsBoss || ScriptHelper.SameTeam(enemy.Player, Owner)) continue;

                if (RandomHelper.Percentage(Game.IsEditorTest ? 1 : .1f) || enemy.Player.IsDead)
                {
                    var ownerDir = Owner.GetFaceDirection();
                    var pBox = enemy.Player.GetAABB();

                    Owner.SetHealth(Owner.GetHealth() + 10);
                    // If you kill here, this callback will be invoked again and can cause unexpected result.
                    ScriptHelper.Timeout(() =>
                    {
                        enemy.Player.Gib();
                        ScriptHelper.Timeout(() =>
                        {
                            foreach (var o in Game.GetObjectsByArea(pBox))
                            {
                                if (o.Name.StartsWith("Giblet"))
                                {
                                    var dir = RandomHelper.Direction(10, 180 - 10);
                                    o.SetLinearVelocity(dir * RandomHelper.Between(10, 20));
                                    o.SetAngularVelocity(RandomHelper.Between(-20, 20));
                                }
                            }
                        }, 17);
                    }, 0);
                    break;
                }
            }
        }
    }
}
