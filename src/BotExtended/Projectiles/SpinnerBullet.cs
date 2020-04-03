using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class SpinnerBullet : HoveringProjectile
    {
        public SpinnerBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Spinner)
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

        protected override void OnHover()
        {
            base.OnHover();
            if (Instance.ProjectileItem != ProjectileItem.GRENADE_LAUNCHER)
                Instance.FlagForRemoval();
        }

        private float m_fireTime = 0f;
        private float m_fireAngle = 0f;
        protected override void UpdateHovering(float elapsed)
        {
            if (ScriptHelper.IsElapsed(m_fireTime, 30))
            {
                var totalBullets = 20;
                var angleInBetween = 360 / totalBullets;
                var powerup = ScriptHelper.GetPowerup(Instance);
                var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(m_fireAngle));

                Game.PlaySound("SilencedUzi", HoverPosition);
                Game.SpawnProjectile(ProjectileItem.MAGNUM, HoverPosition, direction, powerup);

                if (m_fireAngle == 360 - angleInBetween)
                    Destroy();

                m_fireTime = Game.TotalElapsedGameTime;
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
