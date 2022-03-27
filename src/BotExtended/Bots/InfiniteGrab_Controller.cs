using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class InfiniteGrab_Controller : Controller<Bot>
    {
        private bool _isHoldingPlayerInGrab = false;
        public override void OnUpdate(float elapsed)
        {
            if (Player.IsHoldingPlayerInGrab)
            {
                if (Player.IsInputEnabled)
                    Player.SetInputEnabled(false);

                var grabbedPlayer = Game.GetPlayer(Player.HoldingPlayerInGrabID);
                if (grabbedPlayer.IsRemoved)
                    Player.SetInputEnabled(true);
            }

            if (_isHoldingPlayerInGrab && !Player.IsHoldingPlayerInGrab)
            {
                if (!Player.IsInputEnabled)
                    Player.SetInputEnabled(true);
            }

            _isHoldingPlayerInGrab = Player.IsHoldingPlayerInGrab;
        }
    }
}
