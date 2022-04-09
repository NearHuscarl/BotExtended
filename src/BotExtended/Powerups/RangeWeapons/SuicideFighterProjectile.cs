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
    class SuicideFighterProjectile : CustomProjectile
    {
        public SuicideFighterProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

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
            var bot = BotManager.SpawnBot(BotType.SuicideDwarf,
                team: Game.GetPlayer(InitialOwnerPlayerID).GetTeam(),
                ignoreFullSpawner: true,
                triggerOnSpawn: false);
            var fighter = bot.Player;
            var length = Math.Max(fighter.GetAABB().Width, fighter.GetAABB().Height);
            var position = projectile.Position + projectile.Direction * (length + 1);
            var dir = Math.Sign(position.X - Game.GetPlayer(InitialOwnerPlayerID).GetWorldPosition().X);

            fighter.SetNametagVisible(false);
            fighter.SetStatusBarsVisible(false);
            fighter.SetWorldPosition(position);
            fighter.SetLinearVelocity(projectile.Velocity / 20);
            fighter.SetFaceDirection(dir);

            ScriptHelper.Fall(fighter);
            _bombers.Add(fighter.UniqueID);
            projectile.FlagForRemoval();

            // player is invincible while being a projectile
            bot.SetHealth(5000);
            ScriptHelper.RunIf(() =>
            {
                bot.ResetModifiers();
                // must include interval time or the player modifiers will be reset immediately
            }, () => fighter.IsDead || fighter.IsRemoved || fighter.IsIdle && fighter.IsOnGround, interval: 25);

            return fighter;
        }
    }
}
