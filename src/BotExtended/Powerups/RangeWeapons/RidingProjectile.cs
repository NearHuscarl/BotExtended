using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class RidingProjectile : Projectile
    {
        public RidingProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        private Vector2 _direction;

        protected override void OnProjectileCreated()
        {
            Instance.FlagForRemoval();
            Instance = Game.SpawnProjectile(ProjectileItem.BAZOOKA, Instance.Position, Instance.Direction, ProjectilePowerup.Bouncing);
            Instance.Velocity *= 100; // slowest possible

            _direction = Instance.Direction;
        }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Instance.BounceCount > 0) Instance.BounceCount = 0;
            //Instance.Direction = _direction;
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

           
        }
    }
}
