using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    abstract class ProjectileHooks
    {
        public abstract IObject OnProjectileCreated(IProjectile projectile);
        public virtual void OnProjectileHit(IObject projectile) { }
    }
}
