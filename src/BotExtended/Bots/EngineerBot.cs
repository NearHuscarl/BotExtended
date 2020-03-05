using BotExtended.Library;
using BotExtended.Weapons;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class EngineerBot : Bot
    {
        private enum AvailableTurretDirection
        {
            None,
            Left,
            Right,
        }

        private static readonly HashSet<WeaponItem> BuildItems = new HashSet<WeaponItem>()
        {
            WeaponItem.LEAD_PIPE,
            WeaponItem.PIPE,
            WeaponItem.HAMMER,
        };

        public float BuildTime { get; private set; }
        public bool IsBuilding { get { return m_state == EngineerState.Building; } }

        private AvailableTurretDirection m_availableDirection = AvailableTurretDirection.None;

        private Area DangerArea
        {
            get
            {
                var position = Player.GetWorldPosition();
                return new Area(
                    position - Vector2.UnitX * 30 - Vector2.UnitY * 5,
                    position + Vector2.UnitX * 30 + Vector2.UnitY * 18);
            }
        }

        private Vector2[] ScanLine(float angle)
        {
            var start = Player.GetWorldPosition() + Vector2.UnitY * 9; // same height as turret's tip
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

        public EngineerBot() : base()
        {
            BuildTime = Game.IsEditorTest ? 1000 : 5000; // TODO: remove
            UpdateInterval = 0;
            PlayerDropWeaponEvent += OnPlayerDropWeapon;
        }

        private void OnPlayerDropWeapon(IPlayer previousOwner, IObjectWeaponItem weaponObj)
        {
            if (BuildItems.Contains(weaponObj.WeaponItem))
            {
                if (m_state != EngineerState.Normal) m_state = EngineerState.Normal;
            }
        }

        enum EngineerState
        {
            Normal,
            Analyzing,
            GoingToPlaceholder,
            Building,
        }
        private EngineerState m_state = EngineerState.Normal;

        private float m_buildCooldown = 0f;
        private float m_analyzePlaceCooldown = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            switch (m_state)
            {
                case EngineerState.Normal:
                    m_buildCooldown += elapsed;

                    if (m_buildCooldown >= (Game.IsEditorTest ? 3000 : 7000) /*TODO: remove*/
                        && BuildItems.Contains(Player.CurrentMeleeWeapon.WeaponItem))
                    {
                        m_state = EngineerState.Analyzing;
                        m_analyzePlaceCooldown = 300;
                    }
                    break;
                case EngineerState.Analyzing:
                    m_analyzePlaceCooldown += elapsed;

                    if (m_analyzePlaceCooldown >= (Game.IsEditorTest ? 0 : 300))
                    {
                        if (CheckExistingPlaceholder())
                            GoToExistingPlaceholder();
                        else if (CanBuildTurretHere())
                            StartBuildingTurret();
                        m_analyzePlaceCooldown = 0;
                    }
                    break;
                case EngineerState.GoingToPlaceholder:
                    CheckArriveTargetPlaceholder();
                    break;
                case EngineerState.Building:
                    UpdateBuildingTurret(elapsed);
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

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);
            WeaponManager.RemoveBuilderFromTurretPlaceholder(Player.UniqueID);
        }

        private bool IsAttacked()
        {
            return (Player.IsStaggering || Player.IsCaughtByPlayerInDive || Player.IsStunned);
        }
        private bool IsInactive()
        {
            return (Player.IsIdle || Player.IsWalking) && !Player.IsInMidAir && !IsAttacked();
        }
        private bool CanBuildTurret()
        {
            return BuildItems.Contains(Player.CurrentMeleeWeapon.WeaponItem) && IsInactive();
        }
        private bool CanBuildTurretHere()
        {
            if (!CanBuildTurret())
                return false;

            m_availableDirection = AvailableTurretDirection.None;
            for (var i = 0; i < ScanLines.Count; i++)
            {
                var scanLine = ScanLines[i];
                Game.DrawLine(scanLine[0], scanLine[1], Color.Yellow);
                if (RayCastHelper.ImpassableObjects(scanLine[0], scanLine[1]).Count() == 0)
                {
                    m_availableDirection = i == 0 ? AvailableTurretDirection.Right : AvailableTurretDirection.Left;
                }
            }
            if (m_availableDirection == AvailableTurretDirection.None) return false;

            // Uncomment if Engineer is too OP
            // foreach (var bot in BotManager.GetBots<EngineerBot>())
            // {
            //     if (bot.IsBuilding) return false;
            // }

            // Don't build turret when enemies are nearby
            foreach (var bot in BotManager.GetBots<Bot>())
            {
                if (!ScriptHelper.SameTeam(Player, bot.Player))
                {
                    if (DangerArea.Intersects(bot.Player.GetAABB()))
                        return false;
                }
            }

            var closestTurretDistance = float.PositiveInfinity;
            foreach (var turret in WeaponManager.GetWeapons<Turret>())
            {
                if (turret.Broken) continue;
                var distanceToTurret = Vector2.Distance(turret.Position, Player.GetWorldPosition());
                closestTurretDistance = Math.Min(distanceToTurret, closestTurretDistance);
            }

            if (closestTurretDistance < 275)
                return false;
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
                    var distanceToPlayer = Vector2.Distance(p.Value.Placeholder.Position, Player.GetWorldPosition());
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
            Player.SetGuardTarget(m_targetPlaceholder.RepresentedObject);
            var bs = Player.GetBotBehaviorSet();
            bs.GuardRange = 1f;
            bs.ChaseRange = 1f;
            Player.SetBotBehaviorSet(bs);
            m_state = EngineerState.GoingToPlaceholder;
        }

        private void CheckArriveTargetPlaceholder()
        {
            if (m_targetPlaceholder.GetAABB().Intersects(Player.GetAABB()) && CanBuildTurret())
            {
                // At the time the builder arrives, another builder may arrived first and already started building
                if (!WeaponManager.GetUntouchedTurretPlaceholders()
                    .Where((p) => p.Key == m_targetPlaceholder.UniqueID)
                    .Any() || !CanBuildTurretHere())
                {
                    Player.SetGuardTarget(null);
                    m_state = EngineerState.Normal;
                    m_buildCooldown = 0f;
                }
                else
                    StartBuildingTurret(m_targetPlaceholder);

                m_targetPlaceholder = null;
            }
        }

        private void StartBuildingTurret(TurretPlaceholder placeholder = null)
        {
            m_state = EngineerState.Building;
            Player.SetInputEnabled(false);
            Player.AddCommand(new PlayerCommand(PlayerCommandType.Sheath));
            Player.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));
            Player.AddCommand(new PlayerCommand(PlayerCommandType.DrawMelee));

            if (placeholder == null)
            {
                var direction = m_availableDirection == AvailableTurretDirection.Left ? TurretDirection.Left : TurretDirection.Right;
                m_placeholder = WeaponManager.CreateTurretPlaceholder(Player, direction);
            }
            else
            {
                m_placeholder = placeholder;
                m_buildTimer = placeholder.BuildProgress * BuildTime;
                WeaponManager.AddBuilderToTurretPlaceholder(placeholder.UniqueID, Player);
            }
        }

        private void StopBuilding()
        {
            m_buildTimer = 0f;
            m_hitTimer = 0f;
            m_buildCooldown = 0f;
            Player.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
            Player.SetInputEnabled(true);
            m_state = EngineerState.Normal;
            WeaponManager.RemoveBuilderFromTurretPlaceholder(m_placeholder.UniqueID);
        }

        private float m_buildTimer = 0f;
        private float m_hitTimer = 0f;
        private TurretPlaceholder m_placeholder;
        private void UpdateBuildingTurret(float elapsed)
        {
            if (IsAttacked())
            {
                StopBuilding(); return;
            }

            m_buildTimer += elapsed;
            m_hitTimer += elapsed;
            m_placeholder.BuildProgress = m_buildTimer / BuildTime;

            if (m_hitTimer >= 700)
            {
                Player.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                ScriptHelper.Timeout(() =>
                {
                    if (!IsBuilding) return;
                    var hitPosition = Player.GetWorldPosition() + Vector2.UnitX * Player.GetFaceDirection() * 12;
                    Game.PlayEffect(EffectName.BulletHitMetal, hitPosition);
                    Game.PlaySound("ImpactMetal", hitPosition);
                }, 215);
                m_hitTimer = 0f;
            }

            if (m_buildTimer >= BuildTime)
            {
                StopBuilding();
                WeaponManager.SpawnTurret(Player, m_placeholder.Direction);
                m_placeholder.Remove();
                m_placeholder = null;
            }
        }
    }
}
