using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using BotExtended.Powerups;
using SFDGameScriptInterface;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    public class TranslucentBot : Bot
    {
        private enum State { Visible, ToInvisible, Invisible, Exposed, }
        public TranslucentBot(BotArgs args) : base(args) { }

        private State _state;
        public override void OnSpawn()
        {
            base.OnSpawn();

            _state = State.Visible;
            _profile = GetProfile();
            _emptyProfile = GetProfile();
            foreach (var type in SharpHelper.EnumToArray<ClothingType>())
            {
                ScriptHelper.Strip(_emptyProfile, type);
            }
            _emptyProfile.Skin.Name = "Invisible";
            BecomeInvisible();
        }

        private IProfile _profile;
        private IProfile _emptyProfile;
        private float _flashTime;
        private const float MAX_FLASH_INTERVAL = 1000;
        private float _flashInterval = MAX_FLASH_INTERVAL;
        private float _intervalChangedTime = 0;
        private bool _forceStopInvisibleTransition = false;
        private void BecomeInvisible()
        {
            if (_state == State.Invisible || _state == State.ToInvisible) return;
            _state = State.ToInvisible;

            ScriptHelper.RunUntil(() =>
            {
                if (ScriptHelper.IsElapsed(_flashTime, _flashInterval))
                {
                    _flashTime = Game.TotalElapsedGameTime;
                    ToggleInvisible();
                }
                if (ScriptHelper.IsElapsed(_intervalChangedTime, 500))
                {
                    _intervalChangedTime = Game.TotalElapsedGameTime;
                    _flashInterval /= 2;
                    if (_flashInterval == 250 || _flashInterval == 125f || _flashInterval == 62.5f || _flashInterval == 31.25f)
                        Game.PlaySound("Madness", Position, 1);
                }
            }, () => _flashInterval <= 15 || _forceStopInvisibleTransition, () =>
            {
                _flashInterval = MAX_FLASH_INTERVAL;

                if (_forceStopInvisibleTransition)
                    _forceStopInvisibleTransition = false;
                else
                    SetInvisible(true);
            });
        }
        private void BecomeVisible()
        {
            if (_state == State.Visible) return;
            SetVisible(true);
            ScriptHelper.Timeout(BecomeInvisible, 2000);
        }

        private void ToggleInvisible() { if (GetProfile().Skin.Name != "Invisible") SetInvisible(); else SetVisible(); }
        private void SetInvisible(bool changeState = false)
        {
            Player.SetProfile(_emptyProfile);
            Player.SetNametagVisible(false);
            Player.SetStatusBarsVisible(false);
            if (changeState)
            {
                _state = State.Invisible;
                if (Player.Statistics != null)
                    _totalDamageTakenWhenInvisible = Player.Statistics.TotalDamageTaken;
            }
        }
        private void SetVisible(bool changeState = false)
        {
            Player.SetProfile(_profile);
            Player.SetNametagVisible(true);
            Player.SetStatusBarsVisible(true);
            if (changeState) _state = State.Visible;
        }

        private float _totalDamageTakenWhenInvisible = 0f;
        public override void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            base.OnDamage(attacker, args);

            if (_state == State.Visible || Player.IsDead) return;

            if (_state == State.ToInvisible)
            {
                _forceStopInvisibleTransition = true;
                BecomeVisible();
                return;
            }

            if (_state == State.Invisible)
            {
                if (Player.Statistics.TotalDamageTaken - _totalDamageTakenWhenInvisible >= 50)
                {
                    BecomeVisible();
                    return;
                }

                // temprorarily exposed when taking damage
                _state = State.Exposed;
                SetVisible();
                ScriptHelper.Timeout(() =>
                {
                    if (_state == State.Exposed)
                    {
                        SetInvisible();
                        _state = State.Invisible; // don't reset total damage by calling SetInvisible(true)
                    }
                }, 100);
            }
        }
    }
}
