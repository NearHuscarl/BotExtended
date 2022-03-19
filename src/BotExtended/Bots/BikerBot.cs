using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;
using BotExtended.Factions;

namespace BotExtended.Bots
{
    public class GatherSpot
    {
        public IObject Spot;
        public float UpdateTime;
    }

    public class BikerBot : Bot
    {
        public BikerBot(BotArgs args) : base(args) { }

        private static Dictionary<PlayerTeam, GatherSpot> GatherSpots = new Dictionary<PlayerTeam, GatherSpot>
        {
            { PlayerTeam.Team1, null },
            { PlayerTeam.Team2, null },
            { PlayerTeam.Team3, null },
            { PlayerTeam.Team4, null },
        };

        public override void OnSpawn()
        {
            base.OnSpawn();
            if (GatherSpot == null) ChangeGatherSpot();
        }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Faction != BotFaction.Biker || Player.IsDead) return;

            foreach (var arg in args)
            {
                SetHealth(Player.GetHealth() + 4, true);

                if (arg.IsPlayer && RandomHelper.Percentage(.3f))
                {
                    var enemy = BotManager.GetBot(arg.ObjectID);

                    foreach (var weapon in new WeaponItemType[] { WeaponItemType.Melee, WeaponItemType.Rifle, WeaponItemType.Handgun, WeaponItemType.Thrown, WeaponItemType.Powerup })
                    {
                        var weaponObj = enemy.Player.Disarm(weapon);
                        if (weaponObj != null)
                        {
                            Player.GiveWeaponItem(weaponObj.RangedWeapon.WeaponItem);
                            weaponObj.Remove();
                            break;
                        }
                    }
                }
            }
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (GatherSpot == null) return;

            Game.DrawArea(GatherSpot.Spot.GetAABB(), Color.Magenta);
            if (ScriptHelper.IsElapsed(GatherSpot.UpdateTime, 42560))
                ChangeGatherSpot();
        }

        public GatherSpot GatherSpot
        {
            get
            {
                if (Player.GetTeam() == PlayerTeam.Independent) return null;
                return GatherSpots[Player.GetTeam()];
            }
            set
            {
                if (Player.GetTeam() == PlayerTeam.Independent) return;
                GatherSpots[Player.GetTeam()] = value;
            }
        }

        private void ChangeGatherSpot()
        {
            if (GatherSpot != null) GatherSpot.Spot.Remove();
            var spawner = RandomHelper.GetItem(Game.GetObjectsByName("SpawnPlayer"));
            var spot = Game.CreateObject("InvisibleBlockNoCollision", spawner.GetWorldPosition());
            GatherSpot = new GatherSpot
            {
                Spot = spot,
                UpdateTime = Game.TotalElapsedGameTime,
            };

            foreach (var bot in BotManager.GetBots())
                if (bot.Faction == BotFaction.Biker && ScriptHelper.SameTeam(bot.Player, Player))
                    bot.Player.SetGuardTarget(GatherSpot.Spot);
        }
    }
}
