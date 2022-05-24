using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class DangerChallenge : Challenge
    {
        public DangerChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Dynamic objects are explosion barrels, small objects are propane tanks or gas cans."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            var objs = Game.GetObjects<IObject>().Where(o => ScriptHelper.IsDynamicG1(o));
            foreach (var o in objs)
            {
                var size = o.GetSize();

                if (size.X <= 8) continue;
                if (size.Y >= 16)
                    Game.CreateObject("BarrelExplosive", o.GetWorldPosition());
                else if (size.Y >= 11)
                    Game.CreateObject("PropaneTank", o.GetWorldPosition());
                else if (size.Y >= 10)
                    Game.CreateObject("Gascan00", o.GetWorldPosition());

                o.Remove();
            }
        }
    }
}
