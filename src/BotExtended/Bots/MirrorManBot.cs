﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class MirrorManBot : Bot
    {
        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            base.OnSpawn(bots);
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
            Game.SpawnProjectile(projectile.ProjectileItem, position, reflectVec);
        }
    }
}
