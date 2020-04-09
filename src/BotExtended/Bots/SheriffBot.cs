using BotExtended.Library;
using SFDGameScriptInterface;

namespace BotExtended.Bots
{
    class SheriffBot : CowboyBot
    {
        public SheriffBot(BotArgs args) : base(args) { }

        public override void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            base.OnDroppedWeapon(arg);

            if (!Player.IsDead && Player.CurrentSecondaryWeapon.WeaponItem == WeaponItem.NONE)
            {
                // Don't give primary weapon again. it's OP
                if (arg.WeaponItemType == WeaponItemType.Rifle)
                {
                    Player.GiveWeaponItem(WeaponItem.REVOLVER);
                }
                else
                    Player.GiveWeaponItem(arg.WeaponItem);
            }
        }
    }
}
