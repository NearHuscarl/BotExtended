using BotExtended.Library;
using SFDGameScriptInterface;
using System;

namespace BotExtended.Projectiles
{
    class MuzzleInfo
    {
        public Vector2 Position;
        public Vector2 Direction;
    }

    class RangeWpn : Wpn
    {
        public RangedWeaponPowerup Powerup { get; protected set; }

        virtual public bool IsValidPowerup() { return true; }

        public RangeWpn(IPlayer owner) : this(owner, WeaponItem.NONE, RangedWeaponPowerup.None) { }
        public RangeWpn(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup)
            : base(owner, name)
        {
            Powerup = powerup;
            if (!IsValidPowerup()) throw new Exception("Weapon " + name + " cannot have powerup " + powerup);
        }

        public void Add(WeaponItem name, RangedWeaponPowerup powerup)
        {
            Name = name;
            Type = Mapper.GetWeaponItemType(name);
            Powerup = powerup;
        }

        public override void Remove()
        {
            base.Remove();
            Powerup = RangedWeaponPowerup.None;
        }

        private bool _oldManualAiming = false;
        public virtual void Update(float elapsed)
        {
            if (!_oldManualAiming && Owner.IsManualAiming)
                OnStartManualAim();
            if (_oldManualAiming && !Owner.IsManualAiming)
                OnStopManualAim();
            _oldManualAiming = Owner.IsManualAiming;
        }

        protected virtual void OnStartManualAim() { }
        protected virtual void OnStopManualAim() { }

        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos) { }

        public virtual void OnProjectileCreated(IProjectile projectile) { }
        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args) { }

        public MuzzleInfo GetMuzleInfo()
        {
            Vector2 position, direction;
            if (Owner.GetWeaponMuzzleInfo(out position, out direction)) return new MuzzleInfo
            {
                Position = position,
                Direction = direction
            };
            return null;
        }
    }
}
