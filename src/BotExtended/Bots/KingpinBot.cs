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

        public KingpinBot(BotArgs args, Controller<KingpinBot> controller) : base(args)
        {
            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }
        }

        private float m_crushEnemyTime = 0f;
        private float m_searchBodyguardTime = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (Game.IsEditorTest)
            {
                foreach (var bodyguard in m_bodyguards) Game.DrawArea(bodyguard.Player.GetAABB(), Color.Blue);
                LogDebug(m_bodyguards.Count);
            }

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
            }
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            foreach (var bodyguard in m_bodyguards) bodyguard.Player.SetGuardTarget(null);
        }

        private void SearchBodyguards()
        {
            Func<Bot, bool> isBodyguard = (Bot b) => b.Type == BotType.Bodyguard || b.Type == BotType.Bodyguard2 || b.Type == BotType.GangsterHulk;
            var bodyguards = BotManager.GetBots()
                .Where(b => !b.Player.IsDead && isBodyguard(b))
                .Take(BodyguardCount);

            foreach (var bodyguard in bodyguards)
            {
                var modifiers = bodyguard.Player.GetModifiers();
                modifiers.RunSpeedModifier += .1f;
                modifiers.SprintSpeedModifier += .1f;
                bodyguard.SetModifiers(modifiers, true);

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
