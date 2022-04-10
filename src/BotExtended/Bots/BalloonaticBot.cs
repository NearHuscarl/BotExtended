using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class BalloonaticBot : Bot
    {
        public BalloonaticBot(BotArgs args) : base(args) { }

        private static readonly string[] BalloonColors = new string[]
        {
            "BgLightRed",
            "BgLightOrange",
            "BgLightYellow",
            "BgLightGreen",
            "BgLightBlue",
            "BgLightCyan",
            "BgLightMagenta",
            "BgLightPink",
        };

        private List<IObject> m_markers = new List<IObject>();

        public override void OnSpawn()
        {
            base.OnSpawn();

            var colors = RandomHelper.Shuffle(BalloonColors);
            for (var i = 0; i < 5; i++)
            {
                var balloon = Game.CreateObject("Balloon00");
                var balloonJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");
                var distanceJoint = (IObjectDistanceJoint)Game.CreateObject("DistanceJoint");

                balloon.SetColor1(colors[i]);
                balloon.SetWorldPosition(Position + new Vector2((i - 2) * 10, 40));

                balloonJoint.SetWorldPosition(balloon.GetWorldPosition());
                balloonJoint.SetTargetObject(balloon);

                // TODO: restraint the balloons
                distanceJoint.SetTargetObjectJoint(balloonJoint);
                distanceJoint.SetWorldPosition(Position);
                distanceJoint.SetTargetObject(Player);
                distanceJoint.SetLineVisual(LineVisual.DJRope);

                m_markers.Add(balloonJoint);
                m_markers.Add(distanceJoint);

            }
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (args.Removed)
            {
                foreach (var m in m_markers) m.Remove();
            }
        }
    }
}
