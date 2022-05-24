using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class LootBoxChallenge : Challenge
    {
        public LootBoxChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Dynamic objects have 50% health. Destroyed objects spawn weapons."; }
        }

        public static readonly HashSet<string> DangerousObjects = new HashSet<string>()
        {
            "BarrelExplosive",
            "PropaneTank",
            "Gascan00",
        };

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            var objs = Game.GetObjects<IObject>().Where(o => ScriptHelper.IsDynamicG1(o) && !DangerousObjects.Contains(o.Name));
            foreach (var o in objs) o.SetHealth(o.GetHealth() / 2);
        }

        public override void OnObjectTerminated(IObject o)
        {
            base.OnObjectTerminated(o);

            if (ScriptHelper.IsDynamicG1(o) && !DangerousObjects.Contains(o.Name))
                Game.CreateObject(RandomHelper.GetItem(Constants.WeaponNames), o.GetWorldPosition());
        }
    }
}
