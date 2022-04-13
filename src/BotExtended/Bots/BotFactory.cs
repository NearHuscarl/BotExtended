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
                case BotType.AssassinMelee:
                case BotType.AssassinRange:
                    bot = new AssassinBot(args);
                    break;

                case BotType.Balista:
                    bot = new BalistaBot(args);
                    break;
                case BotType.Balloonatic:
                    bot = new BalloonaticBot(args);
                    break;

                case BotType.Bandido:
                    bot = new BandidoBot(args);
                    break;

                case BotType.Berserker:
                    bot = new BerserkerBot(args);
                    break;

                case BotType.Biker:
                case BotType.BikerHulk:
                    bot = new BikerBot(args);
                    break;

                case BotType.Boffin:
                    bot = new BoffinBot(args);
                    break;

                case BotType.Cindy:
                    bot = new CindyBot(args);
                    break;

                case BotType.Cowboy:
                case BotType.ClownCowboy:
                    bot = new CowboyBot(args);
                    break;

                case BotType.Cyborg:
                    bot = new CyborgBot(args);
                    break;

                case BotType.Engineer:
                    bot = new EngineerBot(args, player.IsBot ? new EngineerBot_Controller() : null);
                    break;

                case BotType.Farmer:
                    bot = new FarmerBot(args);
                    break;

                case BotType.Gangster:
                case BotType.GangsterHulk:
                    bot = new GangsterBot(args);
                    break;

                case BotType.Hacker:
                    bot = new HackerBot(args);
                    break;

                case BotType.Hitman:
                    bot = new HitmanBot(args);
                    break;

                case BotType.Hunter:
                    bot = new HunterBot(args);
                    break;

                case BotType.Incinerator:
                    bot = new IncineratorBot(args);
                    break;

                case BotType.Fireman:
                case BotType.Pyromaniac:
                    bot = new PyromaniacBot(args);
                    break;

                case BotType.Kingpin:
                    bot = new KingpinBot(args, player.IsBot ? new InfiniteGrab_Controller() : null);
                    break;

                case BotType.Kriegbar:
                    bot = new KriegbärBot(args, player.IsBot ? new KriegbarBot_Controller() : null);
                    break;

                case BotType.Mecha:
                    bot = new MechaBot(args, player.IsBot ? new MechaBot_Controller() : null);
                    break;

                case BotType.MetroCop:
                    bot = new MetroCopBot(args);
                    break;

                case BotType.Mutant:
                case BotType.BigMutant:
                    bot = new MutantBot(args);
                    break;

                case BotType.Nadja:
                    bot = new NadjaBot(args);
                    break;

                case BotType.PoliceChief:
                    bot = new PoliceChiefBot(args);
                    break;

                case BotType.President:
                    bot = new PresidentBot(args);
                    break;

                case BotType.Sheriff:
                    bot = new SheriffBot(args);
                    break;

                case BotType.Smoker:
                    bot = new SmokerBot(args);
                    break;

                case BotType.Spy:
                    bot = new SpyBot(args);
                    break;

                case BotType.Survivor:
                    bot = new SurvivorBot(args);
                    break;

                case BotType.Stripper:
                    bot = new StripperBot(args);
                    break;

                case BotType.Tank:
                    bot = new TankBot(args);
                    break;

                case BotType.Teddybear:
                    bot = new TeddybearBot(args);
                    break;

                case BotType.Thug:
                case BotType.ThugHulk:
                    bot = new ThugBot(args);
                    break;

                case BotType.Translucent:
                    bot = new TranslucentBot(args);
                    break;

                case BotType.Babybear:
                    bot = new BabybearBot(args);
                    break;

                case BotType.ZombieEater:
                    bot = new ZombieEaterBot(args, player.IsBot ? new InfiniteGrab_Controller() : null);
                    break;

                case BotType.ZombieFat:
                    bot = new ZombieFatBot(args);
                    break;

                case BotType.ZombieFlamer:
                    bot = new ZombieFlamerBot(args);
                    break;

                default:
                    bot = new Bot(args);
                    break;
            }

            return bot;
        }
    }
}
