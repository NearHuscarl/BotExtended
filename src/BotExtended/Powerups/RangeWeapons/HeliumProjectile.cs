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
    class HeliumProjectile : Projectile
    {
        private class Info
        {
            public Info(IPlayer player)
            {
                Player = player;
                var magnetPosition = GetMagnetPosition();

                PullJoint = (IObjectPullJoint)Game.CreateObject("PullJoint");
                Magnet = Game.CreateObject("InvisibleBlockSmall");
                MagnetJoint = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint");

                Magnet.SetCollisionFilter(Constants.NoCollision);
                Magnet.SetWorldPosition(magnetPosition);
                MagnetJoint.SetWorldPosition(magnetPosition);
                MagnetJoint.SetTargetObject(Magnet);

                PullJoint.SetWorldPosition(Player.GetWorldPosition());
                PullJoint.SetTargetObject(Player);
                PullJoint.SetTargetObjectJoint(MagnetJoint);
                PullJoint.SetForce(0);
            }

            private Vector2 GetMagnetPosition() { return Player.GetWorldPosition() + Vector2.UnitY * 50; }

            private List<KeyValuePair<float, float>> m_deflateTimes = new List<KeyValuePair<float, float>>();
            private float m_updateDelay = 0f;
            private bool m_oldIsFalling = false;
            private Vector2 m_oldLinearVelocity = Vector2.Zero;
            private float m_fallingTime = 0f;
            public void Update(float elapsed)
            {
                if (ScriptHelper.IsElapsed(m_updateDelay, 250))
                {
                    m_updateDelay = Game.TotalElapsedGameTime;
                    var magnetPosition = GetMagnetPosition();
                    Magnet.SetWorldPosition(magnetPosition);
                    MagnetJoint.SetWorldPosition(magnetPosition);
                    PullJoint.SetForce(InflatedModifier);
                    if (!Player.IsFalling && RandomHelper.Percentage(InflatedModifier))
                        ScriptHelper.ExecuteSingleCommand(Player, PlayerCommandType.Fall);

                    if (m_deflateTimes.Any())
                    {
                        var deflateInfo = m_deflateTimes.First();
                        if (Game.TotalElapsedGameTime >= deflateInfo.Key)
                        {
                            Deflate(deflateInfo.Value);
                            m_deflateTimes.RemoveAt(0);
                        }
                    }
                }

                var velocity = Player.GetLinearVelocity();

                if (Player.IsFalling && !m_oldIsFalling)
                {
                    m_oldIsFalling = true;
                    m_fallingTime = Game.TotalElapsedGameTime;

                    var velocityDiff = MathExtension.Diff(velocity.Length(), m_oldLinearVelocity.Length());

                    //ScriptHelper.RunIn(() => Game.DrawText(velocityDiff.ToString(),
                        //Player.GetWorldPosition() + Vector2.UnitY * 15), 1000);
                    if (velocityDiff >= 4)
                        Player.SetLinearVelocity(velocity + Vector2.Normalize(velocity) * InflatedModifier * 70);
                }
                if (!Player.IsFalling && m_oldIsFalling)
                    m_oldIsFalling = false;

                m_oldLinearVelocity = velocity;
            }

            public void Inflate(float modifier)
            {
                var prevDeflateTime = m_deflateTimes.Any() ? m_deflateTimes.Last().Key : Game.TotalElapsedGameTime;
                prevDeflateTime = Math.Max(prevDeflateTime, Game.TotalElapsedGameTime);
                m_deflateTimes.Add(new KeyValuePair<float, float>(prevDeflateTime + 10000, modifier));
                ScriptHelper.LogDebug(modifier);
                InflatedModifier += .0125f * modifier; // .2f is no-return value (~16 shots)

                var mod = Player.GetModifiers();
                mod.SizeModifier += 0.015f * modifier;
                mod.ImpactDamageTakenModifier -= .1f * modifier;
                Player.SetModifiers(mod);
            }

            public void Deflate(float modifier)
            {
                InflatedModifier -= .0125f * modifier;

                var mod = Player.GetModifiers();
                mod.SizeModifier -= 0.015f * modifier;
                mod.ImpactDamageTakenModifier += .1f * modifier;
                Player.SetModifiers(mod);
            }

            public void Remove()
            {
                PullJoint.Remove();
                MagnetJoint.Remove();
                Magnet.Remove();
            }

            public IPlayer Player;
            public IObjectPullJoint PullJoint;
            public IObjectTargetObjectJoint MagnetJoint;
            public IObject Magnet;
            public float InflatedModifier = 0f;
        }

        private static Dictionary<int, Info> HeliumInfos = new Dictionary<int, Info>();
        private static Info GetInfo(IPlayer player)
        {
            Info info;

            if (!HeliumInfos.TryGetValue(player.UniqueID, out info))
            {
                info = new Info(player);
                HeliumInfos.Add(player.UniqueID, info);
            }
            return info;
        }

        static HeliumProjectile()
        {
            Events.PlayerDeathCallback.Start((p, a) =>
            {
                if (a.Removed)
                {
                    GetInfo(p).Remove();
                    HeliumInfos.Remove(p.UniqueID);
                }
            });
            Events.PlayerDamageCallback.Start((IPlayer p, PlayerDamageArgs a) =>
            {
                var info = GetInfo(p);
                var dmgTypeModifier = 1f;

                if (a.DamageType == PlayerDamageEventType.Missile) dmgTypeModifier = 1.5f;
                if (a.DamageType == PlayerDamageEventType.Projectile) dmgTypeModifier = 2f;
                if (a.DamageType == PlayerDamageEventType.Explosion) dmgTypeModifier = 4f;

                var popChance = info.InflatedModifier * 0.05f * a.Damage * dmgTypeModifier;
                if (info.InflatedModifier >= .15f && RandomHelper.Percentage(popChance))
                    Game.TriggerExplosion(info.Player.GetWorldPosition());
            });
            Events.UpdateCallback.Start(e =>
            {
                if (Game.IsEditorTest)
                {
                    foreach (var info in HeliumInfos.Values)
                        Game.DrawText(ScriptHelper.ToDisplayString(info.InflatedModifier, info.Player.GetModifiers().SizeModifier),
                            info.Player.GetWorldPosition());
                }
                foreach (var info in HeliumInfos.Values) info.Update(e);
            });
        }

        public HeliumProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.Helium)
        {
        }

        protected override bool OnProjectileCreated()
        {
            Instance.DamageDealtModifier *= .25f;
            return base.OnProjectileCreated();
        }

        public override void OnProjectileHit(ProjectileHitArgs args)
        {
            base.OnProjectileHit(args);

            if (!args.IsPlayer) return;
            var bot = BotManager.GetBot(args.HitObjectID);
            if (bot == Bot.None) return;

            var info = GetInfo(bot.Player);
            var modifier = Instance.GetProperties().PlayerDamage / 6; // SMG projectile deals 6hp
            info.Inflate(modifier);
        }
    }
}
