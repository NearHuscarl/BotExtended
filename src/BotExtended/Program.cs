using System;
using SFDGameScriptInterface;
using System.Collections.Generic;
using BotExtended.Library;
using System.Linq;
using BotExtended.Factions;
using BotExtended.Projectiles;

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
            // 7. Define bot faction name in BotFaction.cs (optional)
            // 8. Define bot faction and sub-faction in BotFactionSets.cs

            //System.Diagnostics.Debugger.Break();

            BotManager.Initialize();

            if (Game.IsEditorTest)
            {
                var player = Game.GetPlayers()[0];
                var modifiers = player.GetModifiers();

                modifiers.MaxHealth = 300;
                modifiers.CurrentHealth = 300;
                modifiers.EnergyConsumptionModifier = .1f;
                modifiers.RunSpeedModifier = 1.25f;
                modifiers.SprintSpeedModifier = 1.25f;
                //modifiers.MeleeStunImmunity = 1;
                modifiers.InfiniteAmmo = 1;

                player.SetTeam(PlayerTeam.Team1);
                player.SetHitEffect(PlayerHitEffect.Metal);
                player.SetModifiers(modifiers);
                //player.GiveWeaponItem(WeaponItem.KNIFE);
                //player.GiveWeaponItem(WeaponItem.FLAREGUN);
                //player.GiveWeaponItem(WeaponItem.ASSAULT);
                player.GiveWeaponItem(WeaponItem.GRENADES);
                player.GiveWeaponItem(WeaponItem.STRENGTHBOOST);

                var n = 0;
                foreach (var p in Game.GetPlayers())
                {
                    if (p.Name != "Near")
                    {
                        //p.SetInputEnabled(false);
                        //p.SetUser(null);
                    }
                    if (p.Name.StartsWith("Mecha"))
                    {
                        p.SetHealth(1);
                    }
                    if (p.Name.StartsWith("Cowboy") || p.Name.StartsWith("Bandido"))
                        p.Remove();
                    if (p.Name.StartsWith("Hunter"))
                    {
                        if (n > 2) p.Remove();
                        n++;
                    }
                }

                //Game.SetAllowedCameraModes(CameraMode.Static);
                //Game.SetCameraArea(new Area(10, -130, -10, 130));
                //// image size: 140 x 140 - top: 20 left: 30M 30F
                //Command.SetPlayer(new List<string>() { "near", "MirrorMan" });
                //Command.SetPlayer(new List<string>() { "player 2", "Agent" });
                //Command.SetPlayer(new List<string>() { "player 3", "Agent" });
                //Command.SetPlayer(new List<string>() { "player 4", "Agent" });
                //Command.SetPlayer(new List<string>() { "player 5", "Agent" });
                //Command.SetPlayer(new List<string>() { "player 6", "Agent" });
                //Command.SetPlayer(new List<string>() { "player 7", "Agent" });
                //Command.SetPlayer(new List<string>() { "player 8", "Agent" });
            }
        }

        public void OnShutdown()
        {
            BotManager.OnShutdown();
        }
    }
}