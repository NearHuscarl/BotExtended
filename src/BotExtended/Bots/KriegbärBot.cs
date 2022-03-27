using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class KriegbärBot : Bot
    {
        private Controller<KriegbärBot> m_controller;
        public IPlayer ThrowTarget { get; private set; }

        public KriegbärBot(BotArgs args, Controller<KriegbärBot> controller) : base(args)
        {
            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }
        }

        private int _holdingPlayerInGrabID = 0;
        private bool IsThrowingFromGrab = false;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (m_controller != null)
                m_controller.OnUpdate(elapsed);

            if (_holdingPlayerInGrabID == 0 && Player.HoldingPlayerInGrabID != 0)
            {
                OnEnemyGrabbed(Game.GetPlayer(Player.HoldingPlayerInGrabID));
                _holdingPlayerInGrabID = Player.HoldingPlayerInGrabID;
            }
            if (_holdingPlayerInGrabID != 0 && Player.HoldingPlayerInGrabID == 0)
            {
                OnEnemyGetThrownFromGrab(Game.GetPlayer(_holdingPlayerInGrabID));
                _holdingPlayerInGrabID = Player.HoldingPlayerInGrabID;
                IsThrowingFromGrab = false;
            }
        }

        public override void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos)
        {
            base.OnPlayerKeyInput(keyInfos);

            foreach (var keyInfo in keyInfos)
            {
                if (keyInfo.Event == VirtualKeyEvent.Pressed && keyInfo.Key == VirtualKey.BLOCK)
                {
                    IsThrowingFromGrab = Player.IsHoldingPlayerInGrab;
                    break;
                }
            }
        }

        public IEnumerable<IPlayer> GetThrowTargets(int thrownPlayerID = 0)
        {
            return Game.GetPlayers()
                .Where(p => p.UniqueID != thrownPlayerID && p.UniqueID != Player.UniqueID && !p.IsDead && !ScriptHelper.SameTeam(p, Player));
        }

        private void OnEnemyGrabbed(IPlayer grabbedPlayer)
        {
            var mod = Player.GetModifiers();
            mod.MeleeStunImmunity = 1;
            Player.SetModifiers(mod);

            var targets = GetThrowTargets(grabbedPlayer.UniqueID);
            var grabPosition = grabbedPlayer.GetWorldPosition();

            foreach (var target in targets)
            {
                var end = target.GetWorldPosition();
                if (Vector2.Distance(grabPosition, end) > 450) continue;
                var results = Game.RayCast(grabPosition, end, new RayCastInput()
                {
                    FilterOnMaskBits = true,
                    MaskBits = CategoryBits.Player + CategoryBits.StaticGround,
                }).Where(r => r.HitObject != null && r.ObjectID != grabbedPlayer.UniqueID && r.ObjectID != Player.UniqueID);

                if (!results.Any()) continue;
                var result = results.FirstOrDefault();
                if (!result.IsPlayer) continue;

                ThrowTarget = Game.GetPlayer(result.ObjectID);

                if (ThrowTarget.GetWorldPosition().X < Position.X && Player.GetFaceDirection() != -1)
                {
                    ScriptHelper.Command(Player, PlayerCommandType.Walk, FaceDirection.Left);
                }
                else if (ThrowTarget.GetWorldPosition().X > Position.X && Player.GetFaceDirection() != 1)
                {
                    ScriptHelper.Command(Player, PlayerCommandType.Walk, FaceDirection.Right);
                }

                if (Game.IsEditorTest)
                {
                    var p = ThrowTarget;
                    ScriptHelper.RunIn(() => Game.DrawArea(p.GetAABB(), Color.Green), 3000);
                }
                break;
            }
        }

        private void OnEnemyGetThrownFromGrab(IPlayer thrownPlayer)
        {
            if (!Player.IsBot && !IsThrowingFromGrab || ThrowTarget == null) return;

            var grabPosition = thrownPlayer.GetWorldPosition();
            var throwDirection = Vector2.Normalize(ThrowTarget.GetWorldPosition() - grabPosition) + Vector2.UnitY * .05f;
            var mod = thrownPlayer.GetModifiers();

            mod.ImpactDamageTakenModifier = .01f;
            thrownPlayer.SetModifiers(mod);
            thrownPlayer.TrackAsMissile(true);
            ResetModifiers();

            var cb = (Events.PlayerDamageCallback)null;
            var stop = false;
            var mass = thrownPlayer.GetMass();

            thrownPlayer.SetMass(0); // avoid gravity
            Action Stop = () =>
            {
                if (stop) return;
                cb.Stop(); stop = true; thrownPlayer.SetMass(mass); BotManager.GetBot(thrownPlayer).ResetModifiers();
            };

            ScriptHelper.Timeout(() => Stop(), 1500); // safeguard
            ScriptHelper.RunUntil(() => thrownPlayer.SetLinearVelocity(throwDirection * 25),
                () => thrownPlayer.IsOnGround || thrownPlayer.IsRemoved || stop, Stop);

            cb = Events.PlayerDamageCallback.Start((player, args) =>
            {
                if (args.DamageType == PlayerDamageEventType.Fall)
                {
                    if (player.UniqueID == thrownPlayer.UniqueID) Stop();
                    if (player.UniqueID != Player.UniqueID && thrownPlayer.GetAABB().Intersects(player.GetAABB()))
                    {
                        var velocity = player.GetLinearVelocity() * 1.75f;
                        velocity.Y = Math.Max(4f, velocity.Y + 4f); // bounce up a little to counter the friction
                        velocity = MathExtension.ClampMagnitude(velocity, 20);
                        player.SetLinearVelocity(velocity);
                        Stop();
                    }
                }
            });

            ThrowTarget = null;
        }
    }
}
