using ChallengePlus.Challenges;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus
{
    public static class ChallengeFactory
    {
        public static IChallenge Create(ChallengeName name)
        {
            switch (name)
            {
                case ChallengeName.Athelete:
                    return new AtheleteChallenge(name);
                case ChallengeName.Bouncing:
                    return new BouncingChallenge(name);
                case ChallengeName.BuffGun:
                    return new BuffGunChallenge(name);
                case ChallengeName.BuffMelee:
                    return new BuffMeleeChallenge(name);
                case ChallengeName.Chonky:
                    return new ChonkyChallenge(name);
                case ChallengeName.Crit:
                    return new CritChallenge(name);
                case ChallengeName.Disease:
                    return new DiseaseChallenge(name);
                case ChallengeName.FastBullet:
                    return new FastBulletChallenge(name);
                case ChallengeName.Fire:
                    return new FireChallenge(name);
                case ChallengeName.Kickass:
                    return new KickassChallenge(name);
                case ChallengeName.Minesweeper:
                    return new MinesweeperChallenge(name);
                case ChallengeName.Moonwalk:
                    return new MoonwalkChallenge(name);
                case ChallengeName.SlowBullet:
                    return new SlowBulletChallenge(name);
                case ChallengeName.SpecificWeapon:
                    return new SpecificWpnChallenge(name);
                case ChallengeName.Tiny:
                    return new TinyChallenge(name);
                case ChallengeName.Unstable:
                    return new UnstableChallenge(name);
                case ChallengeName.Weak:
                    return new WeakChallenge(name);
                default:
                    throw new ArgumentException("Challenge name " + name + " is not implemented");
            }
        }
    }
}
