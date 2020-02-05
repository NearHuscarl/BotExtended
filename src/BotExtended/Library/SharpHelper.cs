using System;
using System.Collections.Generic;
using SFDGameScriptInterface;

namespace SFDScript.Library
{
    public static class SharpHelper
    {
        public static T StringToEnum<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }
        public static T[] GetArrayFromEnum<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            var enumArray = GetArrayFromEnum<T>();

            foreach (var enumVal in enumArray)
            {
                yield return enumVal;
            }
        }
        public static string EnumToString<T>(T enumVal)
        {
            return Enum.GetName(typeof(T), enumVal);
        }
        public static bool TryParseEnum<T>(string str, out T result) where T : struct, IConvertible
        {
            result = default(T);

            if (!typeof(T).IsEnum)
            {
                return false;
            }

            int index = -1;
            if (int.TryParse(str, out index))
            {
                if (Enum.IsDefined(typeof(T), index))
                {
                    // https://stackoverflow.com/questions/10387095/cast-int-to-generic-enum-in-c-sharp
                    result = (T)(object)index;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!Enum.TryParse(str, ignoreCase: true, result: out result))
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetNamespace<T>()
        {
            return typeof(T).Namespace;
        }

        public static bool IsIntersectRectangle(Vector2 start, Vector2 end, Vector2[] corners)
        {
            if (corners.Length != 4)
                throw new Exception("A rectangle must have 4 corners");

            var normal = Vector2.Normalize(end - start);

            //we don't know yet on which side of the line the rectangle lies
            float rectangleSide = 0;
            foreach (Vector2 corner in corners)
            {
                //cornerSide will be positive if the corner is on the side the normal points to,
                //zero if the corner is exactly on the line, and negative otherwise
                float cornerSide = Vector2.Dot(corner - start, normal);
                if (rectangleSide == 0)
                    //first evaluated corner or all previous corners lie exactly on the line
                    rectangleSide = cornerSide;
                else
                    if (cornerSide != 0 && // ignore corners on the line
                      (cornerSide > 0) != (rectangleSide > 0)) // different sides
                    return true;
            }

            return false;
        }
    }
}
