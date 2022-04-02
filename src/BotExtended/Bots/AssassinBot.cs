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
        public AssassinBot(BotArgs args) : base(args) { }

        private static Dictionary<PlayerTeam, AssassinInfo> AssassinInfos = new Dictionary<PlayerTeam, AssassinInfo>
        {
            { PlayerTeam.Team1, new AssassinInfo() },
            { PlayerTeam.Team2, new AssassinInfo() },
            { PlayerTeam.Team3, new AssassinInfo() },
            { PlayerTeam.Team4, new AssassinInfo() },
        };

        private AssassinInfo AssInfo { get { return AssassinInfos[Player.GetTeam()]; } }
        public IPlayer Target
        {
            get
            {
                if (Player.GetTeam() == PlayerTeam.Independent) return null;
                return AssInfo.Target;
            }
            set
            {
                if (Player.GetTeam() == PlayerTeam.Independent) return;
                AssInfo.Target = value;
            }
        }

        public override void OnSpawn()
        {
            base.OnSpawn();
            FindTarget();
        }

        private void FindTarget()
        {
            if (Player.GetTeam() == PlayerTeam.Independent || Target != null) return;

            var potentialTargets = Game.GetPlayers().Where(p => !ScriptHelper.SameTeam(p, Player) && !p.IsDead).ToList();
            if (potentialTargets.Count > 0)
            {
                Target = RandomHelper.GetItem(potentialTargets);
                Game.CreateDialogue("Target: " + Target.Name, DialogueColor, Player, duration: 3000f);
                
                var targetPos = Target.GetWorldPosition();
                targetPos.Y += 35;
                var teamColor = new Dictionary<PlayerTeam, string> { { PlayerTeam.Team1, "Blue" }, { PlayerTeam.Team2, "Red" }, { PlayerTeam.Team3, "Green" }, { PlayerTeam.Team4, "Yellow" }, };
                
                if (AssInfo.TargetIndicator == null)
                {
                    AssInfo.TargetIndicator = Game.CreateObject("Target00");
                }
                else
                {
                    AssInfo.WeldJoint.Remove();
                }
                AssInfo.WeldJoint = ScriptHelper.Weld(Target, AssInfo.TargetIndicator);
                AssInfo.TargetIndicator.SetWorldPosition(targetPos);
                AssInfo.TargetIndicator.SetBodyType(BodyType.Dynamic);
                AssInfo.TargetIndicator.SetColor1("Neon" + teamColor[Player.GetTeam()]);

                Events.PlayerDeathCallback cb = null;
                cb = Events.PlayerDeathCallback.Start((player) =>
                {
                    if (GetTeammates().All(x => x.Player.IsDead))
                    {
                        if (AssInfo.WeldJoint != null)
                        {
                            AssInfo.WeldJoint.Remove();
                            AssInfo.TargetIndicator.Remove();
                        }
                        return;
                    }
                    if (Target == null || player.UniqueID == Target.UniqueID)
                    {
                        Target = null;
                        FindTarget();
                        cb.Stop();
                    }
                });
            }

            foreach (var bot in GetTeammates())
                bot.Player.SetForcedBotTarget(Target);
        }

        private IEnumerable<Bot> GetTeammates() { return BotManager.GetBots<AssassinBot>().Where(x => ScriptHelper.SameTeam(x.Player, Player)); }
    }
}
