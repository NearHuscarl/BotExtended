using BotExtended.Library;
using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    // Placeholder for now
    class MeleeWpn : Wpn
    {
        public MeleeWeaponPowerup Powerup { get; protected set; }

        public MeleeWpn(IPlayer owner) : base(owner)
        {
            Powerup = MeleeWeaponPowerup.None;
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
    }
}
