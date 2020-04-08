using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class FatigueProjectile : Projectile
    {
        public FatigueProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Fatigue)
        {
        }
    }
}
