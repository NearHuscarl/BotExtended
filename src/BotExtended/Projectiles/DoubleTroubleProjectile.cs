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
    class DoubleTroubleProjectile : Projectile
    {
        public DoubleTroubleProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.DoubleTrouble)
        {
        }

        public override bool IsRemoved { get { return true; } }

        protected override bool OnProjectileCreated()
        {
            var owner = Game.GetPlayer(Instance.InitialOwnerPlayerID);
            Vector2 position;
            Vector2 direction;

            if (owner.GetWeaponMuzzleInfo(out position, out direction))
            {
                var secondProjDirection = Vector2.Negate(direction);
                var start = position;
                var end = position + secondProjDirection * 20;
                var results = Game.RayCast(end, start, new RayCastInput()
                {
                    FilterOnMaskBits = true,
                    MaskBits = CategoryBits.Player,
                    IncludeOverlap = true,
                });

                foreach (var r in results)
                {
                    if (r.HitObject.UniqueID == owner.UniqueID)
                    {
                        var spawnPosition = r.Position + secondProjDirection * 1f;
                        // TODO: make a bug report and hope gurt will fix it
                        ScriptHelper.Timeout(() =>
                        {
                            Game.SpawnProjectile(Instance.ProjectileItem, spawnPosition, secondProjDirection,
                                ScriptHelper.GetPowerup(Instance));
                        }, 0);

                        if (Game.IsEditorTest)
                        {
                            ScriptHelper.RunIn(() =>
                            {
                                Game.DrawLine(start, end);
                                Game.DrawArea(owner.GetAABB(), Color.Red);
                                Game.DrawCircle(spawnPosition, .5f, Color.Green);
                            }, 1500);
                        }
                        break;
                    }
                }
            }

            return true;
        }
    }
}
