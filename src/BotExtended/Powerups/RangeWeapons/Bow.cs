using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class Bow : Projectile
    {
        public Bow(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Bow) { }

        protected override bool OnProjectileCreated()
        {
            return Instance.ProjectileItem == ProjectileItem.BOW;
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (!args.RemoveFlag) return;

            var arrow = Game.CreateObject("BowArrow", Instance.Position, ScriptHelper.GetAngle(Instance.Direction));
            var hitObject = Game.GetObject(args.HitObjectID);
            if (hitObject == null) return;

            var arrows = Game.GetObjectsByArea(arrow.GetAABB())
                .Where(o => ScriptHelper.IsDynamicG2(o) && o.UniqueID != arrow.UniqueID && o.Name == "BowArrow");
            foreach (var a in arrows)
            {
                a.Remove();
                var debris = RandomHelper.Boolean();
                Game.CreateObject(debris ? "BowArrowDebris1" : "BowArrowDebris3", a.GetWorldPosition(), a.GetAngle());
                Game.CreateObject(debris ? "BowArrowDebris2" : "BowArrowDebris4", a.GetWorldPosition(), a.GetAngle());
            }

            var player = ScriptHelper.AsPlayer(hitObject);
            if (player == null)
            {
                var cf = arrow.GetCollisionFilter();
                cf.CategoryBits = CategoryBits.DynamicG1;
                arrow.SetCollisionFilter(cf);
                ScriptHelper.Weld(hitObject, arrow);
                return;
            }
            else
                ScriptHelper.WeldPlayer(player, arrow);

            var bot = BotManager.GetBot(player);
            if (bot != Bot.None && !bot.Player.IsDead)
            {
                var mod = bot.Player.GetModifiers();
                mod.EnergyConsumptionModifier += mod.EnergyConsumptionModifier * 0.2f;
                bot.SetModifiers(mod, true);
            }
        }
    }
}
