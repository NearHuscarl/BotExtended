using BotExtended.Bots;
using BotExtended.Factions;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Weapons
{
    public class LaserSweeper : Weapon
    {
        enum State { Normal, CheckingFire, Firing }
        private State _state = State.Normal;

        public MetroCopBot Bot { get; private set; }
        public Vector2 StationaryPosition { get; private set; }
        private float _rotatedAngle { get; set; }
        private int _facingDirection { get; set; }
        public StreetsweeperWeaponType Weapon { get; private set; }
        public IObjectStreetsweeper Sweeper { get; private set; }
        public override IEnumerable<IObject> Components { get; set; }
        public static readonly float SweepAngle = 65;

        public LaserSweeper(MetroCopBot bot) : base(bot.Player)
        {
            Bot = bot;

            Sweeper = (IObjectStreetsweeper)Game.CreateObject("Streetsweeper", bot.Position + Vector2.UnitY * 3);
            Sweeper.SetOwnerPlayer(bot.Player);
            Sweeper.CustomID = "LaserSweeper";
            Weapon = Sweeper.GetWeaponType();
            Components = new List<IObject>() { Sweeper };
            Instance = Sweeper;
         
            _fireLaserTime = Game.TotalElapsedGameTime;
            _isElapsedCheckFireLaser = ScriptHelper.WithIsElapsed(1500);
            _isElapsedUpdateFiring = ScriptHelper.WithIsElapsed(16);
            _isElapsedSound = ScriptHelper.WithIsElapsed(250);
            _isElapsedUpdatePosition = ScriptHelper.WithIsElapsed(800, 2000);
        }

        private float _fireLaserTime;
        private Func<bool> _isElapsedCheckFireLaser;
        private Func<bool> _isElapsedUpdateFiring;
        public override void Update(float elapsed)
        {
            switch (_state)
            {
                case State.Normal:
                    UpdatePosition();
                    if (ScriptHelper.IsElapsed(_fireLaserTime, 4100))
                        _state = State.CheckingFire;
                    break;
                case State.CheckingFire:
                    UpdatePosition();
                    if (_isElapsedCheckFireLaser() && CanFire())
                        FireLaser();
                    break;
                case State.Firing:
                    if (_isElapsedUpdateFiring())
                        UpdateFiring();
                    break;
            }
        }

        private Func<bool> _isElapsedUpdatePosition;
        private void UpdatePosition()
        {
            if (!_isElapsedUpdatePosition()) return;

            // try to stay away and above the players before shooting laser
            float minDistance;
            var player = ScriptHelper.GetClosestPlayer(Position, p => !p.IsDead, out minDistance);

            if (minDistance < 40)
            {
                var moveDir = Vector2.Normalize(Position - player.GetWorldPosition());
                Sweeper.SetLinearVelocity(moveDir * RandomHelper.Between(3, 8));
            }
        }

        private Vector2 EyePosition { get { return new Vector2(Position.X + Sweeper.GetFaceDirection(), Position.Y); } }
        private Area ScanArea { get { return ScriptHelper.Area(EyePosition, EyePosition + new Vector2(180 * Sweeper.GetFaceDirection(), -120)); } }

        private bool CanFire()
        {
            if (Sweeper.GetLinearVelocity().Length() > 5) return false;

            var enemiesInRange = Game.GetObjectsByArea<IPlayer>(ScanArea).Where(x => !x.IsDead && !ScriptHelper.SameTeam(x, Owner)).Take(3);
            foreach (var player in enemiesInRange)
            {
                var results = Game.RayCast(EyePosition, player.GetWorldPosition(), new RayCastInput()
                {
                    FilterOnMaskBits = true,
                    MaskBits = CategoryBits.StaticGround + CategoryBits.Player,
                    AbsorbProjectile = RayCastFilterMode.True,
                    IncludeOverlap = true,
                }).Where(r => r.HitObject != null);

                var result = results.FirstOrDefault();
                if (result.IsPlayer && Position.Y - result.HitObject.GetWorldPosition().Y > 30) return true;
            }

            return false;
        }

        private void FireLaser()
        {
            _state = State.Firing;
            _facingDirection = Sweeper.GetFaceDirection();
            Sweeper.SetWeaponType(StreetsweeperWeaponType.None);
            StationaryPosition = EyePosition;

            var startAngle = MathExtension.ToRadians(Sweeper.GetFaceDirection() == 1 ? -SweepAngle : -180 + SweepAngle);
            _laser = new RopeObject("Laser00", EyePosition + ScriptHelper.GetDirection(startAngle) * 6 + Vector2.UnitY * 3);
            _laser.SetAngle(startAngle);
            _laserSplash = Game.CreateObject("LaserSplash00");
        }

        private Func<bool> _isElapsedSound;
        private RopeObject _laser;
        private IObject _laserSplash;
        private void UpdateFiring()
        {
            Sweeper.SetWorldPosition(StationaryPosition);
            Sweeper.SetFaceDirection(_facingDirection);

            var angle = _laser.GetAngle();
            var nextAngle = angle + MathExtension.OneDeg / 2 * Sweeper.GetFaceDirection();
            _rotatedAngle += Math.Abs(nextAngle - angle);
            if (_rotatedAngle >= MathExtension.ToRadians(SweepAngle))
            {
                StopFiring();
                return;
            }

            if (_isElapsedSound())
                Game.PlaySound("Sawblade", Position);

            var start = _laser.StartPosition;
            var nextDir = ScriptHelper.GetDirection(nextAngle);
            var end = start + nextDir * ScriptHelper.GetDistanceToEdge(start, nextDir);
            var results = Game.RayCast(start, end, new RayCastInput()
            {
                FilterOnMaskBits = true,
                MaskBits = CategoryBits.StaticGround + CategoryBits.Player + CategoryBits.Dynamic,
            }).Where(r => r.HitObject != null);

            var groundResult = results.FirstOrDefault(x => ScriptHelper.IsHardStaticGround(x.HitObject));
            // laser shoot to the void
            if (groundResult.HitObject == null)
            {
                _laser.SetEndPosition(nextAngle, 1500);
                _laserSplash.SetWorldPosition(ScriptHelper.GetFarAwayPosition());
                return;
            }
            var distance = Vector2.Distance(_laser.StartPosition, groundResult.Position);
            _laser.SetEndPosition(nextAngle, distance);

            var splashAngle = ScriptHelper.GetAngle(groundResult.Normal);
            _laserSplash.SetWorldPosition(groundResult.Position + groundResult.Normal * RopeObject.TILE_SIZE);
            _laserSplash.SetAngle(ScriptHelper.GetAngle(groundResult.Normal) - MathExtension.PIOver2);

            foreach (var result in results)
            {
                result.HitObject.DealDamage(1);
                if (RandomHelper.Percentage(.1f)) result.HitObject.SetMaxFire();
            }
        }

        private void StopFiring()
        {
            _rotatedAngle = 0f;
            _state = State.Normal;
            Sweeper.SetWeaponType(Weapon);
            _fireLaserTime = Game.TotalElapsedGameTime;
            _laser.Remove();
            _laserSplash.Remove();
        }

        public override void OnComponentTerminated(IObject component)
        {
            base.OnComponentTerminated(component);
            if (_state == State.Firing) StopFiring();

            MetroCopBot.Sweepers.Remove(this);
            IsDestroyed = true;
        }
    }
}
