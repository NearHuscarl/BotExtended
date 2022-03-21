using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.MeleeWeapons
{
    class SplittingPowerup : MeleeWpn
    {
        public override bool IsValidPowerup()
        {
            return IsSharpWeapon(Name);
        }

        public SplittingPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.Splitting) { }

        private static readonly ClothingType[] LowerBodyClothingTypes = new ClothingType[] { ClothingType.Feet, ClothingType.Legs, ClothingType.Waist, };

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three) return;

            var arg = args.FirstOrDefault(x => x.IsPlayer);
            if (arg.ObjectID == 0) return;
            
            var twin1 = BotManager.GetBot(arg.ObjectID);
            if (twin1.Player.IsDead) return;

            var mod = twin1.Player.GetModifiers();
            if (mod.SizeModifier <= Size.Tiny) return;

            twin1.DisarmAll();
            var twin2 = BotManager.SpawnBot(twin1.Type, faction: twin1.Faction, team: twin1.Player.GetTeam(), ignoreFullSpawner: true);

            twin2.Player.SetWorldPosition(twin1.Position);
            twin1.Decorate(twin2.Player);
            
            mod.SizeModifier = Size.Tiny;
            mod.MaxHealth /= 2;
            twin1.SetModifiers(mod, true);
            twin2.SetModifiers(mod, true);

            var profile1 = ScriptHelper.StripUnderwear(twin1.Player.GetProfile());
            var profile2 = ScriptHelper.StripUnderwear(twin1.Player.GetProfile());
            var stripeableClothingTypes = ScriptHelper.StrippeableClothingTypes(profile1);
            var lowerBodyClothingTypes = stripeableClothingTypes.Where(x => LowerBodyClothingTypes.Any(xx => xx == x)).ToList();
            var upperBodyClothingTypes = stripeableClothingTypes.Where(x => LowerBodyClothingTypes.All(xx => xx != x)).ToList();

            lowerBodyClothingTypes.ForEach(x => ScriptHelper.Strip(profile1, x));
            upperBodyClothingTypes.ForEach(x => ScriptHelper.Strip(profile2, x));
            twin1.Player.SetProfile(profile1);
            twin2.Player.SetProfile(profile2);

            twin1.Player.SetLinearVelocity(RandomVelocity(Owner));
            twin2.Player.SetLinearVelocity(RandomVelocity(Owner));
            twin1.Player.SetBotName("Mini " + twin1.Player.Name);
            twin2.Player.SetBotName("Mini " + twin2.Player.Name);

            ScriptHelper.Fall(twin1.Player);
            ScriptHelper.Fall(twin2.Player);

            Game.PlayEffect(EffectName.Gib, twin1.Position);
        }

        private Vector2 RandomVelocity(IPlayer p) { return new Vector2(RandomHelper.Between(6, 10) * p.GetFaceDirection(), RandomHelper.Between(4, 7)); }
    }
}
