using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class StrongObjectChallenge : Challenge
    {
        public StrongObjectChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Dynamic objects have infinite health."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            var objs = Game.GetObjects<IObject>().Where(o => ScriptHelper.IsDynamicObject(o));
            foreach (var o in objs) o.SetHealth(Math.Min(o.GetHealth() * 100, 5000));
        }

        public override void OnObjectDamage(IObject o, ObjectDamageArgs args)
        {
            base.OnObjectDamage(o, args);

            if (ScriptHelper.IsDynamicObject(o)) o.SetHealth(o.GetHealth() + args.Damage);
        }
    }
}
