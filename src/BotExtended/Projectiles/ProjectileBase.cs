using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    abstract class ProjectileBase
    {
        public abstract int ID { get; }
        public int OwnerID { get; private set; }
        public abstract bool IsRemoved { get; }
        public RangedWeaponPowerup Powerup { get; protected set; }
        public bool IsCustomProjectile { get; protected set; }
        protected float UpdateDelay { get; set; }

        public ProjectileBase(IProjectile projectile, RangedWeaponPowerup powerup)
        {
            OwnerID = projectile.InitialOwnerPlayerID;
            Powerup = powerup;
            UpdateDelay = 0f;
        }

        private float m_updateTime = 0f;
        public void OnUpdate(float elapsed)
        {
            if (ScriptHelper.IsElapsed(m_updateTime, UpdateDelay))
            {
                m_updateTime = Game.TotalElapsedGameTime;
                Update(elapsed);
            }
        }
        protected virtual void Update(float elapsed) { }

        public virtual void OnProjectileHit() { }
        public virtual void OnProjectileHit(ProjectileHitArgs args) { }
    }
}
