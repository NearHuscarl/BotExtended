using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus.Challenges
{
    public class WeakChallenge : Challenge
    {
        public WeakChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All players are weak. Damage taken x3."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                mod.ExplosionDamageTakenModifier = 3f;
                mod.FireDamageTakenModifier = 3f;
                mod.ImpactDamageTakenModifier = 3f;
                mod.MeleeDamageTakenModifier = 3f;
                mod.ProjectileCritChanceTakenModifier = 3f;
                mod.ProjectileDamageTakenModifier = 3f;
                p.SetModifiers(mod);
            }
        }
    }
}
