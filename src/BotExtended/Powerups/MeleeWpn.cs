using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;

namespace BotExtended.Powerups
{
    class MeleeWpn : Wpn
    {
        public MeleeWeaponPowerup Powerup { get; protected set; }

        public MeleeWpn(IPlayer owner) : this(owner, WeaponItem.NONE, MeleeWeaponPowerup.None) { }
        public MeleeWpn(IPlayer owner, WeaponItem name, MeleeWeaponPowerup powerup)
            : base(owner, name)
        {
            Powerup = powerup;
            if (!IsValidPowerup()) throw new Exception("Weapon " + name + " cannot have powerup " + powerup);
        }

        public void Add(WeaponItem name, MeleeWeaponPowerup powerup)
        {
            Name = name;
            Type = Mapper.GetWeaponItemType(name);
            Powerup = powerup;
        }

        public override void Remove()
        {
            base.Remove();
            Powerup = MeleeWeaponPowerup.None;
        }

        public MeleeAction CurrentMeleeAction { get { return BotManager.GetBot(Owner).CurrentMeleeAction; } }
        public virtual void OnMeleeAction(PlayerMeleeHitArg[] args) { }

        public static bool IsSharpWeapon(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.AXE
                || weaponItem == WeaponItem.KATANA
                || weaponItem == WeaponItem.KNIFE
                || weaponItem == WeaponItem.MACHETE;
        }
    }
}
