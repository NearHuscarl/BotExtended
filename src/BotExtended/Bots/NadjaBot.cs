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
        private static List<Weapon> Traps = new List<Weapon>();

        public NadjaBot(BotArgs args) : base(args) { }

        private float _placeTrapTime = 0f;

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (ScriptHelper.IsElapsed(_placeTrapTime, 9000) && CanPlaceTrap())
            {
                _placeTrapTime = Game.TotalElapsedGameTime;
                ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.StartCrouch, 1000).ContinueWith((r) =>
                {
                    Traps.Add(WeaponManager.SpawnWeapon(RandomHelper.GetItem(BeWeapon.Tripwire, BeWeapon.Tripwire), Player));
                });
            }
        }

        private bool CanPlaceTrap() { return Player.IsOnGround && !Traps.Any(x => Vector2.Distance(x.Position, Position) < 10); }
    }
}
