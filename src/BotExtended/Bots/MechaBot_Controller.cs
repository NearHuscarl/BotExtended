using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    public class MechaBot_Controller : IController<MechaBot>
    {
        private static readonly float ChargeMinimumRange = 30f;
        private static readonly float ChargeRange = 60;

        public MechaBot Actor { get; set; }

        public MechaBot_Controller() { }

        public void OnUpdate(float elapsed)
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
                lineStart + Actor.Player.AimVector * (ChargeMinimumRange + ChargeRange),
            };
        }

        private bool ShouldSuperCharge()
        {
            var player = Actor.Player;
            return (player.IsSprinting || player.IsIdle || player.IsWalking || player.IsRunning)
                && HasTargetToCharge();
        }

        private bool HasTargetToCharge()
        {
            var los = GetLineOfSight();
            var lineStart = los[0];
            var lineEnd = los[1];

            foreach (var result in RayCastHelper.PlayersInSight(lineStart, lineEnd))
            {
                var player = Game.GetPlayer(result.ObjectID);
                var inMinimumRange = ScriptHelper.IntersectCircle(
                    player.GetAABB(),
                    Actor.Position,
                    ChargeMinimumRange);

                if (!inMinimumRange && !player.IsDead && !player.IsInMidAir && !ScriptHelper.SameTeam(player, Actor.Player))
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
