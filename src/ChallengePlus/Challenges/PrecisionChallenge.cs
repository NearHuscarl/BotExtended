using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class PrecisionChallenge : Challenge
    {
        public PrecisionChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "All guns have laser and pinpoint accuracy."; }
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var bs = p.GetBotBehaviorSet();
                bs.RangedWeaponPrecisionAccuracy = Math.Max(bs.RangedWeaponPrecisionAccuracy, .85f);
                p.SetBotBehaviorSet(bs);
            }
        }

        public override void OnProjectileCreated(IProjectile projectile)
        {
            base.OnProjectileCreated(projectile);

            var owner = Game.GetPlayer(projectile.InitialOwnerPlayerID);
            Vector2 position, direction;

            if (owner == null || !owner.GetWeaponMuzzleInfo(out position, out direction)) return;
            projectile.Direction = direction;
        }

        public override void OnPlayerWeaponAdded(Player player, PlayerWeaponAddedArg args)
        {
            base.OnPlayerWeaponAdded(player, args);

            var p = player.Instance;
            var noPrimaryLaser = !p.CurrentPrimaryRangedWeapon.LazerEquipped && args.WeaponItemType == WeaponItemType.Rifle;
            var noSecondaryLaser = !p.CurrentSecondaryRangedWeapon.LazerEquipped && args.WeaponItemType == WeaponItemType.Handgun;
            
            if (noPrimaryLaser || noSecondaryLaser)
                p.GiveWeaponItem(WeaponItem.LAZER);
        }
    }
}
