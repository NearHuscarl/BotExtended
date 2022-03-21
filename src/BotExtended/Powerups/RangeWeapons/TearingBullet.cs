using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
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

            TearingChance = MathHelper.Clamp(TearingChance * Instance.DamageDealtModifier, 0, 1);
            Tearing2Chance = MathHelper.Clamp(Tearing2Chance * Instance.DamageDealtModifier, 0, 1);

            return base.OnProjectileCreated();
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (!IsExplosiveProjectile)
            {
                if (!args.IsPlayer)
                    return;

                var player = Game.GetPlayer(args.HitObjectID);
                Strip(player);
            }
        }

        protected override void OnProjectileExploded(IEnumerable<IPlayer> playersInRadius)
        {
            base.OnProjectileExploded(playersInRadius);
            foreach (var player in playersInRadius)
                Strip(player);
        }

        private void Strip(IPlayer player)
        {
            if (player.IsBurnedCorpse || player.IsRemoved) return;

            var profile = player.GetProfile();
            var stripeableClothingTypes = ScriptHelper.StrippeableClothingTypes(profile);

            if (RandomHelper.Percentage(TearingChance))
            {
                StripPlayerClothingItem(player, stripeableClothingTypes);
            }
            if (RandomHelper.Percentage(Tearing2Chance))
            {
                StripPlayerClothingItem(player, stripeableClothingTypes);
            }

            var extraDamage = (8 - stripeableClothingTypes.Count) * 1;
            if (!ScriptHelper.HaveUnderwear(profile)) extraDamage++;
            player.DealDamage(extraDamage);
        }

        private void StripPlayerClothingItem(IPlayer player, List<ClothingType> clothingTypes)
        {
            var profile = player.GetProfile();
            if (!clothingTypes.Any() && !ScriptHelper.HaveUnderwear(profile)) return;

            if (!clothingTypes.Any())
            {
                var strippedProfile = ScriptHelper.StripUnderwear(profile);
                player.SetProfile(strippedProfile);
                player.SetBotName("Naked " + player.Name);
            }
            else
            {
                var clothingTypeToStrip = RandomHelper.GetItem(clothingTypes);
                var strippedProfile = ScriptHelper.Strip(profile, clothingTypeToStrip);

                player.SetProfile(strippedProfile);
            }
        }
    }
}
