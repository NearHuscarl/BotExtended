using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public static class ChallengeManager
    {
        private static Challenge _challenge;

        public static void Initialize()
        {
            var names = ScriptHelper.EnumToArray<ChallengeName>().Where(x => x != ChallengeName.None);
            var name = RandomHelper.GetItem(names.ToList());
            _challenge = ChallengeFactory.Create(name);

            ScriptHelper.PrintMessage("Current challenge: " + name);

            Events.UpdateCallback.Start(_challenge.Update);
            Events.PlayerDeathCallback.Start(_challenge.OnPlayerDealth);

            _challenge.OnSpawn(Game.GetPlayers());
        }
    }
}
