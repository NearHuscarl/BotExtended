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
    class HurlingPowerup : MeleeWpn
    {
        public HurlingPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.Hurling) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0) return;

            var criticalAttack = CurrentMeleeAction == MeleeAction.Three
            || CurrentMeleeAction == MeleeAction.Two && RandomHelper.Percentage(.4f)
            || CurrentMeleeAction == MeleeAction.One && RandomHelper.Percentage(.2f);

            if (!criticalAttack) return;

            var dir = Owner.GetFaceDirection();
            var min = dir == 1 ? Owner.GetAABB().TopRight : Owner.GetAABB().TopLeft;
            var max = dir == 1 ? Owner.GetAABB().BottomRight : Owner.GetAABB().BottomLeft;
            min += Vector2.UnitY * 5;
            max += dir * Vector2.UnitX * 45;
            var playersInRange = Game.GetObjectsByArea<IPlayer>(ScriptHelper.Area(min, max));

            foreach (var p in playersInRange)
            {
                if (p.UniqueID == Owner.UniqueID) continue;
                Game.PlayEffect(EffectName.Smack, p.GetWorldPosition());
                p.SetLinearVelocity(new Vector2(5 * dir, RandomHelper.Between(4, 9)));
                ScriptHelper.ExecuteSingleCommand(p, PlayerCommandType.Stagger);
            }

            foreach (var arg in args)
            {
                if (arg.IsPlayer)
                {
                    var enemy = BotManager.GetBot(arg.ObjectID);

                    foreach (var weapon in Constants.WeaponItemTypes)
                    {
                        var weaponObj = enemy.Player.Disarm(weapon);
                        if (weaponObj != null)
                        {
                            Owner.GiveWeaponItem(weaponObj.RangedWeapon.WeaponItem);
                            weaponObj.Remove();
                            break;
                        }
                    }
                }
            }
        }
    }
}
