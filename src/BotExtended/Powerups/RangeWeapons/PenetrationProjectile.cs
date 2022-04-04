using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class PenetrationProjectile : Projectile
    {
        public PenetrationProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Penetration) { }

        protected override bool OnProjectileCreated()
        {
            Instance.PowerupBounceActive = true;
            _initialDir = Instance.Direction;
            return base.OnProjectileCreated();
        }

        private Vector2 _initialDir;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);
            if (Instance.BounceCount > 0)
                Instance.BounceCount = 0;
            // TODO: improve perf, hitting static tiles and play multiple effects seem to lag the game
            if (Instance.Direction != _initialDir)
                Instance.Direction = _initialDir;
        }
    }
}
