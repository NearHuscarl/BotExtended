﻿using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class HuntingProjectile : Projectile
    {
        private IObjectDistanceJoint DistanceJoint { get; set; }
        private IObjectTargetObjectJoint TargetJoint { get; set; }
        private IObject InvisibleBlock { get; set; }
        public IPlayer Target { get; private set; }
     
        public HuntingProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Hunting)
        {
            Instance.DamageDealtModifier = .5f;
            Instance.Velocity /= 2;
        }

        private static readonly float MinRange = 60;
        private static readonly float MaxRange = 120;

        private float _updateDelay = 0f;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Instance.IsRemoved) return;

            if (DistanceJoint != null)
                InvisibleBlock.SetWorldPosition(Instance.Position);

            if (Target != null) Game.DrawArea(Target.GetAABB());
            if (Target == null && ScriptHelper.IsElapsed(_updateDelay, 35))
            {
                _updateDelay = Game.TotalElapsedGameTime;
                SearchTarget();
            }
        }

        private void SearchTarget()
        {
            foreach (var player in Game.GetPlayers())
            {
                if (player.IsDead || player.UniqueID == InitialOwnerPlayerID || ScriptHelper.SameTeam(Game.GetPlayer(InitialOwnerPlayerID), player))
                    continue;

                var distanceToPlayer = Vector2.Distance(Instance.Position, player.GetWorldPosition());
                if (distanceToPlayer >= MinRange && distanceToPlayer <= MaxRange && IsMovingAwayFrom(player))
                {
                    Target = player;
                    DistanceJoint = (IObjectDistanceJoint)Game.CreateObject("DistanceJoint", Instance.Position);
                    TargetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint", player.GetWorldPosition());
                    InvisibleBlock = Game.CreateObject("InvisibleBlockNoCollision", Instance.Position);

                    DistanceJoint.SetTargetObject(InvisibleBlock);
                    DistanceJoint.SetTargetObjectJoint(TargetJoint);
                    DistanceJoint.SetLineVisual(LineVisual.DJRope);
                    DistanceJoint.SetLengthType(DistanceJointLengthType.Elastic);
                    TargetJoint.SetTargetObject(player);

                    var cb = Events.UpdateCallback.Start((e) =>
                    {
                        if (player != null && player.IsInMidAir && !player.IsFalling)
                        {
                            ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.StaggerInfinite)
                                .ContinueWith((r) => ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Fall));
                        }
                    });

                    // TODO: how to fall properly?
                    ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.StaggerInfinite)
                        .ContinueWith((r) => ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Fall));
                    ScriptHelper.Timeout(() =>
                    {
                        cb.Stop();
                        CleanUp();
                    }, (uint)(Game.IsEditorTest ? 5000 : 5000));
                }
            }
        }

        private void CleanUp()
        {
            DistanceJoint.Remove(); TargetJoint.Remove(); InvisibleBlock.Remove();
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (InvisibleBlock != null)
                InvisibleBlock.SetWorldPosition(args.HitPosition);
        }

        private bool IsMovingAwayFrom(IObject o)
        {
            var oPos = o.GetWorldPosition();
            return Vector2.Distance(oPos, Instance.Position) < Vector2.Distance(oPos, Instance.Position + Instance.Direction);
        }
    }
}
