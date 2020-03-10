using SFDGameScriptInterface;
using BotExtended.Library;
using System;
using System.Collections.Generic;
using static BotExtended.GameScript;
using static BotExtended.Library.Mocks.MockObjects;
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

            var behavior = Player.GetBotBehaviorSet();
            behavior.GuardRange = 1f;
            behavior.ChaseRange = 1f;
            Player.SetBotBehaviorSet(behavior);
        }

        private float m_startEnrageTime = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (IsEnraged)
            {
                if (!ChaseOffender())
                {
                    m_offender = FindClosestTarget();
                }

                if (ScriptHelper.IsElapsed(m_startEnrageTime, m_enrageTime))
                {
                    StopEnraging();
                }
            }
        }

        private bool ChaseOffender()
        {
            if (m_offender == null || m_offender.IsDead) return false;

            // This is a workaround to make a bot target specific IPlayer
            // Need those 2 lines for it to work
            // behavior.GuardRange = 1f;
            // behavior.ChaseRange = 1f;
            if (Vector2.Distance(Position, m_offender.GetWorldPosition()) < 75)
            {
                Player.SetGuardTarget(null);
            }
            else
            {
                Player.SetGuardTarget(m_offender);
            }

            return true;
        }

        private PlayerModifiers m_normalModifiers;
        private BotBehaviorSet m_normalBehaviorSet;
        private int m_enrageTime = 0;
        private IPlayer m_offender;
        public void Enrage(IPlayer offender, int enrageTime)
        {
            if (Player.IsRemoved || Player == null) return;
            bool hasAlreadyEnraged = IsEnraged;

            if (hasAlreadyEnraged)
                Game.CreateDialogue("GRRRRRRRROOAAR!", ScriptHelper.Red, Player);
            else
                Game.CreateDialogue("GRRRRRR", ScriptHelper.Orange, Player);
            Player.SetGuardTarget(offender);

            Game.CreateDialogue(RandomHelper.GetItem(PlayerEnrageReactions), offender);
            
            if (!hasAlreadyEnraged)
                m_normalModifiers = Player.GetModifiers();
            var enrageModifiers = Player.GetModifiers();
            enrageModifiers.RunSpeedModifier = hasAlreadyEnraged ? Speed.ExtremelyFast : Speed.VeryFast;
            enrageModifiers.SprintSpeedModifier = hasAlreadyEnraged ? Speed.ExtremelyFast : Speed.VeryFast;
            enrageModifiers.MeleeForceModifier = MeleeForce.ExtremelyStrong;
            enrageModifiers.EnergyConsumptionModifier = .25f;
            Player.SetModifiers(enrageModifiers);

            m_normalBehaviorSet = Player.GetBotBehaviorSet();
            var bs = GetBehaviorSet(BotAI.RagingHulk, hasAlreadyEnraged ? SearchItems.Melee | SearchItems.Makeshift : SearchItems.Makeshift);
            Player.SetBotBehaviorSet(bs);

            Player.SetStrengthBoostTime(enrageTime);

            IsEnraged = true;
            m_enrageTime = enrageTime;
            m_startEnrageTime = Game.TotalElapsedGameTime;
            m_offender = offender;
        }

        private void StopEnraging()
        {
            var mod = Player.GetModifiers();
            m_normalModifiers.CurrentHealth = mod.CurrentHealth;
            m_normalModifiers.CurrentEnergy = mod.CurrentEnergy;

            Player.SetGuardTarget(null);
            Player.SetModifiers(m_normalModifiers);
            Player.SetBotBehaviorSet(m_normalBehaviorSet);
            Player.SetStrengthBoostTime(0);
            IsEnraged = false;
            m_offender = null;
        }
    }
}
