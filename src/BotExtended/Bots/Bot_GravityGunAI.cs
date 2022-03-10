using BotExtended.Library;
using BotExtended.Projectiles;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class Bot_GravityGunAI
    {
        private enum State
        {
            Normal,
            Drawing,
            Reloading,
            AimingTargetedObject,
            Retrieving,
            AimingEnemy,
            Cooldown,
        }
        private State m_state = State.Normal;
        private readonly Bot Bot;
        private IObjectText m_debugText;
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
                m_nearestObjectIsPlayer = ScriptHelper.IsPlayer(m_nearestObject);
            }
        }

        private IPlayer m_targetEnemy;
        private static readonly float CooldownTime = Game.IsEditorTest ? 1000 : 1000;

        public Bot_GravityGunAI(Bot bot)
        {
            Bot = bot;
            if (Constants.IS_ME_ALONE)
            {
                m_debugText = (IObjectText)Game.CreateObject("Text");
                m_debugText.SetTextScale(.5f);
            }
        }

        public void OnDroppedWeapon(PlayerWeaponRemovedArg arg) { Stop("Drop weapon"); }

        public void Update(float elapsed, GravityGun gun)
        {
            if (Constants.IS_ME_ALONE)
            {
                m_debugText.SetWorldPosition(Player.GetWorldPosition());
                m_debugText.SetText(ScriptHelper.ToDisplayString(m_state + "\n", Player.IsInputEnabled + "\n",
                    Player.GetBotBehaviorSet().RangedWeaponUsage + "\n", GetNeareastObject(gun) == null));
            }
            else
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

                if (m_timeout >= 3000f)
                    Stop("State timeout");
                if (Player.IsStaggering || Player.IsStunned || !Player.IsOnGround || Player.IsBurningInferno)
                    Stop("Player is stunned");
            }

            if (Game.IsEditorTest)
            {
                var o = GetNeareastObject(gun);
                if (o != null)
                    Game.DrawArea(o.GetAABB(), Color.Red);
                if (NearestObject != null)
                    Game.DrawArea(NearestObject.GetAABB(), Color.Magenta);
                foreach (var p in SearchedEnemies)
                    Game.DrawArea(p.GetAABB(), Color.Cyan);
                if (m_targetEnemy != null)
                    Game.DrawArea(m_targetEnemy.GetAABB(), Color.Green);
                Game.DrawArea(DangerArea, Color.Red);
                Game.DrawCircle(gun.GetHoldPosition(false), gun.MaxRange);
                Game.DrawArea(GetMimimumRange(), Color.Cyan);
            }

            //ScriptHelper.Stopwatch(() =>
            //{
            switch (m_state)
            {
                case State.Normal:
                {
                    if (!ScriptHelper.IsElapsed(m_stateDelay, 30))
                        break;

                    m_stateDelay = Game.TotalElapsedGameTime;
                    if (EnemiesNearby())
                    {
                        if (!Player.IsInputEnabled) Player.SetInputEnabled(true);
                        break;
                    }

                    var enemies = SearchedEnemies;
                    if (enemies.Count() > 0 && NearestObject == null)
                    {
                        NearestObject = gun.IsSupercharged ? enemies.First() : GetNeareastObject(gun);
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
                            ChangeState(State.Drawing);
                            break;
                        }

                        if (GetCurrentAmmo(gun) == 0)
                        {
                            Player.AddCommand(new PlayerCommand(PlayerCommandType.Reload));
                            ChangeState(State.Reloading);
                            break;
                        }

                        Player.AddCommand(new PlayerCommand(PlayerCommandType.StartAimAtPrecise, NearestObject.UniqueID));
                        m_executeOnce = true;
                    }

                    if (NearestObject != null && Player.CurrentWeaponDrawn == gun.Type && GetCurrentAmmo(gun) > 0)
                    {
                        ChangeState(State.AimingTargetedObject);
                    }
                    break;
                }
                case State.Drawing:
                {
                    if (!Player.IsDrawingWeapon) ChangeState(State.Normal);
                    break;
                }
                case State.Reloading:
                {
                    if (!Player.IsReloading) ChangeState(State.Normal);
                    break;
                }
                case State.AimingTargetedObject:
                {
                    var rangeLimit = GetRangeLimit();
                    var holdPosition = gun.GetHoldPosition(false);

                    if (NearestObject.IsRemoved ||
                        !ScriptHelper.IntersectCircle(NearestObject.GetAABB(), holdPosition, gun.MaxRange,
                        rangeLimit[0], rangeLimit[1]))
                    {
                        Stop("NearestObject not in range");
                        break;
                    }

                    if (Player.IsManualAiming && MaybeLockTarget(gun, NearestObject) && IsObjectInRange(gun, NearestObject))
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
                        Stop(gun.TargetedObject == null ? "TargetedObject = null" : "TargetedObject is stuck");
                        break;
                    }
                    if (gun.IsTargetedObjectStabilized && gun.TargetedObject.GetLinearVelocity().Length() < 1)
                    {
                        var enemies = SearchedEnemies;
                        if (enemies.Count() > 0 || m_nearestObjectIsPlayer)
                        {
                            if (enemies.Count() > 1 && m_nearestObjectIsPlayer)
                            {
                                foreach (var enemy in enemies)
                                {
                                    if (enemy.UniqueID != NearestObject.UniqueID)
                                    {
                                        m_targetEnemy = enemy; break;
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
                            Stop("No enemies to shoot");
                    }
                    break;
                }
                case State.AimingEnemy:
                {
                    if (gun.TargetedObject == null || m_targetEnemy != null && (m_targetEnemy.IsDead || m_targetEnemy.IsRemoved))
                    {
                        Stop(gun.TargetedObject == null ? "Already Shot" : "Enemy already dead");
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

            //    return m_state.ToString();
            //});
        }

        private void UpdateWeaponUsage(GravityGun gun)
        {
            var shouldUseRangeWpn = GetNeareastObject(gun) != null && m_state != State.Cooldown;

            // the old solution was to set ammo to 0 to disable using this gun. But the bot
            // will always sheathe weapon when running out of ammo and switch back, which
            // is very slow and distracting
            // Forbidden bot to use ranged weapon for a while will not make it switch weapon back and forth
            Bot.UseRangeWeapon(shouldUseRangeWpn);
        }

        private Area DangerArea
        {
            get { return ScriptHelper.GrowFromCenter(Player.GetAABB().Center, 50, 30); }
        }

        private bool EnemiesNearby()
        {
            return Game.GetPlayers()
                .Where(p => !p.IsDead && !ScriptHelper.SameTeam(p, Player) && DangerArea.Intersects(p.GetAABB())).Any();
        }

        private void ChangeState(State state, string reason = "")
        {
            var objName = NearestObject != null ? NearestObject.Name : "";
            var eneName = m_targetEnemy != null ? m_targetEnemy.Name : "";

            ScriptHelper.Log(m_state, "->", state, "[", objName, ",", eneName, "]", reason);
            m_timeout = 0f;
            m_state = state;
            m_executeOnce = false;
            m_stateDelay = Game.TotalElapsedGameTime;
            m_objStuckCheckTime = Game.TotalElapsedGameTime;
            m_oldObjPosition = Vector2.Zero;
            m_aimCheckTime = Game.TotalElapsedGameTime;
            m_aimDirection = Vector2.Zero;
    }

        private void Stop(string reason)
        {
            ChangeState(State.Cooldown, reason);
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
            var offset = Player.FacingDirection > 0 ? -MathExtension.OneDeg : MathExtension.OneDeg;

            return new float[] { -MathHelper.PIOver2, MathHelper.PIOver2 + offset };
        }

        private Area GetMimimumRange()
        {
            var h = Player.GetAABB();
            h.Grow(10, 8);
            h.Move(Vector2.UnitY * 5);
            return h;
        }

        private float m_checkNeareastObjectTime = 0f;
        private IObject m_neareastObject;
        private IObject GetNeareastObject(GravityGun gun)
        {
            if (ScriptHelper.IsElapsed(m_checkNeareastObjectTime, 30))
            {
                m_checkNeareastObjectTime = Game.TotalElapsedGameTime;
                m_neareastObject = SearchNearestObject(gun);
            }
            return m_neareastObject;
        }

        private IObject SearchNearestObject(GravityGun gun)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var filterArea = ScriptHelper.GrowFromCenter(
                holdPosition + Vector2.UnitX * Player.FacingDirection * gun.MaxRange / 2, gun.MaxRange);
            IObject nearestObject = null;

            foreach (var obj in Game.GetObjectsByArea(filterArea))
            {
                var rangeLimit = GetRangeLimit();
                var objPosition = obj.GetWorldPosition();

                if (ScriptHelper.IsDynamicObject(obj)
                    && !GravityGun.Blacklist.Contains(obj.Name)
                    && ScriptHelper.IntersectCircle(obj.GetAABB(), holdPosition, gun.MaxRange, rangeLimit[0], rangeLimit[1])
                    && !GetMimimumRange().Intersects(obj.GetAABB())
                    && (nearestObject == null || Rank(nearestObject, obj) == 1))
                {
                    var rcInput = new RayCastInput()
                    {
                        ClosestHitOnly = true,
                        FilterOnMaskBits = true,
                        MaskBits = CategoryBits.StaticGround,
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

        private float m_aimCheckTime;
        private Vector2 m_aimDirection = Vector2.Zero;
        private bool MaybeLockTarget(GravityGun gun, IObject obj)
        {
            if (m_aimDirection == Vector2.Zero)
            {
                m_aimDirection = Player.AimVector;
                return false; // init
            }

            if (ScriptHelper.IsElapsed(m_aimCheckTime, 15))
            {
                var aimDirection = Player.AimVector;
                if (Math.Abs(ScriptHelper.GetAngle(aimDirection) - ScriptHelper.GetAngle(m_aimDirection)) < MathExtension.OneDeg)
                {
                    return true;
                }
                m_aimDirection = aimDirection;
                m_aimCheckTime = Game.TotalElapsedGameTime;
            }
            return false;
        }

        private bool IsObjectInRange(GravityGun gun, IObject obj)
        {
            var holdPosition = gun.GetHoldPosition(false);
            var rcInput = new RayCastInput()
            {
                MaskBits = (ushort)(gun.IsSupercharged ? CategoryBits.Dynamic + CategoryBits.Player : CategoryBits.Dynamic),
                FilterOnMaskBits = true,
            };
            var results = Game.RayCast(holdPosition, holdPosition + Player.AimVector * gun.MaxRange, rcInput);
            Game.DrawLine(holdPosition, holdPosition + Player.AimVector * gun.MaxRange);

            foreach (var result in results)
            {
                if (result.ObjectID == obj.UniqueID)
                    return true;
            }
            return false;
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

            if (ScriptHelper.IsElapsed(m_objStuckCheckTime, 60))
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

        private bool IsPlayerInRange(GravityGun gun, IPlayer player)
        {
            if (player == null) return false;

            var holdPosition = gun.GetHoldPosition(false);
            var rcInput = new RayCastInput()
            {
                MaskBits = CategoryBits.Player,
                FilterOnMaskBits = true,
            };
            var results = Game.RayCast(Bot.Position, Bot.Position + Player.AimVector * gun.MaxRange * 4, rcInput);
            foreach (var result in results)
            {
                if (result.ObjectID == player.UniqueID)
                    return true;
            }
            return false;
        }

        private float m_checkEnemyTime = 0f;
        private IEnumerable<IPlayer> m_searchedEnemies = new List<IPlayer>();
        private IEnumerable<IPlayer> SearchedEnemies
        {
            get
            {
                if (ScriptHelper.IsElapsed(m_checkEnemyTime, 80))
                {
                    m_checkEnemyTime = Game.TotalElapsedGameTime;
                    var rangeLimit = GetRangeLimit();
                    var gravityGunRange = 160; // TODO: hardcoded number
                    m_searchedEnemies = RayCastHelper.GetFirstPlayerInRange(Player, gravityGunRange * 4, rangeLimit[0], rangeLimit[1],
                        true, Player.GetTeam(), Player);
                }
                return m_searchedEnemies;
            }
        }
    }
}
