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
    public class TripWire : Weapon
    {
        public override IEnumerable<IObject> Components { get; set; }
        private Dictionary<int, float> _trappedTimes = new Dictionary<int, float>();

        public TripWire(IPlayer owner) : base(owner)
        {
            var pos = Owner.GetWorldPosition();
            Instance = Game.CreateObject("Pulley00", new Vector2(pos.X, pos.Y + 5));
            Instance.SetBodyType(BodyType.Static);
            Instance.CustomID = "Tripwire";
            Components = new List<IObject>() { Instance };
        }

        public override void Update(float elapsed)
        {
            foreach (var p in Game.GetPlayers())
            {
                if (Instance.GetAABB().Intersects(p.GetAABB()))
                {
                    var trappedTime = -1f;
                    if (_trappedTimes.TryGetValue(p.UniqueID, out trappedTime))
                    {
                        if (ScriptHelper.IsElapsed(trappedTime, 2000))
                        {
                            if (!ScriptHelper.SameTeam(p, Owner) || RandomHelper.Percentage(.25f))
                                TriggerTrap(p);
                            _trappedTimes[p.UniqueID] = Game.TotalElapsedGameTime;
                        }
                    }
                    else
                        _trappedTimes.Add(p.UniqueID, Game.TotalElapsedGameTime);
                }
            }
        }

        private bool _hasGrenade = true;
        private void TriggerTrap(IPlayer player)
        {
            ScriptHelper.Fall(player);
            var vec = player.GetLinearVelocity();
            // move up a bit to remove the friction of the ground and make the enemy 'fly away'
            player.SetLinearVelocity(new Vector2(vec.X, vec.Y + 5));

            if (_hasGrenade)
            {
                Game.CreateObject("WpnGrenadesThrown", player.GetWorldPosition());
                _hasGrenade = false;
            }
        }
    }
}
