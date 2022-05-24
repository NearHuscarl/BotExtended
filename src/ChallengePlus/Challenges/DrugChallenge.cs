using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class DrugChallenge : Challenge
    {
        public static readonly float IdleMaxTime = 1500;

        public DrugChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All players have unlimited strengthboost and speedboost."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                p.SetStrengthBoostTime(float.MaxValue);
                p.SetSpeedBoostTime(float.MaxValue);
            }
        }
    }
}
