using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended.Weapons
{
    public abstract class Weapon
    {
        public virtual bool IsDestroyed { get; protected set; }

        public IPlayer Owner { get; protected set; }
        public PlayerTeam Team { get; private set; }

        public Weapon(IPlayer owner)
        {
            Owner = owner;
            Team = owner == null ? PlayerTeam.Independent : owner.GetTeam();
        }

        public virtual Vector2 Position { get { return Instance.GetWorldPosition(); } }
        public abstract IEnumerable<IObject> Components { get; set; }
        // if the Components only have one item, then you can also reference it in an Instance
        public IObject Instance { get; protected set; }

        public abstract void Update(float elapsed);
        public virtual void OnDamage(IObject component, ObjectDamageArgs args) { }
        public virtual void OnComponentTerminated(IObject component) { }
    }
}
