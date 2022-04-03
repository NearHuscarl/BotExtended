using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class HunterBot : Bot
    {
        public HunterBot(BotArgs args) : base(args)
        {
            _isElapsedCheckTarget = ScriptHelper.WithIsElapsed(1000);
        }

        private Func<bool> _isElapsedCheckTarget;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (_isElapsedCheckTarget())
            {
                var target = Player.GetForcedBotTarget();
                if (target == null || (ScriptHelper.IsPlayer(target) && ScriptHelper.AsPlayer(target).IsDead))
                {
                    target = Game.GetPlayers().Where(p => !p.IsDead && !ScriptHelper.SameTeam(p, Player) && ScriptHelper.IsBear(p)).FirstOrDefault();
                    if (target != null)
                        Player.SetForcedBotTarget(target);
                }
            }
        }
    }
}
