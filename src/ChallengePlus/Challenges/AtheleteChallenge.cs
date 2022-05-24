using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class AtheleteChallenge : ChallengeBase<AtheleteChallenge.PlayerData>
    {
        public static readonly float IdleMaxTime = 1500;

        public AtheleteChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Players explode if standing for too long."; }
        }

        private static Vector2 HidePosition = ScriptHelper.GetFarAwayPosition();
        public class PlayerData
        {
            public float LastIdleTime = -1;
            public IObjectText Text;
            public bool IsIdle = false;
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                // Encourage gun usage to explode on manual aiming
                mod.EnergyConsumptionModifier = 0;
                mod.MeleeDamageDealtModifier = DamageDealt.VeryLow;
                p.SetModifiers(mod);

                // faster aiming
                var bs = p.GetBotBehaviorSet();
                bs.MeleeWaitTimeLimitMin = 0;
                bs.MeleeWaitTimeLimitMax = IdleMaxTime;
                bs.RangedWeaponAimShootDelayMin = 0;
                bs.RangedWeaponAimShootDelayMax = IdleMaxTime / 2;
                bs.RangedWeaponPrecisionAimShootDelayMin = 0;
                bs.RangedWeaponPrecisionAimShootDelayMax = IdleMaxTime / 2;
                bs.RangedWeaponPrecisionInterpolateTime = IdleMaxTime / 4;
                p.SetBotBehaviorSet(bs);
            }
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            // a bit of cooldown so our althletes can warmup
            if (Game.TotalElapsedGameTime < 3000) return;

            var pData = GetPlayerData(player.UniqueID);
            if (pData == null) return;
            if (pData.Text == null)
            {
                pData.Text = (IObjectText)Game.CreateObject("Text", HidePosition);
                pData.Text.SetTextColor(Color.Red);
                pData.Text.SetTextScale(1f);
            }

            var p = player.Instance;
            var isIdle = (p.IsIdle || p.IsManualAiming) && player.Velocity.Length() < 1;
            if (!pData.IsIdle && isIdle)
            {
                pData.LastIdleTime = Game.TotalElapsedGameTime;
                pData.Text.SetWorldPosition(player.Position + new Vector2(-6, -10));
            }
            if (pData.IsIdle && !isIdle)
            {
                pData.LastIdleTime = -1;
                pData.Text.SetWorldPosition(HidePosition);
            }
            if (pData.IsIdle == isIdle && isIdle)
            {
                pData.Text.SetText(((IdleMaxTime - Game.TotalElapsedGameTime + pData.LastIdleTime) / 1000).ToString("0.0").Replace(",", "."));
            }

            pData.IsIdle = isIdle;

            if (pData.LastIdleTime != -1 && ScriptHelper.IsElapsed(pData.LastIdleTime, IdleMaxTime))
                Game.TriggerExplosion(player.Position);
        }

        public override void OnPlayerDealth(Player player, PlayerDeathArgs args)
        {
            var pData = GetPlayerData(player.UniqueID);
            if (pData == null) return;
            pData.Text.Remove();

            base.OnPlayerDealth(player, args);
        }
    }
}
