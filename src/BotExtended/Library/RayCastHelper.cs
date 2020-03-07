using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Library
{
    static class RayCastHelper
    {
        // List of objects that bullet cannot pass initially, but can be broken down
        public static readonly HashSet<string> ObjectsBulletCanDestroy = new HashSet<string>()
        {
            "ReinforcedGlass00A",
            "AtlasStatue00",
            "BulletproofGlass00Weak",
            "StoneWeak00A",
            "StoneWeak00B",
            "StoneWeak00C",
            "Concrete01Weak",
            "Wood06Weak",
            "StreetsweeperCrate"
        };

        // List of objects that bullet cant pass (edge cases)
        // https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3952&p=23291#p23291
        public static readonly HashSet<string> ObjectsBulletCantPass = new HashSet<string>()
        {
            "DinerBooth",
        };

        private static bool BlockProjectile(RayCastResult result)
        {
            return ObjectsBulletCantPass.Contains(result.HitObject.Name)
                    // Filter objects bullet can passthrough like ladder
                    // Not an optimal solution: https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3952&p=23291#p23291
                    || (result.HitObject.GetCollisionFilter().BlockExplosions
                    && !ObjectsBulletCanDestroy.Contains(result.HitObject.Name));
        }

        public static IEnumerable<RayCastResult> ImpassableObjects(Vector2 start, Vector2 end)
        {
            var rayCastInput = new RayCastInput()
            {
                // static_ground, dynamic_platforms
                MaskBits = 0x000B,
                FilterOnMaskBits = true,
            };
            var results = Game.RayCast(start, end, rayCastInput);

            foreach (var result in results)
            {
                if (BlockProjectile(result))
                    yield return result;
            }
        }

        public static bool SameTeamRaycast(IPlayer p1, IPlayer p2, PlayerTeam t1)
        {
            // t1 is cached before p1 is removed
            if (p1.IsRemoved)
            {
                var t2 = p2.GetTeam();
                return t2 == PlayerTeam.Independent ? false : t1 == t2;
            }
            return ScriptHelper.SameTeam(p1, p2);
        }

        /// <summary>
        /// Find players that touch the line. filter players behind block objects (wall, ground...)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<RayCastResult> PlayersInSight(Vector2 start, Vector2 end,
            bool blockTeammates = false, PlayerTeam team = PlayerTeam.Independent, IPlayer fromPlayer = null)
        {
            var rayCastInput = new RayCastInput()
            {
                // How to customize filter
                // Open with notepad ..\Superfighters Deluxe\Content\Data\Tiles\CollisionGroups\collisionGroups.sfdx
                // Search for categoryBits for the object types you want to accept for collision
                // Calc sum of those values (in binary) and convert to hex
                // 
                // static_ground, players, dynamic_platforms
                MaskBits = 0x000F,
                FilterOnMaskBits = true,
                //Types = new Type[1] { typeof(IPlayer) },
            };
            var results = Game.RayCast(start, end, rayCastInput);
            var smallestBlockedFraction = float.PositiveInfinity;
            var smallestTeammateFraction = float.PositiveInfinity;
            int closestBlockObjectID = int.MinValue;
            int closestTeammateID = int.MinValue;
            var playerResult = new List<RayCastResult>();

            foreach (var result in results)
            {
                if (BlockProjectile(result))
                {
                    if (smallestBlockedFraction > result.Fraction)
                    {
                        smallestBlockedFraction = result.Fraction;
                        closestBlockObjectID = result.ObjectID;
                    }
                }
                if (result.IsPlayer) playerResult.Add(result);
                if (result.IsPlayer && blockTeammates)
                {
                    var player = Game.GetPlayer(result.ObjectID);

                    if (SameTeamRaycast(fromPlayer, player, team))
                    {
                        if (smallestTeammateFraction > result.Fraction)
                        {
                            smallestTeammateFraction = result.Fraction;
                            closestTeammateID = result.ObjectID;
                        }
                    }
                }
            }

            //Game.DrawLine(start, end);
            if (closestBlockObjectID != int.MinValue)
                Game.DrawArea(Game.GetObject(closestBlockObjectID).GetAABB(), Color.Yellow);
            if (closestTeammateID != int.MinValue)
                Game.DrawArea(Game.GetPlayer(closestTeammateID).GetAABB(), Color.Red);

            foreach (var result in playerResult)
            {
                var player = Game.GetPlayer(result.ObjectID);
                var blocked = false;

                if (blockTeammates)
                {
                    if (SameTeamRaycast(fromPlayer, player, team))
                        continue;
                    if (smallestTeammateFraction <= result.Fraction)
                        blocked = true;
                }
                if (smallestBlockedFraction < result.Fraction)
                    blocked = true;

                if (!blocked)
                {
                    Game.DrawArea(player.GetAABB(), Color.Green);
                    yield return result;
                }
            }
        }
    }
}
