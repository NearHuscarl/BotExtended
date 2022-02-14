using System;
using System.Collections.Generic;
using System.Linq;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Library
{
    public static class ScriptHelper
    {
        public static readonly Color Red = new Color(128, 32, 32);
        public static readonly Color Orange = new Color(255, 128, 24);

        public static readonly Color MESSAGE_COLOR = new Color(24, 238, 200);
        public static readonly Color ERROR_COLOR = new Color(244, 77, 77);
        public static readonly Color WARNING_COLOR = new Color(249, 191, 11);

        public static void PrintMessage(string message, Color? color = null)
        {
            Game.ShowChatMessage(message, color ?? MESSAGE_COLOR);
        }

        // TODO: remove once gurt fixes
        // https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&t=3995
        // https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3994
        public static string ToDisplayString(params object[] values)
        {
            var str = "";

            foreach (var v in values)
            {
                if (v == null)
                    str += " <null>";
                else if (v is float || v is double)
                    str += " " + ((float)v).ToString("0.00");
                else
                    str += " " + v;
            }

            return str;
        }
        public static void LogDebugF(string format, params object[] values)
        {
            if (!Game.IsEditorTest) return;
            Game.WriteToConsoleF(format, values);
        }
        public static void LogDebug(params object[] values)
        {
            if (!Game.IsEditorTest) return;
            Game.WriteToConsole(ToDisplayString(values));
        }
        public static void LogF(string format, params object[] values)
        {
            Game.WriteToConsoleF(format, values);
        }
        public static void Log(params object[] values)
        {
            Game.WriteToConsole(ToDisplayString(values));
        }

        public static void Timeout(Action callback, uint interval)
        {
            Events.UpdateCallback.Start(e => callback.Invoke(), interval, 1);
        }

        public static void RunIn(Action callback, int ms)
        {
            Events.UpdateCallback.Start(e => callback.Invoke(), 0, (ushort)(60 * ms / 1000));
        }

        public static bool IsElapsed(float timeStarted, float timeToElapse)
        {
            return Game.TotalElapsedGameTime - timeStarted >= timeToElapse;
        }

        public static bool SpawnerHasPlayer(IObject spawner, IPlayer[] players)
        {
            // Player position y: -20 || +9
            // => -21 -> +10
            // Player position x: unchange
            foreach (var player in players)
            {
                var playerPosition = player.GetWorldPosition();
                var spawnerPosition = spawner.GetWorldPosition();

                if (spawnerPosition.Y - 21 <= playerPosition.Y && playerPosition.Y <= spawnerPosition.Y + 10
                    && spawnerPosition.X == playerPosition.X)
                    return true;
            }

            return false;
        }

        public static void MakeInvincible(IPlayer player)
        {
            if (player != null)
            {
                var mod = player.GetModifiers();
                mod.FireDamageTakenModifier = 0;
                mod.ImpactDamageTakenModifier = 0;
                mod.MeleeDamageTakenModifier = 0;
                mod.ExplosionDamageTakenModifier = 0;
                mod.ProjectileDamageTakenModifier = 0;
                player.SetModifiers(mod);
            }
        }

        private static void NormalizeMinMaxAngle(ref float minAngle, ref float maxAngle, bool smallSector)
        {
            minAngle = MathExtension.NormalizeAngle(minAngle);
            maxAngle = MathExtension.NormalizeAngle(maxAngle);

            if (minAngle > maxAngle)
            {
                var swap = minAngle;
                minAngle = maxAngle;
                maxAngle = swap;
            }

            if (maxAngle - minAngle > MathHelper.PI && smallSector)
            {
                var oldMinAngle = minAngle;
                minAngle = maxAngle;
                maxAngle = oldMinAngle + MathHelper.TwoPI;
            }
        }

        public static bool IntersectCircle(Vector2 position, Vector2 center, float radius,
            float minAngle = 0, float maxAngle = 0, bool smallSector = true)
        {
            NormalizeMinMaxAngle(ref minAngle, ref maxAngle, smallSector);
            var fullCircle = minAngle == 0 && maxAngle == 0;
            var distanceToCenter = Vector2.Distance(position, center);

            if (distanceToCenter <= radius)
            {
                if (!fullCircle)
                {
                    var angle = MathExtension.NormalizeAngle(GetAngle(position - center));

                    if (angle >= minAngle && angle <= maxAngle
                            || angle + MathHelper.TwoPI >= minAngle && angle + MathHelper.TwoPI <= maxAngle)
                        return true;
                }
                else
                    return true;
            }

            return false;
        }

        public static bool IntersectCircle(Area area, Vector2 center, float radius)
        {
            return IntersectCircle(area, center, radius, 0, 0, false);
        }
        public static bool IntersectCircle(Area area, Vector2 center, float radius,
            float minAngle = 0, float maxAngle = 0, bool smallSector = true)
        {
            NormalizeMinMaxAngle(ref minAngle, ref maxAngle, smallSector);
            var fullCircle = minAngle == 0 && maxAngle == 0;
            var lines = new List<Vector2[]>()
            {
                new Vector2[] { area.BottomRight, area.BottomLeft },
                new Vector2[] { area.BottomLeft, area.TopLeft },
                new Vector2[] { area.TopLeft, area.TopRight },
                new Vector2[] { area.TopRight, area.BottomRight },
            };

            foreach (var line in lines)
            {
                var distanceToCenter = FindDistanceToSegment(center, line[0], line[1]);

                if (distanceToCenter <= radius)
                {
                    if (!fullCircle)
                    {
                        var corner = line[0];
                        var angle = MathExtension.NormalizeAngle(GetAngle(corner - center));

                        if (angle >= minAngle && angle <= maxAngle
                            || angle + MathHelper.TwoPI >= minAngle && angle + MathHelper.TwoPI <= maxAngle)
                            return true;
                    }
                    else
                        return true;
                }
            }

            return false;
        }

        // https://stackoverflow.com/a/1501725/9449426
        public static float FindDistanceToSegment(Vector2 point, Vector2 p1, Vector2 p2)
        {
            // Return minimum distance between line segment vw and point point
            var lengthSquare = (float)(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));  // i.e. |p2-p1|^2 -  avoid a sqrt
            if (lengthSquare == 0.0) return Vector2.Distance(point, p1);   // p1 == p2 case
            // Consider the line extending the segment, parameterized as p1 + t (p2 - p1).
            // We find projection of point point onto the line. 
            // It falls where t = [(point-p1) . (p2-p1)] / |p2-p1|^2
            // We clamp t from [0,1] to handle points outside the segment vw.
            var t = MathHelper.Clamp(Vector2.Dot(point - p1, p2 - p1) / lengthSquare, 0, 1);
            var projection = p1 + t * (p2 - p1);  // Projection falls on the segment
            return Vector2.Distance(point, projection);
        }

        public static Vector2 GetDirection(float radianAngle)
        {
            return new Vector2()
            {
                X = (float)Math.Cos(radianAngle),
                Y = (float)Math.Sin(radianAngle),
            };
        }

        // https://stackoverflow.com/a/6247163/9449426
        public static float GetAngle(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }

        public static float[] Flip(float[] angles, FlipDirection direction)
        {
            if (direction == FlipDirection.Horizontal)
            {
                angles[0] = MathExtension.FlipAngleY(angles[0]);
                angles[1] = MathExtension.FlipAngleY(angles[1]);
            }
            else
            {
                angles[0] = MathExtension.FlipAngleX(angles[0]);
                angles[1] = MathExtension.FlipAngleX(angles[1]);
            }

            return new float[]
            {
                Math.Min(angles[0], angles[1]),
                Math.Max(angles[0], angles[1]),
            };
        }

        public static bool SameTeam(IPlayer player1, IPlayer player2)
        {
            if (player1 == null || player2 == null) return false;
            return player1.GetTeam() != PlayerTeam.Independent && player1.GetTeam() == player2.GetTeam()
                || player1.GetTeam() == PlayerTeam.Independent && player1.UniqueID == player2.UniqueID;
        }

        public static bool IsIndestructible(IObject o) { return o.GetMaxHealth() == 1; }

        public static Dictionary<string, IUser> GetActiveUsersByAccountID()
        {
            var usersByAccountID = new Dictionary<string, IUser>();

            // NOTE: there can be multiple users with the same AccountID in the Map Editor. wtf moment
            // Anyway, get the first IUser match only
            foreach (var user in Game.GetActiveUsers())
            {
                if (!usersByAccountID.ContainsKey(user.AccountID))
                    usersByAccountID.Add(user.AccountID, user);
            }

            return usersByAccountID;
        }

        public static Area GrowFromCenter(Vector2 center, float width, float height = 0)
        {
            if (height == 0) height = width;
            var halfWidth = width / 2;
            var halfHeight = height / 2;

            return GrowFromCenter(center, halfWidth, halfHeight, halfWidth, halfHeight);
        }

        public static Area GrowFromCenter(Vector2 center, float toLeft, float toTop, float toRight, float toBottom)
        {
            return new Area(
                center.Y + toTop,
                center.X - toLeft,
                center.Y - toBottom,
                center.X + toRight);
        }

        public static System.Reflection.MethodBase GetMethodInfo(int skipFrames)
        {
            return new System.Diagnostics.StackFrame(skipFrames).GetMethod();
        }
        public static void Stopwatch(Func<string> action, int reportThreshold = 1)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();
            var name = action();
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds >= reportThreshold)
                LogDebugF("-Perf {2,6} {0}:{1}", stopwatch.ElapsedMilliseconds, GetMethodInfo(1).Name, name);
        }

        public static ProjectilePowerup GetPowerup(IProjectile projectile)
        {
            if (projectile.PowerupBounceActive)
                return ProjectilePowerup.Bouncing;

            if (projectile.PowerupFireActive)
                return ProjectilePowerup.Fire;

            return ProjectilePowerup.None;
        }

        public static bool IsMeAlone()
        {
            var users = Game.GetActiveUsers().Where(u => !u.IsBot);
            var i = 0;

            foreach (var u in users)
            {
                if (u.AccountName == "NearHuscarl") i++;
            }
            return users.Count() == i;
        }

        // Never use is keyword to check if IObject is IPlayer. it's extremely slow
        public static bool IsPlayer(IObject obj)
        {
            if (obj == null) return false;
            return obj.GetCollisionFilter().CategoryBits == CategoryBits.Player;
        }
        // A faster cast player (dont use as/is)
        public static IPlayer CastPlayer(IObject obj)
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

        public static bool IsInteractiveObject(IObject obj)
        {
            var cf = obj.GetCollisionFilter();
            return cf.CategoryBits == CategoryBits.DynamicG1
                || cf.CategoryBits == CategoryBits.DynamicG2
                || cf.CategoryBits == CategoryBits.Dynamic
                || cf.CategoryBits == CategoryBits.Player
                || cf.CategoryBits == CategoryBits.DynamicPlatform
                || cf.CategoryBits == CategoryBits.StaticGround;
        }

        public static void Unscrew(IObject o)
        {
            var hitbox = o.GetAABB();
            foreach (var j in Game.GetObjectsByArea<IObjectTargetObjectJoint>(hitbox))
            {
                var to = j.GetTargetObject();
                if (to == null) continue;
                if (to.UniqueID == o.UniqueID)
                {
                    o.SetLinearVelocity(Vector2.Zero);
                    j.SetTargetObject(null);
                    j.Remove();
                }
            }
            foreach (var j in Game.GetObjectsByArea<IObjectWeldJoint>(hitbox))
            {
                j.RemoveTargetObject(o);
            }
            foreach (var j in Game.GetObjectsByArea<IObjectRevoluteJoint>(hitbox))
            {
                var to = j.GetTargetObjectA();
                if (to == null) continue;
                if (to.UniqueID == o.UniqueID)
                    j.SetTargetObjectA(null);
            }
        }

        public static System.Threading.Tasks.Task<bool> ExecuteSingleCommand(
            IPlayer player,
            PlayerCommandType commandType,
            uint delay = 10,
            PlayerCommandFaceDirection facingDirection = PlayerCommandFaceDirection.None
            )
        {
            var promise = new System.Threading.Tasks.TaskCompletionSource<bool>();

            if (player == null)
                promise.TrySetResult(false);

            player.SetInputEnabled(false);
            // some commands like Stagger not working without this line
            player.AddCommand(new PlayerCommand(PlayerCommandType.FaceAt, facingDirection));

            Timeout(() =>
            {
                player.AddCommand(new PlayerCommand(commandType, facingDirection));
                if (delay == 0) return;
                Timeout(() =>
                {
                    player.ClearCommandQueue();
                    player.SetInputEnabled(true);
                    promise.TrySetResult(true);
                }, delay);
            }, 2);

            return promise.Task;
        }

        public static Vector2 GetFarAwayPosition()
        {
            var randX = RandomHelper.Between(0, 20);
            var randy = RandomHelper.Between(0, 20);
            return Game.GetCameraMaxArea().TopLeft + new Vector2(10 + randX, 10 + randy);
        }

        // TODO: remove this if gurt added IObject.DealDamage()
        public static void DealDamage(IObject o, float damage)
        {
            var p = CastPlayer(o);
            if (p != null) p.DealDamage(damage);
            else o.SetHealth(o.GetHealth() - damage);
        }
    }
}
