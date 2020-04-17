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
        public const float ThrowRadius = 140f;
        private Controller<KriegbärBot> m_controller;

        public KriegbärBot(BotArgs args, Controller<KriegbärBot> controller) : base(args)
        {
            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }
        }

        private int m_oldHoldingPlayerInGrabID = 0;
        private bool IsThrowingFromGrab = false;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (m_controller != null)
                m_controller.OnUpdate(elapsed);

            if (Player.HoldingPlayerInGrabID != 0 && m_oldHoldingPlayerInGrabID == 0)
            {
                m_oldHoldingPlayerInGrabID = Player.HoldingPlayerInGrabID;
            }
            if (Player.HoldingPlayerInGrabID == 0 && m_oldHoldingPlayerInGrabID != 0)
            {
                OnPlayerGetThrownFromGrab(Game.GetPlayer(m_oldHoldingPlayerInGrabID));
                m_oldHoldingPlayerInGrabID = Player.HoldingPlayerInGrabID;
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
            var throwAngleLimits = new float[] { MathExtension.ToRadians(0), MathExtension.ToRadians(75) };
            if (Player.FacingDirection < 0)
                throwAngleLimits = ScriptHelper.Flip(throwAngleLimits, FlipDirection.Horizontal);

            var i = 0;
            return Game.GetPlayers()
                .Where(p =>
                {
                    var result = p.UniqueID != thrownPlayerID && p.UniqueID != Player.UniqueID
                        && !p.IsDead
                        && !ScriptHelper.SameTeam(p, Player)
                        && ScriptHelper.IntersectCircle(p.GetAABB(), Position, ThrowRadius, throwAngleLimits[0], throwAngleLimits[1]);

                    if (false && Game.IsEditorTest && result && i++ == 0)
                    {
                        ScriptHelper.RunIn(() =>
                        {
                            Game.DrawArea(p.GetAABB(), Color.Red);
                            Game.DrawCircle(Position, ThrowRadius, Color.Cyan);
                            Game.DrawLine(Position, Position + ScriptHelper.GetDirection(throwAngleLimits[0]) * ThrowRadius);
                            Game.DrawLine(Position, Position + ScriptHelper.GetDirection(throwAngleLimits[1]) * ThrowRadius);
                        }, 2000);
                    }

                    return result;
                });
        }

        private void OnPlayerGetThrownFromGrab(IPlayer thrownPlayer)
        {
            if (!Player.IsBot && !IsThrowingFromGrab) return;

            var grabPosition = thrownPlayer.GetWorldPosition();
            var targets = GetThrowTargets(thrownPlayer.UniqueID);

            foreach (var target in targets)
            {
                var results = Game.RayCast(grabPosition, target.GetWorldPosition(), new RayCastInput()
                {
                    FilterOnMaskBits = true,
                    AbsorbProjectile = RayCastFilterMode.True,
                    MaskBits = CategoryBits.Player + CategoryBits.StaticGround,
                    ClosestHitOnly = true,
                }).Where(r => r.HitObject != null && r.ObjectID != thrownPlayer.UniqueID);

                if (results.Any())
                {
                    var result = results.Single();

                    if (result.IsPlayer)
                    {
                        var hitObject = result.HitObject;
                        var throwDirection = Vector2.Normalize(hitObject.GetWorldPosition() - grabPosition);
                        var throwTime = Game.TotalElapsedGameTime;

                        Game.DrawArea(hitObject.GetAABB(), Color.Cyan);
                        Game.DrawLine(grabPosition, grabPosition + throwDirection * 30, Color.Cyan);
                        thrownPlayer.SetLinearVelocity(throwDirection * 20 + Vector2.UnitY * 4);
                        thrownPlayer.TrackAsMissile(true);

                        Events.PlayerDamageCallback damageCB = null;
                        damageCB = Events.PlayerDamageCallback.Start((IPlayer player, PlayerDamageArgs args) =>
                        {
                            if (thrownPlayer.IsRemoved
                            || !thrownPlayer.IsMissile && player.UniqueID != thrownPlayer.UniqueID
                            || ScriptHelper.IsElapsed(throwTime, 2000))
                            {
                                damageCB.Stop();
                            }
                            if (thrownPlayer.GetAABB().Intersects(player.GetAABB()) && args.DamageType == PlayerDamageEventType.Fall)
                            {
                                var velocity = player.GetLinearVelocity() * 1.75f;
                                velocity.Y = Math.Max(4f, velocity.Y + 4f); // bounce up a little to counter the friction
                                velocity = MathExtension.ClampMagnitude(velocity, 20);
                                player.SetLinearVelocity(velocity);
                            }
                        });
                    }
                    return;
                }
            }
        }
    }
}
