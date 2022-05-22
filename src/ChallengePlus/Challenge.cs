using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public class Challenge : ChallengeBase<object>
    {
        public Challenge(ChallengeName name) : base(name) { }
    }
}
