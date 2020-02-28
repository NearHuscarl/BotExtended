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
        public static double ToRadians(double angleDegree)
        {
            return (angleDegree * Math.PI) / 180;
        }
        public static double NormalizeAngle(double radian)
        {
            var result = radian % MathHelper.TwoPI;
            return result < 0 ? result + MathHelper.TwoPI : result;
        }
    }
}
