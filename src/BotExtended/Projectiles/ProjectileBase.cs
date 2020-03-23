using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    abstract class ProjectileBase
    {
        public abstract int ID { get; }
        public abstract bool IsRemoved { get; }
        public RangedWeaponPowerup Powerup { get; protected set; }
        public bool IsCustomProjectile { get; protected set; }

        public ProjectileBase(RangedWeaponPowerup powerup)
        {
            Powerup = powerup;
        }

        public virtual void Update(float elapsed) { }

        public virtual void OnProjectileHit() { }
        public virtual void OnProjectileHit(ProjectileHitArgs args) { }
    }
}
