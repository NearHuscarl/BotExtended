using ChallengePlus.Challenges;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus
{
    public static class ChallengeFactory
    {
        public static HashSet<ChallengeName> ExperimentalChallenges = new HashSet<ChallengeName>
        {
            ChallengeName.Moonwalk,
            ChallengeName.Impostor,
        };

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
                case ChallengeName.Danger:
                    return new DangerChallenge(name);
                case ChallengeName.Disease:
                    return new DiseaseChallenge(name);
                case ChallengeName.Drug:
                    return new DrugChallenge(name);
                case ChallengeName.FastBullet:
                    return new FastBulletChallenge(name);
                case ChallengeName.Fire:
                    return new FireChallenge(name);
                case ChallengeName.Impostor:
                    return new ImpostorChallenge(name);
                case ChallengeName.InfiniteAmmo:
                    return new InfiniteAmmoChallenge(name);
                case ChallengeName.Kickass:
                    return new KickassChallenge(name);
                case ChallengeName.LootBox:
                    return new LootBoxChallenge(name);
                case ChallengeName.Minesweeper:
                    return new MinesweeperChallenge(name);
                case ChallengeName.Moonwalk:
                    return new MoonwalkChallenge(name);
                case ChallengeName.Nuclear:
                    return new NuclearChallenge(name);
                case ChallengeName.Precision:
                    return new PrecisionChallenge(name);
                case ChallengeName.SlowBullet:
                    return new SlowBulletChallenge(name);
                case ChallengeName.Sniper:
                    return new SniperChallenge(name);
                case ChallengeName.SpecificWeapon:
                    return new SpecificWpnChallenge(name);
                case ChallengeName.StrongObject:
                    return new StrongObjectChallenge(name);
                case ChallengeName.Switcharoo:
                    return new SwitcharooChallenge(name);
                case ChallengeName.Tiny:
                    return new TinyChallenge(name);
                case ChallengeName.Trap:
                    return new TrapChallenge(name);
                case ChallengeName.Unstable:
                    return new UnstableChallenge(name);
                case ChallengeName.Weak:
                    return new WeakChallenge(name);
                case ChallengeName.WeakObject:
                    return new WeakObjectChallenge(name);
                default:
                    throw new ArgumentException("Challenge name " + name + " is not implemented");
            }
        }
    }
}
