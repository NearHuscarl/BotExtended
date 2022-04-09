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
    class LightningProjectile : Projectile
    {
        public const float ElectrocuteRadius = 35f;
        public float LightningDamage { get; private set; }

        public override bool IsRemoved { get; protected set; }

        public LightningProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            LightningDamage = 7f;

            var start = projectile.Position + projectile.Direction * 10;
            var end = start + projectile.Direction * ScriptHelper.GetDistanceToEdge(start, projectile.Direction);
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                ProjectileHit = RayCastFilterMode.True,
                AbsorbProjectile = RayCastFilterMode.True,
                IncludeOverlap = false,
                ClosestHitOnly = true,
            }).Where(r => r.HitObject != null);

            end = results.Count() == 0 ? end : results.First().Position;
            var distance = Vector2.Distance(start, end);

            DrawElectricTrace(start, end);

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
            //Game.WriteToConsole(depth, obj.Name);
            if (depth > 3 || _electrocutedObjects.Contains(obj.UniqueID) || obj.IsRemoved)
            {
                IsRemoved = true;
                return;
            }

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
                DrawElectricTrace(position, p.GetWorldPosition());
                m_pendingUpdate.Add(new Info()
                {
                    HitTime = m_pendingUpdate.Any() ? m_pendingUpdate.Last().HitTime + 23 : Game.TotalElapsedGameTime,
                    Action = () => Electrocute(p, ++depth),
                });
            }
        }

        private IEnumerable<IPlayer> GetPlayersInRange(IObject electrocutedObject)
        {
            var position = electrocutedObject.GetWorldPosition();
            var filterArea = ScriptHelper.GrowFromCenter(position, ElectrocuteRadius * 2);
            return Game.GetObjectsByArea<IPlayer>(filterArea)
                .Where(o => !_electrocutedObjects.Contains(o.UniqueID)
                && ScriptHelper.IntersectCircle(o.GetAABB(), position, ElectrocuteRadius));
        }

        private void DrawElectricTrace(Vector2 p1, Vector2 p2)
        {
            var distance = Vector2.Distance(p1, p2);
            var direction = Vector2.Normalize(p2 - p1);

            for (var i = 0; i <= distance; i += 16)
            {
                var position = p1 + direction * i;
                var perpendicular = Vector2.Normalize(new Vector2(position.Y, -position.X));

                Game.PlayEffect(EffectName.Electric, position + perpendicular * RandomHelper.Between(-5, 5));
            }
        }
    }
}
