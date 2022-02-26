using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class TaserGun : RangeWpn
    {
        public TaserGun(IPlayer owner, WeaponItem name) : base(owner, name, RangedWeaponPowerup.Taser) { }

        // TODO: fix issue with bot shooting at long range
        public override float MaxRange { get { return 150; } }
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

        private bool _isIdle = false;
        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Owner.IsIdle && !_isIdle) RemoveObjects();
            _isIdle = Owner.IsIdle;

            var muzzleInfo = GetMuzleInfo();
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
            _weldJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
            _weldJoint.SetWorldPosition(_head.GetWorldPosition());
            _weldJoint.SetTargetObjects(new List<IObject>() { _head, _targetPlayer });

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

            var startPosition = projectile.Position;
            var velocity = projectile.Direction * 24;
            var farPos = ScriptHelper.GetFarAwayPosition();

            // Setting up the rope length
            _distanceJoint = (IObjectDistanceJoint)Game.CreateObject("DistanceJoint", farPos);
            _tail = Game.CreateObject("InvisibleBlockNoCollision", farPos);
            _head = Game.CreateObject("ItemDebrisFlamethrower01", farPos + projectile.Direction * MaxRange);
            _targetObject = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint", farPos + projectile.Direction * MaxRange);

            ScriptHelper.Timeout(() =>
            {
                _tail.SetWorldPosition(startPosition);
                _distanceJoint.SetWorldPosition(Vector2.Zero);
                _distanceJoint.SetLineVisual(LineVisual.DJSteelWire);
                _head.SetWorldPosition(startPosition);
                _head.SetLinearVelocity(velocity);
                _targetObject.SetWorldPosition(startPosition);
            }, 0);
            
            _distanceJoint.SetTargetObject(_tail);
            _distanceJoint.SetLengthType(DistanceJointLengthType.Elastic);
            _distanceJoint.SetTargetObjectJoint(_targetObject);
            _targetObject.SetTargetObject(_head);
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
