using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class TrapChallenge : Challenge
    {
        public TrapChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Dynamic objects have 50% health. Destroyed objects spawn either cooked grenades, molotovs or mines."; }
        }

        public static readonly List<string> Traps = new List<string>()
        {
            "WpnGrenadesThrown",
            "WpnMolotovsThrown",
            "WpnMineThrown",
        };

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            var objs = Game.GetObjects<IObject>().Where(o => ScriptHelper.IsDynamicG1(o) && !LootBoxChallenge.DangerousObjects.Contains(o.Name));
            foreach (var o in objs) o.SetHealth(o.GetHealth() / 2);
        }

        public override void OnObjectTerminated(IObject o)
        {
            base.OnObjectTerminated(o);

            if (ScriptHelper.IsDynamicG1(o) && !LootBoxChallenge.DangerousObjects.Contains(o.Name))
                Game.CreateObject(RandomHelper.GetItem(Traps), o.GetWorldPosition());
        }
    }
}
