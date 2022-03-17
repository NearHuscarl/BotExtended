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
        public TripWire(IPlayer owner) : base(owner, "Tripwire00", Vector2.Zero)
        {
            Instance.SetBodyType(BodyType.Static);
        }

        private bool _hasGrenade = true;
        private int _tripCount = 0;
        protected override bool OnTrigger(IPlayer player)
        {
            _tripCount++;

            ScriptHelper.Fall(player);
            var vec = player.GetLinearVelocity();
            // move up a bit to remove the friction of the ground and make the enemy 'fly away'
            player.SetLinearVelocity(new Vector2(vec.X, vec.Y + 3));

            if (_hasGrenade)
            {
                var grenade = (IObjectGrenadeThrown)Game.CreateObject("WpnGrenadesThrown", player.GetWorldPosition());
                grenade.SetExplosionTimer(900);
                _hasGrenade = false;
            }

            if (_tripCount >= 3)
            {
                Instance.Remove();
                IsDestroyed = true;
            }

            return false;
        }
    }
}
