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
        private static readonly HashSet<WeaponItem> BuildItems = new HashSet<WeaponItem>()
        {
            WeaponItem.LEAD_PIPE,
            WeaponItem.PIPE,
            WeaponItem.HAMMER,
        };

        public float BuildTime { get; private set; }
        public bool IsBuilding { get { return m_state == EngineerState.Building; } }

        public EngineerBot() : base()
        {
            BuildTime = 5000;
        }

        enum EngineerState
        {
            Normal,
            GoingToPlaceholder,
            Building,
        }
        private EngineerState m_state = EngineerState.Normal;

        private float m_buildCooldown = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            switch (m_state)
            {
                case EngineerState.Normal:
                    m_buildCooldown += elapsed;

                    if (m_buildCooldown >= (Game.IsEditorTest ? 3000 : 7000) /*TODO: remove*/
                        && IsInactive())
                    {
                        if (CheckExistingPlaceholder())
                            GoToExistingPlaceholder();
                        else if (CanBuildTurretHere())
                            StartBuildingTurret();
                        else // Cant build turret
                            m_buildCooldown = 0;
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
                }
            }
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);
            WeaponManager.RemoveBuilderFromTurretPlaceholder(Player.UniqueID);
        }

        private bool CanBuildTurretHere()
        {
            if (!BuildItems.Contains(Player.CurrentMeleeWeapon.WeaponItem))
                return false;

            foreach (var bot in BotManager.GetBots<EngineerBot>())
            {
                if (bot.IsBuilding) return false;
            }

            var closestTurretDistance = float.PositiveInfinity;
            foreach (var turret in WeaponManager.GetWeapons<Turret>())
            {
                if (turret.Broken) continue;
                var distanceToTurret = Vector2.Distance(turret.Position, Player.GetWorldPosition());
                closestTurretDistance = Math.Min(distanceToTurret, closestTurretDistance);
            }

            if (closestTurretDistance >= 300)
                return true;
            return false;
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
            if (m_targetPlaceholder.GetAABB().Intersects(Player.GetAABB()) && IsInactive())
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

        private bool IsAttacked()
        {
            return (Player.IsStaggering || Player.IsCaughtByPlayerInDive || Player.IsStunned);
        }
        private bool IsInactive()
        {
            return (Player.IsIdle || Player.IsWalking) && !IsAttacked();
        }

        private void StartBuildingTurret(TurretPlaceholder placeholder = null)
        {
            m_state = EngineerState.Building;
            Player.SetInputEnabled(false);
            Player.AddCommand(new PlayerCommand(PlayerCommandType.Sheath));
            Player.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));
            Player.AddCommand(new PlayerCommand(PlayerCommandType.DrawMelee));

            if (placeholder == null)
                m_placeholder = WeaponManager.CreateTurretPlaceholder(Player);
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
            Game.DrawText(string.Format("{0}/{1}", m_buildTimer, BuildTime), Player.GetWorldPosition());

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
                m_placeholder.Remove();
                m_placeholder = null;
                WeaponManager.SpawnTurret(Player);
            }
        }
    }
}
