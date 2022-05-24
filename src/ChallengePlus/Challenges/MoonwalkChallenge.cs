using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class MoonwalkChallenge : ChallengeBase<MoonwalkChallenge.PlayerData>
    {
        public MoonwalkChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return " (EXPERIMENTAL) Players can only walk backward and face the wrong direction."; }
        }

        private static float GetSpeed(Player player)
        {
            if (player.Instance.IsWalking) return 1.5f;
            if (player.Instance.IsRunning) return 2.5f;
            if (player.Instance.IsSprinting) return 3.5f;
            return Math.Abs(player.Velocity.X);
        }

        public class PlayerData
        {
            public bool PortalCooldown;
            public bool IsFlippedInPortal;
        }

        private static readonly List<IObjectPortal> Portals = Game.GetObjects<IObjectPortal>().ToList();

        public override void OnPlayerKeyInput(Player player, VirtualKeyInfo[] keyInfos)
        {
            base.OnPlayerKeyInput(player, keyInfos);

            var pData = GetPlayerData(player.UniqueID);

            foreach (var keyInfo in keyInfos)
            {
                if (keyInfo.Event == VirtualKeyEvent.Released)
                {
                    if (keyInfo.Key == VirtualKey.AIM_RUN_LEFT)
                    {
                        pData.IsFlippedInPortal = false;
                    }
                    if (keyInfo.Key == VirtualKey.AIM_RUN_RIGHT)
                    {
                        pData.IsFlippedInPortal = false;
                    }
                }
            }
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            if (player.IsBot || player.IsDead) return;

            var pData = GetPlayerData(player.UniqueID);

            if (player.Instance.Name == "Near")
                Game.DrawText((pData == null) + "", player.Position);

            if (pData == null) return;

            if (!pData.PortalCooldown)
            {
                foreach (var portal in Portals)
                {
                    if (portal.GetAABB().Intersects(player.Hitbox))
                    {
                        var destination = portal.GetDestinationPortal();
                        if (destination != null)
                        {
                            player.Position = destination.GetWorldPosition();
                            pData.PortalCooldown = true;
                            pData.IsFlippedInPortal = true;
                            ScriptHelper.Timeout(() => pData.PortalCooldown = false, 2000);
                            break;
                        }
                    }
                }
            }

            var speed = GetSpeed(player);
            player.Position += Vector2.UnitX * speed * -player.Direction * (pData.IsFlippedInPortal ? -1 : 1);
            Game.DrawText(speed + "", player.Position);

            if (player.Instance.Name == "Near")
            {
                //Game.DrawText(player.Instance.KeyMovementIsFlipped + " " + player.Instance.GetLinearVelocity().X + " ", player.Position);
            }
        }
    }
}
