using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class Projectile : ProjectileBase
    {
        public IProjectile Instance { get; private set; }
        public override int ID { get { return Instance.InstanceID; } }
        public override bool IsRemoved { get { return Instance == null ? true : Instance.IsRemoved; } }

        public Projectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(powerup)
        {
            Instance = OnProjectileCreated(projectile);
            if (Instance == null) Powerup = RangedWeaponPowerup.None;

            IsCustomProjectile = false;
        }

        protected virtual IProjectile OnProjectileCreated(IProjectile projectile) { return projectile; }
    }
}
