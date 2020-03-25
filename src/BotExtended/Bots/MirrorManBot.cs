using System.Collections.Generic;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class MirrorManBot : Bot
    {
        public MirrorManBot(BotArgs args) : base(args) { }

        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            base.OnSpawn(bots);
            Player.SetHitEffect(PlayerHitEffect.Metal);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            PlayShinyEffect(elapsed);

            var primaryWeapon = Player.CurrentPrimaryWeapon;
            var secondaryWeapon = Player.CurrentSecondaryWeapon;

            if (primaryWeapon.PowerupBouncingRounds == 0 && primaryWeapon.TotalAmmo > 0)
            {
                Player.SetCurrentPrimaryWeaponAmmo(primaryWeapon.TotalAmmo, ProjectilePowerup.Bouncing);
            }
            if (secondaryWeapon.PowerupBouncingRounds == 0 && secondaryWeapon.TotalAmmo > 0)
            {
                Player.SetCurrentSecondaryWeaponAmmo(secondaryWeapon.TotalAmmo, ProjectilePowerup.Bouncing);
            }
        }

        private List<float> m_effectTimes = new List<float>() { 0, 0 };
        private void PlayShinyEffect(float elapsed)
        {
            for (var i = 0; i < m_effectTimes.Count; i++)
            {
                m_effectTimes[i] += elapsed;
                if (m_effectTimes[i] >= 400)
                {
                    if (RandomHelper.Boolean())
                    {
                        var position = Player.GetWorldPosition();
                        position.X += RandomHelper.Between(-10, 10);
                        position.Y += RandomHelper.Between(-10, 10);
                        Game.PlayEffect(EffectName.ItemGleam, position);
                        m_effectTimes[i] = 0;
                    }
                    else
                        m_effectTimes[i] = RandomHelper.Between(0, 400);
                }
            }
        }

        public override void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            base.OnProjectileHit(projectile, args);

            if (RandomHelper.Between(0, 100) < 90)
            {
                DeflectBullet(projectile, args.HitNormal);
            }
        }

        private void DeflectBullet(IProjectile projectile, Vector2 normal)
        {
            var reflectVec = Vector2.Reflect(projectile.Direction, normal)
                + RandomHelper.Direction(-65, 65);
            var direction = projectile.Direction.X > 0 ? 1 : -1;
            var position = projectile.Position - direction * Vector2.UnitX * 5;
            var powerup = ProjectilePowerup.None;

            if (projectile.PowerupBounceActive)
                powerup = ProjectilePowerup.Bouncing;
            if (projectile.PowerupFireActive)
                powerup = ProjectilePowerup.Fire;

            Game.SpawnProjectile(projectile.ProjectileItem, position, reflectVec, powerup);
        }
    }
}
