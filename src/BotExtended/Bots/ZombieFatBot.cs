using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class ZombieFatBot : Bot
    {
        private static readonly int InfectRadius = 30;

        public ZombieFatBot(BotArgs args) : base(args) { }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            var center = Position + Vector2.UnitY * 5;
            var hitbox = Player.GetAABB();

            if (Game.IsEditorTest)
                ScriptHelper.RunIn(() => Game.DrawCircle(center, InfectRadius, Color.Cyan), 3000);

            foreach (var bot in BotManager.GetBots())
            {
                if (ScriptHelper.IntersectCircle(bot.Player.GetAABB(), center, InfectRadius))
                {
                    bot.Infect(InfectTeam);
                    Game.ShowChatMessage(bot.Player.Name + " is infected");
                }
            }

            Game.TriggerExplosion(Position);

            for (var i = 0; i < 5; i++)
            {
                Game.CreateObject(RandomHelper.GetItem(Constants.Giblets), RandomHelper.WithinArea(hitbox),
                    RandomHelper.Between(0, MathHelper.TwoPI));
                Game.PlayEffect(EffectName.Gib, RandomHelper.WithinArea(hitbox));
            }
        }
    }
}
