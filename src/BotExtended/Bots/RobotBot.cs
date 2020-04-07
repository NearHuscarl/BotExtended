using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class RobotBot : Bot
    {
        private float m_electricElapsed = 0f;

        public RobotBot(BotArgs args) : base(args) { }

        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            base.OnSpawn(bots);
            Player.SetHitEffect(PlayerHitEffect.Metal);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead)
            {
                UpdateCorpse(elapsed);
            }
            else
            {
                var mod = Player.GetModifiers();
                var healthLeft = mod.CurrentHealth / mod.MaxHealth;

                if (healthLeft <= 0.4f)
                    UpdateNearDeathEffects(elapsed, healthLeft);
            }
        }

        private void UpdateNearDeathEffects(float elapsed, float healthLeft)
        {
            m_electricElapsed += elapsed;

            if (m_electricElapsed >= 700)
            {
                if (RandomHelper.Boolean())
                {
                    var position = RandomHelper.WithinArea(Player.GetAABB());

                    if (healthLeft <= 0.2f)
                    {
                        Game.PlayEffect(EffectName.Fire, position);
                        Game.PlaySound("Flamethrower", position);
                    }
                    if (healthLeft <= 0.3f)
                    {
                        Game.PlayEffect(EffectName.Sparks, position);
                    }
                    if (healthLeft <= 0.4f)
                    {
                        if (RandomHelper.Boolean())
                        {
                            Game.PlayEffect(EffectName.Steam, position);
                            Game.PlayEffect(EffectName.Steam, position);
                        }
                        Game.PlayEffect(EffectName.Electric, position);
                        Game.PlaySound("ElectricSparks", position);
                    }
                    m_electricElapsed = 0f;
                }
                else
                {
                    m_electricElapsed -= RandomHelper.Between(0, m_electricElapsed);
                }
            }
        }

        private void UpdateCorpse(float elapsed)
        {
            if (!Player.IsDead) return; // Safeguard
            m_electricElapsed += elapsed;

            if (m_electricElapsed >= 1000)
            {
                if (RandomHelper.Boolean())
                {
                    var position = Player.GetWorldPosition();
                    position.X += RandomHelper.Between(-10, 10);
                    position.Y += RandomHelper.Between(-10, 10);

                    Game.PlayEffect(EffectName.Electric, position);

                    if (RandomHelper.Boolean())
                    {
                        Game.PlayEffect(EffectName.Steam, position);
                        Game.PlayEffect(EffectName.Steam, position);
                        Game.PlayEffect(EffectName.Steam, position);
                    }
                    if (RandomHelper.Boolean())
                        Game.PlayEffect(EffectName.Sparks, position);
                    if (RandomHelper.Boolean())
                        Game.PlayEffect(EffectName.Fire, position);

                    Game.PlaySound("ElectricSparks", position);
                    m_electricElapsed = 0f;
                }
                else
                {
                    m_electricElapsed -= RandomHelper.Between(0, m_electricElapsed);
                }
            }
        }
    }
}
