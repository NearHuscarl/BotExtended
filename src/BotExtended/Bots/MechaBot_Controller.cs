using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    public class MechaBot_Controller : Controller<MechaBot>
    {
        private static readonly float ChargeMinimumRange = 30f;
        private static readonly float ChargeRange = 60;

        public override void OnUpdate(float elapsed)
        {
            if (Actor.CanSuperCharge())
            {
                if (ShouldSuperCharge())
                {
                    Actor.ExecuteSupercharge();
                }
            }

            DrawDebugging();
        }

        private Vector2[] GetLineOfSight()
        {
            var lineStart = Actor.Position + Vector2.UnitY * 12f;

            return new Vector2[]
            {
                lineStart,
                lineStart + Player.AimVector * (ChargeMinimumRange + ChargeRange),
            };
        }

        private bool ShouldSuperCharge()
        {
            return (Player.IsSprinting || Player.IsIdle || Player.IsWalking || Player.IsRunning)
                && HasTargetToCharge();
        }

        private bool HasTargetToCharge()
        {
            var los = GetLineOfSight();
            var lineStart = los[0];
            var lineEnd = los[1];

            foreach (var result in RayCastHelper.Players(lineStart, lineEnd))
            {
                var player = Game.GetPlayer(result.ObjectID);
                var inMinimumRange = ScriptHelper.IntersectCircle(
                    player.GetAABB(),
                    Actor.Position,
                    ChargeMinimumRange);

                if (!inMinimumRange && !player.IsDead && !player.IsInMidAir && !ScriptHelper.SameTeam(player, Player))
                {
                    return true;
                }
            }

            return false;
        }

        private void DrawDebugging()
        {
            if (!Game.IsEditorTest) return;
            var los = GetLineOfSight();

            Game.DrawCircle(Actor.Position, ChargeMinimumRange, Color.Red);
            Game.DrawCircle(Actor.Position, MechaBot.ChargeHitRange, Color.Cyan);
            if (Actor.CanSuperCharge())
            {
                Game.DrawLine(los[0], los[1], Color.Green);
            }
            else
                Game.DrawLine(los[0], los[1], Color.Red);
        }
    }
}
