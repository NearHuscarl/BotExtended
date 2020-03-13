using System.Collections.Generic;

namespace BotExtended.Bots
{
    class SurvivorBot : Bot
    {
        public SurvivorBot(BotArgs args) : base (args) { }

        private int m_actualMaxHealth = 100;
        public override void OnSpawn(IEnumerable<Bot> bots)
        {
            base.OnSpawn(bots);
            // Fake blood on the face to make it look like the infected
            // NOTE: Don't modify modifiers in ctor. modifiers will be applied after ctor call and before OnSpawn call
            var modifiers = Player.GetModifiers();
            m_actualMaxHealth = (int)Player.GetMaxHealth();
            modifiers.MaxHealth = m_actualMaxHealth * 100;
            Player.SetModifiers(modifiers);
        }

        private float m_oldHealth;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            var currentHealth = Player.GetHealth();
            if (currentHealth >= m_actualMaxHealth)
            {
                Player.SetHealth(m_actualMaxHealth);
            }
        }
    }
}
