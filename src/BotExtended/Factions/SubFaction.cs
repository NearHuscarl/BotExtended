using BotExtended.Library;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.GameScript;

namespace BotExtended.Factions
{
    public class SubFaction
    {
        public SubFaction(List<BotType> types, float weight)
        {
            Types = types;
            Weight = weight;
        }

        public SubFaction(BotType type, float weight)
        {
            Types = new List<BotType>() { type };
            Weight = weight;
        }

        public SubFaction(BotType type)
        {
            Types = new List<BotType>() { type };
            Weight = 0f;
        }

        private List<BotType> types;
        public List<BotType> Types
        {
            get { return types; }
            set
            {
                types = value;
                HasBoss = GetInfo(types.First()).IsBoss;
            }
        }
        public float Weight { get; set; }
        public bool HasBoss { get; private set; }
        public BotType GetRandomType()
        {
            return RandomHelper.GetItem(Types);
        }
    }
}
