using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class FireChallenge : Challenge
    {
        public FireChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All guns have fire powerup."; }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            projectile.PowerupFireActive = true;
            projectile.DamageDealtModifier = 0.01f;
        }
    }
}
