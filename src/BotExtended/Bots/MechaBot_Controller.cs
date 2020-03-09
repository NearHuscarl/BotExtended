using BotExtended.Library;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    public class MechaBot_Controller : IController<MechaBot>
    {
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
        }

        private bool ShouldSuperCharge()
        {
            var player = Actor.Player;
            return (player.IsSprinting || player.IsIdle || player.IsWalking || player.IsRunning)
                && HasTargetToCharge();
        }

        private bool HasTargetToCharge()
        {
            var los = Actor.GetLineOfSight();
            var lineStart = los[0];
            var lineEnd = los[1];

            foreach (var result in RayCastHelper.PlayersInSight(lineStart, lineEnd))
            {
                var player = Game.GetPlayer(result.ObjectID);
                var inMinimumRange = ScriptHelper.IntersectCircle(
                    player.GetAABB(),
                    Actor.Position,
                    MechaBot.ChargeMinimumRange);

                if (!inMinimumRange && !player.IsDead && !player.IsInMidAir && player.GetTeam() != Actor.Player.GetTeam())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
