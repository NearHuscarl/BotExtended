using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    public class BabybearBot : Bot
    {
        private TeddybearBot m_mommy = null;
        private IPlayer m_offender;
        public static int EnrageTime = 30; // seconds
        public int m_enrageCount = 0;

        public override void OnSpawn(IEnumerable<Bot> others)
        {
            var names = new Queue<string>(new[] { "Timmy", "Jimmy" });
            UpdateInterval = 0;

            foreach (var bot in others)
            {
                if (bot.Type == BotType.Teddybear)
                {
                    m_mommy = (TeddybearBot)bot;
                    break;
                }
            }
            if (m_mommy.Player == null) return;

            Player.SetBotName(names.Dequeue());

            var behavior = Player.GetBotBehaviorSet();
            behavior.RangedWeaponUsage = false;
            behavior.SearchForItems = false;
            behavior.OffensiveClimbingLevel = 0.9f;
            behavior.OffensiveSprintLevel = 0.85f;
            behavior.GuardRange = 16;
            behavior.ChaseRange = 16;
            Player.SetBotBehaviorSet(behavior);

            Player.SetGuardTarget(m_mommy.Player);
        }

        private bool m_trackRocketRidingOffender = false;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsRocketRiding && !m_trackRocketRidingOffender)
            {
                var projectile = Game.GetProjectile(Player.RocketRidingProjectileInstanceID);
                m_offender = Game.GetPlayer(projectile.OwnerPlayerID);
                m_trackRocketRidingOffender = true;
            }
            else
            {
                m_trackRocketRidingOffender = false;
            }
        }

        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            m_offender = attacker;
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (!args.Removed)
            {
                if (RandomHelper.Between(0, 1) <= 0.75f)
                {
                    Game.PlaySound("CartoonScream", Player.GetWorldPosition());
                }
            }

            if (m_offender == null)
            {
                m_offender = FindClosestTarget();
            }

            m_enrageCount++;
            m_mommy.Enrage(m_offender, EnrageTime * m_enrageCount * 1000);
        }
    }
}
