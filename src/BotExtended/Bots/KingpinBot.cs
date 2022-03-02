using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class KingpinBot : Bot
    {
        private Controller<KingpinBot> m_controller;
        private List<Bot> m_bodyguards = new List<Bot>();
        private const int BodyguardCount = 2;

        public KingpinBot(BotArgs a, Controller<KingpinBot> controller) : base(a)
        {
            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }

            Events.PlayerMeleeActionCallback.Start((player, args) =>
            {
                if (player.UniqueID != Player.UniqueID) return;

                foreach (var arg in args)
                {
                    var enemy = Game.GetPlayer(arg.ObjectID);
                    if (enemy == null) continue;
                    if (enemy.IsInMidAir && !enemy.IsDead && !Player.IsKicking)
                        enemy.SetLinearVelocity(Vector2.UnitY * 10);
                }
            });
        }

        private float m_crushEnemyTime = 0f;
        private float m_searchBodyguardTime = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (m_controller != null)
                m_controller.OnUpdate(elapsed);

            if (m_bodyguards.Count < BodyguardCount && ScriptHelper.IsElapsed(m_searchBodyguardTime, 975))
            {
                m_searchBodyguardTime = Game.TotalElapsedGameTime;
                SearchBodyguards();
            }

            foreach (var bodyguard in m_bodyguards.ToList()) if (bodyguard.Player.IsDead) m_bodyguards.Remove(bodyguard);

            // crush enemy while grabbing
            if (Player.IsHoldingPlayerInGrab && ScriptHelper.IsElapsed(m_crushEnemyTime, 120))
            {
                var enemy = Game.GetPlayer(Player.HoldingPlayerInGrabID);

                if (enemy != null)
                {
                    enemy.DealDamage(1.5f);
                    Game.PlayEffect(EffectName.MeleeHitBlunt, enemy.GetWorldPosition());
                    m_crushEnemyTime = Game.TotalElapsedGameTime;
                }
                if (enemy.GetHealth() == 0) enemy.Gib();
            }
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            foreach (var bodyguard in m_bodyguards) bodyguard.Player.SetGuardTarget(null);
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

        private void SearchBodyguards()
        {
            Func<Bot, bool> isBodyguard = (Bot b) => b.Type == BotType.Bodyguard || b.Type == BotType.Bodyguard2 || b.Type == BotType.GangsterHulk;
            var bodyguards = BotManager.GetBots()
                .Where(b => !b.Player.IsDead && isBodyguard(b))
                .Take(BodyguardCount);

            foreach (var bodyguard in bodyguards)
            {
                var bs = bodyguard.Player.GetBotBehaviorSet();
                bs.GuardRange = 30f;
                bs.ChaseRange = 32f;
                bodyguard.SetBotBehaviorSet(bs, true);
                bodyguard.Player.SetGuardTarget(Player);

                m_bodyguards.Add(bodyguard);
            }
        }
    }
}
