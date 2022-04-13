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
            public IObjectWeldJoint WeldJoint;
        }
        public AssassinBot(BotArgs args) : base(args) { _team = args.Player.GetTeam(); }

        private static Dictionary<PlayerTeam, AssassinInfo> AssassinInfos = new Dictionary<PlayerTeam, AssassinInfo>
        {
            { PlayerTeam.Team1, new AssassinInfo() },
            { PlayerTeam.Team2, new AssassinInfo() },
            { PlayerTeam.Team3, new AssassinInfo() },
            { PlayerTeam.Team4, new AssassinInfo() },
        };

        private PlayerTeam _team;

        public override void OnSpawn()
        {
            base.OnSpawn();
            FindTarget(_team);
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

            var targetPos = assInfo.Target.GetWorldPosition();
            targetPos.Y += 35;

            if (assInfo.TargetIndicator == null)
            {
                assInfo.TargetIndicator = Game.CreateObject("Target00");
            }
            else
            {
                assInfo.WeldJoint.Remove();
            }
            assInfo.TargetIndicator.SetWorldPosition(targetPos);
            assInfo.TargetIndicator.SetBodyType(BodyType.Dynamic);
            assInfo.TargetIndicator.SetColor1("Neon" + ScriptHelper.GetTeamColorText(team));
            assInfo.WeldJoint = ScriptHelper.Weld(assInfo.Target, assInfo.TargetIndicator);

            var cb = (Events.PlayerDeathCallback)null;
            cb = Events.PlayerDeathCallback.Start((player) =>
            {
                if (GetTeammates(team).All(x => x.Player.IsDead))
                {
                    if (assInfo.WeldJoint != null)
                    {
                        assInfo.WeldJoint.Remove();
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
