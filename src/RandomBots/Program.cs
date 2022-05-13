using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomBots
{
    public partial class Program : GameScriptInterface
    {
        /// <summary>
        /// Placeholder constructor that's not to be included in the ScriptWindow!
        /// </summary>
        public Program() : base(null) { }

        // Config Start

        private static readonly string WIN_STATUS_NOBODY = "Nobody wins";
        private static readonly string WIN_STATUS_NO_HUMAN = "No human players remaining!";
        private static readonly string WIN_STATUS_TEAM = "Team {0} wins";
        private static readonly string WIN_STATUS_PLAYER_WIN = "{0} wins";

        // Config End

        // GameWorld.cs#UpdateGameOver()
        private static readonly uint GAME_OVER_DELAY = 3000;
        private static bool _allowOnlyBots = false;

        public void OnStartup() { Initialize(); }

        private static void Initialize()
        {
            RandomHelper.Initialize();

            Events.PlayerDeathCallback.Start(OnPlayerDealth);
            Events.UserMessageCallback.Start(Command.OnUserMessage);

            _allowOnlyBots = Storage.GetAllowOnlyBots();

            var spawners = RandomHelper.Shuffle(ScriptHelper.GetPlayerSpawners(emptyOnly: true));
            if (spawners.Count > 0)
            {
                var isRandom = Storage.GetIsRandom();
                var index = 0;
                Storage.GetBotsData().ForEach(botData =>
                {
                    if (index > spawners.Count - 1) return;

                    var bot = Game.CreatePlayer(spawners[index].GetWorldPosition());
                    bot.SetProfile(isRandom ? RandomHelper.RandomizeProfile() : botData.Profile);
                    bot.SetBotName(isRandom ? RandomHelper.RandomizeName() : botData.Name);
                    bot.SetTeam(botData.Team);
                    bot.SetBotBehaviorSet(BotBehaviorSet.GetBotBehaviorPredefinedSet(botData.AI));
                    bot.SetBotBehaviorActive(true);
                    index++;
                });
            }
        }

        private static void OnPlayerDealth(IPlayer player)
        {
            CheckGameoverStatus();
        }

        private static void CheckGameoverStatus()
        {
            if (Game.IsGameOver) return;

            // wait a bit to see if there is any extra time change
            Events.UpdateCallback.Start(_ =>
            {
                var alivePlayers = new List<IPlayer>();
                var aliveBots = new List<IPlayer>();
                var lastPlayerSurvive = (IPlayer)null;

                foreach (var p in Game.GetPlayers())
                {
                    if (p.IsDead) continue;
                    if (p.IsBot) aliveBots.Add(p);
                    else alivePlayers.Add(p);
                    lastPlayerSurvive = p;
                }

                if (alivePlayers.Count > 0 || aliveBots.Count > 0)
                {
                    if (alivePlayers.Count == 0 && aliveBots.Count > 1 && !_allowOnlyBots)
                    {
                        Game.SetGameOver(WIN_STATUS_NO_HUMAN);
                        return;
                    }

                    var teamsLeft = ScriptHelper.GetAliveTeams();
                    if (teamsLeft.Count == 1)
                    {
                        var teamLeft = teamsLeft[0];

                        if (teamLeft == PlayerTeam.Independent)
                        {
                            if (alivePlayers.Count + aliveBots.Count == 1)
                                Game.SetGameOver(string.Format(WIN_STATUS_PLAYER_WIN, lastPlayerSurvive.Name));
                        }
                        else
                            Game.SetGameOver(string.Format(WIN_STATUS_TEAM, ScriptHelper.GetTeamNumber(teamLeft)));
                    }
                }
                else
                {
                    Game.SetGameOver(WIN_STATUS_NOBODY);
                }
            }, GAME_OVER_DELAY - 100, 1);
        }
    }
}