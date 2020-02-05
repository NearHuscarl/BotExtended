using SFDGameScriptInterface;
using SFDScript.Library;
using System;
using System.Collections.Generic;
using static SFDScript.BotExtended.GameScript;
using static SFDScript.Library.Mocks.MockObjects;

namespace SFDScript.BotExtended.Bots
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

        public override void OnSpawn(List<Bot> others)
        {
            IsEnraged = false;

            if (others.Count >= 1) // has cults
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
            if (Vector2.Distance(Player.GetWorldPosition(), m_offender.GetWorldPosition()) < 75)
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
            bool isAlreadyEnraged = IsEnraged;

            if (isAlreadyEnraged)
                Game.CreateDialogue("GRRRRRRRROOAAR!", ScriptHelper.Red, Player);
            else
                Game.CreateDialogue("GRRRRRR", ScriptHelper.Orange, Player);
            Player.SetGuardTarget(offender);

            Game.CreateDialogue(RandomHelper.GetItem(PlayerEnrageReactions), offender);
            
            m_normalModifiers = Player.GetModifiers();
            var enrageModifiers = Player.GetModifiers();
            enrageModifiers.RunSpeedModifier = isAlreadyEnraged ? 1.5f : 1.25f;
            enrageModifiers.SprintSpeedModifier = isAlreadyEnraged ? 1.5f : 1.25f;
            enrageModifiers.MeleeForceModifier = isAlreadyEnraged ? 3f : 2.25f;
            Player.SetModifiers(enrageModifiers);

            m_normalBehaviorSet = Player.GetBotBehaviorSet();
            var bs = GetBehaviorSet(BotAI.RagingHulk, isAlreadyEnraged ? SearchItems.Melee : SearchItems.Makeshift);
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
