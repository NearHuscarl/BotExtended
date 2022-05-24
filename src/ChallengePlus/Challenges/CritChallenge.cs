using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class CritChallenge : Challenge
    {
        public CritChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All projectiles deal critical damage 100% of the time."; }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);
            projectile.CritChanceDealtModifier = 100f;
        }
    }
}
