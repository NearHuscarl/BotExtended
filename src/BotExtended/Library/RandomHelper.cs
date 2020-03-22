using SFDGameScriptInterface;
using System;
using System.Collections.Generic;

namespace BotExtended.Library
{
    public static class RandomHelper
    {
        public static Rnd Rnd { get; set; }
        private static Dictionary<string, Rnd> m_rnds = new Dictionary<string, Rnd>();

        static RandomHelper()
        {
            Rnd = new Rnd();
        }

        public static void AddRandomGenerator(string name, Rnd rnd)
        {
            m_rnds.Add(name, rnd);
        }
        public static Rnd GetRandomGenerator(string name)
        {
            if (m_rnds.ContainsKey(name))
            {
                return m_rnds[name];
            }
            return null;
        }

        private static bool Boolean(Rnd rnd)
        {
            return rnd.NextDouble() >= 0.5;
        }
        public static bool Boolean(string seedName = "")
        {
            if (m_rnds.ContainsKey(seedName))
            {
                return Boolean(m_rnds[seedName]);
            }
            return Boolean(Rnd);
        }

        private static float Between(Rnd rnd, float min, float max)
        {
            return (float)rnd.NextDouble() * (max - min) + min;
        }
        public static float Between(float min, float max, string seedName = "")
        {
            if (m_rnds.ContainsKey(seedName))
            {
                return Between(m_rnds[seedName], min, max);
            }
            return Between(Rnd, min, max);
        }

        private static T GetItem<T>(Rnd rnd, List<T> list)
        {
            if (list.Count == 0)
                throw new Exception("list is empty duh. me cant randomize");

            var rndIndex = rnd.Next(list.Count);
            return list[rndIndex];
        }
        public static T GetItem<T>(List<T> list, string seedName = "")
        {
            if (m_rnds.ContainsKey(seedName))
            {
                return GetItem(m_rnds[seedName], list);
            }
            return GetItem(Rnd, list);
        }

        private static T GetEnumValue<T>(Rnd rnd) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var enumValues = Enum.GetValues(typeof(T));
            return (T)enumValues.GetValue(rnd.Next(enumValues.Length));
        }
        public static T GetEnumValue<T>(string seedName = "") where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (m_rnds.ContainsKey(seedName))
            {
                return GetEnumValue<T>(m_rnds[seedName]);
            }
            return GetEnumValue<T>(Rnd);
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

        private static Vector2 Direction(Rnd rnd, float minAngle, float maxAngle)
        {
            var angle = Between(minAngle, maxAngle);
            var radianAngle = MathExtension.ToRadians(angle);

            return ScriptHelper.GetDirection(radianAngle);
        }
        public static Vector2 Direction(float minAngle = 0, float maxAngle = 360, string seedName = "")
        {
            if (m_rnds.ContainsKey(seedName))
            {
                return Direction(m_rnds[seedName], minAngle, maxAngle);
            }
            return Direction(Rnd, minAngle, maxAngle);
        }

        public static Vector2 WithinArea(Area area)
        {
            var center = area.Center;
            var halfWidth = area.Width / 2;
            var halfHeight = area.Height / 2;

            return new Vector2()
            {
                X = Between(center.X - halfWidth, center.X + halfWidth),
                Y = Between(center.Y - halfHeight, center.Y + halfHeight),
            };
        }
    }
}
