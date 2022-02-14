using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Factions
{
    public class FactionSet
    {
        public BotFaction Faction { get; private set; }
        public List<Faction> Factions { get; private set; }

        public FactionSet(BotFaction faction)
        {
            Faction = faction;
            Factions = new List<Faction>();
        }

        public void AddFaction(List<SubFaction> subFactions)
        {
            Factions.Add(new Faction(subFactions, Faction));
        }
        public void AddFaction(SubFaction subFaction)
        {
            AddFaction(new List<SubFaction> { subFaction });
        }
    }
}
