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
    class Scattershot: RangeWpn
    {
        public Scattershot(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner, name, powerup) { }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);
            TripleFire(projectile);
        }

        private void TripleFire(IProjectile projectile1)
        {
            var muzzle = GetMuzleInfo();
            if (!muzzle.IsSussess) return;

            var powerup = ScriptHelper.GetPowerup(projectile1);
            var accuracyDeflection = 0.75f / 2;
            var angle = ScriptHelper.GetAngle(muzzle.Direction);
            var dir1 = RandomHelper.Direction(angle - accuracyDeflection, angle + accuracyDeflection, true);
            var dir2 = RandomHelper.Direction(angle - accuracyDeflection, angle + accuracyDeflection, true);
            var dir3 = RandomHelper.Direction(angle - accuracyDeflection, angle + accuracyDeflection, true);

            projectile1.Direction = dir1;
            var projectile2 = Game.SpawnProjectile(projectile1.ProjectileItem, muzzle.Position, dir2, powerup);
            var projectile3 = Game.SpawnProjectile(projectile1.ProjectileItem, muzzle.Position, dir3, powerup);

            projectile1.DamageDealtModifier /= 2;
            projectile2.DamageDealtModifier /= 2;
            projectile3.DamageDealtModifier /= 2;
        }
    }
}
