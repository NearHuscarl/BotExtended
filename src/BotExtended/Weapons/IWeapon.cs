using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended.Weapons
{
    interface IWeapon
    {
        Vector2 Position { get; }
        IEnumerable<IObject> Components { get; }

        void Update(float elapsed);
        void OnDamage(IObject component, ObjectDamageArgs args);
        void OnComponentTerminated(IObject component);
    }
}
