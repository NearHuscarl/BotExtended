using SFDGameScriptInterface;
using System;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class CustomProjectile : ProjectileBase
    {
        public IObject Instance { get; private set; }
        public override int ID { get { return Instance.UniqueID; } }
        public override bool IsRemoved
        {
            get { return Instance == null ? true : Instance.IsRemoved; }
            protected set { }
        }

        private Vector2 m_createPosition;
        public float TotalDistanceTraveled { get; private set; }

        public CustomProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            Instance = OnProjectileCreated(projectile);
            if (Instance == null) Powerup = RangedWeaponPowerup.None;

            m_createPosition = Instance.GetWorldPosition();
            TotalDistanceTraveled = 0f;
            IsCustomProjectile = true;
        }

        protected static IObject CreateCustomProjectile(IProjectile projectile, string objectID)
        {
            return CreateCustomProjectile(projectile, objectID, projectile.Velocity / 50 + Vector2.UnitY * 3);
        }
        protected static IObject CreateCustomProjectile(IProjectile projectile, string objectID, Vector2 velocity)
        {
            var customBullet = Game.CreateObject(objectID);
            var length = Math.Max(customBullet.GetAABB().Width, customBullet.GetAABB().Height);

            customBullet.SetWorldPosition(projectile.Position + projectile.Direction * (length + 1));
            customBullet.SetLinearVelocity(velocity);
            customBullet.SetFaceDirection(Math.Sign(projectile.Direction.X));
            customBullet.TrackAsMissile(true);

            projectile.FlagForRemoval();

            return customBullet;
        }

        protected virtual IObject OnProjectileCreated(IProjectile projectile) { return null; }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);
            TotalDistanceTraveled = Vector2.Distance(Instance.GetWorldPosition(), m_createPosition);
        }
    }
}
