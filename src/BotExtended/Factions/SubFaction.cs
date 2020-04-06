namespace BotExtended.Factions
{
    public class SubFaction
    {
        public SubFaction(BotType[] types, float weight = 0f)
        {
            Types = types;
            Weight = weight;
        }

        public SubFaction(BotType type, float weight = 0f)
        {
            Types = type == BotType.None ? new BotType[] { } : new BotType[] { type };
            Weight = weight;
        }

        public BotType[] Types { get; private set; }
        public float Weight { get; private set; }
        public bool HasBoss { get { return Weight == 0; } }
    }
}
