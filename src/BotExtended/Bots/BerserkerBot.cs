using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class BerserkerBot : Bot
    {
        public BerserkerBot(BotArgs args) : base(args)
        {
            _isElapsedCheckHealth = ScriptHelper.WithIsElapsed(200);
        }

        private Func<bool> _isElapsedCheckHealth;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;
            if (_isElapsedCheckHealth())
            {
                SetHealth(Player.GetHealth() - 1, true);
            }
        }

        public override void OnDealDamage(IPlayer victim, PlayerDamageArgs arg)
        {
            base.OnDealDamage(victim, arg);
            
            if (arg.DamageType != PlayerDamageEventType.Melee) return;
            SetHealth(Player.GetHealth() + 5, true);
        }
    }
}
