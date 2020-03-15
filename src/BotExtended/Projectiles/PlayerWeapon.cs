using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Projectiles
{
    class PlayerWeapon
    {
        public MeleeWpn Melee;
        public RangeWpn Primary { get; set; }
        public RangeWpn Secondary { get; set; }

        public RangeWpn Throwable { get; set; }
        public Wpn Powerup { get; set; }

        public static PlayerWeapon Empty
        {
            get
            {
                return new PlayerWeapon()
                {
                    Melee = new MeleeWpn(null),
                    Primary = new RangeWpn(null),
                    Secondary = new RangeWpn(null),
                    Throwable = new RangeWpn(null),
                    Powerup = new Wpn(null),
                };
            }
        }
    }
}
