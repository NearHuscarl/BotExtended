using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;
using BotExtended.Weapons;

namespace BotExtended.Bots
{
    public class NadjaBot : Bot
    {
        public NadjaBot(BotArgs args) : base(args)
        {
            _isElaspedPlaceTrap = ScriptHelper.WithIsElapsed(9000);
        }

        private Func<bool> _isElaspedPlaceTrap;

        public override void OnSpawn()
        {
            base.OnSpawn();
        }

        private float _placeTrapTime = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (ScriptHelper.IsElapsed(_placeTrapTime, 9000) && Player.IsOnGround)
            {
                WeaponManager.SpawnWeapon(BeWeapon.Tripwire, Player);
                _placeTrapTime = Game.TotalElapsedGameTime;
            }
        }
    }
}
