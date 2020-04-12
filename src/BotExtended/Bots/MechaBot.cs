using SFDGameScriptInterface;
using BotExtended.Library;
using System;
using System.Collections.Generic;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class MechaBot : RobotBot
    {
        private static readonly List<string> DebrisList = new List<string> {
            "MetalDebris00A",
            "MetalDebris00B",
            "MetalDebris00C",
            "MetalDebris00D",
            "MetalDebris00E",
            "ItemDebrisDark00",
            "ItemDebrisDark01",
            "ItemDebrisShiny00",
            "ItemDebrisShiny01",
        };
        private static readonly List<string> WiringTubeList = new List<string> {
            "WiringTube00A",
            "WiringTube00A_D",
            "WiringTube00B",
        };

        enum MechaState
        {
            Normal,
            PreparingSupercharge,
            Supercharging,
            DealthKneeling,
        }

        private MechaState m_state;
        private Controller<MechaBot> m_controller;

        public MechaBot(BotArgs args, MechaBot_Controller controller) : base(args)
        {
            UpdateDelay = 0;
            m_state = MechaState.Normal;

            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            var behavior = Player.GetBotBehaviorSet();
            behavior.SearchForItems = false;
            behavior.DefensiveAvoidProjectilesLevel = 0f;
            behavior.DefensiveBlockLevel = 0f;
            behavior.MeleeWeaponUsage = false;
            behavior.RangedWeaponUsage = false;

            Player.SetBotBehaviorSet(behavior);
        }

        protected override void OnUpdate(float elapsed)
        {
            if (Player == null || Player.IsRemoved) return;

            base.OnUpdate(elapsed);

            UpdateChargeStatusColor();

            if (m_controller != null)
                m_controller.OnUpdate(elapsed);

            switch (m_state)
            {
                case MechaState.Normal:
                    UpdateSuperChargeEnergy(elapsed);
                    break;
                case MechaState.PreparingSupercharge:
                    UpdatePrepareSuperCharge(elapsed);
                    break;
                case MechaState.Supercharging:
                    UpdateSuperCharging(elapsed);
                    break;
                case MechaState.DealthKneeling:
                    UpdateDealthKneeling(elapsed);
                    break;
            }
        }

        private float m_lastChargeEnergy;
        private void UpdateChargeStatusColor()
        {
            if (m_lastChargeEnergy < EnergyToCharge && m_superchargeEnergy >= EnergyToCharge)
            {
                var profile = Player.GetProfile();
                profile.Skin.Color2 = "ClothingLightGreen";
                Player.SetProfile(profile);
            }
            if (m_lastChargeEnergy > m_superchargeEnergy)
            {
                var profile = Player.GetProfile();
                profile.Skin.Color2 = "ClothingLightRed";
                Player.SetProfile(profile);
            }
            m_lastChargeEnergy = m_superchargeEnergy;
        }

        public readonly float EnergyToCharge = Game.IsEditorTest ? 3000f : 9000f;
        public bool IsSuperCharging { get { return m_state == MechaState.Supercharging || m_state == MechaState.PreparingSupercharge; } }
        private float m_superchargeEnergy = 0f;
        private float m_chargeTimer = 0f;
        private void UpdateSuperChargeEnergy(float elapsed)
        {
            if (m_superchargeEnergy < EnergyToCharge)
                m_superchargeEnergy += elapsed;

            Game.DrawText(string.Format("{0}/{1}", m_superchargeEnergy, EnergyToCharge), Position + Vector2.UnitY * 30);
        }

        private HashSet<int> chargedPlayers = new HashSet<int>();
        private HashSet<int> chargedObjects = new HashSet<int>();
        private void UpdateSuperCharging(float elapsed)
        {
            foreach (var player in Game.GetPlayers())
            {
                if (player == Player) continue;

                var position = player.GetWorldPosition();
                var angles = new float[] { MathExtension.ToRadians(25), MathExtension.ToRadians(65) };
                if (Player.FacingDirection < 0)
                    angles = ScriptHelper.Flip(angles, FlipDirection.Horizontal);

                if (ScriptHelper.IntersectCircle(player.GetAABB(), Position, ChargeHitRange)
                    && !chargedPlayers.Contains(player.UniqueID))
                {
                    Game.PlayEffect(EffectName.Electric, position);
                    Game.PlaySound("ElectricSparks", position);
                    var direction = RandomHelper.Direction(angles[0], angles[1], true);
                    player.SetLinearVelocity(direction * 15f);
                    ScriptHelper.ExecuteSingleCommand(player, PlayerCommandType.Fall);
                    chargedPlayers.Add(player.UniqueID);
                }
            }

            var area = Player.GetAABB();
            area.Grow(4);
            foreach (var obj in Game.GetObjectsByArea(area))
            {
                if (obj.UniqueID == Player.UniqueID || chargedObjects.Contains(obj.UniqueID) || ScriptHelper.IsPlayer(obj))
                    continue;

                if (ScriptHelper.IsDynamicObject(obj) || RayCastHelper.ObjectsBulletCanDestroy.Contains(obj.Name))
                {
                    if (ScriptHelper.IntersectCircle(obj.GetWorldPosition(), Position, ChargeHitRange))
                    {
                        var v = obj.GetLinearVelocity();
                        obj.SetLinearVelocity(v + Vector2.UnitX * Player.FacingDirection * 25);
                        ScriptHelper.DealDamage(obj, 3);
                        chargedObjects.Add(obj.UniqueID);
                    }
                }
            }

            Game.PlayEffect(EffectName.FireNodeTrailAir, Position + new Vector2(-4, -4));
            Game.PlayEffect(EffectName.FireNodeTrailAir, Position + new Vector2(4, -4));

            m_chargeTimer += elapsed;
            if (m_chargeTimer >= 1500)
            {
                StopSuperCharge();
                m_chargeTimer = 0f;
            }
        }

        public bool CanSuperCharge()
        {
            return m_state == MechaState.Normal && m_superchargeEnergy >= EnergyToCharge && !Player.IsInMidAir;
        }

        public static readonly float ChargeHitRange = 25f;

        public override void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos)
        {
            base.OnPlayerKeyInput(keyInfos);

            foreach (var keyInfo in keyInfos)
            {
                if (keyInfo.Event == VirtualKeyEvent.Pressed && keyInfo.Key == VirtualKey.SPRINT
                    && Player.KeyPressed(VirtualKey.CROUCH_ROLL_DIVE))
                {
                    ExecuteSupercharge();
                }
            }
        }

        public void ExecuteSupercharge()
        {
            if (CanSuperCharge())
                PrepareSuperCharge();
            else if (m_superchargeEnergy < EnergyToCharge)
                Game.PlayEffect(EffectName.CustomFloatText, Position, "Not enough fuel");
        }

        private void PrepareSuperCharge()
        {
            m_state = MechaState.PreparingSupercharge;
            Player.SetInputEnabled(false);
            Player.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));
        }

        private float m_kneelPrepareTime = 0f;
        private float m_kneelPrepareEffectTime = 0f;
        private void UpdatePrepareSuperCharge(float elapsed)
        {
            m_kneelPrepareTime += elapsed;

            if (m_kneelPrepareTime >= 500)
            {
                Player.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
                m_kneelPrepareTime = 0f;

                StartSuperCharge();
            }
            else
            {
                m_kneelPrepareEffectTime += elapsed;
                if (m_kneelPrepareEffectTime >= 90)
                {
                    var pos = Position + Vector2.UnitX * -Player.FacingDirection * 10;
                    Game.PlayEffect(EffectName.Electric, pos);
                    Game.PlaySound("ElectricSparks", pos);
                    m_kneelPrepareEffectTime = 0f;
                }
            }
        }

        private void StartSuperCharge()
        {
            Player.SetLinearVelocity(Vector2.UnitY * 6);
            ScriptHelper.Timeout(() => Player.SetLinearVelocity(
                Vector2.UnitX * Player.FacingDirection * 16f +
                Vector2.UnitY * 3), 30);

            Game.PlayEffect(EffectName.FireNodeTrailGround, Position + new Vector2(-4, -4));
            Game.PlaySound("Flamethrower", Position);
            m_state = MechaState.Supercharging;
        }

        private void StopSuperCharge()
        {
            Player.SetInputEnabled(true);
            m_superchargeEnergy = 0f;
            chargedPlayers.Clear();
            chargedObjects.Clear();
            m_state = MechaState.Normal;
        }

        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            base.OnDamage(attacker, args);

            var mod = Player.GetModifiers();
            var currentHealth = mod.CurrentHealth;
            var maxHealth = mod.MaxHealth;

            if (currentHealth / maxHealth <= 0.25f)
            {
                Game.PlayEffect(EffectName.Electric, Position);
                Game.PlaySound("ElectricSparks", Position);
            }
        }

        // After the player died, a double body is used for death animation and is the actual body after that
        // the original body is Removed since you cannot "unkill" a player to add additional commands for death animation
        private bool m_useDoubleBody = false;
        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (Player == null || m_useDoubleBody) return;

            if (args.Removed) SelfDestruct();
            else
            {
                if (RandomHelper.Boolean()) SelfDestruct();
            }

            if (!m_useDoubleBody && !m_destroyed)
            {
                m_useDoubleBody = true;
                var doubleBody = Game.CreatePlayer(Position);

                Decorate(doubleBody);
                var newMod = doubleBody.GetModifiers();
                newMod.CurrentHealth = newMod.MaxHealth;

                doubleBody.SetModifiers(newMod);
                doubleBody.SetValidBotEliminateTarget(false);
                doubleBody.SetStatusBarsVisible(false);
                doubleBody.SetNametagVisible(false);
                doubleBody.SetFaceDirection(Player.GetFaceDirection());

                BotManager.SetPlayer(this, doubleBody);
                Player.Remove();
                Player = doubleBody;

                StartDeathKneeling();
            }
        }

        private void Decorate(IPlayer existingPlayer)
        {
            existingPlayer.SetProfile(Player.GetProfile());

            existingPlayer.GiveWeaponItem(Player.CurrentMeleeWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentMeleeMakeshiftWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentPrimaryWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentSecondaryWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentThrownItem.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentPowerupItem.WeaponItem);

            existingPlayer.SetTeam(Player.GetTeam());
            existingPlayer.SetModifiers(Player.GetModifiers());
            existingPlayer.SetHitEffect(Player.GetHitEffect());
        }

        private bool m_destroyed = false;
        private void SelfDestruct()
        {
            m_destroyed = true;
            var effects = new List<Tuple<string, int>>() {
                    Tuple.Create(EffectName.BulletHitMetal, 1),
                    Tuple.Create(EffectName.Steam, 2),
                    Tuple.Create(EffectName.Electric, 4),
                };

            foreach (var effect in effects)
            {
                var effectName = effect.Item1;
                var count = effect.Item2;

                for (var i = 0; i < count; i++)
                {
                    var position = RandomHelper.WithinArea(Player.GetAABB());
                    Game.PlayEffect(effectName, position);
                }
            }

            for (var i = 0; i < 4; i++)
            {
                var debrisLinearVelocity = RandomHelper.Direction(15, 165) * 10;
                var debris = Game.CreateObject(RandomHelper.GetItem(DebrisList),
                    Position,
                    0f,
                    debrisLinearVelocity,
                    0f);
                debris.SetMaxFire();

                Game.CreateObject(RandomHelper.GetItem(DebrisList),
                    Position,
                    0f,
                    debrisLinearVelocity * -Vector2.UnitX,
                    0f);

                if (RandomHelper.Boolean())
                {
                    Game.CreateObject(RandomHelper.GetItem(WiringTubeList),
                        Position,
                        0f,
                        RandomHelper.Direction(0, 180) * 6,
                        0f);
                }
            }

            Game.TriggerExplosion(Position);
        }

        private void StartDeathKneeling()
        {
            if (Player == null) return;

            ScriptHelper.MakeInvincible(Player);
            Player.ClearCommandQueue();
            Player.SetBotBehaviorActive(false);
            Player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));
            m_state = MechaState.DealthKneeling;
        }
        private void StopKneelingAndDie()
        {
            // Make player damageable again, so it can be exploded when overkilled
            Player.SetModifiers(new PlayerModifiers(defaultValues: true)
            {
                SizeModifier = Info.Modifiers.SizeModifier,
            });
            Player.AddCommand(new PlayerCommand(PlayerCommandType.StopDeathKneel));
            Player.SetBotBehaviorActive(true);
            Player.Kill();
        }

        private float m_kneelingTime = 0f;
        private bool m_hasShotGrenades = false;
        private void UpdateDealthKneeling(float elapsed)
        {
            if (Player.IsDeathKneeling)
            {
                m_kneelingTime += elapsed;
                if (m_kneelingTime >= 600 && !m_hasShotGrenades)
                {
                    var grenadeDirection = new Vector2(Player.GetFaceDirection(), 1f);

                    for (uint i = 1; i <= 3; i++)
                    {
                        ScriptHelper.Timeout(() =>
                        {
                            if (Player.IsRemoved) return;
                            Game.PlaySound("GLauncher", Position);
                            Game.SpawnProjectile(ProjectileItem.GRENADE_LAUNCHER, Position + new Vector2(-5, 20), grenadeDirection);
                            grenadeDirection.X *= 2f;
                        }, 300 * i);
                    }
                    m_hasShotGrenades = true;
                }

                if (m_kneelingTime >= 2500)
                {
                    StopKneelingAndDie();
                }
            }
            else
            {
                if (!m_hasShotGrenades)
                {
                    StartDeathKneeling();
                    m_kneelingTime = 0f;
                }
                else
                {
                    StopKneelingAndDie();
                }
            }
        }
    }
}
