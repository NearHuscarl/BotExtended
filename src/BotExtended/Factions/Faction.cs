using BotExtended.Bots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Factions
{
    public class Faction
    {
        public BotFaction BotFaction { get; set; }
        public int Index { get; set; }
        public List<SubFaction> SubFactions { get; private set; }
        public float TotalScore { get; private set; }
        public bool HasBoss { get; private set; }

        public Faction(List<SubFaction> subFactions, BotFaction botFaction)
        {
            BotFaction = botFaction;
            SubFactions = subFactions;
            HasBoss = false;

            foreach (var subFaction in subFactions)
            {
                var hasBoss = subFaction.HasBoss;
                if (hasBoss)
                {
                    HasBoss = true;
                    continue;
                }

                TotalScore += subFaction.Weight;
            }
        }

        public IEnumerable<Bot> Spawn(int factionCount)
        {
            var bots = new List<Bot>();
            if (factionCount == 0) return bots;

            var subFactionCount = 0;
            var factionCountRemaining = factionCount;
            var mobCount = HasBoss ? factionCount - 1 : factionCount;

            foreach (var subFaction in SubFactions)
            {
                subFactionCount++;

                if (!subFaction.HasBoss)
                {
                    var weight = subFaction.Weight;
                    var share = weight / TotalScore;
                    var botCountRemainingThisType = Math.Round(mobCount * share);

                    while (factionCountRemaining > 0 && (botCountRemainingThisType > 0 || subFactionCount == SubFactions.Count))
                    {
                        var botType = subFaction.GetRandomType();
                        var bot = BotHelper.SpawnBot(botType, BotFaction);

                        bots.Add(bot);
                        factionCountRemaining--;
                        botCountRemainingThisType--;
                    }
                }
                else
                {
                    var botType = subFaction.GetRandomType();
                    var bot = BotHelper.SpawnBot(botType, BotFaction, null, true, true, BotHelper.BotTeam, true);

                    bots.Add(bot);
                    factionCountRemaining--;
                }
            }
            return bots;
        }
    }
}
