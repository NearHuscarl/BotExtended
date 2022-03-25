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
    class GroundSlamPowerup : MeleeWpn
    {
        enum State { Normal, PunchingUp, Launching, Jumping, PunchingDown, WaitDown, }

        public override bool IsValidPowerup()
        {
            return Name == WeaponItem.NONE;
        }

        public GroundSlamPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.GroundSlam) { }

        private State _state = State.Normal;

        public override void OnMeleeAction(PlayerMeleeHitArg[] args)
        {
            base.OnMeleeAction(args);

            if (Owner.IsDead || args.Length == 0 || CurrentMeleeAction != MeleeAction.Three || _state != State.Normal) return;

            foreach (var arg in args)
            {
                if (!arg.IsPlayer) continue;
             
                var enemy = BotManager.GetBot(arg.ObjectID);
                if (enemy.Info.IsBoss || ScriptHelper.SameTeam(enemy.Player, Owner)) continue;
                if (enemy.Player.GetHealth() > 15) continue;

                _target = enemy.Player;
                ChangeState(State.PunchingUp);
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

            Game.DrawText(_state.ToString(), Owner.GetWorldPosition());

            if (Owner.IsDead && _state != State.Normal)
            {
                ChangeState(State.Normal); return;
            }

            switch (_state)
            {
                case State.PunchingUp:
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
                        ChangeState(State.Launching);
                    }
                    break;
                case State.Launching:
                    _target.SetLinearVelocity(Vector2.Zero);
                    _target.SetWorldPosition(_targetHoverPosition);
                    if (_executeLaunch)
                    {
                        _executeLaunch = false;
                        Owner.SetLinearVelocity(Vector2.UnitY * 12.5f);
                        Owner.AddCommand(new PlayerCommand(PlayerCommandType.Jump));
                        ChangeState(State.Jumping);
                    }
                    break;
                case State.Jumping:
                    _target.SetLinearVelocity(Vector2.Zero);
                    _target.SetWorldPosition(_targetHoverPosition);
                    Owner.SetLinearVelocity(Vector2.UnitY * 10);
                    if (Owner.IsInMidAir && Owner.GetWorldPosition().Y + 11 >= _target.GetWorldPosition().Y)
                    {
                        _ownerHoverPosition = Owner.GetWorldPosition();
                        Owner.ClearCommandQueue(); // without this line AttackOnce doesn't work
                        Owner.AddCommand(new PlayerCommand(PlayerCommandType.AttackOnce));
                        Game.RunCommand("/settime .1");
                        ChangeState(State.PunchingDown);
                        ScriptHelper.Timeout(() => _executeAttack = true, FullJumpAttackMeleeTime + 30);
                    }
                    break;
                case State.PunchingDown:
                {
                    Owner.SetLinearVelocity(Vector2.Zero);
                    Owner.SetWorldPosition(_ownerHoverPosition);
                    if (_executeAttack)
                    {
                        Game.RunCommand("/settime 1");
                        Owner.AddCommand(new PlayerCommand(PlayerCommandType.WaitLand));
                        ChangeState(State.WaitDown);
                        _target.SetLinearVelocity(-Vector2.UnitY * 40);
                        _executeAttack = false;

                        var cb = (Events.PlayerDamageCallback)null;
                        Action Finish = () => { cb.Stop(); ChangeState(State.Normal); };
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
                case State.WaitDown:
                    if (_target.IsRemoved)
                    {
                        ChangeState(State.Normal);
                    }
                    // if you hit static or indestructible objects, the damage callback is not invoked
                    if (_target.GetLinearVelocity().Y >= 0)
                    {
                        BreakObjects();
                        ChangeState(State.Normal);
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
                    ScriptHelper.SplitTileObject(hitObject, pos);
                    EarthquakePowerup.CreateEarthquake(ScriptHelper.GrowFromCenter(pos, 100, 50));
                    return true;
                }
            }
            return false;
        }

        private void ChangeState(State state)
        {
            if (state == State.Normal)
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
