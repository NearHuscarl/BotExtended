using BotExtended.Library;
using SFDGameScriptInterface;
using System.Collections.Generic;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class StripperBot : Bot
    {
        private static HashSet<int> m_occupiedBouncerIDs = new HashSet<int>();
        private IPlayer m_bouncer;
        private readonly float BouncerCheckTime = 1000f;
        private float m_bouncerCheckTime = 0f;

        public StripperBot(BotArgs args) : base(args)
        {
            BouncerCheckTime = RandomHelper.Between(800, 1200);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (ScriptHelper.IsElapsed(m_bouncerCheckTime, BouncerCheckTime))
            {
                m_bouncerCheckTime = Game.TotalElapsedGameTime;

                if (m_bouncer == null)
                {
                    FindBouncer();
                }
            }
        }

        private void FindBouncer()
        {
            foreach (var bot in BotManager.GetBots())
            {
                var isBodyguard = bot.Type == BotType.Bodyguard
                    || bot.Type == BotType.BikerHulk
                    || bot.Type == BotType.GangsterHulk
                    || bot.Type == BotType.PunkHulk
                    || bot.Type == BotType.ThugHulk;

                if (isBodyguard && ScriptHelper.SameTeam(bot.Player, Player) && !m_occupiedBouncerIDs.Contains(bot.Player.UniqueID))
                {
                    m_bouncer = bot.Player;
                    m_occupiedBouncerIDs.Add(m_bouncer.UniqueID);

                    var bs = m_bouncer.GetBotBehaviorSet();

                    bs.GuardRange = 10f;
                    bs.ChaseRange = 11.5f;
                    m_bouncer.SetBotBehaviorSet(bs);
                    m_bouncer.SetGuardTarget(Player);
                    m_bouncer.SetBotName("Bouncer");

                    if (Game.IsEditorTest)
                    {
                        var color = RandomHelper.GetItem(Color.Red, Color.Yellow, Color.Blue, Color.Green, Color.Magenta, Color.Cyan);

                        ScriptHelper.RunIn(() =>
                        {
                            Game.DrawArea(m_bouncer.GetAABB(), color);
                            Game.DrawLine(m_bouncer.GetWorldPosition(), Player.GetWorldPosition(), color);
                            Game.DrawArea(Player.GetAABB(), color);
                        }, 2000);
                    }
                    break;
                }
            }
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);
            if (m_bouncer != null)
            {
                m_bouncer.SetGuardTarget(null);
                m_occupiedBouncerIDs.Remove(m_bouncer.UniqueID);
            }
        }
    }
}
