using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class InfiniteAmmoChallenge : Challenge
    {
        public InfiniteAmmoChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Guns never run out of ammo."; }
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            var p = player.Instance;

            if (p.CurrentPrimaryWeapon.CurrentAmmo < p.CurrentPrimaryWeapon.MaxTotalAmmo)
                p.SetCurrentPrimaryWeaponAmmo(p.CurrentPrimaryWeapon.MaxTotalAmmo);
            if (p.CurrentSecondaryWeapon.CurrentAmmo < p.CurrentSecondaryWeapon.MaxTotalAmmo)
                p.SetCurrentSecondaryWeaponAmmo(p.CurrentSecondaryWeapon.MaxTotalAmmo);
        }
    }
}
