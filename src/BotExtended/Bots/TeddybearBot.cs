using SFDGameScriptInterface;
using BotExtended.Library;
using System;
using System.Collections.Generic;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;
using System.Linq;

namespace BotExtended.Bots
{
    public class TeddybearBot : Bot
    {
        public bool IsEnraged { get; private set; }
        private static readonly List<string> PlayerEnrageReactions = new List<string>()
        {
            "Oh no",
            "Fuck",
            "Guess I will die",
            "Wait. I'm sorry",
            "It's not my fault",
        };

        public TeddybearBot(BotArgs args) : base(args) { }

        public override void OnSpawn(IEnumerable<Bot> others)
        {
            base.OnSpawn(others);

            IsEnraged = false;

            if (others.Count() >= 1) // has cults
                Player.SetBotName("Mommy Bear");
        }

        private float m_startEnrageTime = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (IsEnraged)
            {
                ChaseOffender();

                m_startEnrageTime += elapsed;
                if (m_startEnrageTime >= m_enrageTime)
                {
                    StopEnraging();
                    m_startEnrageTime = 0f;
                }
            }
        }

        private float oldDistance = 0f;
        private void ChaseOffender()
        {
            if (m_offender == null || m_offender.IsDead) return;

            // This is a workaround to make a bot target specific IPlayer
            // The thing is if the bot is guarding another target, that target will not be enemy even if they're from different teams
            // So the workaround is to set target to null when the target is close
            // Need these 2 lines for it to work
            // behavior.GuardRange = 1f;
            // behavior.ChaseRange = 1f;
            Game.DrawCircle(m_offender.GetWorldPosition(), 75);

            var distanceToTarget = Vector2.Distance(Position, m_offender.GetWorldPosition());
            if (distanceToTarget < 35 && oldDistance >= 35)
            {
                Player.SetGuardTarget(null);
                oldDistance = distanceToTarget;
            }
            if (distanceToTarget >= 35 && oldDistance < 35)
            {
                var behavior = Player.GetBotBehaviorSet();
                behavior.GuardRange = 1f;
                behavior.ChaseRange = 1f;
                Player.SetBotBehaviorSet(behavior);
                Player.SetGuardTarget(m_offender);
                oldDistance = distanceToTarget;
            }
        }

        private PlayerModifiers m_normalModifiers;
        private BotBehaviorSet m_normalBehaviorSet;
        private int m_enrageTime = 0;
        private IPlayer m_offender;
        public void Enrage(IPlayer offender, int enrageTime)
        {
            if (Player.IsRemoved || Player == null) return;
            var hasAlreadyEnraged = IsEnraged;

            if (hasAlreadyEnraged)
                Game.CreateDialogue("GRRRRRRRROOAAR!", ScriptHelper.Red, Player);
            else
                Game.CreateDialogue("GRRRRRR", ScriptHelper.Orange, Player);
            Player.SetGuardTarget(offender);

            Game.CreateDialogue(RandomHelper.GetItem(PlayerEnrageReactions), offender);

            if (!hasAlreadyEnraged)
                m_normalModifiers = Player.GetModifiers();
            var enrageModifiers = Player.GetModifiers();
            enrageModifiers.MeleeStunImmunity = Constants.TOGGLE_ON;
            enrageModifiers.RunSpeedModifier = hasAlreadyEnraged ? Speed.ExtremelyFast : Speed.VeryFast;
            enrageModifiers.SprintSpeedModifier = hasAlreadyEnraged ? Speed.ExtremelyFast : Speed.VeryFast;
            enrageModifiers.MeleeForceModifier = MeleeForce.ExtremelyStrong;
            enrageModifiers.EnergyConsumptionModifier = .25f;
            SetModifiers(enrageModifiers);

            m_normalBehaviorSet = Player.GetBotBehaviorSet();
            var bs = GetBehaviorSet(BotAI.RagingHulk);
            bs.SearchItems = hasAlreadyEnraged ? SearchItems.Melee | SearchItems.Makeshift : SearchItems.Makeshift;
            Player.SetBotBehaviorSet(bs);

            Player.SetStrengthBoostTime(enrageTime);

            IsEnraged = true;
            m_enrageTime = enrageTime;
            m_offender = offender;
        }

        private void StopEnraging()
        {
            Player.SetGuardTarget(null);
            SetModifiers(m_normalModifiers);
            Player.SetBotBehaviorSet(m_normalBehaviorSet);
            Player.SetStrengthBoostTime(0);
            IsEnraged = false;
            m_offender = null;
        }
    }
}
