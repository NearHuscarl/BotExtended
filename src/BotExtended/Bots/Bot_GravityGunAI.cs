using BotExtended.Library;
using BotExtended.Projectiles;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class Bot_GravityGunAI
    {
        private enum State
        {
            Normal,
            AimingTargetedObject,
            Retrieving,
            AimingEnemy,
            Cooldown,
        }
        private State m_state = State.Normal;
        private float m_cooldownTime = 0f;
        private float m_timeout = 0f; // in case bot gets stuck in a state
        private float m_shootDelayTime = 0f;
        private float m_shootDelayTimeThisTurn = 0f;
        private IObject m_nearestObject;
        private IPlayer m_targetEnemy;
        private static readonly float CooldownTime = Game.IsEditorTest ? 1000 : 3000;

        public void Update(float elapsed, Bot bot, GravityGun gun)
        {
            var player = bot.Player;

            // Trick the bot to use this weapon only when there are objects around
            // and stop using it when there is nothing to shoot with
            UpdateWeaponUsage(bot, gun);
            Game.DrawText(m_state.ToString(), bot.Position);

            if (!player.IsManualAiming)
            {
                if (m_state != State.Normal || m_state != State.Cooldown)
                {
                    Stop(bot);
                    m_state = State.Normal;
                }
                return;
            }

            m_timeout += elapsed;

            if (m_timeout >= 3000f || ShouldBeNormalNow(bot, gun))
            {
                if (m_state != State.Normal)
                    Stop(bot);
            }

            switch (m_state)
            {
                case State.Normal:
                {
                    var enemies = SearchNearestEnemy(bot, gun);
                    if (enemies.Count() > 0)
                    {
                        m_nearestObject = SearchNearestObject(bot, gun);
                    }

                    if (m_nearestObject != null)
                    {
                        ChangeState(State.AimingTargetedObject);
                        Game.DrawArea(m_nearestObject.GetAABB());
                        player.SetInputEnabled(false);
                        player.AddCommand(new PlayerCommand(PlayerCommandType.StartAimAtPrecise, m_nearestObject.UniqueID));
                    }
                    break;
                }
                case State.AimingTargetedObject:
                {
                    var rangeLimit = GetRangeLimit(player);
                    var holdPosition = gun.GetHoldPosition(false);

                    if (!ScriptHelper.IntersectCircle(m_nearestObject.GetAABB(), holdPosition, GravityGun.Range,
                        rangeLimit[0], rangeLimit[1]))
                    {
                        Stop(bot);
                        break;
                    }

                    if (IsObjectInRange(bot, gun, m_nearestObject))
                    {
                        gun.PickupObject();
                        ChangeState(State.Retrieving);
                    }
                    break;
                }
                case State.Retrieving:
                {
                    if (gun.TargetedObject == null)
                    {
                        Stop(bot);
                        break;
                    }
                    if (gun.IsTargetedObjectStabilized)
                    {
                        var enemies = SearchNearestEnemy(bot, gun);

                        if (enemies.Count() > 0)
                        {
                            m_targetEnemy = enemies.First();
                            player.AddCommand(new PlayerCommand(PlayerCommandType.StartAimAtPrecise, m_targetEnemy.UniqueID));
                            ChangeState(State.AimingEnemy);
                            var botBehaviorSet = player.GetBotBehaviorSet();
                            m_shootDelayTimeThisTurn = RandomHelper.Between(
                                botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin,
                                botBehaviorSet.RangedWeaponPrecisionAimShootDelayMax);
                        }
                        else
                            Stop(bot);
                    }
                    break;
                }
                case State.AimingEnemy:
                {
                    if (gun.TargetedObject == null || m_targetEnemy.IsDead || m_targetEnemy.IsRemoved)
                    {
                        Stop(bot);
                        break;
                    }
                    Game.DrawArea(m_targetEnemy.GetAABB(), Color.Green);
                    if (IsPlayerInRange(bot, gun, m_targetEnemy))
                    {
                        m_shootDelayTime += elapsed;

                        if (m_nearestObject.GetLinearVelocity().Length() < 1 && m_shootDelayTime >= m_shootDelayTimeThisTurn)
                        {
                            player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                            m_shootDelayTime = 0f;
                            Stop(bot);
                        }
                    }
                    else
                        Stop(bot);
                    break;
                }
                case State.Cooldown:
                {
                    if (ScriptHelper.IsElapsed(m_cooldownTime, CooldownTime))
                    {
                        ChangeState(State.Normal);
                    }
                    break;
                }
            }
        }

        private bool ShouldBeNormalNow(Bot bot, GravityGun gun)
        {
            if (bot.CurrentWeapon != gun.Name)
                return true;
            return false;
        }

        private void UpdateWeaponUsage(Bot bot, GravityGun gun)
        {
            var player = bot.Player;
            var botBehaviorSet = player.GetBotBehaviorSet();

            // disable all other range weapon usages too. A questionable workaround
            if (SearchNearestObject(bot, gun) == null || m_state == State.Cooldown)
            {
                if (botBehaviorSet.RangedWeaponUsage)
                {
                    botBehaviorSet.RangedWeaponUsage = false;
                    player.SetBotBehaviorSet(botBehaviorSet);
                }
            }
            else
            {
                if (!botBehaviorSet.RangedWeaponUsage)
                {
                    botBehaviorSet.RangedWeaponUsage = true;
                    player.SetBotBehaviorSet(botBehaviorSet);
                }
            }
        }

        private void ChangeState(State state)
        {
            ScriptHelper.LogDebug(m_state, "->", state);
            m_timeout = 0f;
            m_state = state;
        }

        private void Stop(Bot bot)
        {
            ChangeState(State.Cooldown);
            m_cooldownTime = Game.TotalElapsedGameTime;
            m_nearestObject = null;
            m_targetEnemy = null;
            bot.Player.SetInputEnabled(true);
        }

        private float[] GetRangeLimit(IPlayer player)
        {
            var oneDeg = 0.0174533f;
            var offset = player.FacingDirection > 0 ? -oneDeg : oneDeg;

            return new float[] { -MathHelper.PIOver2, MathHelper.PIOver2 + offset };
        }

        private Area GetMimimumRange(Bot bot)
        {
            var h = bot.Player.GetAABB();
            h.Grow(10, 8);
            h.Move(Vector2.UnitY * 5);
            return h;
        }

        private IObject SearchNearestObject(Bot bot, GravityGun gun)
        {
            var player = bot.Player;
            var holdPosition = gun.GetHoldPosition(false);
            var filterArea = new Area(
                holdPosition.Y + GravityGun.Range,
                holdPosition.X - GravityGun.Range,
                holdPosition.Y - GravityGun.Range,
                holdPosition.X + GravityGun.Range);
            var minimumRange = GetMimimumRange(bot);

            IObject nearestObject = null;
            //Game.DrawCircle(holdPosition, GravityGun.Range);
            //Game.DrawArea(minimumRange, Color.Red);

            foreach (var obj in Game.GetObjectsByArea(filterArea))
            {
                var rangeLimit = GetRangeLimit(player);
                var categoryBits = obj.GetCollisionFilter().CategoryBits;
                var isDynamicObject = categoryBits == CategoryBits.DynamicG1
                    || categoryBits == CategoryBits.DynamicG2
                    || categoryBits == CategoryBits.Dynamic;
                var objPosition = obj.GetWorldPosition();

                if (isDynamicObject
                    && ScriptHelper.IntersectCircle(obj.GetAABB(), holdPosition, GravityGun.Range, rangeLimit[0], rangeLimit[1])
                    && !minimumRange.Intersects(obj.GetAABB())
                    && (nearestObject == null ||
                        Vector2.DistanceSquared(holdPosition, objPosition) <
                        Vector2.DistanceSquared(holdPosition, nearestObject.GetWorldPosition())))
                {
                    var rcInput = new RayCastInput()
                    {
                        ClosestHitOnly = true,
                        MaskBits = CategoryBits.StaticGround,
                        FilterOnMaskBits = true,
                    };
                    var result = Game.RayCast(holdPosition, objPosition, rcInput).FirstOrDefault();
                    if (result.HitObject != null)
                    {
                        var closestStaticObjPosition = result.Position;

                        if (Vector2.DistanceSquared(holdPosition, objPosition) <
                            Vector2.DistanceSquared(holdPosition, closestStaticObjPosition))
                        {
                            nearestObject = obj;
                        }
                    }
                    else
                        nearestObject = obj;
                }
            }

            return nearestObject;
        }

        private bool IsObjectInRange(Bot bot, GravityGun gun, IObject obj)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var rcInput = new RayCastInput()
            {
                MaskBits = CategoryBits.Dynamic,
                FilterOnMaskBits = true,
            };
            var results = Game.RayCast(holdPosition, holdPosition + bot.Player.AimVector * GravityGun.Range, rcInput);

            foreach (var result in results)
            {
                if (result.ObjectID == obj.UniqueID)
                    return true;
            }
            return false;
        }

        private bool IsPlayerInRange(Bot bot, GravityGun gun, IPlayer player)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var rcInput = new RayCastInput()
            {
                MaskBits = CategoryBits.Player,
                FilterOnMaskBits = true,
            };
            var results = Game.RayCast(bot.Position, bot.Position + bot.Player.AimVector * GravityGun.Range * 4, rcInput);
            foreach (var result in results)
            {
                if (result.ObjectID == player.UniqueID)
                    return true;
            }
            return false;
        }

        private IEnumerable<IPlayer> SearchNearestEnemy(Bot bot, GravityGun gun)
        {
            var player = bot.Player;
            var holdPosition = gun.GetHoldPosition(false);
            var rangeLimit = GetRangeLimit(player);

            return RayCastHelper.GetPlayersInRange(bot.Position, GravityGun.Range * 4, rangeLimit[0], rangeLimit[1],
                true, player.GetTeam(), player);
        }
    }
}
