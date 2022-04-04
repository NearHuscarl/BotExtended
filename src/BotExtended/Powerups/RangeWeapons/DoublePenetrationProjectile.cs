using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class DoublePenetrationProjectile : DoubleTroubleProjectile
    {
        public DoublePenetrationProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.DoublePenetration) { }

        public override bool IsRemoved { get { return Instance.IsRemoved && Instance2.IsRemoved; } }

        private IProjectile Instance2 { get; set; }
        protected override bool OnProjectileCreated()
        {
            var owner = Game.GetPlayer(InitialOwnerPlayerID);
            Instance2 = SpawnOppositeProjectile(owner, Instance) ?? Instance;
            Instance.PowerupBounceActive = true;
            Instance2.PowerupBounceActive = true;
            _initialDir1 = Instance.Direction;
            _initialDir2 = Instance2.Direction;
            return true;
        }

        private Vector2 _initialDir1;
        private Vector2 _initialDir2;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);
            if (Instance.BounceCount > 0)
                Instance.BounceCount = 0;
            if (Instance2.BounceCount > 0)
                Instance2.BounceCount = 0;
            // TODO: improve perf, hitting static tiles and play multiple effects seem to lag the game
            if (Instance.Direction != _initialDir1)
                Instance.Direction = _initialDir1;
            if (Instance2.Direction != _initialDir2)
                Instance2.Direction = _initialDir2;
        }
    }
}
