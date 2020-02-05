using SFDGameScriptInterface;

namespace SFDScript.BotExtended.Bots
{
    public static class BotFactory
    {
        public static Bot Create(IPlayer player, BotType botType, BotInfo info)
        {
            Bot bot = null;
            switch (botType)
            {
                case BotType.AssassinMelee:
                case BotType.AssassinRange:
                    bot = new AssassinBot();
                    break;

                case BotType.ClownCowboy:
                case BotType.Cowboy:
                    bot = new CowboyBot();
                    break;

                case BotType.ZombieFat:
                    bot = new ZombieFatBot();
                    break;

                case BotType.ZombieFlamer:
                    bot = new ZombieFlamerBot();
                    break;

                case BotType.Hacker:
                    bot = new HackerBot();
                    break;

                case BotType.Incinerator:
                    bot = new IncineratorBot();
                    break;

                case BotType.Kingpin:
                    bot = new KingpinBot();
                    break;

                case BotType.Kriegbär:
                    bot = new KriegbärBot();
                    break;

                case BotType.Mecha:
                    bot = new MechaBot();
                    break;

                case BotType.Teddybear:
                    bot = new TeddybearBot();
                    break;

                case BotType.Babybear:
                    bot = new BabybearBot();
                    break;

                default:
                    bot = new Bot();
                    break;
            }

            bot.Player = player;
            bot.Type = botType;
            bot.Info = info;
            bot.SaySpawnLine();

            return bot;
        }
    }
}
