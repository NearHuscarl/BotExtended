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
    class DormantProjectile : CustomProjectile
    {
        protected float ExplodeRange = 60;
        protected float ExplodeRange2 = 10;
        protected float MinDistanceBeforeHover = 100;

        public DormantProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Dormant) { }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            if (projectile.ProjectileItem != ProjectileItem.GRENADE_LAUNCHER)
                return null;

            var owner = Game.GetPlayer(InitialOwnerPlayerID);
            return CreateCustomProjectile(projectile, "BazookaRocket", projectile.Velocity / 20, owner.GetFaceDirection() * RandomHelper.Between(-50, -70));
        }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (IsDormant() && !Instance.IsRemoved)
            {
                var direction = ScriptHelper.GetDirection(Instance.GetAngle());
                var proj = Game.SpawnProjectile(ProjectileItem.BAZOOKA, Instance.GetWorldPosition(), direction);
                var bounceCount = 0;
                proj.PowerupBounceActive = true;

                ScriptHelper.RunUntil(() =>
                {
                    if (proj.BounceCount > 0)
                    {
                        proj.BounceCount = 0;
                        bounceCount++;
                    }
                    if (bounceCount == 2)
                    {
                        Game.TriggerExplosion(proj.Position);
                        proj.FlagForRemoval();
                    }
                }, () => proj.IsRemoved);

                Instance.Remove();
            }
        }

        private bool IsDormant()
        {
            return Instance.GetLinearVelocity() == Vector2.Zero;
        }
    }
}
