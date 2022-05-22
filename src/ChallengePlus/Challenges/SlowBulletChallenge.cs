using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class SlowBulletChallenge : Challenge
    {
        public SlowBulletChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All projectiles have min velocity."; }
        }

        public override void OnProjectileCreated(IProjectile[] projectiles)
        {
            base.OnProjectileCreated(projectiles);

            foreach (var p in projectiles) p.Velocity /= 100;
        }
    }
}
