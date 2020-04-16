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

        public static float ToRadians(float angleDegree)
        {
            return (float)(angleDegree * Math.PI) / 180;
        }
        public static float ToDegree(float radians)
        {
            return radians * 180 / (float)Math.PI;
        }
        public static float NormalizeAngle(float radian)
        {
            var result = radian % MathHelper.TwoPI;
            return result < 0 ? result + MathHelper.TwoPI : result;
        }

        public static float Diff(float a, float b)
        {
            return Math.Abs(Math.Abs(a) - Math.Abs(b));
        }
        public static float DiffAngle(float a, float b)
        {
            a = NormalizeAngle(a);
            b = NormalizeAngle(b);

            var da = a - b;
            var db = a - b + MathHelper.PI * 2;
            var dc = a - b - MathHelper.PI * 2;

            var r = da;
            if (Math.Abs(r) > Math.Abs(db))
                r = db;
            if (Math.Abs(r) > Math.Abs(dc))
                r = dc;

            return r;
        }

        public static float FlipAngleX(float angle)
        {
            angle = NormalizeAngle(angle);
            return MathHelper.TwoPI - angle;
        }

        public static float FlipAngleY(float angle)
        {
            angle = NormalizeAngle(angle);
            if (angle < MathHelper.PI)
                return MathHelper.PI - angle;
            else
                return MathHelper.TwoPI - angle + MathHelper.PI;
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            var lengthSquare = vector.LengthSquared();
            if (lengthSquare > maxLength * maxLength)
            {
                var length = (float)Math.Sqrt(lengthSquare);
                var normalized_x = vector.X / length;
                var normalized_y = vector.Y / length;
                return new Vector2(normalized_x * maxLength, normalized_y * maxLength);
            }
            return vector;
        }
    }
}
