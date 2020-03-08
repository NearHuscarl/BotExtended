using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended.Weapons
{
    abstract class Weapon
    {
        public virtual bool IsDestroyed { get; protected set; }

        public abstract Vector2 Position { get; }
        public abstract IEnumerable<IObject> Components { get; }

        public abstract void Update(float elapsed);
        public virtual void OnDamage(IObject component, ObjectDamageArgs args) { }
        public virtual void OnComponentTerminated(IObject component) { }
    }
}
