using ChallengePlus.Challenges;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus
{
    public static class ChallengeFactory
    {
        public static Challenge Create(ChallengeName name)
        {
            switch (name)
            {
                case ChallengeName.Tiny:
                    return new TinyChallenge();
                case ChallengeName.Chonky:
                    return new ChonkyChallenge();
                default:
                    throw new ArgumentException("Challenge name " + name + " is not implemented");
            }
        }
    }
}
