using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class TeddybearBot : Bot
    {
        public const int EnrageTime = 30000;

        public bool IsEnraged
        {
            get { return Player.GetForcedBotTarget() != null; }
        }
        private static readonly List<string> PlayerEnrageReactions = new List<string>()
        {
            "Oh no",
            "Fuck",
            "Guess I will die",
            "Wait. I'm sorry",
            "It's not my fault",
        };

        public TeddybearBot(BotArgs args) : base(args) { }

        private float m_enrageTimeElasped = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Game.IsEditorTest)
                LogDebug(m_enrageTimeElasped, m_enrageTime,
                    Player.GetModifiers().RunSpeedModifier, Player.GetBotBehaviorSet().SearchItems);

            if (IsEnraged)
            {
                m_enrageTimeElasped += elapsed;
                if (m_enrageTimeElasped >= m_enrageTime)
                {
                    StopEnraging();
                    m_enrageTimeElasped = 0f;
                }
            }
        }

        private int m_enrageTime = 0;
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
            {
                Game.CreateDialogue("GRRRRRR", ScriptHelper.Orange, Player);
                Events.PlayerDeathCallback cb = null;
                cb = Events.PlayerDeathCallback.Start((player) =>
                {
                    if (player.UniqueID == offender.UniqueID)
                    {
                        Player.SetForcedBotTarget(null);
                        cb.Stop();
                    }
                });
            }

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

            if (offender != null && !offender.IsDead)
                Player.SetForcedBotTarget(offender);
        }

        private void StopEnraging()
        {
            ResetModifiers();
            ResetBotBehaviorSet();
            Player.SetStrengthBoostTime(0);
            Player.SetForcedBotTarget(null);
            m_enrageTime = 0;
        }
    }
}
