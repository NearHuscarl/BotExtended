using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class FireProjectile : Projectile
    {
        public FireProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Fire)
        {
            Instance.PowerupFireActive = true;
            Instance.DamageDealtModifier = 0.01f;
        }
    }
}
