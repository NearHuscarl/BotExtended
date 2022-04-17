using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class NinjaBot : Bot
    {
        public NinjaBot(BotArgs args) : base(args)
        {
            _oTerminatedCb = Events.ObjectTerminatedCallback.Start(OnObjectsTerminated);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;
            if (Player.IsClimbing)
            {
                var vec = Player.GetLinearVelocity();
                if (vec == Vector2.UnitY * 2)
                    Player.SetLinearVelocity(Vector2.UnitY * 6);
            }
        }

        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            base.OnDamage(attacker, args);

            if (!Player.IsDead && Player.GetHealth() < Player.GetMaxHealth() * 0.1f)
                Hide();
        }

        private static readonly float SmokeRadius = 18;
        public bool IsHiding { get; private set; }
        public bool HasBeenHiding { get; private set; }
        private void Hide()
        {
            if (IsHiding || HasBeenHiding || Player.IsInMidAir) return;

            IsHiding = true;
            HasBeenHiding = true;
            ScriptHelper.MakeInvincible(Player);
            ScriptHelper.Command(Player, PlayerCommandType.StartCrouch, delayTime: 1000);
            ScriptHelper.RunIn(() =>
            {
                var yBottom = Position.Y - 6;
                for (var i = -SmokeRadius; i < SmokeRadius; i += 6)
                {
                    for (var j = -SmokeRadius; j < SmokeRadius; j += 6)
                    {
                        var p = Position + new Vector2(i, j);
                        if (p.Y > yBottom && ScriptHelper.IntersectCircle(p, Position, SmokeRadius))
                        {
                            Game.PlayEffect(EffectName.Dig, p);
                        }
                    }
                }
            }, 500, () => ScriptHelper.Box(Player), 200);
        }

        private Events.ObjectTerminatedCallback _oTerminatedCb;
        private void OnObjectsTerminated(IObject[] obj)
        {
            if (!IsHiding) return;

            var hidingObject = obj.FirstOrDefault(x => x.GetAABB().Width >= 12 && x.GetAABB().Height >= 12 && ScriptHelper.IsDynamicG1(x));
            if (hidingObject == null) return;

            IsHiding = false;
            ScriptHelper.Unbox(Player, hidingObject.GetWorldPosition());
            ResetModifiers();
            SetHealth(Player.GetHealth() + 15, true);
            _oTerminatedCb.Stop();
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);
            _oTerminatedCb.Stop();
        }
    }
}
