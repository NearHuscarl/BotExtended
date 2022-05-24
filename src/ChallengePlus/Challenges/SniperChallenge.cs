using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class SniperChallenge : Challenge
    {
        public SniperChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "You start with a sniper, all projectiles have infinite bouncing and fire powerups."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                p.GiveWeaponItem(WeaponItem.SNIPER);

                var mod = p.GetModifiers();
                mod.RunSpeedModifier = 0;
                mod.SprintSpeedModifier = 0;
                mod.InfiniteAmmo = 1;
                mod.MaxEnergy = 0;
                p.SetModifiers(mod);
            }
        }

        public override void OnUpdate(float e, IProjectile projectile)
        {
            base.OnUpdate(e, projectile);

            if (projectile.BounceCount > 0)
                projectile.BounceCount = 0;
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            projectile.PowerupBounceActive = true;
            projectile.PowerupFireActive = true;
        }
    }
}
