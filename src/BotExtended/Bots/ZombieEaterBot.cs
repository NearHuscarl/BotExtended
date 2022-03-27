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
                foodNearby.SetTeam(PlayerTeam.Independent);
                ScriptHelper.Timeout(() =>
                {
                    // grab failed
                    if (Player.HoldingPlayerInGrabID != foodNearby.UniqueID) foodNearby.SetTeam(Player.GetTeam());
                }, 1200);
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
                area.Right += area.Width + 10;
            }
            if (Player.FacingDirection < 0)
            {
                area.Right -= area.Width;
                area.Left -= area.Width - 10;
            }
            return Game.GetPlayers().Where(p => area.Intersects(p.GetAABB()) && Vector2.Distance(center, p.GetAABB().Center) > 10);
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
            if (_holdingPlayerInGrabID != 0 && Player.HoldingPlayerInGrabID == 0)
            {
                OnEnemyGetThrownFromGrab(Player.HoldingPlayerInGrabID);
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
                        enemy.Gib();
                        _lostGiblets = 0;
                    }
                }
            }

            _holdingPlayerInGrabID = Player.HoldingPlayerInGrabID;
        }

        private int _meleeImmunity = -1;
        private void OnEnemyGrabbed(int playerID)
        {
            var grabbedBot = BotManager.GetBot(playerID);
            var mod = grabbedBot.Player.GetModifiers();
            var sizeDiff = mod.SizeModifier - Size.Tiny;

            mod.SizeModifier = Size.Tiny;
            grabbedBot.SetModifiers(mod, true);

            mod = Player.GetModifiers();
            _meleeImmunity = mod.MeleeStunImmunity;
            if (mod.SizeModifier == Size.Chonky) Player.SetStrengthBoostTime(10000);
            mod.MeleeStunImmunity = Constants.TOGGLE_ON;
            mod.SizeModifier += sizeDiff;
            mod.MaxHealth += 20;
            mod.CurrentHealth += 20;
            mod.MeleeDamageTakenModifier -= 0.05f;
            mod.MeleeDamageDealtModifier += 0.05f;
            mod.RunSpeedModifier -= 0.1f;
            mod.SprintSpeedModifier -= 0.1f;
            SetModifiers(mod, true);
        }

        private void OnEnemyGetThrownFromGrab(int playerID)
        {
            var mod = Player.GetModifiers();
            mod.MeleeStunImmunity = _meleeImmunity;
            SetModifiers(mod, true);
        }
    }
}
