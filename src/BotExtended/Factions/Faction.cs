using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Factions
{
    public class Faction
    {
        public BotFaction BotFaction { get; private set; }
        public List<SubFaction> SubFactions { get; private set; }
        public float TotalScore { get; private set; }
        public bool HasBoss { get; private set; }
        public List<BotType> Bosses { get; private set; }

        public Faction(List<SubFaction> subFactions, BotFaction botFaction)
        {
            BotFaction = botFaction;
            SubFactions = new List<SubFaction>();
            HasBoss = false;
            Bosses = new List<BotType>();

            foreach (var subFaction in subFactions)
            {
                if (subFaction.Types.Length == 0) continue;

                if (subFaction.HasBoss)
                {
                    HasBoss = true;
                    Bosses.Add(subFaction.Types.Single());
                }
                else
                    TotalScore += subFaction.Weight;

                SubFactions.Add(subFaction);
            }
        }

        public IEnumerable<Bot> Spawn(int factionCount, PlayerTeam team)
        {
            return Spawn(factionCount, team, (botType, isBoss) =>
            {
                if (isBoss)
                    return BotManager.SpawnBot(botType, BotFaction, null, team, ignoreFullSpawner: true, triggerOnSpawn: false);
                else
                    return BotManager.SpawnBot(botType, BotFaction, null, team, triggerOnSpawn: false);
            });
        }

        private IEnumerable<Bot> Spawn(int factionCount, PlayerTeam team, Func<BotType, bool, Bot> spawnCallback)
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
                        var botType = RandomHelper.GetItem(subFaction.Types);
                        var bot = spawnCallback(botType, false);
                        if (bot != null)
                            bots.Add(bot);

                        factionCountRemaining--;
                        botCountRemainingThisType--;
                    }
                }
                else
                {
                    var botType = RandomHelper.GetItem(subFaction.Types);
                    var bot = spawnCallback(botType, true);
                    if (bot != null)
                        bots.Add(bot);

                    factionCountRemaining--;
                }
            }
            return bots;
        }
    }
}
