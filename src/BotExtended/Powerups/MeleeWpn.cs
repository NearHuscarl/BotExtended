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

        public override bool IsValidPowerup()
        {
            return Name != WeaponItem.CHAINSAW || Powerup == MeleeWeaponPowerup.None;
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

        private MeleeAction _lastMeleeAction;
        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Powerup == MeleeWeaponPowerup.None) return;

            if (_lastMeleeAction != CurrentMeleeAction)
            {
                var hitPosition = Owner.GetWorldPosition() + Vector2.UnitX * Owner.GetFaceDirection() * 12;
                OnMeleeActionChanged(CurrentMeleeAction, hitPosition);
                _lastMeleeAction = CurrentMeleeAction;
            }
        }

        protected virtual void OnMeleeActionChanged(MeleeAction meleeAction, Vector2 hitPosition) { }
        public MeleeAction CurrentMeleeAction { get { return BotManager.GetBot(Owner).CurrentMeleeAction; } }
        public virtual void OnMeleeAction(PlayerMeleeHitArg[] args) { }

        public static bool IsSharpWeapon(WeaponItem weaponItem)
        {
            return weaponItem == WeaponItem.AXE
                || weaponItem == WeaponItem.KATANA
                || weaponItem == WeaponItem.KNIFE
                || weaponItem == WeaponItem.MACHETE;
        }

        public static bool IsHitTheFloorWeapon(WeaponItem weaponItem)
        {
            // weapons that have the beat-the-ground animation on third attack
            return weaponItem == WeaponItem.MACHETE
                || weaponItem == WeaponItem.AXE
                || weaponItem == WeaponItem.BAT
                || weaponItem == WeaponItem.BATON
                || weaponItem == WeaponItem.SHOCK_BATON
                || weaponItem == WeaponItem.PIPE
                || weaponItem == WeaponItem.HAMMER
                || weaponItem == WeaponItem.LEAD_PIPE
                || weaponItem == WeaponItem.KATANA;
        }
    }
}
