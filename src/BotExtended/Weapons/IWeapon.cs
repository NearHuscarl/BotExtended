using SFDGameScriptInterface;
using System.Collections.Generic;

namespace BotExtended.Weapons
{
    interface IWeapon
    {
        List<IObject> Components { get; }

        void Update(float elapsed);
        void OnDamage(IObject component);
    }
}
