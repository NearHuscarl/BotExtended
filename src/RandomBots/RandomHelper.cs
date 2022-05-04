using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static RandomBots.Helpers;

namespace RandomBots
{
    public static class RandomHelper
    {
        public static void Initialize() { Rnd = new Random(); }

        public static Random Rnd { get; set; }
        public static T GetRandomItem<T>(List<T> list)
        {
            if (list.Count == 0)
                throw new Exception("list is empty");

            var rndIndex = Rnd.Next(list.Count);
            return list[rndIndex];
        }
        public static IList<T> Shuffle<T>(IList<T> list)  
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static bool Boolean()
        {
            return Rnd.NextDouble() >= 0.5;
        }

        public static string RandomizePackage(List<string> packageNames)
        {
            if (packageNames.Count == 0) return null;
            return GetRandomItem(packageNames);
        }

        public static string[] RandomizePalette(string paletteName)
        {
            var palette = Game.GetColorPalette(paletteName);
            var color1 = RandomizePackage(palette.PrimaryColorPackages.ToList());
            var color2 = RandomizePackage(palette.SecondaryColorPackages.ToList());
            var color3 = RandomizePackage(palette.TertiaryColorPackages.ToList());

            return new string[] { color1, color2, color3 };
        }

        public static IProfileClothingItem RandomizeItemColor(string clothingItemName)
        {
            var palette = Game.GetClothingItemColorPaletteName(clothingItemName);
            var colors = RandomizePalette(palette);

            return new IProfileClothingItem(clothingItemName, colors[0], colors[1], colors[2]);
        }

        private static void SetRandomProfileItem(IProfile profile, int layer, string itemName)
        {
            if (itemName == null)
                ScriptHelper.SetProfileItem(profile, layer, null);
            else
                ScriptHelper.SetProfileItem(profile, layer, RandomizeItemColor(itemName));
        }

        public enum ClothingType
        {
            Accesory,
            ChestOver,
            ChestUnder,
            Feet,
            Hands,
            Head,
            Legs,
            Waist,
        }
        public static ClothingType[] EquipmentLayers = ScriptHelper.EnumToArray<ClothingType>();
        public static IProfile RandomizeProfile()
        {
            var gender = Boolean() ? Gender.Male : Gender.Female;
            var profile = new IProfile() { Gender = gender };

            for (int equipmentLayer = 0; equipmentLayer < EquipmentLayers.Length; ++equipmentLayer)
            {
                var items = ScriptHelper.GetProfileItems(equipmentLayer, profile.Gender).ToList();
                var itemName = items.Count == 0 ? null : GetRandomItem(items);
                float num;
                switch (equipmentLayer)
                {
                    case 0:
                        num = 0f;
                        break;
                    case 8:
                        num = 0.4f;
                        break;
                    default:
                        num = 0.2f;
                        break;
                }
                if (Rnd.NextDouble() >= num)
                {
                    SetRandomProfileItem(profile, equipmentLayer, itemName);
                }
            }

            return profile;
        }

        public static string RandomizeName()
        {
            var strArray = new string[28]
         {
        "Jeff",
        "Bob",
        "Mac",
        "Gerald",
        "Guffin",
        "Martin",
        "Steven",
        "Wreck",
        "Neo",
        "Angelica",
        "Sara",
        "Rain",
        "Snowstorm",
        "Punch",
        "Train",
        "Macho",
        "Superfighter",
        "Geronimo",
        "Ship",
        "Christmas",
        "Santa",
        "Building",
        "American",
        "Hexlex",
        "Luthor",
        "Thor",
        "Hulk",
        "ng"
         };
            var stringList1 = new List<string>();
            var stringList2 = new List<string>();
            var stringList3 = new List<string>();
            var stringList4 = new List<string>();
            var stringList5 = new List<string>();
            stringList5.Add("rf");
            stringList5.Add("rt");
            stringList5.Add("ph");
            stringList5.Add("ff");
            stringList5.Add("ng");
            foreach (var str in strArray)
            {
                for (var index = 1; index < str.Length; ++index)
                {
                    var lowerInvariant = (str[index - 1].ToString() + str[index].ToString()).ToLowerInvariant();
                    stringList4.Add(lowerInvariant);
                    if ("aoueiy".Contains(lowerInvariant[0].ToString()))
                        stringList1.Add(lowerInvariant);
                    else
                        stringList3.Add(lowerInvariant);
                    if ("aoueiy".Contains(lowerInvariant[1].ToString()))
                        stringList2.Add(lowerInvariant);
                    if (index == str.Length - 1)
                        stringList5.Add(lowerInvariant);
                }
            }
            var rndName = "";
            var flag1 = true;
            var flag2 = false;
            var num = Rnd.Next(2, 5);
            for (var index = 0; index < num; ++index)
            {
                var stringList6 = stringList4;
                if (!flag1 && !flag2)
                    stringList6 = stringList1;
                if (flag2)
                    stringList6 = stringList3;
                string str = stringList6[Rnd.Next(0, stringList6.Count)];
                flag1 = stringList1.Contains(str);
                flag2 = stringList2.Contains(str);
                rndName += str;
                if (rndName.Length == 2 && !"aoueiy".Contains(rndName[0].ToString()) && !"aoueiy".Contains(rndName[1].ToString()))
                    rndName = rndName[0].ToString();
            }
            Func<string> func1 = () => rndName.Length >= 2 ? rndName[rndName.Length - 2].ToString() + rndName[rndName.Length - 1].ToString() : "";
            Func<bool> func2 = () => rndName.Length > 2 && !"aoueiy".Contains(rndName[rndName.Length - 1].ToString()) && !"aoueiy".Contains(rndName[rndName.Length - 2].ToString());
            rndName = rndName.Replace("bef", "beef").Replace("ufi", "uffi").Replace("sex", "six").Replace("nxl", "xly").Replace("ic", "ice").Replace("fip", "flip").Replace("ee", "e");
            while (func2())
            {
                var str = func1();
                if (!stringList5.Contains(str))
                    rndName = rndName.Substring(0, rndName.Length - 1);
                else
                    break;
            }
            rndName = rndName[0].ToString().ToUpperInvariant()[0].ToString() + rndName.Substring(1);

            return rndName;
        }
    }
}
