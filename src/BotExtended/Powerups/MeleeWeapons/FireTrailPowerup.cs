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
    class FireTrailPowerup : MeleeWpn
    {
        public override bool IsValidPowerup()
        {
            return IsHitTheFloorWeapon(Name);
        }

        public FireTrailPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.FireTrail) { }

        private float _cooldownTime = 0f;

        protected override void OnMeleeActionChanged(MeleeAction meleeAction, Vector2 hitPosition)
        {
            base.OnMeleeActionChanged(meleeAction, hitPosition);

            if (Owner.IsDead || meleeAction != MeleeAction.Three || !ScriptHelper.IsElapsed(_cooldownTime, Game.IsEditorTest ? 0 : 7000)) return;

            _cooldownTime = Game.TotalElapsedGameTime;

            var timeStarted = 0f;
            var dir = Owner.GetFaceDirection();
            var shouldStop = false;
            var nextPos = hitPosition;

            var velocity = ScriptHelper.GetDirection(dir == 1 ? MathExtension.PIOver4 : MathExtension.PI - MathExtension.PIOver4) * 5;
            Game.SpawnFireNode(hitPosition, velocity, FireNodeType.Flamethrower);

            ScriptHelper.RunUntil(() =>
            {
                if (ScriptHelper.IsElapsed(timeStarted, 40))
                {
                    timeStarted = Game.TotalElapsedGameTime;
                    nextPos += Vector2.UnitX * dir * 10;

                    // handle uneven terrain
                    var start = nextPos + Vector2.UnitY * 10;
                    var end = nextPos - Vector2.UnitY * 10;
                    var rcResults = Game.RayCast(start, end, new RayCastInput
                    {
                        FilterOnMaskBits = true,
                        IncludeOverlap = true,
                        MaskBits = CategoryBits.StaticGround + CategoryBits.DynamicG1 + CategoryBits.DynamicG2 + CategoryBits.Player,
                    }).Where(r => r.HitObject != null);

                    var groundResult = rcResults.FirstOrDefault(x => x.HitObject.GetCollisionFilter().CategoryBits == CategoryBits.StaticGround);

                    // at the edge or something
                    if (groundResult.HitObject == null || groundResult.Fraction == 0 /* deep in static object */)
                    {
                        shouldStop = true; return;
                    }

                    foreach (var result in rcResults)
                    {
                        if (ScriptHelper.IsInteractiveObject(result.HitObject))
                        {
                            result.HitObject.DealDamage(1);
                            result.HitObject.SetMaxFire();
                            Game.SpawnFireNode(result.Position, RandomHelper.Direction(15, 180 - 15) * 3, FireNodeType.Flamethrower);
                        }
                        if (ScriptHelper.IsPlayer(result.HitObject))
                        {
                            ScriptHelper.Timeout(() =>
                            {
                                ScriptHelper.Fall(ScriptHelper.AsPlayer(result.HitObject));
                                result.HitObject.SetLinearVelocity(Vector2.UnitY * 6);
                            }, 150);
                        }
                    }

                    nextPos = groundResult.Position;
                    Game.PlaySound("Flamethrower", nextPos);

                    var pos = nextPos;
                    Events.UpdateCallback.Start((e) => Game.PlayEffect(EffectName.PlayerBurned, pos), 200, 10);
                }
            }, () => Vector2.Distance(hitPosition, nextPos) > 350 || shouldStop);
        }
    }
}
