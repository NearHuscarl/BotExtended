﻿using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class StunProjectile : Projectile
    {
        private static readonly uint StunnedTime = 1500;
        private static readonly float EMPBlastRadius = 15f;

        public StunProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        public float StunChance { get; private set; }
        public float StunRangeChance { get; private set; }

        protected override void OnProjectileCreated()
        {
            if (IsExplosiveProjectile)
            {
                StunChance = 0f;
                StunRangeChance = 1f;
            }
            else
            {
                StunChance = .19f;
                StunRangeChance = .01f;

                if (IsShotgunShell) // shotguns have double chance to stun
                {
                    StunChance = StunChance / ProjectilesPerShell * 2;
                    StunRangeChance = StunRangeChance / ProjectilesPerShell * 2;
                }
            }
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            var rndNum = RandomHelper.Between(0, 1);
            if (rndNum < StunRangeChance)
            {
                ElectrocuteRange(args.HitPosition);
            }
            if (StunRangeChance <= rndNum && rndNum < StunChance)
            {
                Electrocute(args);
            }
        }

        private void PlayStunEffects(Vector2 position, bool isStunningPlayer)
        {
            Game.PlayEffect(EffectName.Electric, position);
            Game.PlaySound("ElectricSparks", position);
        }

        private void StunBot(Bot bot) { StunBot(bot, bot.Position); }
        private void StunBot(Bot bot, Vector2 hitPosition)
        {
            if (!CanBeStunned(bot)) return;

            PlayStunEffects(hitPosition, true);

            bot.Stun(StunnedTime);
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
                var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(i));

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

            if (args.IsPlayer)
            {
                var player = Game.GetPlayer(args.HitObjectID);
                var bot = BotManager.GetBot(player);

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
