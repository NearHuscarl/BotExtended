using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Factions
{
    public class FactionSet
    {
        public BotFaction Faction { get; set; }
        public List<Faction> Factions { get; set; }

        public FactionSet(BotFaction faction)
        {
            Faction = faction;
            Factions = new List<Faction>();
        }
        public bool HasBoss
        {
            get { return Factions.Where(g => g.HasBoss).Any(); }
        }

        public void AddFaction(List<SubFaction> subFactions)
        {
            Factions.Add(new Faction(subFactions, Faction));
            var factionIndex = Factions.Count - 1;
            Factions[factionIndex].Index = factionIndex;
        }
    }
}
