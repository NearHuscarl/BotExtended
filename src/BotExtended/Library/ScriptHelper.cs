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

        private static string GetDefaultPlaceholder(int count)
        {
            var placeholder = "";
            for (var i = 0; i < count; i++)
            {
                placeholder += "{" + i + "}";
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
                    placeholder = GetDefaultPlaceholder(values.Length);
                }
                Game.WriteToConsole(string.Format(placeholder, values));
            }
        }

        public static void LogDebug(params object[] values)
        {
            if (Game.IsEditorTest)
            {
                Game.WriteToConsole(string.Format(GetDefaultPlaceholder(values.Length), values));
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

        public static string ObjectID(WeaponItem weaponItem, bool activated = false)
        {
            switch (weaponItem)
            {
                case WeaponItem.ASSAULT:
                    return "WpnAssaultRifle";
                case WeaponItem.BAZOOKA:
                    return "WpnBazooka";
                case WeaponItem.BOW:
                    return "WpnBow";
                case WeaponItem.CARBINE:
                    return "WpnCarbine";
                case WeaponItem.DARK_SHOTGUN:
                    return "WpnDarkShotgun";
                case WeaponItem.FLAREGUN:
                    return "WpnFlareGun";
                case WeaponItem.FLAMETHROWER:
                    return "WpnFlamethrower";
                case WeaponItem.GRENADE_LAUNCHER:
                    return "WpnGrenadeLauncher";
                case WeaponItem.M60:
                    return "WpnM60";
                case WeaponItem.MACHINE_PISTOL:
                    return "WpnMachinePistol";
                case WeaponItem.MAGNUM:
                    return "WpnMagnum";
                case WeaponItem.MP50:
                    return "WpnMP50";
                case WeaponItem.PISTOL:
                    return "WpnPistol";
                case WeaponItem.PISTOL45:
                    return "WpnPistol45";
                case WeaponItem.REVOLVER:
                    return "WpnRevolver";
                case WeaponItem.SAWED_OFF:
                    return "WpnSawedoff";
                case WeaponItem.SHOTGUN:
                    return "WpnPumpShotgun";
                case WeaponItem.SILENCEDPISTOL:
                    return "WpnSilencedPistol";
                case WeaponItem.SILENCEDUZI:
                    return "WpnSilencedUzi";
                case WeaponItem.SMG:
                    return "WpnSMG";
                case WeaponItem.SNIPER:
                    return "WpnSniperRifle";
                case WeaponItem.TOMMYGUN:
                    return "WpnTommygun";
                case WeaponItem.UZI:
                    return "WpnUzi";
                case WeaponItem.PIPE:
                    return "WpnPipeWrench";
                case WeaponItem.CHAIN:
                    return "WpnChain";
                case WeaponItem.WHIP:
                    return "WpnWhip";
                case WeaponItem.HAMMER:
                    return "WpnHammer";
                case WeaponItem.KATANA:
                    return "WpnKatana";
                case WeaponItem.MACHETE:
                    return "WpnMachete";
                case WeaponItem.CHAINSAW:
                    return "WpnChainsaw";
                case WeaponItem.KNIFE:
                    return "WpnKnife";
                case WeaponItem.BAT:
                    return "WpnBat";
                case WeaponItem.BATON:
                    return "WpnBaton";
                case WeaponItem.SHOCK_BATON:
                    return "WpnShockBaton";
                case WeaponItem.LEAD_PIPE:
                    return "WpnLeadPipe";
                case WeaponItem.AXE:
                    return "WpnAxe";
                case WeaponItem.GRENADES:
                    return activated ? "WpnGrenadesThrown" : "WpnGrenades";
                case WeaponItem.MOLOTOVS:
                    return activated ? "WpnMolotovsThrown" : "WpnMolotovs";
                case WeaponItem.MINES:
                    return activated ? "WpnMineThrown" : "WpnMines";
                case WeaponItem.C4:
                    return activated ? "WpnC4Thrown" : "WpnC4";
                case WeaponItem.SHURIKEN:
                    return "WpnShuriken";
                case WeaponItem.BASEBALL:
                    return "Baseball";
                case WeaponItem.BOTTLE:
                    return "Bottle00";
                case WeaponItem.BROKEN_BOTTLE:
                    return "Bottle00Broken";
                case WeaponItem.CHAIR:
                    return "Chair00";
                case WeaponItem.CUESTICK:
                    return "CueStick00";
                case WeaponItem.CUESTICK_SHAFT:
                    return "CueStick00Shaft";
                case WeaponItem.FLAGPOLE:
                    return "FlagpoleUS";
                case WeaponItem.PILLOW:
                    return "Pillow00";
                case WeaponItem.SUITCASE:
                    return "Suitcase00";
                case WeaponItem.TEAPOT:
                    return "Teapot00";
                case WeaponItem.TRASH_BAG:
                    return "Trashbag00";
                case WeaponItem.TRASHCAN_LID:
                    return "Trashcan00Lid";
                case WeaponItem.CHAIR_LEG:
                    return "ChairLeg";
                default:
                    return "";
            }
        }

        public static bool IntersectCircle(Vector2 position, Vector2 center, float radius, float minAngle = 0, float maxAngle = MathHelper.TwoPI)
        {
            var fullCircle = minAngle == 0 && maxAngle == MathHelper.TwoPI;

            var distanceToCenter = Vector2.Distance(position, center);

            if (distanceToCenter <= radius)
            {
                if (!fullCircle)
                {
                    var angle = MathExtension.NormalizeAngle(GetAngle(position - center));

                    if (angle >= minAngle && angle <= maxAngle)
                        return true;
                }
                else
                    return true;
            }

            return false;
        }

        public static bool IntersectCircle(Area area, Vector2 center, float radius, float minAngle = 0, float maxAngle = MathHelper.TwoPI)
        {
            var fullCircle = minAngle == 0 && maxAngle == MathHelper.TwoPI;
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

                        if (angle >= minAngle && angle <= maxAngle)
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
    }
}
