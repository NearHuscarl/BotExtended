using System;
using System.Collections.Generic;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class PresentBullet : CustomProjectile
    {
        private static readonly List<string> m_oofs = new List<string>()
        {
            "WpnGrenadesThrown",
            "WpnMolotovsThrown",
            "WpnMineThrown",
        };

        public PresentBullet(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup) { }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            return CreateCustomProjectile(projectile, "XmasPresent00");
        }

        public override void OnProjectileTerminated()
        {
            var position = Instance.GetWorldPosition();

            // normally, the present spawn some random shits upon destroyed. make the present disappeared
            // and spawn something else as a workaround
            Instance.SetWorldPosition(ScriptHelper.GetFarAwayPosition());
            Game.PlayEffect(EffectName.DestroyCloth, position);

            var rndNum = RandomHelper.Between(0, 100);
            if (rndNum < 1) // big oof
            {
                var player = Game.CreatePlayer(position);
                var owner = Game.GetPlayer(InitialOwnerPlayerID);
                var bot = BotManager.SpawnBot(BotType.Santa, player: player,
                    team: owner != null ? owner.GetTeam() : PlayerTeam.Independent,
                    triggerOnSpawn: false);

                bot.Info.SpawnLine = "Surprise motherfucker!";
                BotManager.TriggerOnSpawn(bot);
            }
            if (1 <= rndNum && rndNum < 5)
                Game.CreateObject(RandomHelper.GetItem(m_oofs), position);
            if (5 <= rndNum && rndNum < 30)
                Game.CreateObject(RandomHelper.GetItem(Constants.WeaponNames), position);
        }
    }
}
