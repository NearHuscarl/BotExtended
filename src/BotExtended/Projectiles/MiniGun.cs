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
    class MiniGun: RangeWpn
    {
        public override bool IsValidPowerup()
        {
            // Automatic guns only
            return Name == WeaponItem.M60
                || Name == WeaponItem.MACHINE_PISTOL
                || Name == WeaponItem.ASSAULT
                || Name == WeaponItem.UZI
                || Name == WeaponItem.MP50
                || Name == WeaponItem.TOMMYGUN;
        }
        public int GetCooldownTime()
        {
            switch (Name)
            {
                // From SFD 1.3.4 Source code (Cooldown property in Wpn___ file)
                case WeaponItem.M60: return 100;
                case WeaponItem.MACHINE_PISTOL: return 105;
                case WeaponItem.ASSAULT: return 95;
                case WeaponItem.UZI: return 75;
                case WeaponItem.MP50: return 105;
                case WeaponItem.TOMMYGUN: return 105;
                default: return 100;
            }
        }
        public string GetSoundID()
        {
            switch (Name)
            {
                // From SFD 1.3.4 Source code (Cooldown property in Wpn___ file)
                case WeaponItem.M60: return "M60";
                case WeaponItem.MACHINE_PISTOL: return "MachinePistol";
                case WeaponItem.ASSAULT: return "AssaultRifle";
                case WeaponItem.UZI: return "UZI";
                case WeaponItem.MP50: return "MP50";
                case WeaponItem.TOMMYGUN: return "TommyGun";
                default: return "Pistol";
            }
        }

        public MiniGun(IPlayer owner, WeaponItem name) : base(owner, name, RangedWeaponPowerup.Minigun) { }

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (_fireTime == -1) return;

            var cooldown = GetCooldownTime();
            if (_exraShots == 0 && Game.TotalElapsedGameTime >= _fireTime + cooldown / 3) Fire();
            if (_exraShots == 1 && Game.TotalElapsedGameTime >= _fireTime + cooldown * 2 / 3) Fire();
        }

        private void Fire()
        {
            Vector2 position, direction;
            Owner.GetWeaponMuzzleInfo(out position, out direction);

            var accuracyDeflection = 0.13f / 2;
            var angle = ScriptHelper.GetAngle(direction);
            var finalDirection = RandomHelper.Direction(angle - accuracyDeflection, angle + accuracyDeflection, true);
            var projectile = Game.SpawnProjectile(ProjectileItem, position, finalDirection, ProjectilePowerup);

            projectile.DamageDealtModifier = DamageModifier;
            Game.PlaySound(GetSoundID(), position);
            if (_exraShots == 0 && RandomHelper.Percentage(.4f)) Game.PlayEffect(EffectName.Dig, position);
            _exraShots++;

            var pos = Owner.GetWorldPosition();
            pos.X -= Owner.GetFaceDirection() * .2f;
            Owner.SetWorldPosition(pos);
        }

        static public readonly float DamageModifier = .5f;
        private int _exraShots = 0;
        private float _fireTime = -1;
        private ProjectileItem ProjectileItem;
        private ProjectilePowerup ProjectilePowerup;

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            projectile.DamageDealtModifier = DamageModifier;

            ProjectileItem = projectile.ProjectileItem;
            ProjectilePowerup = ScriptHelper.GetPowerup(projectile);

            _fireTime = Game.TotalElapsedGameTime;
            _exraShots = 0;
        }
    }
}
