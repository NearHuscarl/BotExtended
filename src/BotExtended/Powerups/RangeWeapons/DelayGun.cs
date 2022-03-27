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
        public List<IProjectile> _projectiles = new List<IProjectile>();

        public override bool IsValidPowerup()
        {
            return !Projectile.IsSlowProjectile(Mapper.GetProjectile(Name));
        }

        public DelayGun(IPlayer owner, WeaponItem name) : base(owner, name, RangedWeaponPowerup.Delay) { }

        private bool _isManualAiming = false;
        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (_isManualAiming && !Owner.IsManualAiming || !IsEquipping && _projectiles.Count > 0) _projectiles.Clear();
            _isManualAiming = Owner.IsManualAiming;

            foreach (var p in _projectiles.ToList())
            {
                p.Position -= p.Velocity * .01499f;
                if (p.IsRemoved) _projectiles.Remove(p);
            }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);
            _projectiles.Add(projectile);
        }
    }
}
