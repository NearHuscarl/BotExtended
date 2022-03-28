using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class ZombieEaterBot : Bot
    {
        private Controller<Bot> _controller;
        public static readonly float EatBodyPartTime = 250;
        public ZombieEaterBot(BotArgs args, Controller<Bot> controller) : base(args)
        {
            if (controller != null)
            {
                _controller = controller;
                _controller.Actor = this;
            }
            _isElapsedEat = ScriptHelper.WithIsElapsed(EatBodyPartTime);
            _isElapsedSearchFood = ScriptHelper.WithIsElapsed(60);
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;
            if (_controller != null) _controller.OnUpdate(elapsed);

            SearchFood();
            UpdateGrab();
        }

        private Func<bool> _isElapsedSearchFood;
        private float _grabTimeout = 0f;
        private void SearchFood()
        {
            if (!ScriptHelper.IsElapsed(_grabTimeout, 3000) || !_isElapsedSearchFood() || Player.IsHoldingPlayerInGrab) return;

            var foodNearby = FoodNearby().FirstOrDefault();
            if (foodNearby == null) return;

            // because you cannot grab your own teammate
            if (ScriptHelper.SameTeam(foodNearby, Player))
            {
                foodNearby.SetInputEnabled(false);
                foodNearby.SetTeam(PlayerTeam.Independent);
                foodNearby.SetNametagVisible(false);

                // don't let teammate hit and drop the food
                ScriptHelper.RunIf(() => foodNearby.SetTeam(Player.GetTeam()), () => !Player.IsRemoved && Player.HoldingPlayerInGrabID != 0 || Player.IsInputEnabled);
                ScriptHelper.RunIf(() =>
                {
                    // grab failed
                    if (!foodNearby.IsDead)
                    {
                        foodNearby.SetTeam(Player.GetTeam());
                        foodNearby.SetNametagVisible(true);
                        foodNearby.SetInputEnabled(true);
                    }
                }, () => Player.IsRemoved || Player.IsInputEnabled);
            }

            _grabTimeout = Game.TotalElapsedGameTime;
            ScriptHelper.Command(Player, new PlayerCommand[]
            {
                new PlayerCommand(PlayerCommandType.StartCrouch),
                new PlayerCommand(PlayerCommandType.Grab),
            });
        }

        private IEnumerable<IPlayer> FoodNearby()
        {
            var area = Player.GetAABB();
            var center = area.Center;
            if (Player.FacingDirection > 0)
            {
                area.Left += area.Width;
                area.Right += area.Width + 15;
            }
            if (Player.FacingDirection < 0)
            {
                area.Right -= area.Width;
                area.Left -= (area.Width + 15);
            }
            return Game.GetPlayers().Where(p => area.Intersects(p.GetAABB()));
        }

        private int _holdingPlayerInGrabID = 0;
        private Func<bool> _isElapsedEat;
        private int _lostGiblets = 0;
        private void UpdateGrab()
        {
            if (_holdingPlayerInGrabID == 0 && Player.HoldingPlayerInGrabID != 0)
            {
                OnEnemyGrabbed(Player.HoldingPlayerInGrabID);
            }
            if (Player.IsHoldingPlayerInGrab && _isElapsedEat())
            {
                var enemy = Game.GetPlayer(Player.HoldingPlayerInGrabID);

                if (enemy != null)
                {
                    _lostGiblets++;
                    enemy.DealDamage(0.001f); // play effect
                    Game.PlayEffect(EffectName.MeleeHitBlunt, enemy.GetWorldPosition());
                    var giblet = Game.CreateObject(RandomHelper.GetItem(Constants.Giblets), enemy.GetWorldPosition());
                    giblet.SetLinearVelocity(RandomHelper.Direction(20, 180 - 20) * RandomHelper.Between(5, 15));

                    if (_lostGiblets == Constants.Giblets.Length)
                    {
                        ConsumeFood(enemy);
                        return;
                    }
                }
            }
            if (_holdingPlayerInGrabID != 0 && Player.HoldingPlayerInGrabID == 0)
            {
                OnEnemyDroppedFromGrab();
            }

            _holdingPlayerInGrabID = Player.HoldingPlayerInGrabID;
        }

        private PlayerModifiers _newModifiers;
        private void OnEnemyGrabbed(int playerID)
        {
            var grabbedBot = BotManager.GetBot(playerID);
            var mod = grabbedBot.Player.GetModifiers();
            var sizeDiff = Math.Min(mod.SizeModifier - Size.Tiny, 0.25f);

            _newModifiers = Player.GetModifiers();
            _newModifiers.SizeModifier += sizeDiff;
            _newModifiers.MeleeDamageTakenModifier -= 0.05f;
            _newModifiers.MeleeDamageDealtModifier += 0.05f;
            _newModifiers.MeleeForceModifier = Math.Min(_newModifiers.MeleeForceModifier + 0.15f, MeleeForce.UltraStrong);
            _newModifiers.RunSpeedModifier -= 0.1f;
            _newModifiers.SprintSpeedModifier -= 0.1f;

            var mod2 = Player.GetModifiers();
            mod2.MeleeStunImmunity = Constants.TOGGLE_ON;
            SetModifiers(mod2);
        }

        private void OnEnemyDroppedFromGrab()
        {
            _lostGiblets = 0;
            ResetModifiers();
            _grabTimeout = Game.TotalElapsedGameTime;
        }

        private void ConsumeFood(IPlayer food)
        {
            food.Gib();
            _lostGiblets = 0;
            if (Player.GetModifiers().SizeModifier == Size.Chonky) Player.SetStrengthBoostTime(10000);
            SetHealth(Player.GetHealth() + 20, true);
            SetModifiers(_newModifiers, true);
            _grabTimeout = Game.TotalElapsedGameTime;
        }
    }
}
