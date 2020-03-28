using BotExtended.Projectiles;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class BalistaBot : Bot
    {
        public BalistaBot(BotArgs args) : base(args) { }

        private int m_rifleReloadTime = 0;
        private bool m_fireReloadEvent = false;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            LogDebug(m_rifleReloadTime);

            if (Player.IsReloading && !m_fireReloadEvent)
            {
                OnReload();
                m_fireReloadEvent = true;
            }
            if (!Player.IsReloading && m_fireReloadEvent)
                m_fireReloadEvent = false;

            if (m_rifleReloadTime >= 2)
            {
                var playerWpn = ProjectileManager.GetOrCreatePlayerWeapon(Player);

                if (playerWpn.Primary.Name == WeaponItem.GRENADE_LAUNCHER && playerWpn.Primary.Powerup == RangedWeaponPowerup.Spinner)
                    ProjectileManager.SetPowerup(Player, WeaponItem.ASSAULT, RangedWeaponPowerup.None);
                else
                    ProjectileManager.SetPowerup(Player, WeaponItem.GRENADE_LAUNCHER, RangedWeaponPowerup.Spinner);

                m_rifleReloadTime = 0;
            }
        }

        private void OnReload()
        {
            if (Player.CurrentWeaponDrawn == WeaponItemType.Rifle)
                m_rifleReloadTime++;
        }

        public override void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            base.OnDroppedWeapon(arg);

            if (!Player.IsDead)
            {
                var weaponObject = Game.GetObject(arg.TargetObjectID);
                if (weaponObject != null)
                    weaponObject.SetHealth(0);
            }
        }
    }
}
