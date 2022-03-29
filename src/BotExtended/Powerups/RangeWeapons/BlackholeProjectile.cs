using System;
using System.Collections.Generic;
using System.Linq;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    enum BlackholeSize { Small, Big }

    class BlackholeProjectile : HoveringProjectile
    {
        private class PulledObjectInfo
        {
            public IObject Object;
            public IObjectPullJoint PullJoint;
            public float OriginalMass;
            public bool UnScrewed = false;
            public bool BlockedByWall = false;
        }

        private static List<Vector2> BlackholeLocations = new List<Vector2>();

        private float ActiveTime = 4000;
        public const float SuckRadius = 150;
        public const float PullRadius = 100;
        public const float EventHorizon = 50;
        public const float DestroyRadius = 25;

        private IObject m_blackhole;
        private IObjectRevoluteJoint m_blackholeRotor;

        private IObjectTargetObjectJoint m_magnetJoint;
        private Dictionary<int, PulledObjectInfo> m_pulledObjects = new Dictionary<int, PulledObjectInfo>();
        private enum Range { Center, EventHorizon, Level2, Level1, Outside, }

        private BlackholeSize m_size;
        public BlackholeSize Size
        {
            get { return m_size; }
            private set
            {
                m_size = value;
                if (value == BlackholeSize.Big)
                {
                    ExplodeRange = 50;
                    ExplodeRange2 = .5f;
                    ActiveTime = Game.IsEditorTest ? 30000 : 4000;
                }
                else
                {
                    ExplodeRange = 0;
                    ExplodeRange2 = 0;
                    ActiveTime = 2000;
                }
            }
        }

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
            {
                Size = BlackholeSize.Big;
            }
            else
                Size = BlackholeSize.Small;
            return true;
        }

        protected override void OnHover()
        {
            base.OnHover();
            Instance.FlagForRemoval();

            // no blackhole too close with other blackholes
            foreach (var hole in BlackholeLocations)
            {
                if (ScriptHelper.GrowFromCenter(hole, DestroyRadius * 2).Contains(HoverPosition))
                {
                    Destroy();
                    return;
                }
            }
            BlackholeLocations.Add(HoverPosition);

            var noCollision = Constants.NoCollision;

            if (Size == BlackholeSize.Big)
            {
                Game.RunCommand("/settime .1");
                ScriptHelper.Timeout(() => Game.RunCommand("/settime 1"), 2000);
            }

            m_blackhole = Game.CreateObject("Blackhole00A", HoverPosition);
            m_blackhole.SetBodyType(BodyType.Dynamic);
            m_blackhole.SetCollisionFilter(noCollision);

            m_blackholeRotor = (IObjectRevoluteJoint)Game.CreateObject("RevoluteJoint", HoverPosition);
            m_blackholeRotor.SetTargetObjectA(m_blackhole);
            m_blackholeRotor.SetMotorEnabled(true);
            m_blackholeRotor.SetMotorSpeed(20);
            m_blackholeRotor.SetMaxMotorTorque(20);

            ScriptHelper.RunIn(() => Game.DrawCircle(HoverPosition, .5f, Color.Green), 10000);

            m_magnetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint", HoverPosition);
            m_magnetJoint.SetTargetObject(m_blackhole);
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
            DrawDebugging();

            if (HoverTime >= ActiveTime)
            {
                Destroy(); return;
            }

            if (ScriptHelper.IsElapsed(m_updateDelay, 30) && Size == BlackholeSize.Big)
            {
                m_updateDelay = Game.TotalElapsedGameTime;
                UpdateObjectsStatus();
            }

            if (ScriptHelper.IsElapsed(m_update2Delay, 15))
            {
                m_update2Delay = Game.TotalElapsedGameTime;
                UpdateEntities();
            }

            if (Size == BlackholeSize.Big)
                UpdatePulledObjects();
        }

        private void UpdateEntities()
        {
            var projectiles = Game.GetProjectiles()
                .Where(p => ScriptHelper.IntersectCircle(p.Position, HoverPosition, SuckRadius));
            foreach (var projectile in projectiles)
            {
                if (ScriptHelper.IntersectCircle(projectile.Position, HoverPosition, 10))
                {
                    projectile.FlagForRemoval();
                    continue;
                }
                var pf = Vector2.Normalize(HoverPosition - projectile.Position) * GetPullForce(projectile.Position);
                projectile.Direction = projectile.Direction + pf;
            }

            if (Size == BlackholeSize.Small) return;

            var filterArea = ScriptHelper.GrowFromCenter(HoverPosition, SuckRadius * 2);
            var fireNodes = Game.GetFireNodes(filterArea)
                .Where(p => ScriptHelper.IntersectCircle(p.Position, HoverPosition, SuckRadius));
            foreach (var fireNode in fireNodes)
            {
                if (fireNode.AttachedToObjectID != 0 && Game.GetObject(fireNode.AttachedToObjectID).Name == "ItemDebrisFlamethrower01")
                    continue;

                // cannot move fireNode, create object with fireNode and move it instead
                var position = fireNode.Position;
                var objectIgnited = Game.CreateObject("ItemDebrisFlamethrower01", position);

                objectIgnited.SetLinearVelocity(fireNode.Velocity);
                objectIgnited.SetMaxFire();

                Game.EndFireNode(fireNode.InstanceID);
            }
        }

        private void UpdateObjectsStatus()
        {
            var filterArea = ScriptHelper.GrowFromCenter(HoverPosition, SuckRadius * 2);
            var objectsInArea = Game.GetObjectsByArea(filterArea)
                .Where(ScriptHelper.IsInteractiveObject);

            var objectInSuckRadius = objectsInArea
                .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), HoverPosition, SuckRadius)).ToList();

            // closest objects to HoverPosition first
            objectInSuckRadius
                .Sort((a, b) => (int)Vector2.Distance(a.GetWorldPosition(), HoverPosition) - (int)Vector2.Distance(b.GetWorldPosition(), HoverPosition));

            foreach (var o in objectInSuckRadius)
            {
                if (m_pulledObjects.Count >= 20) break; // lag :(
                if (!m_pulledObjects.ContainsKey(o.UniqueID))
                {
                    var objsBlockedByWall = m_pulledObjects.Values.Where(x => x.BlockedByWall).Count();
                    var isBlockedByWall = Game.RayCast(HoverPosition, o.GetWorldPosition(), new RayCastInput()
                    {
                        BlockExplosions = RayCastFilterMode.True,
                        ClosestHitOnly = true,
                    }).Where(x => x.HitObject != null).Any();

                    if (isBlockedByWall && objsBlockedByWall <= 10)
                        Pull(o, isBlockedByWall);
                    if (!isBlockedByWall && m_pulledObjects.Count - objsBlockedByWall <= 10)
                        Pull(o, isBlockedByWall);
                }
            }

            var objectsInDestroyedRadius = objectsInArea
                .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), HoverPosition, DestroyRadius));

            foreach (var o in objectsInDestroyedRadius)
            {
                PulledObjectInfo info;
                if (m_pulledObjects.TryGetValue(o.UniqueID, out info))
                {
                    var smallObject = o.GetAABB().Width * o.GetAABB().Height <= 100;

                    o.DealDamage(1f);
                    if (smallObject || o.GetHealth() == 0)
                        o.Destroy();
                }
            }
        }

        private void UpdatePulledObjects()
        {
            foreach (var kv in m_pulledObjects.ToList())
            {
                var objectInfo = kv.Value;
                var o = objectInfo.Object;
                var pos = GetPositionToCenter(o);

                objectInfo.PullJoint.SetForce(GetPullForce(o));

                // TODO: fix this bug where the lamp doesn't get removed at 0 health
                // https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&p=24809#p24809
                if (o.GetHealth() == 0 && o.Name == "Lamp00")
                {
                    o.Destroy();
                    o.Remove();
                }

                if (pos == Range.Outside || o.IsRemoved)
                {
                    m_pulledObjects.Remove(kv.Key);
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
        }

        private void DrawDebugging()
        {
            if (Game.IsEditorTest)
            {
                Game.DrawCircle(HoverPosition, SuckRadius, Color.Cyan);
                Game.DrawCircle(HoverPosition, PullRadius, Color.Red);
                Game.DrawCircle(HoverPosition, EventHorizon, Color.Red);
                Game.DrawCircle(HoverPosition, DestroyRadius, Color.Red);
                Game.DrawArea(m_blackhole.GetAABB(), Color.Blue);
                Game.DrawArea(m_magnetJoint.GetAABB(), Color.Magenta);
                foreach (var o in m_pulledObjects.Values) Game.DrawArea(o.Object.GetAABB(), Color.Yellow);
            }
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

        private void Pull(IObject o, bool isBlockedByWall)
        {
            var player = ScriptHelper.AsPlayer(o);

            if (player != null)
                ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Stagger, 20, GetStaggerDirection(player));

            var pullJoint = (IObjectPullJoint)Game.CreateObject("PullJoint");
            var originalMass = o.GetMass();

            if (o.GetBodyType() == BodyType.Static) o.SetBodyType(BodyType.Dynamic);
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
                BlockedByWall = isBlockedByWall,
            });
        }

        protected override void Destroy()
        {
            base.Destroy();
            BlackholeLocations.Remove(HoverPosition);

            if (m_blackhole != null) m_blackhole.Remove();
            if (m_blackholeRotor != null) m_blackholeRotor.Remove();
            if (m_magnetJoint != null) m_magnetJoint.Remove();

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
