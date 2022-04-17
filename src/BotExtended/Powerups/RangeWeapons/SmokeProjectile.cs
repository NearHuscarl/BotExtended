using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class SmokeProjectile : CustomProjectile
    {
        public const float SmokeRadius = 50f;
        public const float SmokeTime = 22000f;
        public override bool IsRemoved
        {
            get { return _explodeTime != 0f && ScriptHelper.IsElapsed(_explodeTime, SmokeTime); }
        }
        private float _explodeTime = 0f;
        private Vector2 _explodePosition;
        public float CurrentSmokeRadius { get; private set; }

        public SmokeProjectile(IProjectile projectile, RangedWeaponPowerup powerup) : base(projectile, powerup)
        {
            CurrentSmokeRadius = 0f;
            _isElapsedSmokeTrailing = ScriptHelper.WithIsElapsed(40);
            _isElapsedSmokeExpand = ScriptHelper.WithIsElapsed(400);
        }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            return CreateCustomProjectile(projectile, "WpnC4Detonator", projectile.Velocity / 20);
        }

        private class Info
        {
            public bool IsNametagVisible = false;
            public bool IsStatusBarsVisible = false;
            public IPlayer Player;
        }

        private static HashSet<int> AllPlayersAffected = new HashSet<int>();
        private Dictionary<int, Info> m_playersAffected = new Dictionary<int, Info>();
        private float m_updateDelay = 0f;
        private float _groundPositionY;
        private Func<bool> _isElapsedSmokeTrailing;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            // update projectile
            if (_explodeTime == 0f)
            {
                if (_isElapsedSmokeTrailing()) Game.PlayEffect(EffectName.Dig, Instance.GetWorldPosition());
                if (Instance.GetLinearVelocity().Length() < 3f)
                {
                    var groundObject = ScriptHelper.GetGroundObject(Instance);
                    if (groundObject != null)
                    {
                        _explodePosition = Instance.GetWorldPosition();
                        _explodeTime = Game.TotalElapsedGameTime;
                        _groundPositionY = Instance.GetWorldPosition().Y;
                        Instance.Destroy();
                    }
                }
                return;
            }

            // update smoke effect after contact
            UpdateSmokeEffect();

            // update affected players
            if (ScriptHelper.IsElapsed(m_updateDelay, 150))
            {
                m_updateDelay = Game.TotalElapsedGameTime;

                foreach (var player in Game.GetPlayers())
                {
                    if (player != null && !player.IsDead && !m_playersAffected.ContainsKey(player.UniqueID) && IsInside(player)
                        && !AllPlayersAffected.Contains(player.UniqueID))
                    {
                        m_playersAffected.Add(player.UniqueID, new Info()
                        {
                            IsNametagVisible = player.GetNametagVisible(),
                            IsStatusBarsVisible = player.GetStatusBarsVisible(),
                            Player = player,
                        });
                        AllPlayersAffected.Add(player.UniqueID);

                        var bot = BotManager.GetBot(player);
                        var mod = player.GetModifiers();

                        mod.RunSpeedModifier = .6f;
                        mod.SprintSpeedModifier = .6f;
                        mod.MeleeDamageTakenModifier = .5f;
                        mod.MeleeForceModifier = 2f;
                        mod.ProjectileDamageTakenModifier = 2f;
                        mod.FireDamageTakenModifier = 2f;
                        mod.ExplosionDamageTakenModifier = 2f;
                        bot.SetModifiers(mod);

                        var bs = player.GetBotBehaviorSet();
                        bs.RangedWeaponAccuracy = 0f;
                        bs.RangedWeaponPrecisionAccuracy = .1f;
                        bot.SetBotBehaviorSet(bs);

                        player.SetNametagVisible(false);
                        player.SetStatusBarsVisible(false);
                    }
                }
                foreach (var kv in m_playersAffected.ToList())
                {
                    if (kv.Value.Player.IsDead || !IsInside(kv.Value.Player)) RemoveSmokeEffect(kv.Key);
                }
            }
        }

        private class SmokeInfo
        {
            public Vector2 Position;
            public float LastPlayTime;
        }
        private List<SmokeInfo> _smokeInfos = new List<SmokeInfo>();
        private void ComputeSmokeEffectPositions(float smokeRadius)
        {
            _smokeInfos.Clear();
            var bottomY = Math.Max(_groundPositionY, _explodePosition.Y - smokeRadius);

            for (var i = -smokeRadius; i < smokeRadius; i += 6)
            {
                for (var j = -smokeRadius; j < smokeRadius; j += 6)
                {
                    var p = _explodePosition + new Vector2(i, j);
                    if (IsInside(p) && p.Y >= bottomY)
                    {
                        _smokeInfos.Add(new SmokeInfo { Position = p, LastPlayTime = 0 });
                    }
                }
            }
        }

        private Func<bool> _isElapsedSmokeExpand;
        private float _smokeEffectDelay = 0f;
        private void UpdateSmokeEffect()
        {
            if (CurrentSmokeRadius < SmokeRadius && _isElapsedSmokeExpand())
            {
                CurrentSmokeRadius = Math.Min(CurrentSmokeRadius + 6, SmokeRadius);
                ComputeSmokeEffectPositions(CurrentSmokeRadius);
            }

            Game.DrawCircle(_explodePosition, CurrentSmokeRadius, Color.Cyan);

            var maxEffectDelay = 300;
            var smokes = _smokeInfos.Where(x => ScriptHelper.IsElapsed(x.LastPlayTime, maxEffectDelay)).ToList();
            if (ScriptHelper.IsElapsed(_smokeEffectDelay, maxEffectDelay / _smokeInfos.Count) && smokes.Count > 0)
            {
                _smokeEffectDelay = Game.TotalElapsedGameTime;

                // draw 5 smokes at a time to save performance
                for (var i = 0; i < 5; i++)
                {
                    var smoke = RandomHelper.GetItem(smokes);
                    smoke.LastPlayTime = Game.TotalElapsedGameTime;
                    Game.PlayEffect(EffectName.Dig, smoke.Position);
                }
            }
        }

        public override void OnRemove()
        {
            base.OnRemove();
            foreach (var kv in m_playersAffected.ToList()) RemoveSmokeEffect(kv.Key);
        }

        private void RemoveSmokeEffect(int playerID)
        {
            var info = m_playersAffected[playerID];
            var bot = BotManager.GetBot(playerID);

            bot.ResetModifiers();
            bot.ResetBotBehaviorSet();
            bot.Player.SetNametagVisible(info.IsNametagVisible);
            bot.Player.SetStatusBarsVisible(info.IsStatusBarsVisible);

            m_playersAffected.Remove(playerID);
            AllPlayersAffected.Remove(playerID);
        }

        private bool IsInside(Vector2 position) { return ScriptHelper.IntersectCircle(position, _explodePosition, CurrentSmokeRadius); }
        private bool IsInside(IPlayer player)
        {
            var hitBox = player.GetAABB();
            return ScriptHelper.IntersectCircle(hitBox, _explodePosition, CurrentSmokeRadius) && hitBox.Top >= _groundPositionY;
        }
    }
}
