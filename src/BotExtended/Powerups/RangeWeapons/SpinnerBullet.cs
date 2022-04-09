using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class SpinnerBullet : HoveringProjectile
    {
        public SpinnerBullet(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            if (RangedWpns.IsSlowWpns(Mapper.GetWeaponItem(projectile.ProjectileItem)))
                UpdateDelay = 0;
            else
                UpdateDelay = 4;

            _isElapsedFire = ScriptHelper.WithIsElapsed(30);
        }

        protected override void OnHover()
        {
            base.OnHover();
            if (Instance.ProjectileItem != ProjectileItem.GRENADE_LAUNCHER)
                Instance.FlagForRemoval();
        }

        private Func<bool> _isElapsedFire;
        private float m_fireAngle = 0f;
        protected override void UpdateHovering(float elapsed)
        {
            if (_isElapsedFire())
            {
                var totalBullets = 20;
                var angleInBetween = 360 / totalBullets;
                var powerup = ScriptHelper.GetPowerup(Instance);
                var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(m_fireAngle));

                Game.PlaySound("SilencedUzi", HoverPosition);
                Game.SpawnProjectile(ProjectileItem.MAGNUM, HoverPosition, direction, powerup);

                if (m_fireAngle == 360 - angleInBetween)
                    Destroy();

                m_fireAngle += angleInBetween;
            }
        }

        protected override void Destroy()
        {
            base.Destroy();
            if (Instance.ProjectileItem == ProjectileItem.GRENADE_LAUNCHER
                || Instance.ProjectileItem == ProjectileItem.BAZOOKA)
                Game.TriggerExplosion(HoverPosition);
            else
                Game.PlayEffect(EffectName.Block, HoverPosition);
        }
    }
}
