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
            Game.ShowChatMessage(message, color ?? ScriptColors.MESSAGE_COLOR);
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
            var spawner = RandomHelper.GetItem(_spawners.Where(s => !s.HasSpawned).ToList());

            var player = Game.CreatePlayer(spawner.Position);
            player.SetBotBehaviorSet(botBehaviorSet);
            player.SetBotBehaviorActive(true);
            spawner.HasSpawned = true;

            return player;
        }

        public static void Timeout(Action callback, uint interval)
        {
            Events.UpdateCallback.Start(e => callback.Invoke(), interval, 1);
        }

        public static bool IsElapsed(float timeStarted, float timeToElapse)
        {
            return Game.TotalElapsedGameTime - timeStarted >= timeToElapse;
        }
        public static Func<float, bool> WithIsElapsed()
        {
            var timeStarted = 0f;
            return (interval) =>
            {
                if (IsElapsed(timeStarted, interval))
                {
                    timeStarted = Game.TotalElapsedGameTime;
                    return true;
                }
                return false;
            };
        }
        public static Func<float, float, bool> WithIsElapsed2()
        {
            var timeStarted = 0f;
            var interval = 0f;
            return (minTime, maxTime) =>
            {
                if (IsElapsed(timeStarted, interval))
                {
                    timeStarted = Game.TotalElapsedGameTime;
                    interval = RandomHelper.Between(minTime, maxTime);
                    return true;
                }
                return false;
            };
        }

        public static bool TryParseEnum<T>(string str, out T result) where T : struct, IConvertible
        {
            result = default(T);

            if (!typeof(T).IsEnum)
            {
                return false;
            }

            int index = -1;
            if (int.TryParse(str, out index))
            {
                if (Enum.IsDefined(typeof(T), index))
                {
                    // https://stackoverflow.com/questions/10387095/cast-int-to-generic-enum-in-c-sharp
                    result = (T)(object)index;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!Enum.TryParse(str, ignoreCase: true, result: out result))
                {
                    return false;
                }
            }

            return true;
        }
        public static T StringToEnum<T>(string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }
        // a bit faster than ToString(). https://stackoverflow.com/a/17034624/9449426
        public static string EnumToString<T>(T enumVal)
        {
            return Enum.GetName(typeof(T), enumVal);
        }
        public static T[] EnumToArray<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
