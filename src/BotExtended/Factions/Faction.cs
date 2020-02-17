using BotExtended.Bots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Factions
{
    public class Faction
    {
        public List<SubFaction> SubFactions { get; private set; }
        public float TotalScore { get; private set; }
        public bool HasBoss { get; private set; }

        public Faction(List<SubFaction> subFactions)
        {
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

        public void Spawn(int factionCount)
        {
            if (factionCount == 0) return;

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

                        BotHelper.SpawnBot(botType);
                        factionCountRemaining--;
                        botCountRemainingThisType--;
                    }
                }
                else
                {
                    var botType = subFaction.GetRandomType();

                    BotHelper.SpawnBot(botType, null, true, true, BotHelper.BotTeam, true);
                    factionCountRemaining--;
                }
            }
        }
    }
}
