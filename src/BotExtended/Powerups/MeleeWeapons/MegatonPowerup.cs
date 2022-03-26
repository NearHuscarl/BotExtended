using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.MeleeWeapons
{
    class MegatonPowerup : MeleeWpn
    {
        public MegatonPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.Megaton) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three) return;

            foreach (var arg in args)
            {
                if (!arg.IsPlayer) continue;

                var enemy = Game.GetPlayer(arg.ObjectID);
                if (enemy.IsRemoved || ScriptHelper.SameTeam(enemy, Owner) || enemy.GetHealth() > 25) continue;

                Kill(enemy);
                break;
            }
        }

        private void Kill(IPlayer enemy)
        {
            var mod = enemy.GetModifiers();
            mod.ImpactDamageTakenModifier = 0.0001f;
            enemy.Kill();
            enemy.SetModifiers(mod);
            enemy.SetNametagVisible(false);
            enemy.SetStatusBarsVisible(false);

            var deflectCount = 0;

            enemy.SetLinearVelocity(new Vector2(25 * Owner.GetFaceDirection(), 25));
            Game.RunCommand("/settime 0.1");
            ScriptHelper.Timeout(() => Game.RunCommand("/settime 1"), 1000);

            var cb = (Events.PlayerDamageCallback)null;
            cb = Events.PlayerDamageCallback.Start((player, dArgs) =>
            {
                if (player.UniqueID != enemy.UniqueID) return;
                if (enemy.GetCorpseHealth() < enemy.GetCorpseMaxHealth()) enemy.SetCorpseHealth(enemy.GetCorpseMaxHealth());
                if (dArgs.DamageType == PlayerDamageEventType.Fall)
                {
                    var accuracyDeflection = 0.2f / 2;
                    var angle = ScriptHelper.GetAngle(Vector2.Normalize(player.GetLinearVelocity()));
                    var finalDirection = RandomHelper.Direction(angle - accuracyDeflection, angle + accuracyDeflection, true);
                    player.SetLinearVelocity(finalDirection * RandomHelper.Between(30, 40));
                    deflectCount++;
                }
            });

            ScriptHelper.RunUntil(() =>
            {
                if (enemy.IsLayingOnGround)
                    enemy.SetLinearVelocity(RandomHelper.Direction(180 - 25, 25) * 30);
                if (enemy.GetLinearVelocity().Length() < 25)
                    enemy.SetLinearVelocity(Vector2.Normalize(enemy.GetLinearVelocity()) * 35);
            }, () => deflectCount > 30 || enemy.IsRemoved, () => cb.Stop());
        }
    }
}
