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
            ScriptHelper.CreateEarthquake(area, Owner);
        }
    }
}
