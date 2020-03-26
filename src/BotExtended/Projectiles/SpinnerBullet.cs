using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class SpinnerBullet : Projectile
    {
        private Vector2 m_explodePosition;

        private enum Direction
        {
            Left,
            Top,
            Right,
            Bottom,
        }

        private enum State
        {
            Normal,
            Exploding,
            Exploded,
        }
        private State m_state = State.Normal;

        public SpinnerBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Spinner)
        {
            UpdateDelay = 4;
        }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            switch (m_state)
            {
                case State.Normal:
                {
                    if (CanExplode())
                        Explode();
                    break;
                }
                case State.Exploding:
                {
                    // TODO: wait for gurt to fix this and test again
                    // https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&p=23439&sid=3b9c195145b551d2b961648ddc5f432d#p23439
                    Instance.Position = m_explodePosition;
                    Instance.Velocity = Vector2.Zero;
                    Instance.Direction = Vector2.Zero;
                    break;
                }
                case State.Exploded:
                    break;
            }
        }

        private void ChangeState(State state)
        {
            // events
            if (state == State.Exploding)
            {
                Instance.Velocity = Vector2.Zero;
                Instance.Direction = Vector2.Zero;
            }
            if (state == State.Exploded)
            {
                Instance.FlagForRemoval();

                if (Instance.ProjectileItem == ProjectileItem.GRENADE_LAUNCHER
                    || Instance.ProjectileItem == ProjectileItem.BAZOOKA)
                    Game.TriggerExplosion(m_explodePosition);
                else
                    Game.PlayEffect(EffectName.Block, m_explodePosition);
            }

            m_state = state;
        }

        private Direction GetHeadingDirection(float angle)
        {
            angle = MathExtension.NormalizeAngle(angle);

            if (angle >= 0 && angle < MathHelper.PIOver4 || angle >= MathExtension.PI_3Over2 && angle <= MathExtension.PIOver2)
                return Direction.Right;
            if (angle >= MathHelper.PIOver4 && angle < MathHelper.PIOver2 + MathHelper.PIOver4)
                return Direction.Top;
            if (angle >= MathHelper.PIOver2 + MathHelper.PIOver4 && angle < MathHelper.PI + MathHelper.PIOver4)
                return Direction.Left;
            return Direction.Bottom;
        }

        private bool CanExplode()
        {
            var headingDirection = GetHeadingDirection(ScriptHelper.GetAngle(Instance.Direction));
            var explodeRange = ScriptHelper.GrowFromCenter(Instance.Position,
                headingDirection == Direction.Left ? 60 : 10,
                headingDirection == Direction.Top ? 60 : 10,
                headingDirection == Direction.Right ? 60 : 10,
                headingDirection == Direction.Bottom ? 60 : 10);
            var os = Game.GetObjectsByArea(explodeRange, PhysicsLayer.Active);

            foreach (var o in os)
            {
                var collisionFilter = o.GetCollisionFilter();
                if (collisionFilter.BlockExplosions && Instance.TotalDistanceTraveled >= 100)
                {
                    if (Game.IsEditorTest)
                    {
                        var position = Instance.Position;
                        ScriptHelper.RunIn(() =>
                        {
                            Game.DrawCircle(position, .5f, Color.Red);
                            Game.DrawLine(position, o.GetWorldPosition(), Color.Yellow);
                            Game.DrawArea(o.GetAABB(), Color.Yellow);
                            Game.DrawText(o.Name + " " + headingDirection, position);
                            Game.DrawArea(explodeRange);
                        }, 2000);
                    }
                    return true;
                }
            }

            return false;
        }

        private void Explode()
        {
            if (Instance.IsRemoved) return;

            ChangeState(State.Exploding);
            m_explodePosition = Instance.Position;

            var totalBullets = 20;
            var angleInBetween = (360 / totalBullets);

            for (var i = 0; i < 360; i += angleInBetween) // Shoot 20 times in circle (360 / 20 = 18)
            {
                var ii = i;
                var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(i));
                var powerup = ScriptHelper.GetPowerup(Instance);

                ScriptHelper.Timeout(() =>
                {
                    Game.PlaySound("SilencedUzi", m_explodePosition);
                    Game.SpawnProjectile(ProjectileItem.REVOLVER, m_explodePosition, direction, powerup);

                    if (ii == 360 - angleInBetween)
                        ChangeState(State.Exploded);
                }, (ushort)(ii / angleInBetween * 30));
            }
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            // in case bouncing ammo hit multiple times
            if (m_state == State.Normal && args.RemoveFlag)
                Explode();
        }
    }
}
