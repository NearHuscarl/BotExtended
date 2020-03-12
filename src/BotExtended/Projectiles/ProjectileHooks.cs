using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    abstract class ProjectileHooks
    {
        // return null to skip creating powerup for that specific projectile.
        // Used to filter ProjectileItem for example
        public virtual IObject OnCustomProjectileCreated(IProjectile projectile)
        {
            return null;
        }
        public virtual IProjectile OnProjectileCreated(IProjectile projectile)
        {
            return projectile;
        }

        public virtual void OnCustomProjectileHit(IObject projectile) { }
        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args) { }
    }
}
