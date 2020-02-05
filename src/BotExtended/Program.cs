using System;
using SFDGameScriptInterface;
using System.Collections.Generic;

namespace SFDScript.BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        /// <summary>
        /// Placeholder constructor that's not to be included in the ScriptWindow!
        /// </summary>
        public GameScript() : base(null) { }

        public void OnStartup()
        {
            List<int> ints = new List<int> { 1 };
            // How to create a bot
            // 1. Define bot type in BotType.cs
            // 2. Define bot profile in BotProfiles.cs
            // 3. Define bot weapon in BotWeapons.cs
            // 4. Define bot behavior in BotBehaviors.cs (optional)
            // 5. Define bot info in BotInfos.cs
            // 6. Define bot class in Bots/ and add it to BotFactory.cs for additional behaviors (optional)
            // 7. Define bot group and sub-group in BotGroupSets.cs

            try
            {
                //System.Diagnostics.Debugger.Break();

                if (Game.IsEditorTest)
                {
                    var player = Game.GetPlayers()[0];
                    var modifiers = player.GetModifiers();

                    modifiers.MaxHealth = 5000;
                    modifiers.CurrentHealth = 5000;
                    modifiers.InfiniteAmmo = 1;
                    //modifiers.MeleeStunImmunity = 1;

                    player.SetModifiers(modifiers);
                    player.GiveWeaponItem(WeaponItem.WHIP);
                    player.GiveWeaponItem(WeaponItem.FLAREGUN);
                    player.GiveWeaponItem(WeaponItem.MACHINE_PISTOL);
                    player.GiveWeaponItem(WeaponItem.BAZOOKA);
                    player.GiveWeaponItem(WeaponItem.MOLOTOVS);
                    player.GiveWeaponItem(WeaponItem.STRENGTHBOOST);
                }

                BotHelper.Initialize();
            }
            catch (Exception e)
            {
                Game.ShowChatMessage("[Botextended script]: Error");
                Game.ShowChatMessage(e.Message);
                Game.ShowChatMessage(e.StackTrace);
                Game.ShowChatMessage(e.TargetSite.ToString());
            }
        }

        public void OnShutdown()
        {
            BotHelper.StoreStatistics();
        }
    }
}