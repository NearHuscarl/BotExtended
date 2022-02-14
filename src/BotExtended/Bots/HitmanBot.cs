using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class HitmanBot : Bot
    {
        private static Vector2 FarAwayPosition = ScriptHelper.GetFarAwayPosition();
        static HitmanBot()
        {
            Game.CreateObject("Concrete07B", new Vector2(FarAwayPosition.X, FarAwayPosition.Y - 30));
        }

        public bool IsHiding { get; private set; }

        public HitmanBot(BotArgs args) : base(args) { }

        public override void OnSpawn()
        {
            base.OnSpawn();

            Events.ProjectileHitCallback.Start((projectile, args) =>
            {
                if (!args.IsPlayer || projectile.InitialOwnerPlayerID != Player.UniqueID) return;

                var player = Game.GetPlayer(args.HitObjectID);
                var isHitFromBehind = Math.Sign(projectile.Velocity.X) == Math.Sign(player.FacingDirection);

                if (isHitFromBehind)
                {
                    var extraDamage = projectile.GetProperties().PlayerDamage * 3;
                    player.DealDamage(extraDamage);
                    Game.PlayEffect(EffectName.CustomFloatText, player.GetWorldPosition(), "critical damage");
                }
                else
                {
                    var healDamage = projectile.GetProperties().PlayerDamage * .5f; // damage from the front only deals half as much
                    player.SetHealth(player.GetHealth() + healDamage);
                }
            });
        }

        private float _checkTime = 0;
        private float _cooldownTime = 0;
        private bool _isCooldown = false;
        private const float CooldownTime = 7000;
        private float _hidingTime = 0;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            _checkTime += elapsed;

            if (_isCooldown) _cooldownTime += elapsed;
            if (_cooldownTime >= CooldownTime) _isCooldown = false;

            if (IsHiding || _isCooldown) return;

            if (_checkTime >= 60)
            {
                foreach (var portal in Game.GetObjects<IObjectPortal>())
                {
                    if (Player.GetAABB().Intersects(portal.GetAABB()))
                        Hide();
                }

                _checkTime = 0;
            }
        }

        private void Hide()
        {
            Game.WriteToConsole("hide");
            Player.SetInputEnabled(false);
            Player.SetNametagVisible(false);
            Player.SetWorldPosition(FarAwayPosition);
            IsHiding = true;
            _hidingTime = RandomHelper.Between(5000, 20000);

            ScriptHelper.Timeout(() =>
            {
                var minDistance = float.MaxValue;
                IObject portalToShow = null;

                foreach (var portal in Game.GetObjects<IObjectPortal>())
                {
                    foreach (var enemy in Game.GetPlayers().Where(p => !ScriptHelper.SameTeam(p, Player)))
                    {
                        var distanceToEnemy = Vector2.Distance(enemy.GetWorldPosition(), portal.GetWorldPosition());
                        if (distanceToEnemy < minDistance)
                        {
                            minDistance = distanceToEnemy;
                            portalToShow = portal;
                        }
                    }
                }
                Show(portalToShow);
            }, (uint)_hidingTime);
        }

        private void Show(IObject portalToShow)
        {
            Game.WriteToConsole("show");
            Player.SetInputEnabled(true);
            Player.SetNametagVisible(true);
            Player.SetWorldPosition(portalToShow.GetWorldPosition());
            IsHiding = false;
            _isCooldown = true;
            _cooldownTime = 0;
        }
    }
}
