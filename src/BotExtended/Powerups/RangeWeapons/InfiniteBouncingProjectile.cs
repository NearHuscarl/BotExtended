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
    class InfiniteBouncingProjectile : Projectile
    {
        public InfiniteBouncingProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.InfiniteBouncing)
        {
            _isElapsedUpdate = ScriptHelper.WithIsElapsed(36);
            Instance.PowerupBounceActive = true;
        }

        private Func<bool> _isElapsedUpdate;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);
            if (_isElapsedUpdate() && Instance.BounceCount > 0)
                Instance.BounceCount = 0;
        }
    }
}
