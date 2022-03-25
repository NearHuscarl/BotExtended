using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.MeleeWeapons
{
    class GroundBreakerPowerup : MeleeWpn
    {
        public override bool IsValidPowerup()
        {
            return IsHitTheFloorWeapon(Name);
        }

        public GroundBreakerPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.GroundBreaker) { }

        protected override void OnMeleeActionChanged(MeleeAction meleeAction, Vector2 hitPosition)
        {
            base.OnMeleeActionChanged(meleeAction, hitPosition);

            if (Owner.IsDead || CurrentMeleeAction != MeleeAction.Three) return;

            var groundObject = ScriptHelper.GetGroundObject(Owner);
            if (groundObject == null) return;

            var dir = Owner.GetFaceDirection();
            var groundSizeFactor = groundObject.GetSizeFactor();
            var os = ScriptHelper.SplitTileObject(groundObject, hitPosition);
            if (os.Length != 2) return;

            var oLeft = os[0];
            var oRight = os[1];
            var oLifted = dir == -1 ? oLeft : oRight;


            Game.PlayEffect(EffectName.CameraShaker, hitPosition, 10f, 400f, false);
            Game.PlaySound("Explosion", hitPosition);
            Game.PlayEffect(EffectName.Explosion, hitPosition);
            Game.PlayEffect(EffectName.BulletHit, hitPosition);
            Game.PlayEffect(EffectName.BulletHit, hitPosition);
            Game.PlayEffect(EffectName.BulletHit, hitPosition);

            if (oLifted.GetSizeFactor().X < 6) return; // only lift large ground object, small ground object will just crush everything.
            var oldMass = oLifted.GetMass();

            var iLeft = Game.CreateObject("InvisibleBlockNoCollision", oLeft.GetAABB().TopLeft);
            var iRight = Game.CreateObject("InvisibleBlockNoCollision", oRight.GetAABB().TopRight);
            var weldLeft = (IObjectWeldJoint)Game.CreateObject("WeldJoint");
            var weldRight = (IObjectWeldJoint)Game.CreateObject("WeldJoint");

            iLeft.SetBodyType(BodyType.Dynamic);
            iRight.SetBodyType(BodyType.Dynamic);
            weldLeft.SetTargetObjects(new IObject[] { oLeft, iLeft });
            weldRight.SetTargetObjects(new IObject[] { oRight, iRight });

            var distanceJoint = (IObjectDistanceJoint)Game.CreateObject("DistanceJoint", oLeft.GetAABB().TopRight);
            var distanceTarget = (IObjectTargetObjectJoint)Game.CreateObject("TargetObjectJoint", oRight.GetAABB().TopLeft);
            var invisiblePlatform = Game.CreateObject("InvisiblePlatform", oLeft.GetAABB().BottomLeft);

            invisiblePlatform.SetSizeFactor(new Point(groundSizeFactor.X, 1));
            distanceJoint.SetLengthType(DistanceJointLengthType.Elastic);
            distanceJoint.SetTargetObject(oLeft);
            distanceJoint.SetTargetObjectJoint(distanceTarget);
            distanceTarget.SetTargetObject(oRight);

            var iLifted = dir == -1 ? iLeft : iRight;
            var eqArea = ScriptHelper.Area(
                dir == -1 ? iLeft.GetAABB().TopLeft : iRight.GetAABB().TopRight + Vector2.UnitY * 20,
                dir == -1 ? iLeft.GetAABB().BottomLeft + Vector2.UnitX * 60 : iRight.GetAABB().BottomRight - Vector2.UnitX * 60
                );
            EarthquakePowerup.CreateEarthquake(eqArea);

            oLifted.SetMass(.01f);
            var force = Math.Min((float)Math.Pow(oLifted.GetSizeFactor().X, 3) * .06f, 300);
            Events.UpdateCallback.Start(e =>
            {
                iLifted.SetLinearVelocity(Vector2.UnitY * force);
            }, 0, 5);

            //ScriptHelper.RunIn(() => Game.DrawText(force.ToString(), iLifted.GetWorldPosition()), 3000);

            ScriptHelper.Timeout(() =>
            {
                oLifted.SetMass(oldMass);
                invisiblePlatform.Destroy();
                distanceJoint.Destroy();
                distanceTarget.Destroy();
                iLeft.Destroy();
                iRight.Destroy();
                weldLeft.Destroy();
                weldRight.Destroy();
            }, 2000);
        }
    }
}
