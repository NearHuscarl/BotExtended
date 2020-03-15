using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class RangeWpn : Wpn
    {
        public RangedWeaponPowerup Powerup { get; protected set; }

        public RangeWpn(IPlayer owner) : base(owner)
        {
            Powerup = RangedWeaponPowerup.None;
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

        public virtual void Update(float elapsed, WeaponItem weapon, float currentAmmo) { }

        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos) { }
    }
}
