﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class FatigueProjectile : Projectile
    {
        class FatigueInfo
        {
            public IPlayer Player;
            public int ProjectileCount;
            public bool IsExhausted;
        }

        private static Dictionary<int, FatigueInfo> FatigueInfos = new Dictionary<int, FatigueInfo>();
        static FatigueProjectile()
        {
            Events.PlayerDeathCallback.Start((p, _) => FatigueInfos.Remove(p.UniqueID));
            Events.UpdateCallback.Start(_ =>
            {
                foreach (var kv in FatigueInfos)
                {
                    var fatigueInfo = kv.Value;
                    if (fatigueInfo.IsExhausted)
                    {
                        var player = fatigueInfo.Player;
                        if (player.IsInMidAir && !player.IsFalling)
                        {
                            var velocity = player.GetLinearVelocity();
                            if (velocity.Y > 0)
                                player.SetWorldPosition(player.GetWorldPosition() - Vector2.UnitY * .75f);
                        }
                    }
                }

                if (Game.IsEditorTest)
                {
                    foreach (var p in Game.GetPlayers())
                    {
                        if (!FatigueInfos.ContainsKey(p.UniqueID)) continue;
                        var debugText = ScriptHelper.ToDisplayString(
                            FatigueInfos[p.UniqueID].ProjectileCount,
                            FatigueInfos[p.UniqueID].IsExhausted,
                            p.GetModifiers().RunSpeedModifier);
                        Game.DrawText(debugText, p.GetWorldPosition());
                    }
                }
            });
        }

        public override bool IsRemoved { get; protected set; }
        public float FatigueCritChance { get; private set; }
        public float FatigueModifier { get; private set; }

        public FatigueProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override void OnProjectileCreated()
        {
            FatigueCritChance = Math.Max(Instance.GetProperties().CritChance, .1f);
            FatigueModifier = 1;

            if (IsExplosiveProjectile)
            {
                FatigueCritChance = .5f;
                FatigueModifier = 3;
            }
            else if (IsShotgunShell)
            {
                FatigueModifier /= ProjectilesPerShell;
                FatigueCritChance /= ProjectilesPerShell;
            }
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            IsRemoved = true;
            base.OnProjectileHit(args);

            if (!args.IsPlayer || IsExplosiveProjectile) return;

            Fatigue(args.HitObjectID);
        }

        protected override void OnProjectileExploded(IEnumerable<IPlayer> playersInRadius)
        {
            base.OnProjectileExploded(playersInRadius);
            foreach (var player in playersInRadius)
                Fatigue(player.UniqueID);
        }

        private void Fatigue(int playerID)
        {
            var player = Game.GetPlayer(playerID);
            IsRemoved = player == null || player.IsDead;
            if (IsRemoved) return;

            if (!FatigueInfos.ContainsKey(playerID))
            {
                FatigueInfos[playerID] = new FatigueInfo()
                {
                    Player = player,
                    ProjectileCount = 0,
                    IsExhausted = false,
                };
            }
            var fatigueInfo = FatigueInfos[playerID];
            var bot = BotManager.GetBot(player);
            var modifiers = player.GetModifiers();
            var isCrit = RandomHelper.Percentage(FatigueCritChance);

            if (isCrit)
            {
                fatigueInfo.IsExhausted = true;
                modifiers.EnergyRechargeModifier = 0f;
                Game.PlayEffect(EffectName.CustomFloatText, bot.Position, "exhausted");
                FatigueModifier *= 10;
                
                if (IsShotgunShell)
                    FatigueModifier *= ProjectilesPerShell;
            }

            modifiers.RunSpeedModifier -= .1f * FatigueModifier;
            modifiers.SprintSpeedModifier -= .1f * FatigueModifier;
            modifiers.EnergyConsumptionModifier += .2f * FatigueModifier;
            modifiers.MeleeForceModifier -= .1f * FatigueModifier;
            modifiers.MeleeDamageDealtModifier -= .1f * FatigueModifier;

            bot.SetModifiers(modifiers);
            fatigueInfo.ProjectileCount++;

            ScriptHelper.Timeout(() =>
            {
                fatigueInfo.ProjectileCount--;
                IsRemoved = true;

                if (fatigueInfo.ProjectileCount == 0)
                {
                    fatigueInfo.IsExhausted = false;
                    bot.ResetModifiers();
                    bot.ResetBotBehaviorSet();
                }
            }, 6000);
        }
    }
}
