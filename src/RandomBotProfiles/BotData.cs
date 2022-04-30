using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RandomBotProfiles.Helpers;

namespace RandomBotProfiles
{
    public class BotData
    {
        public IProfile Profile { get; set; }
        public PlayerTeam Team { get; set; }
        public string Name { get; set; }
        public PredefinedAIType AI { get; set; }

        private static string ReadProfileItem(IProfileClothingItem clothingItem)
        {
            var str = "";
            if (clothingItem == null)
                return "||||";

            str += clothingItem.Name + "|";
            str += clothingItem.Color1 + "|";
            str += clothingItem.Color2 + "|";
            str += clothingItem.Color3 + "|";
            return str;
        }

        private static string ReadAI(PredefinedAIType AI)
        {
            switch (AI)
            {
                case PredefinedAIType.BotA: return "expert";
                case PredefinedAIType.BotB: return "hard";
                case PredefinedAIType.BotC: return "medium";
                case PredefinedAIType.BotD: return "easy";
                default: return "hard";
            }
        }

        private static string ReadTeam(PlayerTeam team)
        {
            switch (team)
            {
                case PlayerTeam.Team1: return "1";
                case PlayerTeam.Team2: return "2";
                case PlayerTeam.Team3: return "3";
                case PlayerTeam.Team4: return "4";
                default: return "_";
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Name + "|");
            sb.Append(ReadAI(AI) + "|");
            sb.Append(ReadTeam(Team) + "|");
            sb.Append(ReadProfileItem(Profile.Skin));
            sb.Append(ReadProfileItem(Profile.Head));
            sb.Append(ReadProfileItem(Profile.ChestOver));
            sb.Append(ReadProfileItem(Profile.ChestUnder));
            sb.Append(ReadProfileItem(Profile.Hands));
            sb.Append(ReadProfileItem(Profile.Waist));
            sb.Append(ReadProfileItem(Profile.Legs));
            sb.Append(ReadProfileItem(Profile.Accessory));

            return sb.ToString();
        }

        private static IProfileClothingItem ParseProfileItem(List<string> parts, int startIndex)
        {
            return new IProfileClothingItem(parts[startIndex], parts[startIndex + 1], parts[startIndex + 2], parts[startIndex + 3]);
        }

        public static PredefinedAIType ParseAI(string str)
        {
            switch (str)
            {
                case "expert": return PredefinedAIType.BotA;
                case "hard": return PredefinedAIType.BotB;
                case "medium": return PredefinedAIType.BotC;
                case "easy": return PredefinedAIType.BotD;
                default: return PredefinedAIType.BotB;
            }
        }

        public static PlayerTeam ParseTeam(string str)
        {
            switch (str)
            {
                case "1": return PlayerTeam.Team1;
                case "2": return PlayerTeam.Team2;
                case "3": return PlayerTeam.Team3;
                case "4": return PlayerTeam.Team4;
                default: return PlayerTeam.Independent;
            }
        }

        public static BotData FromString(string str, bool parseProfile = true)
        {
            var parts = str.Split('|').ToList();
            var name = parts[0];
            var ai = ParseAI(parts[1]);
            var team = ParseTeam(parts[2]);
            var profile = new IProfile();

            if (parseProfile)
            {
                profile.Skin = ParseProfileItem(parts, 3);
                profile.Head = ParseProfileItem(parts, 7);
                profile.ChestOver = ParseProfileItem(parts, 11);
                profile.ChestUnder = ParseProfileItem(parts, 15);
                profile.Hands = ParseProfileItem(parts, 19);
                profile.Waist = ParseProfileItem(parts, 23);
                profile.Legs = ParseProfileItem(parts, 27);
                profile.Accessory = ParseProfileItem(parts, 31);
            }

            return new BotData { Name = name, Team = team, Profile = profile, AI = ai };
        }
    }
}
