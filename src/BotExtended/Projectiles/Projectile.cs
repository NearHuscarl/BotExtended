using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class Projectile : ProjectileBase
    {
        public IProjectile Instance { get; private set; }
        public override int ID { get { return Instance.InstanceID; } }
        public override bool IsRemoved { get { return Instance == null ? true : Instance.IsRemoved; } }

        public Projectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            Instance = OnProjectileCreated(projectile);
            if (Instance == null) Powerup = RangedWeaponPowerup.None;

            IsCustomProjectile = false;
        }

        protected virtual IProjectile OnProjectileCreated(IProjectile projectile) { return projectile; }

        public static bool IsShotgunShell(IProjectile projectile)
        {
            return projectile.ProjectileItem == ProjectileItem.SHOTGUN
                     || projectile.ProjectileItem == ProjectileItem.DARK_SHOTGUN
                     || projectile.ProjectileItem == ProjectileItem.SAWED_OFF;
        }

        public bool ShotgunShell { get { return IsShotgunShell(Instance); } }
    }
}
