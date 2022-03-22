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
        enum GroundBreakerState { Normal, PunchingUp, Launching, Jumping, PunchingDown, WaitDown, }

        public override bool IsValidPowerup()
        {
            return Name == WeaponItem.NONE;
        }

        public GroundBreakerPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.GroundBreaker) { }

        private GroundBreakerState _state = GroundBreakerState.Normal;

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three || _state != GroundBreakerState.Normal) return;

            foreach (var arg in args)
            {
                if (!arg.IsPlayer) continue;
             
                var enemy = BotManager.GetBot(arg.ObjectID);
                if (enemy.Info.IsBoss || ScriptHelper.SameTeam(enemy.Player, Owner)) continue;
                if (enemy.Player.GetHealth() > 15) continue;

                _target = enemy.Player;
                ChangeState(GroundBreakerState.PunchingUp);
                _target.SetLinearVelocity(Vector2.UnitY * 11);
                _target.SetInputEnabled(false);
                _target.SetNametagVisible(false);
                _target.SetStatusBarsVisible(false);
                _target.SetHealth(0);
                _targetInitialPosition = _target.GetWorldPosition();
                var tMod = _target.GetModifiers();
                tMod.ImpactDamageTakenModifier = .0001f; // can never be gibbed when slammed, but still allow to register damage
                _target.SetModifiers(tMod);
                var mod = Owner.GetModifiers();
                mod.MeleeStunImmunity = 1;
                Owner.SetModifiers(mod);
                Owner.SetInputEnabled(false);
            }
        }

        private IPlayer _target;
        private Vector2 _targetInitialPosition;
        private Vector2 _targetHoverPosition;
        private Vector2 _ownerHoverPosition;
        private bool _executeLaunch = false;
        private bool _executeAttack = false;
        private static readonly uint FullJumpAttackMeleeTime = 500;

        public override void Update(float elapsed)
        {
            base.Update(elapsed);

            if (Owner.IsDead && _state != GroundBreakerState.Normal)
            {
                ChangeState(GroundBreakerState.Normal); return;
            }

            switch (_state)
            {
                case GroundBreakerState.PunchingUp:
                    var pos = _target.GetWorldPosition();
                    if (pos.X != _targetInitialPosition.X)
                    {
                        pos.X = _targetInitialPosition.X;
                        _target.SetWorldPosition(pos); // don't reflect horizontally
                    }
                    if (_target.GetLinearVelocity().Length() <= 1)
                    {
                        _targetHoverPosition = _target.GetWorldPosition();
                        Owner.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch, 0, 200));
                        Game.RunCommand("/settime .4");
                        ScriptHelper.Timeout(() => _executeLaunch = true, 220);
                        ChangeState(GroundBreakerState.Launching);
                    }
                    break;
                case GroundBreakerState.Launching:
                    _target.SetLinearVelocity(Vector2.Zero);
                    _target.SetWorldPosition(_targetHoverPosition);
                    if (_executeLaunch)
                    {
                        _executeLaunch = false;
                        Owner.SetLinearVelocity(Vector2.UnitY * 12.5f);
                        Owner.AddCommand(new PlayerCommand(PlayerCommandType.Jump));
                        ChangeState(GroundBreakerState.Jumping);
                    }
                    break;
                case GroundBreakerState.Jumping:
                    _target.SetLinearVelocity(Vector2.Zero);
                    _target.SetWorldPosition(_targetHoverPosition);
                    Owner.SetLinearVelocity(Vector2.UnitY * 10);
                    if (Owner.IsInMidAir && Owner.GetWorldPosition().Y + 11 >= _target.GetWorldPosition().Y)
                    {
                        _ownerHoverPosition = Owner.GetWorldPosition();
                        Owner.ClearCommandQueue(); // without this line AttackOnce doesn't work
                        Owner.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                        Game.RunCommand("/settime .1");
                        ChangeState(GroundBreakerState.PunchingDown);
                        ScriptHelper.Timeout(() => _executeAttack = true, FullJumpAttackMeleeTime + 30);
                    }
                    break;
                case GroundBreakerState.PunchingDown:
                {
                    Owner.SetLinearVelocity(Vector2.Zero);
                    Owner.SetWorldPosition(_ownerHoverPosition);
                    if (_executeAttack)
                    {
                        Game.RunCommand("/settime 1");
                        Owner.AddCommand(new PlayerCommand(PlayerCommandType.WaitLand));
                        ChangeState(GroundBreakerState.WaitDown);
                        _target.SetLinearVelocity(-Vector2.UnitY * 40);
                        _executeAttack = false;

                        var cb = (Events.PlayerDamageCallback)null;
                        Action Finish = () => { cb.Stop(); ChangeState(GroundBreakerState.Normal); };
                        cb = Events.PlayerDamageCallback.Start((player, args) =>
                        {
                            if (_target.IsRemoved || args.DamageType == PlayerDamageEventType.Explosion)
                            {
                                Finish(); return;
                            }
                            if (player.UniqueID != _target.UniqueID) return;
                            if (_target.GetCorpseHealth() <= 100) _target.SetCorpseHealth(1000);
                            if (args.DamageType == PlayerDamageEventType.Fall)
                            {
                                if (BreakObjects()) Finish();
                            }
                        });
                    }
                    break;
                }
                case GroundBreakerState.WaitDown:
                    if (_target.IsRemoved)
                    {
                        ChangeState(GroundBreakerState.Normal);
                    }
                    // if you hit static or indestructible objects, the damage callback is not invoked
                    if (_target.GetLinearVelocity().Y >= 0)
                    {
                        BreakObjects();
                        ChangeState(GroundBreakerState.Normal);
                    }
                    Owner.SetLinearVelocity(Vector2.Zero);
                    Owner.SetWorldPosition(_ownerHoverPosition);
                    break;
            }
        }

        private bool BreakObjects()
        {
            var pBox = _target.GetAABB();
            var hitObjects = Game.GetObjectsByArea(ScriptHelper.GrowFromCenter(pBox.Center, pBox.Width + 3, pBox.Height + 3));
            var pos = _target.GetWorldPosition();

            Game.PlayEffect(EffectName.CameraShaker, pBox.Center, 9f, 400f, false);
            foreach (var hitObject in hitObjects)
            {
                var cbits = hitObject.GetCollisionFilter().CategoryBits;
                if (hitObject.UniqueID == Owner.UniqueID || cbits != CategoryBits.DynamicG1 && cbits != CategoryBits.StaticGround) continue;
                if (cbits == CategoryBits.DynamicG1) hitObject.Destroy();
                if (cbits == CategoryBits.StaticGround)
                {
                    hitObject.SetBodyType(BodyType.Dynamic);
                    hitObject.SetLinearVelocity(Vector2.UnitY * 12);
                    Game.PlaySound("Explosion", pos);
                    SplitTileObject(hitObject, pos);
                    EarthquakePowerup.CreateEarthquake(ScriptHelper.GrowFromCenter(pos, 100, 50));
                    return true;
                }
            }
            return false;
        }

        private void SplitTileObject(IObject o, Vector2 position)
        {
            var xTiles = o.GetSizeFactor().X;
            var yTiles = o.GetSizeFactor().Y;

            if (xTiles == 1) return; // not a tile object

            var tileSize = 8;
            var leftPos = o.GetAABB().Left;

            ScriptHelper.Unscrew(o);
            var effectArea = ScriptHelper.GrowFromCenter(position, 8, 2);

            for (var i = 0; i < 4; i++)
                Game.PlayEffect(EffectName.BulletHitDefault, RandomHelper.WithinArea(effectArea));

            for (var i = 0; i < xTiles; i++)
            {
                if (leftPos + tileSize * i >= position.X)
                {
                    var oLeft = Game.CreateObject(o.Name, o.GetWorldPosition());
                    var oRight = Game.CreateObject(o.Name, o.GetWorldPosition() + Vector2.UnitX * tileSize * i);

                    oLeft.SetAngle(o.GetAngle());
                    oLeft.SetLinearVelocity(Vector2.UnitY * -20);
                    oLeft.SetSizeFactor(new Point(i - 1, yTiles));
                    oLeft.SetBodyType(BodyType.Dynamic);
                    oRight.SetAngle(o.GetAngle());
                    oRight.SetLinearVelocity(Vector2.UnitY * -20);
                    oRight.SetSizeFactor(new Point(xTiles - i, yTiles));
                    oRight.SetBodyType(BodyType.Dynamic);
                    break;
                }
            }
            o.Remove();
        }

        private void ChangeState(GroundBreakerState state)
        {
            if (state == GroundBreakerState.Normal)
            {
                _target.SetInputEnabled(true);
                Owner.SetInputEnabled(true);
                BotManager.GetBot(Owner).ResetModifiers();
                Game.RunCommand("/settime 1");
            }
            _state = state;
        }
    }
}
