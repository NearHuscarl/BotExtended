using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Library
{
    static class MathExtension
    {
        public const float TwoPI = MathHelper.TwoPI;
        public const float PI = MathHelper.PI;
        public const float PIOver2 = MathHelper.PIOver2;
        public const float PIOver4 = MathHelper.PIOver4;
        public const float PIOver8 = MathHelper.PIOver8;

        public const float PI_3Over2 = TwoPI - PIOver2;

        public const float OneDeg = MathHelper.PI / 180;

        public static float ToRadians(double angleDegree)
        {
            return (float)(angleDegree * Math.PI) / 180;
        }
        public static float NormalizeAngle(float radian)
        {
            var result = radian % MathHelper.TwoPI;
            return result < 0 ? result + MathHelper.TwoPI : result;
        }

        public static float Min(float a1, float a2, float a3)
        {
            float res;
            res = Math.Min(a1, a2);
            res = Math.Min(res, a3);

            return res;
        }

        public static float Min(float a1, float a2, float a3, float a4)
        {
            float res;
            res = Math.Min(a1, a2);
            res = Math.Min(res, a3);
            res = Math.Min(res, a4);

            return res;
        }

        public static float Max(float a1, float a2, float a3)
        {
            float res;
            res = Math.Max(a1, a2);
            res = Math.Max(res, a3);

            return res;
        }

        public static float Max(float a1, float a2, float a3, float a4)
        {
            float res;
            res = Math.Max(a1, a2);
            res = Math.Max(res, a3);
            res = Math.Max(res, a4);

            return res;
        }
    }
}
