using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class DiseaseChallenge : Challenge
    {
        public DiseaseChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Players' health reduce gradually, deal damage to heal."; }
        }

        private readonly Func<float, bool> _isElapsedDisease = ScriptHelper.WithIsElapsed();
        public override void OnUpdate(float e)
        {
            base.OnUpdate(e);

            if (!_isElapsedDisease(300)) return;

            foreach (var p in PlayerManager.GetPlayers()) p.DealDamage(1f);
        }

        public override void OnPlayerDamage(Player player, PlayerDamageArgs args, Player attacker)
        {
            base.OnPlayerDamage(player, args, attacker);

            if (attacker == null || player.IsDead) return;
            attacker.Instance.SetHealth(attacker.Instance.GetHealth() + 5);
        }
    }
}
