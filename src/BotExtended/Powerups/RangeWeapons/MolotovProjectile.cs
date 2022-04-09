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
    class MolotovProjectile : CustomProjectile
    {
        public MolotovProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            var molotov = CreateCustomProjectile(projectile, "WpnMolotovsThrown", projectile.Velocity / 20);
            var facingDirection = Game.GetPlayer(InitialOwnerPlayerID).FacingDirection;
            molotov.SetAngularVelocity(-facingDirection * 20f);
            
            return molotov;
        }
    }
}
