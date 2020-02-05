using SFDGameScriptInterface;
using SFDScript.Library;
using System;
using System.Collections.Generic;
using static SFDScript.Library.Mocks.MockObjects;

namespace SFDScript.BotExtended.Bots
{
    public class MechaBot : Bot
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

        public MechaBot() : base()
        {
            UpdateInterval = 0;
            IsSuperCharging = false;
        }

        public override void OnSpawn(List<Bot> others)
        {
            var behavior = Player.GetBotBehaviorSet();
            behavior.SearchForItems = false;
            behavior.DefensiveAvoidProjectilesLevel = 0f;
            behavior.DefensiveBlockLevel = 0f;
            behavior.MeleeWeaponUsage = false;
            behavior.RangedWeaponUsage = false;

            Player.SetBotBehaviorSet(behavior);
            Player.SetHitEffect(PlayerHitEffect.Metal);
        }

        private float m_electricElapsed = 0f;
        protected override void OnUpdate(float elapsed)
        {
            if (Player == null || Player.IsRemoved) return;

            base.OnUpdate(elapsed);

            if (Player.IsDead)
            {
                UpdateCorpse(elapsed);
            }
            else
            {
                if (m_isDeathKneeling)
                {
                    UpdateDealthKneeling(elapsed);
                }
                else
                {
                    var mod = Player.GetModifiers();
                    var healthLeft = mod.CurrentHealth / mod.MaxHealth;

                    if (healthLeft <= 0.4f)
                        UpdateNearDeathEffects(elapsed, healthLeft);

                    UpdateSuperCharge(elapsed);
                }
            }
        }

        public readonly float EnergyToCharge = 9000f;
        public bool IsSuperCharging { get; set; }
        private float m_superchargeEnergy = 0f;
        private float m_chargeTimer = 0f;
        private void UpdateSuperCharge(float elapsed)
        {
            m_superchargeEnergy += elapsed;

            if (m_superchargeEnergy >= EnergyToCharge)
            {
                if (!IsSuperCharging && CanSuperCharge())
                {
                    StartSuperCharge();
                }
                if (IsSuperCharging)
                {
                    UpdateSuperCharge();
                    m_chargeTimer += elapsed;
                    if (m_chargeTimer >= 1500)
                    {
                        StopSuperCharge();
                        m_chargeTimer = 0f;
                    }
                }
            }

            DrawDebugging();
        }

        private bool CanSuperCharge()
        {
            return !Player.IsInMidAir
                && (Player.IsSprinting || Player.IsIdle || Player.IsWalking || Player.IsRunning)
                && GetValidTargetCount() >= 2;
        }

        private Vector2[] GetLineOfSight()
        {
            var lineStart = Player.GetWorldPosition() + Vector2.UnitY * 12f;

            return new Vector2[]
            {
                lineStart,
                lineStart + Player.AimVector * (ChargeMinimumRange + ChargeRange),
            };
        }

        public readonly float ChargeMinimumRange = 30f;
        public readonly float ChargeRange = 60;
        public readonly float ChargeHitRange = 15f;
        private int GetValidTargetCount()
        {
            var los = GetLineOfSight();
            var lineStart = los[0];
            var lineEnd = los[1];

            var rayCastInput = new RayCastInput()
            {
                Types = new Type[1] { typeof(IPlayer) },
            };
            var playersInRange = Game.RayCast(lineStart, lineEnd, rayCastInput);
            var validTargetCount = 0;

            foreach (var playerResult in playersInRange)
            {
                var player = Game.GetPlayer(playerResult.ObjectID);

                if (ScriptHelper.IsTouchingCircle(player.GetAABB(), Player.GetWorldPosition(), ChargeMinimumRange))
                {
                    Game.DrawArea(playerResult.HitObject.GetAABB(), Color.Red);
                    return 0;
                }
                if (!player.IsDead && !player.IsInMidAir)
                {
                    Game.DrawArea(playerResult.HitObject.GetAABB(), Color.Green);
                    validTargetCount++;
                }
            }

            return validTargetCount;
        }

        private void StartSuperCharge()
        {
            Player.SetBotBehaviorActive(false);
            Player.SetLinearVelocity(new Vector2(Player.FacingDirection * 14f, 4f));
            //Player.AddCommand(new PlayerCommand(PlayerCommandType.Inf));
            IsSuperCharging = true;

            if (Game.IsEditorTest)
                Game.PlayEffect(EffectName.CustomFloatText, Player.GetWorldPosition(), "start charging");

            Game.PlayEffect(EffectName.FireNodeTrailGround, Player.GetWorldPosition() + new Vector2(-4, -4));
            Game.PlaySound("Flamethrower", Player.GetWorldPosition());
        }

