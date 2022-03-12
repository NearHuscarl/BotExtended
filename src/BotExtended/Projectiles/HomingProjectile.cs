using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    /// <summary>
    /// Credit to DangerRoss for the original idea and most of the code here
    /// </summary>
    class HomingProjectile : Projectile
    {
        public IPlayer Target { get; private set; }

        public HomingProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Homing)
        {
            //if (Game.IsEditorTest) Instance.Velocity /= 20;
        }

        private float m_updateDelay = 0f;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (ScriptHelper.IsElapsed(m_updateDelay, 75))
            {
                m_updateDelay = Game.TotalElapsedGameTime;
                SearchTarget();
            }
            if (Target != null) Game.DrawArea(Target.GetAABB());

            Guide(elapsed);
        }

        private void Guide(float elapsed)
        {
            if (Target == null) return;

            var targetDir = Target.GetWorldPosition() - Instance.Position;
            var steerAngle = Math.Sign(MathExtension.AngleBetween(Instance.Direction, targetDir))
                * MathExtension.OneDeg * elapsed * 2/3;

            Instance.Direction = ScriptHelper.GetDirection(ScriptHelper.GetAngle(Instance.Direction) + steerAngle);
        }

        private void SearchTarget()
        {
            var minDistanceToPlayer = float.MaxValue;

            foreach (var player in Game.GetPlayers())
            {
                if (SameTeam(player) || player.IsDead) continue;

                var distanceToPlayer = Vector2.Distance(Instance.Position, player.GetWorldPosition());
                if (minDistanceToPlayer > distanceToPlayer)
                {
                    minDistanceToPlayer = distanceToPlayer;
                    Target = player;
                }
            }
        }
    }
}
