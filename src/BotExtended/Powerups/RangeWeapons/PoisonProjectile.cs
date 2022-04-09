using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class PoisonProjectile : Projectile
    {
        class PoisonPlayer
        {
            public IPlayer Player;
            public float HitTime = 0f;
            public float LastPoisonTime = 0f;
            public float LastStaggerTime = 0f;
            public float StaggerTime = 0f;
        }

        private static List<PoisonPlayer> _poisonedPlayers = new List<PoisonPlayer>();
        public static readonly float PoisonTime = 30000;

        static PoisonProjectile()
        {
            Events.UpdateCallback.Start((e) =>
            {
                foreach (var item in _poisonedPlayers.ToList())
                {
                    var player = item.Player;
                    if (player.IsDead || player.IsRemoved || ScriptHelper.IsElapsed(item.HitTime, PoisonTime))
                    {
                        _poisonedPlayers.Remove(item);
                        continue;
                    }

                    if (ScriptHelper.IsElapsed(item.LastPoisonTime, 1000))
                    {
                        // TODO: add custom poison effect?
                        player.DealDamage(2);
                        item.LastPoisonTime = Game.TotalElapsedGameTime;
                    }

                    if (ScriptHelper.IsElapsed(item.LastStaggerTime, item.StaggerTime))
                    {
                        if (RandomHelper.Boolean()) ScriptHelper.Fall(player);
                        else ScriptHelper.KneelFall(player);
                        item.LastStaggerTime = Game.TotalElapsedGameTime;
                        item.StaggerTime = RandomHelper.Between(2000, 4000);
                    }
                }
            });
        }

        public PoisonProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            projectile.DamageDealtModifier = 0.1f; // mainly poison damage over time
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            var player = Game.GetPlayer(args.HitObjectID);
            if (player == null || player.IsDead) return;
            
            // prolong poison time instead of stacking damage
            var item = _poisonedPlayers.FirstOrDefault(i => i.Player.UniqueID == player.UniqueID);
            if (item != null)
            {
                item.HitTime = Game.TotalElapsedGameTime;
                return;
            }

            Game.PlayEffect(EffectName.CustomFloatText, player.GetWorldPosition(), "poisoned");
            _poisonedPlayers.Add(new PoisonPlayer
            {
                Player = player,
                HitTime = Game.TotalElapsedGameTime,
            });
        }
    }
}
