using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class RangeWpn : Wpn
    {
        public RangedWeaponPowerup Powerup { get; protected set; }
        
        public RangeWpn(IPlayer owner) : this(owner, WeaponItem.NONE, RangedWeaponPowerup.None) { }
        public RangeWpn(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner)
        {
            Name = name;
            Powerup = powerup;
        }

        public void Add(WeaponItem name, RangedWeaponPowerup powerup)
        {
            Name = name;
            Powerup = powerup;
        }

        public override void Remove()
        {
            base.Remove();
            Powerup = RangedWeaponPowerup.None;
        }

        public virtual void Update(float elapsed) { }

        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos) { }

        public virtual void OnProjectileCreated(IProjectile projectile) { }
        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args) { }
    }
}
