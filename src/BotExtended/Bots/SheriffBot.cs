using BotExtended.Library;
using SFDGameScriptInterface;

namespace BotExtended.Bots
{
    class SheriffBot : CowboyBot
    {
        public SheriffBot(BotArgs args) : base(args)
        {
            PlayerDropWeaponEvent += OnDropWeapon;
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);
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
