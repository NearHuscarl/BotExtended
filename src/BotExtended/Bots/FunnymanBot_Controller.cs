using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class FunnymanBot_Controller : Controller<FunnymanBot>
    {
        private Area SafeArea(Vector2 spawnPosition) { return ScriptHelper.GrowFromCenter(spawnPosition, 60, 30); }
        private IObject m_targetLocation = null;

        enum State
        {
            Normal,
            Fleeing,
        }
        private State m_state;
        private float m_stateDelay = 0f;
        private float m_totalDamage = 0f;

        public override void OnUpdate(float elapsed)
        {
            switch (m_state)
            {
                case State.Normal:
                {
                    if (ScriptHelper.IsElapsed(m_stateDelay, 2000))
                    {
                        if (Player.Statistics.TotalDamageTaken - m_totalDamage >= (Game.IsEditorTest ? 1 : 30))
                        {
                            TryToFlee();
                        }

                        m_stateDelay = Game.TotalElapsedGameTime;
                        m_totalDamage = Player.Statistics.TotalDamageTaken;
                    }
                    break;
                }
                case State.Fleeing:
                {
                    if (!ScriptHelper.IsElapsed(m_stateDelay, 30))
                        break;
                    m_stateDelay = Game.TotalElapsedGameTime;

                    if (EnemiesInArea(SafeArea(m_targetLocation.GetWorldPosition())))
                    {
                        TryToFlee(); // search for a new location
                        break;
                    }

                    if (Player.GetAABB().Intersects(m_targetLocation.GetAABB()))
                    {
                        if (EnemiesInArea(DangerArea))
                            TryToFlee();
                        else
                            StopFleeing();
                    }
                    break;
                }
            }

            Actor.LogDebug(m_state, Player.Statistics.TotalDamageTaken - m_totalDamage);
            if (m_targetLocation != null)
            {
                Game.DrawArea(m_targetLocation.GetAABB(), Color.Green);
                //Game.DrawArea(SafeArea(m_targetLocation.GetWorldPosition()), Color.Red);
            }
            Game.DrawArea(DangerArea, Color.Cyan);
        }

        private void ChangeState(State state)
        {
            m_state = state;
            m_stateDelay = Game.TotalElapsedGameTime;
        }

        private static readonly PlayerModifiers RunningModifiers;
        static FunnymanBot_Controller()
        {
            RunningModifiers = GetInfo(BotType.Funnyman).Modifiers;
            RunningModifiers.RunSpeedModifier = Speed.Fast;
            RunningModifiers.SprintSpeedModifier = Speed.Fast;
            RunningModifiers.EnergyConsumptionModifier = 0;
        }

        private Area DangerArea
        {
            get { return ScriptHelper.GrowFromCenter(Player.GetAABB().Center, 50, 30); }
        }

        private bool EnemiesInArea(Area area)
        {
            foreach (var player in Game.GetPlayers())
            {
                if (!ScriptHelper.SameTeam(Player, player))
                {
                    if (area.Intersects(player.GetAABB()))
                        return true;
                }
            }
            return false;
        }

        private static int removeme = 0;
        private void TryToFlee()
        {
            m_targetLocation = null;

            foreach (var spawner in Game.GetObjectsByName("SpawnPlayer"))
            {
                var safeArea = SafeArea(spawner.GetWorldPosition());
                var enemiesNearSpawner = Game.GetObjectsByArea<IPlayer>(safeArea).Any(p => !ScriptHelper.SameTeam(Player, p));

                if (!enemiesNearSpawner)
                {
                    var pathNode = Game.GetSingleObjectByArea<IObjectPathNode>(spawner.GetAABB(), PhysicsLayer.Background);
                    if (pathNode == null) continue;

                    Game.DrawArea(safeArea, Color.Magenta);
                    if (m_targetLocation == null)
                        m_targetLocation = pathNode;
                    else if (m_targetLocation != null && Vector2.DistanceSquared(m_targetLocation.GetWorldPosition(), Actor.Position)
                        > Vector2.DistanceSquared(pathNode.GetWorldPosition(), Actor.Position))
                        m_targetLocation = pathNode;
                }
            }

            if (m_targetLocation != null)
            {
                var runningBehavior = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.FunnymanRunning);

                runningBehavior.GuardRange = 1f;
                runningBehavior.ChaseRange = 1f;
                Player.SetBotBehaviorSet(runningBehavior);
                Actor.SetModifiers(RunningModifiers);
                Player.SetGuardTarget(m_targetLocation);
                ChangeState(State.Fleeing);
            }
        }

        private void StopFleeing()
        {
            Actor.ResetBotBehaviorSet();
            Actor.ResetModifiers();
            Player.SetGuardTarget(null);
            m_targetLocation = null;
            ChangeState(State.Normal);
        }
    }
}
