using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class QueenBot : Bot
    {
        private enum State { Normal, FindingCorpse, GoingToCorpse, GoingToPenis, GivingHandJob, }
        public QueenBot(BotArgs args) : base(args)
        {
            _isElapsedFindCorpse = ScriptHelper.WithIsElapsed(123);
            _isElapsedCheckCorpse = ScriptHelper.WithIsElapsed(300);
            _isElapsedCheckCloserCorpse = ScriptHelper.WithIsElapsed(1000);
            _isElapsedGiveHand = ScriptHelper.WithIsElapsed(1000);
        }

        private State _state;
        private IPlayer _targetCorpse;

        private float _cooldownTime;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (Game.IsEditorTest)
            {
                Game.DrawText(_state.ToString(), Position);
                //if (_targetCorpse != null)
                //    Game.DrawArea(_targetCorpse.GetAABB(), Color.Red);
                //Game.DrawArea(ScriptHelper.Grow(Player.GetAABB(), 10, 4), Color.Green);
            }

            if (Player.IsDead) return;

            switch (_state)
            {
                case State.Normal:
                    if (ScriptHelper.IsElapsed(_cooldownTime, 2000) && !IsHurtRecently)
                        SetState(State.FindingCorpse);
                    break;
                case State.FindingCorpse:
                    FindEnemyCorpse();
                    break;
                case State.GoingToCorpse:
                    GoingToCorpse();
                    break;
                case State.GoingToPenis:
                    if (Player.IsCrouching) StartGivingHandJob();
                    break;
                case State.GivingHandJob:
                    UpdateGivingHandJob();
                    break;
            }
        }

        private Func<IPlayer, bool> WithFindDeadMaleBody()
        {
            return p => p.IsDead && !p.IsRemoved && p.GetProfile().Skin.Name != "MechSkin";
        }

        private IPlayer FindClosestCorpse()
        {
            var players = Game.GetPlayers().ToList();
            players.Sort(ScriptHelper.WithGetClosestPlayer(Position));
            return players.FirstOrDefault(WithFindDeadMaleBody());
        }

        private Func<bool> _isElapsedFindCorpse;
        private void FindEnemyCorpse()
        {
            if (!_isElapsedFindCorpse()) return;

            _targetCorpse = FindClosestCorpse();
            if (_targetCorpse == null) return;

            GoTo(_targetCorpse);
            SetState(State.GoingToCorpse);
        }

        private Func<bool> _isElapsedCheckCorpse;
        private Func<bool> _isElapsedCheckCloserCorpse;
        private void GoingToCorpse()
        {
            if (!_isElapsedCheckCorpse()) return;

            if (_targetCorpse.IsRemoved)
            {
                SetState(State.Normal);
                return;
            }

            if (_isElapsedCheckCloserCorpse())
            {
                var closestCorpse = FindClosestCorpse();

                if (Distance(closestCorpse) < Distance(_targetCorpse))
                {
                    GoTo(closestCorpse);
                    _targetCorpse = closestCorpse;
                    return;
                }
            }

            if (IsNearCorpse())
            {
                Player.SetInputEnabled(false);
                Player.AddCommand(new PlayerCommand(PlayerCommandType.StartMoveToPosition, _targetCorpse.GetAABB().Center, 300));
                Player.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));
                Player.AddCommand(new PlayerCommand(PlayerCommandType.Sheath));
                SetState(State.GoingToPenis);
            }
        }

        private bool IsNearCorpse() { return _targetCorpse.GetAABB().Intersects(ScriptHelper.Grow(Player.GetAABB(), 10, 8)); }

        private void StartGivingHandJob()
        {
            if (_targetCorpse.IsRemoved)
            {
                SetState(State.Normal);
                return;
            }

            var corpse = _targetCorpse;

            // new players that die immediately will tend to lay on the ground belly up. There is no way to make the player lay forward now
            _targetCorpse = Clone(corpse);
            _isBurntCorpse = corpse.IsBurnedCorpse;
            if (_isBurntCorpse)
            {
                _targetCorpse.SetProfile(ScriptHelper.GetBurntProfile(_targetCorpse.GetProfile().Gender));
            }
            _targetCorpse.SetWorldPosition(corpse.GetWorldPosition() + Vector2.UnitX * Player.FacingDirection * 8);
            _targetCorpse.SetFaceDirection(-Player.FacingDirection);
            _targetCorpse.Kill();

            corpse.Remove();

            SetState(State.GivingHandJob);
        }

        private bool _isBurntCorpse = false;
        private Func<bool> _isElapsedGiveHand;
        private int _handJobCount = 0;
        private void UpdateGivingHandJob()
        {
            if (!_isElapsedGiveHand()) return;

            if (_targetCorpse.IsRemoved || IsHurtRecently || !IsNearCorpse())
            {
                SetState(State.Normal);
                return;
            }

            Player.AddCommand(new PlayerCommand(PlayerCommandType.Block));
            _handJobCount++;
            _targetCorpse.DealDamage(0.001f);

            if (_handJobCount == 1)
            {
                var profile = _targetCorpse.GetProfile();
                
                if (_isBurntCorpse)
                    profile = new IProfile { Skin = new IProfileClothingItem("BurntDeadNaked", null), Gender = profile.Gender, };
                else
                    profile.Skin.Name += "DeadNaked";

                profile.Legs = null;
                _targetCorpse.SetProfile(profile);
            }
            if (_handJobCount >= 4)
            {
                CreateSimp(_targetCorpse);
                SetState(State.Normal);
            }
        }

        private void CreateSimp(IPlayer corpse)
        {
            var dir = Player.FacingDirection;
            ScriptHelper.RunIn(() =>
            {
                if (corpse.IsRemoved) return;
                Game.PlayEffect(EffectName.DestroyGlass, corpse.GetAABB().Center + new Vector2(-dir * 3, 4));
            }, 2000, () =>
            {
                if (corpse.IsRemoved) return;
                
                var simp = Clone(corpse);
                
                var maxHealth = simp.GetModifiers().MaxHealth * .5f;
                var mod = simp.GetModifiers();
                mod.CurrentHealth = maxHealth;
                mod.MaxHealth = (int)maxHealth;
                
                simp.SetModifiers(mod);
                simp.SetTeam(Player.GetTeam());
                simp.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));

                var profile = simp.GetProfile();
                profile = ScriptHelper.StripUnderwear(profile);

                if (profile.Skin.Name.StartsWith("Burnt"))
                    profile = new IProfile { Skin = new IProfileClothingItem("BurntSkin", null), Gender = profile.Gender, };
                else
                    profile.Skin.Name = SharpHelper.RemoveSuffix(profile.Skin.Name, "DeadNaked");

                simp.SetProfile(profile);
                if (simp.Name == "COM") simp.SetBotName("Simp");

                ScriptHelper.Timeout(() =>
                {
                    BotManager.GetBot(simp).SayLine("Yes, my Queen");
                    simp.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
                    if (simp.GetBotBehavior().PredefinedAI == PredefinedAIType.None)
                        simp.SetBotBehaviorSet(BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotD));
                    simp.SetBotBehaivorActive(true);
                }, 800);

                corpse.Remove();
            }, 50);
        }

        private void SetState(State state)
        {
            if (_state == State.GoingToCorpse && state != State.GoingToCorpse)
            {
                Player.SetGuardTarget(null);
            }
            if (state == State.Normal)
            {
                if (!Player.IsInputEnabled)
                {
                    Player.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
                    ScriptHelper.Timeout(() => Player.SetInputEnabled(true), 16);
                }
                _handJobCount = 0;
                _targetCorpse = null;
                _cooldownTime = Game.TotalElapsedGameTime;
            }
            _state = state;
        }
    }
}
