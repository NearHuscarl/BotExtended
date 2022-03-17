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
    public class ShotgunTrap : Trap
    {
        public static readonly uint TrapTime = 5000;
        public ShotgunTrap(IPlayer owner) : base(owner, "ShotgunTrap00", Vector2.Zero) { }

        protected override bool ShouldTrigger(IPlayer player)
        {
            var pPos = player.GetWorldPosition();
            var distance = Vector2.Distance(pPos, Position);
            var angle = ScriptHelper.GetAngle(player.GetWorldPosition() - Position);
            return player.IsInMidAir && distance > 15 && distance <= 40 && angle >= MathExtension.PIOver4 && angle <= MathExtension.PI - MathExtension.PIOver4;
        }

        private int _tripCount = 0;
        protected override bool OnTrigger(IPlayer player)
        {
            _tripCount++;

            base.OnTrigger(player);
            var angle = ScriptHelper.GetAngle(player.GetWorldPosition() - Position);

            for (var i = 0; i < 6; i++)
            {
                var finalDirection = RandomHelper.Direction(angle - 0.09f, angle + 0.09f, true);
                var proj = Game.SpawnProjectile(ProjectileItem.DARK_SHOTGUN, Position, finalDirection);
                proj.Velocity /= 2;
            }
            Game.PlaySound(ScriptHelper.GetSoundID(Mapper.GetWeaponItem(ProjectileItem.DARK_SHOTGUN)), Position);

            if (_tripCount >= 4)
            {
                Instance.Remove();
                IsDestroyed = true;
            }

            return false;
        }
    }
}
