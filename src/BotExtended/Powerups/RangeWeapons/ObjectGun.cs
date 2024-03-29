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
    class ObjectGun : RangeWpn
    {
        private string _loadedObject;
        private IObject _bullet;

        public ObjectGun(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner, name, powerup)
        {
            DisableRangeCheck = true;
            BotManager.GetBot(Owner).UseRangeWeapon(false);
        }

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            //Game.DrawText(_loadedObject + " " + Owner.GetBotBehaviorSet().RangedWeaponUsage, Owner.GetWorldPosition());

            var bot = BotManager.GetBot(Owner);
            if (Owner.IsDead) return;

            if (_loadedObject == null)
            {
                foreach (var o in Game.GetObjectsByArea(ScriptHelper.Grow(Owner.GetAABB(), 1, 1)))
                {
                    if (!IsBullet(o)) continue;
                    
                    _loadedObject = o.Name;
                    bot.UseRangeWeapon(true);
                    o.Remove();
                    break;
                }
            }
        }

        private bool IsBullet(IObject o) { return (_bullet == null || o.UniqueID != _bullet.UniqueID) && ScriptHelper.IsDynamicObject(o); }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);
            projectile.FlagForRemoval();

            // there is a bug when you create new objects here
            try
            {
                BotManager.GetBot(Owner).UseRangeWeapon(false);
                _bullet = Game.CreateObject(_loadedObject);
                _bullet.TrackAsMissile(true);
                _bullet.SetWorldPosition(projectile.Position + projectile.Direction * _bullet.GetAABB().Width / 2);
                _bullet.SetLinearVelocity(projectile.Velocity / 10);

                ScriptHelper.Timeout(() => _bullet = null, 2500);
                _loadedObject = null;
            }
            catch { }
        }
    }
}