        private List<IPlayer> chargedPlayers = new List<IPlayer>();
        private void UpdateSuperCharge()
        {
            foreach (var player in Game.GetPlayers())
            {
                if (player == Player) continue;

                var position = player.GetWorldPosition();

                if (ScriptHelper.IsTouchingCircle(player.GetAABB(), Player.GetWorldPosition(), ChargeHitRange)
                    && !chargedPlayers.Contains(player))
                {
                    Game.PlayEffect(EffectName.Electric, position);
                    Game.PlaySound("ElectricSparks", position);
                    player.SetLinearVelocity(RandomHelper.Direction(90, Player.FacingDirection == 1 ? 0 : 180) * 15f);
                    MakePlayerStaggering(player);
                    chargedPlayers.Add(player);
                }
            }

            Game.PlayEffect(EffectName.FireNodeTrailAir, Player.GetWorldPosition() + new Vector2(-4, -4));
            Game.PlayEffect(EffectName.FireNodeTrailAir, Player.GetWorldPosition() + new Vector2(4, -4));
        }

        private void MakePlayerStaggering(IPlayer player)
        {
            // https://www.mythologicinteractiveforums.com/viewtopic.php?f=15&t=3810
            player.SetInputEnabled(false);
            Events.UpdateCallback.Start((t) =>
            {
                player.AddCommand(new PlayerCommand(PlayerCommandType.Stagger));
                player.SetInputEnabled(true);
            }, 1, 1);
        }

        private void StopSuperCharge()
        {
            Player.SetBotBehaviorActive(true);
            IsSuperCharging = false;
            m_superchargeEnergy = 0f;
            chargedPlayers.Clear();

            if (Game.IsEditorTest)
                Game.PlayEffect(EffectName.CustomFloatText, Player.GetWorldPosition(), "stop charging");
        }

        private void DrawDebugging()
        {
            var los = GetLineOfSight();

            Game.DrawCircle(Player.GetWorldPosition(), ChargeMinimumRange, Color.Red);
            Game.DrawCircle(Player.GetWorldPosition(), ChargeHitRange, Color.Cyan);
            if (m_superchargeEnergy >= EnergyToCharge)
            {
                Game.DrawLine(los[0], los[1], Color.Green);
            }
            else
                Game.DrawLine(los[0], los[1], Color.Red);
        }

        private void UpdateCorpse(float elapsed)
        {
            m_electricElapsed += elapsed;

            if (m_electricElapsed >= 1000)
            {
                if (RandomHelper.Boolean())
                {
                    var position = Player.GetWorldPosition();
                    position.X += RandomHelper.Between(-10, 10);
                    position.Y += RandomHelper.Between(-10, 10);

                    Game.PlayEffect(EffectName.Electric, position);

                    if (RandomHelper.Boolean())
                    {
                        Game.PlayEffect(EffectName.Steam, position);
                        Game.PlayEffect(EffectName.Steam, position);
                        Game.PlayEffect(EffectName.Steam, position);
                    }
                    if (RandomHelper.Boolean())
                        Game.PlayEffect(EffectName.Sparks, position);
                    if (RandomHelper.Boolean())
                        Game.PlayEffect(EffectName.Fire, position);

                    Game.PlaySound("ElectricSparks", position);
                    m_electricElapsed = 0f;
                }
                else
                {
                    m_electricElapsed -= RandomHelper.Between(0, m_electricElapsed);
                }
            }
        }
        private void UpdateNearDeathEffects(float elapsed, float healthLeft)
        {
            m_electricElapsed += elapsed;

            if (m_electricElapsed >= 700)
            {
                if (RandomHelper.Boolean())
                {
                    var position = Player.GetWorldPosition();
                    position.X += RandomHelper.Between(-10, 10);
                    position.Y += RandomHelper.Between(-10, 10);

                    if (healthLeft <= 0.2f)
                    {
                        Game.PlayEffect(EffectName.Fire, position);
                        Game.PlaySound("Flamethrower", position);
                    }
                    if (healthLeft <= 0.3f)
                    {
                        Game.PlayEffect(EffectName.Sparks, position);
                    }
                    if (healthLeft <= 0.4f)
                    {
                        if (RandomHelper.Boolean())
                        {
                            Game.PlayEffect(EffectName.Steam, position);
                            Game.PlayEffect(EffectName.Steam, position);
                        }
                        Game.PlayEffect(EffectName.Electric, position);
                        Game.PlaySound("ElectricSparks", position);
                    }
                    m_electricElapsed = 0f;
                }
                else
                {
                    m_electricElapsed -= RandomHelper.Between(0, m_electricElapsed);
                }
            }
        }

        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            var mod = Player.GetModifiers();
            var currentHealth = mod.CurrentHealth;
            var maxHealth = mod.MaxHealth;

