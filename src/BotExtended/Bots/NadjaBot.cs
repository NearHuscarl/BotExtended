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
        private static List<TripWire> Traps = new List<TripWire>();

        public NadjaBot(BotArgs args) : base(args)
        {
            _isElaspedPlaceTrap = ScriptHelper.WithIsElapsed(9000);
        }

        private Func<bool> _isElaspedPlaceTrap;

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (_isElaspedPlaceTrap() && CanPlaceTrap())
            {
                ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.StartCrouch, 1000).ContinueWith((r) =>
                {
                    Traps.Add((TripWire)WeaponManager.SpawnWeapon(BeWeapon.Tripwire, Player));
                });
            }
        }

        private bool CanPlaceTrap() { return Player.IsOnGround && Traps.Count < 100 && !Traps.Any(x => Vector2.Distance(x.Position, Position) < 10); }
    }
}
