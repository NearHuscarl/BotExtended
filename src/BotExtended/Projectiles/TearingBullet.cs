using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class TearingBullet : Projectile
    {
        public TearingBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Tearing)
        {
        }

        public float TearingChance { get; private set; }
        public float Tearing2Chance { get; private set; }

        protected override bool OnProjectileCreated()
        {
            if (IsExplosiveProjectile)
            {
                TearingChance = .9f;
                Tearing2Chance = .33f;
            }
            else
            {
                TearingChance = .25f;
                Tearing2Chance = 0f;

                if (IsShotgunShell)
                {
                    TearingChance = TearingChance / ProjectilesPerShell * 1.5f;
                    Tearing2Chance = TearingChance;
                }
            }

            return base.OnProjectileCreated();
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (IsExplosiveProjectile)
            {
                var explosiveArea = ScriptHelper.GrowFromCenter(args.HitPosition, Constants.ExplosionRadius * 2);
                var playersInRadius = Game.GetObjectsByArea<IPlayer>(explosiveArea)
                    .Where((p) => ScriptHelper.IntersectCircle(p.GetAABB(), args.HitPosition, Constants.ExplosionRadius));

                foreach (var player in playersInRadius)
                    Strip(player);
            }
            else
            {
                if (!args.IsPlayer)
                    return;

                var player = Game.GetPlayer(args.HitObjectID);
                Strip(player);
            }
        }

        private void Strip(IPlayer player)
        {
            if (player.IsBurnedCorpse || player.IsRemoved) return;

            var profile = player.GetProfile();
            var stripeableClothingTypes = StrippeableClothingTypes(profile);

            if (RandomHelper.Percentage(TearingChance))
            {
                StripPlayerClothingItem(player, stripeableClothingTypes);
            }
            if (RandomHelper.Percentage(Tearing2Chance))
            {
                StripPlayerClothingItem(player, stripeableClothingTypes);
            }

            var extraDamage = (8 - stripeableClothingTypes.Count) * 1;
            if (!HaveUnderwear(profile)) extraDamage++;
            player.DealDamage(extraDamage);
        }

        private void StripPlayerClothingItem(IPlayer player, List<ClothingType> clothingTypes)
        {
            var profile = player.GetProfile();
            if (!clothingTypes.Any() && !HaveUnderwear(profile)) return;

            if (!clothingTypes.Any())
            {
                var strippedProfile = StripUnderwear(profile);
                player.SetProfile(strippedProfile);
            }
            else
            {
                var clothingTypeToStrip = RandomHelper.GetItem(clothingTypes);
                var strippedProfile = Strip(profile, clothingTypeToStrip);

                player.SetProfile(strippedProfile);
            }
        }

        private enum ClothingType
        {
            Accesory,
            ChestOver,
            ChestUnder,
            Feet,
            Hands,
            Head,
            Legs,
            Waist,
        }

        private bool HaveUnderwear(IProfile profile)
        {
            var skin = profile.Skin;
            var noUnderwear = skin.Color1 == "Skin1" && skin.Color2 == "ClothingBrown"
                || skin.Color1 == "Skin2" && skin.Color2 == "ClothingPink"
                || skin.Color1 == "Skin3" && skin.Color2 == "ClothingLightPink"
                || skin.Color1 == "Skin4" && skin.Color2 == "ClothingLightPink"
                || skin.Color1 == "Skin5" && skin.Color2 == "ClothingLightGray";
            return !noUnderwear;
        }

        private IProfile StripUnderwear(IProfile profile)
        {
            var skin = profile.Skin;
            if (skin.Color1 == "Skin1") profile.Skin.Color2 = "ClothingBrown";
            if (skin.Color1 == "Skin2") profile.Skin.Color2 = "ClothingPink";
            if (skin.Color1 == "Skin3") profile.Skin.Color2 = "ClothingLightPink";
            if (skin.Color1 == "Skin4") profile.Skin.Color2 = "ClothingLightPink";
            if (skin.Color1 == "Skin5") profile.Skin.Color2 = "ClothingLightGray";
            return profile;
        }

        private List<ClothingType> StrippeableClothingTypes(IProfile profile)
        {
            var strippeableClothingTypes = new List<ClothingType>();

            if (profile.Accesory != null && CanBeStripped(ClothingType.Accesory, profile.Accesory.Name))
                strippeableClothingTypes.Add(ClothingType.Accesory);
            if (profile.ChestOver != null && CanBeStripped(ClothingType.ChestOver, profile.ChestOver.Name))
                strippeableClothingTypes.Add(ClothingType.ChestOver);
            if (profile.ChestUnder != null && CanBeStripped(ClothingType.ChestUnder, profile.ChestUnder.Name))
                strippeableClothingTypes.Add(ClothingType.ChestUnder);
            if (profile.Feet != null && CanBeStripped(ClothingType.Feet, profile.Feet.Name))
                strippeableClothingTypes.Add(ClothingType.Feet);
            if (profile.Hands != null && CanBeStripped(ClothingType.Hands, profile.Hands.Name))
                strippeableClothingTypes.Add(ClothingType.Hands);
            if (profile.Head != null && CanBeStripped(ClothingType.Head, profile.Head.Name))
                strippeableClothingTypes.Add(ClothingType.Head);
            if (profile.Legs != null && CanBeStripped(ClothingType.Legs, profile.Legs.Name))
                strippeableClothingTypes.Add(ClothingType.Legs);
            if (profile.Waist != null && CanBeStripped(ClothingType.Waist, profile.Waist.Name))
                strippeableClothingTypes.Add(ClothingType.Waist);

            return strippeableClothingTypes;
        }

        private bool CanBeStripped(ClothingType type, string clothingItem)
        {
            switch (type)
            {
                case ClothingType.Head:
                {
                    switch (clothingItem)
                    {
                        case "Afro":
                        case "Buzzcut":
                        case "Mohawk":
                            return false;
                    }
                    break;
                }
                case ClothingType.Accesory:
                {
                    switch (clothingItem)
                    {
                        case "Moustache":
                        case "Small Moustache":
                            return false;
                    }
                    break;
                }
            }

            return true;
        }

        private IProfile Strip(IProfile profile, ClothingType cloethingType)
        {
            if (cloethingType == ClothingType.Accesory)
                profile.Accesory = null;
            if (cloethingType == ClothingType.ChestOver)
                profile.ChestOver = null;
            if (cloethingType == ClothingType.ChestUnder)
                profile.ChestUnder = null;
            if (cloethingType == ClothingType.Feet)
                profile.Feet = null;
            if (cloethingType == ClothingType.Hands)
                profile.Hands = null;
            if (cloethingType == ClothingType.Head)
                profile.Head = null;
            if (cloethingType == ClothingType.Legs)
                profile.Legs = null;
            if (cloethingType == ClothingType.Waist)
                profile.Waist = null;
            return profile;
        }
    }
}
