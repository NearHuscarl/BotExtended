using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class KnockbackProjectile : Projectile
    {
        public KnockbackProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override void OnProjectileCreated()
        {
            Instance.CritChanceDealtModifier = 100f;
        }
    }
}
