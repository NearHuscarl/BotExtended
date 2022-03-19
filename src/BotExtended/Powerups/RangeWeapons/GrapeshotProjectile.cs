using System;
using System.Collections.Generic;
using System.Linq;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
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

            _isElapsedEffect = ScriptHelper.WithIsElapsed(100, 300);
        }

        private Func<bool> _isElapsedEffect;

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
            
            Game.PlayEffect(EffectName.Smack, HoverPosition);
            
            if (Instance.ProjectileItem != ProjectileItem.GRENADE_LAUNCHER)
                Instance.FlagForRemoval();
        }

        protected override void UpdateHovering(float elapsed)
        {
            base.UpdateHovering(elapsed);

            if (HoverTime > 1500)
            {
                Destroy();
            }
            else
            {
                if (_isElapsedEffect()) Game.PlayEffect(EffectName.Electric, HoverPosition);
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
