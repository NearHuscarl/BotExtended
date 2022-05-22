using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class SpecificWpnChallenge : Challenge
    {
        private readonly WeaponItem Weapon;
        private readonly static HashSet<WeaponItem> Blacklist = new HashSet<WeaponItem>
        { WeaponItem.NONE, WeaponItem.SLOWMO_10, WeaponItem.SLOWMO_5, WeaponItem.FIREAMMO, WeaponItem.BOUNCINGAMMO };

        private readonly static List<WeaponItem> AllWeapons = ScriptHelper.EnumToArray<WeaponItem>()
            .Where(w => !Blacklist.Contains(w) && Mapper.GetWeaponItemType(w) != WeaponItemType.Thrown).ToList();
        public SpecificWpnChallenge(ChallengeName name) : base(name) { Weapon = RandomHelper.GetItem(AllWeapons); }

        public override string Description
        {
            get { return string.Format("All supply crates spawn {0} weapon.", Weapon); }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            var spawnChance = Game.GetWeaponSpawnChances();

            ScriptHelper.EnumToArray<WeaponItem>().ToList().ForEach(w => spawnChance[(short)w] = w == Weapon ? 100 : 0);
            Game.UpdateWeaponSpawnChances(spawnChance);
        }
    }
}
