using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using BotExtended.Weapons;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended
{
    static class BotHelper
    {
        private static IScriptStorage _storage;
        public static IScriptStorage Storage
        {
            get
            {
                if (_storage == null)
                    _storage = Game.LocalStorage; return _storage;
            }
        }

        public static string StorageKey(string key)
        {
            return Constants.STORAGE_KEY_PREFIX + key;
        }
        public static string StorageKey(BotFaction botFaction, int factionIndex)
        {
            return Constants.STORAGE_KEY_PREFIX + SharpHelper.EnumToString(botFaction).ToUpperInvariant() + "_" + factionIndex;
        }

        public static IEnumerable<BotFaction> GetAvailableBotFactions()
        {
            return SharpHelper.EnumToList<BotFaction>().Where((f) => f != BotFaction.None);
        }

        public static BotFaction RandomFaction(List<BotFaction> botFactions, int botCount)
        {
            List<BotFaction> filteredBotFactions = null;
            if (botCount < 3) // Too few for a faction, spawn boss instead
            {
                filteredBotFactions = botFactions
                    .Select(g => g)
                    .Where(g => (int)g >= Constants.BOSS_FACTION_START_INDEX).ToList();
                if (!filteredBotFactions.Any())
                    filteredBotFactions = botFactions;
            }
            else
                filteredBotFactions = botFactions;

            var rndBotFaction = RandomHelper.GetItem(filteredBotFactions, "BOT_FACTION");

            return rndBotFaction;
        }

        public static List<PlayerSpawner> GetEmptyPlayerSpawners()
        {
            var spawners = Game.GetObjectsByName("SpawnPlayer");
            var emptySpawners = new List<PlayerSpawner>();
            var players = Game.GetPlayers();

            foreach (var spawner in spawners)
            {
                if (!ScriptHelper.SpawnerHasPlayer(spawner, players))
                {
                    emptySpawners.Add(new PlayerSpawner
                    {
                        Position = spawner.GetWorldPosition(),
                        HasSpawned = false,
                    });
                }
            }

            return emptySpawners;
        }

        public static BotType GetZombieType(BotType botType)
        {
            if (botType == BotType.None)
                return BotType.Zombie;

            var botInfo = GetInfo(botType);
            var aiType = botInfo.AIType;

            switch (aiType)
            {
                case BotAI.Hacker:
                case BotAI.Expert:
                case BotAI.Hard:
                case BotAI.MeleeHard:
                case BotAI.MeleeExpert:
                    return BotType.ZombieFighter;

                case BotAI.Ninja:
                    return BotType.ZombieChild;

                case BotAI.Hulk:
                    return BotType.ZombieBruiser;
            }

            var modifiers = botInfo.Modifiers;

            if (modifiers.SprintSpeedModifier >= 1.1f)
                return BotType.ZombieChild;

            if (modifiers.SizeModifier == 1.25f)
                return BotType.ZombieFat;

            return BotType.Zombie;
        }

        public static void Equip(IPlayer player, WeaponSet weaponSet)
        {
            if (player == null || weaponSet.IsEmpty) return;

            player.GiveWeaponItem(weaponSet.Melee);
            player.GiveWeaponItem(weaponSet.Primary);
            player.GiveWeaponItem(weaponSet.Secondary);
            player.GiveWeaponItem(weaponSet.Throwable);
            player.GiveWeaponItem(weaponSet.Powerup);

            WeaponManager.SetPrimaryPowerup(player, weaponSet.Primary, weaponSet.PrimaryPowerup);
            WeaponManager.SetSecondaryPowerup(player, weaponSet.Secondary, weaponSet.SecondaryPowerup);

            if (weaponSet.UseLazer) player.GiveWeaponItem(WeaponItem.LAZER);
        }

        public static IProfile ToZombieProfile(IProfile profile)
        {
            switch (profile.Skin.Name)
            {
                case "Normal":
                case "Tattoos":
                    profile.Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", "");
                    break;

                case "Normal_fem":
                case "Tattoos_fem":
                    profile.Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", "");
                    break;

                case "BearSkin":
                    profile.Skin = new IProfileClothingItem("FrankenbearSkin", "ClothingDarkGray", "ClothingLightBlue", "");
                    break;
            }

            return profile;
        }
    }
}
