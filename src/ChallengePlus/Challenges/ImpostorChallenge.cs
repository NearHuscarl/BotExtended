using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class ImpostorChallenge : Challenge
    {
        public static readonly float SwitchCooldown = 2000;

        public ImpostorChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Players swap body on contact."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            ScriptHelper.Timeout(() =>
            {
                var playerList = players.ToList();
                var shuffledPlayers = RandomHelper.Shuffle(playerList);

                for (var i = 0; i < playerList.Count; i++)
                {
                    var p1 = playerList[i];
                    var p2 = shuffledPlayers[i];
                    if (p1.UniqueID == p2.UniqueID) continue;

                    var u1 = p1.GetUser();
                    var u2 = p2.GetUser();
                    var bs1 = p1.GetBotBehaviorSet();
                    var bs2 = p2.GetBotBehaviorSet();

                    // TODO: wtf is this bug
                    p1.SetUser(u2, flash: false);
                    p2.SetUser(u1, flash: false);
                    p1.SetBotBehaviorSet(bs2);
                    p2.SetBotBehaviorSet(bs1);
                    p1.SetBotBehaviorActive(true);
                    p2.SetBotBehaviorActive(true);

                    p1.SetNametagVisible(false);
                    p1.SetStatusBarsVisible(false);
                }
                //for (var i = 0; i < playerList.Count; i++)
                //{
                //    var p1 = playerList[i];
                //    p1.SetUser(null, flash: false);
                //}
            }, 50);
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            var p = player.Instance;
            Game.DrawText((p.GetUser() == null) + " " + p.GetBotBehaviorActive() + " " + p.GetBotBehavior().PredefinedAI, player.Position);
        }
    }
}
