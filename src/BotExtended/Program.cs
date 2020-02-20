using System;
using SFDGameScriptInterface;
using System.Collections.Generic;
using BotExtended.Library;

namespace BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        /// <summary>
        /// Placeholder constructor that's not to be included in the ScriptWindow!
        /// </summary>
        public GameScript() : base(null) { }

        public void OnStartup()
        {
            // How to create a bot
            // 1. Define bot type in BotType.cs
            // 2. Define bot profile in BotProfiles.cs
            // 3. Define bot weapon in BotWeapons.cs
            // 4. Define bot behavior in BotBehaviors.cs (optional)
            // 5. Define bot info in BotInfos.cs
            // 6. Define bot class in Bots/ and add it to BotFactory.cs for additional behaviors (optional)
            // 7. Define bot faction and sub-faction in BotFactionSets.cs

            try
            {
                //System.Diagnostics.Debugger.Break();
                BotHelper.Initialize();

                if (Game.IsEditorTest)
                {
                    var player = Game.GetPlayers()[0];
                    var modifiers = player.GetModifiers();

                    modifiers.MaxHealth = 5000;
                    modifiers.CurrentHealth = 5000;
                    modifiers.EnergyRechargeModifier = 1.5f;
                    modifiers.RunSpeedModifier = 1.25f;
                    modifiers.SprintSpeedModifier = 1.25f;
                    //modifiers.InfiniteAmmo = 1;
                    //modifiers.MeleeStunImmunity = 1;

                    player.SetModifiers(modifiers);
                    player.GiveWeaponItem(WeaponItem.WHIP);
                    player.GiveWeaponItem(WeaponItem.FLAREGUN);
                    player.GiveWeaponItem(WeaponItem.M60);
                    player.GiveWeaponItem(WeaponItem.MOLOTOVS);
                    player.GiveWeaponItem(WeaponItem.STRENGTHBOOST);

                    //Game.SetCurrentCameraMode(CameraMode.Dynamic);

                    //// image size: 82 x 100
                    Command.SetPlayer(new List<string>() { "near", "Mecha" });
                    //Command.SetPlayer(new List<string>() { "player 2", "Bandido" });
                    //Command.SetPlayer(new List<string>() { "player 3", "Bandido" });
                    //Command.SetPlayer(new List<string>() { "player 4", "Bandido" });
                    //Command.SetPlayer(new List<string>() { "player 5", "Bandido" });
                    //Command.SetPlayer(new List<string>() { "player 6", "Bandido" });
                    //Command.SetPlayer(new List<string>() { "player 7", "Bandido" });
                    //Command.SetPlayer(new List<string>() { "player 8", "Bandido" });
                }
            }
            catch (Exception e)
            {
                Game.ShowPopupMessage(string.Format("[BotExtended script]: {0}", e.Message), ScriptHelper.ERROR_COLOR);
                Game.WriteToConsole("[BotExtended script]: Error");
                Game.WriteToConsole(e.Message);
                Game.WriteToConsole(e.Source);
                Game.WriteToConsole(e.StackTrace);
                Game.WriteToConsole(e.TargetSite.ToString());
            }
        }

        public void OnShutdown()
        {
            BotHelper.OnShutdown();
        }
    }
}