using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class SpyBot : Bot
    {
        private static HashSet<int> SwappedBodies = new HashSet<int>();

        public bool IsDisguising { get { return OriginalTeam != Player.GetTeam(); } }

        private enum State { Normal, FindingCorpse, GoingToCorpse, Disguised, }
        public SpyBot(BotArgs args) : base(args)
        {
            OriginalTeam = args.Player.GetTeam();
            _isElapsedFindCorpse = ScriptHelper.WithIsElapsed(123);
            _isElapsedCheckCorpse = ScriptHelper.WithIsElapsed(200);
            _isElapsedCheckEnemy = ScriptHelper.WithIsElapsed(1500);
        }

        static SpyBot()
        {
            Game.AutoVictoryConditionEnabled = false; // don't stop the game when there is 1 team
        }
        
        private State _state;
        public PlayerTeam OriginalTeam { get; private set; }
        private IPlayer _targetCorpse;
        private IPlayer _targetEnemy;

        public override void OnSpawn()
        {
            base.OnSpawn();
            
            // disable grab
            var bs = Player.GetBotBehaviorSet();
            bs.MeleeActions.Grab = 0;
            bs.MeleeActionsWhenHit.Grab = 0;
            bs.MeleeActionsWhenEnraged.Grab = 0;
            bs.MeleeActionsWhenEnragedAndHit.Grab = 0;
            SetBotBehaviorSet(bs, true);

            SetCostumeTeamColor();
        }

        private void SetCostumeTeamColor()
        {
            var profile = GetProfile();
            var clothingColor = "Clothing" + ScriptHelper.GetTeamColorText(OriginalTeam);
            var clothingDarkColor = "ClothingDark" + ScriptHelper.GetTeamColorText(OriginalTeam);

            if (profile.ChestOver != null) profile.ChestOver.Color1 = clothingColor;
            if (profile.ChestUnder != null) profile.ChestUnder.Color2 = clothingDarkColor;
            if (profile.Legs != null) profile.Legs.Color1 = clothingColor;
            if (profile.Accesory != null) profile.Accesory.Color1 = clothingDarkColor;

            Player.SetProfile(profile);
        }

        private float _cooldownTime;
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            Game.DrawText(_state.ToString(), Position);
            if (_targetCorpse != null)
                Game.DrawArea(_targetCorpse.GetAABB(), Color.Red);
            if (_targetEnemy != null)
                Game.DrawArea(_targetEnemy.GetAABB(), Color.Red);

            if (Player.IsDead) return;

            switch (_state)
            {
                case State.Normal:
                    if (ScriptHelper.IsElapsed(_cooldownTime, 3000))
                        SetState(State.FindingCorpse);
                    break;
                case State.FindingCorpse:
                    FindEnemyCorpse();
                    break;
                case State.GoingToCorpse:
                    GoingToCorpse();
                    break;
                case State.Disguised:
                    UpdateDisguide();
                    break;
            }
        }

        private Func<IPlayer, bool> WithFindDeadBody()
        {
            var alivePlayers = GetAlivePlayers();
            return p =>
            {
                var hasAliveTeammates = p.GetTeam() != PlayerTeam.Independent && alivePlayers[p.GetTeam()] > 0;
                var cleanCorpse = p.IsDead && !p.IsBurnedCorpse && !p.IsRemoved;
                return cleanCorpse && hasAliveTeammates && !SwappedBodies.Contains(p.UniqueID) && !ScriptHelper.SameTeam(p, Player);
            };
        }

        private Func<bool> _isElapsedFindCorpse;
        private void FindEnemyCorpse()
        {
            if (!_isElapsedFindCorpse()) return;

            var players = Game.GetPlayers().ToList();
            players.Sort(ScriptHelper.WithGetClosestPlayer(Position));

            var corpse = players.FirstOrDefault(WithFindDeadBody());
            if (corpse == null) return;

            GoTo(corpse);
            _targetCorpse = corpse;
            SetState(State.GoingToCorpse);
        }

        private Func<bool> _isElapsedCheckCorpse;
        private void GoingToCorpse()
        {
            if (!_isElapsedCheckCorpse()) return;

            var alivePlayers = GetAlivePlayers();
            var corpseTeam = _targetCorpse.GetTeam();
            if (_targetCorpse.IsRemoved || corpseTeam == PlayerTeam.Independent || alivePlayers[corpseTeam] < 1)
            {
                SetState(State.Normal);
                return;
            }

            var players = Game.GetObjectsByArea<IPlayer>(ScriptHelper.Grow(Player.GetAABB(), 3, 3));
            var corpse = players.FirstOrDefault(WithFindDeadBody());
            if (corpse != null)
                Disguise(corpse);
        }

        private void Disguise(IPlayer corpse)
        {
            var p1 = GetProfile();
            var p2 = BotManager.GetBot(corpse).GetProfile();

            SwappedBodies.Add(corpse.UniqueID);
            corpse.SetProfile(p1);
            Player.SetProfile(p2);
            Player.SetTeam(corpse.GetTeam());
            Player.SetBotName(corpse.Name);
            SetState(TargetEnemy() ? State.Disguised : State.Normal);
        }

        private bool TargetEnemy(IPlayer enemy = null)
        {
            enemy = enemy ?? GetClosestEnemy();
            if (enemy == null) return false;
            _targetEnemy = enemy;
            Player.SetForcedBotTarget(enemy);
            return true;
        }

        private IPlayer GetClosestEnemy()
        {
            var players = Game.GetPlayers().ToList();
            players.Sort(ScriptHelper.WithGetClosestPlayer(Position));
            return players.FirstOrDefault(p => !p.IsDead && ScriptHelper.SameTeam(p, Player.GetTeam()) && p.UniqueID != Player.UniqueID);
        }

        private Func<bool> _isElapsedCheckEnemy;
        private void UpdateDisguide()
        {
            var alivePlayers = GetAlivePlayers();
            if (_targetEnemy.IsDead && !TargetEnemy() || alivePlayers[Player.GetTeam()] == 0)
            {
                SetState(State.Normal);
                return;
            }
            if (_isElapsedCheckEnemy())
            {
                var players = Game.GetPlayers().ToList();
                players.Sort(ScriptHelper.WithGetClosestPlayer(Position));
                var enemy = players.FirstOrDefault(p => !p.IsDead && ScriptHelper.SameTeam(p, Player.GetTeam())
                && p.UniqueID != Player.UniqueID && p.UniqueID != _targetEnemy.UniqueID);

                if (enemy != null && Distance(enemy.GetWorldPosition()) < 35)
                    TargetEnemy(enemy);
            }
        }

        public override void OnDealDamage(IPlayer victim, PlayerDamageArgs arg)
        {
            base.OnDealDamage(victim, arg);

            if (_state != State.Disguised) return;
            if (arg.DamageType == PlayerDamageEventType.Projectile && ScriptHelper.SameTeam(victim, Player))
            {
                victim.DealDamage(arg.Damage * 4); // x5 damage
                Game.PlayEffect(EffectName.CustomFloatText, victim.GetWorldPosition(), "Critical Damage");
                SetState(State.Normal);
            }
        }

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (_state != State.Disguised) return;
            var playEffect = false;
            
            foreach (var arg in args)
            {
                var hitPlayer = ScriptHelper.AsPlayer(arg.HitObject);
                if (hitPlayer != null && ScriptHelper.SameTeam(hitPlayer, Player))
                {
                    if (!playEffect)
                    {
                        Game.PlayEffect(EffectName.CustomFloatText, arg.HitPosition, "Critical Damage");
                        Game.PlayEffect(EffectName.Blood, arg.HitPosition);
                        Game.PlayEffect(EffectName.Smack, arg.HitPosition);
                        Game.PlaySound("ImpactFlesh", arg.HitPosition);
                        ScriptHelper.Fall(hitPlayer);
                        playEffect = true;
                    }
                    arg.HitObject.DealDamage(arg.HitDamage * 4); // x5 damage
                }
            }

            if (playEffect)
                SetState(State.Normal);
        }

        private void SetState(State state)
        {
            if (_state == State.GoingToCorpse && state != State.GoingToCorpse)
            {
                _targetCorpse = null;
                Player.SetGuardTarget(null);
            }
            if (_state == State.Disguised && state != State.Disguised)
            {
                _targetEnemy = null;
                Player.SetForcedBotTarget(null);
            }
            if (state == State.Normal)
            {
                var spyProfile = GameScript.GetProfiles(BotType.Spy).First();
                Player.SetTeam(OriginalTeam);
                Player.SetBotName(spyProfile.Name);
                Player.SetProfile(spyProfile);
                SetCostumeTeamColor();
                _cooldownTime = Game.TotalElapsedGameTime;
            }
            _state = state;
        }
    }
}
