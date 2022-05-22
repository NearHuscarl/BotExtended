using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengePlus.Challenges
{
    public class BuffMeleeChallenge : Challenge
    {
        public BuffMeleeChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All melee weapons (including fist) deal x8 damage, guns deal x0.1 damage."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                mod.ProjectileDamageDealtModifier = DamageDealt.UltraLow;
                mod.MeleeDamageDealtModifier = 8f;
                p.SetModifiers(mod);

                var bs = p.GetBotBehaviorSet();
                bs.SearchItems = SearchItems.Makeshift | SearchItems.Melee | SearchItems.Streetsweeper | SearchItems.Health | SearchItems.Powerups;
                bs.MeleeUsage = true;
                bs.RangedWeaponUsage = false;
                p.SetBotBehaviorSet(bs);
            }
        }
    }
}
