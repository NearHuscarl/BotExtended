﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class MiniGun: RangeWpn
    {
        public int GetCooldownTime()
        {
            switch (Name)
            {
                // From SFD 1.3.4 Source code (Cooldown property in Wpn___ file)
                case WeaponItem.M60: return 100;
                case WeaponItem.MACHINE_PISTOL: return 105;
                case WeaponItem.ASSAULT: return 95;
                case WeaponItem.UZI: return 75;
                case WeaponItem.SILENCEDUZI: return 75;
                case WeaponItem.MP50: return 105;
                case WeaponItem.SMG: return 95;
                case WeaponItem.TOMMYGUN: return 105;
                default: return 100;
            }
        }

        public MiniGun(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner, name, powerup) { }

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (_fireTime == -1 || !IsEquipping) return;

            var cooldown = GetCooldownTime();
            if (_exraShots == 0 && Game.TotalElapsedGameTime >= _fireTime + cooldown / 3) Fire();
            if (_exraShots == 1 && Game.TotalElapsedGameTime >= _fireTime + cooldown * 2 / 3) Fire();
        }

        private void Fire()
        {
            var muzzle = GetMuzleInfo();
            if (!muzzle.IsSussess) return;

            var accuracyDeflection = 0.13f / 2;
            var angle = ScriptHelper.GetAngle(muzzle.Direction);
            var finalDirection = RandomHelper.Direction(angle - accuracyDeflection, angle + accuracyDeflection, true);
            var projectile = Game.SpawnProjectile(ProjectileItem, muzzle.Position, finalDirection, ProjectilePowerup);

            projectile.DamageDealtModifier = DamageModifier;
            Game.PlaySound(ScriptHelper.GetSoundID(Name), muzzle.Position);
            if (_exraShots == 0 && RandomHelper.Percentage(.4f)) Game.PlayEffect(EffectName.Dig, muzzle.Position);
            _exraShots++;

            // recoil
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
