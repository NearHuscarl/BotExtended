using BotExtended.Powerups;
using SFDGameScriptInterface;

namespace BotExtended.Bots
{
    class CindyBot : Bot
    {
        public CindyBot(BotArgs args) : base(args) { }

        public override void OnPickedupWeapon(PlayerWeaponAddedArg arg)
        {
            base.OnPickedupWeapon(arg);

            if (arg.WeaponItemType == WeaponItemType.Handgun
                && PowerupManager.GetOrCreatePlayerWeapon(Player).Secondary.Powerup != RangedWeaponPowerup.Stun)
            {
                PowerupManager.SetPowerup(Player, arg.WeaponItem, RangedWeaponPowerup.Stun);
            }
        }
    }
}
