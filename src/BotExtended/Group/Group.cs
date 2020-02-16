using BotExtended.Bots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Group
{
    public class Group
    {
        public List<SubGroup> SubGroups { get; private set; }
        public float TotalScore { get; private set; }
        public bool HasBoss { get; private set; }

        public Group(List<SubGroup> subGroups)
        {
            SubGroups = subGroups;
            HasBoss = false;

            foreach (var subGroup in subGroups)
            {
                var hasBoss = subGroup.HasBoss;
                if (hasBoss)
                {
                    HasBoss = true;
                    continue;
                }

                TotalScore += subGroup.Weight;
            }
        }

        public void Spawn(int groupCount)
        {
            if (groupCount == 0) return;

            var subGroupCount = 0;
            var groupCountRemaining = groupCount;
            var mobCount = HasBoss ? groupCount - 1 : groupCount;

            foreach (var subGroup in SubGroups)
            {
                subGroupCount++;

                if (!subGroup.HasBoss)
                {
                    var weight = subGroup.Weight;
                    var share = weight / TotalScore;
                    var botCountRemainingThisType = Math.Round(mobCount * share);

                    while (groupCountRemaining > 0 && (botCountRemainingThisType > 0 || subGroupCount == SubGroups.Count))
                    {
                        var botType = subGroup.GetRandomType();

                        BotHelper.SpawnBot(botType);
                        groupCountRemaining--;
                        botCountRemainingThisType--;
                    }
                }
                else
                {
                    var botType = subGroup.GetRandomType();

                    BotHelper.SpawnBot(botType, null, true, true, BotHelper.BotTeam, true);
                    groupCountRemaining--;
                }
            }
        }
    }
}
