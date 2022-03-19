using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups
{
    abstract class ProjectileBase
    {
        public abstract int ID { get; }
        public PlayerTeam Team { get; private set; }
        // Don't reference Instance.InitialOwnerPlayerID. Instance can be updated and makes
        // Instance.InitialOwnerPlayerID become obsolete, since there is no way to copy InitialOwnerPlayerID
        // value to the new Instance
        // Also CustomProjectile do not have its own InitialOwnerPlayerID
        public int InitialOwnerPlayerID { get; private set; }
        public abstract bool IsRemoved { get; protected set; }
        public RangedWeaponPowerup Powerup { get; protected set; }
        public bool IsCustomProjectile { get; protected set; }
        protected float UpdateDelay { get; set; }

        public ProjectileBase(IProjectile projectile, RangedWeaponPowerup powerup)
        {
            Powerup = powerup;
            UpdateDelay = 0f;
            // in case the original player is not available when the projectile hits
            Team = Game.GetPlayer(projectile.InitialOwnerPlayerID).GetTeam();
            InitialOwnerPlayerID = projectile.InitialOwnerPlayerID;
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
        public virtual void OnRemove() { }

        public bool SameTeam(IPlayer player)
        {
            if (player == null) return false;
            return Team == player.GetTeam() && Team != PlayerTeam.Independent
                || InitialOwnerPlayerID == player.UniqueID;
        }
    }
}
