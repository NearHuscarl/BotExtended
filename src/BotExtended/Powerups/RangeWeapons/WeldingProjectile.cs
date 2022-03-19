using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class WeldingBullet : HoveringProjectile
    {
        public WeldingBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Welding)
        {
            throw new NotImplementedException();
        }
    }
}
