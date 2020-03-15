using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class GravityGunHook : ProjectileHooks
    {
        public override IProjectile OnProjectileCreated(IProjectile projectile)
        {
            // Remove projectile completely since gravity gun only use objects laying around the map as ammunation
            projectile.FlagForRemoval();
            return null;
        }
    }
}
