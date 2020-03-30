using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class BlastBullet : Projectile
    {
        public BlastBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Blast) { }

        protected override IProjectile OnProjectileCreated(IProjectile projectile)
        {
            switch (projectile.ProjectileItem)
            {
                case ProjectileItem.GRENADE_LAUNCHER:
                case ProjectileItem.BAZOOKA:
                    return null;
            }

            return projectile;
        }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Instance.TotalDistanceTraveled >= 70)
            {
                Game.PlayEffect(EffectName.BulletHit, Instance.Position);
                Instance.FlagForRemoval();
            }
        }

        private IObject GetObject(ProjectileHitArgs args)
        {
            if (args.IsPlayer)
            {
                var player = Game.GetPlayer(args.HitObjectID);

                if (!player.IsFalling)
                {
                    player.SetInputEnabled(false);
                    player.AddCommand(new PlayerCommand(PlayerCommandType.Fall));
                    ScriptHelper.Timeout(() => player.SetInputEnabled(true), 0);
                }

                return player;
            }
            else
            {
                return Game.GetObject(args.HitObjectID);
            }
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            var hitObject = GetObject(args);

            if (!args.IsPlayer && hitObject.GetBodyType() == BodyType.Static) return;

            var angles = new float[] { MathExtension.ToRadians(35), MathExtension.ToRadians(70) };
            var angle = MathExtension.NormalizeAngle(ScriptHelper.GetAngle(Instance.Direction));
            if (angle > MathHelper.PIOver2 && angle <= MathExtension.PI_3Over2)
                angles = ScriptHelper.Flip(angles, FlipDirection.Horizontal);

            var position = Instance.Position;
            var pushDirection = Instance.Direction;
            var upDirection = RandomHelper.Direction(angles[0], angles[1], true);
            Vector2 velocity;

            if (args.IsPlayer)
            {
                velocity = Instance.Direction * 4 + upDirection * 8;
                hitObject.SetLinearVelocity(velocity);
            }
            else
            {
                var mass = hitObject.GetMass();
                var magnitude = MathHelper.Clamp(1f / mass / 5f, 3, 30);
                velocity = Instance.Direction * magnitude + upDirection * magnitude;
                hitObject.SetLinearVelocity(velocity);
                ScriptHelper.LogDebug(hitObject.Name, mass, magnitude);
            }

            if (Game.IsEditorTest)
            {
                ScriptHelper.RunIn(() =>
                {
                    Game.DrawLine(position, position + pushDirection * 16);
                    Game.DrawLine(position, position + upDirection * 8, Color.Yellow);
                    Game.DrawLine(position, position + velocity, Color.Green);
                }, 2000);
            }
        }
    }
}
