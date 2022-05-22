using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class UnstableChallenge : Challenge
    {
        public UnstableChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Players have 15% chance of exploding after taking damage."; }
        }

        public override void OnPlayerDamage(Player player, PlayerDamageArgs args, Player attacker)
        {
            base.OnPlayerDamage(player, args, attacker);

            if (RandomHelper.Percentage(.15f))
                Game.TriggerExplosion(player.Position);
        }
    }
}
