using System;
using System.Collections.Generic;
using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

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

        public static string GetDefaultPlaceholder(object[] messages)
        {
            var placeholder = "";
            var count = messages.Length;
            for (var i = 0; i < count; i++)
            {
                var isFloatOrDouble = messages[i] is float || messages[i] is double;
                placeholder += "{" + i + (isFloatOrDouble ? ":0.00" : "") + "}";
                if (i != count - 1) placeholder += " ";
            }
            return placeholder;
        }
        public static void LogDebugF(string placeholder, params object[] values)
        {
            if (Game.IsEditorTest)
            {
                if (string.IsNullOrEmpty(placeholder))
                {
                    placeholder = GetDefaultPlaceholder(values);
                }
                Game.WriteToConsole(string.Format(placeholder, values));
            }
        }

        public static void LogDebug(params object[] values)
        {
            if (Game.IsEditorTest)
            {
                Game.WriteToConsole(string.Format(GetDefaultPlaceholder(values), values));
            }
        }

        public static void Timeout(Action callback, uint interval)
        {
            Events.UpdateCallback.Start((float e) => callback.Invoke(), interval, 1);
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

        public static bool IsDifferentTeam(IPlayer player1, IPlayer player2)
        {
            return player1.GetTeam() != player2.GetTeam() || player1.GetTeam() == PlayerTeam.Independent;
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

        public static bool SameTeam(IPlayer player1, IPlayer player2)
        {
            if (player1 == null || player2 == null) return false;
            return player1.GetTeam() == player2.GetTeam()
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
    }
}
