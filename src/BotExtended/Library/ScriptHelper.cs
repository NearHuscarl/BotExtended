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
            var timeStarted = Game.TotalElapsedGameTime;
            var cb = (Events.UpdateCallback)null;

            cb = Events.UpdateCallback.Start(e =>
            {
                callback.Invoke();
                if (IsElapsed(timeStarted, ms)) cb.Stop();
            });
        }

        public static void RunUntil(Action callback, Func<bool> stopCondition, Action cleanup = null)
        {
            var cb = (Events.UpdateCallback)null;
            cb = Events.UpdateCallback.Start(e =>
            {
                callback.Invoke();
                if (stopCondition())
                {
                    if (cleanup != null) cleanup();
                    cb.Stop();
                }
            });
        }

        public static void RunIf(Action callback, Func<bool> If, uint interval = 0, ushort count = 10000)
        {
            var cb = (Events.UpdateCallback)null;
            cb = Events.UpdateCallback.Start(e =>
            {
                if (If())
                {
                    callback.Invoke();
                    cb.Stop();
                }
            }, interval, count);
        }

        public static bool IsElapsed(float timeStarted, float timeToElapse)
        {
            return Game.TotalElapsedGameTime - timeStarted >= timeToElapse;
        }

        public static Func<bool> WithIsElapsed(float minTime, float maxTime = 0)
        {
            var interval = maxTime == 0 ? minTime : RandomHelper.Between(minTime, maxTime);
            var timeStarted = 0f;

            return () =>
            {
                if (IsElapsed(timeStarted, interval))
                {
                    timeStarted = Game.TotalElapsedGameTime;
                    interval = maxTime == 0 ? minTime : RandomHelper.Between(minTime, maxTime);
                    return true;
                }
                return false;
            };
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

        public static bool SameTeam(IPlayer player, PlayerTeam team)
        {
            if (team == PlayerTeam.Independent || player.GetTeam() == PlayerTeam.Independent) return false;
            return player.GetTeam() == team;
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

        public static Area Grow(Area area, float width = 0, float height = 0)
        {
            return GrowFromCenter(area.Center, area.Width + width, area.Height + height);
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
        public static Area Area(Vector2 min, Vector2 max)
        {
            var area = new Area(min, max); area.Normalize(); return area;
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

        public static IObject GetGroundObject(IObject aboveObject)
        {
            var boundingBox = aboveObject.GetAABB();
            var start = new Vector2(boundingBox.Center.X, boundingBox.Bottom);
            var end = start + new Vector2(0, -1);
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                FilterOnMaskBits = true,
                MaskBits = CategoryBits.StaticGround,
                ClosestHitOnly = true,
                IncludeOverlap = true,
            }).Where(r => r.HitObject != null);

            if (results.Any()) return results.First().HitObject;
            return null;
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

        public static IObject[] SplitTileObject(IObject o, Vector2 position)
        {
            var xTiles = o.GetSizeFactor().X;
            var yTiles = o.GetSizeFactor().Y;

            if (xTiles == 1) return new IObject[] { }; // not a tile object

            var tileSize = 8;
            var leftPos = o.GetAABB().Left;
            var effectArea = GrowFromCenter(position, 8, 2);
            var oLeft = (IObject)null;
            var oRight = (IObject)null;

            Unscrew(o);

            for (var i = 0; i < 4; i++)
                Game.PlayEffect(EffectName.BulletHitDefault, RandomHelper.WithinArea(effectArea));

            for (var i = 0; i < xTiles; i++)
            {
                if (leftPos + tileSize * i >= position.X)
                {
                    oLeft = Game.CreateObject(o.Name, o.GetWorldPosition());
                    oRight = Game.CreateObject(o.Name, o.GetWorldPosition() + Vector2.UnitX * tileSize * i);

                    oLeft.SetAngle(o.GetAngle());
                    oLeft.SetLinearVelocity(Vector2.UnitY * -20);
                    oLeft.SetSizeFactor(new Point(i - 1, yTiles));
                    oLeft.SetBodyType(BodyType.Dynamic);
                    oRight.SetAngle(o.GetAngle());
                    oRight.SetLinearVelocity(Vector2.UnitY * -20);
                    oRight.SetSizeFactor(new Point(xTiles - i, yTiles));
                    oRight.SetBodyType(BodyType.Dynamic);
                    break;
                }
            }

            var results = new IObject[] { oLeft, oRight }.Where(x => x != null).ToArray();
            if (results.Length == 2)
                o.Remove();
            return results;
        }

        public struct RopeResult
        {
            public IObjectDistanceJoint DistanceJoint;
            public IObject DistanceJointObject;
            public IObjectTargetObjectJoint TargetObjectJoint;
            public System.Threading.Tasks.Task<bool> Task;
        }
        public static RopeResult CreateRope(Vector2 position, IObject attachedObject, float maxLength, LineVisual visual = LineVisual.None)
        {
            var promise = new System.Threading.Tasks.TaskCompletionSource<bool>();
            var oPos = attachedObject.GetWorldPosition();
            var farPos = GetFarAwayPosition();
            // Setting up the rope length
            var distanceJoint = (IObjectDistanceJoint)Game.CreateObject("DistanceJoint", farPos);
            var distanceJointObject = Game.CreateObject("InvisibleBlockNoCollision", farPos);
            var targetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint", farPos + Vector2.UnitY * maxLength);
            attachedObject.SetWorldPosition(farPos + Vector2.UnitY * maxLength);

            Timeout(() =>
            {
                distanceJoint.SetWorldPosition(position);
                distanceJointObject.SetWorldPosition(position);
                distanceJoint.SetLineVisual(visual);
                targetJoint.SetWorldPosition(oPos);
                attachedObject.SetWorldPosition(oPos);
                promise.TrySetResult(true);
            }, 0);

            distanceJoint.SetTargetObject(distanceJointObject);
            distanceJoint.SetLengthType(DistanceJointLengthType.Elastic);
            distanceJoint.SetTargetObjectJoint(targetJoint);
            targetJoint.SetTargetObject(attachedObject);

            return new RopeResult
            {
                DistanceJoint = distanceJoint,
                DistanceJointObject = distanceJointObject,
                TargetObjectJoint = targetJoint,
                Task = promise.Task,
            };
        }

        // TODO: how to fall properly?
        public async static void Fall(IPlayer player)
        {
            try
            {
                await ExecuteSingleCommand(player, PlayerCommandType.StaggerInfinite)
                    .ContinueWith((r) => ExecuteSingleCommand(player, PlayerCommandType.Fall));
            }
            catch (Exception ex)
            {
                if (Game.IsEditorTest)
                {
                    Game.ShowChatMessage(ex.Message);
                    Game.ShowChatMessage(ex.StackTrace);
                }
                Game.WriteToConsole(ex.Message);
                Game.WriteToConsole(ex.StackTrace);
            }
        }
        public async static void KneelFall(IPlayer player)
        {
            try
            {
                await ExecuteSingleCommand(player, PlayerCommandType.DeathKneel)
                    .ContinueWith((r) => ExecuteSingleCommand(player, PlayerCommandType.Fall));
            }
            catch (Exception ex)
            {
                if (Game.IsEditorTest)
                {
                    Game.ShowChatMessage(ex.Message);
                    Game.ShowChatMessage(ex.StackTrace);
                }
                Game.WriteToConsole(ex.Message);
                Game.WriteToConsole(ex.StackTrace);
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
            {
                promise.TrySetResult(false);
                return promise.Task;
            }

            player.SetInputEnabled(false);
            // some commands like Stagger not working without this line
            player.AddCommand(new PlayerCommand(PlayerCommandType.FaceAt, facingDirection));

            Timeout(() =>
            {
                if (player != null) player.AddCommand(new PlayerCommand(commandType, facingDirection));
                if (delay == 0) return;
                Timeout(() =>
                {
                    if (player != null)
                    {
                        player.ClearCommandQueue();
                        player.SetInputEnabled(true);
                    }
                    promise.TrySetResult(true);
                }, delay);
            }, 2);

            return promise.Task;
        }

        // a better command method
        public static System.Threading.Tasks.Task<bool> Command(IPlayer player, PlayerCommandType commandType,
            FaceDirection direction = FaceDirection.None,
            int targetObjectID = 0)
        {
            var promise = new System.Threading.Tasks.TaskCompletionSource<bool>();
            var facingDirection = PlayerCommandFaceDirection.None;

            if (player == null || player.IsDead)
            {
                promise.TrySetResult(false);
                return promise.Task;
            }

            if (direction == FaceDirection.None)
                facingDirection = player.GetFaceDirection() == -1 ? PlayerCommandFaceDirection.Left : PlayerCommandFaceDirection.Right;
            else
                facingDirection = direction == FaceDirection.Left ? PlayerCommandFaceDirection.Left : PlayerCommandFaceDirection.Right;

            if (player.IsInputEnabled) player.SetInputEnabled(false);

            RunIf(() =>
            {
                if (targetObjectID != 0)
                    player.AddCommand(new PlayerCommand(commandType, targetObjectID, facingDirection));
                else
                    player.AddCommand(new PlayerCommand(commandType, facingDirection));

                RunIf(() =>
                {
                    player.SetInputEnabled(true);
                    promise.TrySetResult(true);
                }, If: () => player.IsDead || player.CurrentCommandIndex == player.PerformedCommandCount, interval: 32);
            }, If: () => !player.IsDead && player.CurrentCommandIndex == player.PerformedCommandCount, interval: 32);

            return promise.Task;
        }

        public static System.Threading.Tasks.Task<bool> Command(IPlayer player, PlayerCommand[] playerCommands)
        {
            var promise = new System.Threading.Tasks.TaskCompletionSource<bool>();

            if (player == null || player.IsDead)
            {
                promise.TrySetResult(false);
                return promise.Task;
            }

            if (player.IsInputEnabled) player.SetInputEnabled(false);

            var HasQueuedCommands = WithHasQueuedCommands(player);

            RunIf(() =>
            {
                foreach (var command in playerCommands) player.AddCommand(command);

                RunIf(() =>
                {
                    player.SetInputEnabled(true);
                    promise.TrySetResult(true);
                }, If: () => player.IsDead || !HasQueuedCommands());
            }, If: () => !player.IsDead && !HasQueuedCommands());

            return promise.Task;
        }

        public static Func<bool> WithHasQueuedCommands(IPlayer player)
        {
            var exhaustCommandTime = 0f;
            var waitTime = 32;

            return new Func<bool>(() =>
            {
                // don't return true inside this check, there is a frame between multiple commands where CurrentCommandIndex == PerformedCommandCount.
                // We need to wait for a little bit before we are sure that all commands are executed
                if (player.CurrentCommandIndex == player.PerformedCommandCount)
                {
                    if (exhaustCommandTime == 0) exhaustCommandTime = Game.TotalElapsedGameTime;
                }
                else
                    exhaustCommandTime = 0f;

                if (exhaustCommandTime != 0f && IsElapsed(exhaustCommandTime, waitTime))
                {
                    exhaustCommandTime = 0f;
                    return false;
                }
                return true;
            });
        }

        public static Vector2 GetFarAwayPosition()
        {
            var randX = RandomHelper.Between(0, 20);
            var randy = RandomHelper.Between(0, 20);
            return Game.GetCameraMaxArea().TopLeft + new Vector2(10 + randX, 10 + randy);
        }

        public static bool HaveUnderwear(IProfile profile)
        {
            var skin = profile.Skin;
            var noUnderwear = skin.Color1 == "Skin1" && skin.Color2 == "ClothingBrown"
                || skin.Color1 == "Skin2" && skin.Color2 == "ClothingPink"
                || skin.Color1 == "Skin3" && skin.Color2 == "ClothingLightPink"
                || skin.Color1 == "Skin4" && skin.Color2 == "ClothingLightPink"
                || skin.Color1 == "Skin5" && skin.Color2 == "ClothingLightGray";
            return !noUnderwear;
        }

        public static IProfile StripUnderwear(IProfile profile)
        {
            var skin = profile.Skin;
            if (skin.Color1 == "Skin1") profile.Skin.Color2 = "ClothingBrown";
            if (skin.Color1 == "Skin2") profile.Skin.Color2 = "ClothingPink";
            if (skin.Color1 == "Skin3") profile.Skin.Color2 = "ClothingLightPink";
            if (skin.Color1 == "Skin4") profile.Skin.Color2 = "ClothingLightPink";
            if (skin.Color1 == "Skin5") profile.Skin.Color2 = "ClothingLightGray";
            return profile;
        }

        public static List<ClothingType> StrippeableClothingTypes(IProfile profile)
        {
            var strippeableClothingTypes = new List<ClothingType>();

            if (profile.Accesory != null && CanBeStripped(ClothingType.Accesory, profile.Accesory.Name))
                strippeableClothingTypes.Add(ClothingType.Accesory);
            if (profile.ChestOver != null && CanBeStripped(ClothingType.ChestOver, profile.ChestOver.Name))
                strippeableClothingTypes.Add(ClothingType.ChestOver);
            if (profile.ChestUnder != null && CanBeStripped(ClothingType.ChestUnder, profile.ChestUnder.Name))
                strippeableClothingTypes.Add(ClothingType.ChestUnder);
            if (profile.Feet != null && CanBeStripped(ClothingType.Feet, profile.Feet.Name))
                strippeableClothingTypes.Add(ClothingType.Feet);
            if (profile.Hands != null && CanBeStripped(ClothingType.Hands, profile.Hands.Name))
                strippeableClothingTypes.Add(ClothingType.Hands);
            if (profile.Head != null && CanBeStripped(ClothingType.Head, profile.Head.Name))
                strippeableClothingTypes.Add(ClothingType.Head);
            if (profile.Legs != null && CanBeStripped(ClothingType.Legs, profile.Legs.Name))
                strippeableClothingTypes.Add(ClothingType.Legs);
            if (profile.Waist != null && CanBeStripped(ClothingType.Waist, profile.Waist.Name))
                strippeableClothingTypes.Add(ClothingType.Waist);

            return strippeableClothingTypes;
        }

        public static bool CanBeStripped(ClothingType type, string clothingItem)
        {
            switch (type)
            {
                case ClothingType.Head:
                {
                    switch (clothingItem)
                    {
                        case "Afro":
                        case "Buzzcut":
                        case "Mohawk":
                            return false;
                    }
                    break;
                }
                case ClothingType.Accesory:
                {
                    switch (clothingItem)
                    {
                        case "ClownMakeup":
                        case "ClownMakeup_fem":
                        case "Moustache":
                        case "Small Moustache":
                            return false;
                    }
                    break;
                }
            }

            return true;
        }

        public static IProfile Strip(IProfile profile, ClothingType clothingType)
        {
            if (clothingType == ClothingType.Accesory)
                profile.Accesory = null;
            if (clothingType == ClothingType.ChestOver)
                profile.ChestOver = null;
            if (clothingType == ClothingType.ChestUnder)
                profile.ChestUnder = null;
            if (clothingType == ClothingType.Feet)
                profile.Feet = null;
            if (clothingType == ClothingType.Hands)
                profile.Hands = null;
            if (clothingType == ClothingType.Head)
                profile.Head = null;
            if (clothingType == ClothingType.Legs)
                profile.Legs = null;
            if (clothingType == ClothingType.Waist)
                profile.Waist = null;
            return profile;
        }

        public static string GetSoundID(WeaponItem weaponItem)
        {
            switch (weaponItem)
            {
                // TODO: gun only for now
                // Superfighters Deluxe\Content\Data\Sounds\Sounds.sfds
                case WeaponItem.PISTOL45: return "Pistol45";
                case WeaponItem.ASSAULT: return "AssaultRifle";
                case WeaponItem.BAZOOKA: return "Bazooka";
                case WeaponItem.BOW: return "BowShoot";
                case WeaponItem.CARBINE: return "Carbine";
                case WeaponItem.FLAMETHROWER: return "Flamethrower";
                case WeaponItem.FLAREGUN: return "Flaregun";
                case WeaponItem.GRENADE_LAUNCHER: return "GLauncher";
                case WeaponItem.M60: return "M60";
                case WeaponItem.MACHINE_PISTOL: return "MachinePistol";
                case WeaponItem.MAGNUM: return "Magnum";
                case WeaponItem.MP50: return "MP50";
                case WeaponItem.PISTOL: return "Pistol";
                case WeaponItem.SHOTGUN: return "Shotgun";
                case WeaponItem.REVOLVER: return "Revolver";
                case WeaponItem.DARK_SHOTGUN: return "RiotShotgun";
                case WeaponItem.SAWED_OFF: return "SawedOff";
                case WeaponItem.SILENCEDPISTOL: return "SilencedPistol";
                case WeaponItem.SILENCEDUZI: return "SilencedUzi";
                case WeaponItem.SNIPER: return "Sniper";
                case WeaponItem.SUB_MACHINEGUN: return "SMG";
                case WeaponItem.TOMMYGUN: return "TommyGun";
                case WeaponItem.UZI: return "UZI";
                default: return "Pistol";
            }
        }
    }
}
