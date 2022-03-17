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
        private static readonly List<BeWeapon> TrapNames = new List<BeWeapon>() { BeWeapon.ShotgunTrap, BeWeapon.FireTrap, BeWeapon.Tripwire };
        private NadjaBotController _controller;

        public NadjaBot(BotArgs args) : base(args)
        {
            _isElapsedCheckPlaceTrap = ScriptHelper.WithIsElapsed(125);
            _controller = new NadjaBotController()
            {
                Bot = this
            };
        }

        private float _placeTrapTime = 0f;
        private Func<bool> _isElapsedCheckPlaceTrap;

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead || !Player.IsAIControlled) return;

            if (ScriptHelper.IsElapsed(_placeTrapTime, 9000))
            {
                if (_isElapsedCheckPlaceTrap() && CanPlaceTrap())
                {
                    _placeTrapTime = Game.TotalElapsedGameTime;
                    ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.StartCrouch, 1000).ContinueWith((r) => PlaceTrap());
                }
            }
        }

        public void PlaceTrap() { Traps.Add(WeaponManager.SpawnWeapon(RandomHelper.GetItem(TrapNames), Player)); }

        public bool CanPlaceTrap()
        {
            var check1 = Player.IsOnGround && !Traps.Any(x => Vector2.Distance(x.Position, Position) < 10);
            if (!check1) return false;
            var groundObj = ScriptHelper.GetGroundObject(Player);
            var check2 = groundObj != null && groundObj.GetCollisionFilter().CategoryBits == CategoryBits.StaticGround;
            return check2;
        }

        public override void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos)
        {
            base.OnPlayerKeyInput(keyInfos);
            _controller.OnPlayerKeyInput(keyInfos);
        }
    }

    public class NadjaBotController
    {
        public NadjaBot Bot;
        private float _placeTrapCooldownTime = 0f;
        public void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos)
        {
            foreach (var keyInfo in keyInfos)
            {
                if (keyInfo.Event == VirtualKeyEvent.Pressed && keyInfo.Key == VirtualKey.SPRINT && Bot.Player.IsCrouching)
                {
                    if (ScriptHelper.IsElapsed(_placeTrapCooldownTime, 3000) && Bot.CanPlaceTrap())
                    {
                        _placeTrapCooldownTime = Game.TotalElapsedGameTime;
                        Bot.PlaceTrap();
                    }
                    break;
                }
            }
        }
    }
}
