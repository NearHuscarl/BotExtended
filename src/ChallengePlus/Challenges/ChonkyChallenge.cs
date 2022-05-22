using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus.Challenges
{
    public class ChonkyChallenge : Challenge
    {
        public ChonkyChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All players are huge, extremely slow, have high melee damage and very strong melee forces."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                mod.SizeModifier = Size.Chonky;
                mod.RunSpeedModifier = Speed.ExtremelySlow;
                mod.SprintSpeedModifier = Speed.ExtremelySlow;
                mod.MeleeForceModifier = MeleeForce.VeryStrong;
                mod.MeleeDamageDealtModifier = DamageDealt.High;
                p.SetModifiers(mod);
            }
        }
    }
}
