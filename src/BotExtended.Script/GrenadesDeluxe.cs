﻿using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrenadesDeluxe
{
    public partial class GameScript : GameScriptInterface
    {
        public GameScript() : base(null) { }
        
        /*
	        Grenades Deluxe! The next collaborative scripting event to add new grenade types to the game. 
	        Anyone and everyone is invited to get creative and make some cool explosives. The only guidelines
	        to remember are to keep the unavoidable mega death nukes to a minimum and be creative!
	
	        Below I have outlined some key parts of the base script which you may use to customize the behaviour
	        of your custom Grenades. You can change the thrown item, ammo counts and more but you will need to keep
	        in mind the limitations of WeaponItems and their respective MaxAmmo.
	
	
	
	        Base ThrowableBehaviour Class:
	
	        -----------------------------------------------------------------------------------------
	        public Throwable Throwable		| The Throwable item being manipulated by the Behaviour	
	        -----------------------------------------------------------------------------------------	
	        public virtual void Create()	| Called when the item is thrown and spawned
	        -----------------------------------------------------------------------------------------	
	        public virtual void Update()	| Called every game update
	        -----------------------------------------------------------------------------------------	
	        public virtual void Destroy()	| Called when timer is completed or source object is destroyed
	        -----------------------------------------------------------------------------------------	
	        public virtual bool IsRemoved() | Returns bool if the Throwable is ready to be removed, is supplied if not used
	        -----------------------------------------------------------------------------------------
	
	
	
	        Throwable Class Reference:
	
	        -----------------------------------------------------------------------------------------
	        public IObject Object 		| Gets the thrown object
	        -----------------------------------------------------------------------------------------
	        public IPlayer OwnerPlayer 	| Get the thrower of the object
	        -----------------------------------------------------------------------------------------
	        public bool OwnerColliding	| Get if the thrown object is still colliding with the owner
	        -----------------------------------------------------------------------------------------
	        public float ElapsedTime	| Get the total time in milliseconds the object is alive for
	        -----------------------------------------------------------------------------------------
        */
        public static List<ThrowableDefinition> ThrowableDefinitions = new List<ThrowableDefinition>()
        {
            //Template
            //new ThrowableDefinition(
            //       "Arrow Trap",										//Name
            // "Ebomb09",											//Author
            // "Shoots bouncy arrows out of the handheld trap",	//Description

            // WeaponItem.C4,										//Held Weapon Item
            // "WpnC4Thrown",										//Thrown Object Name

            // 1,													//Default Ammo
            // 1,													//Max Ammo

            // 3000,												//Detonation Time(ms), or -1 for no detonation time
            // ArrowTrap.Init										//Function returning new ThrowableBehaviour
            //),
               new ThrowableDefinition(
                   "Tearing Nugget",										//Name
             "NearHuscarl",											//Author
             "tears player clothing item",	//Description

             WeaponItem.GRENADES,										//Held Weapon Item
             "WpnGrenadesThrown",										//Thrown Object Name

             100,													//Default Ammo
             100,													//Max Ammo

             3000,												//Detonation Time(ms), or -1 for no detonation time
             TearingNugget.Init										//Function returning new ThrowableBehaviour
            ),
         //   new ThrowableDefinition(
         //       "Sticky Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "sticks the first object it contacts with and explodes after a while, can destroy small indestructible objects",	//Description
		       // WeaponItem.C4,										//Held Weapon Item
		       // "WpnC4Thrown",										//Thrown Object Name
		       // 4,													// Ebomb09: not working
		       // 4,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // StickyNugget.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Suicide Dove",										//Name
		       // "NearHuscarl",											//Author
		       // "creates a dove that finds the closest enemy and explodes",	//Description
		       // WeaponItem.GRENADES,										//Held Weapon Item
		       // "Dove00",										//Thrown Object Name
		       // 69,													// Ebomb09: not working
		       // 69,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // SuicideDove.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Present Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "spawns a bunch of presents in your face",	//Description
		       // WeaponItem.GRENADES,										//Held Weapon Item
		       // "WpnGrenadesThrown",										//Thrown Object Name
		       // 33,													// Ebomb09: not working
		       // 33,													// Ebomb09: not working
		       // 2000,												//Detonation Time(ms), or -1 for no detonation time
		       // PresentNugget.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Stun Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "stuns enemies, has a small chance to stun enemies in a range",	//Description
		       // WeaponItem.C4,										//Held Weapon Item
		       // "WpnC4Thrown",										//Thrown Object Name
		       // 420,													// Ebomb09: not working
		       // 420,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // StunNugget.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Spinning Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "spins and shoots bullets in circle",	//Description
		       // WeaponItem.GRENADES,										//Held Weapon Item
		       // "Baseball",										//Thrown Object Name
		       // 666,													// Ebomb09: not working
		       // 666,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // SpinnerNugget.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Smoke Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "spawns smoke that slows down & hides all players in range",	//Description
		       // WeaponItem.GRENADES,										//Held Weapon Item
		       // "ItemStrengthBoost",										//Thrown Object Name
		       // 20,													// Ebomb09: not working
		       // 20,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // SmokeNugget.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Blast Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "blows the enemy away",	//Description
		       // WeaponItem.SHURIKEN,										//Held Weapon Item
		       // "WpnShuriken",										//Thrown Object Name
		       // 999,													// Ebomb09: not working
		       // 999,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // BlastNugget.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Lightning Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "Electrocutes enemies, any nearby enemies will get electrocuted too",	//Description
		       // WeaponItem.SHURIKEN,										//Held Weapon Item
		       // "WpnShockBaton",										//Thrown Object Name
		       // int.MaxValue,													// Ebomb09: not working
		       // int.MaxValue,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // LightningNugget.Init										//Function returning new ThrowableBehaviour
	        //),
         //   new ThrowableDefinition(
         //       "Helium Nugget",										//Name
		       // "NearHuscarl",											//Author
		       // "Inflates hit enemy, overinflated enemies may explode if receive damage",	//Description
		       // WeaponItem.MINES,										//Held Weapon Item
		       // "Balloon00",										//Thrown Object Name
		       // int.MaxValue,													// Ebomb09: not working
		       // int.MaxValue,													// Ebomb09: not working
		       // -1,												//Detonation Time(ms), or -1 for no detonation time
		       // HeliumNugget.Init										//Function returning new ThrowableBehaviour
	        //),
        };

        #region NearHuscarl's utility classes. Use at your own risk

        public static class Constants
        {
            // normal explosion radius: bazooka rockets, grenades, mines, explosive barrels, propane tank
            internal const float ExplosionRadius = 38.5f;
            internal static CollisionFilter NoCollision
            {
                get
                {
                    return new CollisionFilter()
                    {
                        AboveBits = 0,
                        CategoryBits = 0,
                        MaskBits = 0,
                        AbsorbProjectile = false,
                        BlockExplosions = false,
                        BlockFire = false,
                        BlockMelee = false,
                        ProjectileHit = false,
                    };
                }
            }
        }

        public static class RandomHelper
        {
            public static Random Rnd { get; set; }
            static RandomHelper() { Rnd = new Random(); }

            public static float Between(float min, float max)
            {
                return (float)Rnd.NextDouble() * (max - min) + min;
            }

            public static T GetItem<T>(List<T> list)
            {
                if (list.Count == 0)
                    throw new Exception("list is empty");

                var rndIndex = Rnd.Next(list.Count);
                return list[rndIndex];
            }

            /// <summary>
            /// Chance from 0f to 1f. 0f means never. 1f means always
            /// </summary>
            /// <param name="chance"></param>
            /// <returns></returns>
            public static bool Percentage(float chance)
            {
                return Between(0f, 1f) < chance;
            }

            public static Vector2 WithinArea(Area area)
            {
                var center = area.Center;
                var halfWidth = area.Width / 2;
                var halfHeight = area.Height / 2;

                return new Vector2()
                {
                    X = Between(center.X - halfWidth, center.X + halfWidth),
                    Y = Between(center.Y - halfHeight, center.Y + halfHeight),
                };
            }
        }

        static class MathExtension
        {
            public const float OneDeg = MathHelper.PI / 180;

            public static float ToRadians(float angleDegree)
            {
                return (float)(angleDegree * Math.PI) / 180;
            }
            public static float ToDegree(float radians)
            {
                return radians * 180 / (float)Math.PI;
            }
            public static float NormalizeAngle(float radian)
            {
                var result = radian % MathHelper.TwoPI;
                return result < 0 ? result + MathHelper.TwoPI : result;
            }

            public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
            {
                var lengthSquare = vector.LengthSquared();
                if (lengthSquare > maxLength * maxLength)
                {
                    var length = (float)Math.Sqrt(lengthSquare);
                    var normalized_x = vector.X / length;
                    var normalized_y = vector.Y / length;
                    return new Vector2(normalized_x * maxLength, normalized_y * maxLength);
                }
                return vector;
            }
            public static float Diff(float a, float b)
            {
                return Math.Abs(Math.Abs(a) - Math.Abs(b));
            }
        }

        public static class ScriptHelper
        {
            public static bool IsInteractiveObject(IObject obj)
            {
                var cf = obj.GetCollisionFilter();
                return cf.CategoryBits == 0x0008
                    || cf.CategoryBits == 0x0010
                    || cf.CategoryBits == 0x0008 + 0x0010
                    || cf.CategoryBits == 0x0004
                    || cf.CategoryBits == 0x0002
                    || cf.CategoryBits == 0x0001;
            }
            public static Vector2 GetFarAwayPosition()
            {
                var randX = RandomHelper.Between(0, 20);
                var randy = RandomHelper.Between(0, 20);
                return Game.GetCameraMaxArea().BottomLeft + new Vector2(-10 - randX, -10 - randy);
            }

            public static bool SameTeam(IPlayer player1, IPlayer player2)
            {
                if (player1 == null || player2 == null) return false;
                return player1.GetTeam() != PlayerTeam.Independent && player1.GetTeam() == player2.GetTeam()
                    || player1.GetTeam() == PlayerTeam.Independent && player1.UniqueID == player2.UniqueID;
            }

            // TODO: remove once gurt fixed
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

            // Never use is keyword to check if IObject is IPlayer. it's extremely slow
            public static bool IsPlayer(IObject obj)
            {
                if (obj == null) return false;
                return obj.GetCollisionFilter().CategoryBits == 0x0004;
            }
            // A faster cast player (dont use as/is)
            public static IPlayer CastPlayer(IObject obj)
            {
                if (obj == null) return null;
                return Game.GetPlayer(obj.UniqueID);
            }
            public static bool IsIndestructible(IObject o) { return o.GetMaxHealth() == 1; }

            // TODO: remove this if gurt added IObject.DealDamage()
            public static void DealDamage(IObject o, float damage)
            {
                var p = CastPlayer(o);
                if (p != null) p.DealDamage(damage);
                else o.SetHealth(o.GetHealth() - damage);
            }
        }

        #endregion

        public class Nugget : ThrowableBehaviour
        {
            public float ExplosionRadius { get; protected set; }
            protected static int gen = 0; // LogDebug(gen) to see if there is any dangling references to throwable items

            public IObject Instance { get { return Throwable.Object; } }
            // Ebomb09: Throwable.Object.GetWorldPosition() is (0,0) inside Destroy(), so I have save the position from the last frame
            public Vector2 Position { get; private set; }
            private Vector2 m_createPosition;
            public float TotalDistanceTraveled { get; private set; }
            public bool CheckColliding { get; protected set; }
            public Vector2 Direction { get; private set; }

            public Nugget() { ExplosionRadius = Constants.ExplosionRadius; CheckColliding = false; }

            public override void Create()
            {
                base.Create();
                m_createPosition = Instance.GetWorldPosition();
                TotalDistanceTraveled = 0f;
            }

            public override void Update()
            {
                gen++;
                base.Update();

                if (CheckColliding) CheckIfCollide();

                Position = Instance.GetWorldPosition();
                Direction = Vector2.Normalize(Instance.GetLinearVelocity());
                TotalDistanceTraveled = Vector2.Distance(Position, m_createPosition);
            }

            public void InvokeExplosionCB()
            {
                var explosiveArea = ScriptHelper.GrowFromCenter(Position, Constants.ExplosionRadius * 2);
                var playersInRadius = Game.GetObjectsByArea<IPlayer>(explosiveArea)
                    .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), Position, ExplosionRadius));

                OnExploded(playersInRadius);
            }

            public override void Destroy()
            {
                InvokeExplosionCB();
                Instance.Remove();
            }

            protected virtual void OnExploded(IEnumerable<IPlayer> playersInRadius) { }

            private Vector2 m_lastVelocity;
            private float m_lastAngle;
            protected void CheckIfCollide()
            {
                var currentVec = Instance.GetLinearVelocity();
                var angle = ScriptHelper.GetAngle(Direction);
                IObject collidedObject = null;

                if (currentVec.Length() - m_lastVelocity.Length() <= -6
                    || TotalDistanceTraveled >= 15 && currentVec.Length() <= 1
                    || m_lastAngle != 0 && MathExtension.Diff(angle, m_lastAngle) >= MathExtension.OneDeg * 3)
                {
                    collidedObject = Game.GetObjectsByArea(Instance.GetAABB())
                        .Where(o => o.UniqueID != Instance.UniqueID && ScriptHelper.IsInteractiveObject(o))
                        .FirstOrDefault();

                    if (collidedObject != null)
                    {
                        OnCollision(collidedObject);
                    }
                }

                m_lastVelocity = currentVec;
                m_lastAngle = angle;
            }

            protected virtual void OnCollision(IObject collidedObject) { }
        }

        public class TearingNugget : Nugget
        {
            private const float TearingChance = .9f;
            private const float Tearing2Chance = .33f;

            public static ThrowableBehaviour Init() { return new TearingNugget(); }

            protected override void OnExploded(IEnumerable<IPlayer> playersInRadius)
            {
                base.OnExploded(playersInRadius);

                ScriptHelper.RunIn(() =>
                {
                    Game.DrawCircle(Position, ExplosionRadius, Color.Cyan);
                }, 2000);

                foreach (var player in playersInRadius)
                    Strip(player);
            }

            private void Strip(IPlayer player)
            {
                if (player.IsBurnedCorpse || player.IsRemoved) return;

                var profile = player.GetProfile();
                var stripeableClothingTypes = StrippeableClothingTypes(profile);

                if (RandomHelper.Percentage(TearingChance))
                {
                    StripPlayerClothingItem(player, stripeableClothingTypes);
                }
                if (RandomHelper.Percentage(Tearing2Chance))
                {
                    StripPlayerClothingItem(player, stripeableClothingTypes);
                }

                var extraDamage = (8 - stripeableClothingTypes.Count) * 1;
                if (!HaveUnderwear(profile)) extraDamage++;
                player.DealDamage(extraDamage);
            }

            private void StripPlayerClothingItem(IPlayer player, List<ClothingType> clothingTypes)
            {
                var profile = player.GetProfile();
                if (!clothingTypes.Any() && !HaveUnderwear(profile)) return;

                if (!clothingTypes.Any())
                {
                    var strippedProfile = StripUnderwear(profile);
                    player.SetProfile(strippedProfile);
                    player.SetBotName("Naked " + player.Name);
                }
                else
                {
                    var clothingTypeToStrip = RandomHelper.GetItem(clothingTypes);
                    var strippedProfile = Strip(profile, clothingTypeToStrip);

                    player.SetProfile(strippedProfile);
                }
            }

            private enum ClothingType
            {
                Accesory,
                ChestOver,
                ChestUnder,
                Feet,
                Hands,
                Head,
                Legs,
                Waist,
            }

            private bool HaveUnderwear(IProfile profile)
            {
                var skin = profile.Skin;
                var noUnderwear = skin.Color1 == "Skin1" && skin.Color2 == "ClothingBrown"
                    || skin.Color1 == "Skin2" && skin.Color2 == "ClothingPink"
                    || skin.Color1 == "Skin3" && skin.Color2 == "ClothingLightPink"
                    || skin.Color1 == "Skin4" && skin.Color2 == "ClothingLightPink"
                    || skin.Color1 == "Skin5" && skin.Color2 == "ClothingLightGray";
                return !noUnderwear;
            }

            private IProfile StripUnderwear(IProfile profile)
            {
                var skin = profile.Skin;
                if (skin.Color1 == "Skin1") profile.Skin.Color2 = "ClothingBrown";
                if (skin.Color1 == "Skin2") profile.Skin.Color2 = "ClothingPink";
                if (skin.Color1 == "Skin3") profile.Skin.Color2 = "ClothingLightPink";
                if (skin.Color1 == "Skin4") profile.Skin.Color2 = "ClothingLightPink";
                if (skin.Color1 == "Skin5") profile.Skin.Color2 = "ClothingLightGray";
                return profile;
            }

            private List<ClothingType> StrippeableClothingTypes(IProfile profile)
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

            private bool CanBeStripped(ClothingType type, string clothingItem)
            {
                switch (type)
                {
                    case ClothingType.Head:
                    {
                        switch (clothingItem)
                        {
                            // TODO: add female cases?
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

            private IProfile Strip(IProfile profile, ClothingType clothingType)
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
        }

        public class StickyNugget : Nugget
        {
            private float m_timeElasped = 0f;
            public IObject TargetedObject { get; private set; }
            public IPlayer TargetedPlayer { get; private set; }

            private Vector2 m_relPlayerPosition;
            public IObjectWeldJoint m_weldJoint;

            public static ThrowableBehaviour Init() { return new StickyNugget(); }

            public StickyNugget() { CheckColliding = true; }

            public override void Create()
            {
                base.Create();
                //Remove C4 Detonator
                Instance.SetLinearVelocity(Instance.GetLinearVelocity() * 1.5f);
                Throwable.OwnerPlayer.RemoveWeaponItemType(WeaponItemType.Thrown);
            }

            public override void Update()
            {
                base.Update();

                if (TargetedObject != null) Game.DrawArea(TargetedObject.GetAABB(), Color.Red);
                if (TargetedPlayer != null)
                {
                    if (TargetedPlayer.IsOnGround)
                    {
                        if (m_weldJoint != null)
                        {
                            Instance.SetBodyType(BodyType.Static);
                            m_weldJoint.Remove();
                            m_weldJoint = null;
                        }

                        if (TargetedPlayer.IsCrouching)
                        {
                            Instance.SetWorldPosition(TargetedPlayer.GetWorldPosition() - m_relPlayerPosition - Vector2.UnitY * 5);
                        }
                        else if (TargetedPlayer.IsRolling)
                        {
                            Instance.SetWorldPosition(TargetedPlayer.GetAABB().Center);
                        }
                        else
                        {
                            Instance.SetWorldPosition(TargetedPlayer.GetWorldPosition() - m_relPlayerPosition);
                        }
                    }
                    else if (TargetedPlayer.IsInMidAir) // cannot track position accurately when player is in mid air
                    {
                        if (m_weldJoint == null)
                        {
                            Instance.SetBodyType(BodyType.Dynamic);
                            m_weldJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
                            m_weldJoint.SetWorldPosition(Instance.GetWorldPosition());
                            m_weldJoint.SetTargetObjects(new List<IObject>() { Instance, TargetedPlayer });
                        }
                    }
                }

                if (m_timeElasped != 0 && ScriptHelper.IsElapsed(m_timeElasped, 2000))
                {
                    if (m_weldJoint != null) m_weldJoint.Remove();
                    DealExplosionDamage(Instance.GetWorldPosition());
                    Game.TriggerExplosion(Position);
                    Instance.Destroy();
                }
            }
            private void DealExplosionDamage(Vector2 center)
            {
                var filterArea = ScriptHelper.GrowFromCenter(center, ExplosionRadius * 2);
                var objectsInRadius = Game.GetObjectsByArea(filterArea)
                    .Where(o => filterArea.Contains(o.GetAABB())
                    && ScriptHelper.IntersectCircle(o.GetAABB(), center, ExplosionRadius));

                foreach (var o in objectsInRadius)
                {
                    if (ScriptHelper.IsIndestructible(o)) ScriptHelper.DealDamage(o, 1f);
                }
            }

            protected override void OnCollision(IObject collidedObject)
            {
                base.OnCollision(collidedObject);
                m_timeElasped = Game.TotalElapsedGameTime;
                TargetedObject = collidedObject;
                TargetedPlayer = ScriptHelper.CastPlayer(TargetedObject);
                CheckColliding = false;

                ScriptHelper.LogDebug(collidedObject.Name);
                if (TargetedPlayer != null)
                {
                    Instance.SetBodyType(BodyType.Static);
                    m_relPlayerPosition = TargetedPlayer.GetWorldPosition() - Instance.GetWorldPosition();
                }
                else
                {
                    m_weldJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
                    m_weldJoint.SetWorldPosition(Instance.GetWorldPosition());
                    m_weldJoint.SetTargetObjects(new List<IObject>() { Instance, TargetedObject });
                }
            }
        }

        public class SuicideDove : Nugget
        {
            public static ThrowableBehaviour Init() { return new SuicideDove(); }

            public override void Create()
            {
                base.Create();
                Instance.SetAngularVelocity(0);
            }

            public IPlayer Target { get; private set; }
            private float m_updateDelay = 0f;
            public override void Update()
            {
                base.Update();

                if (ScriptHelper.IsElapsed(m_updateDelay, 105))
                {
                    m_updateDelay = Game.TotalElapsedGameTime;

                    if (TotalDistanceTraveled > 20)
                    {
                        SearchTarget(Throwable.OwnerPlayer);
                    }
                }

                Guide(Instance);

                if (Target != null)
                {
                    Game.DrawArea(Instance.GetAABB(), Color.Green);
                    Game.DrawArea(Target.GetAABB(), Color.Green);

                    if (Instance.GetAABB().Intersects(Target.GetAABB()))
                    {
                        Game.TriggerExplosion(Instance.GetWorldPosition());
                    }
                }
            }

            private void SearchTarget(IPlayer owner)
            {
                var minDistanceToPlayer = float.MaxValue;

                foreach (var player in Game.GetPlayers())
                {
                    if (ScriptHelper.SameTeam(player, owner) || player.IsDead) continue;

                    var distanceToPlayer = Vector2.Distance(Position, player.GetWorldPosition());
                    if (minDistanceToPlayer > distanceToPlayer)
                    {
                        minDistanceToPlayer = distanceToPlayer;
                        Target = player;
                    }
                }
            }

            private float m_guideDelay = 0f;
            private float m_guideDelayTime = 1000f;
            private void Guide(IObject Instance)
            {
                if (ScriptHelper.IsElapsed(m_guideDelay, m_guideDelayTime))
                {
                    m_guideDelay = Game.TotalElapsedGameTime;
                    m_guideDelayTime = RandomHelper.Between(1000, 3000);

                    if (Target == null) return;
                    if (Vector2.Distance(Target.GetWorldPosition(), Position) >= 60)
                        m_guideDelayTime = 500;

                    var targetDirection = Vector2.Normalize(Target.GetWorldPosition() - Instance.GetWorldPosition());
                    var angle = MathExtension.NormalizeAngle(ScriptHelper.GetAngle(targetDirection));
                    var isFacingLeft = angle >= MathHelper.PIOver2 && angle <= MathHelper.PI * 3 / 2;

                    Instance.SetFaceDirection(isFacingLeft ? -1 : 1);
                    Instance.SetLinearVelocity(targetDirection * RandomHelper.Between(2, 8));
                }
            }
        }

        public class PresentNugget : Nugget
        {
            private static HashSet<int> PresentIDs = new HashSet<int>();
            static PresentNugget()
            {
                Events.ObjectTerminatedCallback.Start(os =>
                {
                    foreach (var o in os)
                    {
                        if (PresentIDs.Contains(o.UniqueID))
                        {
                            var position = o.GetWorldPosition();
                            // normally, the present spawn some random shits upon destroyed. make the present disappeared
                            // and spawn something else as a workaround
                            o.SetWorldPosition(new Vector2(-1000, 1000));
                            Game.PlayEffect(EffectName.DestroyCloth, position);

                            var rndNum = RandomHelper.Between(0, 100);
                            if (rndNum < 1) // big oof
                            {
                                SpawnBadSanta(position);
                            }
                            if (1 <= rndNum && rndNum < 5)
                            {
                                Game.CreateObject(RandomHelper.GetItem(m_oofs), position);
                            }
                            if (5 <= rndNum && rndNum < 30)
                            {
                                Game.CreateObject(RandomHelper.GetItem(m_presents), position);
                            }

                            PresentIDs.Remove(o.UniqueID);
                        }
                    }
                });
            }

            private static readonly List<string> m_presents = new List<string>()
            {
                "XmasPresent00",
                "WpnPistol",
                "WpnPistol45",
                "WpnSilencedPistol",
                "WpnMachinePistol",
                "WpnMagnum",
                "WpnRevolver",
                "WpnPumpShotgun",
                "WpnDarkShotgun",
                "WpnTommygun",
                "WpnSMG",
                "WpnM60",
                "WpnPipeWrench",
                "WpnChain",
                "WpnWhip",
                "WpnHammer",
                "WpnKatana",
                "WpnMachete",
                "WpnChainsaw",
                "WpnKnife",
                "WpnSawedoff",
                "WpnBat",
                "WpnBaton",
                "WpnShockBaton",
                "WpnLeadPipe",
                "WpnUzi",
                "WpnSilencedUzi",
                "WpnBazooka",
                "WpnAxe",
                "WpnAssaultRifle",
                "WpnMP50",
                "WpnSniperRifle",
                "WpnCarbine",
                "WpnFlamethrower",
                "ItemPills",
                "ItemMedkit",
                "ItemSlomo5",
                "ItemSlomo10",
                "ItemStrengthBoost",
                "ItemSpeedBoost",
                "ItemLaserSight",
                "ItemBouncingAmmo",
                "ItemFireAmmo",
                "WpnGrenades",
                "WpnMolotovs",
                "WpnMines",
                "WpnShuriken",
                "WpnBow",
                "WpnFlareGun",
                "WpnGrenadeLauncher",
            };
            private static readonly List<string> m_oofs = new List<string>()
            {
                "WpnGrenadesThrown",
                "WpnMolotovsThrown",
                "WpnMineThrown",
            };
            public static ThrowableBehaviour Init() { return new PresentNugget(); }

            public override void Destroy()
            {
                base.Destroy();

                for (var i = 0; i < 360; i += 72) // Spawn present 5 times (360 / 5 = 72)
                {
                    var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(i));
                    var present = Game.CreateObject("XmasPresent00", Position, 0, direction * ExplosionRadius, 0);
                    PresentIDs.Add(present.UniqueID);
                }
            }

            private static void SpawnBadSanta(Vector2 position)
            {
                var player = Game.CreatePlayer(position);

                player.SetModifiers(new PlayerModifiers()
                {
                    MaxHealth = 200,
                    CurrentHealth = 200,
                    ExplosionDamageTakenModifier = 0.5f,
                    MeleeForceModifier = 1.5f,
                    SizeModifier = 1.1f,
                    InfiniteAmmo = 1,
                });
                player.SetProfile(new IProfile()
                {
                    Name = "Bad Santa",
                    Accesory = new IProfileClothingItem("SantaMask", "ClothingLightGray", "ClothingLightGray", ""),
                    ChestOver = new IProfileClothingItem("Coat", "ClothingRed", "ClothingLightGray", ""),
                    ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray", ""),
                    Feet = new IProfileClothingItem("BootsBlack", "ClothingBrown", "ClothingLightGray", ""),
                    Gender = Gender.Male,
                    Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                    Head = new IProfileClothingItem("SantaHat", "ClothingRed", "ClothingLightGray", ""),
                    Legs = new IProfileClothingItem("Pants", "ClothingRed", "ClothingLightGray", ""),
                    Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink", ""),
                    Waist = new IProfileClothingItem("Belt", "ClothingDarkRed", "ClothingLightYellow", ""),
                });
                player.SetBotName("Bad Santa");

                player.SetBotBehaviorSet(BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.ChallengeA));
                player.SetBotBehaviorActive(true);

                player.SetTeam(PlayerTeam.Independent);

                player.GiveWeaponItem(WeaponItem.KNIFE);
                player.GiveWeaponItem(WeaponItem.M60);
                player.GiveWeaponItem(WeaponItem.UZI);

                Game.CreateDialogue("Ho ho ho!", new Color(128, 32, 32), player);
            }
        }

        public class StunNugget : Nugget
        {
            private class StunInfo
            {
                public IPlayer Player;
                public float StunTime;
                public float Elapsed = 0f;
            }
            private static Dictionary<int, StunInfo> StunnedPlayers = new Dictionary<int, StunInfo>();
            static StunNugget()
            {
                Events.UpdateCallback.Start(e =>
                {
                    foreach (var kv in StunnedPlayers.ToList())
                    {
                        var info = kv.Value;
                        var player = info.Player;

                        info.Elapsed += e;

                        if (!player.IsDeathKneeling)
                            player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));

                        if (ScriptHelper.IsElapsed(info.StunTime, 400))
                        {
                            info.StunTime = Game.TotalElapsedGameTime;
                            Game.PlayEffect(EffectName.Electric, RandomHelper.WithinArea(player.GetAABB()));
                        }
                        if (info.Elapsed >= StunTime)
                        {
                            player.AddCommand(new PlayerCommand(PlayerCommandType.StopDeathKneel));
                            player.SetInputEnabled(true);
                            StunnedPlayers.Remove(player.UniqueID);
                        }
                    }
                });
            }

            public static ThrowableBehaviour Init() { return new StunNugget(); }
            public const float StunRangeChance = 0.05f;
            public const float StunTime = 2000;

            public StunNugget() { CheckColliding = true; ExplosionRadius = 30f; }

            public override void Create()
            {
                //Remove C4 Detonator
                Throwable.OwnerPlayer.RemoveWeaponItemType(WeaponItemType.Thrown);
            }

            protected override void OnCollision(IObject collidedObject)
            {
                base.OnCollision(collidedObject);
                CheckColliding = false;

                if (RandomHelper.Percentage(StunRangeChance))
                    ElectrocuteRange(Instance, collidedObject);
                else
                    Electrocute(Instance, collidedObject);
                Instance.Remove();
            }

            private void PlayStunEffects(Vector2 position, bool isStunningPlayer)
            {
                Game.PlayEffect(EffectName.Electric, position);
                Game.PlaySound("ElectricSparks", position);

                var sparkFireChance = isStunningPlayer ? .1f : .5f;
                if (RandomHelper.Percentage(sparkFireChance))
                {
                    Game.SpawnFireNode(position, Vector2.Zero);
                    Game.PlayEffect(EffectName.FireTrail, position);
                }
            }

            private void Stun(IPlayer player) { Stun(player, player.GetWorldPosition()); }
            private void Stun(IPlayer player, Vector2 hitPosition)
            {
                if (!CanBeStunned(player)) return;

                PlayStunEffects(hitPosition, true);
                Game.PlayEffect(EffectName.CustomFloatText, hitPosition, "stunned");

                player.SetInputEnabled(false);
                player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));

                StunnedPlayers.Add(player.UniqueID, new StunInfo()
                {
                    Player = player,
                    StunTime = Game.TotalElapsedGameTime,
                });
            }

            private void ElectrocuteRange(IObject Instance, IObject collidedObject)
            {
                foreach (var player in Game.GetPlayers())
                {
                    if (ScriptHelper.IntersectCircle(player.GetAABB(), Position, ExplosionRadius))
                    {
                        Stun(player);
                    }
                }

                for (var i = 0; i < 360; i += 72) // Play electric effect 5 times in circle (360 / 5 = 72)
                {
                    var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(i));

                    Game.PlayEffect(EffectName.Electric, Position + direction * ExplosionRadius);
                    Game.PlaySound("ElectricSparks", Position);
                }

                if (Game.IsEditorTest)
                {
                    Events.UpdateCallback.Start((e) => Game.DrawCircle(Position, ExplosionRadius, Color.Cyan),
                        0, 60 * 2);
                }
            }

            private void Electrocute(IObject Instance, IObject collidedObject)
            {
                var player = ScriptHelper.CastPlayer(collidedObject);
                if (player != null && !player.IsDead)
                {
                    Stun(player, Position);
                }
                else
                    PlayStunEffects(Position, false);
            }

            private bool CanBeStunned(IPlayer player)
            {
                return !player.IsDead && !StunnedPlayers.ContainsKey(player.UniqueID);
            }
        }

        public abstract class HoveringNugget : Nugget
        {
            protected Vector2 HoverPosition;
            protected float ExplodeRange = 60;
            protected float ExplodeRange2 = 10;

            private enum HeadingDirection { Left, Top, Right, Bottom, }
            protected enum State { Normal, Hovering, Destroyed, }
            protected State CurrentState { get; private set; }

            public HoveringNugget() { CurrentState = State.Normal; CheckColliding = true; }

            public override void Create()
            {
                base.Create();
                Instance.SetAngle(0);
                Instance.SetLinearVelocity(Instance.GetLinearVelocity() * 2f);
            }

            public override void Update()
            {
                base.Update();

                switch (CurrentState)
                {
                    case State.Normal:
                    {
                        if (CanHover()) Hover();
                        break;
                    }
                    case State.Hovering:
                    {
                        Instance.SetWorldPosition(HoverPosition);
                        Instance.SetLinearVelocity(new Vector2(0, .6f));
                        Instance.SetAngle(0);
                        UpdateHovering();
                        break;
                    }
                    case State.Destroyed:
                        break;
                }
            }

            protected override void OnCollision(IObject collidedObject)
            {
                base.OnCollision(collidedObject);
                CheckColliding = false;

                if (CurrentState == State.Normal)
                    Hover();
            }

            protected void Hover()
            {
                if (Instance.IsRemoved) return;

                CurrentState = State.Hovering;
                HoverPosition = Instance.GetWorldPosition();
                Instance.SetLinearVelocity(new Vector2(0, .6f));
                Instance.SetAngle(0);
                OnHover();
            }
            protected virtual void UpdateHovering() { }

            protected virtual void OnHover() { }
            protected virtual void StopHovering()
            {
                Destroy();
                CurrentState = State.Destroyed;
            }

            private HeadingDirection GetHeadingDirection(float angle)
            {
                angle = MathExtension.NormalizeAngle(angle);

                if (angle >= 0 && angle < MathHelper.PIOver4 || angle >= MathHelper.PI * 3/2 && angle <= MathHelper.PIOver2)
                    return HeadingDirection.Right;
                if (angle >= MathHelper.PIOver4 && angle < MathHelper.PIOver2 + MathHelper.PIOver4)
                    return HeadingDirection.Top;
                if (angle >= MathHelper.PIOver2 + MathHelper.PIOver4 && angle < MathHelper.PI + MathHelper.PIOver4)
                    return HeadingDirection.Left;
                return HeadingDirection.Bottom;
            }

            private bool CanHover()
            {
                var headingDirection = GetHeadingDirection(ScriptHelper.GetAngle(Direction));
                var explodeRange = ScriptHelper.GrowFromCenter(Position,
                    headingDirection == HeadingDirection.Left ? ExplodeRange : ExplodeRange2,
                    headingDirection == HeadingDirection.Top ? ExplodeRange : ExplodeRange2,
                    headingDirection == HeadingDirection.Right ? ExplodeRange : ExplodeRange2,
                    headingDirection == HeadingDirection.Bottom ? ExplodeRange : ExplodeRange2);
                var os = Game.GetObjectsByArea(explodeRange, PhysicsLayer.Active);

                Game.DrawArea(explodeRange);

                foreach (var o in os)
                {
                    var collisionFilter = o.GetCollisionFilter();
                    if ((collisionFilter.BlockExplosions || ScriptHelper.IsPlayer(o)) && TotalDistanceTraveled >= 100)
                    {
                        var position = Position;
                        if (Game.IsEditorTest)
                        {
                            ScriptHelper.RunIn(() =>
                            {
                                Game.DrawCircle(position, .5f, Color.Red);
                                Game.DrawLine(position, o.GetWorldPosition(), Color.Yellow);
                                Game.DrawArea(o.GetAABB(), Color.Yellow);
                                Game.DrawText(o.Name + " " + headingDirection, position);
                                Game.DrawArea(explodeRange);
                            }, 2000);
                        }
                        return true;
                    }
                }

                return false;
            }
        }

        public class SpinnerNugget : HoveringNugget
        {
            public static ThrowableBehaviour Init() { return new SpinnerNugget(); }

            private float m_fireTime = 0f;
            private float m_fireAngle = 0f;
            protected override void UpdateHovering()
            {
                base.UpdateHovering();

                if (ScriptHelper.IsElapsed(m_fireTime, 30))
                {
                    Instance.SetWorldPosition(ScriptHelper.GetFarAwayPosition());
                    ScriptHelper.Timeout(() => Instance.SetWorldPosition(HoverPosition), 0);
                    var totalBullets = 20;
                    var angleInBetween = 360 / totalBullets;
                    var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(m_fireAngle));

                    Game.PlaySound("SilencedUzi", HoverPosition);
                    Game.SpawnProjectile(ProjectileItem.MAGNUM, HoverPosition, direction);

                    if (m_fireAngle == 360 - angleInBetween)
                        StopHovering();

                    m_fireTime = Game.TotalElapsedGameTime;
                    m_fireAngle += angleInBetween;
                }
            }

            protected override void StopHovering()
            {
                base.StopHovering();
                Game.TriggerExplosion(HoverPosition);
                Game.PlayEffect(EffectName.Block, HoverPosition);
            }
        }

        public class SmokeNugget : Nugget
        {
            public static readonly float SmokeTime = Game.IsEditorTest ? 22000f : 22000f;
            private float m_explodeTime = 0f;
            private Vector2 m_explodePosition;
            public float CurrentSmokeRadius { get; private set; }

            public static ThrowableBehaviour Init() { return new SmokeNugget(); }
            public SmokeNugget() { CheckColliding = true; ExplosionRadius = 50f; CurrentSmokeRadius = 0f; }

            protected override void OnCollision(IObject collidedObject)
            {
                base.OnCollision(collidedObject);
                CheckColliding = false;
                Instance.SetWorldPosition(ScriptHelper.GetFarAwayPosition()); // hide instance without removing completely
                Instance.SetBodyType(BodyType.Static);

                m_explodePosition = Position;
                m_explodeTime = Game.TotalElapsedGameTime;

                var rcResult = Game.RayCast(Position, Position - Vector2.UnitY * ExplosionRadius, new RayCastInput()
                {
                    IncludeOverlap = true,
                    ClosestHitOnly = true,
                    FilterOnMaskBits = true,
                    MaskBits = 0x0001,
                }).Where(r => r.HitObject != null);

                if (rcResult.Any()) m_groundPositionY = rcResult.Single().HitObject.GetWorldPosition().Y;
                else m_groundPositionY = float.MinValue;
            }

            private class SmokeInfo
            {
                public bool IsNametagVisible = false;
                public bool IsStatusBarsVisible = false;
                public IPlayer Player;
                public PlayerModifiers OldModifiers;
                public BotBehaviorSet OldBotBehaviorSet;
            }

            private static HashSet<int> AllPlayersAffected = new HashSet<int>();
            private Dictionary<int, SmokeInfo> m_playersAffected = new Dictionary<int, SmokeInfo>();
            private float m_smokeEffectDelay = 0f;
            private float m_smokeEffectBottomDelay = 0f;
            private float m_updateDelay = 0f;
            private float m_groundPositionY;
            private float m_smokeRadiusExpandDelay = 0f;
            public override void Update()
            {
                base.Update();

                if (m_explodeTime == 0f) return;

                if (m_explodeTime != 0f && ScriptHelper.IsElapsed(m_explodeTime, SmokeTime))
                {
                    foreach (var kv in m_playersAffected.ToList())
                    {
                        var info = kv.Value;
                        Restore(info);
                    }
                    Instance.Remove();
                    return;
                }

                if (ScriptHelper.IsElapsed(m_smokeRadiusExpandDelay, 400))
                {
                    m_smokeRadiusExpandDelay = Game.TotalElapsedGameTime;
                    CurrentSmokeRadius = Math.Min(CurrentSmokeRadius + 6, ExplosionRadius);
                }

                if (!ScriptHelper.IsElapsed(m_explodeTime, SmokeTime))
                {
                    Game.DrawCircle(m_explodePosition, CurrentSmokeRadius, Color.Cyan);
                    var playSmokeEffect = false;
                    var playSmokeEffectBottom = false;

                    if (ScriptHelper.IsElapsed(m_smokeEffectDelay, 460))
                    {
                        m_smokeEffectDelay = Game.TotalElapsedGameTime;
                        playSmokeEffect = true;
                    }
                    if (ScriptHelper.IsElapsed(m_smokeEffectBottomDelay, 300))
                    {
                        m_smokeEffectBottomDelay = Game.TotalElapsedGameTime;
                        playSmokeEffectBottom = true;
                    }

                    var isBottom = false;
                    var startY = Math.Max(m_groundPositionY, m_explodePosition.Y - CurrentSmokeRadius);
                    for (var i = -CurrentSmokeRadius; i < CurrentSmokeRadius; i += 6)
                    {
                        for (var j = -CurrentSmokeRadius; j < CurrentSmokeRadius; j += 6)
                        {
                            var p = m_explodePosition + new Vector2(i, j);
                            if (!IsInside(p) || p.Y < startY)
                            {
                                isBottom = true;
                                continue;
                            }
                            else
                                isBottom = isBottom || j == -CurrentSmokeRadius;

                            if (isBottom && playSmokeEffectBottom || playSmokeEffect)
                            {
                                Game.PlayEffect(EffectName.Dig, p);
                                Game.DrawCircle(p, .5f, isBottom ? Color.Green : Color.Red);
                            }
                            isBottom = false;
                        }
                    }
                }

                if (ScriptHelper.IsElapsed(m_updateDelay, 150))
                {
                    m_updateDelay = Game.TotalElapsedGameTime;

                    foreach (var player in Game.GetPlayers())
                    {
                        if (!player.IsDead && !m_playersAffected.ContainsKey(player.UniqueID) && IsInside(player)
                            && !AllPlayersAffected.Contains(player.UniqueID))
                        {
                            m_playersAffected.Add(player.UniqueID, new SmokeInfo()
                            {
                                IsNametagVisible = player.GetNametagVisible(),
                                IsStatusBarsVisible = player.GetStatusBarsVisible(),
                                Player = player,
                                OldModifiers = player.GetModifiers(),
                                OldBotBehaviorSet = player.GetBotBehaviorSet(),
                            });
                            AllPlayersAffected.Add(player.UniqueID);

                            var mod = player.GetModifiers();
                            mod.RunSpeedModifier = .6f;
                            mod.SprintSpeedModifier = .6f;
                            mod.MeleeDamageTakenModifier = 2f;
                            mod.ProjectileDamageTakenModifier = 2f;
                            mod.FireDamageTakenModifier = 2f;
                            mod.ExplosionDamageTakenModifier = 2f;
                            player.SetModifiers(mod);

                            var bs = player.GetBotBehaviorSet();
                            bs.RangedWeaponAccuracy = 0f;
                            bs.RangedWeaponPrecisionAccuracy = .1f;
                            player.SetBotBehaviorSet(bs);

                            player.SetNametagVisible(false);
                            player.SetStatusBarsVisible(false);
                        }
                    }
                    foreach (var kv in m_playersAffected.ToList())
                    {
                        var info = kv.Value;
                        var player = info.Player;

                        if (player.IsDead || !IsInside(player))
                        {
                            Restore(info);
                        }
                    }
                }
            }

            private void Restore(SmokeInfo info)
            {
                var player = info.Player;
                player.SetModifiers(info.OldModifiers);
                player.SetBotBehaviorSet(info.OldBotBehaviorSet);
                player.SetNametagVisible(info.IsNametagVisible);
                player.SetStatusBarsVisible(info.IsStatusBarsVisible);

                m_playersAffected.Remove(player.UniqueID);
                AllPlayersAffected.Remove(player.UniqueID);
            }

            private bool IsInside(Vector2 position) { return ScriptHelper.IntersectCircle(position, m_explodePosition, CurrentSmokeRadius); }
            private bool IsInside(IPlayer player)
            {
                var hitBox = player.GetAABB();
                return ScriptHelper.IntersectCircle(hitBox, m_explodePosition, CurrentSmokeRadius) && hitBox.Top >= m_groundPositionY;
            }
        }

        public class BlastNugget : Nugget
        {
            public static ThrowableBehaviour Init() { return new BlastNugget(); }

            public BlastNugget() { CheckColliding = true; }

            public override void Create()
            {
                base.Create();
                Instance.SetLinearVelocity(Instance.GetLinearVelocity() * 1.25f);
            }

            protected override void OnCollision(IObject collidedObject)
            {
                base.OnCollision(collidedObject);
                CheckColliding = false;
                Instance.Remove();
                
                var isPlayer = ScriptHelper.IsPlayer(collidedObject);
                if (!isPlayer && collidedObject.GetBodyType() == BodyType.Static) return;

                var modifiers = GetForceModifier();
                var velocity = collidedObject.GetLinearVelocity();
                if (isPlayer)
                {
                    var player = Game.GetPlayer(collidedObject.UniqueID);

                    player.SetInputEnabled(false);
                    player.AddCommand(new PlayerCommand(PlayerCommandType.Fall));

                    ScriptHelper.Timeout(() =>
                    {
                        player.ClearCommandQueue();
                        player.SetInputEnabled(true);
                    }, 3);

                    velocity += (Direction * 4 + Vector2.UnitY * 4) * modifiers * 6;
                    collidedObject.SetLinearVelocity(MathExtension.ClampMagnitude(velocity, 15));
                }
                else
                {
                    var mass = collidedObject.GetMass();
                    var magnitude = MathHelper.Clamp(1f / mass / 7f, 3, 30) * modifiers;
                    velocity += (Direction * magnitude + Vector2.UnitY) * magnitude / 15;
                    collidedObject.SetLinearVelocity(velocity);
                    //ScriptHelper.LogDebug(hitObject.Name, mass, magnitude);
                }
                if (Game.IsEditorTest)
                {
                    var p = Position;
                    ScriptHelper.RunIn(() =>
                    {
                        Game.DrawLine(p, p + velocity);
                    }, 2000);
                }
            }

            private float GetForceModifier()
            {
                // (0,1.25) (70,1) (140, 0.75)
                return 1.25f - 0.00357143f * TotalDistanceTraveled;
            }
        }

        public class LightningNugget : Nugget
        {
            public const float LightningDamage = 5f;

            public static ThrowableBehaviour Init() { return new LightningNugget(); }

            public LightningNugget() { CheckColliding = true; ExplosionRadius = 20f; }

            public override void Create()
            {
                base.Create();
                Instance.SetLinearVelocity(Instance.GetLinearVelocity() * 1.75f);
            }

            public override void Update()
            {
                base.Update();

                if (m_pendingUpdate.Any())
                {
                    if (Game.TotalElapsedGameTime >= m_pendingUpdate.First().HitTime)
                    {
                        var i = m_pendingUpdate.First();
                        i.Action.Invoke();
                        m_pendingUpdate.RemoveAt(0);
                    }
                }
                else
                {
                    if (!CheckColliding) Instance.Remove();
                }
            }

            protected override void OnCollision(IObject collidedObject)
            {
                base.OnCollision(collidedObject);
                CheckColliding = false;
                Instance.SetWorldPosition(ScriptHelper.GetFarAwayPosition());
                Instance.SetBodyType(BodyType.Static);

                Electrocute(collidedObject);
                InvokeExplosionCB();
            }

            protected override void OnExploded(IEnumerable<IPlayer> playersInRadius)
            {
                base.OnExploded(playersInRadius);
                foreach (var p in playersInRadius) Electrocute(p);
            }

            private HashSet<int> m_electrocutedObjects = new HashSet<int>();
            private class LightningInfo
            {
                public Action Action;
                public float HitTime;
            }
            private List<LightningInfo> m_pendingUpdate = new List<LightningInfo>();
            private void Electrocute(IObject obj)
            {
                if (m_electrocutedObjects.Contains(obj.UniqueID) || obj.IsRemoved) return;

                var position = obj.GetWorldPosition();
                if (!ScriptHelper.IsIndestructible(obj))
                {
                    ScriptHelper.DealDamage(obj, LightningDamage);
                    Game.PlayEffect(EffectName.Electric, position);
                    if (RandomHelper.Percentage(.02f))
                    {
                        Game.SpawnFireNode(position, Vector2.Zero);
                        Game.PlayEffect(EffectName.FireTrail, position);
                    }
                }
                m_electrocutedObjects.Add(obj.UniqueID);

                foreach (var p in GetPlayersInRange(obj))
                {
                    m_pendingUpdate.Add(new LightningInfo()
                    {
                        HitTime = m_pendingUpdate.Any() ? m_pendingUpdate.Last().HitTime + 23 : Game.TotalElapsedGameTime,
                        Action = () =>
                        {
                            if (p == null || p.IsRemoved || m_electrocutedObjects.Contains(p.UniqueID)) return;
                            var results = Game.RayCast(position, p.GetWorldPosition(), new RayCastInput()
                            {
                                IncludeOverlap = true,
                                FilterOnMaskBits = true,
                                MaskBits = 0x0004 + 0x0001,
                            }).Where(r => r.HitObject != null);

                            foreach (var result in results)
                            {
                                if (!result.IsPlayer && result.HitObject.GetCollisionFilter().AbsorbProjectile)
                                {
                                    m_electrocutedObjects.Add(result.ObjectID);
                                    break;
                                }

                                if (IsConductive(result.HitObject))
                                    Electrocute(result.HitObject);

                                if (Game.IsEditorTest)
                                {
                                    ScriptHelper.RunIn(() =>
                                    {
                                        Game.DrawLine(position, p.GetWorldPosition());
                                        Game.DrawArea(result.HitObject.GetAABB(), Color.Cyan);
                                    }, 800);
                                }
                            }
                        },
                    });
                }
            }

            private IEnumerable<IPlayer> GetPlayersInRange(IObject electrocutedObject)
            {
                if (ScriptHelper.IsPlayer(electrocutedObject))
                {
                    var position = electrocutedObject.GetWorldPosition();
                    var filterArea = ScriptHelper.GrowFromCenter(position, ExplosionRadius * 2);
                    return Game.GetObjectsByArea<IPlayer>(filterArea)
                        .Where(o => !m_electrocutedObjects.Contains(o.UniqueID)
                        && ScriptHelper.IntersectCircle(o.GetAABB(), position, ExplosionRadius));
                }
                else
                {
                    return Game.GetObjectsByArea<IPlayer>(electrocutedObject.GetAABB());
                }
            }

            private bool IsConductive(IObject o)
            {
                return o.CanBurn
                    || o.Name.StartsWith("Metal")
                    || o.Name.StartsWith("Wood");
            }
        }

        public class HeliumNugget : Nugget
        {
            private static readonly List<string> BalloonColors = new List<string>
            {
                "BgLightRed",
                "BgLightOrange",
                "BgLightYellow",
                "BgLightGreen",
                "BgLightBlue",
                "BgLightCyan",
                "BgLightMagenta",
                "BgLightPink",
            };

            public static ThrowableBehaviour Init() { return new HeliumNugget(); }

            public HeliumNugget() { CheckColliding = true; ExplosionRadius = 20f; }

            public override void Create()
            {
                base.Create();
                Instance.SetMass(.1f);
                Instance.SetColor1(RandomHelper.GetItem(BalloonColors));
            }

            protected override void OnCollision(IObject collidedObject)
            {
                base.OnCollision(collidedObject);
                CheckColliding = false;
                InvokeExplosionCB();
                Instance.Remove();
            }

            protected override void OnExploded(IEnumerable<IPlayer> playersInRadius)
            {
                base.OnExploded(playersInRadius);
                foreach (var p in playersInRadius)
                {
                    var info = GetInfo(p);
                    info.Inflate(5f);
                }

                for (var i = 0; i < 360; i += 72)
                {
                    var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(i));
                    Game.PlayEffect(EffectName.Smack, Position + direction * ExplosionRadius);
                }
            }

            private class HeliumInfo
            {
                public HeliumInfo(IPlayer player)
                {
                    Player = player;
                    var magnetPosition = GetMagnetPosition();

                    PullJoint = (IObjectPullJoint)Game.CreateObject("PullJoint");
                    Magnet = Game.CreateObject("InvisibleBlockSmall");
                    MagnetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");

                    Magnet.SetCollisionFilter(Constants.NoCollision);
                    Magnet.SetWorldPosition(magnetPosition);
                    MagnetJoint.SetWorldPosition(magnetPosition);
                    MagnetJoint.SetTargetObject(Magnet);

                    PullJoint.SetWorldPosition(Player.GetWorldPosition());
                    PullJoint.SetTargetObject(Player);
                    PullJoint.SetTargetObjectJoint(MagnetJoint);
                    PullJoint.SetForce(0);
                }

                private Vector2 GetMagnetPosition() { return Player.GetWorldPosition() + Vector2.UnitY * 50; }

                private List<KeyValuePair<float, float>> m_deflateTimes = new List<KeyValuePair<float, float>>();
                private float m_updateDelay = 0f;
                private bool m_oldIsFalling = false;
                private Vector2 m_oldLinearVelocity = Vector2.Zero;
                private float m_fallingTime = 0f;
                public void Update(float elapsed)
                {
                    if (ScriptHelper.IsElapsed(m_updateDelay, 250))
                    {
                        m_updateDelay = Game.TotalElapsedGameTime;
                        var magnetPosition = GetMagnetPosition();
                        Magnet.SetWorldPosition(magnetPosition);
                        MagnetJoint.SetWorldPosition(magnetPosition);
                        PullJoint.SetForce(InflatedModifier);
                        if (!Player.IsFalling && RandomHelper.Percentage(InflatedModifier))
                        {
                            Player.SetInputEnabled(false);
                            Player.AddCommand(new PlayerCommand(PlayerCommandType.Fall));
                            ScriptHelper.Timeout(() =>
                            {
                                Player.ClearCommandQueue();
                                Player.SetInputEnabled(true);
                            }, 4);
                        }

                        if (m_deflateTimes.Any())
                        {
                            var deflateInfo = m_deflateTimes.First();
                            if (Game.TotalElapsedGameTime >= deflateInfo.Key)
                            {
                                Deflate(deflateInfo.Value);
                                m_deflateTimes.RemoveAt(0);
                            }
                        }
                    }

                    var velocity = Player.GetLinearVelocity();

                    if (Player.IsFalling && !m_oldIsFalling)
                    {
                        m_oldIsFalling = true;
                        m_fallingTime = Game.TotalElapsedGameTime;

                        var velocityDiff = MathExtension.Diff(velocity.Length(), m_oldLinearVelocity.Length());

                        //ScriptHelper.RunIn(() => Game.DrawText(velocityDiff.ToString(),
                        //Player.GetWorldPosition() + Vector2.UnitY * 15), 1000);
                        if (velocityDiff >= 4)
                            Player.SetLinearVelocity(velocity + Vector2.Normalize(velocity) * InflatedModifier * 70);
                    }
                    if (!Player.IsFalling && m_oldIsFalling)
                        m_oldIsFalling = false;

                    m_oldLinearVelocity = velocity;
                }

                public void Inflate(float modifier)
                {
                    var prevDeflateTime = m_deflateTimes.Any() ? m_deflateTimes.Last().Key : Game.TotalElapsedGameTime;
                    prevDeflateTime = Math.Max(prevDeflateTime, Game.TotalElapsedGameTime);
                    m_deflateTimes.Add(new KeyValuePair<float, float>(prevDeflateTime + 10000, modifier));
                    ScriptHelper.LogDebug(modifier);
                    InflatedModifier += .0125f * modifier; // .2f is no-return value (~16 shots)

                    var mod = Player.GetModifiers();
                    mod.SizeModifier += 0.015f * modifier;
                    mod.ImpactDamageTakenModifier -= .1f * modifier;
                    Player.SetModifiers(mod);
                }

                public void Deflate(float modifier)
                {
                    InflatedModifier -= .0125f * modifier;

                    var mod = Player.GetModifiers();
                    mod.SizeModifier -= 0.015f * modifier;
                    mod.ImpactDamageTakenModifier += .1f * modifier;
                    Player.SetModifiers(mod);
                }

                public void Remove()
                {
                    PullJoint.Remove();
                    MagnetJoint.Remove();
                    Magnet.Remove();
                }

                public IPlayer Player;
                public IObjectPullJoint PullJoint;
                public IObjectTargetObjectJoint MagnetJoint;
                public IObject Magnet;
                public float InflatedModifier = 0f;
            }

            private static Dictionary<int, HeliumInfo> HeliumInfos = new Dictionary<int, HeliumInfo>();
            private static HeliumInfo GetInfo(IPlayer player)
            {
                HeliumInfo info;

                if (!HeliumInfos.TryGetValue(player.UniqueID, out info))
                {
                    info = new HeliumInfo(player);
                    HeliumInfos.Add(player.UniqueID, info);
                }
                return info;
            }

            static HeliumNugget()
            {
                Events.PlayerDeathCallback.Start((p, a) =>
                {
                    if (a.Removed)
                    {
                        GetInfo(p).Remove();
                        HeliumInfos.Remove(p.UniqueID);
                    }
                });
                Events.PlayerDamageCallback.Start((IPlayer p, PlayerDamageArgs a) =>
                {
                    var info = GetInfo(p);
                    var dmgTypeModifier = 1f;

                    if (a.DamageType == PlayerDamageEventType.Missile) dmgTypeModifier = 1.5f;
                    if (a.DamageType == PlayerDamageEventType.Projectile) dmgTypeModifier = 2f;
                    if (a.DamageType == PlayerDamageEventType.Explosion) dmgTypeModifier = 4f;

                    var popChance = info.InflatedModifier * 0.05f * a.Damage * dmgTypeModifier;
                    if (info.InflatedModifier >= .15f && RandomHelper.Percentage(popChance))
                        Game.TriggerExplosion(info.Player.GetWorldPosition());
                });
                Events.UpdateCallback.Start(e =>
                {
                    if (Game.IsEditorTest)
                    {
                        foreach (var info in HeliumInfos.Values)
                            Game.DrawText(ScriptHelper.ToDisplayString(info.InflatedModifier, info.Player.GetModifiers().SizeModifier),
                                info.Player.GetWorldPosition());
                    }
                    foreach (var info in HeliumInfos.Values) info.Update(e);
                });
            }
        }

        public class ArrowTrap : ThrowableBehaviour
        {

            public static ThrowableBehaviour Init()
            {
                return new ArrowTrap();
            }

            public override void Create()
            {
                //Remove C4 Detonator
                Throwable.OwnerPlayer.RemoveWeaponItemType(WeaponItemType.Thrown);
            }

            public override void Update()
            {
                //Steam Mask
                Game.PlayEffect("STM", Throwable.Object.GetWorldPosition() + new Vector2(rnd.Next(-20, 20), rnd.Next(-20, 20)));
            }

            public override void Destroy()
            {
                //Shoot 7 arrows
                for (int i = -3; i <= 3; i++)
                {
                    Vector2 Direction = new Vector2((float)Math.Cos(MathHelper.PIOver2 + (MathHelper.PI / 12) * i), (float)Math.Sin(MathHelper.PIOver2 + (MathHelper.PI / 6) * i));
                    Game.SpawnProjectile(ProjectileItem.BOW, Throwable.Object.GetWorldPosition(), Direction, ProjectilePowerup.Bouncing);
                }
                Throwable.Object.Remove();
            }
        }



















        /*
            Further Is The Base Script

            Event Callbacks
        */
        public static Events.PlayerWeaponAddedActionCallback e_PlayerWeaponAddedActionCallback = null;
        public static Events.PlayerWeaponRemovedActionCallback e_PlayerWeaponRemovedActionCallback = null;
        public static Events.ObjectCreatedCallback e_ObjectCreatedCallback = null;
        public static Events.UpdateCallback e_UpdateCallback = null;

        /* 
            Static Lists
        */

        // Player List
        public static List<gPlayer> PlayerList = new List<gPlayer>();

        // Drop / Pickup Items
        public static List<ThrownItem> ThrownItems = new List<ThrownItem>();

        // Current Thrown Objects
        public static List<Throwable> Throwables = new List<Throwable>();

        /*
            Common Global Vars
        */
        public static Random rnd = new Random();

        /*
            Base Running Functions
        */
        public static void OnStartup()
        {
            e_UpdateCallback = Events.UpdateCallback.Start(OnUpdate);
            e_ObjectCreatedCallback = Events.ObjectCreatedCallback.Start(OnObjectCreated);
            e_PlayerWeaponAddedActionCallback = Events.PlayerWeaponAddedActionCallback.Start(OnPlayerWeaponAddedAction);
            e_PlayerWeaponRemovedActionCallback = Events.PlayerWeaponRemovedActionCallback.Start(OnPlayerWeaponRemovedAction);
        }

        public static void OnUpdate(float time)
        {

            // Add new players to list
            foreach (IPlayer ply in Game.GetPlayers())
            {
                gPlayer Player = GetgPlayer(ply);

                if (Player == null)
                {
                    PlayerList.Add(new gPlayer(ply));
                }
            }

            // Remove dead players from list
            bool done;
            do
            {
                done = true;

                foreach (gPlayer ply in PlayerList)
                {

                    if (ply.Player == null || ply.Player.IsDead || ply.Player.IsRemoved)
                    {
                        ply.Remove();
                        done = false;
                        break;
                    }
                }
            } while (!done);

            // Update Thrown Custom Items
            foreach (Throwable tr in Throwables)
            {
                tr.Update();
            }

            // Remove done throwables from list
            do
            {
                done = true;

                foreach (Throwable tr in Throwables)
                {

                    if (tr.Destroyed)
                    {
                        tr.Remove();
                        done = false;
                        break;
                    }
                }
            } while (!done);
        }

        /*
            Callback functions
        */

        //**Note this is run before WeaponAddedCallback
        public static void OnPlayerWeaponRemovedAction(IPlayer player, PlayerWeaponRemovedArg arg)
        {

            if (arg.WeaponItemType == WeaponItemType.Thrown)
            {
                gPlayer Player = GetgPlayer(player);
                IObject ThrownObject = Game.GetObject(arg.TargetObjectID);

                if (Player != null && Player.Item != null)
                {

                    //Dropped Weapon			
                    if (arg.WeaponItem == Player.Item.WeaponItem && (ThrownObject is IObjectWeaponItem))
                    {
                        Player.Item.ObjectID = arg.TargetObjectID;
                        Player.Item = null;
                        return;
                    }

                    //Thrown Object
                    if (arg.WeaponItem == Player.Item.WeaponItem && !(ThrownObject is IObjectWeaponItem) && ThrownObject != null)
                    {
                        Player.Item.Ammo--;

                        // Has custom throwable?
                        if (Player.Item.Def != null)
                            Throwables.Add(new Throwable(ThrownObject, Player));

                        // Out of ammo?
                        if (Player.Item.Ammo <= 0)
                            Player.Item = null;

                        return;
                    }
                }
            }
        }

        public static void OnPlayerWeaponAddedAction(IPlayer player, PlayerWeaponAddedArg arg)
        {

            if (arg.WeaponItemType == WeaponItemType.Thrown)
            {
                gPlayer Player = GetgPlayer(player);
                ThrownItem Item = GetThrownItem(arg.SourceObjectID);

                if (Player != null)
                {

                    // Catch Setting Ammo Size
                    if (Player.IgnoreWeaponCycle <= 0)
                    {

                        if (Item == null)
                        {
                            Item = new ThrownItem(arg.WeaponItem, null);
                            ThrownItems.Add(Item);
                        }
                        Player.PickupItem(Item);

                        // For SetCurrentThrownItemAmmo					
                    }
                    else
                    {
                        Player.IgnoreWeaponCycle--;
                    }
                }
            }
        }

        /*
            Throwable Classes
        */
        public class ThrowableDefinition
        {
            public string Name = "";
            public string Author = "";
            public string Description = "";

            public WeaponItem HeldItem = WeaponItem.NONE;
            public string ObjectName = "";
            public int StartAmmo = 0;
            public int MaxAmmo = 0;
            public float DetonationTime = 0;

            public Func<ThrowableBehaviour> CreateBehaviour = null;

            public ThrowableDefinition(string _name, string _auth, string _desc, WeaponItem _item, string _objName, int _startAmmo, int _maxAmmo, int _detTime, Func<ThrowableBehaviour> _create)
            {
                Name = _name;
                Author = _auth;
                Description = _desc;

                HeldItem = _item;
                ObjectName = _objName;
                StartAmmo = _startAmmo;
                MaxAmmo = _maxAmmo;
                DetonationTime = _detTime;

                CreateBehaviour = _create;
            }
        }

        public static ThrowableDefinition GetThrowableDefinition(WeaponItem _item)
        {

            List<ThrowableDefinition> Getter = new List<ThrowableDefinition>();

            foreach (ThrowableDefinition def in ThrowableDefinitions)
            {

                if (def.HeldItem == _item)
                    Getter.Add(def);
            }

            if (Getter.Count == 0)
            {
                return null;

            }
            else
            {
                return Getter[rnd.Next(0, Getter.Count)];
            }
        }

        public static void ThrowableInfo(IUser user, ThrowableDefinition def)
        {

            //Stop Bots In SinglePlayer From Spamming Chat
            if (!user.IsBot && def != null)
            {

                //Name
                Game.ShowChatMessage(def.Name + ":", Color.White, user.UserIdentifier);

                //Author
                Game.ShowChatMessage("(" + def.Author + ")", user.UserIdentifier);

                //Description
                Game.ShowChatMessage(def.Description, Color.Grey, user.UserIdentifier);
            }
        }

        public class ThrowableBehaviour
        {
            public Throwable Throwable = null;

            public virtual void Create() { }
            public virtual void Update() { }
            public virtual void Destroy() { }

            public virtual bool IsRemoved()
            {
                return (Throwable.Object.RemovalInitiated || Throwable.Object.DestructionInitiated || Throwable.Object.IsRemoved || (Throwable.ElapsedTime >= Throwable.Def.DetonationTime && Throwable.Def.DetonationTime != -1));
            }
        }

        public class Throwable
        {
            public IObject Object = null;
            public IPlayer OwnerPlayer = null;

            private IObjectAlterCollisionTile DisableCollisions = null;
            public bool OwnerColliding = true;

            private ThrowableBehaviour Behaviour = null;
            public ThrowableDefinition Def = null;

            private float InitialTime = 0;
            public float ElapsedTime = 0;

            public bool Destroyed = false;

            public Throwable(IObject _obj, gPlayer _ply)
            {
                OwnerPlayer = _ply.Player;
                InitialTime = Game.TotalElapsedGameTime;
                Def = _ply.Item.Def;

                // Behaviour Creation
                if (Def.CreateBehaviour != null)
                    Behaviour = Def.CreateBehaviour();
                else
                    Behaviour = new ThrowableBehaviour();

                // Store Class
                Behaviour.Throwable = this;

                // Use custom object
                if (_obj.Name != _ply.Item.Def.ObjectName)
                {
                    Object = Game.CreateObject(_ply.Item.Def.ObjectName, _obj.GetWorldPosition(), _obj.GetAngle(), _obj.GetLinearVelocity(), _obj.GetAngularVelocity(), _obj.GetFaceDirection());
                    _obj.Remove();

                    // Use default object
                }
                else
                {
                    Object = _obj;
                }

                // Disable Collision Until Out of Player
                DisableCollisions = (IObjectAlterCollisionTile)Game.CreateObject("AlterCollisionTile");
                DisableCollisions.AddTargetObject(Object);
                DisableCollisions.AddTargetObject(OwnerPlayer);
                DisableCollisions.SetDisableCollisionTargetObjects(true);

                // Create Callback
                Behaviour.Create();
            }

            public void Update()
            {
                ElapsedTime = Game.TotalElapsedGameTime - InitialTime;

                if (Behaviour.IsRemoved())
                {
                    Destroy();
                    return;
                }

                // Wait till not colliding
                if (!Object.GetAABB().Intersects(OwnerPlayer.GetAABB()) && DisableCollisions != null)
                {
                    DisableCollisions.SetDisableCollisionTargetObjects(false);
                    DisableCollisions.Remove();
                    DisableCollisions = null;
                    OwnerColliding = false;
                    Object.TrackAsMissile(true);
                }

                // Update Callback
                Behaviour.Update();
            }

            public void Destroy()
            {

                // Destroy Callback
                Behaviour.Destroy();

                if (!Object.DestructionInitiated && !Object.RemovalInitiated && !Object.IsRemoved)
                    Object.Destroy();

                Destroyed = true;
            }

            public void Remove()
            {

                // Remove self from list
                for (int i = 0; i < Throwables.Count; i++)
                {

                    if (Throwables[i] == this)
                    {
                        Throwables.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /*
            Item Control
        */
        public class ThrownItem
        {
            public int ObjectID = -1;
            public WeaponItem WeaponItem = WeaponItem.NONE;
            public ThrowableDefinition Def = null;

            public int Ammo = 0;

            public ThrownItem(IObjectWeaponItem _obj, ThrowableDefinition _def, int _ammo = 3)
            {
                ObjectID = _obj.UniqueID;
                WeaponItem = _obj.WeaponItem;
                Def = _def;

                if (Def != null)
                {
                    Ammo = Def.StartAmmo;
                }
                else
                {
                    Ammo = _ammo;
                }
            }

            public ThrownItem(WeaponItem _item, ThrowableDefinition _def, int _ammo = 3)
            {
                WeaponItem = _item;
                Def = _def;

                if (Def != null)
                {
                    Ammo = Def.StartAmmo;
                }
                else
                {
                    Ammo = _ammo;
                }
            }
        }

        public static ThrownItem GetThrownItem(int objectID)
        {

            foreach (ThrownItem item in ThrownItems)
            {

                if (item.ObjectID == objectID)
                    return item;
            }
            return null;
        }

        /*
            Player Control
        */
        public class gPlayer
        {
            public IPlayer Player = null;
            public ThrownItem Item = null;
            public int IgnoreWeaponCycle = 0;

            public gPlayer(IPlayer _ply)
            {
                Player = _ply;
            }

            public void PickupItem(ThrownItem _item)
            {

                // Assign New Grenade
                if (Item != null)
                {

                    // Same def and object add ammo
                    if (_item.Def == Item.Def && _item.WeaponItem == Item.WeaponItem)
                    {
                        Item.Ammo += _item.Ammo;

                        if (Item.Def != null)
                        {

                            if (Item.Def.MaxAmmo < Item.Ammo)
                            {
                                Item.Ammo = Item.Def.MaxAmmo;
                            }
                        }

                        // Replacement
                    }
                    else
                    {

                        // Same Weapon but not same def
                        if (Item.WeaponItem == _item.WeaponItem)
                            DropItem();

                        Item = _item;

                        if (Player.GetUser() != null)
                            ThrowableInfo(Player.GetUser(), Item.Def);
                    }

                    // Replacement
                }
                else
                {
                    Item = _item;

                    if (Player.GetUser() != null)
                        ThrowableInfo(Player.GetUser(), Item.Def);
                }

                //If Increasing Ammo Will Call WeaponAddedCallback
                if (Player.CurrentThrownItem.CurrentAmmo <= Item.Ammo)
                    IgnoreWeaponCycle++;

                Player.SetCurrentThrownItemAmmo(Item.Ammo);
            }

            public void DropItem()
            {

                // Create New Item With Previous Item Properties
                if (Item != null)
                {
                    IObjectWeaponItem wpn = Game.SpawnWeaponItem(Item.WeaponItem, Player.GetWorldPosition(), true);
                    wpn.SetLinearVelocity(new Vector2(2 * Player.GetFaceDirection(), 2));

                    ThrownItem _item = new ThrownItem(wpn, Item.Def);
                    _item.Ammo = Item.Ammo;
                    ThrownItems.Add(_item);

                    Item = null;
                }
            }

            public void Remove()
            {

                for (int i = 0; i < PlayerList.Count; i++)
                {

                    if (PlayerList[i] == this)
                    {
                        PlayerList.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public static gPlayer GetgPlayer(IPlayer _ply)
        {

            foreach (gPlayer ply in PlayerList)
            {

                if (ply.Player == _ply)
                    return ply;
            }

            return null;
        }

        /*
            Custom Throwable Crate
        */
        public static void OnObjectCreated(IObject[] objects)
        {

            foreach (IObject obj in objects)
            {

                //Custom Grenade Crates Spawn
                if (obj is IObjectSupplyCrate)
                {
                    WeaponItem item = (obj as IObjectSupplyCrate).GetWeaponItem();

                    //Correct Category
                    if (item == WeaponItem.BOUNCINGAMMO ||
                        item == WeaponItem.FIREAMMO ||
                        item == WeaponItem.STRENGTHBOOST ||
                        item == WeaponItem.SPEEDBOOST ||
                        item == WeaponItem.SLOWMO_5 ||
                        item == WeaponItem.SLOWMO_10 ||
                        item == WeaponItem.C4 ||
                        item == WeaponItem.GRENADES ||
                        item == WeaponItem.SHURIKEN ||
                        item == WeaponItem.MOLOTOVS ||
                        item == WeaponItem.MINES ||
                        item == WeaponItem.PILLS ||
                        item == WeaponItem.MEDKIT
                    )
                    {

                        //Check drop chance
                        CreateGrenadeCrate(obj.GetWorldPosition());
                        obj.Remove();
                    }
                }
            }
        }

        public static void CreateGrenadeCrate(Vector2 pos)
        {
            //Crates CustomID
            string cID = "DISABLED" + Game.TotalElapsedGameTime;

            //Joint To Hold Custom Crate Together
            IObjectWeldJoint Welder = (IObjectWeldJoint)Game.CreateObject("WeldJoint", pos);

            //Invisible Block For Game World Collision
            IObject Grid = Game.CreateObject("InvisibleBlockNoCollision", pos + new Vector2(-4, 4));
            Grid.SetSizeFactor(new Point(2, 2));

            CollisionFilter col = new CollisionFilter();
            col.CategoryBits = 32;
            col.MaskBits = 3;
            col.AboveBits = 0;
            col.BlockExplosions = false;
            col.BlockFire = false;
            col.BlockMelee = false;
            col.ProjectileHit = false;
            Grid.SetCollisionFilter(col);

            //Background Crate Image
            IObject BG = Game.CreateObject("BgFurnace00A", pos);

            //Disable Collision Tile With Crate Objects
            IObjectAlterCollisionTile Alter = (IObjectAlterCollisionTile)Game.CreateObject("AlterCollisionTile", pos);
            Alter.SetDisablePlayerMelee(true);
            Alter.SetDisableProjectileHit(true);
            Alter.SetDisabledCategoryBits(0xFFFF);
            Alter.SetDisabledMaskBits(0xFFFF);
            Alter.SetDisabledAboveBits(0xFFFF);

            //Crate Formation using Turret Crate Pieces
            for (int i = 0; i < 4; i++)
            {
                Vector2 Shift = Vector2.Zero;
                Shift.X = (float)Math.Cos((MathHelper.PI / 2) * i + MathHelper.PI / 4) * 5.65f;
                Shift.Y = (float)Math.Sin((MathHelper.PI / 2) * i + MathHelper.PI / 4) * 5.65f;

                IObject Box = Game.CreateObject("StreetsweeperCratePart", pos + Shift, MathHelper.PI / 2 * i - MathHelper.PI / 2);
                Box.SetBodyType(BodyType.Static);
                Box.CustomID = cID;

                Welder.AddTargetObject(Box);
                Alter.AddTargetObject(Box);
            }

            //Button Players Interact With To Recieve Custom Ammunition
            IObjectActivateTrigger Button = (IObjectActivateTrigger)Game.CreateObject("ActivateTrigger", pos);
            Button.SetHighlightObject(Grid);
            Button.SetScriptMethod("PickupCustomThrowable");

            //Add Crate Pieces To Main Weld
            Welder.AddTargetObject(Button);
            Welder.AddTargetObject(Grid);
            Welder.AddTargetObject(BG);
            Welder.AddTargetObject(Alter);

            //Set CustomID's of Crate Pieces
            Button.CustomID = cID;
            Grid.CustomID = cID;
            BG.CustomID = cID;
            Welder.CustomID = cID;
            Alter.CustomID = cID;

            //Wait a few Secs for Game To Load In Objects Correctly
            CreateTimer(500, 1, "ValidateThrownCrate", cID);
        }

        public static void PickupCustomThrowable(TriggerArgs args)
        {

            if (args.Sender is IPlayer)
            {
                gPlayer Player = GetgPlayer(args.Sender as IPlayer);

                if (Player != null && args.Caller is IObject)
                {

                    //Create Throwable Item
                    ThrowableDefinition Def = ThrowableDefinitions[rnd.Next(0, ThrowableDefinitions.Count)];
                    ThrownItem Item = new ThrownItem(Def.HeldItem, Def);
                    ThrownItems.Add(Item);

                    //Give Throwable
                    if (Item.WeaponItem != Player.Player.CurrentThrownItem.WeaponItem)
                    {
                        Player.IgnoreWeaponCycle++;
                        Player.Item = null;
                        Player.Player.GiveWeaponItem(Item.WeaponItem);

                    }
                    else
                    {
                        Player.DropItem();
                    }
                    Player.PickupItem(Item);

                    //Remove Crate Objects		
                    IObject Button = args.Caller as IObject;
                    foreach (IObject obj in Game.GetObjectsByCustomID(Button.CustomID))
                    {
                        obj.Destroy();
                    }
                }
            }
        }

        public static void ValidateThrownCrate(TriggerArgs args)
        {

            //Only ObjectTriggers Will Use This Function
            if (args.Caller is IObject)
            {
                string cID = (args.Caller as IObject).CustomID;

                //Destroy Unused Timer
                (args.Caller as IObject).Remove();

                //Set Each Create Object As Dynamic
                foreach (IObject obj in Game.GetObjectsByCustomID(cID))
                {
                    obj.SetBodyType(BodyType.Dynamic);
                }
            }
        }

        /*
            Common Helper Functions
        */
        public static void CreateTimer(int interval, int count, string method, string id)
        {
            IObjectTimerTrigger timerTrigger = (IObjectTimerTrigger)Game.CreateObject("TimerTrigger");
            timerTrigger.SetIntervalTime(interval);
            timerTrigger.SetRepeatCount(count);
            timerTrigger.SetScriptMethod(method);
            timerTrigger.CustomId = id;
            timerTrigger.Trigger();
        }
    }
}
