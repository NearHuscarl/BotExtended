using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class StripperBot : Bot
    {
        private IPlayer _bouncer;
        private Func<bool> _isElapsedCheckBouncer;
        private static HashSet<int> _occupiedBouncerIDs = new HashSet<int>();

        public StripperBot(BotArgs args) : base(args)
        {
            _isElapsedCheckBouncer = ScriptHelper.WithIsElapsed(1000);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;
            if (_isElapsedCheckBouncer() && _bouncer == null) FindBouncer();
        }

        private void FindBouncer()
        {
            foreach (var bot in BotManager.GetBots())
            {
                var isBodyguard = bot.Type == BotType.Bodyguard
                    || bot.Type == BotType.BikerHulk
                    || bot.Type == BotType.GangsterHulk
                    || bot.Type == BotType.PunkHulk
                    || bot.Type == BotType.ThugHulk;

                if (isBodyguard && ScriptHelper.SameTeam(bot.Player, Player) && !_occupiedBouncerIDs.Contains(bot.Player.UniqueID))
                {
                    var gangsterBot = bot as GangsterBot;
                    if (gangsterBot != null) gangsterBot.CanSetupCamp = false;

                    _bouncer = bot.Player;
                    _occupiedBouncerIDs.Add(_bouncer.UniqueID);

                    bot.GoTo(Player, 10f, 11.5f);
                    _bouncer.SetBotName("Bouncer");

                    if (Game.IsEditorTest)
                    {
                        var color = RandomHelper.GetItem(Color.Red, Color.Yellow, Color.Blue, Color.Green, Color.Magenta, Color.Cyan);

                        ScriptHelper.RunIn(() =>
                        {
                            Game.DrawArea(_bouncer.GetAABB(), color);
                            Game.DrawLine(_bouncer.GetWorldPosition(), Player.GetWorldPosition(), color);
                            Game.DrawArea(Player.GetAABB(), color);
                        }, 2000);
                    }
                    break;
                }
            }
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);
            if (_bouncer != null)
            {
                _bouncer.SetGuardTarget(null);
                _occupiedBouncerIDs.Remove(_bouncer.UniqueID);
            }
        }
    }
}
