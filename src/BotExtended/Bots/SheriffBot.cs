using SFDGameScriptInterface;

namespace BotExtended.Bots
{
    class SheriffBot : CowboyBot
    {
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            PlayerDropWeaponEvent += OnDropWeapon;
        }

        private void OnDropWeapon(IPlayer previousOwner, IObjectWeaponItem weaponObj, float totalAmmo)
        {
            // Don't give primary weapon again. it's OP
            if (weaponObj.WeaponItemType == WeaponItemType.Rifle)
            {
                Player.GiveWeaponItem(WeaponItem.REVOLVER);
            }
            else
                Player.GiveWeaponItem(weaponObj.WeaponItem);
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            PlayerDropWeaponEvent -= OnDropWeapon;
        }
    }
}
