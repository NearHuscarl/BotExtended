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
    class PushbackPowerup : MeleeWpn
    {
        public PushbackPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.Pushback) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three) return;

            var arg = args.FirstOrDefault(x => x.IsPlayer);
            if (arg.ObjectID == 0) return;
            
            var dir = Owner.GetFaceDirection();
            var enemy = BotManager.GetBot(arg.ObjectID);
            var enemyPlayer = enemy.Player;
            var oldHealth = enemy.Player.GetHealth();

            enemy.Player.DealDamage(arg.HitDamage); // damage x2
            enemy.Player.SetLinearVelocity(new Vector2(25 * dir, 2));
            Game.PlayEffect(EffectName.CameraShaker, enemy.Position, 4f, 300f, false);
            ScriptHelper.Fall(enemyPlayer);

            var cb1 = (Events.UpdateCallback)null;
            var cb2 = (Events.PlayerDamageCallback)null;
            cb1 = Events.UpdateCallback.Start((e) =>
            {
                if (!enemyPlayer.IsRemoved)
                {
                    var pBox = enemyPlayer.GetAABB();
                    Game.PlayEffect(EffectName.Steam, RandomHelper.WithinArea(pBox));
                    Game.PlayEffect(EffectName.Steam, RandomHelper.WithinArea(pBox));
                }
                if (enemyPlayer.IsOnGround) cb1.Stop();
            }, 30, 2000);
            cb2 = Events.PlayerDamageCallback.Start((player, dArgs) =>
            {
                if (enemyPlayer.IsRemoved || dArgs.DamageType == PlayerDamageEventType.Explosion)
                {
                    cb1.Stop(); cb2.Stop(); return;
                }
                if (player.UniqueID != enemyPlayer.UniqueID) return;
                if (dArgs.DamageType == PlayerDamageEventType.Fall)
                {
                    var pBox = player.GetAABB();
                    var hitObjects = Game.GetObjectsByArea(ScriptHelper.GrowFromCenter(pBox.Center, pBox.Width + 1, pBox.Height));
                    
                    player.SetHealth(Math.Min(oldHealth, player.GetHealth() + dArgs.Damage)); // undo impact damage
                    foreach (var hitObject in hitObjects)
                    {
                        var cbits = hitObject.GetCollisionFilter().CategoryBits;
                        if (hitObject.UniqueID == Owner.UniqueID || cbits != CategoryBits.DynamicG1 && cbits != CategoryBits.StaticGround) continue;
                        if (cbits == CategoryBits.DynamicG1) hitObject.Destroy();
                        if (cbits == CategoryBits.StaticGround) hitObject.SetBodyType(BodyType.Dynamic);
                        if (Game.IsEditorTest)
                        {
                            ScriptHelper.RunIn(() => Game.DrawArea(hitObject.GetAABB(), Color.Blue), 3000);
                        }
                    }
                    cb1.Stop(); cb2.Stop();
                }
            });
        }
    }
}
