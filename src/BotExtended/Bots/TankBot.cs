using System.Collections.Generic;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class TankBot : Bot
    {
        public TankBot(BotArgs args) : base(args) { }

        public override void OnSpawn()
        {
            base.OnSpawn();
            Player.SetHitEffect(PlayerHitEffect.Metal);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

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

        public override void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            base.OnProjectileHit(projectile, args);

            if (RandomHelper.Percentage(0.9f))
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

            Game.SpawnProjectile(projectile.ProjectileItem, position, reflectVec, ScriptHelper.GetPowerup(projectile));
        }
    }
}
