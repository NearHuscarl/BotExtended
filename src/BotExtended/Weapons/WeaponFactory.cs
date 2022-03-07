using BotExtended.Bots;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Weapons
{
    class WeaponFactory
    {
        public static Weapon Create(BeWeapon weapon, object arg)
        {
            switch (weapon)
            {
                case BeWeapon.Chicken:
                    return new Chicken((FarmerBot)arg);
                case BeWeapon.Tripwire:
                    return new TripWire((IPlayer)arg);
                case BeWeapon.Turret:
                    return new Turret((TurretArg)arg);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
