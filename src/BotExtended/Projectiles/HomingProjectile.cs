﻿using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    /// <summary>
    /// Credit to DangerRoss for the original idea and most of the code here
    /// </summary>
    class HomingProjectile : Projectile
    {
        public readonly PlayerTeam Team;
        public IPlayer Target { get; private set; }

        public HomingProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Homing)
        {
            // in case the original player is not available when the projectile hits
            Team = Game.GetPlayer(Instance.InitialOwnerPlayerID).GetTeam();
            //if (Game.IsEditorTest) Instance.Velocity /= 20;
        }

        private float m_updateDelay = 0f;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (ScriptHelper.IsElapsed(m_updateDelay, 75))
            {
                m_updateDelay = Game.TotalElapsedGameTime;
                SearchTarget();
            }
            if (Target != null) Game.DrawArea(Target.GetAABB());

            Guide();
        }

        private void Guide()
        {
            if (Target == null) return;

            var projectileAngle = ScriptHelper.GetAngle(Instance.Direction);
            var targetAngle = ScriptHelper.GetAngle(Target.GetWorldPosition() - Instance.Position);
            var steerAngle = Math.Sign(MathExtension.DiffAngle(targetAngle, projectileAngle)) * MathExtension.OneDeg * 10;

            Instance.Direction = ScriptHelper.GetDirection(projectileAngle + steerAngle);
        }

        private void SearchTarget()
        {
            var minDistanceToPlayer = float.MaxValue;

            foreach (var player in Game.GetPlayers())
            {
                var playerTeam = player.GetTeam();

                if (Team == playerTeam && Team != PlayerTeam.Independent
                    || Instance.InitialOwnerPlayerID == player.UniqueID
                    || player.IsDead)
                    continue;

                var distanceToPlayer = Vector2.Distance(Instance.Position, player.GetWorldPosition());
                if (minDistanceToPlayer > distanceToPlayer)
                {
                    minDistanceToPlayer = distanceToPlayer;
                    Target = player;
                }
            }
        }
    }
}