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
        public readonly List<BotFaction> BotFactions;
        public readonly BotFaction CurrentFaction;

        public Settings(
            int botCount,
            int factionRotationInterval,
            int roundsUntilFactionRotation,
            List<BotFaction> botFactions,
            BotFaction currentFaction
            )
        {
            BotCount = botCount;
            FactionRotationInterval = factionRotationInterval;
            RoundsUntilFactionRotation = roundsUntilFactionRotation;
            BotFactions = botFactions;
            BotFactions = botFactions;
            CurrentFaction = currentFaction;
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

            botCount = (int)MathHelper.Clamp(botCount, 1, 10);

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

            string currentFactionStr;
            var currentFactionKey = BotHelper.StorageKey("CURRENT_FACTION");
            if (!BotHelper.Storage.TryGetItemString(currentFactionKey, out currentFactionStr))
            {
                currentFactionStr = SharpHelper.EnumToString(BotFaction.None);
            }

            string[] factions = null;
            var factionsKey = BotHelper.StorageKey("BOT_FACTIONS");
            if (!BotHelper.Storage.TryGetItemStringArr(factionsKey, out factions))
            {
                factions = Constants.DEFAULT_FACTIONS;
                BotHelper.Storage.SetItem(factionsKey, Constants.DEFAULT_FACTIONS);
            }

            var botFactions = new List<BotFaction>();
            foreach (var faction in factions)
                botFactions.Add(SharpHelper.StringToEnum<BotFaction>(faction));

            return new Settings(
                botCount,
                factionRotationInterval,
                roundsUntilRotation,
                botFactions,
                SharpHelper.StringToEnum<BotFaction>(currentFactionStr)
            );
        }
    }
}
