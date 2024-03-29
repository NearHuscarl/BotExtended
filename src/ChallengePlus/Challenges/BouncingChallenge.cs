﻿using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class BouncingChallenge : Challenge
    {
        public BouncingChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All guns have bouncing powerup."; }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);
            projectile.PowerupBounceActive = true;
        }
    }
}
