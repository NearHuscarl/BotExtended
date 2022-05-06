using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class AssassinBot : Bot
    {
        class AssassinInfo
        {
            public IPlayer Target;
            public IObject TargetIndicator;
        }

        private static Dictionary<PlayerTeam, AssassinInfo> AssassinInfos = new Dictionary<PlayerTeam, AssassinInfo>
        {
            { PlayerTeam.Team1, new AssassinInfo() },
            { PlayerTeam.Team2, new AssassinInfo() },
            { PlayerTeam.Team3, new AssassinInfo() },
            { PlayerTeam.Team4, new AssassinInfo() },
        };
        static AssassinBot()
        {
            Events.UpdateCallback.Start(_ =>
            {
                foreach (var assInfo in AssassinInfos.Values)
                {
                    if (assInfo.TargetIndicator == null || assInfo.Target == null) continue;
                    assInfo.TargetIndicator.SetWorldPosition(assInfo.Target.GetWorldPosition() + Vector2.UnitY * 35);
                }
            });
        }
        
        public AssassinBot(BotArgs args) : base(args) { _team = args.Player.GetTeam(); }

        private PlayerTeam _team;

        public override void OnSpawn()
        {
            base.OnSpawn();
            FindTarget(_team);
            if (_team != PlayerTeam.Independent)
                Player.SetForcedBotTarget(AssassinInfos[_team].Target);
        }

        private static void FindTarget(PlayerTeam team)
        {
            if (team == PlayerTeam.Independent) return;

            var assInfo = AssassinInfos[team];
            if (assInfo.Target != null) return;

            var potentialTargets = Game.GetPlayers().Where(p => !ScriptHelper.SameTeam(p, team) && !p.IsDead && BotManager.GetBot(p).Type != BotType.Spy).ToList();
            if (potentialTargets.Count == 0) return;

            assInfo.Target = RandomHelper.GetItem(potentialTargets);
            var teammates = GetTeammates(team).ToList();
            var assassin = RandomHelper.GetItem(teammates);
            assassin.SayLine("Target: " + assInfo.Target.Name);

            if (assInfo.TargetIndicator == null)
            {
                assInfo.TargetIndicator = Game.CreateObject("Target00");
            }
            assInfo.TargetIndicator.SetColor1("Neon" + ScriptHelper.GetTeamColorText(team));

            var cb = (Events.PlayerDeathCallback)null;
            cb = Events.PlayerDeathCallback.Start((player) =>
            {
                var enemiesAlive = Game.GetPlayers().Any(p => !p.IsDead && !ScriptHelper.SameTeam(p, team));
                if (!enemiesAlive || GetTeammates(team).All(x => x.Player.IsDead))
                {
                    if (assInfo.TargetIndicator != null || !assInfo.TargetIndicator.IsRemoved)
                    {
                        assInfo.TargetIndicator.Remove();
                    }
                    cb.Stop();
                    return;
                }
                if (assInfo.Target == null || player.UniqueID == assInfo.Target.UniqueID)
                {
                    assInfo.Target = null;
                    FindTarget(team);
                    cb.Stop();
                }
            });

            foreach (var bot in teammates)
                bot.Player.SetForcedBotTarget(assInfo.Target);
        }

        private static IEnumerable<Bot> GetTeammates(PlayerTeam team) { return BotManager.GetBots<AssassinBot>().Where(x => ScriptHelper.SameTeam(x.Player, team)); }
    }
}
