using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class BouncingLaser : Projectile
    {
        public BouncingLaser(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        private int _bounceCount = 0;
        private float _bounceTime = 0;
        private RayCastResult _bounceResult;
        private ProjectileProperties _props;
        private Vector2 _lastDir;
        public override bool IsRemoved { get { return _bounceCount >= 6; } }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (_bounceTime == 0) return;

            if (ScriptHelper.IsElapsed(_bounceTime, 100))
            {
                if (_bounceResult.HitObject == null)
                    _bounceCount = int.MaxValue; // remove
                else
                    ShootLaser(_bounceResult.Position - _lastDir * 2, Vector2.Reflect(_lastDir, _bounceResult.Normal));
            }
        }

        protected override void OnProjectileCreated()
        {
            _props = Instance.GetProperties();
            ShootLaser(Instance.Position, Instance.Direction);
            Instance.FlagForRemoval();
        }

        private void ShootLaser(Vector2 position, Vector2 direction)
        {
            _bounceResult = GaussGun.SpawnLaser(position, direction,
                playerDamage: _props.PlayerDamage,
                objectDamage: _props.ObjectDamage);
            _bounceTime = Game.TotalElapsedGameTime;
            _lastDir = direction;
            _bounceCount++;
        }
    }
}
