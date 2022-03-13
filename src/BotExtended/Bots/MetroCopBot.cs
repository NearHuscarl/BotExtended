using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;
using BotExtended.Weapons;

namespace BotExtended.Bots
{
    public class MetroCopBot : Bot
    {
        public static List<LaserSweeper> Sweepers = new List<LaserSweeper>();

        public MetroCopBot(BotArgs args) : base(args)
        {
            _isElaspedSpawnSweeper = ScriptHelper.WithIsElapsed(4340);
        }

        private Func<bool> _isElaspedSpawnSweeper;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (!Player.IsDead && _isElaspedSpawnSweeper() && Sweepers.Count < 2)
                Sweepers.Add((LaserSweeper)WeaponManager.SpawnWeapon(BeWeapon.LaserSweeper, this));
        }
    }
}
