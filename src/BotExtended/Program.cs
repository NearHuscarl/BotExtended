using System;
using SFDGameScriptInterface;
using System.Collections.Generic;
using BotExtended.Library;
using System.Linq;
using BotExtended.Factions;
using BotExtended.Powerups;
using BotExtended.Weapons;
using BotExtended.Bots;

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
            // invoke the static contructor to create the null instance IPlayer
            var bot = Bot.None;
        }

        public void AfterStartup()
        {
            // Initialize in AfterStartup instead of in OnStartup because we need to wait until the null IPlayer instance is removed from the world.
            // otherwise, IPlayer.IsRemoved is not updated yet after calling Remove() and Game.GetPlayers() still returns the null IPlayer
            Initialize();
        }

        private void Initialize()
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

            PowerupManager.Initialize();
            WeaponManager.Initialize();
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
            }
        }

        public void OnShutdown()
        {
            BotManager.OnShutdown();
        }
    }
}