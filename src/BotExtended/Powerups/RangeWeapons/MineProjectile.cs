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
    class MineProjectile : CustomProjectile
    {
        public MineProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            var mine = (IObjectMineThrown)CreateCustomProjectile(projectile, "WpnMineThrown", projectile.Velocity / 20);
            var isElapsedCheckGround = ScriptHelper.WithIsElapsed(220);
            var facingDirection = Game.GetPlayer(InitialOwnerPlayerID).FacingDirection;

            mine.SetAngularVelocity(facingDirection * 20f);
            mine.SetDudChance(.5f);

            ScriptHelper.RunUntil(() =>
            {
                if (!isElapsedCheckGround()) return;
                var groundObj = ScriptHelper.GetGroundObject(mine, CategoryBits.StaticGround + CategoryBits.DynamicG1);
                if (groundObj != null)
                {
                    mine.SetAngularVelocity(0);
                    mine.SetAngle(0);
                    mine.TrackAsMissile(false);
                }
            }, () => mine.GetAngularVelocity() == 0);
            
            return mine;
        }
    }
}
