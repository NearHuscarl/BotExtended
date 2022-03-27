using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class PresidentBot : Bot
    {
        public PresidentBot(BotArgs args) : base(args) { }

        public override void OnSpawn()
        {
            base.OnSpawn();
            foreach (var bot in BotManager.GetBots())
            {
                if (bot.Player.UniqueID != Player.UniqueID && ScriptHelper.SameTeam(Player, bot.Player))
                {
                    var bs = bot.Player.GetBotBehaviorSet();
                    bs.GuardRange = 8;
                    bs.ChaseRange = 10;
                    bot.SetBotBehaviorSet(bs, true);
                    bot.Player.SetGuardTarget(Player);

                    var mod = bot.Player.GetModifiers();
                    mod.ImpactDamageTakenModifier = DamageTaken.VeryResistant;
                    bot.SetModifiers(mod, true);
                }
            }
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            UpdateObesity();
        }

        private bool _isFalling = false;
        private bool _isInMidAir = false;
        private bool _isLayingOnGround = false;
        private Vector2 _fallPosition;
        private Vector2 _linearVelocity;
        private void UpdateObesity()
        {
            var linearVec = Player.GetLinearVelocity();

            if (!_isInMidAir && Player.IsInMidAir && linearVec.Y > 3 && !Player.IsClimbing)
            {
                Player.SetLinearVelocity(linearVec + Vector2.UnitY * 2.5f);
            }
            if (!_isLayingOnGround && Player.IsLayingOnGround)
            {
                var groundObj = ScriptHelper.GetGroundObject(Player, CategoryBits.DynamicG1);
                if (groundObj != null && ScriptHelper.IsDynamicObject(groundObj))
                    groundObj.Destroy();
            }
            if (Player.IsInMidAir && !Player.IsFalling && linearVec.Y <= 0 && _linearVelocity.Y > 0)
            {
                if (RandomHelper.Boolean())
                {
                    ScriptHelper.Fall2(Player);
                }
            }
            if (!_isFalling && Player.IsFalling)
                _fallPosition = Position;

            _isFalling = Player.IsFalling;
            _isInMidAir = Player.IsInMidAir;
            _isLayingOnGround = Player.IsLayingOnGround;
            _linearVelocity = linearVec;
        }

        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            base.OnDamage(attacker, args);

            if (args.DamageType == PlayerDamageEventType.Fall)
            {
                var fallDepth = Vector2.Distance(_fallPosition, Position);
                if (fallDepth < 10) return;
                var maxForce = Math.Min(fallDepth / 3, 25);
                ScriptHelper.CreateEarthquake(ScriptHelper.Grow(Player.GetAABB(), 60, 2), Player, 3, maxForce);
            }
        }
    }
}
