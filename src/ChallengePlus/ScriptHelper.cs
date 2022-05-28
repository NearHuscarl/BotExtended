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

        public static Vector2 GetDirection(Vector2 velocity)
        {
            velocity.Normalize();
            return velocity;
        }

        public static Vector2 GetDirection(float radianAngle)
        {
            return new Vector2()
            {
                X = (float)Math.Cos(radianAngle),
                Y = (float)Math.Sin(radianAngle),
            };
        }

        private static List<PlayerSpawner> _spawners = GetPlayerSpawners();
        public static IPlayer SpawnBot(PredefinedAIType ai)
        {
            if (_spawners.Count == 0) return null;
            var spawner = RandomHelper.GetItem(_spawners.Where(s => !s.HasSpawned).ToList());

            var player = Game.CreatePlayer(spawner.Position);
            player.SetBotBehavior(new BotBehavior
            {
                PredefinedAI = ai,
                Active = true,
            });
            spawner.HasSpawned = true;

            return player;
        }

        public static Events.UpdateCallback RunIn(Action callback, int ms, Action onTimeout = null, uint interval = 0)
        {
            var timeStarted = Game.TotalElapsedGameTime;
            var cb = (Events.UpdateCallback)null;

            cb = Events.UpdateCallback.Start(e =>
            {
                if (IsElapsed(timeStarted, ms))
                {
                    if (onTimeout != null) onTimeout();
                    cb.Stop();
                }
                else
                    callback.Invoke();
            }, interval);

            return cb;
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

        // Never use is keyword to check if IObject is IPlayer. it's slow
        public static bool IsPlayer(IObject obj)
        {
            if (obj == null) return false;
            return obj.GetCollisionFilter().CategoryBits == CategoryBits.Player;
        }
        // A faster cast player (dont use as/is)
        public static IPlayer AsPlayer(IObject obj)
        {
            if (obj == null) return null;
            return Game.GetPlayer(obj.UniqueID);
        }

        public static bool IsDynamicObject(IObject obj)
        {
            var cf = obj.GetCollisionFilter();
            return cf.CategoryBits == CategoryBits.DynamicG1
                || cf.CategoryBits == CategoryBits.DynamicG2
                || cf.CategoryBits == CategoryBits.Dynamic;
        }

        public static bool IsStaticGround(IObject o) { return o.GetCollisionFilter().CategoryBits == CategoryBits.StaticGround; }
        public static bool IsHardStaticGround(IObject o) { return IsStaticGround(o) && !IsPlatform(o); }
        // Open tiles.sfdx and search for 'plat' to see all other properties
        public static bool IsPlatform(IObject o)
        {
            var cf = o.GetCollisionFilter();
            return cf.CategoryBits == CategoryBits.StaticGround && !cf.AbsorbProjectile && !cf.BlockExplosions;
        }
        public static bool IsDynamicG1(IObject o) { return o.GetCollisionFilter().CategoryBits == CategoryBits.DynamicG1; }
        public static bool IsDynamicG2(IObject o) { return o.GetCollisionFilter().CategoryBits == CategoryBits.DynamicG2; }

        public static bool IsInteractiveObject(IObject obj)
        {
            var cf = obj.GetCollisionFilter();
            return cf.CategoryBits == CategoryBits.DynamicG1
                || cf.CategoryBits == CategoryBits.DynamicG2
                || cf.CategoryBits == CategoryBits.Dynamic
                || cf.CategoryBits == CategoryBits.Player;
        }

        public static bool IsActiveObject(IObject obj)
        {
            var cf = obj.GetCollisionFilter();
            return cf.CategoryBits == CategoryBits.DynamicG1
                || cf.CategoryBits == CategoryBits.DynamicG2
                || cf.CategoryBits == CategoryBits.Dynamic
                || cf.CategoryBits == CategoryBits.Player
                || cf.CategoryBits == CategoryBits.DynamicPlatform
                || cf.CategoryBits == CategoryBits.StaticGround;
        }

        public static Vector2 GetFarAwayPosition()
        {
            var randX = RandomHelper.Between(0, 200);
            var randy = RandomHelper.Between(0, 200);
            return Game.GetCameraMaxArea().TopLeft + new Vector2(-10 - randX, 10 + randy);
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
