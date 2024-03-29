﻿using BotExtended.Bots;
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
        public PushbackPowerup(IPlayer owner, WeaponItem name, MeleeWeaponPowerup powerup) : base(owner, name, powerup) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three) return;

            var arg = args.FirstOrDefault(x => x.IsPlayer);
            if (arg.HitObject == null) return;
            
            var dir = Owner.GetFaceDirection();
            var enemy = BotManager.GetBot(arg.ObjectID);
            if (enemy.Player.IsRemoved) return;
            var enemyPlayer = enemy.Player;
            var oldHealth = enemy.Player.GetHealth();

            enemy.Player.DealDamage(arg.HitDamage); // damage x2
            enemy.Player.SetLinearVelocity(new Vector2(25 * dir, 4));
            Game.PlayEffect(EffectName.CameraShaker, enemy.Position, 4f, 300f, false);
            ScriptHelper.Fall(enemyPlayer);

            var cb = (Events.PlayerDamageCallback)null;
            var hitByStaticObject = false;

            ScriptHelper.RunUntil(() =>
            {
                var pBox = enemyPlayer.GetAABB();
                Game.PlayEffect(EffectName.Steam, RandomHelper.WithinArea(pBox));
                Game.PlayEffect(EffectName.Steam, RandomHelper.WithinArea(pBox));
            }, () => enemyPlayer.IsRemoved || enemyPlayer.IsOnGround || hitByStaticObject, () => cb.Stop());

            cb = Events.PlayerDamageCallback.Start((player, dArgs) =>
            {
                if (player.UniqueID != enemyPlayer.UniqueID) return;
                if (dArgs.DamageType == PlayerDamageEventType.Fall)
                {
                    var pBox = player.GetAABB();
                    var hitObjects = Game.GetObjectsByArea(ScriptHelper.Grow(pBox, 5));
                    
                    player.SetHealth(Math.Min(oldHealth, player.GetHealth() + dArgs.Damage)); // undo impact damage
                    foreach (var hitObject in hitObjects)
                    {
                        if (!ScriptHelper.IsStaticGround(hitObject) && !ScriptHelper.IsDynamicG1(hitObject)) continue;
                        if (hitObject.UniqueID == Owner.UniqueID) continue;
                        
                        if (ScriptHelper.IsDynamicG1(hitObject))
                            hitObject.Destroy();
                        if (hitObject.GetBodyType() == BodyType.Static)
                        {
                            hitObject.SetBodyType(BodyType.Dynamic);
                            hitByStaticObject = true;
                        }
                        if (Game.IsEditorTest)
                        {
                            ScriptHelper.RunIn(() => Game.DrawArea(hitObject.GetAABB(), Color.Blue), 3000);
                        }
                    }
                }
            });
        }
    }
}
