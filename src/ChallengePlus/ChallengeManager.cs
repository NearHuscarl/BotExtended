using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public static class ChallengeManager
    {
        private static IChallenge _challenge;

        public static void Initialize()
        {
            var name = GetCurrentChallenge();
            if (name == ChallengeName.None) return;
            _challenge = ChallengeFactory.Create(name);

            Events.UserMessageCallback.Start(Command.OnUserMessage);
            Events.UpdateCallback.Start(_challenge.OnUpdate);
            Events.PlayerWeaponAddedActionCallback.Start(OnPlayerWeaponAdded);
            Events.ProjectileCreatedCallback.Start(OnProjectileCreated);
            Events.ProjectileHitCallback.Start(_challenge.OnProjectileHit);
            Events.ObjectCreatedCallback.Start(OnObjectCreated);
            Events.ObjectDamageCallback.Start(OnObjectDamage);
            Events.ObjectTerminatedCallback.Start(OnObjectTerminated);

            _challenge.OnSpawn(Game.GetPlayers());
        }

        private static ChallengeName GetCurrentChallenge()
        {
            var settings = Settings.Get();
            var currentChallenge = settings.CurrentChallenge;

            if (settings.RoundsUntilRotation == 1)
            {
                List<ChallengeName> challenges;

                if (settings.EnabledChallenges.Count > 1)
                    challenges = settings.EnabledChallenges.Where((f) => f != settings.CurrentChallenge).ToList();
                else
                    challenges = settings.EnabledChallenges;

                currentChallenge = RandomHelper.GetItem(challenges);
                ScriptHelper.PrintMessage("Change challenge to " + currentChallenge);
            }
            Storage.SetItem("CURRENT_CHALLENGE", currentChallenge.ToString());

            if (settings.RotationEnabled)
            {
                var roundTillNextRotation = settings.RoundsUntilRotation == 1 ?
                    settings.RotationInterval
                    :
                    settings.RoundsUntilRotation - 1;
                Storage.SetItem("ROUNDS_UNTIL_ROTATION", roundTillNextRotation);
            }

            return currentChallenge;
        }

        internal static void OnPlayerCreated(Player p)
        {
            _challenge.OnPlayerCreated(p);
        }

        internal static void OnPlayerKeyInput(Player player, VirtualKeyInfo[] keyInfos)
        {
            _challenge.OnPlayerKeyInput(player, keyInfos);
        }

        internal static void OnUpdate(float e, Player p)
        {
            _challenge.OnUpdate(e, p);
        }
        
        private static void OnPlayerWeaponAdded(IPlayer player, PlayerWeaponAddedArg args)
        {
            _challenge.OnPlayerWeaponAdded(PlayerManager.GetPlayer(player), args);
        }

        private static void OnProjectileCreated(IProjectile[] projectiles)
        {
            foreach (var p in projectiles) _challenge.OnProjectileCreated(p);
        }

        internal static void OnPlayerDamage(Player p, PlayerDamageArgs args, Player attacker)
        {
            _challenge.OnPlayerDamage(p, args, attacker);
        }

        internal static void OnPlayerDealth(Player p, PlayerDeathArgs args)
        {
            _challenge.OnPlayerDealth(p, args);
        }

        private static void OnObjectCreated(IObject[] objs)
        {
            foreach (var o in objs) _challenge.OnObjectCreated(o);
        }

        private static void OnObjectDamage(IObject o, ObjectDamageArgs args)
        {
            _challenge.OnObjectDamage(o, args);
        }

        private static void OnObjectTerminated(IObject[] objs)
        {
            foreach (var o in objs) _challenge.OnObjectTerminated(o);
        }
    }
}
