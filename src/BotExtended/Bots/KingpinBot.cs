using BotExtended.Library;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class KingpinBot : Bot
    {
        private Controller<KingpinBot> m_controller;

        public KingpinBot(BotArgs args, Controller<KingpinBot> controller) : base(args)
        {
            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }
        }

        public override void OnSpawn(IEnumerable<Bot> others)
        {
            base.OnSpawn(others);

            var bodyguards = others.Where(Q => Q.Type == BotType.Bodyguard || Q.Type == BotType.GangsterHulk).Take(2);
            var bodyguardMaxCount = 2;
            var bodyguardCount = bodyguards.Count();
            var bodyguardMissing = bodyguardMaxCount - bodyguardCount;
            if (bodyguardCount < bodyguardMaxCount)
                bodyguards.Concat(others.Where(Q => Q.Type == BotType.Bodyguard2).Take(bodyguardMissing));

            foreach (var bodyguard in bodyguards)
            {
                bodyguard.Player.SetGuardTarget(Player);
            }
        }

        private float dealDamageInGrabTime = 0f;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (m_controller != null)
                m_controller.OnUpdate(elapsed);

            // crush enemy while grabbing
            if (Player.IsHoldingPlayerInGrab && ScriptHelper.IsElapsed(dealDamageInGrabTime, 120))
            {
                var enemy = Game.GetPlayer(Player.HoldingPlayerInGrabID);

                if (enemy != null)
                {
                    enemy.DealDamage(1.5f);
                    Game.PlayEffect(EffectName.MeleeHitBlunt, enemy.GetWorldPosition());
                    dealDamageInGrabTime = Game.TotalElapsedGameTime;
                }
            }
        }
    }
}
