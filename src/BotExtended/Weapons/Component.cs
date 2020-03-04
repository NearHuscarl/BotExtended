using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

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

        public bool OnDamage(ObjectDamageArgs args)
        {
            var cloneObj = false;
            Health -= args.Damage; // IObject.GetHealth() is already recalculated when this event is fired

            if (Object.GetHealth() == 0 && Health > 0 || !RemoveWhenDestroyed)
            {
                Object.Remove();
                Object = CloneObject();
                cloneObj = true;
            }

            return cloneObj;
        }

        private IObject CloneObject()
        {
            // TODO: this line SOMETIMES throws error wtf. https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&t=3956
            var clone = Game.CreateObject(Object.Name, Object.GetWorldPosition());

            clone.CustomID = Object.CustomID;
            clone.SetAngle(Object.GetAngle());
            clone.SetLinearVelocity(Object.GetLinearVelocity());
            clone.SetAngularVelocity(Object.GetAngularVelocity());
            clone.SetFaceDirection(Object.GetFaceDirection());
            clone.SetBodyType(Object.GetBodyType());

            return clone;
        }
    }
}
