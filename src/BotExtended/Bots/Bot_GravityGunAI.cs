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
            Fleeing, // Gravity gun is best use for run'n'gunner. Compeletely useless and is a liability at close range
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
        private float m_stateDelay = 0f;

        private bool m_nearestObjectIsPlayer;
        private IObject m_nearestObject;
        private IObject NearestObject
        {
            get { return m_nearestObject; }
            set
            {
                m_nearestObject = value;
                // using is keyword in update loop reduces fps to 10-20
                m_nearestObjectIsPlayer = m_nearestObject is IPlayer;
            }
        }

        private IPlayer m_targetEnemy;
        private static readonly float CooldownTime = Game.IsEditorTest ? 1000 : 1000;

        public Bot_GravityGunAI(Bot bot) { Bot = bot; }

        public void OnPlayerDropWeapon(IPlayer newOwner, IObjectWeaponItem weaponObj, float totalAmmo)
        {
            Stop();
        }

        public void Update(float elapsed, GravityGun gun)
        {
            Bot.LogDebug(m_state, Player.IsInputEnabled, Player.GetBotBehaviorSet().RangedWeaponUsage);

            if (m_state == State.Normal || m_state == State.Cooldown)
            {
                // Trick the bot to use this weapon only when there are objects around
                // and stop using it when there is nothing to shoot with
                UpdateWeaponUsage(gun);
            }
            else
            {
                m_timeout += elapsed;

                if (m_state != State.Fleeing)
                {
                    if (m_timeout >= 3000f)
                        Stop();
                    if (Player.IsStaggering || Player.IsStunned || !Player.IsOnGround || Player.IsBurningInferno)
                        Stop();
                }
            }

            if (Game.IsEditorTest)
            {
                var o = SearchNearestObject(gun);
                if (o != null)
                    Game.DrawArea(o.GetAABB(), Color.Red);
                if (NearestObject != null)
                    Game.DrawArea(NearestObject.GetAABB(), Color.Magenta);
                foreach (var p in SearchEnemies(gun))
                    Game.DrawArea(p.GetAABB(), Color.Cyan);
                if (m_targetEnemy != null)
                    Game.DrawArea(m_targetEnemy.GetAABB(), Color.Green);
                Game.DrawArea(DangerArea, Color.Cyan);
                if (m_targetLocation != null)
                    Game.DrawArea(m_targetLocation.GetAABB(), Color.Green);
            }

            switch (m_state)
            {
                case State.Fleeing:
                {
                    if (!ScriptHelper.IsElapsed(m_stateDelay, 30))
                        break;
                    m_stateDelay = Game.TotalElapsedGameTime;

                    var enemiesNearby = EnemiesNearby();
                    if (!enemiesNearby)
                    {
                        StopFleeing();
                        Stop();
                        //ChangeState(State.Normal);
                        break;
                    }
                    if (Player.GetAABB().Intersects(m_targetLocation.GetAABB()) && enemiesNearby)
                    {
                        StopFleeing();
                        Stop();
                        //ChangeState(State.Normal);
                    }
                    break;
                }
                case State.Normal:
                {
                    if (!ScriptHelper.IsElapsed(m_stateDelay, 30))
                        break;
                    m_stateDelay = Game.TotalElapsedGameTime;

                    // TODO: Flee state will be removed completely after it's used as reference when implmenting Funnyman
                    //if (EnemiesNearby() && TryToFlee())
                    //    break;

                    var enemies = SearchEnemies(gun);
                    if (enemies.Count() > 0 && NearestObject == null)
                    {
                        NearestObject = gun.IsSupercharged ? enemies.First() : SearchNearestObject(gun);
                    }

                    if (NearestObject != null && !m_executeOnce && Player.IsOnGround
                        && !Player.IsStaggering && !Player.IsStunned && !Player.IsHoldingPlayerInGrab)
                    {
                        Player.SetInputEnabled(false);

                        if (Player.CurrentWeaponDrawn != gun.Type)
                        {
                            if (gun.Type == WeaponItemType.Rifle)
                                Player.AddCommand(new PlayerCommand(PlayerCommandType.DrawRifle));
                            if (gun.Type == WeaponItemType.Handgun)
                                Player.AddCommand(new PlayerCommand(PlayerCommandType.DrawHandgun));
                        }

                        if (GetCurrentAmmo(gun) == 0)
                            Player.AddCommand(new PlayerCommand(PlayerCommandType.Reload));

                        Player.AddCommand(new PlayerCommand(PlayerCommandType.StartAimAtPrecise, NearestObject.UniqueID));
                        m_executeOnce = true;
                    }

                    if (NearestObject != null && Player.CurrentWeaponDrawn == gun.Type && GetCurrentAmmo(gun) > 0)
                    {
                        ChangeState(State.AimingTargetedObject);
                    }
                    break;
                }
                case State.AimingTargetedObject:
                {
                    var rangeLimit = GetRangeLimit();
                    var holdPosition = gun.GetHoldPosition(false);

                    if (!ScriptHelper.IntersectCircle(NearestObject.GetAABB(), holdPosition, GravityGun.Range,
                        rangeLimit[0], rangeLimit[1]))
                    {
                        Stop();
                        break;
                    }

                    if (IsObjectInRange(gun, NearestObject) && Player.IsManualAiming)
                    {
                        gun.PickupObject();
                        ChangeState(State.Retrieving);
                    }
                    break;
                }
                case State.Retrieving:
                {
                    if (gun.TargetedObject == null || IsObjectStuck(gun.TargetedObject))
                    {
                        Stop();
                        break;
                    }
                    if (gun.IsTargetedObjectStabilized && gun.TargetedObject.GetLinearVelocity().Length() < 1)
                    {
                        var enemies = SearchEnemies(gun);
                        if (enemies.Count() > 0 || m_nearestObjectIsPlayer)
                        {
                            if (enemies.Count() > 1 && m_nearestObjectIsPlayer)
                            {
                                foreach (var enemy in enemies)
                                {
                                    if (enemy.UniqueID != NearestObject.UniqueID)
                                    {
                                        m_targetEnemy = enemy;break;
                                    }
                                }
                            }
                            else if (enemies.Count() > 0)
                                m_targetEnemy = enemies.First();

                            if (m_targetEnemy != null)
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
                    if (gun.TargetedObject == null || m_targetEnemy != null && (m_targetEnemy.IsDead || m_targetEnemy.IsRemoved))
                    {
                        Stop();
                        break;
                    }
                    if (IsPlayerInRange(gun, m_targetEnemy) || m_nearestObjectIsPlayer)
                    {
                        m_shootDelayTime += elapsed;

                        if (m_shootDelayTime >= m_shootDelayTimeThisTurn)
                        {
                            if (!m_nearestObjectIsPlayer && NearestObject.GetLinearVelocity().Length() < 1
                                || m_nearestObjectIsPlayer)
                            {
                                Player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                                m_shootDelayTime = 0f;
                            }
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

        private float m_objStuckCheckTime;
        private Vector2 m_oldObjPosition = Vector2.Zero;
        private bool IsObjectStuck(IObject obj)
        {
            if (m_oldObjPosition == Vector2.Zero)
            {
                m_oldObjPosition = obj.GetWorldPosition();
                return false; // init
            }

            if (ScriptHelper.IsElapsed(m_objStuckCheckTime, 30))
            {
                var currentPosition = obj.GetWorldPosition();
                if (Vector2.Distance(currentPosition, m_oldObjPosition) < .5f)
                {
                    return true;
                }
                m_oldObjPosition = currentPosition;
                m_objStuckCheckTime = Game.TotalElapsedGameTime;
            }
            return false;
        }

        private BotBehaviorSet m_oldBotBehaviorSet = null;
        private IObject m_targetLocation = null;
        private Area SafeArea(Vector2 spawnPosition) { return ScriptHelper.GrowFromCenter(spawnPosition, 60, 30); }
        private IObjectPathNode[] AllPathNodes
        {
            get
            {
                // NOTE: cannot init field since markers only available after Startup()
                // TODO: Game.GetObjectsByArea<IObjectPathNode> doesn't work. Maybe a ScriptAPI bug
                return Game.GetObjects<IObjectPathNode>();
            }
        }
        private bool TryToFlee()
        {
            var pathNodes = Game.GetObjects<IObjectPathNode>();
            foreach (var spawner in Game.GetObjectsByName("SpawnPlayer"))
            {
                var safeArea = SafeArea(spawner.GetWorldPosition());
                var enemiesNearSpawner = Game.GetObjectsByArea<IPlayer>(safeArea).Any(p => !ScriptHelper.SameTeam(Player, p));
                var pathNode = AllPathNodes.Where((p) => p.GetAABB().Intersects(spawner.GetAABB())).FirstOrDefault();

                if (pathNode == null) continue;

                if (!enemiesNearSpawner)
                {
                    if (m_targetLocation == null)
                        m_targetLocation = pathNode;
                    else if (m_targetLocation != null && Vector2.DistanceSquared(m_targetLocation.GetWorldPosition(), Bot.Position)
                        > Vector2.DistanceSquared(pathNode.GetWorldPosition(), Bot.Position))
                        m_targetLocation = pathNode;
                }
            }

            if (m_targetLocation != null)
            {
                if (m_oldBotBehaviorSet == null)
                    m_oldBotBehaviorSet = Player.GetBotBehaviorSet();
                var runningBehavior = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.FunnymanRunning);
                runningBehavior.MeleeUsage = true; // dont just run like that coward funnyman
                runningBehavior.GuardRange = 1f;
                runningBehavior.ChaseRange = 1f;
                Player.SetBotBehaviorSet(runningBehavior);
                Player.SetGuardTarget(m_targetLocation);
                Player.ClearCommandQueue();
                Player.SetInputEnabled(true);
                ChangeState(State.Fleeing);
                return true;
            }
            return false;
        }

        private void StopFleeing()
        {
            Player.SetBotBehaviorSet(m_oldBotBehaviorSet);
            Player.SetGuardTarget(null);
            m_targetLocation = null;
            m_oldBotBehaviorSet = null;
        }

        private void UpdateWeaponUsage(GravityGun gun)
        {
            // BotBehaviorSet is already modified when fleeing.
            if (m_state == State.Fleeing) return;

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

        private Area DangerArea
        {
            get { return ScriptHelper.GrowFromCenter(Player.GetAABB().Center, 50, 30); }
        }

        private bool EnemiesNearby()
        {
            foreach (var player in Game.GetPlayers())
            {
                if (!ScriptHelper.SameTeam(Player, player))
                {
                    if (DangerArea.Intersects(player.GetAABB()))
                        return true;
                }
            }
            return false;
        }

        private void ChangeState(State state)
        {
            var objName = NearestObject != null ? NearestObject.Name : "";
            var eneName = m_targetEnemy != null ? m_targetEnemy.Name : "";
            ScriptHelper.LogDebug(m_state, "->", state, "[", objName, ",", eneName, "]");
            m_timeout = 0f;
            m_state = state;
            m_executeOnce = false;
            m_stateDelay = Game.TotalElapsedGameTime;
            m_objStuckCheckTime = Game.TotalElapsedGameTime;
            m_oldObjPosition = Vector2.Zero;
        }

        private void Stop()
        {
            ChangeState(State.Cooldown);
            m_cooldownTime = Game.TotalElapsedGameTime;
            NearestObject = null;
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
            var filterArea = ScriptHelper.GrowFromCenter(
                holdPosition + Vector2.UnitX * Player.FacingDirection * GravityGun.Range / 2, GravityGun.Range);
            var minimumRange = GetMimimumRange();

            IObject nearestObject = null;
            Game.DrawCircle(holdPosition, GravityGun.Range);
            Game.DrawArea(minimumRange, Color.Red);

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
                    && (nearestObject == null || Rank(nearestObject, obj) == 1))
                {
                    var rcInput = new RayCastInput()
                    {
                        ClosestHitOnly = true,
                        MaskBits = CategoryBits.StaticGround,
                        FilterOnMaskBits = true,
                    };
                    var results = Game.RayCast(holdPosition, objPosition, rcInput);

                    if (results.Count() > 0 && results.First().HitObject != null)
                    {
                        var result = results.First();
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

        // Higher weight means the object with that collision group deals more damage generally
        private static readonly Dictionary<ushort, int> CollisionCategoryWeight = new Dictionary<ushort, int>()
        {
            { CategoryBits.DynamicG2, 0 },
            { CategoryBits.Dynamic, 1 },
            { CategoryBits.DynamicG1, 2 },
        };

        private int Rank(IObject o1, IObject o2)
        {
            var o1c = o1.GetCollisionFilter().CategoryBits;
            var o2c = o2.GetCollisionFilter().CategoryBits;
            var s1 = o1.GetAABB().Width * o1.GetAABB().Height;
            var s2 = o2.GetAABB().Width * o2.GetAABB().Height;

            if (CollisionCategoryWeight[o1c] < CollisionCategoryWeight[o2c])
                return 1;
            if (o2.GetMass() > o1.GetMass())
                return 1;
            if (s2 > s1)
                return 1;
            return -1;
        }

        private bool IsObjectInRange(GravityGun gun, IObject obj)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var rcInput = new RayCastInput()
            {
                MaskBits = (ushort)(gun.IsSupercharged ? CategoryBits.Dynamic + CategoryBits.Player : CategoryBits.Dynamic),
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
            if (player == null) return false;

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

        private IEnumerable<IPlayer> SearchEnemies(GravityGun gun)
        {
            var rangeLimit = GetRangeLimit();

            return RayCastHelper.GetPlayersInRange(Player, GravityGun.Range * 4, rangeLimit[0], rangeLimit[1],
                true, Player.GetTeam(), Player);
        }
    }
}
