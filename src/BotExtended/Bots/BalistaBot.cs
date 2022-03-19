using BotExtended.Powerups;
using SFDGameScriptInterface;
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

            if (Player.IsDead) return;
            if (IsUsingPowerupWpn && Player.CurrentPrimaryWeapon.TotalAmmo == 0)
                ProjectileManager.SetPowerup(Player, WeaponItem.ASSAULT, RangedWeaponPowerup.None);
            else if (!IsUsingPowerupWpn && (m_rifleReloadTime >= 1 || Player.CurrentPrimaryWeapon.TotalAmmo == 0))
                ProjectileManager.SetPowerup(Player, WeaponItem.GRENADE_LAUNCHER, RangedWeaponPowerup.Spinner);
        }

        private bool IsUsingPowerupWpn
        {
            get
            {
                var playerWpn = ProjectileManager.GetOrCreatePlayerWeapon(Player);
                return playerWpn.Primary.Name == WeaponItem.GRENADE_LAUNCHER
                    && playerWpn.Primary.Powerup == RangedWeaponPowerup.Spinner;
            }
        }

        private void OnReload()
        {
            if (Player.CurrentWeaponDrawn == WeaponItemType.Rifle)
                m_rifleReloadTime++;
        }

        public override void OnPickedupWeapon(PlayerWeaponAddedArg arg)
        {
            base.OnPickedupWeapon(arg);
            m_rifleReloadTime = 0;
        }

        public override void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            base.OnDroppedWeapon(arg);

            if (arg.WeaponItemType != WeaponItemType.Rifle)
                return;

            var weaponObject = Game.GetObject(arg.TargetObjectID);

            if (!Player.IsDead)
            {
                if (weaponObject != null)
                    weaponObject.SetHealth(0);
            }
            else
            {
                // always drops powerup weapon as a reward for players
                var powerupWpn = ProjectileManager.CreateWeapon("WpnGrenadeLauncher", RangedWeaponPowerup.Spinner);

                powerupWpn.SetWorldPosition(weaponObject.GetWorldPosition());
                powerupWpn.SetLinearVelocity(weaponObject.GetLinearVelocity());
                powerupWpn.SetAngularVelocity(weaponObject.GetAngularVelocity());
                weaponObject.Remove();
            }
        }
    }
}
