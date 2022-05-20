using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public static class ScriptHelper
    {
        public static void PrintMessage(string message, Color? color = null)
        {
            Game.ShowChatMessage(message, color ?? BeColors.MESSAGE_COLOR);
        }

        public class PlayerSpawner
        {
            public Vector2 Position { get; set; }
            public bool HasSpawned { get; set; }
        }
        public static List<PlayerSpawner> GetPlayerSpawners()
        {
            var spawners = Game.GetObjectsByName("SpawnPlayer");
            var emptySpawners = new List<PlayerSpawner>();
            var players = Game.GetPlayers().Where(p => !p.IsDead).ToList();

            foreach (var spawner in spawners)
            {
                var hasPlayer = players.Any(x => x.GetAABB().Intersects(spawner.GetAABB()));

                emptySpawners.Add(new PlayerSpawner
                {
                    Position = spawner.GetWorldPosition(),
                    HasSpawned = hasPlayer,
                });
            }

            return emptySpawners;
        }

        private static List<PlayerSpawner> _spawners = GetPlayerSpawners();
        public static IPlayer SpawnBot(BotBehaviorSet botBehaviorSet)
        {
            if (_spawners.Count == 0) return null;
            var spawner = RandomHelper.GetItem(_spawners);

            var player = Game.CreatePlayer(spawner.Position);
            player.SetBotBehaviorSet(botBehaviorSet);
            player.SetBotBehaviorActive(true);

            return player;
        }

        public static T[] EnumToArray<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }
        public static List<T> EnumToList<T>()
        {
            return EnumToArray<T>().ToList();
        }
    }
}
