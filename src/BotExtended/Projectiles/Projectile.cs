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
            Instance = projectile;

            if (!OnProjectileCreated())
            {
                Instance = null;
                Powerup = RangedWeaponPowerup.None;
            }

            IsCustomProjectile = false;
        }

        protected virtual bool OnProjectileCreated() { return true; }

        public bool IsShotgunShell
        {
            get
            {
                return Instance.ProjectileItem == ProjectileItem.SHOTGUN
                         || Instance.ProjectileItem == ProjectileItem.DARK_SHOTGUN
                         || Instance.ProjectileItem == ProjectileItem.SAWED_OFF;
            }
        }

        public int ProjectilesPerShell
        {
            get
            {
                if (Instance == null) return 0;

                switch (Instance.ProjectileItem)
                {
                    case ProjectileItem.SHOTGUN:
                        return 6;
                    case ProjectileItem.DARK_SHOTGUN:
                        return 8;
                    case ProjectileItem.SAWED_OFF:
                        return 6;
                    case ProjectileItem.NONE:
                        return 0;
                    default:
                        return 1;
                }
            }
        }
    }
}
