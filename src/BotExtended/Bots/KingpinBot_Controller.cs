using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class KingpinBot_Controller : Controller<KingpinBot>
    {
        private bool _isHoldingPlayerInGrab = false;
        public override void OnUpdate(float elapsed)
        {
            if (Player.IsHoldingPlayerInGrab && Player.IsInputEnabled)
            {
                Player.SetInputEnabled(false);
            }

            if (Player.IsHoldingPlayerInGrab)
            {
                var grabbedPlayer = Game.GetPlayer(Player.HoldingPlayerInGrabID);
                if (grabbedPlayer == null || grabbedPlayer.IsDead)
                    Player.SetInputEnabled(true);
            }

            if (_isHoldingPlayerInGrab != Player.IsHoldingPlayerInGrab)
            {
                if (!Player.IsHoldingPlayerInGrab)
                    if (!Player.IsInputEnabled)
                        Player.SetInputEnabled(true);
            }

            _isHoldingPlayerInGrab = Player.IsHoldingPlayerInGrab;
        }
    }
}
