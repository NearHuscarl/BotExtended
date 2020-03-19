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
        private readonly Bot Bot;
        private IPlayer Player { get { return Bot.Player; } }

        private bool m_executeOnce = false;
        private float m_cooldownTime = 0f;
        private float m_timeout = 0f; // in case bot gets stuck in a state
        private float m_shootDelayTime = 0f;
        private float m_shootDelayTimeThisTurn = 0f;
        private IObject m_nearestObject;
        private IPlayer m_targetEnemy;
        private static readonly float CooldownTime = Game.IsEditorTest ? 1000 : 1000;

        public Bot_GravityGunAI(Bot bot) { Bot = bot; }

        public void OnPlayerDropWeapon(IPlayer newOwner, IObjectWeaponItem weaponObj, float totalAmmo)
        {
            Stop();
        }

        public void Update(float elapsed, GravityGun gun)
        {
            //Bot.LogDebug(m_state, Player.GetBotBehaviorSet().RangedWeaponUsage, Player.CurrentPrimaryWeapon.CurrentAmmo);

            if (m_state == State.Normal || m_state == State.Cooldown)
            {
                // Trick the bot to use this weapon only when there are objects around
                // and stop using it when there is nothing to shoot with
                UpdateWeaponUsage(gun);
            }
            else
            {
                m_timeout += elapsed;
                if (m_timeout >= 3000f)
                    Stop();
                if (Player.IsStaggering || Player.IsStunned || !Player.IsOnGround || Player.IsBurningInferno)
                    Stop();
            }

            if (Game.IsEditorTest)
            {
                var o = SearchNearestObject(gun);
                if (o != null)
                    Game.DrawArea(o.GetAABB(), Color.Red);
                if (m_nearestObject != null)
                    Game.DrawArea(m_nearestObject.GetAABB(), Color.Magenta);
                foreach (var p in SearchNearestEnemy(gun))
                    Game.DrawArea(p.GetAABB(), Color.Cyan);
                if (m_targetEnemy != null)
                    Game.DrawArea(m_targetEnemy.GetAABB(), Color.Green);
            }

            switch (m_state)
            {
                case State.Normal:
                {
                    var enemies = SearchNearestEnemy(gun);
                    if (enemies.Count() > 0)
                    {
                        m_nearestObject = SearchNearestObject(gun);
                    }

                    if (m_nearestObject != null && !m_executeOnce)
                    {
                        Player.SetInputEnabled(false);

                        if (Player.CurrentWeaponDrawn != gun.Type)
                        {
                            ScriptHelper.LogDebug(Player.IsInputEnabled, Player.GetBotBehaviorSet().RangedWeaponUsage);
                            if (gun.Type == WeaponItemType.Rifle)
                                Player.AddCommand(new PlayerCommand(PlayerCommandType.DrawRifle));
                            if (gun.Type == WeaponItemType.Handgun)
                                Player.AddCommand(new PlayerCommand(PlayerCommandType.DrawHandgun));
                        }

                        if (GetCurrentAmmo(gun) == 0)
                            Player.AddCommand(new PlayerCommand(PlayerCommandType.Reload));

                        Player.AddCommand(new PlayerCommand(PlayerCommandType.StartAimAtPrecise, m_nearestObject.UniqueID));
                        m_executeOnce = true;
                    }

                    if (m_nearestObject != null && Player.CurrentWeaponDrawn == gun.Type && GetCurrentAmmo(gun) > 0)
                    {
                        ChangeState(State.AimingTargetedObject);
                    }
                    break;
                }
                case State.AimingTargetedObject:
                {
                    var rangeLimit = GetRangeLimit();
                    var holdPosition = gun.GetHoldPosition(false);

                    if (!ScriptHelper.IntersectCircle(m_nearestObject.GetAABB(), holdPosition, GravityGun.Range,
                        rangeLimit[0], rangeLimit[1]))
                    {
                        //Stop();
                        ChangeState(State.Normal);
                        break;
                    }

                    if (IsObjectInRange(gun, m_nearestObject) && Player.IsManualAiming)
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
                        Stop();
                        break;
                    }
                    if (gun.IsTargetedObjectStabilized && gun.TargetedObject.GetLinearVelocity().Length() < 1)
                    {
                        var enemies = SearchNearestEnemy(gun);

                        if (enemies.Count() > 0)
                        {
                            m_targetEnemy = enemies.First();
                            Player.AddCommand(new PlayerCommand(PlayerCommandType.StartAimAtPrecise, m_targetEnemy.UniqueID));
                            ChangeState(State.AimingEnemy);
                            var botBehaviorSet = Player.GetBotBehaviorSet();
                            m_shootDelayTimeThisTurn = RandomHelper.Between(
                                botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin,
                                botBehaviorSet.RangedWeaponPrecisionAimShootDelayMax);
                        }
                        else
                            Stop();
                    }
                    break;
                }
                case State.AimingEnemy:
                {
                    if (gun.TargetedObject == null || m_targetEnemy.IsDead || m_targetEnemy.IsRemoved)
                    {
                        Stop();
                        break;
                    }
                    if (IsPlayerInRange(gun, m_targetEnemy))
                    {
                        m_shootDelayTime += elapsed;

                        if (m_nearestObject.GetLinearVelocity().Length() < 1 && m_shootDelayTime >= m_shootDelayTimeThisTurn)
                        {
                            Player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                            m_shootDelayTime = 0f;
                        }
                    }
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

        private void UpdateWeaponUsage(GravityGun gun)
        {
            var botBehaviorSet = Player.GetBotBehaviorSet();

            if (SearchNearestObject(gun) == null || m_state == State.Cooldown)
            {
                // the old solution was to set ammo to 0 to disable using this gun. But the bot
                // will always sheathe weapon when running out of ammo and switch back, which
                // is very slow and distracting
                // Forbidden bot to use ranged weapon for a while will not make it switch weapon back and forth
                if (botBehaviorSet.RangedWeaponUsage)
                {
                    botBehaviorSet.RangedWeaponUsage = false;
                    Player.SetBotBehaviorSet(botBehaviorSet);
                }
            }
            else
            {
                if (!botBehaviorSet.RangedWeaponUsage)
                {
                    botBehaviorSet.RangedWeaponUsage = true;
                    Player.SetBotBehaviorSet(botBehaviorSet);
                }
            }
        }

        private void ChangeState(State state)
        {
            //ScriptHelper.LogDebug(m_state, "->", state);
            m_timeout = 0f;
            m_state = state;
            m_executeOnce = false;
        }

        private void Stop()
        {
            ChangeState(State.Cooldown);
            m_cooldownTime = Game.TotalElapsedGameTime;
            m_nearestObject = null;
            m_targetEnemy = null;
            Bot.Player.SetInputEnabled(true);
        }

        private int GetCurrentAmmo(GravityGun gun)
        {
            return gun.Type == WeaponItemType.Rifle ?
                Player.CurrentPrimaryWeapon.CurrentAmmo
                :
                Player.CurrentSecondaryWeapon.CurrentAmmo;
        }

        private float[] GetRangeLimit()
        {
            var oneDeg = 0.0174533f;
            var offset = Player.FacingDirection > 0 ? -oneDeg : oneDeg;

            return new float[] { -MathHelper.PIOver2, MathHelper.PIOver2 + offset };
        }

        private Area GetMimimumRange()
        {
            var h = Player.GetAABB();
            h.Grow(10, 8);
            h.Move(Vector2.UnitY * 5);
            return h;
        }

        private IObject SearchNearestObject(GravityGun gun)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var filterArea = new Area(
                holdPosition.Y + GravityGun.Range,
                holdPosition.X - GravityGun.Range,
                holdPosition.Y - GravityGun.Range,
                holdPosition.X + GravityGun.Range);
            var minimumRange = GetMimimumRange();

            IObject nearestObject = null;
            //Game.DrawCircle(holdPosition, GravityGun.Range);
            //Game.DrawArea(minimumRange, Color.Red);

            foreach (var obj in Game.GetObjectsByArea(filterArea))
            {
                var rangeLimit = GetRangeLimit();
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

        private bool IsObjectInRange(GravityGun gun, IObject obj)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var rcInput = new RayCastInput()
            {
                MaskBits = CategoryBits.Dynamic,
                FilterOnMaskBits = true,
            };
            var results = Game.RayCast(holdPosition, holdPosition + Player.AimVector * GravityGun.Range, rcInput);
            Game.DrawLine(holdPosition, holdPosition + Player.AimVector * GravityGun.Range);

            foreach (var result in results)
            {
                if (result.ObjectID == obj.UniqueID)
                    return true;
            }
            return false;
        }

        private bool IsPlayerInRange(GravityGun gun, IPlayer player)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var rcInput = new RayCastInput()
            {
                MaskBits = CategoryBits.Player,
                FilterOnMaskBits = true,
            };
            var results = Game.RayCast(Bot.Position, Bot.Position + Player.AimVector * GravityGun.Range * 4, rcInput);
            foreach (var result in results)
            {
                if (result.ObjectID == player.UniqueID)
                    return true;
            }
            return false;
        }

        private IEnumerable<IPlayer> SearchNearestEnemy(GravityGun gun)
        {
            var rangeLimit = GetRangeLimit();

            return RayCastHelper.GetPlayersInRange(Player, GravityGun.Range * 4, rangeLimit[0], rangeLimit[1],
                true, Player.GetTeam(), Player);
        }
    }
}
