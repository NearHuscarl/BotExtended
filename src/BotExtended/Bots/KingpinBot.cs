﻿using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class KingpinBot : Bot
    {
        private Controller<Bot> _controller;

        public KingpinBot(BotArgs a, Controller<Bot> controller) : base(a)
        {
            if (controller != null)
            {
                _controller = controller;
                _controller.Actor = this;
            }
        }

        private float m_crushEnemyTime = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (_controller != null) _controller.OnUpdate(elapsed);

            // push objects away while sprinting
            var vec = Player.GetLinearVelocity();
            if (Math.Abs(vec.X) > Constants.MAX_WALK_SPEED)
            {
                var results = Game.RayCast(Position, Position + Player.AimVector * 8, new RayCastInput()
                {
                    FilterOnMaskBits = true,
                    MaskBits = CategoryBits.DynamicG1,
                    ClosestHitOnly = true,
                }).Where(r => r.HitObject != null);

                if (results.Count() > 0)
                    results.First().HitObject.SetLinearVelocity(new Vector2(Player.AimVector.X * 8, 3));
            }

            // crush enemy while grabbing
            if (Player.IsHoldingPlayerInGrab && ScriptHelper.IsElapsed(m_crushEnemyTime, 120))
            {
                var enemy = Game.GetPlayer(Player.HoldingPlayerInGrabID);

                if (enemy != null)
                {
                    enemy.DealDamage(1.5f);
                    Game.PlayEffect(EffectName.MeleeHitBlunt, enemy.GetWorldPosition());
                    m_crushEnemyTime = Game.TotalElapsedGameTime;
                    if (enemy.GetHealth() == 0) enemy.Gib();
                }
            }
        }

        public override void OnMeleeDamage(IPlayer attacker, PlayerMeleeHitArg arg)
        {
            base.OnMeleeDamage(attacker, arg);
            // TODO: move this ability to other bot
            // Immune to melee attack but will be pushed back a bit
            var pos = Player.GetWorldPosition();
            var direction = Math.Sign(pos.X - arg.HitPosition.X);
            Player.SetLinearVelocity(new Vector2(direction * 5, 0));
        }
    }
}
