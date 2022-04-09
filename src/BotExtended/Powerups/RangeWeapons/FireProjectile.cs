using SFDGameScriptInterface;

namespace BotExtended.Powerups.RangeWeapons
{
    class FireProjectile : Projectile
    {
        public FireProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            Instance.PowerupFireActive = true;
            Instance.DamageDealtModifier = 0.01f;
        }
    }
}
