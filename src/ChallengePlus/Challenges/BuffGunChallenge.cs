using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class BuffGunChallenge : ChallengeBase<BuffGunChallenge.PlayerData>
    {
        public BuffGunChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All guns deal x8 damage, melee weapons deal x0.1 damage."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            var spawnChance = Game.GetWeaponSpawnChances();

            ScriptHelper.EnumToArray<WeaponItem>().ToList().ForEach(w =>
            {
                var type = Mapper.GetWeaponItemType(w);
                if (type == WeaponItemType.Rifle || type == WeaponItemType.Handgun)
                    spawnChance[(short)w] = 99;
                else
                    spawnChance[(short)w] = 1;
            });
            Game.UpdateWeaponSpawnChances(spawnChance);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                mod.MeleeDamageDealtModifier = DamageDealt.UltraLow;
                mod.ProjectileDamageDealtModifier = 8f;
                p.SetModifiers(mod);

                var bs = p.GetBotBehaviorSet();
                bs.AggroRange = 5; // avoid melee
                bs.SearchItems = SearchItems.Primary | SearchItems.Secondary | SearchItems.Streetsweeper | SearchItems.Health | SearchItems.Powerups;
                bs.MeleeWeaponUsage = false;
                bs.RangedWeaponUsage = true;
                p.SetBotBehaviorSet(bs);
            }
        }
        public class PlayerData
        {
            public bool HasRangedWeapon;
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            if (!player.IsBot || player.IsDead) return;

            var pData = GetPlayerData(player.UniqueID);
            if (pData == null) return;

            var hasRangedWeapon = player.Instance.CurrentWeaponDrawn == WeaponItemType.Rifle || player.Instance.CurrentWeaponDrawn != WeaponItemType.Handgun;

            if (!pData.HasRangedWeapon && hasRangedWeapon)
            {
                var bs = player.Instance.GetBotBehaviorSet();
                bs.AggroRange = 0;
                player.Instance.SetBotBehaviorSet(bs);
                pData.HasRangedWeapon = hasRangedWeapon;
            }
            if (pData.HasRangedWeapon && !hasRangedWeapon)
            {
                var bs = player.Instance.GetBotBehaviorSet();
                bs.AggroRange = 5;
                player.Instance.SetBotBehaviorSet(bs);
                pData.HasRangedWeapon = hasRangedWeapon;
            }
        }
    }
}
