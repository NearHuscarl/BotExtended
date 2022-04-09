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

        public sealed override bool IsValidPowerup()
        {
            return PowerupDatabase.IsValidPowerup(Powerup, Name);
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
    }
}
