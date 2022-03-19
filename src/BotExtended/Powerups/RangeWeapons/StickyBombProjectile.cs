using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotExtended.Library;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.RangeWeapons
{
    class StickyBombProjectile : CustomProjectile
    {
        private float m_timeElasped = 0f;
        public IObject TargetedObject { get; private set; }
        public IPlayer TargetedPlayer { get; private set; }

        private Vector2 m_relPlayerPosition;
        public IObjectWeldJoint m_weldJoint;

        public StickyBombProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.StickyBomb)
        {
        }

        protected override IObject OnProjectileCreated(IProjectile projectile)
        {
            Vector2 velocity;

            switch (projectile.ProjectileItem)
            {
                case ProjectileItem.FLAREGUN:
                case ProjectileItem.BOW:
                case ProjectileItem.GRENADE_LAUNCHER:
                    velocity = projectile.Velocity / 30 + Vector2.UnitY * 3;
                    break;
                default:
                    velocity = projectile.Velocity / 35;
                    break;
            }

            return CreateCustomProjectile(projectile, "WpnC4Thrown", velocity);
        }

        protected override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (TargetedObject == null)
                CheckIfCollide();

            if (TargetedObject != null) Game.DrawArea(TargetedObject.GetAABB(), Color.Red);
            if (TargetedPlayer != null)
            {
                if (TargetedPlayer.IsOnGround)
                {
                    if (m_weldJoint != null)
                    {
                        Instance.SetBodyType(BodyType.Static);
                        m_weldJoint.Remove();
                        m_weldJoint = null;
                    }

                    if (TargetedPlayer.IsCrouching)
                    {
                        Instance.SetWorldPosition(TargetedPlayer.GetWorldPosition() - m_relPlayerPosition - Vector2.UnitY * 5);
                    }
                    else if (TargetedPlayer.IsRolling)
                    {
                        Instance.SetWorldPosition(TargetedPlayer.GetAABB().Center);
                    }
                    else
                    {
                        Instance.SetWorldPosition(TargetedPlayer.GetWorldPosition() - m_relPlayerPosition);
                    }
                }
                else if (TargetedPlayer.IsInMidAir) // cannot track position accurately when player is in mid air
                {
                    if (m_weldJoint == null)
                    {
                        Instance.SetBodyType(BodyType.Dynamic);
                        m_weldJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
                        m_weldJoint.SetWorldPosition(Instance.GetWorldPosition());
                        m_weldJoint.SetTargetObjects(new List<IObject>() { Instance, TargetedPlayer });
                    }
                }
            }

            if (m_timeElasped != 0 && ScriptHelper.IsElapsed(m_timeElasped, 2000))
            {
                if (m_weldJoint != null) m_weldJoint.Remove();
                DealExplosionDamage();
                Instance.Destroy();
            }
        }

        private void DealExplosionDamage()
        {
            var center = Instance.GetWorldPosition();
            var filterArea = ScriptHelper.GrowFromCenter(center, Constants.ExplosionRadius * 2);
            var objectsInRadius = Game.GetObjectsByArea(filterArea)
                .Where(o => filterArea.Contains(o.GetAABB())
                && ScriptHelper.IntersectCircle(o.GetAABB(), center, Constants.ExplosionRadius));

            foreach (var o in objectsInRadius)
            {
                if (ScriptHelper.IsIndestructible(o)) o.DealDamage(1f);
            }
        }

        private Vector2 m_lastVelocity;
        private float m_lastAngle;
        private void CheckIfCollide()
        {
            var currentVec = Instance.GetLinearVelocity();

            if (currentVec.Length() - m_lastVelocity.Length() <= -6
                || MathExtension.Diff(Instance.GetAngle(), m_lastAngle) >= MathExtension.OneDeg * 3
                || TotalDistanceTraveled >= 15 && currentVec.Length() <= 1)
            {
                TargetedObject = Game.GetObjectsByArea(Instance.GetAABB())
                    .Where(o => o.UniqueID != Instance.UniqueID && ScriptHelper.IsInteractiveObject(o))
                    .FirstOrDefault();

                if (TargetedObject != null)
                {
                    m_timeElasped = Game.TotalElapsedGameTime;
                    TargetedPlayer = ScriptHelper.CastPlayer(TargetedObject);

                    if (TargetedPlayer != null)
                    {
                        Instance.SetBodyType(BodyType.Static);
                        m_relPlayerPosition = TargetedPlayer.GetWorldPosition() - Instance.GetWorldPosition();
                    }
                    else
                    {
                        m_weldJoint = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
                        m_weldJoint.SetWorldPosition(Instance.GetWorldPosition());
                        m_weldJoint.SetTargetObjects(new List<IObject>() { Instance, TargetedObject });
                    }
                }
            }

            m_lastVelocity = currentVec;
            m_lastAngle = Instance.GetAngle();
        }
    }
}
