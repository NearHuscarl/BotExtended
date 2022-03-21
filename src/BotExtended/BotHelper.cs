using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using BotExtended.Powerups;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;

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

            var rndBotFaction = RandomHelper.GetItem(filteredBotFactions);

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

        // TODO: need better type detection
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
                case BotAI.MeleeExpert:
                    return BotType.ZombieFighter;

                case BotAI.Ninja:
                    return BotType.ZombieChild;

                case BotAI.Hulk:
                    return BotType.ZombieBruiser;
            }

            var modifiers = botInfo.Modifiers;

            if (modifiers.SprintSpeedModifier >= Speed.Fast)
                return BotType.ZombieChild;

            if (modifiers.SizeModifier >= Size.Big && modifiers.SizeModifier < Size.Chonky)
                return BotType.ZombieBruiser;
            if (modifiers.SizeModifier == Size.Chonky)
                return BotType.ZombieFat;

            return BotType.Zombie;
        }

        public static void Equip(IPlayer player, WeaponSet weaponSet)
        {
            if (player == null || weaponSet.IsEmpty) return;

            PowerupManager.SetPowerup(player, weaponSet.Melee, weaponSet.MeleePowerup);
            PowerupManager.SetPowerup(player, WeaponItem.NONE, weaponSet.MeleeHandPowerup);
            PowerupManager.SetPowerup(player, weaponSet.Primary, weaponSet.PrimaryPowerup);
            PowerupManager.SetPowerup(player, weaponSet.Secondary, weaponSet.SecondaryPowerup);
            // TODO: thrown weapon
            player.GiveWeaponItem(weaponSet.Throwable);
            player.GiveWeaponItem(weaponSet.Powerup);

            if (weaponSet.UseLazer) player.GiveWeaponItem(WeaponItem.LAZER);
        }

        public static WeaponSet GetWeaponSet(IPlayer player)
        {
            var bot = BotManager.GetBot(player);
            var playerWpn = PowerupManager.GetOrCreatePlayerWeapon(player);

            return new WeaponSet()
            {
                Melee = bot.CurrentMeleeWeapon,
                MeleePowerup = playerWpn != null ? playerWpn.Melee.Powerup : MeleeWeaponPowerup.None,
                MeleeHandPowerup = playerWpn.MeleeHand.Powerup,
                Primary = player.CurrentPrimaryWeapon.WeaponItem,
                PrimaryPowerup = playerWpn != null ? playerWpn.Primary.Powerup : RangedWeaponPowerup.None,
                Secondary = player.CurrentSecondaryWeapon.WeaponItem,
                SecondaryPowerup = playerWpn != null ? playerWpn.Secondary.Powerup : RangedWeaponPowerup.None,
                // TODO: add thrown powerup weapon here
                Throwable = player.CurrentThrownItem.WeaponItem,
                Powerup = player.CurrentPowerupItem.WeaponItem,
                // TODO: wait for gurt to add this: https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=4000
                //UseLazer = ...
            };
        }

        public static IProfile ToZombieProfile(IProfile profile)
        {
            switch (profile.Skin.Name)
            {
                case "Normal":
                case "Tattoos":
                case "Warpaint":
                    profile.Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", "");
                    break;

                case "Normal_fem":
                case "Tattoos_fem":
                case "Warpaint_fem":
                    profile.Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", "");
                    break;

                case "BearSkin":
                    profile.Skin = new IProfileClothingItem("FrankenbearSkin", "ClothingDarkGray", "ClothingLightBlue", "");
                    break;
            }

            return profile;
        }

        public static void SetPlayer(IPlayer player, BotType botType)
        {
            if (botType == BotType.None)
                return;
            BotManager.SpawnBot(botType, BotFaction.None, player, player.GetTeam());
        }

        public static void SetWeapon(IPlayer player, string weaponItemStr, string powerupStr)
        {
            var weaponItem = SharpHelper.StringToEnum<WeaponItem>(weaponItemStr);
            var type = Mapper.GetWeaponItemType(weaponItem);

            switch (type)
            {
                // TODO: thrown powerup
                case WeaponItemType.NONE: // bare hand
                case WeaponItemType.Melee:
                {
                    var powerup = SharpHelper.StringToEnum<MeleeWeaponPowerup>(powerupStr);
                    if (powerup == MeleeWeaponPowerup.None)
                    {
                        player.GiveWeaponItem(weaponItem);
                        break;
                    }
                    PowerupManager.SetPowerup(player, weaponItem, powerup);
                    break;
                }
                case WeaponItemType.Rifle:
                case WeaponItemType.Handgun:
                {
                    var powerup = SharpHelper.StringToEnum<RangedWeaponPowerup>(powerupStr);
                    PowerupManager.SetPowerup(player, weaponItem, powerup);
                    break;
                }
                default:
                    player.GiveWeaponItem(weaponItem);
                    break;
            }
        }
    }
}
