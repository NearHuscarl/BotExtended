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
    class TelsaProjectile : Projectile
    {
        public const float ZapRadius = 15f;

        public TelsaProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Telsa)
        {
        }

        protected override bool OnProjectileCreated()
        {
            if (!IsSlowProjectile(Instance))
                Instance.Velocity = Instance.Direction * 400;
            return true;
        }

        private float m_effectDelay = 0f;
        private HashSet<int> m_electrocutedPlayers = new HashSet<int>();
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Instance.TotalDistanceTraveled <= ZapRadius)
                return;

            if (ScriptHelper.IsElapsed(m_effectDelay, 75))
            {
                m_effectDelay = Game.TotalElapsedGameTime;
                Game.PlayEffect(EffectName.Electric, Instance.Position);
            }

            var filterArea = ScriptHelper.GrowFromCenter(Instance.Position, ZapRadius * 2);
            var players = Game.GetObjectsByArea<IPlayer>(filterArea)
                .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), Instance.Position, ZapRadius));

            foreach (var player in players)
            {
                var position = player.GetWorldPosition();

                if (!m_electrocutedPlayers.Contains(player.UniqueID))
                {
                    Game.PlayEffect(EffectName.Electric, position);
                    Game.PlaySound("ElectricSparks", position);

                    if (RandomHelper.Percentage(.02f))
                    {
                        Game.SpawnFireNode(position, Vector2.Zero);
                        Game.PlayEffect(EffectName.FireTrail, position);
                    }
                }
                ScriptHelper.DealDamage(player, .5f);
                m_electrocutedPlayers.Add(player.UniqueID);
            }
        }
    }
}
