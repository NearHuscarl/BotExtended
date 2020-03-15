using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Projectiles
{
    class PlayerWeapon
    {
        public IPlayer Owner { get; private set; }
        public MeleeWpn Melee;
        public RangeWpn Primary { get; set; }
        public RangeWpn Secondary { get; set; }

        public RangeWpn Throwable { get; set; }
        public Wpn Powerup { get; set; }

        public RangeWpn CurrentRangeWeapon
        {
            get
            {
                switch (Owner.CurrentWeaponDrawn)
                {
                    case WeaponItemType.Rifle:
                        return Primary;
                    case WeaponItemType.Handgun:
                        return Secondary;
                    case WeaponItemType.Thrown:
                        return Throwable;
                    default:
                        return null;
                }
            }
        }

        public static PlayerWeapon Empty(IPlayer owner)
        {
            return new PlayerWeapon()
            {
                Owner = owner,
                Melee = new MeleeWpn(owner),
                Primary = new RangeWpn(owner),
                Secondary = new RangeWpn(owner),
                Throwable = new RangeWpn(owner),
                Powerup = new Wpn(owner),
            };
        }
    }
}
