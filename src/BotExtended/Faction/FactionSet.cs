using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Faction
{
    public class FactionSet
    {
        public List<Faction> Factions { get; set; }

        public FactionSet()
        {
            Factions = new List<Faction>();
        }
        public bool HasBoss
        {
            get { return Factions.Where(g => g.HasBoss).Any(); }
        }

        public void AddFaction(List<SubFaction> subFactions)
        {
            Factions.Add(new Faction(subFactions));
        }
    }
}
