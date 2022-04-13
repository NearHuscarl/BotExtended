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
            LightningDamage = 14f;

            var start = projectile.Position + projectile.Direction * 10;
            var end = start + projectile.Direction * ScriptHelper.GetDistanceToEdge(start, projectile.Direction);
            var result = Game.RayCast(start, end, new RayCastInput()
            {
                ProjectileHit = RayCastFilterMode.True,
                AbsorbProjectile = RayCastFilterMode.True,
                IncludeOverlap = false,
                ClosestHitOnly = true,
            }).FirstOrDefault(r => r.HitObject != null);

            if (result.HitObject != null)
            {
                end = result.Position;
                Electrocute(result.HitObject);
            }

            DrawElectricTrace(start, end);

            projectile.FlagForRemoval();
        }

        private HashSet<int> _electrocutedObjects = new HashSet<int>();

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (!_pendingUpdate.Any()) return;
            if (Game.TotalElapsedGameTime >= _pendingUpdate.First().HitTime)
            {
                var i = _pendingUpdate.First();
                i.Action.Invoke();
                _pendingUpdate.RemoveAt(0);
            }
        }

        private class Info
        {
            public Action Action;
            public float HitTime;
        }
        private List<Info> _pendingUpdate = new List<Info>();

        private void Electrocute(IObject obj, int depth = 1)
        {
            if (_electrocutedObjects.Contains(obj.UniqueID) || obj.IsRemoved) return;
            if (_electrocutedObjects.Count >= 50)
            {
                IsRemoved = true;
                return;
            }

            var position = obj.GetWorldPosition();
            if (obj.Destructable)
            {
                obj.DealDamage(LightningDamage);
                Game.PlayEffect(EffectName.Electric, position);

                if (ScriptHelper.IsPlayer(obj) && obj.GetHealth() == 0) obj.SetMaxFire();
                if (RandomHelper.Percentage(.02f))
                {
                    Game.SpawnFireNode(position, Vector2.Zero);
                    Game.PlayEffect(EffectName.FireTrail, position);
                }

                var hitPlayer = ScriptHelper.AsPlayer(obj);
                if (hitPlayer != null && !hitPlayer.IsRemoved && !hitPlayer.IsBurnedCorpse)
                {
                    SetBurntSkin(hitPlayer);
                }

                Game.PlaySound("ElectricSparks", position);
                _electrocutedObjects.Add(obj.UniqueID);
            }

            foreach (var p in GetPlayersInRange(obj))
            {
                DrawElectricTrace(position, p.GetWorldPosition());
                _pendingUpdate.Add(new Info()
                {
                    HitTime = _pendingUpdate.Any() ? _pendingUpdate.Last().HitTime + 33 : Game.TotalElapsedGameTime,
                    Action = () => Electrocute(p, ++depth),
                });
            }
        }

        private void SetBurntSkin(IPlayer hitPlayer)
        {
            var bot = BotManager.GetBot(hitPlayer);
            var profile = bot.GetProfile();
            var burntProfile = bot.GetProfile();

            if (profile.Skin.Name == "BurntSkin") return;

            burntProfile.Skin.Name = "BurntSkin";
            burntProfile.Head = null;
            burntProfile.ChestOver = null;
            burntProfile.ChestUnder = null;
            burntProfile.Hands = null;
            burntProfile.Waist = null;
            burntProfile.Legs = null;
            burntProfile.Accesory = null;

            hitPlayer.SetProfile(burntProfile);
            ScriptHelper.Timeout(() => hitPlayer.SetProfile(profile), 250);
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
