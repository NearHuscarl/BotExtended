using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus.Challenges
{
    public class KickassChallenge : Challenge
    {
        public KickassChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Players have huge melee forces."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                mod.MeleeForceModifier = MeleeForce.OnePunch;
                mod.ImpactDamageTakenModifier = DamageTaken.ExtremelyResistant;
                mod.ProjectileDamageTakenModifier = DamageTaken.VeryResistant;
                mod.MeleeDamageTakenModifier = DamageTaken.VeryResistant;
                p.SetModifiers(mod);
            }
        }
    }
}
