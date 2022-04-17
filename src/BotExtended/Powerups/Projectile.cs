using BotExtended.Library;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups
{
    class Projectile : ProjectileBase
    {
        private IProjectile _instance;
        public IProjectile Instance
        {
            get { return _instance; }
            protected set
            {
                if (Instance != null && value != null)
                    PowerupManager.UpdateProjectile(Instance, value);
                _instance = value;
            }
        }

        public override int ID { get { return Instance.InstanceID; } }
        public override bool IsRemoved
        {
            get { return Instance.IsRemoved; }
            protected set { }
        }

        public Projectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            Instance = projectile;

            if (!IsValidPowerup())
                Powerup = RangedWeaponPowerup.None;
            else
                OnProjectileCreated();
            
            IsCustomProjectile = false;
        }

        protected virtual void OnProjectileCreated() { }

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

        public IPlayer GetRocketRider()
        {
            return Game.GetPlayers().Where(p => p.IsRocketRiding && p.RocketRidingProjectileInstanceID == ID).FirstOrDefault();
        }

        public bool IsShotgunShell { get { return RangedWpns.IsShotgunWpns(Mapper.GetWeaponItem(Instance.ProjectileItem)); } }
        public bool IsExplosiveProjectile { get { return RangedWpns.IsExplosiveWpns(Mapper.GetWeaponItem(Instance.ProjectileItem)); } }

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
