﻿using System;
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

        public static void LogDebug(string message) { if (Game.IsEditorTest) Game.WriteToConsole(message); }

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

        public static WeaponItem GetWeaponItem(ProjectileItem projectileItem)
        {
            switch (projectileItem)
            {
                case ProjectileItem.ASSAULT:
                    return WeaponItem.ASSAULT;
                case ProjectileItem.BAZOOKA:
                    return WeaponItem.BAZOOKA;
                case ProjectileItem.BOW:
                    return WeaponItem.BOW;
                case ProjectileItem.CARBINE:
                    return WeaponItem.CARBINE;
                case ProjectileItem.DARK_SHOTGUN:
                    return WeaponItem.DARK_SHOTGUN;
                case ProjectileItem.FLAKCANNON:
                    return WeaponItem.NONE;
                case ProjectileItem.FLAREGUN:
                    return WeaponItem.FLAREGUN;
                case ProjectileItem.GRENADE_LAUNCHER:
                    return WeaponItem.GRENADE_LAUNCHER;
                case ProjectileItem.M60:
                    return WeaponItem.M60;
                case ProjectileItem.MACHINE_PISTOL:
                    return WeaponItem.MACHINE_PISTOL;
                case ProjectileItem.MAGNUM:
                    return WeaponItem.MAGNUM;
                case ProjectileItem.MP50:
                    return WeaponItem.MP50;
                case ProjectileItem.PISTOL:
                    return WeaponItem.PISTOL;
                case ProjectileItem.PISTOL45:
                    return WeaponItem.PISTOL45;
                case ProjectileItem.REVOLVER:
                    return WeaponItem.REVOLVER;
                case ProjectileItem.SAWED_OFF:
                    return WeaponItem.SAWED_OFF;
                case ProjectileItem.SHOTGUN:
                    return WeaponItem.SHOTGUN;
                case ProjectileItem.SILENCEDPISTOL:
                    return WeaponItem.SILENCEDPISTOL;
                case ProjectileItem.SILENCEDUZI:
                    return WeaponItem.SILENCEDUZI;
                case ProjectileItem.SMG:
                    return WeaponItem.SMG;
                case ProjectileItem.SNIPER:
                    return WeaponItem.SNIPER;
                case ProjectileItem.TOMMYGUN:
                    return WeaponItem.TOMMYGUN;
                case ProjectileItem.UZI:
                    return WeaponItem.UZI;
                default:
                    return WeaponItem.NONE;
            }
        }

        public static Skin GetSkin(IPlayer player)
        {
            var skinName = player.GetProfile().Skin.Name;

            switch(skinName)
            {
                case "Normal":
                    return Skin.NormalMale;
                case "Normal_fem":
                    return Skin.NormalFemale;
                case "Tattoos":
                    return Skin.TatoosMale;
                case "Tattoos_fem":
                    return Skin.TatoosFemale;
                case "BearSkin":
                    return Skin.Bear;
                case "FrankenbearSkin":
                    return Skin.Frankenbear;
                case "MechSkin":
                    return Skin.Mech;
                case "Warpaint":
                    return Skin.WarpaintMale;
                case "Warpaint_fem":
                    return Skin.WarpaintFemale;
                case "Zombie":
                    return Skin.ZombieMale;
                case "Zombie_fem":
                    return Skin.ZombieFemale;
                default:
                    return Skin.None;
            }
        }

        public static bool IsTouchingCircle(Area area, Vector2 center, float radius)
        {
            var lines = new List<Vector2[]>()
            {
                new Vector2[] { area.BottomRight, area.BottomLeft },
                new Vector2[] { area.BottomLeft, area.TopLeft },
                new Vector2[] { area.TopLeft, area.TopRight },
                new Vector2[] { area.TopRight, area.BottomRight },
            };

            var minDistanceToCenter = float.MaxValue;

            foreach (var line in lines)
            {
                var distanceToCenter = FindDistanceToSegment(center, line[0], line[1]);
                if (distanceToCenter < minDistanceToCenter) minDistanceToCenter = distanceToCenter;
            }

            return minDistanceToCenter <= radius;
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

        // take into account PlayerModifiers.SizeModifier. Not 100% accurate
        public static Area GetAABB(IPlayer player)
        {
            var aabb = player.GetAABB();
            var sizeModifier = player.GetModifiers().SizeModifier;
            var newWidth = aabb.Width * sizeModifier;
            var newHeight = aabb.Height * sizeModifier;

            aabb.Left -= (newWidth - aabb.Width) / 2;
            aabb.Right += (newWidth - aabb.Width) / 2;
            aabb.Top += (newHeight - aabb.Height) / 2;
            aabb.Bottom -= (newHeight - aabb.Height) / 2;

            return aabb;
        }
    }
}
