using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class SuicideDoveProjectile : CustomProjectile
    {
        public IPlayer Target { get; private set; }

        public SuicideDoveProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.SuicideDove) { }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            return CreateCustomProjectile(projectile, "Dove00", projectile.Velocity / 20);
        }

        private bool m_exploded = false;
        private float m_updateDelay = 0f;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (ScriptHelper.IsElapsed(m_updateDelay, 105))
            {
                m_updateDelay = Game.TotalElapsedGameTime;

                if (TotalDistanceTraveled > 20)
                {
                    SearchTarget();
                }
            }

            Guide(elapsed);

            if (Target != null)
            {
                Game.DrawArea(Instance.GetAABB(), Color.Green);
                Game.DrawArea(Target.GetAABB(), Color.Green);

                if (Instance.GetAABB().Intersects(Target.GetAABB()))
                {
                    m_exploded = true;
                    Game.TriggerExplosion(Instance.GetWorldPosition());
                }
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

        public override void OnProjectileHit()
        {
            base.OnProjectileHit();
            if (!m_exploded)
                Game.TriggerExplosion(Instance.GetWorldPosition());
        }
    }
}
