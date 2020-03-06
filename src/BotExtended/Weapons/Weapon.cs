using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Weapons
{
    abstract class Weapon
    {
        public abstract Vector2 Position { get; }
        public abstract IEnumerable<IObject> Components { get; }

        public abstract void Update(float elapsed);
        public virtual void OnDamage(IObject component, ObjectDamageArgs args) { }
        public virtual void OnComponentTerminated(IObject component) { }
    }
}
