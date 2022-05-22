using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public class ChallengeBase<PlayerData> : IChallenge where PlayerData : new()
    {
        public override ChallengeName Name { get; protected set; }
        public ChallengeBase(ChallengeName name) { Name = name; Description = ""; }

        public override string Description { get; protected set; }
        public override void OnSpawn(IPlayer[] players)
        {
            Game.ShowPopupMessage(string.Format(@"Challenge: {0}
{1}", Name, Description), ScriptColors.WARNING_COLOR);

            ScriptHelper.Timeout(() => Game.HidePopupMessage(), 5000);
        }

        public override void OnUpdate(float e)
        {
            foreach (var p in Game.GetProjectiles()) OnUpdate(e, p);
        }

        protected static readonly Dictionary<int, PlayerData> PData = new Dictionary<int, PlayerData>();
        public override void OnPlayerCreated(Player player)
        {
            PData[player.UniqueID] = new PlayerData();
        }

        protected PlayerData GetPlayerData(int uniqueID)
        {
            PlayerData pData;
            if (PData.TryGetValue(uniqueID, out pData)) return pData;
            return default(PlayerData);
        }

        public override void OnUpdate(float e, Player player) { }

        public override void OnPlayerDealth(Player player, PlayerDeathArgs args)
        {
            PData.Remove(player.UniqueID);
        }

        public override void OnObjectTerminated(IObject[] objs) { }

        public override void OnPlayerKeyInput(Player player, VirtualKeyInfo[] keyInfos) { }
    }
}
