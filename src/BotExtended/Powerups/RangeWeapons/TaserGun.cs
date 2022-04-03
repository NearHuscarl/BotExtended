using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class TaserGun : RangeWpn
    {
        public TaserGun(IPlayer owner, WeaponItem name) : base(owner, name, RangedWeaponPowerup.Taser) { }

        public override float MaxRange { get { return 100; } }
        public override bool IsValidPowerup()
        {
            return Name == WeaponItem.PISTOL
                || Name == WeaponItem.PISTOL45
                || Name == WeaponItem.REVOLVER;
        }

        private IObject _head;
        private IObject _tail;
        private IObjectDistanceJoint _distanceJoint;
        private IObjectTargetObjectJoint _targetObject;
        private IObjectWeldJoint _weldJoint;
        private IPlayer _targetPlayer;

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (!IsEquipping) return;

            var muzzleInfo = GetMuzleInfo();

            if (!Owner.IsHipFiring && !Owner.IsManualAiming || !muzzleInfo.IsSussess) RemoveObjects();
            if (!muzzleInfo.IsSussess) return;
            
            if (_tail != null) _tail.SetWorldPosition(muzzleInfo.Position);

            if (_targetPlayer == null && _head != null)
            {
                foreach (var bot in BotManager.GetBots())
                {
                    if (_head.GetAABB().Intersects(bot.Player.GetAABB()) && bot.Player.UniqueID != Owner.UniqueID)
                    {
                        Stun(bot);
                        break;
                    }
                }
            }
        }

        private void Stun(Bot bot)
        {
            if (bot.IsStunned) return;

            _targetPlayer = bot.Player;
            _targetPlayer.SetValidBotEliminateTarget(false);
            _weldJoint = ScriptHelper.Weld(_head, _targetPlayer);

            bot.Stun(6000).ContinueWith((r) =>
            {
                _targetPlayer.SetValidBotEliminateTarget(true);
                _targetPlayer = null;
                RemoveObjects();
            });
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            projectile.FlagForRemoval();

            if (_head != null) RemoveObjects();

            var muzzleInfo = GetMuzleInfo();
            if (!muzzleInfo.IsSussess) return;

            var velocity = projectile.Direction * 24;
            _head = Game.CreateObject("ItemDebrisFlamethrower01", projectile.Position);
            _head.SetLinearVelocity(velocity);
            var result = ScriptHelper.CreateRope(projectile.Position, _head, MaxRange, LineVisual.DJSteelWire);

            _tail = result.DistanceJointObject;
            _targetObject = result.TargetObjectJoint;
            _distanceJoint = result.DistanceJoint;
        }

        private void RemoveObjects()
        {
            if (_distanceJoint == null) return;
            _distanceJoint.Remove();
            _targetObject.Remove();
            _head.Remove();
            _tail.Remove();

            if (_weldJoint == null) return;
            _weldJoint.Remove();
        }
    }
}
