using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class NuclearChallenge : Challenge
    {
        public NuclearChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Explodes and destroys all entities in a radius on dealth."; }
        }

        public override void OnPlayerDealth(Player player, PlayerDeathArgs args)
        {
            base.OnPlayerDealth(player, args);

            var position = player.Position;

            var angle = 0;
            while (true)
            {
                var totalBullets = 10;
                var angleInBetween = 360 / totalBullets;
                var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(angle));
                var pos = position + direction * 15;
                Game.TriggerFireplosion(pos);
                Game.TriggerExplosion(pos);

                if (angle == 360 - angleInBetween)
                    break;

                angle += angleInBetween;
            }
        }
    }
}
