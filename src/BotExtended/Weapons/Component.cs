using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using static BotExtended.Library.SFD;

namespace BotExtended.Weapons
{
    /// <summary>
    /// A wrapper around IObject just like Bot is a wrapper around IPlayer
    /// </summary>
    class Component
    {
        public bool RemoveWhenDestroyed { get; set; }
        public IObject Object { get; private set; }

        public string CustomID
        {
            get { return Object.CustomID; }
            set { Object.CustomID = value; }
        }

        private float m_maxHealth = 0;
        public float MaxHealth
        {
            get { return m_maxHealth == 0 ? Object.GetMaxHealth() : m_maxHealth; }
            set
            {
                m_maxHealth = value;
                Health = value;
                Object.SetHealth(value);
            }
        }

        private float m_health;
        public float Health
        {
            get { return m_health; }
            set { m_health = MathHelper.Clamp(value, 0, MaxHealth); }
        }

        public Component(string objectID, Vector2 worldPosition)
        {
            Object = Game.CreateObject(objectID, worldPosition);
            RemoveWhenDestroyed = true;
        }

        public void OnDamage(ObjectDamageArgs args)
        {
            Health -= args.Damage; // IObject.GetHealth() is already recalculated when this event is fired

            if (Object.GetHealth() == 0 && Health > 0)
            {
                Object.SetHealth(Math.Min(Object.GetMaxHealth(), Health));
            }

            if (Health == 0)
            {
                if (RemoveWhenDestroyed)
                    Object.Remove();
                else
                    Object.SetHealth(Object.GetMaxHealth());
            }
        }
    }
}
