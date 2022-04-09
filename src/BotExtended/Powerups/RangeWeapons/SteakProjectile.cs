using System;
using System.Collections.Generic;
using System.Linq;
using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class SteakProjectile : CustomProjectile
    {
        private static List<Bot> Zombies = new List<Bot>();

        public SteakProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            _isElapsedCheckFood = ScriptHelper.WithIsElapsed(110);
        }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            return CreateCustomProjectile(projectile, RandomHelper.GetItem(Constants.Giblets), projectile.Direction * 20);
        }

        private bool _spawnZombie;
        private Func<bool> _isElapsedCheckFood;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Instance.IsRemoved) return;

            if (ScriptHelper.IsElapsed(CreatedTime, 10000))
                Instance.Destroy();

            var vec = Instance.GetLinearVelocity();
            if (!_spawnZombie && vec == Vector2.Zero)
            {
                var owner = Game.GetPlayer(InitialOwnerPlayerID);
                SpawnZombieNearFood(owner);
                Zombies.ForEach(z =>
                {
                    ChaseFood(z.Player);
                    z.Player.SetTeam(owner.GetTeam());
                });
                _spawnZombie = true;
            }
            if (vec == Vector2.Zero && _isElapsedCheckFood())
            {
                foreach (var z in Zombies)
                {
                    if (z.Player.IsDead) continue;
                    if (z.Player.GetAABB().Intersects(ScriptHelper.Grow(Instance.GetAABB(), 5, 5)))
                    {
                        Game.PlaySound("GetHealthSmall", z.Position);
                        Instance.Destroy();
                        z.ResetModifiers();
                        z.ResetBotBehaviorSet();
                        z.SetHealth(z.Player.GetHealth() + 15, true);
                        break;
                    }
                }
            }
        }

        private void SpawnZombieNearFood(IPlayer owner)
        {
            var foodPosition = Instance.GetWorldPosition();
            var groundPathNodes = Game.GetObjects<IObjectPathNode>()
                .Where(x => x.GetPathNodeType() == PathNodeType.Ground && MathExtension.InRange(Vector2.Distance(x.GetWorldPosition(), foodPosition), 15, 80))
                .ToList();
            if (groundPathNodes.Count == 0) return;
            var node = RandomHelper.GetItem(groundPathNodes);
            var position = node.GetWorldPosition() - Vector2.UnitY * 2;
            var bot = SpawnZombie(owner, position, foodPosition.X - position.X > 0 ? 1 : -1);

            if (bot == null) return;
            bot.Player.SetNametagVisible(false);
            bot.Player.SetStatusBarsVisible(false);
        }

        private void ChaseFood(IPlayer zombie)
        {
            var mod = zombie.GetModifiers();
            mod.RunSpeedModifier = Speed.VeryFast;
            mod.SprintSpeedModifier = Speed.VeryFast;
            zombie.SetModifiers(mod);
            var bs = zombie.GetBotBehaviorSet();
            bs.GuardRange = 5;
            bs.ChaseRange = 7;
            bs.OffensiveSprintLevel = 0.9f;
            zombie.SetBotBehaviorSet(bs);
            zombie.SetGuardTarget(Instance);
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
        public static Bot SpawnZombie(IPlayer owner, Vector2 position, int direction = -1)
        {
            Zombies = Zombies.Where(x => !x.Player.IsDead).ToList();
            if (Zombies.Count >= 6) return null;

            var player = PlayerSpawnTrigger.CreatePlayer(position);
            player.SetFaceDirection(direction);

            var rndNum = RandomHelper.Between(0, 100);
            var botType = BotType.None;
            var health = 50;
            if (rndNum < 1) // big oof
                botType = BotType.ZombieFighter;
            if (rndNum >= 1 && rndNum < 2)
                botType = BotType.ZombieEater;
            if (rndNum >= 2 && rndNum < 10)
            {
                botType = RandomHelper.GetItem(GameScript.MutatedZombieTypes);
                health = 20;
            }
            if (rndNum >= 10)
            {
                botType = RandomHelper.GetItem(GameScript.CommonZombieTypes);
                health = 10;
            }

            var bot = BotManager.SpawnBot(botType, BotManager.GetBot(owner).Faction, player, owner.GetTeam(), ignoreFullSpawner: true);
            if (bot.Player.IsBurning) bot.Player.ClearFire();
            if (health > 0) bot.SetHealth(health, true);
            Zombies.Add(bot);

            return bot;
        }
    }
}
