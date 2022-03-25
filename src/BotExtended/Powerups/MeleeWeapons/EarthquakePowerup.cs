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
            return IsHitTheFloorWeapon(Name);
        }

        public EarthquakePowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.Earthquake) { }

        protected override void OnMeleeActionChanged(MeleeAction meleeAction, Vector2 hitPosition)
        {
            base.OnMeleeActionChanged(meleeAction, hitPosition);

            if (Owner.IsDead || meleeAction != MeleeAction.Three) return;

            var area = ScriptHelper.GrowFromCenter(hitPosition, 140, 50);
            CreateEarthquake(area, Owner);
        }

        public static void CreateEarthquake(Area area, IPlayer owner = null)
        {
            var width = area.Width;
            var center = area.Center;
            var objects = Game.GetObjectsByArea(area);

            Game.PlayEffect(EffectName.CameraShaker, center, 6f, 200f, false);
            Game.PlaySound("Break", center, 150);

            foreach (var o in objects)
            {
                if (owner != null && o.UniqueID == owner.UniqueID) continue;

                // stupid lamps can't be removed once destroyed
                if (o.Name == "Lamp00")
                {
                    o.Remove();
                    continue;
                }
                if (ScriptHelper.IsDynamicObject(o) || ScriptHelper.IsPlayer(o))
                {
                    if (ScriptHelper.IsPlayer(o)) ScriptHelper.Fall((IPlayer)o);

                    var distance = Vector2.Distance(center, o.GetWorldPosition());
                    var upVec = MathHelper.Lerp(12, 3, distance / (width / 2));
                    o.SetLinearVelocity(new Vector2(RandomHelper.Between(-2, 2), upVec));
                    o.SetAngularVelocity(RandomHelper.Between(-6, 6));
                    o.DealDamage(1f);
                }
            }
        }
    }
}
