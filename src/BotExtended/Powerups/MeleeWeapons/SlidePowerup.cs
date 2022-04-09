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
    class SlidePowerup : MeleeWpn
    {
        public SlidePowerup(IPlayer owner, WeaponItem name, MeleeWeaponPowerup powerup) : base(owner, name, powerup) { }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three) return;

            var arg = args.FirstOrDefault(x => x.IsPlayer);
            if (arg.HitObject == null) return;
            var enemy = Game.GetPlayer(arg.ObjectID);
            var hitDir = Owner.GetFaceDirection();

            // fall to the ground immediately, dont fly around, it feels slow compared to the slide
            enemy.SetLinearVelocity(Vector2.UnitY * -10);
            enemy.DealDamage(arg.HitDamage); // x2 damage
            var mod = enemy.GetModifiers();
            mod.ImpactDamageTakenModifier = 0.0001f;
            enemy.SetModifiers(mod);

            ScriptHelper.RunIf(() =>
            {
                var startPos = enemy.GetWorldPosition();
                var shouldStop = false;
                var maxSlideDistance = 110;
                var isElapsedPlayEffect = ScriptHelper.WithIsElapsed(1);
                Game.RunCommand("/settime 0.1");
                Action Stop = () =>
                {
                    Game.RunCommand("/settime 1");
                    BotManager.GetBot(enemy).ResetModifiers();
                    shouldStop = true;
                };

                ScriptHelper.RunUntil(() =>
                {
                    var position = enemy.GetWorldPosition();
                    var slideDistance = Vector2.Distance(startPos, position);
                    var velocity = MathHelper.Lerp(4, 2, Math.Max(0, slideDistance - maxSlideDistance) / 40);

                    if (enemy.IsRemoved || slideDistance > maxSlideDistance && velocity < 0.02f)
                    {
                        Stop(); return;
                    }

                    enemy.SetWorldPosition(position + Vector2.UnitX * hitDir * velocity);
                    enemy.SetLinearVelocity(Vector2.UnitX * hitDir * 15);
                    
                    if (!enemy.IsInMidAir && isElapsedPlayEffect())
                    {
                        Game.PlaySound("BulletHitFlesh", position);
                        Game.PlayEffect(EffectName.MeleeHitBlunt, position);
                    }

                    var enemyBox = enemy.GetAABB();
                    foreach (var o in Game.GetObjectsByArea(enemyBox))
                    {
                        if (ScriptHelper.IsStaticGround(o))
                        {
                            var hitStatic = hitDir == 1
                            ? MathExtension.Diff(enemyBox.Right, o.GetAABB().Left) < 1 : MathExtension.Diff(enemyBox.Left, o.GetAABB().Right) < 1;
                            if (hitStatic) { Stop(); return; }
                        }

                        if (!ScriptHelper.IsInteractiveObject(o) || Math.Sign(o.GetAABB().Center.X - enemyBox.Center.X) != hitDir) continue;
                        var direction = hitDir == -1 ? RandomHelper.Direction(135, 180) : RandomHelper.Direction(45, 90);

                        o.SetLinearVelocity(direction * RandomHelper.Between(10, 15));
                        o.SetAngularVelocity(RandomHelper.Between(-20, 20));
                    }
                }, () => shouldStop);
            }, () => enemy.IsLayingOnGround, 300);
        }
    }
}
