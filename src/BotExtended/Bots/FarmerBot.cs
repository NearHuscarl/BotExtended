using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;
using BotExtended.Weapons;

namespace BotExtended.Bots
{
    public class FarmerBot : Bot
    {
        public FarmerBot(BotArgs args) : base(args) { }

        public override void OnSpawn()
        {
            base.OnSpawn();

            var chickens = Game.IsEditorTest ? 6 : 6;
            for (var i = 0; i < chickens; i++)
                WeaponManager.SpawnWeapon(BeWeapon.Chicken, this);
        }

        public void OnChickenDead()
        {
            WeaponManager.SpawnWeapon(BeWeapon.Chicken, this);
        }
    }
}
