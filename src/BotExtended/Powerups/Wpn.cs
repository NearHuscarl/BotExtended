using BotExtended.Library;
using SFDGameScriptInterface;

namespace BotExtended.Powerups
{
    class Wpn
    {
        public WeaponItem Name { get; protected set; }
        public WeaponItemType Type { get; protected set; }
        public IPlayer Owner { get; protected set; }

        public virtual bool IsValidPowerup() { return true; }
        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos) { }
        public virtual void Update(float elapsed) { }

        public Wpn(IPlayer owner, WeaponItem name = WeaponItem.NONE)
        {
            Name = name;
            Type = Mapper.GetWeaponItemType(name);
            Owner = owner;
        }

        public virtual void Remove()
        {
            Name = WeaponItem.NONE;
            Type = WeaponItemType.NONE;
        }
    }
}
