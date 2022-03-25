using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;
using BotExtended.Factions;

namespace BotExtended.Bots
{
    public class ThugBot : Bot
    {
        public ThugBot(BotArgs args) : base(args)
        {
            _isElapsedCheckTarget = ScriptHelper.WithIsElapsed(500, 900);
            _isElapsedCheckWpn = ScriptHelper.WithIsElapsed(200);
        }

        static ThugBot()
        {
            Events.ObjectDamageCallback.Start((obj, args) =>
            {
                if (!args.IsPlayer || args.SourceID == 0) return;
                var hitter = BotManager.GetBot(args.SourceID);
                if (hitter.Faction != BotFaction.Thug || hitter.Player.IsDead) return;

                // x3 object damage for thug
                obj.DealDamage(args.Damage * 2);

                if (obj.GetHealth() == 0 && IsLootable(obj))
                {
                    var thugBot = hitter as ThugBot;
                    if (thugBot != null) thugBot.Loot(null);
                    Game.CreateObject(RandomHelper.GetItem(Constants.WeaponNames), obj.GetWorldPosition());
                }
            });
        }

        private Func<bool> _isElapsedCheckTarget;
        private Func<bool> _isElapsedCheckWpn;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);
            if (Faction != BotFaction.Thug) return;

            var lootObject = Player.GetForcedBotTarget();
            if (Game.IsEditorTest)
            {
                if (lootObject != null) Game.DrawLine(Position, lootObject.GetWorldPosition());
            }

            var isUsingRangedWpn = Player.CurrentWeaponDrawn == WeaponItemType.Rifle || Player.CurrentWeaponDrawn == WeaponItemType.Handgun;
            if (isUsingRangedWpn && lootObject != null && _isElapsedCheckWpn())
            {
                ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.Sheath);
            }

            if (!Player.IsDead && _isElapsedCheckTarget())
            {
                var enemiesNearby = AreEnemiesNearby();

                if (!enemiesNearby && lootObject == null)
                {
                    var playerBox = Player.GetAABB();
                    var searchRange = ScriptHelper.GrowFromCenter(playerBox.Center, playerBox.Width + 6, playerBox.Height);
                    var newTarget = Game.GetObjectsByArea(searchRange).FirstOrDefault(IsLootable);
                    if (newTarget != null) Loot(newTarget);
                }
                else if (enemiesNearby || Vector2.Distance(Position, lootObject.GetWorldPosition()) > 30)
                    Loot(null);
            }
        }

        public void Loot(IObject o)
        {
            UseRangeWeapon(o == null, shealthRangeWpn: true);
            Player.SetForcedBotTarget(o);
        }

        private static bool IsLootable(IObject o)
        {
            return o.GetCollisionFilter().CategoryBits == CategoryBits.DynamicG1 && o.Destructable && o.Name != "BarrelExplosive";
        }
    }
}
