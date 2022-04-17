using System;
using System.Linq;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class TacoProjectile : Projectile
    {
        public TacoProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override void OnProjectileCreated()
        {
            Instance.PowerupBounceActive = true;
            _isElapsedSwapInstance = ScriptHelper.WithIsElapsed(2500, isElapsedFirstTime: false);
            _inputCb = Events.PlayerKeyInputCallback.Start(OnPlayerInput);
        }

        private Func<bool> _isElapsedSwapInstance;
        private Events.PlayerKeyInputCallback _inputCb;
        private IPlayer _rider;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Instance.IsRemoved) return;

            if (_rider == null)
            {
                _rider = GetRocketRider();
                return;
            }

            if (_isHoldingLeft || _isHoldingRight)
            {
                if (_lastPosition == null) _lastPosition = Instance.Position;
                else Instance.Position = _lastPosition ?? Instance.Position;

                var steerAngle = ScriptHelper.GetAngle(Instance.Direction) + MathExtension.ToRadians(_isHoldingRight ? -1f : 1f);
                Instance.Direction = ScriptHelper.GetDirection(steerAngle);
            }
            else
                _lastPosition = null;
        }

        private Vector2? _lastPosition;
        private bool _isHoldingLeft;
        private bool _isHoldingRight;
        private void OnPlayerInput(IPlayer player, VirtualKeyInfo[] keyInfos)
        {
            if (_rider == null || _rider.UniqueID != player.UniqueID) return;

            foreach (var keyInfo in keyInfos)
            {
                if (keyInfo.Key == VirtualKey.AIM_RUN_LEFT)
                    _isHoldingLeft = keyInfo.Event == VirtualKeyEvent.Pressed;
                if (keyInfo.Key == VirtualKey.AIM_RUN_RIGHT)
                    _isHoldingRight = keyInfo.Event == VirtualKeyEvent.Pressed;
            }
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (!args.RemoveFlag) return;
            _inputCb.Stop();
        }
    }
}
