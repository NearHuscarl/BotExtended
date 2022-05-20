using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus.Challenges
{
    public class ChonkyChallenge : Challenge
    {
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
