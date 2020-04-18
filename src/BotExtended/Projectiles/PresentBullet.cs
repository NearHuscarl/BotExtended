using System;
using System.Collections.Generic;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class PresentBullet : CustomProjectile
    {
        private static readonly List<string> m_presents = new List<string>()
        {
            "XmasPresent00",
            "WpnPistol",
            "WpnPistol45",
            "WpnSilencedPistol",
            "WpnMachinePistol",
            "WpnMagnum",
            "WpnRevolver",
            "WpnPumpShotgun",
            "WpnDarkShotgun",
            "WpnTommygun",
            "WpnSMG",
            "WpnM60",
            "WpnPipeWrench",
            "WpnChain",
            "WpnWhip",
            "WpnHammer",
            "WpnKatana",
            "WpnMachete",
            "WpnChainsaw",
            "WpnKnife",
            "WpnSawedoff",
            "WpnBat",
            "WpnBaton",
            "WpnShockBaton",
            "WpnLeadPipe",
            "WpnUzi",
            "WpnSilencedUzi",
            "WpnBazooka",
            "WpnAxe",
            "WpnAssaultRifle",
            "WpnMP50",
            "WpnSniperRifle",
            "WpnCarbine",
            "WpnFlamethrower",
            "ItemPills",
            "ItemMedkit",
            "ItemSlomo5",
            "ItemSlomo10",
            "ItemStrengthBoost",
            "ItemSpeedBoost",
            "ItemLaserSight",
            "ItemBouncingAmmo",
            "ItemFireAmmo",
            "WpnGrenades",
            "WpnMolotovs",
            "WpnMines",
            "WpnShuriken",
            "WpnBow",
            "WpnFlareGun",
            "WpnGrenadeLauncher",
        };
        private static readonly List<string> m_oofs = new List<string>()
        {
            "WpnGrenadesThrown",
            "WpnMolotovsThrown",
            "WpnMineThrown",
        };

        public PresentBullet(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Present) { }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            switch (projectile.ProjectileItem)
            {
                case ProjectileItem.BAZOOKA:
                case ProjectileItem.GRENADE_LAUNCHER:
                    return null;
                default:
                    return CreateCustomProjectile(projectile, "XmasPresent00");
            }
        }

        public override void OnProjectileHit()
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
            {
                Game.CreateObject(RandomHelper.GetItem(m_oofs), position);
            }
            if (5 <= rndNum && rndNum < 30)
            {
                Game.CreateObject(RandomHelper.GetItem(m_presents), position);
            }
        }
    }
}
