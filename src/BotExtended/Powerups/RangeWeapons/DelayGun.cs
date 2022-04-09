using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class DelayGun : RangeWpn
    {
        public List<IObject> _slowmoProjectiles = new List<IObject>();

        public DelayGun(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner, name, powerup) { }

        private bool _isManualAiming = false;
        private bool _isHipFiring = false;
        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (_isManualAiming && !Owner.IsManualAiming || !IsEquipping || _isHipFiring && !Owner.IsHipFiring)
                AccelerateProjectiles();

            _isManualAiming = Owner.IsManualAiming;
            _isHipFiring = Owner.IsHipFiring;

            foreach (var p in _slowmoProjectiles.ToList())
            {
                p.SetLinearVelocity(Vector2.Zero);
                p.SetWorldPosition(p.GetWorldPosition() + ScriptHelper.GetDirection(p.GetAngle()) * .1f);
                if (p.IsRemoved) _slowmoProjectiles.Remove(p);
            }
        }

        private void AccelerateProjectiles()
        {
            if (_slowmoProjectiles.Count == 0) return;
            foreach (var proj in _slowmoProjectiles)
            {
                proj.Remove();
                var proj2 = Game.SpawnProjectile(Mapper.GetProjectile(Name), proj.GetWorldPosition(), ScriptHelper.GetDirection(proj.GetAngle()));
                proj2.Velocity /= 1.5f;
            }
            _slowmoProjectiles.Clear();
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            projectile.FlagForRemoval();
            var bullet = Game.CreateObject("BulletCommonSlowmo", projectile.Position);
            bullet.SetAngle(ScriptHelper.GetAngle(projectile.Direction));
            bullet.SetMass(0);
            _slowmoProjectiles.Add(bullet);
        }
    }
}
