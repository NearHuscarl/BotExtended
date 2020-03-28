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
            else
            {
                if (!Player.IsInputEnabled)
                    Player.SetInputEnabled(true);
            }
        }
    }
}
