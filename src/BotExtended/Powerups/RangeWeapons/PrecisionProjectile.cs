using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class PrecisionProjectile : Projectile
    {
        public PrecisionProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override void OnProjectileCreated()
        {
            var owner = Game.GetPlayer(InitialOwnerPlayerID);
            Vector2 position, direction;

            if (owner == null || !owner.GetWeaponMuzzleInfo(out position, out direction)) return;

            Instance.Direction = direction;
        }
    }
}
