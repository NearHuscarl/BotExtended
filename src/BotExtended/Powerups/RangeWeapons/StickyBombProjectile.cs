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
        private float _hitTime = 0f;
        public IObject TargetedObject { get; private set; }

        public IObjectWeldJoint _weldJoint;

        public StickyBombProjectile(IProjectile projectile) : base(projectile, RangedWeaponPowerup.StickyBomb) { }

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

            if (TargetedObject != null)
                Game.DrawArea(TargetedObject.GetAABB(), Color.Red);

            if (_hitTime != 0 && ScriptHelper.IsElapsed(_hitTime, 2000))
            {
                if (_weldJoint != null) _weldJoint.Remove();
                DealExplosionDamage();
            }
        }

        private void DealExplosionDamage()
        {
            var center = Instance.GetWorldPosition();
            var filterArea = ScriptHelper.GrowFromCenter(center, Constants.ExplosionRadius * 2);
            var objectsInRadius = Game.GetObjectsByArea(filterArea)
                .Where(o => filterArea.Contains(o.GetAABB())
                && ScriptHelper.IsActiveObject(o)
                && ScriptHelper.IntersectCircle(o.GetAABB(), center, Constants.ExplosionRadius));

            foreach (var o in objectsInRadius)
            {
                if (!o.Destructable && o.GetBodyType() == BodyType.Static) o.SetBodyType(BodyType.Dynamic);
            }

            Instance.Destroy();
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
                    .Where(o => o.UniqueID != Instance.UniqueID && ScriptHelper.IsActiveObject(o))
                    .FirstOrDefault();

                if (TargetedObject != null)
                {
                    _hitTime = Game.TotalElapsedGameTime;
                    var player = ScriptHelper.AsPlayer(TargetedObject);

                    if (player != null)
                        ScriptHelper.WeldPlayer(player, Instance);
                    else
                        _weldJoint = ScriptHelper.Weld(Instance, TargetedObject);
                }
            }

            m_lastVelocity = currentVec;
            m_lastAngle = Instance.GetAngle();
        }
    }
}
