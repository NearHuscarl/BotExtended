using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Projectiles
{
    class SmokeProjectile : CustomProjectile
    {
        public const float SmokeRadius = 50f;
        public const float SmokeTime = 22000f;
        public override bool IsRemoved
        {
            get { return m_explodeTime != 0f && ScriptHelper.IsElapsed(m_explodeTime, SmokeTime); }
        }
        private float m_explodeTime = 0f;
        private Vector2 m_explodePosition;
        public float CurrentSmokeRadius { get; private set; }

        public SmokeProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Smoke)
        {
            CurrentSmokeRadius = 0f;
            _isElapsedSmokeTrailing = ScriptHelper.WithIsElapsed(40);
        }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            if (!Projectile.IsSlowProjectile(projectile.ProjectileItem))
                return null;

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
        private float m_smokeEffectDelay = 0f;
        private float m_smokeEffectBottomDelay = 0f;
        private float m_updateDelay = 0f;
        private float m_groundPositionY;
        private float m_smokeRadiusExpandDelay = 0f;
        private Func<bool> _isElapsedSmokeTrailing;
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            // update projectile
            if (m_explodeTime == 0f)
            {
                if (_isElapsedSmokeTrailing()) Game.PlayEffect(EffectName.Dig, Instance.GetWorldPosition());
                if (Instance.GetLinearVelocity().Length() < 3f)
                {
                    var groundObject = ScriptHelper.GetGroundObject(Instance);
                    if (groundObject != null)
                    {
                        m_explodePosition = Instance.GetWorldPosition();
                        m_explodeTime = Game.TotalElapsedGameTime;
                        m_groundPositionY = Instance.GetWorldPosition().Y;
                        Instance.Destroy();
                    }
                }
                return;
            }

            // update smoke effect after contact
            if (ScriptHelper.IsElapsed(m_smokeRadiusExpandDelay, 400))
            {
                m_smokeRadiusExpandDelay = Game.TotalElapsedGameTime;
                CurrentSmokeRadius = Math.Min(CurrentSmokeRadius + 6, SmokeRadius);
            }

            Game.DrawCircle(m_explodePosition, CurrentSmokeRadius, Color.Cyan);
            var playSmokeEffect = false;
            var playSmokeEffectBottom = false;

            if (ScriptHelper.IsElapsed(m_smokeEffectDelay, 460))
            {
                m_smokeEffectDelay = Game.TotalElapsedGameTime;
                playSmokeEffect = true;
            }
            if (ScriptHelper.IsElapsed(m_smokeEffectBottomDelay, 300))
            {
                m_smokeEffectBottomDelay = Game.TotalElapsedGameTime;
                playSmokeEffectBottom = true;
            }

            var isBottom = false;
            var startY = Math.Max(m_groundPositionY, m_explodePosition.Y - CurrentSmokeRadius);
            for (var i = -CurrentSmokeRadius; i < CurrentSmokeRadius; i += 6)
            {
                for (var j = -CurrentSmokeRadius; j < CurrentSmokeRadius; j += 6)
                {
                    var p = m_explodePosition + new Vector2(i, j);
                    if (!IsInside(p) || p.Y < startY)
                    {
                        isBottom = true;
                        continue;
                    }
                    else
                        isBottom = isBottom || j == -CurrentSmokeRadius;

                    if (isBottom && playSmokeEffectBottom || playSmokeEffect)
                    {
                        Game.PlayEffect(EffectName.Dig, p);
                        Game.DrawCircle(p, .5f, isBottom ? Color.Green : Color.Red);
                    }
                    isBottom = false;
                }
            }

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

        private bool IsInside(Vector2 position) { return ScriptHelper.IntersectCircle(position, m_explodePosition, CurrentSmokeRadius); }
        private bool IsInside(IPlayer player)
        {
            var hitBox = player.GetAABB();
            return ScriptHelper.IntersectCircle(hitBox, m_explodePosition, CurrentSmokeRadius) && hitBox.Top >= m_groundPositionY;
        }
    }
}
