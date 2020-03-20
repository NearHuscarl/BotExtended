using BotExtended.Library;
using BotExtended.Projectiles;
using SFDGameScriptInterface;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class BoffinBot : Bot
    {
        RangedWeaponPowerup m_currentPowerup;

        public BoffinBot(BotArgs args) : base(args)
        {
            PlayerDropWeaponEvent += OnDropWeapon;
            m_currentPowerup = GetWeapons(Type).First().PrimaryPowerup;
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.GetHealth() <= 50 && m_currentPowerup != RangedWeaponPowerup.GravityDE)
            {
                // Wait until next frame to re-equip the same gun so the custom DropEvent can diff weapon properly (and fire)
                ScriptHelper.Timeout(() =>
                {
                    Game.PlayEffect(EffectName.Electric, Position);
                    Game.PlaySound("ElectricSparks", Position);
                    Game.CreateDialogue("You underestimate the gravity of the situation", DialogueColor, Player, duration: 3500, showInChat: false);
                    m_currentPowerup = RangedWeaponPowerup.GravityDE;
                    ResetWeapon();
                    Player.SetStrengthBoostTime(1000 * 60 * 1);
                }, 0);
            }
        }

        private void OnDropWeapon(IPlayer previousOwner, IObjectWeaponItem weaponObj, float totalAmmo)
        {
            if (weaponObj.WeaponItemType == WeaponItemType.Rifle)
            {
                weaponObj.Remove();
                ResetWeapon();
            }
        }

        private void ResetWeapon()
        {
            var weaponSet = GetWeapons(Type).First();

            Player.GiveWeaponItem(weaponSet.Primary);
            ProjectileManager.SetPrimaryPowerup(Player, weaponSet.Primary, m_currentPowerup);
        }

        public override void OnDeath(PlayerDeathArgs args)
        {
            PlayerDropWeaponEvent -= OnDropWeapon;
            base.OnDeath(args);
        }
    }
}
