using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class MuzzleInfo
    {
        public Vector2 Position;
        public Vector2 Direction;
        public bool IsSussess = false;
    }

    class RangeWpn : Wpn
    {
        public RangedWeaponPowerup Powerup { get; protected set; }

        virtual public bool IsValidPowerup() { return true; }
        virtual public float MaxRange { get { return float.MaxValue; } }

        public RangeWpn(IPlayer owner) : this(owner, WeaponItem.NONE, RangedWeaponPowerup.None) { }
        public RangeWpn(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup)
            : base(owner, name)
        {
            Powerup = powerup;
            _isElapsedCheckRange = ScriptHelper.WithIsElapsed(95);
            if (!IsValidPowerup()) throw new Exception("Weapon " + name + " cannot have powerup " + powerup);
        }

        public void Add(WeaponItem name, RangedWeaponPowerup powerup)
        {
            Name = name;
            Type = Mapper.GetWeaponItemType(name);
            Powerup = powerup;
        }

        public override void Remove()
        {
            base.Remove();
            Powerup = RangedWeaponPowerup.None;
        }

        private bool _oldManualAiming = false;
        private Func<bool> _isElapsedCheckRange;
        public virtual void Update(float elapsed)
        {
            if (!_oldManualAiming && Owner.IsManualAiming)
                OnStartManualAim();
            if (_oldManualAiming && !Owner.IsManualAiming)
                OnStopManualAim();
            _oldManualAiming = Owner.IsManualAiming;

            // don't shoot if the enemy is too far away because some guns have limited range
            if (Powerup != RangedWeaponPowerup.None && _isElapsedCheckRange())
            {
                foreach (var player in Game.GetPlayers())
                {
                    if (!ScriptHelper.SameTeam(player, Owner))
                    {
                        var inRange = ScriptHelper.IntersectCircle(player.GetAABB(), Owner.GetWorldPosition(), MaxRange);
                        BotManager.GetBot(Owner).UseRangeWeapon(inRange);
                    }
                }
            }
        }

        protected virtual void OnStartManualAim() { }
        protected virtual void OnStopManualAim() { }

        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos) { }

        public virtual void OnProjectileCreated(IProjectile projectile) { }
        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args) { }

        public MuzzleInfo GetMuzleInfo()
        {
            Vector2 position, direction;
            var result = Owner.GetWeaponMuzzleInfo(out position, out direction);

            return new MuzzleInfo
            {
                Position = position,
                Direction = direction,
                IsSussess = result,
            };
        }
    }
}
