using BotExtended.Library;
using BotExtended.Weapons;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class EngineerBot_Controller : IController<EngineerBot>
    {
        private EngineerBot m_actor;
        public EngineerBot Actor
        {
            get { return m_actor; }
            set
            {
                if (m_actor != null)
                {
                    m_actor.PlayerDropWeaponEvent -= OnPlayerDropWeapon;
                    m_actor.BuildCompletedEvent -= OnBuildCompleted;
                }
                m_actor = value;
                m_actor.PlayerDropWeaponEvent += OnPlayerDropWeapon;
                m_actor.BuildCompletedEvent += OnBuildCompleted;
            }
        }

        public bool IsBuilding { get { return m_state == EngineerState.Building; } }

        private Area DangerArea
        {
            get
            {
                return new Area(
                    Actor.Position - Vector2.UnitX * 30 - Vector2.UnitY * 5,
                    Actor.Position + Vector2.UnitX * 30 + Vector2.UnitY * 18);
            }
        }

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

        private void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weaponObj, float totalAmmo)
        {
            if (!Actor.HasEquipment())
            {
                StopBuilding();
            }
        }

        private float m_analyzePlaceCooldown = 0f;
        private float m_buildCooldown = 0f;
        private float BuildCooldownTime = 7000;

        public void OnUpdate(float elapsed)
        {
            switch (m_state)
            {
                case EngineerState.Normal:
                    m_buildCooldown += elapsed;

                    if ((Actor.HasEnoughEnergy() || m_buildCooldown >= BuildCooldownTime) && Actor.HasEquipment())
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
        public void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
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

        public void OnDeath(PlayerDeathArgs args)
        {
            Actor.PlayerDropWeaponEvent -= OnPlayerDropWeapon;
            Actor.BuildCompletedEvent -= OnBuildCompleted;
        }

        private bool IsAttacked()
        {
            var player = Actor.Player;
            return (player.IsStaggering || player.IsCaughtByPlayerInDive || player.IsStunned || player.IsFalling || player.IsBurningInferno);
        }
        private bool IsInactive()
        {
            var player = Actor.Player;
            return (player.IsIdle || player.IsWalking) && !player.IsInMidAir && !IsAttacked();
        }
        private bool CanBuildTurretNow()
        {
            return Actor.HasEquipment() && IsInactive();
        }
        private bool ShouldBuildTurretHere()
        {
            if (!CanBuildTurretNow())
                return false;

            if (!Actor.CanBuildTurretHere())
                return false;

            var player = Actor.Player;

            m_availableDirection = AvailableTurretDirection.None;
            var prioritizedDirection = player.FacingDirection == 1 ? AvailableTurretDirection.Right : AvailableTurretDirection.Left;
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

            // Don't build turret when enemies are nearby
            foreach (var bot in BotManager.GetBots())
            {
                if (!ScriptHelper.SameTeam(player, bot.Player))
                {
                    if (DangerArea.Intersects(bot.Player.GetAABB()))
                        return false;
                }
            }

            foreach (var turret in WeaponManager.GetWeapons<Turret>())
            {
                if (turret.Broken) continue;

                var area = new Area(
                    turret.Position + Vector2.UnitX * 32 * turret.Direction + Vector2.UnitY * 7,
                    turret.Position - Vector2.UnitX * 20 * turret.Direction - Vector2.UnitY * 14);
                area.Normalize();
                Game.DrawArea(area);
                //ScriptHelper.LogDebug(area, area.Intersects(player.GetAABB()));
                if (area.Intersects(player.GetAABB()))
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
            var player = Actor.Player;
            var bs = player.GetBotBehaviorSet();

            player.SetGuardTarget(m_targetPlaceholder.RepresentedObject);
            bs.GuardRange = 1f;
            bs.ChaseRange = 1f;
            player.SetBotBehaviorSet(bs);
            m_state = EngineerState.GoingToPlaceholder;
        }

        private void CheckArriveTargetPlaceholder()
        {
            var player = Actor.Player;

            if (m_targetPlaceholder.GetAABB().Intersects(player.GetAABB()) && Actor.HasEquipment())
            {
                // At the time the builder arrives, another builder may arrived first and already started building
                if (!WeaponManager.GetUntouchedTurretPlaceholders()
                    .Where((p) => p.Key == m_targetPlaceholder.UniqueID)
                    .Any())
                {
                    if (!IsInactive())
                    {
                        m_state = EngineerState.Analyzing;
                    }
                    else
                    {
                        if (!ShouldBuildTurretHere())
                        {
                            player.SetGuardTarget(null);
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
            var player = Actor.Player;

            player.SetInputEnabled(false);
            if (player.CurrentWeaponDrawn != WeaponItemType.Melee)
                player.AddCommand(new PlayerCommand(PlayerCommandType.DrawMelee));
            player.AddCommand(new PlayerCommand(PlayerCommandType.Walk, direction == TurretDirection.Left ?
                PlayerCommandFaceDirection.Left : PlayerCommandFaceDirection.Right, 10));

            m_state = EngineerState.PreBuilding;
        }

        private float m_prepareTimer = 0f;
        private void UpdatePrebuilding(float elapsed)
        {
            if (IsAttacked())
                StopBuilding();

            if (Actor.Player.IsIdle)
            {
                // Wait for the player to walk to position. If execute StartCrouch immediately, player will roll instead
                // WaitDestinationReached not working btw
                m_prepareTimer += elapsed;
                if (m_prepareTimer >= 150)
                {
                    Actor.Player.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));

                    if (m_targetPlaceholder == null)
                    {
                        m_state = EngineerState.Building;
                        m_prepareTimer = 0f;
                        Actor.CreateNewTurret();
                    }
                    else
                    {
                        // Execute first hit before changing state to occupy placeholder
                        Actor.Player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
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
            m_buildCooldown = 0f;
            m_targetPlaceholder = null;

            Actor.Player.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
            Actor.Player.SetInputEnabled(true);
            m_state = EngineerState.Normal;
        }

        private float m_hitTimer = 0f;
        private static readonly uint HitTime = 700;
        private void UpdateBuildingTurret()
        {
            if (IsAttacked() || !Actor.IsOccupying)
            {
                StopBuilding(); return;
            }

            if (ScriptHelper.IsElapsed(m_hitTimer, HitTime))
            {
                Actor.Player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                m_hitTimer = Game.TotalElapsedGameTime;
            }
        }

        private void OnBuildCompleted() { StopBuilding(); }
    }
}
