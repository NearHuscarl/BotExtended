using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class ApocalypseChallenge : Challenge
    {
        private static List<IPlayer> Zombies = new List<IPlayer>();
        private readonly Func<float, float, bool> _isElapsedSpawnZombie = ScriptHelper.WithIsElapsed2();
        private readonly Func<float, bool> _isElapsedZombieDmg = ScriptHelper.WithIsElapsed();
        private static float _damageModifier = 1f;

        public ApocalypseChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Zombies spawn near you, zombie damage increases over time. Survive the zombies for 3 minutes."; }
        }

        public static bool IsZombie(IPlayer player)
        {
            return player.GetProfile().Skin.Name.Contains("Zombie");
        }

        public override void OnUpdate(float e, Player player)
        {
            base.OnUpdate(e, player);

            // gameover
            if (Game.TotalElapsedGameTime > 3 * 60 * 1000)
                return;
            // too soon
            if (Game.TotalElapsedGameTime < 5000)
                return;

            if (_isElapsedZombieDmg(30000))
            {
                _damageModifier += .15f;
                foreach (var zombie in Zombies)
                {
                    var mod = zombie.GetModifiers();
                    mod.MeleeDamageDealtModifier = _damageModifier;
                    zombie.SetModifiers(mod);
                    Game.PlayEffect(EffectName.CustomFloatText, zombie.GetWorldPosition(), "Level up");
                }
            }

            if (_isElapsedSpawnZombie(2000, 4000))
            {
                var players = PlayerManager.GetPlayers();
                var groundPathNodes = Game.GetObjects<IObjectPathNode>()
                    .Where(x => x.GetPathNodeType() == PathNodeType.Ground && players.Any(p => !p.IsDead && !IsZombie(p.Instance) && p.Distance(x) < 40))
                    .ToList();
                if (groundPathNodes.Count == 0) return;
                var node = RandomHelper.GetItem(groundPathNodes);
                var position = node.GetWorldPosition() - Vector2.UnitY * 2;
                SpawnZombie(position, RandomHelper.Boolean() ? -1 : 1);
            }
        }

        public override void OnPlayerDealth(Player player, PlayerDeathArgs args)
        {
            base.OnPlayerDealth(player, args);
            Zombies.Remove(player.Instance);
        }

        private static IObjectPlayerSpawnTrigger _playerSpawnTrigger;
        private static IObjectPlayerSpawnTrigger PlayerSpawnTrigger
        {
            get
            {
                if (_playerSpawnTrigger == null)
                {
                    _playerSpawnTrigger = (IObjectPlayerSpawnTrigger)Game.CreateObject("PlayerSpawnTrigger");
                    _playerSpawnTrigger.SetPredefinedAI(PredefinedAIType.ZombieB); // set zombie spawn animation
                }
                return _playerSpawnTrigger;
            }
        }
        public static IPlayer SpawnZombie(Vector2 position, int direction = -1)
        {
            Zombies = Zombies.Where(x => !x.IsDead).ToList();
            if (Zombies.Count >= 25) return null;

            var player = PlayerSpawnTrigger.CreatePlayer(position);
            player.SetFaceDirection(direction);
            player.SetProfile(ProfileDatabase.Get(ProfileType.Zombie));
            player.SetTeam(PlayerTeam.Team4);
            player.SetNametagVisible(false);
            player.SetStatusBarsVisible(false);
            player.SetHealth(15);

            var mod = player.GetModifiers();
            mod.MeleeDamageDealtModifier = _damageModifier;
            player.SetModifiers(mod);

            Zombies.Add(player);

            return player;
        }
    }
}
