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
        public const int EnrageTime = 30000;

        public bool IsEnraged { get; private set; }
        private static readonly List<string> PlayerEnrageReactions = new List<string>()
        {
            "Oh no",
            "Fuck",
            "Guess I will die",
            "Wait. I'm sorry",
            "It's not my fault",
        };

        public TeddybearBot(BotArgs args) : base(args) { IsEnraged = false; }

        private float m_enrageTimeElasped = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Game.IsEditorTest)
                LogDebug(m_enrageTimeElasped, m_enrageTime,
                    Player.GetModifiers().RunSpeedModifier, Player.GetBotBehaviorSet().SearchItems);

            if (IsEnraged)
            {
                ChaseOffender();

                m_enrageTimeElasped += elapsed;
                if (m_enrageTimeElasped >= m_enrageTime)
                {
                    StopEnraging();
                    m_enrageTimeElasped = 0f;
                }
            }
        }

        private float m_oldDistance = 0f;
        private void ChaseOffender()
        {
            if (m_offender == null || m_offender.IsDead) return;

            // This is a workaround to make a bot target specific IPlayer
            // The thing is if the bot is guarding another target, that target will not be enemy even if they're from different teams
            // So the workaround is to set target to null when the target is close
            // Need these 2 lines for it to work
            // behavior.GuardRange = 1f;
            // behavior.ChaseRange = 1f;

            var distanceToTarget = Vector2.Distance(Position, m_offender.GetWorldPosition());
            if (distanceToTarget < 35 && m_oldDistance >= 35)
            {
                Player.SetGuardTarget(null);
                m_oldDistance = distanceToTarget;
            }
            if (distanceToTarget >= 35 && m_oldDistance < 35)
            {
                var behavior = Player.GetBotBehaviorSet();
                behavior.GuardRange = 1f;
                behavior.ChaseRange = 1f;
                SetBotBehaviorSet(behavior);
                Player.SetGuardTarget(m_offender);
                m_oldDistance = distanceToTarget;
            }
        }

        private int m_enrageTime = 0;
        private IPlayer m_offender;
        public void Enrage(IPlayer offender)
        {
            if (Player.IsRemoved || Player == null) return;

            var hasAlreadyEnraged = IsEnraged;
            m_enrageTime = EnrageTime;
            m_enrageTimeElasped = 0;

            if (hasAlreadyEnraged)
            {
                m_enrageTime *= 2;
                Game.CreateDialogue("GRRRRRRRROOAAR!", ScriptHelper.Red, Player);
            }
            else
                Game.CreateDialogue("GRRRRRR", ScriptHelper.Orange, Player);
            Player.SetGuardTarget(offender);

            Game.CreateDialogue(RandomHelper.GetItem(PlayerEnrageReactions), offender);

            var enrageModifiers = Player.GetModifiers();
            enrageModifiers.MeleeStunImmunity = Constants.TOGGLE_ON;
            enrageModifiers.RunSpeedModifier = hasAlreadyEnraged ? Speed.ExtremelyFast : Speed.VeryFast;
            enrageModifiers.SprintSpeedModifier = hasAlreadyEnraged ? Speed.ExtremelyFast : Speed.VeryFast;
            enrageModifiers.MeleeForceModifier = MeleeForce.ExtremelyStrong;
            enrageModifiers.EnergyConsumptionModifier = .25f;
            SetModifiers(enrageModifiers);

            var bs = GetBehaviorSet(BotAI.RagingHulk);
            bs.SearchItems = hasAlreadyEnraged ? SearchItems.Melee | SearchItems.Makeshift : SearchItems.Makeshift;
            SetBotBehaviorSet(bs);
            Player.SetStrengthBoostTime(float.MaxValue);

            IsEnraged = true;
            m_offender = offender;
        }

        private void StopEnraging()
        {
            Player.SetGuardTarget(null);
            ResetModifiers();
            ResetBotBehaviorSet();
            Player.SetStrengthBoostTime(0);
            IsEnraged = false;
            m_offender = null;
            m_enrageTime = 0;
        }
    }
}
