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

        private static Dictionary<PlayerTeam, List<IPlayer>> m_playerByTeam = null;
        private static Dictionary<PlayerTeam, List<IPlayer>> PlayerByTeam
        {
            get
            {
                if (m_playerByTeam == null)
                {
                    m_playerByTeam = new Dictionary<PlayerTeam, List<IPlayer>>()
                    {
                        { PlayerTeam.Independent, new List<IPlayer>() },
                        { PlayerTeam.Team1, new List<IPlayer>() },
                        { PlayerTeam.Team2, new List<IPlayer>() },
                        { PlayerTeam.Team3, new List<IPlayer>() },
                        { PlayerTeam.Team4, new List<IPlayer>() },
                    };
                    foreach (var player in Game.GetPlayers())
                    {
                        PlayerByTeam[player.GetTeam()].Add(player);
                    }
                }
                return m_playerByTeam;
            }
        }

        public IEnumerable<Bot> Spawn(PlayerTeam team)
        {
            var factionCount = PlayerByTeam[team].Count;

            return Spawn(factionCount, team, (i, botType, isBoss) =>
            {
                if (i >= PlayerByTeam[team].Count())
                    return null;
                var player = PlayerByTeam[team][i];
                if (isBoss)
                    return BotManager.SpawnBot(botType, BotFaction, player, team, true, triggerOnSpawn: false);
                else
                    return BotManager.SpawnBot(botType, BotFaction, player, team, triggerOnSpawn: false);
            });
        }
        public IEnumerable<Bot> Spawn(int factionCount, PlayerTeam team)
        {
            return Spawn(factionCount, team, (_, botType, isBoss) =>
            {
                if (isBoss)
                    return BotManager.SpawnBot(botType, BotFaction, null, team, true, triggerOnSpawn: false);
                else
                    return BotManager.SpawnBot(botType, BotFaction, null, team, triggerOnSpawn: false);
            });
        }

        private IEnumerable<Bot> Spawn(int factionCount, PlayerTeam team, Func<int, BotType, bool, Bot> spawnCallback)
        {
            var bots = new List<Bot>();
            if (factionCount == 0) return bots;

            var subFactionCount = 0;
            var factionCountRemaining = factionCount;
            var mobCount = HasBoss ? factionCount - 1 : factionCount;
            var i = 0;

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

                        var bot = spawnCallback(i++, botType, false);
                        if (bot != null)
                            bots.Add(bot);

                        factionCountRemaining--;
                        botCountRemainingThisType--;
                    }
                }
                else
                {
                    var botType = subFaction.GetRandomType();
                    var bot = spawnCallback(i++, botType, true);
                    if (bot != null)
                        bots.Add(bot);

                    factionCountRemaining--;
                }
            }
            return bots;
        }
    }
}
