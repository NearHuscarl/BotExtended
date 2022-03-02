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
    class LightningProjectile : Projectile
    {
        public const float ElectrocuteRadius = 20f;
        public float LightningDamage { get; private set; }

        public LightningProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Lightning)
        {
            LightningDamage = 7f;

            var start = projectile.Position + projectile.Direction * 10;
            var end = start + projectile.Direction * 600;
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                ProjectileHit = RayCastFilterMode.True,
                IncludeOverlap = false,
                ClosestHitOnly = true,
            }).Where(r => r.HitObject != null);

            end = results.Count() == 0 ? end : results.First().Position;
            var distance = Vector2.Distance(start, end);

            for (var i = 0; i <= distance; i += 16)
            {
                var position = start + projectile.Direction * i;
                var perpendicular = Vector2.Normalize(new Vector2(position.Y, -position.X));

                Game.PlayEffect(EffectName.Electric, position + perpendicular * RandomHelper.Between(-7, 7));
            }

            foreach (var result in results)
            {
                if (result.HitObject != null)
                {
                    Electrocute(result.HitObject); break;
                }
            }

            projectile.FlagForRemoval();
        }

        private HashSet<int> _electrocutedObjects = new HashSet<int>();

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (!m_pendingUpdate.Any()) return;
            if (Game.TotalElapsedGameTime >= m_pendingUpdate.First().HitTime)
            {
                var i = m_pendingUpdate.First();
                i.Action.Invoke();
                m_pendingUpdate.RemoveAt(0);
            }
        }

        private class Info
        {
            public Action Action;
            public float HitTime;
        }
        private List<Info> m_pendingUpdate = new List<Info>();
        private void Electrocute(IObject obj, int depth = 1)
        {
            if (depth > 3 || _electrocutedObjects.Contains(obj.UniqueID) || obj.IsRemoved) return;

            var position = obj.GetWorldPosition();
            if (!ScriptHelper.IsIndestructible(obj))
            {
                obj.DealDamage(LightningDamage);
                Game.PlayEffect(EffectName.Electric, position);

                if (ScriptHelper.IsPlayer(obj) && obj.GetHealth() == 0) obj.SetMaxFire();
                if (RandomHelper.Percentage(.02f))
                {
                    Game.SpawnFireNode(position, Vector2.Zero);
                    Game.PlayEffect(EffectName.FireTrail, position);
                }
            }
            _electrocutedObjects.Add(obj.UniqueID);

            foreach (var p in GetPlayersInRange(obj))
            {
                m_pendingUpdate.Add(new Info()
                {
                    HitTime = m_pendingUpdate.Any() ? m_pendingUpdate.Last().HitTime + 23 : Game.TotalElapsedGameTime,
                    Action = () =>
                    {
                        if (p == null || p.IsRemoved || _electrocutedObjects.Contains(p.UniqueID)) return;
                        var results = Game.RayCast(position, p.GetWorldPosition(), new RayCastInput()
                        {
                            IncludeOverlap = true,
                            FilterOnMaskBits = true,
                            MaskBits = CategoryBits.Player + CategoryBits.StaticGround,
                        }).Where(r => r.HitObject != null);

                        foreach (var result in results)
                        {
                            if (!result.IsPlayer && result.HitObject.GetCollisionFilter().AbsorbProjectile)
                            {
                                _electrocutedObjects.Add(result.ObjectID);
                                break;
                            }

                            if (IsConductive(result.HitObject))
                                Electrocute(result.HitObject, ++depth);

                            if (Game.IsEditorTest)
                            {
                                ScriptHelper.RunIn(() =>
                                {
                                    Game.DrawLine(position, p.GetWorldPosition());
                                    Game.DrawArea(result.HitObject.GetAABB(), Color.Cyan);
                                }, 800);
                            }
                        }
                    },
                });
            }
        }

        private IEnumerable<IPlayer> GetPlayersInRange(IObject electrocutedObject)
        {
            if (ScriptHelper.IsPlayer(electrocutedObject))
            {
                var position = electrocutedObject.GetWorldPosition();
                var filterArea = ScriptHelper.GrowFromCenter(position, ElectrocuteRadius * 2);
                return Game.GetObjectsByArea<IPlayer>(filterArea)
                    .Where(o => !_electrocutedObjects.Contains(o.UniqueID)
                    && ScriptHelper.IntersectCircle(o.GetAABB(), position, ElectrocuteRadius));
            }
            else
            {
                return Game.GetObjectsByArea<IPlayer>(electrocutedObject.GetAABB());
            }
        }

        private bool IsConductive(IObject o)
        {
            return o.CanBurn
                || o.Name.StartsWith("Metal")
                || o.Name.StartsWith("Wood");
        }
    }
}
