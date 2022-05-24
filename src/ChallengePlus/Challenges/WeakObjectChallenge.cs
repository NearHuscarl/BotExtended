using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class WeakObjectChallenge : Challenge
    {
        public WeakObjectChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Dynamic objects have 20 health max."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            var objs = Game.GetObjects<IObject>().Where(o => ScriptHelper.IsDynamicObject(o));
            foreach (var o in objs) o.SetHealth(Math.Min(o.GetHealth() / 10, 20));
        }
    }
}
