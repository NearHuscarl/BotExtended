using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class SuicideFighterProjectile : CustomProjectile
    {
        public SuicideFighterProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.SuicideFighter) { }

        private static List<int> _bombers = new List<int>();
        static SuicideFighterProjectile()
        {
            Events.PlayerDeathCallback.Start((player, args) =>
            {
                var index = _bombers.FindIndex(x => x == player.UniqueID);
                if (index == -1) return;
                _bombers.Remove(player.UniqueID);
                Game.TriggerExplosion(player.GetWorldPosition());
            });
        }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            if (!Projectile.IsSlowProjectile(projectile.ProjectileItem)) return null;

            var bot = BotManager.SpawnBot(BotType.SuicideDwarf,
                team: Game.GetPlayer(InitialOwnerPlayerID).GetTeam(),
                ignoreFullSpawner: true,
                triggerOnSpawn: false);
            var fighter = bot.Player;
            var length = Math.Max(fighter.GetAABB().Width, fighter.GetAABB().Height);

            fighter.SetNametagVisible(false);
            fighter.SetWorldPosition(projectile.Position + projectile.Direction * (length + 1));
            fighter.SetLinearVelocity(projectile.Velocity / 20);

            ScriptHelper.Fall(fighter);
            _bombers.Add(fighter.UniqueID);
            projectile.FlagForRemoval();

            // player is invincible while being a projectile
            bot.SetHealth(5000);
            Events.UpdateCallback cb = null;
            cb = Events.UpdateCallback.Start((e) =>
            {
                if (fighter.IsDead || fighter.IsRemoved || fighter.IsIdle && fighter.IsOnGround)
                {
                    bot.ResetModifiers();
                    cb.Stop();
                }
                // must include interval time or the player modifiers will be reset immediately
            }, 20);

            return fighter;
        }
    }
}
