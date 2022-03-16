using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Weapons
{
    public class FireTrap : Trap
    {
        public static readonly uint TrapTime = 5000;
        private Func<bool> _isElapsedSpawnFire;
        public FireTrap(IPlayer owner) : base(owner, "FireTrap00", new Vector2(0, -3))
        {
            _isElapsedSpawnFire = ScriptHelper.WithIsElapsed(1000);
        }

        protected override bool OnTrigger(IPlayer player)
        {
            base.OnTrigger(player);

            var result = ScriptHelper.CreateRope(Position, player, 30, LineVisual.DJRope);
            var distanceJoint = result.DistanceJoint;
            var distanceJointObject = result.DistanceJointObject;
            var targetJoint = result.TargetObjectJoint;

            ScriptHelper.Timeout(() =>
            {
                distanceJoint.Remove();
                targetJoint.Remove();
                distanceJointObject.Remove();
                Instance.Remove();
                IsDestroyed = true;
            }, TrapTime);

            return true;
        }

        protected override void OnUpdateAfterTrigger()
        {
            base.OnUpdateAfterTrigger();

            if (_isElapsedSpawnFire())
            {
                Game.PlaySound("Flamethrower", Position);
                Game.SpawnFireNodes(Position, 10, 5f, FireNodeType.Flamethrower);
            }
        }
    }
}
