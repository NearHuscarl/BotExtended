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
        private IPlayer m_target;

        public HunterBot(BotArgs args) : base(args) { }

        private float m_updateDelay = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;
            if (m_target != null) Game.DrawArea(m_target.GetAABB(), Color.Magenta);

            if (ScriptHelper.IsElapsed(m_updateDelay, 1000))
            {
                if (m_target == null || m_target.IsDead)
                    SearchJuicyBenny();
            }
            TryKeepDistance();
        }

        private float m_oldDistance = 0f;
        private void TryKeepDistance()
        {
            if (m_target == null || m_target.IsDead) return;

            var distanceToTarget = Vector2.Distance(Position, m_target.GetWorldPosition());
            if (distanceToTarget < 35 && m_oldDistance >= 35)
            {
                ScriptHelper.Timeout(() =>
                {
                    m_target = null;
                    Player.SetGuardTarget(null);
                }, 4000);
            }
            m_oldDistance = distanceToTarget;
        }

        private void SearchJuicyBenny()
        {
            foreach (var p in Game.GetPlayers())
            {
                if (p.IsRemoved || p.IsDead) continue;
                var skinName = p.GetProfile().Skin.Name;
                var isBear = skinName == "FrankenbearSkin" || skinName == "BearSkin";

                if (isBear && !ScriptHelper.SameTeam(p, Player))
                {
                    m_target = p;
                    Player.SetGuardTarget(m_target);
                    break;
                }
            }
        }
    }
}
