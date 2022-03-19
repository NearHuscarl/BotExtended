using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class SuicideDoveProjectile : CustomProjectile
    {
        public IPlayer Target { get; private set; }

        public SuicideDoveProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.SuicideDove) { }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            _isElapsedUpdate = ScriptHelper.WithIsElapsed(105);
            return CreateCustomProjectile(projectile, "Dove00", projectile.Velocity / 20);
        }

        private Func<bool> _isElapsedUpdate;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (_isElapsedUpdate() && TotalDistanceTraveled > 20)
                SearchTarget();

            Guide();

            if (Target != null)
            {
                Game.DrawLine(Instance.GetWorldPosition(), Target.GetWorldPosition());

                if (Instance.GetAABB().Intersects(Target.GetAABB()))
                    Instance.Destroy(); // trigger explosion
            }
        }

        private void SearchTarget()
        {
            var minDistanceToPlayer = float.MaxValue;

            foreach (var player in Game.GetPlayers())
            {
                if (SameTeam(player) || player.IsDead) continue;

                var distanceToPlayer = Vector2.Distance(Instance.GetWorldPosition(), player.GetWorldPosition());
                if (minDistanceToPlayer > distanceToPlayer)
                {
                    minDistanceToPlayer = distanceToPlayer;
                    Target = player;
                }
            }
        }

        private float m_guideDelay = 0f;
        private float m_guideDelayTime = 1000f;
        private void Guide()
        {
            if (ScriptHelper.IsElapsed(m_guideDelay, m_guideDelayTime))
            {
                m_guideDelay = Game.TotalElapsedGameTime;
                m_guideDelayTime = RandomHelper.Between(1000, 3000);

                if (Target == null) return;
                if (Vector2.Distance(Target.GetWorldPosition(), Instance.GetWorldPosition()) >= 60)
                    m_guideDelayTime = 500;

                var targetDirection = Vector2.Normalize(Target.GetWorldPosition() - Instance.GetWorldPosition());
                var angle = MathExtension.NormalizeAngle(ScriptHelper.GetAngle(targetDirection));
                var isFacingLeft = angle >= MathHelper.PIOver2 && angle <= MathExtension.PI_3Over2;

                Instance.SetFaceDirection(isFacingLeft ? -1 : 1);
                Instance.SetLinearVelocity(targetDirection * RandomHelper.Between(2, 8));
            }
        }

        public override void OnProjectileTerminated()
        {
            base.OnProjectileTerminated();
            Game.TriggerExplosion(Instance.GetWorldPosition());
        }
    }
}
