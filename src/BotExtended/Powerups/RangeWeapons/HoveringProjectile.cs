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
    class HoveringProjectile : Projectile
    {
        protected Vector2 HoverPosition;
        protected float ExplodeRange = 60;
        protected float ExplodeRange2 = 10;
        protected float MinDistanceBeforeHover = 100;

        protected enum State
        {
            Normal,
            Hovering,
            Destroyed,
        }
        protected State CurrentState { get; private set; }

        public override bool IsRemoved { get { return CurrentState == State.Destroyed; } }

        public HoveringProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            CurrentState = State.Normal;
        }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            switch (CurrentState)
            {
                case State.Normal:
                {
                    if (CanHover()) Hover();
                    break;
                }
                case State.Hovering:
                {
                    Instance.Position = HoverPosition;
                    Instance.Velocity = new Vector2(0, 100);
                    Instance.Direction = Vector2.Zero;
                    UpdateHovering(elapsed);
                    break;
                }
                case State.Destroyed:
                    break;
            }
        }

        protected float HoverTime { get; private set; }
        protected virtual void UpdateHovering(float elapsed) { HoverTime += elapsed; }

        protected void Hover()
        {
            if (Instance.IsRemoved) return;

            CurrentState = State.Hovering;
            HoverPosition = Instance.Position;
            Instance.Velocity = new Vector2(0, 100);
            Instance.Direction = Vector2.Zero;
            
            if (!RangedWpns.IsSlowWpns(WeaponItem)) Instance.FlagForRemoval();

            HoverTime = 0f;
            OnHover();
        }

        protected virtual void OnHover() { }

        protected virtual void Destroy()
        {
            CurrentState = State.Destroyed;
            Instance.FlagForRemoval();
        }

        private bool CanHover()
        {
            var headingDirection = ScriptHelper.GetDir(ScriptHelper.GetAngle(Instance.Direction));
            var explodeRange = ScriptHelper.GrowFromCenter(Instance.Position,
                headingDirection == Direction.Left ? ExplodeRange : ExplodeRange2,
                headingDirection == Direction.Top ? ExplodeRange : ExplodeRange2,
                headingDirection == Direction.Right ? ExplodeRange : ExplodeRange2,
                headingDirection == Direction.Bottom ? ExplodeRange : ExplodeRange2);
            var os = Game.GetObjectsByArea(explodeRange);

            foreach (var o in os)
            {
                var collisionFilter = o.GetCollisionFilter();
                if ((collisionFilter.BlockExplosions || collisionFilter.CategoryBits == CategoryBits.Player)
                    && Instance.TotalDistanceTraveled >= MinDistanceBeforeHover)
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

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            // in case bouncing ammo hit multiple times
            if (CurrentState == State.Normal && args.RemoveFlag)
                Hover();
        }
    }
}
