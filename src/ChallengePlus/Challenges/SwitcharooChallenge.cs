using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class SwitcharooChallenge : ChallengeBase<SwitcharooChallenge.PlayerData>
    {
        public static readonly float SwitchCooldown = 2000;

        public SwitcharooChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Players swap body on contact."; }
        }

        public class PlayerData
        {
            public float LastSwapTime = 0;
            public readonly Func<float, bool> IsElapsedCheckSwap = ScriptHelper.WithIsElapsed();
        }

        private bool CanBeSwitched(Player player)
        {
            var pData = GetPlayerData(player.UniqueID);
            if (pData == null) return false;

            return ScriptHelper.IsElapsed(pData.LastSwapTime, SwitchCooldown);
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            if (player.IsDead) return;

            var pData = GetPlayerData(player.UniqueID);
            if (pData == null) return;

            if (!pData.IsElapsedCheckSwap(200)) return;

            var swappedPlayer = PlayerManager.GetPlayers()
                .FirstOrDefault(p => !p.IsDead && p.UniqueID != player.UniqueID && CanBeSwitched(p) && p.Hitbox.Intersects(player.Hitbox));
            if (swappedPlayer == null) return;

            if (CanBeSwitched(player))
            {
                var p1 = player;
                var p2 = swappedPlayer;
                var u1 = p1.Instance.GetUser();
                var u2 = p2.Instance.GetUser();
                var bs1 = p1.Instance.GetBotBehaviorSet();
                var bs2 = p2.Instance.GetBotBehaviorSet();

                p1.Instance.SetUser(u2, flash: false);
                p2.Instance.SetUser(u1, flash: false);
                p2.Instance.SetBotBehaviorSet(bs1);
                p1.Instance.SetBotBehaviorSet(bs2);
                p1.Instance.SetBotBehaviorActive(true);
                p2.Instance.SetBotBehaviorActive(true);

                GetPlayerData(p1.UniqueID).LastSwapTime = Game.TotalElapsedGameTime;
                GetPlayerData(p2.UniqueID).LastSwapTime = Game.TotalElapsedGameTime;

                Game.PlayEffect(EffectName.CustomFloatText, p1.Position + ((p2.Position - p1.Position) / 2), "swapped");
            }
        }
    }
}
