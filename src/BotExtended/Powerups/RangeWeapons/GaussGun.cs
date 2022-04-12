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
    class GaussGun : RangeWpn
    {
        private float _chargeModifier;
        public float ChargeModifier { get; private set; }

        public override float MaxRange { get { return RangedWpns.IsShotgunWpns(Name) ? 300 : float.MaxValue; } }
        public static readonly float MaxCharge = 5000;

        public GaussGun(IPlayer owner, WeaponItem name, RangedWeaponPowerup powerup) : base(owner, name, powerup) { }

        private bool m_prevManualAiming = false;
        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (!IsEquipping) return;

            if (Owner.IsManualAiming && !m_prevManualAiming)
            {
                m_prevManualAiming = true;
            }
            if (!Owner.IsManualAiming && m_prevManualAiming)
            {
                ChargeModifier = 0f;
                m_prevManualAiming = false;
            }

            if (Owner.IsManualAiming)
            {
                if (ChargeModifier <= MaxCharge)
                    ChargeModifier += 1 * elapsed;
                Game.DrawText(ChargeModifier.ToString(), Owner.GetWorldPosition());
            }

            UpdateChargeStatus();

            _chargeModifier = ChargeModifier;
        }

        private void UpdateChargeStatus()
        {
            var pos = Owner.GetWorldPosition();
            if (_chargeModifier < MaxCharge && ChargeModifier >= MaxCharge)
            {
                Game.PlaySound("C4Detonate", pos);
                Game.PlayEffect(EffectName.CustomFloatText, pos, "Fully charged!");
            }
            else if (_chargeModifier < 4000 && ChargeModifier >= 4000)
            {
                Game.PlaySound("StreetsweeperBootingDone", pos);
                Game.PlayEffect(EffectName.CustomFloatText, pos, "+4");
            }
            else if (_chargeModifier < 3000 && ChargeModifier >= 3000)
            {
                Game.PlaySound("StreetsweeperBootingDone", pos);
                Game.PlayEffect(EffectName.CustomFloatText, pos, "+3");
            }
            else if (_chargeModifier < 3000 && ChargeModifier >= 3000)
            {
                Game.PlaySound("StreetsweeperBootingDone", pos);
                Game.PlayEffect(EffectName.CustomFloatText, pos, "+3");
            }
            else if (_chargeModifier < 2000 && ChargeModifier >= 2000)
            {
                Game.PlaySound("StreetsweeperBootingDone", pos);
                Game.PlayEffect(EffectName.CustomFloatText, pos, "+2");
            }
            else if (_chargeModifier < 1000 && ChargeModifier >= 1000)
            {
                Game.PlaySound("StreetsweeperBootingDone", pos);
                Game.PlayEffect(EffectName.CustomFloatText, pos, "+1");
            }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            var maxHitCount = (int)(ChargeModifier / 1000 + 1);
            var props = projectile.GetProperties();

            SpawnLaser(projectile.Position, projectile.Direction,
                playerDamage: props.PlayerDamage,
                objectDamage: props.ObjectDamage,
                maxRange: MaxRange,
                maxHitCount: maxHitCount
                );

            projectile.FlagForRemoval();
        }

        public static RayCastResult SpawnLaser(
            Vector2 start,
            Vector2 direction,
            float playerDamage = 10f,
            float objectDamage = 10f,
            float maxRange = float.MaxValue,
            int maxHitCount = 1
            )
        {
            var end = start + direction * Math.Min(maxRange, ScriptHelper.GetDistanceToEdge(start, direction));
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                ProjectileHit = RayCastFilterMode.True,
                IncludeOverlap = true,
            }).Where(r => r.HitObject != null);

            var hitCount = 0;
            var rcResult = default(RayCastResult);
            foreach (var result in results)
            {
                var hitObject = result.HitObject;
                var cf = hitObject.GetCollisionFilter();

                Game.PlayEffect(EffectName.Electric, result.Position);
                Game.PlaySound("ElectricSparks", result.Position);

                if (ScriptHelper.IsPlatform(hitObject)) continue;

                if (cf.AbsorbProjectile)
                {
                    hitCount++;
                    if (ScriptHelper.IsHardStaticGround(hitObject))
                    {
                        end = result.Position;
                        rcResult = result;
                        break;
                    }
                }
                hitObject.DealDamage(result.IsPlayer ? playerDamage : objectDamage);
                if (hitCount >= maxHitCount)
                {
                    end = result.Position;
                    rcResult = result;
                    break;
                }
            }

            var distance = Vector2.Distance(start, end);
            for (var i = 0f; i <= distance; i += 1.5f)
                Game.PlayEffect(EffectName.ItemGleam, start + direction * i);

            Game.WriteToConsole(rcResult.HitObject == null);
            return rcResult;
        }
    }
}
