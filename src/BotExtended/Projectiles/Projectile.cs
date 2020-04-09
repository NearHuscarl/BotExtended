using BotExtended.Library;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class Projectile : ProjectileBase
    {
        public readonly IProjectile Instance;
        public override int ID { get { return Instance.InstanceID; } }
        public override bool IsRemoved
        {
            get { return Instance.IsRemoved; }
            protected set { }
        }

        public Projectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            Instance = projectile;

            if (!OnProjectileCreated())
            {
                Powerup = RangedWeaponPowerup.None;
            }

            IsCustomProjectile = false;
        }

        protected virtual bool OnProjectileCreated() { return true; }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (IsExplosiveProjectile)
            {
                var explosiveArea = ScriptHelper.GrowFromCenter(args.HitPosition, Constants.ExplosionRadius * 2);
                var playersInRadius = Game.GetObjectsByArea<IPlayer>(explosiveArea)
                    .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), args.HitPosition, Constants.ExplosionRadius));

                OnProjectileExploded(playersInRadius);
            }
        }

        protected virtual void OnProjectileExploded(IEnumerable<IPlayer> playersInRadius) { }

        public bool IsShotgunShell
        {
            get
            {
                return Instance.ProjectileItem == ProjectileItem.SHOTGUN
                    || Instance.ProjectileItem == ProjectileItem.DARK_SHOTGUN
                    || Instance.ProjectileItem == ProjectileItem.SAWED_OFF;
            }
        }

        public bool IsExplosiveProjectile
        {
            get
            {
                // TODO: test Flak Cannon
                return Instance.ProjectileItem == ProjectileItem.BAZOOKA
                    || Instance.ProjectileItem == ProjectileItem.GRENADE_LAUNCHER;
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
