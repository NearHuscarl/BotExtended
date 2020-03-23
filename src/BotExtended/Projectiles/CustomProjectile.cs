using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class CustomProjectile : ProjectileBase
    {
        public IObject Instance { get; private set; }
        public override int ID { get { return Instance.UniqueID; } }
        public override bool IsRemoved { get { return Instance == null ? true : Instance.IsRemoved; } }

        public CustomProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(powerup)
        {
            Instance = OnProjectileCreated(projectile);
            if (Instance == null) Powerup = RangedWeaponPowerup.None;

            IsCustomProjectile = true;
        }

        protected virtual IObject OnProjectileCreated(IProjectile projectile) { return null; }
    }
}
