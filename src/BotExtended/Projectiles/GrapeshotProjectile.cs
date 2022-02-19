using System;
using System.Collections.Generic;
using System.Linq;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class GrapeshotProjectile : HoveringProjectile
    {
        public GrapeshotProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Grapeshot)
        {
            if (projectile.ProjectileItem == ProjectileItem.BAZOOKA
                || projectile.ProjectileItem == ProjectileItem.GRENADE_LAUNCHER
                || projectile.ProjectileItem == ProjectileItem.FLAREGUN
                || projectile.ProjectileItem == ProjectileItem.BOW
                || projectile.ProjectileItem == ProjectileItem.SNIPER)
                UpdateDelay = 0;
            else
                UpdateDelay = 4;
        }

        private static List<IProjectile> _bouncyProjectiles = new List<IProjectile>();
        static GrapeshotProjectile()
        {
            Events.UpdateCallback.Start((e) =>
            {
                foreach (var projectile in _bouncyProjectiles.ToList())
                {
                    if (projectile.BounceCount >= 2) projectile.FlagForRemoval();
                    else
                    {
                        projectile.Velocity = new Vector2(projectile.Velocity.X, projectile.Velocity.Y - 15);
                    }
                }
            }, 10);
        }

        protected override void OnHover()
        {
            base.OnHover();
            if (Instance.ProjectileItem != ProjectileItem.GRENADE_LAUNCHER)
                Instance.FlagForRemoval();
        }

        private float _effectTime = 0;
        protected override void UpdateHovering(float elapsed)
        {
            base.UpdateHovering(elapsed);

            if (HoverTime > 1500)
            {
                Destroy();
            }
            else
            {
                if (ScriptHelper.IsElapsed(_effectTime, 500))
                {
                    Game.PlayEffect(EffectName.Electric, HoverPosition);
                    _effectTime = Game.TotalElapsedGameTime;
                }
            }
        }

        protected override void Destroy()
        {
            base.Destroy();
            if (Instance.ProjectileItem == ProjectileItem.GRENADE_LAUNCHER
                || Instance.ProjectileItem == ProjectileItem.BAZOOKA)
                Game.TriggerExplosion(HoverPosition);

            Game.PlaySound("Pistol", HoverPosition);

            var bouncyAmmos = IsShotgunShell ? RandomHelper.Percentage(.5f) ? 1 : 0 : 5;
            for (var i = 0; i < bouncyAmmos; i++)
            {
                var projectile = Game.SpawnProjectile(ProjectileItem.PISTOL, HoverPosition, RandomHelper.Direction(0, 360), ProjectilePowerup.Bouncing);
                projectile.Velocity /= 4;
                projectile.DamageDealtModifier = .3f;
                _bouncyProjectiles.Add(projectile);
            }
        }
    }
}
