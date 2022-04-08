using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class MutantBot : Bot
    {
        public MutantBot(BotArgs args) : base(args) { }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (args.Removed) return;

            var mod = Player.GetModifiers();
            if (mod.SizeModifier <= Size.Tiny) return;

            var twin1 = Breed();
            var twin2 = Breed();
            
            twin1.DisarmAll();

            twin2.Player.SetWorldPosition(twin1.Position);
            twin1.Decorate(twin2.Player);

            mod.SizeModifier = Size.Tiny;
            mod.MaxHealth /= 2;
            mod.MeleeDamageDealtModifier /= 2;
            twin1.SetModifiers(mod, true);
            twin2.SetModifiers(mod, true);

            var profiles = SplitProfile(Player);
            twin1.Player.SetProfile(profiles[0]);
            twin2.Player.SetProfile(profiles[1]);

            Game.PlayEffect(EffectName.Smack, Position);
            Player.Remove();
        }

        private static readonly ClothingType[] LowerBodyClothingTypes = new ClothingType[] { ClothingType.Feet, ClothingType.Legs, ClothingType.Waist, };
        private static readonly ClothingType[] UpperBodyClothingTypes = new ClothingType[]
        {
            ClothingType.Accesory, ClothingType.ChestOver, ClothingType.ChestUnder, ClothingType.Hands, ClothingType.Head, 
        };
        public static IProfile[] SplitProfile(IPlayer player)
        {
            var bot = BotManager.GetBot(player);
            var profile1 = ScriptHelper.StripUnderwear(bot.GetProfile());
            var profile2 = ScriptHelper.StripUnderwear(bot.GetProfile());
            var stripeableClothingTypes = ScriptHelper.StrippeableClothingTypes(profile1);
            var lowerBodyClothingTypes = stripeableClothingTypes.Where(x => LowerBodyClothingTypes.Any(xx => xx == x)).ToList();
            var upperBodyClothingTypes = stripeableClothingTypes.Where(x => UpperBodyClothingTypes.Any(xx => xx == x)).ToList();

            lowerBodyClothingTypes.ForEach(x => ScriptHelper.Strip(profile1, x));
            upperBodyClothingTypes.ForEach(x => ScriptHelper.Strip(profile2, x));

            return new IProfile[] { profile1, profile2 };
        }

        private Bot Breed()
        {
            var bot = BotManager.SpawnBot(Type, faction: Faction, team: Player.GetTeam(), ignoreFullSpawner: true);
            bot.Position = Position;
            return bot;
        }
    }
}
