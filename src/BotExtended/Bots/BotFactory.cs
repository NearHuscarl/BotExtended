using BotExtended.Factions;
using SFDGameScriptInterface;
using static BotExtended.GameScript;

namespace BotExtended.Bots
{
    public static class BotFactory
    {
        public static Bot Create(IPlayer player, BotType botType, BotFaction faction)
        {
            Bot bot = null;
            switch (botType)
            {
                case BotType.Cowboy:
                case BotType.ClownCowboy:
                    bot = new CowboyBot();
                    break;

                case BotType.Sheriff:
                    bot = new SheriffBot();
                    break;

                case BotType.Engineer:
                    bot = new EngineerBot();
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

                case BotType.MirrorMan:
                    bot = new MirrorManBot();
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
            bot.Faction = faction;
            bot.Info = GetInfo(botType);

            return bot;
        }
    }
}
