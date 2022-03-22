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
    class EarthquakePowerup : MeleeWpn
    {
        public override bool IsValidPowerup()
        {
            // weapons that have the beat-the-ground animation on third attack
            return Name == WeaponItem.MACHETE
                || Name == WeaponItem.AXE
                || Name == WeaponItem.BAT
                || Name == WeaponItem.BATON
                || Name == WeaponItem.SHOCK_BATON
                || Name == WeaponItem.PIPE
                || Name == WeaponItem.HAMMER
                || Name == WeaponItem.LEAD_PIPE
                || Name == WeaponItem.KATANA;
        }

        public EarthquakePowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.Earthquake) { }

        protected override void OnMeleeActionChanged(MeleeAction meleeAction)
        {
            base.OnMeleeActionChanged(meleeAction);

            if (Owner.IsDead || meleeAction != MeleeAction.Three) return;

            var position = Owner.GetWorldPosition();
            var width = 140;
            var area = ScriptHelper.GrowFromCenter(position, width, 50);
            var objects = Game.GetObjectsByArea(area);

            Game.PlayEffect(EffectName.CameraShaker, position, 6f, 200f, false);
            Game.PlaySound("Break", position, 150);

            foreach (var o in objects)
            {
                if (o.UniqueID == Owner.UniqueID) continue;
                if (ScriptHelper.IsDynamicObject(o) || ScriptHelper.IsPlayer(o))
                {
                    if (ScriptHelper.IsPlayer(o)) ScriptHelper.Fall((IPlayer)o);

                    var distance = Vector2.Distance(position, o.GetWorldPosition());
                    var upVec = MathHelper.Lerp(12, 3, distance / (width / 2));
                    o.SetLinearVelocity(new Vector2(RandomHelper.Between(-2, 2), upVec));
                    o.SetAngularVelocity(RandomHelper.Between(-6, 6));
                    o.DealDamage(1f);
                }
            }
        }
    }
}
