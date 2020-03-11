using System;
using SFDGameScriptInterface;
using System.Collections.Generic;
using BotExtended.Library;
using System.Linq;

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
                BotManager.Initialize();

                if (Game.IsEditorTest)
                {
                    var player = Game.GetPlayers()[0];
                    var modifiers = player.GetModifiers();

                    modifiers.MaxHealth = 5000;
                    modifiers.CurrentHealth = 5000;
                    modifiers.EnergyConsumptionModifier = .1f;
                    //modifiers.EnergyRechargeModifier = 1.5f;
                    modifiers.RunSpeedModifier = 1.25f;
                    modifiers.SprintSpeedModifier = 1.25f;
                    modifiers.MeleeStunImmunity = 1;
                    //modifiers.InfiniteAmmo = 1;

                    //player.SetTeam(PlayerTeam.Team1);
                    player.SetHitEffect(PlayerHitEffect.Metal);
                    player.SetModifiers(modifiers);
                    player.GiveWeaponItem(WeaponItem.WHIP);
                    player.GiveWeaponItem(WeaponItem.FLAREGUN);
                    player.GiveWeaponItem(WeaponItem.ASSAULT);
                    player.GiveWeaponItem(WeaponItem.GRENADES);
                    player.GiveWeaponItem(WeaponItem.STRENGTHBOOST);

                    //Game.SetAllowedCameraModes(CameraMode.Static);
                    //Game.SetCameraArea(new Area(-170, -150, -210, 50));
                    //// image size: 180 x 180 - top: 24 left: 37M 36F
                    //Command.SetPlayer(new List<string>() { "near", "MirrorMan" });
                    //Command.SetPlayer(new List<string>() { "player 2", "AssassinRange" });
                    //Command.SetPlayer(new List<string>() { "player 3", "AssassinRange" });
                    //Command.SetPlayer(new List<string>() { "player 4", "AssassinRange" });
                    //Command.SetPlayer(new List<string>() { "player 5", "AssassinRange" });
                    //Command.SetPlayer(new List<string>() { "player 6", "AssassinRange" });
                    //Command.SetPlayer(new List<string>() { "player 7", "AssassinRange" });
                    //Command.SetPlayer(new List<string>() { "player 8", "AssassinRange" });

                    //foreach (var p in Game.GetPlayers())
                    //{
                    //    p.SetInputEnabled(false);
                    //    p.AddCommand(new PlayerCommand(PlayerCommandType.DrawHandgun));
                    //}
                }
            }
            catch (Exception e)
            {
                Game.ShowChatMessage(string.Format("[BotExtended script]: {0}", e.Message), ScriptHelper.ERROR_COLOR);
                Game.WriteToConsole("[BotExtended script]: Error");
                Game.WriteToConsole(e.Message);
                Game.WriteToConsole(e.Source);
                Game.WriteToConsole(e.StackTrace);
                Game.WriteToConsole(e.TargetSite.ToString());
            }
        }

        public void OnShutdown()
        {
            BotManager.OnShutdown();
        }
    }
}