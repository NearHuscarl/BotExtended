using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Weapons
{
    public class TripWire : Trap
    {
        private Dictionary<int, float> _trappedTimes = new Dictionary<int, float>();

        public TripWire(IPlayer owner) : base(owner, "Tripwire00", Vector2.Zero) { }

        protected override bool OnTrigger(IPlayer player)
        {
            base.OnTrigger(player);
            
            var trappedTime = -1f;
            if (_trappedTimes.TryGetValue(player.UniqueID, out trappedTime))
            {
                if (ScriptHelper.IsElapsed(trappedTime, 2000))
                {
                    TriggerTrap(player);
                    _trappedTimes[player.UniqueID] = Game.TotalElapsedGameTime;
                }
            }
            else
            {
                TriggerTrap(player);
                _trappedTimes.Add(player.UniqueID, Game.TotalElapsedGameTime);
            }
            return false;
        }

        private bool _hasGrenade = true;
        private int _tripCount = 0;
        private void TriggerTrap(IPlayer player)
        {
            _tripCount++;

            ScriptHelper.Fall(player);
            var vec = player.GetLinearVelocity();
            // move up a bit to remove the friction of the ground and make the enemy 'fly away'
            player.SetLinearVelocity(new Vector2(vec.X, vec.Y + 3));

            if (_hasGrenade)
            {
                Game.CreateObject("WpnGrenadesThrown", player.GetWorldPosition());
                _hasGrenade = false;
            }

            if (_tripCount >= 3)
            {
                Instance.Remove();
                IsDestroyed = true;
            }
        }
    }
}
