using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class BandidoBot : Bot
    {
        public BandidoBot(BotArgs args) : base(args)
        {
            _isElapsedAmmoFire = ScriptHelper.WithIsElapsed(50, 350);
        }

        private uint _ammoLeft = 500;
        private bool _fireAmmoFromCorpse = false;
        private Vector2 _firePosition;
        private Func<bool> _isElapsedAmmoFire;
        private WeaponItem _lastRangeWpn = WeaponItem.NONE;
        public static readonly float FireAmmoOnDeathChance = Game.IsEditorTest ? 1 : .65f;

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (!_fireAmmoFromCorpse || _ammoLeft == 0) return;
            if (_isElapsedAmmoFire())
            {
                var proj = Mapper.GetProjectile(_lastRangeWpn);
                Game.SpawnProjectile(proj, _firePosition, RandomHelper.Direction(15, 180 - 15));
                Game.PlaySound(ScriptHelper.GetSoundID(_lastRangeWpn), _firePosition);
                _ammoLeft--;
            }
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (args.Removed) return;

            if (Player.CurrentPrimaryRangedWeapon.WeaponItem != WeaponItem.NONE)
                _lastRangeWpn = Player.CurrentPrimaryRangedWeapon.WeaponItem;
            if (Player.CurrentSecondaryRangedWeapon.WeaponItem != WeaponItem.NONE)
                _lastRangeWpn = Player.CurrentSecondaryRangedWeapon.WeaponItem;

            Events.UpdateCallback cb = null;
            cb = Events.UpdateCallback.Start((e) =>
            {
                if (!Player.IsOnGround) return;

                var groundObj = ScriptHelper.GetGroundObject(Player);
                if (groundObj == null || groundObj.GetCollisionFilter().CategoryBits != CategoryBits.StaticGround)
                    return;

                if (_lastRangeWpn != WeaponItem.NONE && RandomHelper.Percentage(FireAmmoOnDeathChance))
                {
                    _firePosition = new Vector2(Position.X, Position.Y - 2);
                    Game.CreateObject("AmmoStash00", _firePosition);
                    // fire above corpse so it doesn't get gibbed
                    _firePosition.Y += 9;
                    _fireAmmoFromCorpse = true;
                }
                cb.Stop();
            }, 270);
        }
    }
}
