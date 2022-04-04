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
    class DoubleTroubleProjectile : Projectile
    {
        public DoubleTroubleProjectile(IProjectile projectile) : this(projectile, RangedWeaponPowerup.DoubleTrouble) { }
        public DoubleTroubleProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        public override bool IsRemoved { get { return true; } }

        protected override bool OnProjectileCreated()
        {
            var owner = Game.GetPlayer(InitialOwnerPlayerID);
            SpawnOppositeProjectile(owner, Instance);

            return true;
        }

        public static IProjectile SpawnOppositeProjectile(IPlayer owner, IProjectile projectile)
        {
            Vector2 position, direction;

            if (!owner.GetWeaponMuzzleInfo(out position, out direction))
                return null;

            var oppositeDir = Vector2.Negate(projectile.Direction);
            var oBox = owner.GetAABB();
            var offset = Vector2.Distance(oBox.Center, position) / 2f;
            var start = position + oppositeDir * offset;
            var end = start + oppositeDir * (oBox.Height + 3);
            var result = Game.RayCast(end, start, new RayCastInput()
            {
                FilterOnMaskBits = true,
                MaskBits = CategoryBits.Player,
                ClosestHitOnly = true,
                IncludeOverlap = true,
            }).FirstOrDefault(r => r.HitObject.UniqueID == owner.UniqueID);

            if (result.HitObject == null) return null;

            var spawnPosition = result.Position + oppositeDir * 3;
            var projectileItem = projectile.ProjectileItem;

            return Game.SpawnProjectile(projectileItem, spawnPosition, oppositeDir, ProjectilePowerup.Bouncing);
        }
    }
}
