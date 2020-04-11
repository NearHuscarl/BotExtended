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
        public override bool IsRemoved { get; protected set; }
        public float LightningDamage { get; private set; }

        public LightningProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Lightning)
        {
            IsRemoved = false;
        }

        protected override bool OnProjectileCreated()
        {
            LightningDamage = 3f;

            if (IsShotgunShell)
                LightningDamage /= ProjectilesPerShell * 1.5f;

            return true;
        }

        private HashSet<int> m_electrocutedObjects = new HashSet<int>();
        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            var o = Game.GetObject(args.HitObjectID);
            if (o != null)
                Electrocute(o);
        }

        protected override void OnProjectileExploded(IEnumerable<IPlayer> playersInRadius)
        {
            base.OnProjectileExploded(playersInRadius);
            foreach (var p in playersInRadius) Electrocute(p);
        }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (m_pendingUpdate.Any())
            {
                if (Game.TotalElapsedGameTime >= m_pendingUpdate.First().HitTime)
                {
                    var i = m_pendingUpdate.First();
                    i.Action.Invoke();
                    m_pendingUpdate.RemoveAt(0);
                }
            }
            else
            {
                if (Instance.IsRemoved) IsRemoved = true;
            }
        }

        private class Info
        {
            public Action Action;
            public float HitTime;
        }
        private List<Info> m_pendingUpdate = new List<Info>();
        private void Electrocute(IObject obj)
        {
            if (m_electrocutedObjects.Contains(obj.UniqueID) || obj.IsRemoved) return;

            var position = obj.GetWorldPosition();
            if (!ScriptHelper.IsIndestructible(obj))
            {
                var p = ScriptHelper.CastPlayer(obj);
                if (p != null) p.DealDamage(LightningDamage);
                else obj.SetHealth(obj.GetHealth() - LightningDamage);
                Game.PlayEffect(EffectName.Electric, position);
                if (RandomHelper.Percentage(.02f))
                {
                    Game.SpawnFireNode(position, Vector2.Zero);
                    Game.PlayEffect(EffectName.FireTrail, position);
                }
            }
            m_electrocutedObjects.Add(obj.UniqueID);

            foreach (var p in GetPlayerInRange(obj))
            {
                m_pendingUpdate.Add(new Info()
                {
                    HitTime = m_pendingUpdate.Any() ? m_pendingUpdate.Last().HitTime + 23 : Game.TotalElapsedGameTime,
                    Action = () =>
                    {
                        if (p == null || p.IsRemoved || m_electrocutedObjects.Contains(p.UniqueID)) return;
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
                                m_electrocutedObjects.Add(result.ObjectID);
                                break;
                            }

                            if (IsConductive(result.HitObject))
                                Electrocute(result.HitObject);

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

        private IEnumerable<IPlayer> GetPlayerInRange(IObject electrocutedObject)
        {
            var position = electrocutedObject.GetWorldPosition();

            if (ScriptHelper.IsPlayer(electrocutedObject))
            {
                var filterArea = ScriptHelper.GrowFromCenter(position, ElectrocuteRadius * 2);
                return Game.GetObjectsByArea<IPlayer>(filterArea)
                    .Where(o => !m_electrocutedObjects.Contains(o.UniqueID)
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
