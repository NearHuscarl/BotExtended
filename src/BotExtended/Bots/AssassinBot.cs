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
        public AssassinBot(BotArgs args) : base(args) { }

        private static Dictionary<PlayerTeam, IPlayer> Targets = new Dictionary<PlayerTeam, IPlayer>
        {
            { PlayerTeam.Team1, null },
            { PlayerTeam.Team2, null },
            { PlayerTeam.Team3, null },
            { PlayerTeam.Team4, null },
        };

        public IPlayer Target
        {
            get
            {
                if (Player.GetTeam() == PlayerTeam.Independent) return null;
                return Targets[Player.GetTeam()];
            }
            set
            {
                if (Player.GetTeam() == PlayerTeam.Independent) return;
                Targets[Player.GetTeam()] = value;
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

            var potentialTargets = new List<IPlayer>();
            foreach (var player in Game.GetPlayers())
            {
                if (!ScriptHelper.SameTeam(player, Player) && !player.IsDead)
                    potentialTargets.Add(player);
            }

            if (potentialTargets.Count > 0)
            {
                Target = RandomHelper.GetItem(potentialTargets);
                Game.CreateDialogue("Target: " + Target.Name, DialogueColor, Player, duration: 3000f);
                
                var targetPos = Target.GetWorldPosition();
                var weldJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint", targetPos);
                var targetIndicator = Game.CreateObject("Target00", new Vector2(targetPos.X, targetPos.Y + 35));
                var teamColor = new Dictionary<PlayerTeam, string> { { PlayerTeam.Team1, "Blue" }, { PlayerTeam.Team2, "Red" }, { PlayerTeam.Team3, "Green" }, { PlayerTeam.Team4, "Yellow" }, };

                targetIndicator.SetBodyType(BodyType.Dynamic);
                targetIndicator.SetColor1("Neon" + teamColor[Player.GetTeam()]);

                weldJoint.AddTargetObject(Target);
                weldJoint.AddTargetObject(targetIndicator);

                Events.PlayerDeathCallback cb = null;
                cb = Events.PlayerDeathCallback.Start((player) =>
                {
                    if (Target == null || player.UniqueID == Target.UniqueID)
                    {
                        Target = null; weldJoint.Remove(); targetIndicator.Remove();
                        FindTarget();
                        cb.Stop();
                    }
                });
            }

            var assassinTeam = BotManager.GetBots<AssassinBot>()
                .Where(x => ScriptHelper.SameTeam(x.Player, Player));

            foreach (var bot in assassinTeam)
                bot.Player.SetForcedBotTarget(Target);
        }
    }
}