            if (currentHealth / maxHealth <= 0.25f)
            {
                var position = Player.GetWorldPosition();
                Game.PlayEffect(EffectName.Electric, position);
                Game.PlaySound("ElectricSparks", position);
            }
        }

        // After the player died, a double body is used for death animation and is the actual body after that
        // the original body is Removed since you cannot "unkill" a player to add additional commands for death animation
        private bool m_useDoubleBody = false;
        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (Player == null) return;

            var selfDestructed = false;

            if (args.Removed)
            {
                SelfDestruct(); selfDestructed = true;
            }
            else
            {
                if (RandomHelper.Boolean())
                {
                    SelfDestruct(); selfDestructed = true;
                }
            }
            if (!m_useDoubleBody && !selfDestructed)
            {
                var newPlayer = Game.CreatePlayer(Player.GetWorldPosition());

                Decorate(newPlayer);
                var newMod = newPlayer.GetModifiers();
                newMod.CurrentHealth = newMod.MaxHealth;

                newPlayer.SetModifiers(newMod);
                newPlayer.SetValidBotEliminateTarget(false);
                newPlayer.SetStatusBarsVisible(false);
                newPlayer.SetNametagVisible(false);
                newPlayer.SetFaceDirection(Player.GetFaceDirection());

                // reset CustomID so when call Player.Remove() it will not called OnDeath() again for the old body
                Player.CustomID = "";
                Player.Remove();
                Player = newPlayer;
                m_useDoubleBody = true;

                StartDeathKneeling();
            }
        }

        private void SelfDestruct()
        {
            var deathPosition = Player.GetWorldPosition();
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
                    var position = Player.GetWorldPosition();
                    position.X += RandomHelper.Between(-10, 10);
                    position.Y += RandomHelper.Between(-10, 10);
                    Game.PlayEffect(effectName, position);
                }
            }

            Game.TriggerExplosion(deathPosition);

            for (var i = 0; i < 4; i++)
            {
                var debrisLinearVelocity = RandomHelper.Direction(15, 165) * 10;
                var debris = Game.CreateObject(RandomHelper.GetItem(DebrisList),
                    deathPosition,
                    0f,
                    debrisLinearVelocity,
                    0f);
                debris.SetMaxFire();

                Game.CreateObject(RandomHelper.GetItem(DebrisList),
                    deathPosition,
                    0f,
                    debrisLinearVelocity * -Vector2.UnitX,
                    0f);

                if (RandomHelper.Boolean())
                {
                    Game.CreateObject(RandomHelper.GetItem(WiringTubeList),
                        deathPosition,
                        0f,
                        RandomHelper.Direction(0, 180) * 6,
                        0f);
                }
            }
        }

        private bool m_isDeathKneeling = false;
        private void StartDeathKneeling()
        {
            if (Player == null) return;

            ScriptHelper.MakeInvincible(Player);
            Player.ClearCommandQueue();
            Player.SetBotBehaviorActive(false);
            m_isDeathKneeling = true;
            Player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));
        }
        private void StopKneeling()
        {
            // Make player damageable again, so it can be exploded when overkilled
            Player.SetModifiers(new PlayerModifiers(defaultValues: true)
            {
                SizeModifier = Info.Modifiers.SizeModifier,
            });
            Player.AddCommand(new PlayerCommand(PlayerCommandType.StopDeathKneel));
            m_isDeathKneeling = false;
            Player.SetBotBehaviorActive(true);
        }

        private float m_kneelingTime = 0f;
        private bool m_hasShotGrenades = false;
        private void UpdateDealthKneeling(float elapsed)
        {
            if (Player.IsDeathKneeling)
            {
                m_kneelingTime += elapsed;

                if (m_kneelingTime >= 1000 && !m_hasShotGrenades)
                {
                    m_grenadeDirection = new Vector2(Player.GetFaceDirection(), 1f);

                    for (uint i = 1; i <= 3; i++)
                    {
                        Events.UpdateCallback.Start(ShootGrenades, 300 * i, 1);
                    }
                    m_hasShotGrenades = true;
                }

                if (m_kneelingTime >= 2500)
                {
                    StopKneeling();
                    Player.Kill();
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
                    StopKneeling();
                    Player.Kill();
                }
            }
        }

        private Vector2 m_grenadeDirection;
        private void ShootGrenades(float elapsed)
        {
            Game.PlaySound("GLauncher", Player.GetWorldPosition());
            Game.SpawnProjectile(ProjectileItem.GRENADE_LAUNCHER, Player.GetWorldPosition() + new Vector2(-5, 20), m_grenadeDirection);
            m_grenadeDirection.X *= 2f;
        }
    }
}
