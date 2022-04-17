using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;
using BotExtended.Factions;
using BotExtended.Weapons;

namespace BotExtended.Bots
{
    public class GangsterBot : Bot
    {
        public GangsterBot(BotArgs args) : base(args)
        {
            CanSetupCamp = true;
            _isElapsedCheckCamp = ScriptHelper.WithIsElapsed(60, 120);
        }

        private static Dictionary<PlayerTeam, Camp> Gangs = new Dictionary<PlayerTeam, Camp>
        {
            { PlayerTeam.Team1, null },
            { PlayerTeam.Team2, null },
            { PlayerTeam.Team3, null },
            { PlayerTeam.Team4, null },
        };

        private Camp Gang
        {
            get { return Gangs[Player.GetTeam()]; }
            set { Gangs[Player.GetTeam()] = value; }
        }

        public bool CanSetupCamp { get; set; }

        private Func<bool> _isElapsedCheckCamp;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead || !CanSetupCamp) return;

            if (Gang == null && Player.GetTeam() != PlayerTeam.Independent && _isElapsedCheckCamp())
                TryCamping();
        }

        private bool CanCamp()
        {
            foreach (var camp in Gangs.Values)
            {
                var enemyHeadquarter = camp != null ? camp.Position : new Vector2(1000, 1000);
                if (Vector2.Distance(enemyHeadquarter, Position) < 100) return false;
            }

            if (!Player.IsOnGround) return false;

            var ground = ScriptHelper.GetGroundObject(Player);
            if (ground == null || ground.GetCollisionFilter().CategoryBits != CategoryBits.StaticGround) return false;
            
            return true;
        }


        private void TryCamping()
        {
            if (!CanCamp()) return;
            Gang = (Camp)WeaponManager.SpawnWeapon(BeWeapon.Camp, this);
        }
    }
}
