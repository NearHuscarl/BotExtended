using BotExtended.Factions;
using BotExtended.Library;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;

namespace BotExtended
{
    class Settings
    {
        public readonly int BotCount;
        public readonly int FactionRotationInterval;
        public bool FactionRotationEnabled { get { return FactionRotationInterval != 0; } }
        public readonly int RoundsUntilFactionRotation;
        public readonly Dictionary<PlayerTeam, List<BotFaction>> BotFactions;
        public readonly Dictionary<PlayerTeam, BotFaction> CurrentFaction;
        public readonly string[] PlayerSettings;

        public Settings(
            int botCount,
            int factionRotationInterval,
            int roundsUntilFactionRotation,
            Dictionary<PlayerTeam, List<BotFaction>> botFactions,
            Dictionary<PlayerTeam, BotFaction> currentFaction,
            string[] playerSettings
            )
        {
            BotCount = botCount;
            FactionRotationInterval = factionRotationInterval;
            RoundsUntilFactionRotation = roundsUntilFactionRotation;
            BotFactions = botFactions;
            CurrentFaction = currentFaction;
            PlayerSettings = playerSettings;
        }

        public bool HasFaction(PlayerTeam team)
        {
            return team != PlayerTeam.Independent && BotFactions[team].Where(f => f != BotFaction.None).Any();
        }

        public List<PlayerTeam> TeamsWithFactions()
        {
            var teams = new List<PlayerTeam>();
            foreach (var team in SharpHelper.EnumToList<PlayerTeam>())
            {
                if (team == PlayerTeam.Independent)
                    continue;
                if (HasFaction(team))
                    teams.Add(team);
            }
            return teams;
        }

        public static Settings Get()
        {
            int botCount;
            var botCountKey = BotHelper.StorageKey("BOT_COUNT");
            if (!BotHelper.Storage.TryGetItemInt(botCountKey, out botCount))
            {
                botCount = Constants.DEFAULT_MAX_BOT_COUNT;
                BotHelper.Storage.SetItem(botCountKey, Constants.DEFAULT_MAX_BOT_COUNT);
            }

            botCount = (int)MathHelper.Clamp(botCount, Constants.BOT_COUNT_MIN, Constants.BOT_COUNT_MAX);

            int factionRotationInterval;
            var factionRotationIntervalKey = BotHelper.StorageKey("FACTION_ROTATION_INTERVAL");
            if (!BotHelper.Storage.TryGetItemInt(factionRotationIntervalKey, out factionRotationInterval))
            {
                factionRotationInterval = Constants.DEFAULT_FACTION_ROTATION_INTERVAL;
                BotHelper.Storage.SetItem(factionRotationIntervalKey, Constants.DEFAULT_FACTION_ROTATION_INTERVAL);
            }

            int roundsUntilRotation;
            var roundsUntilRotationKey = BotHelper.StorageKey("ROUNDS_UNTIL_FACTION_ROTATION");
            if (!BotHelper.Storage.TryGetItemInt(roundsUntilRotationKey, out roundsUntilRotation))
            {
                roundsUntilRotation = factionRotationInterval;
                BotHelper.Storage.SetItem(roundsUntilRotationKey, factionRotationInterval);
            }

            var botFactions = new Dictionary<PlayerTeam, List<BotFaction>>();
            var currentFaction = new Dictionary<PlayerTeam, BotFaction>();

            string[] currentFactionStr;
            var currentFactionKey = BotHelper.StorageKey("CURRENT_FACTION");
            if (!BotHelper.Storage.TryGetItemStringArr(currentFactionKey, out currentFactionStr))
            {
                currentFactionStr = new string[] { "None", "None", "None", "None" };
            }

            for (var i = 0; i < 4; i++)
            {
                currentFaction.Add((PlayerTeam)i+1, SharpHelper.StringToEnum<BotFaction>(currentFactionStr[i]));
            }

            foreach (var team in SharpHelper.EnumToList<PlayerTeam>())
            {
                if (team == PlayerTeam.Independent)
                    continue;

                string[] factions = null;
                var factionsKey = BotHelper.StorageKey("BOT_FACTIONS_" + team);
                if (!BotHelper.Storage.TryGetItemStringArr(factionsKey, out factions))
                {
                    if (team == BotManager.BotTeam)
                        factions = Constants.DEFAULT_FACTIONS;
                    else
                        factions = new string[] { "None" };
                    BotHelper.Storage.SetItem(factionsKey, factions);
                }

                List<BotFaction> botFactionList;
                if (factions.Count() == 1 && factions.Single() == "All")
                {
                    botFactionList = BotHelper.GetAvailableBotFactions().ToList();
                }
                else
                {
                    botFactionList = new List<BotFaction>();
                    foreach (var faction in factions)
                    {
                        botFactionList.Add(SharpHelper.StringToEnum<BotFaction>(faction));
                    }
                }

                botFactions.Add(team, botFactionList);
            }

            string[] playerSettings;
            var playerSettingsKey = BotHelper.StorageKey("PLAYER_SETTINGS");
            if (!BotHelper.Storage.TryGetItemStringArr(playerSettingsKey, out playerSettings))
            {
                playerSettings = new string[] { };
            }

            return new Settings(
                botCount,
                factionRotationInterval,
                roundsUntilRotation,
                botFactions,
                currentFaction,
                playerSettings
            );
        }
    }
}
