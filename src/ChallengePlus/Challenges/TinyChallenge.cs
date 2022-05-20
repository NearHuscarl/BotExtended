using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus.Challenges
{
    public class TinyChallenge : Challenge
    {
        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                mod.SizeModifier = Size.Tiny;
                mod.RunSpeedModifier = Speed.ExtremelyFast;
                mod.SprintSpeedModifier = Speed.ExtremelyFast;
                mod.MeleeForceModifier = MeleeForce.Weak;
                mod.MeleeDamageDealtModifier = DamageDealt.Low;
                p.SetModifiers(mod);
            }
        }
    }
}
