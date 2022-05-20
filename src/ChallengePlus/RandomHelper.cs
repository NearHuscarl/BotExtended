using SFDGameScriptInterface;
using System;
using System.Collections.Generic;

namespace ChallengePlus
{
    public static class RandomHelper
    {
        public static Random Rnd { get; set; }
        static RandomHelper() { Rnd = new Random(); }

        public static bool Boolean()
        {
            return Rnd.NextDouble() >= 0.5;
        }

        public static float Between(float min, float max)
        {
            return (float)Rnd.NextDouble() * (max - min) + min;
        }
        public static int BetweenInt(int min, int max)
        {
            return Rnd.Next(min, max);
        }

        public static T GetItem<T>(List<T> list)
        {
            if (list.Count == 0)
                throw new Exception("list is empty");

            var rndIndex = Rnd.Next(list.Count);
            return list[rndIndex];
        }

        public static T GetItem<T>(params T[] list)
        {
            if (list.Length == 0)
                throw new Exception("list is empty");

            var rndIndex = Rnd.Next(list.Length);
            return list[rndIndex];
        }

        public static T GetEnumValue<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var enumValues = Enum.GetValues(typeof(T));
            return (T)enumValues.GetValue(Rnd.Next(enumValues.Length));
        }

        /// <summary>
        /// Chance from 0f to 1f. 0f means never. 1f means always
        /// </summary>
        /// <param name="chance"></param>
        /// <returns></returns>
        public static bool Percentage(float chance)
        {
            return Between(0f, 1f) < chance;
        }

        // https://stackoverflow.com/a/1262619/9449426
        public static IList<T> Shuffle<T>(IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rnd.Next(n + 1);
                var swap = list[k];
                list[k] = list[n];
                list[n] = swap;
            }
            return list;
        }
    }
}
