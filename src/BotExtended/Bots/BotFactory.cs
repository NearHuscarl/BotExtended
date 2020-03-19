using BotExtended.Factions;
using SFDGameScriptInterface;
using static BotExtended.GameScript;

namespace BotExtended.Bots
{
    public class BotArgs
    {
        public IPlayer Player;
        public BotType BotType = BotType.None;
        public BotFaction BotFaction = BotFaction.None;
        public BotInfo Info;
    }

    public static class BotFactory
    {
        public static Bot Create(IPlayer player, BotType botType, BotFaction faction)
        {
            Bot bot = null;
            var args = new BotArgs
            {
                Player = player,
                BotType = botType,
                BotFaction = faction,
                Info = GetInfo(botType),
            };

            switch (botType)
            {
                case BotType.Boffin:
                    bot = new BoffinBot(args);
                    break;

                case BotType.Cowboy:
                case BotType.ClownCowboy:
                    bot = new CowboyBot(args);
                    break;

                case BotType.Sheriff:
                    bot = new SheriffBot(args);
                    break;

                case BotType.Engineer:
                    bot = new EngineerBot(args, player.IsBot ? new EngineerBot_Controller() : null);
                    break;

                case BotType.ZombieFat:
                    bot = new ZombieFatBot(args);
                    break;

                case BotType.ZombieFlamer:
                    bot = new ZombieFlamerBot(args);
                    break;

                case BotType.Hacker:
                    bot = new HackerBot(args);
                    break;

                case BotType.Incinerator:
                    bot = new IncineratorBot(args);
                    break;

                case BotType.Kingpin:
                    bot = new KingpinBot(args);
                    break;

                case BotType.Kriegbär:
                    bot = new KriegbärBot(args);
                    break;

                case BotType.Mecha:
                    bot = new MechaBot(args, player.IsBot ? new MechaBot_Controller() : null);
                    break;

                case BotType.MirrorMan:
                    bot = new MirrorManBot(args);
                    break;

                case BotType.SurvivorBiker:
                case BotType.SurvivorCrazy:
                case BotType.SurvivorNaked:
                case BotType.SurvivorRifleman:
                case BotType.SurvivorRobber:
                case BotType.SurvivorTough:
                    bot = new SurvivorBot(args);
                    break;

                case BotType.Teddybear:
                    bot = new TeddybearBot(args);
                    break;

                case BotType.Babybear:
                    bot = new BabybearBot(args);
                    break;

                default:
                    bot = new Bot(args);
                    break;
            }

            return bot;
        }
    }
}
