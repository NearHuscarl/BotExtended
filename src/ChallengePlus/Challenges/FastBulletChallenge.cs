using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class FastBulletChallenge : Challenge
    {
        public FastBulletChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All projectiles have max velocity."; }
        }

        public override void OnProjectileCreated(IProjectile[] projectiles)
        {
            base.OnProjectileCreated(projectiles);

            //foreach (var p in projectiles) p.Velocity /= 100;
            foreach (var p in projectiles) p.Velocity *= 10;
        }

        public override void OnUpdate(float e, IProjectile projectile)
        {
            base.OnUpdate(e, projectile);
            projectile.Position += projectile.Velocity * (e / 1000);
        }
    }
}
