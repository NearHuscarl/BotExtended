using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Bots
{
    class KriegbarBot_Controller : Controller<KriegbärBot>
    {
        private int m_oldHoldingPlayerInGrabID = 0;
        public override void OnUpdate(float elapsed)
        {
            if (Player.HoldingPlayerInGrabID != 0 && m_oldHoldingPlayerInGrabID == 0)
            {
                m_oldHoldingPlayerInGrabID = Player.HoldingPlayerInGrabID;
                OnGrabbingPlayer();
            }
            if (Player.HoldingPlayerInGrabID == 0 && m_oldHoldingPlayerInGrabID != 0)
            {
                m_oldHoldingPlayerInGrabID = Player.HoldingPlayerInGrabID;
            }
        }

        private void OnGrabbingPlayer()
        {
            var targets = Actor.GetThrowTargets(Player.HoldingPlayerInGrabID);

            if (!targets.Any())
            {
                ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.FaceAt, 10,
                    (PlayerCommandFaceDirection)(Player.FacingDirection * -1));
            }
        }
    }
}
