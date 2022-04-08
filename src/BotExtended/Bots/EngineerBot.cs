using BotExtended.Library;
using BotExtended.Weapons;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

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

        public static readonly float CreateNewCooldownTime = Game.IsEditorTest ? 3000 : 12000;

        private EngineerBot_Controller m_controller;
        private TurretPlaceholder m_placeholder;

        public EngineerBot(BotArgs args, EngineerBot_Controller controller) : base(args)
        {
            UpdateDelay = 0;

            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }
        }

        private bool m_notifyCooldownOver = false;
        private float m_createNewTurretCooldown = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (m_controller != null)
                m_controller.OnUpdate(elapsed);

            if (m_createNewTurretCooldown < CreateNewCooldownTime)
            {
                m_createNewTurretCooldown += elapsed;
            }
            else if (!m_notifyCooldownOver)
            {
                NotifyCooldownOver();
            }

            if (MaybeIsBuildingTurret())
            {
                UpdateBuildingProgress();
            }
            if (m_placeholder != null)
            {
                Game.DrawArea(m_placeholder.GetAABB());
                // Edge cases: player can be moved when standing on dynamic platforms
                if (!m_placeholder.GetAABB().Intersects(Player.GetAABB()) || m_placeholder.IsRemoved)
                    StopOccupying();
            }

            m_prevTotalAttackSwings = Player.Statistics.TotalMeleeAttackSwings;
        }

        private void NotifyCooldownOver()
        {
            var profile = GetProfile();
            var originalColor = profile.Head.Color1;
            var flashCount = 20;

            for (uint i = 0; i < flashCount; i++)
            {
                var ii = i;

                ScriptHelper.Timeout(() =>
                {
                    var color = profile.Head.Color1;

                    if (color == "ClothingLightYellow")
                        color = "ClothingLightGray";
                    else
                        color = "ClothingLightYellow";

                    if (ii == flashCount-1)
                        color = originalColor;

                    profile.Head.Color1 = color;
                    Player.SetProfile(profile);
                }, 75 * ii);
            }
            m_notifyCooldownOver = true;
        }

        private int m_prevTotalAttackSwings = 0;
        private bool MaybeIsBuildingTurret()
        {
            if (Player.IsCrouching
                && m_prevTotalAttackSwings != Player.Statistics.TotalMeleeAttackSwings
                && IsHoldingEquipment)
            {
                return true;
            }
            return false;
        }

        public bool HasEnoughEnergy
        {
            get { return m_createNewTurretCooldown >= CreateNewCooldownTime; }
        }

        public bool HasEquipment { get { return BuildItems.Contains(Player.CurrentMeleeWeapon.WeaponItem); } }
        private bool IsHoldingEquipment
        {
            get
            {
                return HasEquipment
                    && Player.CurrentWeaponDrawn == WeaponItemType.Melee
                    && Player.CurrentMeleeMakeshiftWeapon.WeaponItem == WeaponItem.NONE;
            }
        }

        public override void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            base.OnDroppedWeapon(arg);

            if (m_controller != null)
                m_controller.OnDroppedWeapon(arg);
        }

        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            base.OnDamage(attacker, args);

            if (m_controller != null)
                m_controller.OnDamage(attacker, args);
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);
            StopOccupying();
        }

        private bool IsNearEdge()
        {
            var start = Position;
            var scanLines = new List<Vector2[]>();
            var deg70 = 1.22173f;

            scanLines.Add(new Vector2[] { start, start - Vector2.UnitY * 5 });
            scanLines.Add(new Vector2[] { start, start - Vector2.UnitY * 5 + Vector2.UnitX * (float)(5 / Math.Cos(deg70)) });
            scanLines.Add(new Vector2[] { start, start - Vector2.UnitY * 5 - Vector2.UnitX * (float)(5 / Math.Cos(deg70)) });

            var rayCastInput = new RayCastInput()
            {
                MaskBits = CategoryBits.StaticGround,
                FilterOnMaskBits = true,
            };

            var hitCount = 0;
            foreach (var l in scanLines)
            {
                var results = Game.RayCast(l[0], l[1], rayCastInput);
                Game.DrawLine(l[0], l[1]);

                foreach (var result in results)
                {
                    if (result.HitObject.GetBodyType() == BodyType.Static
                        && ScriptHelper.IsIndestructible(result.HitObject)
                        && !RayCastHelper.ObjectsBulletCanDestroy.Contains(result.HitObject.Name))
                    {
                        hitCount++;break;
                    }
                }
            }
            return hitCount < 3;
        }

        public bool CanBuildTurretHere()
        {
            if (IsNearEdge())
                return false;

            return true;
        }

        public bool CreateNewTurret()
        {
            if (HasEnoughEnergy)
            {
                if (!CanBuildTurretHere())
                {
                    Game.PlayEffect(EffectName.CustomFloatText, Position, "Cannot build here");
                    return false;
                }

                var direction = Player.FacingDirection == -1 ? TurretDirection.Left : TurretDirection.Right;
                m_placeholder = WeaponManager.CreateTurretPlaceholder(Player, direction);
                m_createNewTurretCooldown = 0f;
                m_notifyCooldownOver = false;
                return true;
            }
            else
            {
                Game.PlayEffect(EffectName.CustomFloatText, Position, "Not enough energy");
                return false;
            }
        }

        public bool IsOccupying { get { return m_placeholder != null; } }

        private void StopOccupying()
        {
            m_buildProgress = 0;

            if (m_placeholder != null)
            {
                WeaponManager.RemoveBuilderFromTurretPlaceholder(m_placeholder.UniqueID);
                m_placeholder = null;
            }
        }

        public override void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos)
        {
            base.OnPlayerKeyInput(keyInfos);

            foreach (var keyInfo in keyInfos)
            {
                if (Player.KeyPressed(VirtualKey.CROUCH_ROLL_DIVE))
                {
                    if (keyInfo.Event == VirtualKeyEvent.Pressed && keyInfo.Key == VirtualKey.SPRINT)
                    {
                        if (BuildItems.Contains(Player.CurrentMeleeWeapon.WeaponItem))
                            CreateNewTurret();
                    }
                }
            }
        }

        private float m_hitCooldown = 0f; // Prevent player spamming attack combo to speedup building progress
        private int m_buildProgress = 0;
        private static readonly int MaxProgress = Game.IsEditorTest ? 8 : 6;
        public float BuildProgress { get { return m_buildProgress / (float)MaxProgress ; } }
        private void UpdateBuildingProgress()
        {
            if (!MakeSurePlaceHolderExists())
                return;

            if (ScriptHelper.IsElapsed(m_hitCooldown, 500))
            {
                m_hitCooldown = Game.TotalElapsedGameTime;

                if (m_placeholder == null)
                    return;

                var hitPosition = Position + Vector2.UnitX * Player.GetFaceDirection() * 12;
                Game.PlayEffect(EffectName.BulletHitMetal, hitPosition);
                Game.PlaySound("ImpactMetal", hitPosition);

                m_buildProgress++;
                m_placeholder.BuildProgress = BuildProgress;

                if (m_buildProgress >= MaxProgress)
                {
                    WeaponManager.SpawnWeapon(BeWeapon.Turret, new TurretArg
                    {
                        Owner = Player,
                        Position = m_placeholder.Position,
                        Direction = m_placeholder.Direction,
                    });

                    if (m_controller != null)
                        m_controller.OnBuildCompleted();

                    m_placeholder.Remove();
                    StopOccupying();
                }
            }
        }

        private bool MakeSurePlaceHolderExists()
        {
            if (m_placeholder != null) return true;

            var untouchPlaceholders = WeaponManager.GetUntouchedTurretPlaceholders();

            if (untouchPlaceholders.Count() > 0)
            {
                foreach (var p in untouchPlaceholders)
                {
                    if (p.Value.Placeholder.GetAABB().Intersects(Player.GetAABB()))
                    {
                        m_placeholder = p.Value.Placeholder;
                        m_buildProgress = (int)Math.Round(m_placeholder.BuildProgress * MaxProgress);
                        WeaponManager.AddBuilderToTurretPlaceholder(m_placeholder.UniqueID, Player);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
