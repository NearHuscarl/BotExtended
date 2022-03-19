using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;
using BotExtended.Factions;

namespace BotExtended.Bots
{
    public class JoBot : Bot
    {
        public JoBot(BotArgs args) : base(args) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Player.IsDead || args.Length == 0) return;

            // Need to wait for a bit because this event is fired a bit early. https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&p=24824&sid=80ecb190dfe9c7febc1f3ede990a83c6#p24824
            ScriptHelper.Timeout(() =>
            {
                var criticalAttack = CurrentMeleeAction == MeleeAction.Three
                || CurrentMeleeAction == MeleeAction.Two && RandomHelper.Percentage(.4f)
                || CurrentMeleeAction == MeleeAction.One && RandomHelper.Percentage(.2f);

                if (!criticalAttack) return;
                
                var dir = Player.GetFaceDirection();
                var min = dir == 1 ? Player.GetAABB().TopRight : Player.GetAABB().TopLeft;
                var max = dir == 1 ? Player.GetAABB().BottomRight : Player.GetAABB().BottomLeft;
                min += Vector2.UnitY * 5;
                max += dir * Vector2.UnitX * 45;
                var playersInRange = Game.GetObjectsByArea<IPlayer>(ScriptHelper.Area(min, max));

                foreach (var p in playersInRange)
                {
                    if (p.UniqueID == Player.UniqueID) continue;
                    Game.PlayEffect(EffectName.Smack, p.GetWorldPosition());
                    p.SetLinearVelocity(new Vector2(5 * dir, RandomHelper.Between(4, 9)));
                    ScriptHelper.ExecuteSingleCommand(p, PlayerCommandType.Stagger);
                }
            }, 0);

            foreach (var arg in args)
            {
                if (arg.IsPlayer)
                {
                    var enemy = BotManager.GetBot(arg.ObjectID);

                    foreach (var weapon in new WeaponItemType[] { WeaponItemType.Melee, WeaponItemType.Handgun, WeaponItemType.Thrown, WeaponItemType.Powerup })
                    {
                        var weaponObj = enemy.Player.Disarm(weapon);
                        if (weaponObj != null)
                        {
                            Player.GiveWeaponItem(weaponObj.RangedWeapon.WeaponItem);
                            weaponObj.Remove();
                            break;
                        }
                    }
                }
            }
        }
    }
}
