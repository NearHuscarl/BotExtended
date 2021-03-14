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
    class SmokeProjectile : Projectile
    {
        public const float SmokeRadius = 50f;
        public const float SmokeTime = 22000f;
        public override bool IsRemoved
        {
            get { return m_explodeTime != 0f && !m_playersAffected.Any() && ScriptHelper.IsElapsed(m_explodeTime, SmokeTime); }
        }
        private float m_explodeTime = 0f;
        private Vector2 m_explodePosition;
        public float CurrentSmokeRadius { get; private set; }

        public SmokeProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Smoke)
        {
            CurrentSmokeRadius = 0f;
        }

        protected override bool OnProjectileCreated()
        {
            if (IsSlowProjectile(Instance))
            {
                var position = Instance.Position;
                var direction = Instance.Direction;

                Instance.DamageDealtModifier = 0.01f; // in case it's removed immediately
                // TODO: wait for gurt to fix it
                ScriptHelper.Timeout(() =>
                {
                    Instance.FlagForRemoval();
                    Instance = Game.SpawnProjectile(ProjectileItem.GRENADE_LAUNCHER, position, direction, ProjectilePowerup.Bouncing);
                    Instance.DamageDealtModifier = 0.01f;
                }, 0);

                return true;
            }
            return false;
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
        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (m_explodeTime == 0f) return;

            if (ScriptHelper.IsElapsed(m_smokeRadiusExpandDelay, 400))
            {
                m_smokeRadiusExpandDelay = Game.TotalElapsedGameTime;
                CurrentSmokeRadius = Math.Min(CurrentSmokeRadius + 6, SmokeRadius);
            }

            if (!ScriptHelper.IsElapsed(m_explodeTime, SmokeTime))
            {
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
                for (var i = -CurrentSmokeRadius; i < CurrentSmokeRadius; i+=6)
                {
                    for (var j = -CurrentSmokeRadius; j < CurrentSmokeRadius; j+=6)
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
            }

            if (ScriptHelper.IsElapsed(m_updateDelay, 150))
            {
                m_updateDelay = Game.TotalElapsedGameTime;

                foreach (var player in Game.GetPlayers())
                {
                    if (!player.IsDead && !m_playersAffected.ContainsKey(player.UniqueID) && IsInside(player)
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
                        mod.MeleeDamageTakenModifier = 2f;
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
                    var info = kv.Value;
                    var bot = BotManager.GetBot(kv.Key);

                    if (bot == Bot.None || bot.Player.IsDead || !IsInside(info.Player))
                    {
                        bot.ResetModifiers();
                        bot.ResetBotBehaviorSet();
                        bot.Player.SetNametagVisible(info.IsNametagVisible);
                        bot.Player.SetStatusBarsVisible(info.IsStatusBarsVisible);

                        m_playersAffected.Remove(info.Player.UniqueID);
                        AllPlayersAffected.Remove(info.Player.UniqueID);
                    }
                }
            }
        }

        private bool IsInside(Vector2 position) { return ScriptHelper.IntersectCircle(position, m_explodePosition, CurrentSmokeRadius); }
        private bool IsInside(IPlayer player)
        {
            var hitBox = player.GetAABB();
            return ScriptHelper.IntersectCircle(hitBox, m_explodePosition, CurrentSmokeRadius) && hitBox.Top >= m_groundPositionY;
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (!args.RemoveFlag) return;

            var hitObject = Game.GetObject(args.HitObjectID);
            if (hitObject != null && hitObject.GetCollisionFilter().CategoryBits == CategoryBits.StaticGround)
                return;

            m_explodePosition = Instance.Position;
            m_explodeTime = Game.TotalElapsedGameTime;
            Instance.FlagForRemoval();

            var rcResult = Game.RayCast(args.HitPosition, args.HitPosition - Vector2.UnitY * SmokeRadius, new RayCastInput()
            {
                IncludeOverlap = true,
                ClosestHitOnly = true,
                FilterOnMaskBits = true,
                MaskBits = CategoryBits.StaticGround,
            }).Where(r => r.HitObject != null);

            if (rcResult.Any()) m_groundPositionY = rcResult.Single().HitObject.GetWorldPosition().Y;
            else m_groundPositionY = float.MinValue;
        }
    }
}
