using BotExtended.Library;
using BotExtended.Powerups;
using SFDGameScriptInterface;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class BoffinBot : Bot
    {
        RangedWeaponPowerup m_currentPowerup;

        public BoffinBot(BotArgs args) : base(args)
        {
            m_currentPowerup = GetWeapons(Type).First().PrimaryPowerup;
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.GetHealth() <= 80 && m_currentPowerup != RangedWeaponPowerup.GravityDE)
            {
                Game.PlayEffect(EffectName.Electric, Position);
                Game.PlaySound("ElectricSparks", Position);
                Game.CreateDialogue("You underestimate the gravity of the situation", DialogueColor, Player, duration: 3500, showInChat: false);
                m_currentPowerup = RangedWeaponPowerup.GravityDE;
                ResetWeapon();

                var mod = Player.GetModifiers();
                mod.MeleeForceModifier = MeleeForce.Strong;
                SetModifiers(mod);
            }
        }

        public override void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            base.OnDroppedWeapon(arg);

            if (arg.WeaponItemType == WeaponItemType.Rifle && !Player.IsDead)
            {
                if (arg.TargetObjectID != 0)
                {
                    Game.GetObject(arg.TargetObjectID).Remove();
                }
                ResetWeapon();
            }
        }

        private void ResetWeapon()
        {
            var weaponSet = GetWeapons(Type).First();
            ProjectileManager.SetPowerup(Player, weaponSet.Primary, m_currentPowerup);
        }
    }
}
