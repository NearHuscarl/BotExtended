using System;
using System.Collections.Generic;
using System.Linq;
using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class TermiteProjectile : Projectile
    {
        public TermiteProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override void OnProjectileCreated()
        {
            Instance.DamageDealtModifier = .2f;
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (args.IsPlayer) return;

            var hitObject = Game.GetObject(args.HitObjectID);
            if (hitObject.GetCollisionFilter().CategoryBits != CategoryBits.DynamicG1 || hitObject.Name.Contains("Debris")) return;

            if (!hitObject.Destructable && RandomHelper.Percentage(.06f))
                hitObject.Destroy();

            // x4.5 damage for objects
            hitObject.DealDamage(args.Damage * 3.5f);

            if (!hitObject.DestructionInitiated) return;

            var oBox = hitObject.GetAABB();
            var pBox = Game.GetPlayer(InitialOwnerPlayerID).GetAABB();
            ScriptHelper.Timeout(() =>
            {
                var originalDebrises = Game.GetObjectsByArea(oBox).Where(x => x.GetLinearVelocity().Length() <= 30 && x.Name.Contains("Debris")).ToList();
                var debrises = new List<IObject>();
                var debrisCount = IsShotgunShell ? 2 : RandomHelper.BetweenInt(4, 6);

                if (originalDebrises.Count == 0)
                {
                    for (var i = 0; i < debrisCount; i++)
                        debrises.Add(Game.CreateObject(RandomHelper.GetItem(MechaBot.DebrisList), oBox.Center));
                }
                var debrisCount2 = debrises.Count;
                for (var i = 0; i < debrisCount - debrisCount2; i++)
                    debrises.Add(Game.CreateObject(RandomHelper.GetItem(originalDebrises).Name, oBox.Center));
                originalDebrises.ForEach(o => o.Remove());

                foreach (var o in debrises)
                {
                    var dir = RandomHelper.Direction(10, 180 - 10);
                    var dirToOwner = pBox.Center - o.GetWorldPosition();

                    o.SetLinearVelocity(dir * RandomHelper.Between(30, 40));
                    o.SetAngularVelocity(RandomHelper.Between(-40, 40));

                    // Avoid hitting and disarming the one who fired
                    if (Math.Abs(MathExtension.ToDegree(MathExtension.AngleBetween(dir, dirToOwner))) > 30)
                        o.TrackAsMissile(true);
                }
            }, 17);
        }
    }
}
