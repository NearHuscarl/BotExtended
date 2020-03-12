using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Projectiles
{
    class StunBullet : ProjectileHooks
    {
        private static readonly uint StunnedTime = 3000;
        private static readonly float EMPBlastRadius = 15f;

        public override IProjectile OnProjectileCreated(IProjectile projectile)
        {
            switch (projectile.ProjectileItem)
            {
                case ProjectileItem.BAZOOKA:
                case ProjectileItem.GRENADE_LAUNCHER:
                    return null;
                default:
                    return projectile;
            }
        }

        public override void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            var rndNum = RandomHelper.Between(0, 100);
            if (rndNum < 1)
            {
                ElectrocuteRange(args.HitPosition);
            }
            if (1 <= rndNum && rndNum < 21)
            {
                Electrocute(args);
            }
        }

        private void PlayStunEffects(Vector2 position, bool isStunningPlayer)
        {
            Game.PlayEffect(EffectName.Electric, position);
            Game.PlaySound("ElectricSparks", position);

            var sparkFireChance = isStunningPlayer ? .3f : .7f;
            if (RandomHelper.Percentage(sparkFireChance))
            {
                Game.SpawnFireNode(position, Vector2.Zero);
                Game.PlayEffect(EffectName.FireTrail, position);
            }
        }

        private void StunBot(Bot bot) { StunBot(bot, bot.Position); }
        private void StunBot(Bot bot, Vector2 hitPosition)
        {
            if (!CanBeStunned(bot)) return;

            PlayStunEffects(hitPosition, true);
            Game.PlayEffect(EffectName.CustomFloatText, hitPosition, "stunned");

            bot.Stun(StunnedTime, (e) =>
            {
                var position = bot.Position;
                position.X += RandomHelper.Between(-10, 10);
                position.Y += RandomHelper.Between(-10, 10);

                Game.PlayEffect(EffectName.Electric, position);
            }, 400);
        }

        private void ElectrocuteRange(Vector2 position)
        {
            foreach (var bot in BotManager.GetBots())
            {
                var player = bot.Player;
                if (ScriptHelper.IntersectCircle(player.GetAABB(), position, EMPBlastRadius))
                {
                    StunBot(bot);
                }
            }

            for (var i = 0; i < 360; i += 72) // Play electric effect 5 times in circle (360 / 5 = 72)
            {
                var direction = new Vector2()
                {
                    X = (float)Math.Cos(MathExtension.ToRadians(i)),
                    Y = (float)Math.Sin(MathExtension.ToRadians(i)),
                };

                Game.PlayEffect(EffectName.Electric, position + direction * EMPBlastRadius);
                Game.PlaySound("ElectricSparks", position);
            }

            if (Game.IsEditorTest)
            {
                Events.UpdateCallback.Start((e) => Game.DrawCircle(position, EMPBlastRadius, Color.Cyan),
                    0, 60 * 2);
            }
        }

        private void Electrocute(ProjectileHitArgs args)
        {
            var position = args.HitPosition;

            var obj = Game.GetObject(args.HitObjectID);

            if (args.IsPlayer)
            {
                var player = Game.GetPlayer(args.HitObjectID);
                var bot = BotManager.GetExtendedBot(player);

                if (bot != Bot.None)
                {
                    StunBot(bot, position);
                }
            }
            else
                PlayStunEffects(position, false);
        }

        private bool CanBeStunned(Bot bot)
        {
            var player = bot.Player;
            return !player.IsRemoved && !player.IsDead && !bot.IsStunned;
        }
    }
}
