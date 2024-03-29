﻿using BotExtended.Bots;
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
                case BeWeapon.Camp:
                    return new Camp((GangsterBot)arg);
                case BeWeapon.Chicken:
                    return new Chicken((FarmerBot)arg);
                case BeWeapon.FireTrap:
                    return new FireTrap((IPlayer)arg);
                case BeWeapon.LaserSweeper:
                    return new LaserSweeper((MetroCopBot)arg);
                case BeWeapon.ShotgunTrap:
                    return new ShotgunTrap((IPlayer)arg);
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
