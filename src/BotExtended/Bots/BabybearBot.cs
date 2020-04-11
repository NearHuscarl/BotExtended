using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;

namespace BotExtended.Bots
{
    public class BabybearBot : Bot
    {
        private TeddybearBot m_mommy = null;
        private IPlayer m_offender;
        private static Queue<string> Names = new Queue<string>(new[] { "Timmy", "Jimmy" });

        public BabybearBot(BotArgs args) : base(args) { }

        public override void OnSpawn()
        {
            base.OnSpawn();
            Player.SetBotName(Names.Dequeue());
        }

        private bool m_trackRocketRidingOffender = false;
        private float m_findDelay = -2000f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (m_mommy == null)
            {
                if (ScriptHelper.IsElapsed(m_findDelay, 2000))
                {
                    m_findDelay = Game.TotalElapsedGameTime;
                    FindMommy();
                }
            }

            if (Player.IsRocketRiding && !m_trackRocketRidingOffender)
            {
                var projectile = Game.GetProjectile(Player.RocketRidingProjectileInstanceID);
                m_offender = Game.GetPlayer(projectile.InitialOwnerPlayerID);
                m_trackRocketRidingOffender = true;
            }
            else
            {
                m_trackRocketRidingOffender = false;
            }
        }

        private void FindMommy()
        {
            foreach (var bot in BotManager.GetBots())
            {
                if (bot.Type == BotType.Teddybear)
                {
                    m_mommy = (TeddybearBot)bot;
                    m_mommy.Player.SetBotName("Mommy Bear");
                    break;
                }
            }
            Player.SetGuardTarget(m_mommy.Player);
        }

        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            base.OnDamage(attacker, args);
            m_offender = attacker;
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (!args.Removed)
                if (RandomHelper.Percentage(.75f)) Game.PlaySound("CartoonScream", Position);

            if (m_offender == null || m_offender.IsDead)
                m_offender = FindClosestTarget();

            if (m_offender != null)
                m_mommy.Enrage(m_offender);
        }

        private IPlayer FindClosestTarget()
        {
            IPlayer target = null;

            foreach (var player in Game.GetPlayers())
            {
                if (player.IsDead || player.IsRemoved || ScriptHelper.SameTeam(player, Player))
                    continue;

                if (target == null) target = player;

                var targetDistanceSq = Vector2.DistanceSquared(target.GetWorldPosition(), Position);
                var potentialTargetDistanceSq = Vector2.DistanceSquared(player.GetWorldPosition(), Position);

                if (potentialTargetDistanceSq < targetDistanceSq)
                {
                    target = player;
                }
            }

            return target;
        }
    }
}
