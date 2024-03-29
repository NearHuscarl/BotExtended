﻿using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RandomBots.Helpers;

namespace RandomBots
{
    public static class ScriptHelper
    {
        public static T[] EnumToArray<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static List<IObject> GetPlayerSpawners(bool emptyOnly = false)
        {
            if (!emptyOnly)
                return Game.GetObjectsByName("SpawnPlayer").ToList();

            var players = Game.GetPlayers();
            return Game.GetObjectsByName("SpawnPlayer").Where(o => !players.Any(x => x.GetAABB().Intersects(o.GetAABB()))).ToList();
        }

        public static List<PlayerTeam> GetAliveTeams()
        {
            var teamCount = new Dictionary<PlayerTeam, int>
            {
                { PlayerTeam.Team1, 0 },
                { PlayerTeam.Team2, 0 },
                { PlayerTeam.Team3, 0 },
                { PlayerTeam.Team4, 0 },
                { PlayerTeam.Independent, 0 },
            };
            foreach (var p in Game.GetPlayers())
            {
                if (!p.IsDead)
                {
                    teamCount[p.GetTeam()]++;
                }
            }

            return teamCount.Where(t => t.Value > 0).Select(t => t.Key).ToList();
        }

        public static int GetTeamNumber(PlayerTeam team)
        {
            switch (team)
            {
                case PlayerTeam.Team1: return 1;
                case PlayerTeam.Team2: return 2;
                case PlayerTeam.Team3: return 3;
                case PlayerTeam.Team4: return 4;
                default: return 0;
            }
        }

        // layer text is in Equipment.cs#GetText()
        public static string[] GetProfileItems(int layer, Gender gender)
        {
            switch (layer)
            {
                case 0:
                    return Game.GetClothingItemNamesSkin(gender);
                case 1:
                    return Game.GetClothingItemNamesChestUnder(gender);
                case 2:
                    return Game.GetClothingItemNamesLegs(gender);
                case 3:
                    return Game.GetClothingItemNamesWaist(gender);
                case 4:
                    return Game.GetClothingItemNamesFeet(gender);
                case 5:
                    return Game.GetClothingItemNamesChestOver(gender);
                case 6:
                    return Game.GetClothingItemNamesAccessory(gender);
                case 7:
                    return Game.GetClothingItemNamesHands(gender);
                case 8:
                    return Game.GetClothingItemNamesHead(gender);
                default:
                    throw new ArgumentException("Invalid layer index: " + layer);
            }
        }
        public static void SetProfileItem(IProfile profile, int layer, IProfileClothingItem item)
        {
            switch (layer)
            {
                case 0:
                    profile.Skin = item; return;
                case 1:
                    profile.ChestUnder = item; return;
                case 2:
                    profile.Legs = item; return;
                case 3:
                    profile.Waist = item; return;
                case 4:
                    profile.Feet = item; return;
                case 5:
                    profile.ChestOver = item; return;
                case 6:
                    profile.Accesory = item; return;
                case 7:
                    profile.Hands = item; return;
                case 8:
                    profile.Head = item; return;
                default:
                    throw new ArgumentException("Invalid layer index: " + layer);
            }
        }
    }
}
