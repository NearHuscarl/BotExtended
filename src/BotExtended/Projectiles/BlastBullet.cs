using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class BlastBullet : Projectile
    {
        public BlastBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Blast) { }

        protected override bool OnProjectileCreated()
        {
            if (IsExplosiveProjectile)
                return false;

            Instance.DamageDealtModifier = IsShotgunShell ? .35f : .5f;
            return true;
        }

        private IObject GetObject(ProjectileHitArgs args)
        {
            if (args.IsPlayer)
            {
                var player = Game.GetPlayer(args.HitObjectID);

                if (!player.IsFalling)
                {
                    ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Fall, 30);
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
            var modifiers = GetForceModifier();
            var velocity = hitObject.GetLinearVelocity();

            if (args.IsPlayer)
            {
                velocity += Instance.Direction * 4 + upDirection * 14 * modifiers;
                if (velocity.Length() >= 15)
                    velocity -= Vector2.Normalize(velocity) * (velocity.Length() - 15);
                hitObject.SetLinearVelocity(velocity);
            }
            else
            {
                var mass = hitObject.GetMass();
                var magnitude = MathHelper.Clamp(1f / mass / 7f, 3, 30) * modifiers;
                velocity += Instance.Direction * magnitude + upDirection * magnitude / 10;
                hitObject.SetLinearVelocity(velocity);
                //ScriptHelper.LogDebug(hitObject.Name, mass, magnitude);
            }

            //if (Game.IsEditorTest)
            //{
            //    ScriptHelper.RunIn(() =>
            //    {
            //        if (args.IsPlayer)
            //            Game.DrawText(modifiers.ToString(), position);
            //        Game.DrawLine(position, position + pushDirection * 3);
            //        Game.DrawLine(position, position + upDirection * 3, Color.Yellow);
            //        Game.DrawLine(position, position + velocity, Color.Green);
            //    }, 2000);
            //}
        }

        private float GetForceModifier()
        {
            // (0,1.25) (70,1) (140, 0.75)
            var ammoModifier = IsShotgunShell ? .3f : 1f;
            var modifier = ammoModifier * (1.25f - 0.00357143f * Instance.TotalDistanceTraveled);
            return Math.Max(modifier, ammoModifier * .5f);
        }
    }
}
