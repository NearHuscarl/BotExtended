using System;
using System.Collections.Generic;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class ShrinkingProjectile : HoveringProjectile
    {
        public ShrinkingProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            ExplodeRange = 10;
            ExplodeRange2 = 5;
            MinDistanceBeforeHover = 15;
        }

        private static readonly float Radius = 60;
        protected override void OnHover()
        {
            base.OnHover();
            
            Instance.FlagForRemoval();

            var effectAngle = 0f;
            var totalEffects = 18;
            var angleInBetween = 360 / totalEffects;

            for (var i = 0; i < totalEffects; i++)
            {
                var direction = ScriptHelper.GetDirection(MathExtension.ToRadians(effectAngle));
                var position = HoverPosition + direction * Radius;
                
                Game.PlayEffect(EffectName.Electric, position);
                Game.PlaySound("ElectricSparks", position);
                effectAngle += angleInBetween;
            }

            foreach (var bot in BotManager.GetBots())
            {
                if (ScriptHelper.IntersectCircle(bot.Player.GetAABB(), HoverPosition, Radius))
                {
                    _shrinkedPlayers.Add(bot.Player);

                    var mod = bot.Player.GetModifiers();

                    mod.ExplosionDamageTakenModifier += 1; // +100% damage taken
                    mod.MeleeDamageTakenModifier += 1;
                    mod.ProjectileDamageTakenModifier += 1;
                    mod.ProjectileCritChanceTakenModifier += 1;
                    mod.ImpactDamageTakenModifier /= 2; // half damage taken
                    mod.SizeModifier -= 0.25f; // 0.5 / 0.25 = 2 times until tiny
                    bot.SetModifiers(mod, permanent: true);
                }
            }
        }

        private List<IPlayer> _shrinkedPlayers = new List<IPlayer>();
        private float _effectTime = 0f;
        protected override void UpdateHovering(float elapsed)
        {
            base.UpdateHovering(elapsed);

            if (HoverTime > 1000)
                Destroy();

            if (ScriptHelper.IsElapsed(_effectTime, 200))
            {
                _effectTime = Game.TotalElapsedGameTime;
                foreach (var p in _shrinkedPlayers)
                {
                    Game.PlayEffect(EffectName.PlayerLandFull, p.GetWorldPosition());
                }
            }
        }
    }
}
