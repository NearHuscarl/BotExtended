using System;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class BlastProjectile : Projectile
    {
        public BlastProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override void OnProjectileCreated()
        {
            Instance.DamageDealtModifier = IsShotgunShell ? .35f : .5f;
        }

        private IObject GetObject(ProjectileHitArgs args)
        {
            var player = Game.GetPlayer(args.HitObjectID);
            if (player != null)
            {
                if (!player.IsFalling) ScriptHelper.Fall(player);
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
            var blastDirection = RandomHelper.Direction(angles[0], angles[1], true);
            var modifiers = GetForceModifier();
            var velocity = hitObject.GetLinearVelocity();

            if (args.IsPlayer)
            {
                velocity += Instance.Direction * 4 + blastDirection * 14 * modifiers;
                hitObject.SetLinearVelocity(MathExtension.ClampMagnitude(velocity, 15));
            }
            else
            {
                var mass = hitObject.GetMass();
                var magnitude = MathHelper.Clamp(1f / mass / 7f, 3, 30) * modifiers;
                velocity += Instance.Direction * magnitude + blastDirection * magnitude / 10;
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
