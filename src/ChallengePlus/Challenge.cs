using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public class Challenge
    {
        public ChallengeName Name { get; private set; }
        public Challenge(ChallengeName name) { Name = name; Description = ""; }

        public virtual string Description { get; protected set; }
        public virtual void OnSpawn(IPlayer[] players)
        {
            Game.ShowPopupMessage(string.Format(@"Challenge: {0}
{1}", Name, Description), ScriptColors.WARNING_COLOR);

            ScriptHelper.Timeout(() => Game.HidePopupMessage(), 5000);
        }

        public virtual void Update(float e) { }

        public virtual void OnPlayerDealth(IPlayer player, PlayerDeathArgs args) { }

        public virtual void OnObjectTerminated(IObject[] objs) { }
    }
}
