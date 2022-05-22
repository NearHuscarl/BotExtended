using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public class PlayerManager
    {
        private static readonly Dictionary<int, Player> Players = new Dictionary<int, Player>();

        public static void Initialize()
        {
            Events.UpdateCallback.Start(OnUpdate);
            Events.PlayerDamageCallback.Start(OnPlayerDamage);
            Events.PlayerDeathCallback.Start(OnPlayerDeath);
            Events.PlayerKeyInputCallback.Start(OnPlayerKeyInput);
        }

        public static Player GetPlayer(int uniqueID)
        {
            Player player;
            if (Players.TryGetValue(uniqueID, out player)) return player;
            return Player.None;
        }
        public static IEnumerable<Player> GetPlayers() { return Players.Values; }
        public static Player GetPlayer(IObject player) { return GetPlayer(player.UniqueID); }

        public static void OnUpdate(float e)
        {
            foreach (var player in Game.GetPlayers())
            {
                var p = GetPlayer(player);

                if (p != Player.None)
                {
                    ChallengeManager.OnUpdate(e, p);
                }
                else if (!player.IsRemoved && player.CustomID != Player.NoneCustomID)
                {
                    p = Wrap(player); // New player
                    ChallengeManager.OnPlayerCreated(p);
                }
            }
        }

        private static Player Wrap(IPlayer player)
        {
            var p = new Player(player);
            Players.Add(player.UniqueID, p);
            return p;
        }

        private static void OnPlayerDamage(IPlayer player, PlayerDamageArgs args)
        {
            var p = GetPlayer(player);
            if (p == Player.None) return;

            var attacker = (IPlayer)null;
            if (args.DamageType == PlayerDamageEventType.Melee)
            {
                attacker = Game.GetPlayer(args.SourceID);
            }
            if (args.DamageType == PlayerDamageEventType.Projectile)
            {
                var projectile = Game.GetProjectile(args.SourceID);
                attacker = Game.GetPlayer(projectile.OwnerPlayerID);
            }

            var attackPlayer = attacker == null ? null : GetPlayer(attacker);
            ChallengeManager.OnPlayerDamage(p, args, attackPlayer);
        }

        private static void OnPlayerDeath(IPlayer player, PlayerDeathArgs args)
        {
            if (player == null) return;

            var p = GetPlayer(player);
            if (p == Player.None) return;

            ChallengeManager.OnPlayerDealth(p, args);

            if (args.Removed)
            {
                Players.Remove(player.UniqueID);
            }
        }

        private static void OnPlayerKeyInput(IPlayer player, VirtualKeyInfo[] keyInfos)
        {
            var p = GetPlayer(player);
            ChallengeManager.OnPlayerKeyInput(p, keyInfos);
        }
    }
}
