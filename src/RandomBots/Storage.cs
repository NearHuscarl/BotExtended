using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RandomBots.Helpers;

namespace RandomBots
{
    public static class Storage
    {
        private static IScriptStorage _storage = Game.GetSharedStorage("RandomBots");

        public static List<BotData> GetBotsData()
        {
            var botsValue = _storage.GetItem("RB_BOTS") as string[];

            if (botsValue != null)
            {
                return botsValue.Select(str => BotData.FromString(str)).ToList();
            }
            return new List<BotData>();
        }
        public static void SaveBotsData(IEnumerable<BotData> botsData)
        {
            _storage.SetItem("RB_BOTS", botsData.Select(b => b.ToString()).ToArray());
        }

        public static bool GetIsRandom()
        {
            var value = _storage.GetItem("RB_IS_RANDOM");
            return bool.Parse(value == null ? "false" : value.ToString());
        }
        public static void SaveIsRandom(bool value)
        {
            _storage.SetItem("RB_IS_RANDOM", value);
        }

        public static bool GetAllowOnlyBots()
        {
            var value = _storage.GetItem("RB_ALLOW_ONLY_BOTS");
            return bool.Parse(value == null ? "false" : value.ToString());
        }
        public static void SaveAllowOnlyBots(bool value)
        {
            _storage.SetItem("RB_ALLOW_ONLY_BOTS", value);
        }
    }
}
