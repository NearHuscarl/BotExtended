using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended.Weapons
{
    interface IWeapon
    {
        Vector2 Position { get; }
        List<IObject> Components { get; }

        void Update(float elapsed);
        void OnDamage(IObject component);
    }
}
