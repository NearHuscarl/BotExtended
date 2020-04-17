using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class KriegbarBot_Controller : Controller<KriegbärBot>
    {
        private int m_oldHoldingPlayerInGrabID = 0;
        private float m_checkCorpseDelay = 0f;
        private float m_cooldownTime = 0f;
        private enum State { Normal, Cooldown }
        private State m_state = State.Normal;

        public override void OnUpdate(float elapsed)
        {
            if (Player.HoldingPlayerInGrabID != 0 && m_oldHoldingPlayerInGrabID == 0)
            {
                m_oldHoldingPlayerInGrabID = Player.HoldingPlayerInGrabID;
                OnGrabbingPlayer();
            }
            if (Player.HoldingPlayerInGrabID == 0 && m_oldHoldingPlayerInGrabID != 0)
            {
                m_oldHoldingPlayerInGrabID = Player.HoldingPlayerInGrabID;
            }

            switch (m_state)
            {
                case State.Normal:
                    if (ScriptHelper.IsElapsed(m_checkCorpseDelay, 125))
                    {
                        m_checkCorpseDelay = Game.TotalElapsedGameTime;
                        if (!PlayersNearby() && Actor.GetThrowTargets().Any())
                        {
                            var corpseNearby = CorpsesNearby().FirstOrDefault();

                            if (corpseNearby != null)
                            {
                                ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.StartCrouch, 0);
                                ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.Grab, 750);
                                m_state = State.Cooldown;
                                m_cooldownTime = Game.TotalElapsedGameTime;
                            }
                        }
                    }
                break;
                case State.Cooldown:
                    if (ScriptHelper.IsElapsed(m_cooldownTime, 3000))
                        m_state = State.Normal;
                    break;
            }
        }

        private bool PlayersNearby()
        {
            Game.DrawArea(ScriptHelper.GrowFromCenter(Actor.Position, 50, 30), Color.Red);
            return Game.GetPlayers()
                .Where(p => !p.IsDead
                && !ScriptHelper.SameTeam(p, Player)
                && ScriptHelper.GrowFromCenter(Actor.Position, 50, 30).Intersects(p.GetAABB())).Any();
        }

        private IEnumerable<IPlayer> CorpsesNearby()
        {
            var area = Player.GetAABB();
            var center = area.Center;
            if (Player.FacingDirection > 0)
            {
                area.Left += 5;
                area.Right += 20;
            }
            if (Player.FacingDirection < 0)
            {
                area.Right -= 5;
                area.Left -= 20;
            }
            Game.DrawArea(area, Color.Green);
            return Game.GetPlayers().Where(p => p.IsDead && p.IsLayingOnGround
                && area.Intersects(p.GetAABB()) && Vector2.Distance(center, p.GetAABB().Center) > 10);
        }

        private void OnGrabbingPlayer()
        {
            var targets = Actor.GetThrowTargets(Player.HoldingPlayerInGrabID);

            if (!targets.Any())
            {
                ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.FaceAt, 10,
                    (PlayerCommandFaceDirection)(Player.FacingDirection * -1));
            }
        }
    }
}
