using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class AtheleteChallenge : ChallengeBase<AtheleteChallenge.PlayerData>
    {
        public AtheleteChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Players explode if standing for too long."; }
        }

        public class PlayerData
        {
            public float LastIdleTime = -1;
            public bool IsIdle = false;
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            // TODO: add indicator

            // a bit of cooldown so our althletes can warmup
            if (Game.TotalElapsedGameTime < 5000) return;

            var pData = GetPlayerData(player.UniqueID);
            if (pData == null) return;

            var p = player.Instance;
            var isIdle = (p.IsIdle || p.IsManualAiming) && player.Velocity.Length() < 1;
            if (!pData.IsIdle && isIdle)
                pData.LastIdleTime = Game.TotalElapsedGameTime;
            if (pData.IsIdle && !isIdle)
                pData.LastIdleTime = -1;
            pData.IsIdle = isIdle;

            if (p.Name == "Near")
                Game.DrawText(pData.IsIdle + "", player.Position);

            if (pData.LastIdleTime != -1 && ScriptHelper.IsElapsed(pData.LastIdleTime, 2000))
                Game.TriggerExplosion(player.Position);
        }
    }
}
