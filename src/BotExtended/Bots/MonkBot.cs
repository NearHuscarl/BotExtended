using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.SFD;
using System;
using System.Linq;

namespace BotExtended.Bots
{
    public class MonkBot : Bot
    {
        public MonkBot(BotArgs args) : base(args)
        {
            _isElapsedClone = ScriptHelper.WithIsElapsed(5000);
            CanClone = true;
        }

        public bool CanClone { get; set; }

        private const int MAX_CLONES = 6;
        private const int MAX_TOTAL_CLONES = 20;
        private Func<bool> _isElapsedClone;
        private readonly List<int> _clones = new List<int>();
        private static readonly List<int> AllClones = new List<int>();
        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (Player.IsDead) return;

            if (CanClone && _isElapsedClone() && Player.IsOnGround && !IsHurtRecently && _clones.Count < MAX_CLONES && AllClones.Count < MAX_TOTAL_CLONES)
            {
                Player.SetNametagVisible(false);
                Player.SetStatusBarsVisible(false);

                ScriptHelper.Command(Player, PlayerCommandType.StartCrouch, delayTime: 700);
                ScriptHelper.Timeout(() =>
                {
                    if (Player.IsDead) return;
                    
                    NinjaBot.PlaySmoke(Position);
                    NinjaBot.PlaySmoke(Position);
                    NinjaBot.PlaySmoke(Position);

                    var cloneCount = Math.Min(MAX_CLONES - _clones.Count, MAX_TOTAL_CLONES - AllClones.Count);
                    for (var i = 0; i < cloneCount; i++)
                        Clone();
                }, 600);
            }
        }

        private void Clone()
        {
            var clone = (MonkBot)BotManager.SpawnBot(BotType.Monk, Faction, team: Player.GetTeam(), ignoreFullSpawner: true);
            var cloneID = clone.Player.UniqueID;

            clone.Position = Position;
            clone.CanClone = false;
            clone.SetHealth(10, true);
            clone.Player.SetNametagVisible(false);
            clone.Player.SetStatusBarsVisible(false);
            clone.OnCloneDeath = () =>
            {
                _clones.Remove(cloneID);
                if (_clones.Count == 0)
                {
                    Player.SetNametagVisible(true);
                    Player.SetStatusBarsVisible(true);
                }
            };

            var mod = Player.GetModifiers();
            mod.MeleeDamageDealtModifier = 0.001f; // enough to break glass but can deal barely any damage
            clone.SetModifiers(mod, true);

            _clones.Add(clone.Player.UniqueID);
            AllClones.Add(clone.Player.UniqueID);
        }

        private Action OnCloneDeath;

        public override void OnDeath(PlayerDeathArgs args)
        {
            base.OnDeath(args);

            if (!CanClone)
            {
                if (OnCloneDeath != null) OnCloneDeath.Invoke();
                if (!args.Removed)
                {
                    NinjaBot.PlaySmoke(Position);
                    Player.Remove();
                }
            }   
            
            AllClones.Remove(Player.UniqueID);
        }
    }
}
