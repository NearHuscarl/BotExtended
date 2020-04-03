using System;
using System.Collections.Generic;
using System.Linq;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class BlackholeProjectile : HoveringProjectile
    {
        private class PulledObjectInfo
        {
            public IObject Object;
            public IObjectPullJoint PullJoint;
            public float OriginalMass;
            public bool UnScrewed = false;
        }

        private static readonly float ActiveTime = Game.IsEditorTest ? 4000 : 4000;
        public const float SuckRadius = 150;
        public const float PullRadius = 100;
        public const float EventHorizon = 50;
        public const float DestroyRadius = 25;

        private float m_activeTime = 0f;
        private IObject m_blackhole;
        private List<IObject> m_blackholes = new List<IObject>();

        private IObjectTargetObjectJoint m_magnetJoint;
        private IObjectRevoluteJoint m_revoluteJoint;
        private Dictionary<int, PulledObjectInfo> m_pulledObjects = new Dictionary<int, PulledObjectInfo>();
        private enum Range { Center, EventHorizon, Level2, Level1, Outside, }

        public BlackholeProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Blackhole)
        {
            UpdateDelay = 0;
        }

        protected override bool OnProjectileCreated()
        {
            if (Instance.ProjectileItem == ProjectileItem.BAZOOKA
                || Instance.ProjectileItem == ProjectileItem.GRENADE_LAUNCHER
                || Instance.ProjectileItem == ProjectileItem.SNIPER
                || Instance.ProjectileItem == ProjectileItem.BOW
                || Instance.ProjectileItem == ProjectileItem.FLAREGUN
                || Instance.ProjectileItem == ProjectileItem.MAGNUM)
                return true;

            return false;
        }

        protected override void OnHover()
        {
            base.OnHover();
            Instance.FlagForRemoval();

            var noCollisionFilter = Game.CreateObject("FarBgBlimp00").GetCollisionFilter();

            Game.RunCommand("/settime .1");
            ScriptHelper.Timeout(() => Game.RunCommand("/settime 1"), 2000);

            m_blackhole = Game.CreateObject("Shadow00A");
            m_blackhole.SetBodyType(BodyType.Dynamic);
            m_blackhole.SetCollisionFilter(noCollisionFilter);
            m_blackhole.SetWorldPosition(HoverPosition);

            for (var i = 1; i < 40; i++)
            {
                var egg = Game.CreateObject("Shadow00A");
                egg.SetAngle(MathHelper.TwoPI * i / 39);
                egg.SetBodyType(BodyType.Static);
                egg.SetCollisionFilter(noCollisionFilter);
                egg.SetWorldPosition(HoverPosition);
                m_blackholes.Add(egg);
            }

            m_magnetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");
            m_magnetJoint.SetWorldPosition(HoverPosition);
            m_magnetJoint.SetTargetObject(m_blackhole);

            m_revoluteJoint = (IObjectRevoluteJoint)Game.CreateObject("RevoluteJoint");
            m_revoluteJoint.SetWorldPosition(HoverPosition);
            m_revoluteJoint.SetTargetObjectA(m_blackhole);
            m_revoluteJoint.SetMotorEnabled(true);
            m_revoluteJoint.SetMotorSpeed(3000);
            m_revoluteJoint.SetMaxMotorTorque(50000);

            m_activeTime = Game.TotalElapsedGameTime;
        }

        private Range GetPositionToCenter(IObject o)
        {
            if (ScriptHelper.IntersectCircle(o.GetAABB(), HoverPosition, DestroyRadius))
            {
                return Range.Center;
            }
            else if (ScriptHelper.IntersectCircle(o.GetAABB(), HoverPosition, EventHorizon))
            {
                return Range.EventHorizon;
            }
            else if (ScriptHelper.IntersectCircle(o.GetAABB(), HoverPosition, PullRadius))
            {
                return Range.Level2;
            }
            else if (ScriptHelper.IntersectCircle(o.GetAABB(), HoverPosition, SuckRadius))
            {
                return Range.Level1;
            }
            return Range.Outside;
        }

        private float m_updateDelay = 0f;
        private float m_update2Delay = 0f;
        protected override void UpdateHovering(float elapsed)
        {
            base.UpdateHovering(elapsed);

            if (ScriptHelper.IsElapsed(m_activeTime, ActiveTime))
            {
                Destroy(); return;
            }

            if (ScriptHelper.IsElapsed(m_updateDelay, 30))
            {
                m_updateDelay = Game.TotalElapsedGameTime;

                var filterArea = ScriptHelper.GrowFromCenter(HoverPosition, SuckRadius * 2);
                var objectsInRadius = Game.GetObjectsByArea(filterArea, PhysicsLayer.Active)
                    .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), HoverPosition, SuckRadius));

                Game.DrawCircle(HoverPosition, SuckRadius, Color.Cyan);
                Game.DrawCircle(HoverPosition, PullRadius, Color.Red);
                Game.DrawCircle(HoverPosition, EventHorizon, Color.Red);
                Game.DrawCircle(HoverPosition, DestroyRadius, Color.Red);
                Game.DrawArea(m_blackhole.GetAABB(), Color.Red);
                Game.DrawArea(m_magnetJoint.GetAABB(), Color.Magenta);

                foreach (var o in objectsInRadius)
                {
                    if (!m_pulledObjects.ContainsKey(o.UniqueID) && (ScriptHelper.IsDynamicObject(o) || ScriptHelper.IsPlayer(o)))
                    {
                        Pull(o);
                    }
                }

                var destroyArea = ScriptHelper.GrowFromCenter(HoverPosition, DestroyRadius * 2);
                objectsInRadius = Game.GetObjectsByArea(filterArea, PhysicsLayer.Active)
                    .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), HoverPosition, DestroyRadius));

                foreach (var o in objectsInRadius)
                {
                    if (m_pulledObjects.ContainsKey(o.UniqueID))
                    {
                        o.SetHealth(o.GetHealth() - 1f);
                        if (o.GetHealth() == 0) o.Destroy();
                    }
                }
            }

            if (ScriptHelper.IsElapsed(m_update2Delay, 15))
            {
                m_update2Delay = Game.TotalElapsedGameTime;
                var projectiles = Game.GetProjectiles()
                    .Where(p => ScriptHelper.IntersectCircle(p.Position, HoverPosition, SuckRadius));
                foreach (var projectile in projectiles)
                {
                    if (ScriptHelper.IntersectCircle(projectile.Position, HoverPosition, 5))
                    {
                        projectile.FlagForRemoval();
                        continue;
                    }
                    var pf = Vector2.Normalize(HoverPosition - projectile.Position) * GetPullForce(projectile.Position);
                    projectile.Direction = projectile.Direction + pf;
                }
            }

            var removeList = new List<int>();
            foreach (var kv in m_pulledObjects)
            {
                var objectInfo = kv.Value;
                var o = objectInfo.Object;
                var pos = GetPositionToCenter(o);

                objectInfo.PullJoint.SetForce(GetPullForce(o));

                if (o.IsRemoved || pos == Range.Outside)
                {
                    removeList.Add(kv.Key);
                    StopPulling(objectInfo);
                }

                if (ScriptHelper.IsPlayer(o))
                {
                    var player = Game.GetPlayer(o.UniqueID);

                    if (pos <= Range.EventHorizon)
                    {
                        Game.DrawArea(player.GetAABB(), Color.Red);
                        if (!player.IsFalling)
                            ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Fall, 0);
                    }
                    else if (pos == Range.Level2)
                    {
                        Game.DrawArea(player.GetAABB(), Color.Yellow);
                        if (!player.IsStaggering)
                            ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Stagger, 0, GetStaggerDirection(player));
                    }

                    if (pos <= Range.Level1)
                    {
                        if (player.IsOnGround || player.IsLayingOnGround)
                        {
                            var playerPos = player.GetWorldPosition();
                            var newPos = playerPos + Vector2.UnitX * 1 * -Math.Sign(playerPos.X - HoverPosition.X);
                            player.SetWorldPosition(newPos);
                        }
                    }
                }
                else
                {
                    if (pos <= Range.EventHorizon)
                    {
                        if (!objectInfo.UnScrewed)
                        {
                            ScriptHelper.Unscrew(o);
                            objectInfo.UnScrewed = true;
                        }
                    }
                    else if (pos == Range.Level2)
                    {
                        if (!objectInfo.UnScrewed && RandomHelper.Percentage(0.01f))
                        {
                            ScriptHelper.Unscrew(o);
                            objectInfo.UnScrewed = true;
                        }
                    }
                }
            }
            foreach (var i in removeList) m_pulledObjects.Remove(i);
        }

        private float GetPullForce(Vector2 position)
        {
            return .05f * 1 / (float)Math.Pow(Vector2.Distance(position, HoverPosition) / SuckRadius, 1.5);
        }

        private float GetPullForce(IObject o)
        {
            var pullForce = ScriptHelper.IsPlayer(o) ? .1f : .05f;
            return pullForce * 1 /
                (float)Math.Pow(Vector2.Distance(o.GetWorldPosition(), HoverPosition) / SuckRadius, 1.5);
        }

        private PlayerCommandFaceDirection GetStaggerDirection(IPlayer player)
        {
            return player.GetWorldPosition().X > HoverPosition.X
                ? PlayerCommandFaceDirection.Right : PlayerCommandFaceDirection.Left;
        }

        private void Pull(IObject o)
        {
            var player = ScriptHelper.CastPlayer(o);

            if (player != null)
                ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Stagger, 20, GetStaggerDirection(player));

            var pullJoint = (IObjectPullJoint)Game.CreateObject("PullJoint");
            var originalMass = o.GetMass();

            o.SetMass(.004f);

            pullJoint.SetWorldPosition(o.GetWorldPosition());
            pullJoint.SetForce(GetPullForce(o));
            pullJoint.SetForcePerDistance(0);
            //if (Game.IsEditorTest) pullJoint.SetLineVisual(LineVisual.DJRope);

            pullJoint.SetTargetObject(o);
            pullJoint.SetTargetObjectJoint(m_magnetJoint);

            m_pulledObjects.Add(o.UniqueID, new PulledObjectInfo()
            {
                Object = o,
                OriginalMass = originalMass,
                PullJoint = pullJoint,
            });
        }

        protected override void Destroy()
        {
            base.Destroy();
            m_blackhole.Remove();
            foreach (var o in m_blackholes) o.Remove();
            m_magnetJoint.Remove();

            foreach (var objectInfo in m_pulledObjects.Values)
            {
                StopPulling(objectInfo);
            }
        }

        private void StopPulling(PulledObjectInfo objectInfo)
        {
            objectInfo.Object.SetMass(objectInfo.OriginalMass);
            objectInfo.PullJoint.Remove();

            if (ScriptHelper.IsPlayer(objectInfo.Object))
            {
                var player = Game.GetPlayer(objectInfo.Object.UniqueID);
                if (player != null)
                    player.SetInputEnabled(true);
            }
        }
    }
}
