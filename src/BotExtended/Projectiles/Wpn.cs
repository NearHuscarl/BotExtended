using SFDGameScriptInterface;

namespace BotExtended.Projectiles
{
    class Wpn
    {
        public WeaponItem Name { get; protected set; }
        public IPlayer Owner { get; protected set; }

        public Wpn(IPlayer owner)
        {
            Name = WeaponItem.NONE;
            Owner = owner;
        }

        public virtual void Remove()
        {
            Name = WeaponItem.NONE;
            Owner = null;
        }
    }
}
