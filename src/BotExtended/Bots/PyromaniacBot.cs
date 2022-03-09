using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class PyromaniacBot : Bot
    {
        public int BoostOnBurnLevel { get; protected set; }
        public int SpeedOnBurnLevel { get; protected set; }

        public PyromaniacBot(BotArgs args) : base(args)
        {
            _isElapsedCheckFire = ScriptHelper.WithIsElapsed(109);
            BoostOnBurnLevel = -1;
            SpeedOnBurnLevel = 1;
        }

        private Func<bool> _isElapsedCheckFire;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (_isElapsedCheckFire())
            {
                var burnLevel = Player.IsBurningInferno ? 2 : Player.IsBurning ? 1 : 0;
                if (BoostOnBurnLevel == burnLevel)
                    Player.SetStrengthBoostTime(15000);
                if (SpeedOnBurnLevel == burnLevel)
                    Player.SetSpeedBoostTime(15000);
            }
        }
    }
}
