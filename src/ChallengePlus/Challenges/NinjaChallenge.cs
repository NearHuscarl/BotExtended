using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class NinjaChallenge : ChallengeBase<NinjaChallenge.PlayerData>
    {
        public NinjaChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Ninjas move extremely fast. Increase climbing, diving and throwing speed"; }
        }

        public class PlayerData
        {
            public bool IsDiving;
        }

        public override void OnSpawn(IPlayer[] players)
        {
            base.OnSpawn(players);

            foreach (var p in players)
            {
                var mod = p.GetModifiers();
                mod.EnergyConsumptionModifier = 0;
                mod.RunSpeedModifier = 100;
                mod.SprintSpeedModifier = 100;
                p.SetModifiers(mod);
                
                p.SetSpeedBoostTime(float.MaxValue);
                p.GiveWeaponItem(WeaponItem.SHURIKEN);
                p.GiveWeaponItem(WeaponItem.KATANA);
                var head = (p.GetProfile() ?? new IProfile()).Head;
                var ninjaProfile = ProfileDatabase.Get(ProfileType.Ninja);

                ninjaProfile.Head = head;

                p.SetProfile(ninjaProfile);
            }
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            if (player.Instance.IsClimbing)
            {
                if (player.Velocity.Y >= 2 && player.Velocity.X == 0)
                    player.Velocity = Vector2.UnitY * 8;
            }

            var pData = GetPlayerData(player.UniqueID);
            if (pData != null)
            {
                if (!pData.IsDiving && player.Instance.IsDiving)
                    player.Velocity = player.Instance.AimVector * 14;
                pData.IsDiving = player.Instance.IsDiving;
            }

            if (!player.Instance.IsThrowing) return;

            foreach (var o in Game.GetObjectsByArea(player.Hitbox))
            {
                if (o.IsMissile && o.GetLinearVelocity().Length() < 14)
                {
                    o.SetLinearVelocity(ScriptHelper.GetDirection(o.GetLinearVelocity()) * 30);
                }
            }
        }
    }
}
