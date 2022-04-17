using BotExtended.Library;
using BotExtended.Weapons;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class EngineerBot_Controller : Controller<EngineerBot>
    {
        public bool IsBuilding { get { return m_state == EngineerState.Building; } }

        private Vector2[] ScanLine(float angle)
        {
            var start = Actor.Position + Vector2.UnitY * 9; // same height as turret's tip
            var end = start + ScriptHelper.GetDirection(angle) * 200;
            return new Vector2[] { start, end };
        }

        private List<Vector2[]> ScanLines
        {
            get
            {
                return new List<Vector2[]>()
                {
                    ScanLine(0),
                    ScanLine(MathHelper.PI),
                };
            }
        }

        enum EngineerState
        {
            Normal,
            Analyzing,
            GoingToPlaceholder,
            PreBuilding,
            Building,
        }
        private EngineerState m_state = EngineerState.Normal;

        private enum AvailableTurretDirection
        {
            None,
            Left,
            Right,
        }

        private AvailableTurretDirection m_availableDirection = AvailableTurretDirection.None;

        public void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            if (!Actor.HasEquipment)
            {
                StopBuilding();
            }
        }

        private float m_analyzePlaceCooldown = 0f;

        public override void OnUpdate(float elapsed)
        {
            Actor.LogDebug(m_state, Actor.BuildProgress);

            switch (m_state)
            {
                case EngineerState.Normal:
                    if (Actor.HasEnoughEnergy && Actor.HasEquipment)
                        m_state = EngineerState.Analyzing;
                    break;
                case EngineerState.Analyzing:
                    if (ScriptHelper.IsElapsed(m_analyzePlaceCooldown, 300))
                    {
                        m_analyzePlaceCooldown = Game.TotalElapsedGameTime;

                        if (CheckExistingPlaceholder())
                            GoToExistingPlaceholder();
                        else if (ShouldBuildTurretHere())
                            StartBuildingTurret();
                    }
                    break;
                case EngineerState.GoingToPlaceholder:
                    CheckArriveTargetPlaceholder();
                    break;
                case EngineerState.PreBuilding:
                    UpdatePrebuilding(elapsed);
                    break;
                case EngineerState.Building:
                    UpdateBuildingTurret();
                    break;
            }
        }

        private float m_damageTakenWhileBuilding = 0f;
        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            base.OnDamage(attacker, args);

            if (IsBuilding)
            {
                m_damageTakenWhileBuilding += args.Damage;

                if (m_damageTakenWhileBuilding >= 20)
                {
                    StopBuilding();
                    m_damageTakenWhileBuilding = 0;
                }
            }
        }

        private bool IsAttacked
        {
            get
            {
                return Player.IsStaggering || Player.IsCaughtByPlayerInDive || Player.IsStunned
                    || Player.IsFalling || Player.IsBurningInferno;
            }
        }
        private bool IsInactive
        {
            get { return (Player.IsIdle || Player.IsWalking) && !Player.IsInMidAir && !IsAttacked; }
        }
        private bool CanBuildTurretNow { get { return Actor.HasEquipment && IsInactive; } }
        private bool ShouldBuildTurretHere()
        {
            if (!CanBuildTurretNow)
                return false;

            if (!Actor.CanBuildTurretHere())
                return false;

            m_availableDirection = AvailableTurretDirection.None;
            var prioritizedDirection = Player.FacingDirection == 1 ? AvailableTurretDirection.Right : AvailableTurretDirection.Left;
            for (var i = 0; i < ScanLines.Count; i++)
            {
                var scanLine = ScanLines[i];
                Game.DrawLine(scanLine[0], scanLine[1], Color.Yellow);
                if (RayCastHelper.ImpassableObjects(scanLine[0], scanLine[1]).Count() == 0)
                {
                    m_availableDirection = i == 0 ? AvailableTurretDirection.Right : AvailableTurretDirection.Left;
                    if (m_availableDirection == prioritizedDirection)
                        break;
                }
            }
            if (m_availableDirection == AvailableTurretDirection.None) return false;

            // Uncomment if Engineer is too OP
            // foreach (var bot in BotManager.GetBots<EngineerBot>())
            // {
            //     if (bot.IsBuilding) return false;
            // }

            if (Actor.AreEnemiesNearby()) return false;

            foreach (var turret in WeaponManager.GetWeapons<Turret>())
            {
                if (turret.Broken) continue;

                var area = ScriptHelper.Area(
                    turret.Position + Vector2.UnitX * 32 * turret.Direction + Vector2.UnitY * 7,
                    turret.Position - Vector2.UnitX * 20 * turret.Direction - Vector2.UnitY * 14);
                Game.DrawArea(area);
                //ScriptHelper.LogDebug(area, area.Intersects(player.GetAABB()));
                if (area.Intersects(Player.GetAABB()))
                    return false;

                if (ScriptHelper.IntersectCircle(Actor.Position, turret.Position, 275, turret.MinAngle, turret.MaxAngle))
                    return false;
            }

            return true;
        }

        private TurretPlaceholder m_targetPlaceholder = null;
        private bool CheckExistingPlaceholder()
        {
            var untouchPlaceholders = WeaponManager.GetUntouchedTurretPlaceholders();

            if (untouchPlaceholders.Count() > 0)
            {
                var minDistanceToPlaceholder = float.PositiveInfinity;
                foreach (var p in untouchPlaceholders)
                {
                    var distanceToPlayer = Vector2.Distance(p.Value.Placeholder.Position, Actor.Position);
                    if (minDistanceToPlaceholder > distanceToPlayer)
                    {
                        minDistanceToPlaceholder = distanceToPlayer;
                        m_targetPlaceholder = p.Value.Placeholder;
                    }
                }
            }

            return m_targetPlaceholder != null;
        }

        private void GoToExistingPlaceholder()
        {
            var bs = Player.GetBotBehaviorSet();

            Player.SetGuardTarget(m_targetPlaceholder.RepresentedObject);
            bs.GuardRange = 1f;
            bs.ChaseRange = 1f;
            Player.SetBotBehaviorSet(bs);
            m_state = EngineerState.GoingToPlaceholder;
        }

        private void CheckArriveTargetPlaceholder()
        {
            if (m_targetPlaceholder.GetAABB().Intersects(Player.GetAABB()) && Actor.HasEquipment)
            {
                // At the time the builder arrives, another builder may arrived first and already started building
                if (!WeaponManager.GetUntouchedTurretPlaceholders()
                    .Where((p) => p.Key == m_targetPlaceholder.UniqueID)
                    .Any())
                {
                    if (!IsInactive)
                    {
                        m_state = EngineerState.Analyzing;
                    }
                    else
                    {
                        if (!ShouldBuildTurretHere())
                        {
                            Player.SetGuardTarget(null);
                            m_state = EngineerState.Normal;
                        }
                    }
                }
                else
                    StartBuildingTurret(m_targetPlaceholder.Direction);
            }
        }

        private void StartBuildingTurret()
        {
            if (m_availableDirection == AvailableTurretDirection.None) return;
            var direction = m_availableDirection == AvailableTurretDirection.Left ? TurretDirection.Left : TurretDirection.Right;
            StartBuildingTurret(direction);
        }
        private void StartBuildingTurret(TurretDirection direction)
        {
            Player.SetInputEnabled(false);
            if (Player.CurrentWeaponDrawn != WeaponItemType.Melee)
                Player.AddCommand(new PlayerCommand(PlayerCommandType.DrawMelee));
            Player.AddCommand(new PlayerCommand(PlayerCommandType.Walk, direction == TurretDirection.Left ?
                PlayerCommandFaceDirection.Left : PlayerCommandFaceDirection.Right, 10));

            m_state = EngineerState.PreBuilding;
        }

        private float m_prepareTimer = 0f;
        private void UpdatePrebuilding(float elapsed)
        {
            if (IsAttacked)
                StopBuilding();

            if (Player.IsIdle)
            {
                // Wait for the player to walk to position. If execute StartCrouch immediately, player will roll instead
                // WaitDestinationReached not working btw
                m_prepareTimer += elapsed;
                if (m_prepareTimer >= 150)
                {
                    Player.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));

                    if (m_targetPlaceholder == null)
                    {
                        m_state = EngineerState.Building;
                        m_prepareTimer = 0f;
                        Actor.CreateNewTurret();
                    }
                    else
                    {
                        // Execute first hit before changing state to occupy placeholder
                        Player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                        ScriptHelper.Timeout(() =>
                        {
                            m_state = EngineerState.Building;
                            m_prepareTimer = 0f;
                        }, HitTime);
                    }
                }
            }
        }

        private void StopBuilding()
        {
            m_hitTimer = 0f;
            m_targetPlaceholder = null;

            Player.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
            Player.SetInputEnabled(true);
            m_state = EngineerState.Normal;
        }

        private float m_hitTimer = 0f;
        private static readonly uint HitTime = 700;
        private void UpdateBuildingTurret()
        {
            if (IsAttacked || !Actor.IsOccupying)
            {
                StopBuilding(); return;
            }

            if (ScriptHelper.IsElapsed(m_hitTimer, HitTime))
            {
                Player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                m_hitTimer = Game.TotalElapsedGameTime;
            }
        }

        public void OnBuildCompleted() { StopBuilding(); }
    }
}
