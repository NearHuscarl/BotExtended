using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFDGameScriptInterface;

namespace BotExtended.Script
{
    public partial class GameScript : GameScriptInterface
    {
        public GameScript() : base(null) { }

        // This script convert IObjectPlayerProfileInfo, your Superfighter outfits from the Map Editor, to C# code
        public void OnStartup()
        {
            // Search for 'PlayerProfileInfo' in the search bar. Drag'n'drop it to the Map Editor
            // Hit EDIT on Profile Info row to start editing your Superfighter outfits
            // Make sure all of your PlayerProfileInfo tiles are within the Camera Area

            // When you're done. Hit F7 (or go to Test > Map Debug)
            // - Tick Show script output
            // - Untick Freeze output
            // Hit F5 to test map

            // Depend on the number of PlayerProfileInfo tiles you have on the Map Editor, you may have to wait for a bit
            // Once you can focus the SFD Map Debug window, right click the area, select all and copy the generated C# code
            //
            // Convert all profiles found in the map editor
            // Convert();
            //
            // Convert a specific profile by profile name or IObjectPlayerProfileInfo's custom ID
            // Convert("Engineer", "Kinpin", "Police");
            Convert("SurvivorCrazy");

            //System.Diagnostics.Debugger.Break();
        }

        private void Convert(params string[] ids)
        {
            var idLookup = new HashSet<string>(ids);
            var playerProfiles = Game.GetObjects<IObjectPlayerProfileInfo>();

            foreach (var playerProfile in playerProfiles)
            {
                if (idLookup.Contains(playerProfile.CustomID) || idLookup.Contains(playerProfile.GetProfile().Name))
                    Game.WriteToConsole(Convert(playerProfile.GetProfile()));
            }
        }

        private static string EnumToString<T>(T enumVal)
        {
            return Enum.GetName(typeof(T), enumVal);
        }

        public string Convert(IProfile profile, int indentSize = 0)
        {
            if (profile == null) return "";

            var sb = new StringBuilder();
            var indent = new string(' ', indentSize);

            sb.AppendLine(indent + "new IProfile()");
            sb.AppendLine(indent + "{");
            sb.AppendLine(indent + "    Name = \"" + profile.Name + "\",");
            sb.AppendLine(indent + "    Accesory = " + GetClothingItemInfo(profile.Accessory) + ",");
            sb.AppendLine(indent + "    ChestOver = " + GetClothingItemInfo(profile.ChestOver) + ",");
            sb.AppendLine(indent + "    ChestUnder = " + GetClothingItemInfo(profile.ChestUnder) + ",");
            sb.AppendLine(indent + "    Feet = " + GetClothingItemInfo(profile.Feet) + ",");
            sb.AppendLine(indent + "    Gender = Gender." + EnumToString(profile.Gender) + ",");
            sb.AppendLine(indent + "    Hands = " + GetClothingItemInfo(profile.Hands) + ",");
            sb.AppendLine(indent + "    Head = " + GetClothingItemInfo(profile.Head) + ",");
            sb.AppendLine(indent + "    Legs = " + GetClothingItemInfo(profile.Legs) + ",");
            sb.AppendLine(indent + "    Skin = " + GetClothingItemInfo(profile.Skin) + ",");
            sb.AppendLine(indent + "    Waist = " + GetClothingItemInfo(profile.Waist) + ",");
            sb.AppendLine(indent + "};");

            return sb.ToString();
        }

        private string GetClothingItemInfo(IProfileClothingItem clothingItem)
        {
            if (clothingItem == null) return "null";

            return "new IProfileClothingItem(\"" + clothingItem.Name + "\", \"" + clothingItem.Color1 + "\", \"" + clothingItem.Color2 + "\", \"" + clothingItem.Color3 + "\")";
        }
    }
}
