using BotExtended.Bots;
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
    public class Chicken : Weapon
    {
        public FarmerBot FarmerBot { get; private set; }
        public IPlayer Target { get; private set; }
        public Vector2 TargetPosition { get { return Target == null ? Owner.GetWorldPosition() : Target.GetWorldPosition(); } }

        private static readonly float MaxDistanceFromOwner = 35f;
        public override IEnumerable<IObject> Components { get; set; }

        public Chicken(FarmerBot bot) : base(bot.Player)
        {
            FarmerBot = bot;
            Instance = Game.CreateObject("Chicken00", Owner.GetWorldPosition());
            Instance.CustomID = "Chicken";
            Instance.SetTargetAIData(new ObjectAITargetData
            {
                Team = Owner.GetTeam(),
                Range = 100,
                PriorityModifier = 5f,
            });
            Components = new List<IObject>() { Instance };
        }

        public override void Update(float elapsed)
        {
            Instance.SetAngularVelocity(0);
            Instance.SetAngle(0);

            FindClosestTarget();
            WanderAroundTarget();
            UpdateMovement();
            UpdateAttack();
        }

        private float _attackTime = 0;
        private void UpdateAttack()
        {
            if (Target == null) return;

            if (ScriptHelper.IsElapsed(_attackTime, 600))
            {
                foreach (var player in Game.GetPlayers())
                {
                    if (player.IsDead || ScriptHelper.SameTeam(player, Owner)) continue;
                    if (Instance.GetAABB().Intersects(player.GetAABB()))
                    {
                        player.DealDamage(2);
                        Game.PlayEffect(EffectName.Blood, player.GetWorldPosition());
                        if (RandomHelper.Percentage(.25f)) ScriptHelper.Fall(player);
                    }
                    _attackTime = Game.TotalElapsedGameTime;
                }
            }
        }

        private float _findTime = 0;
        private void FindClosestTarget()
        {
            if (ScriptHelper.IsElapsed(_findTime, 2600) && (Target == null || Vector2.Distance(Target.GetWorldPosition(), Position) >= 35))
            {
                var searchArea = ScriptHelper.GrowFromCenter(Position, 100, 5);
                var minDistance = float.MaxValue;
                foreach (var player in Game.GetObjectsByArea<IPlayer>(searchArea))
                {
                    if (player.IsDead || ScriptHelper.SameTeam(player, Team)) continue;

                    if (Owner.IsDead)
                        Game.WriteToConsole(Owner.GetTeam());
                    var distance = Vector2.Distance(Position, player.GetWorldPosition());
                    if (distance < minDistance)
                    {
                        Target = player; minDistance = distance;
                    }
                }
                _findTime = Game.TotalElapsedGameTime;
            }
        }

        public bool IsOnGround { get { return Math.Abs(Instance.GetLinearVelocity().Y) <= 0.1; } }

        private float _lastJumpTime = 0f;
        private float _jumpTime = 300f;
        private void UpdateMovement()
        {
            if (ScriptHelper.IsElapsed(_lastJumpTime, _jumpTime) && IsOnGround)
            {
                var diffY = MathHelper.Clamp(TargetPosition.Y - Position.Y, 0, 25);
                Instance.SetLinearVelocity(Vector2.UnitY * (3 + diffY * .17f));
                _jumpTime = RandomHelper.Between(300, 600);
                _lastJumpTime = Game.TotalElapsedGameTime;
            }
        }

        private float _destination = 0f;
        private float _lastMoveTime = 0f;
        private float _moveTime = 650;
        private Vector2 Destination
        {
            get
            {
                return new Vector2 { X = TargetPosition.X + _destination, Y = TargetPosition.Y };
            }
        }

        private void WanderAroundTarget()
        {
            if (!IsOnGround) return;
            if (ScriptHelper.IsElapsed(_lastMoveTime, _moveTime))
            {
                var distance = Destination.X - Position.X;
                var direction = Math.Sign(distance);
                var diffY = MathHelper.Clamp(TargetPosition.Y - Position.Y, 0, 25);

                Instance.SetLinearVelocity(new Vector2(direction * 5f, diffY * 0.2f));
                Instance.SetFaceDirection(direction);
                _lastMoveTime = Game.TotalElapsedGameTime;
                _moveTime = MathHelper.Clamp(RandomHelper.Between(350, 800) - Math.Abs(distance * 4), 20, 800);
            }

            if (Instance.GetAABB().Contains(Destination))
                _destination = RandomHelper.Between(-MaxDistanceFromOwner, MaxDistanceFromOwner);
        }

        public override void OnComponentTerminated(IObject component)
        {
            base.OnComponentTerminated(component);
            if (!FarmerBot.Player.IsDead) FarmerBot.OnChickenDead();
            IsDestroyed = true;
        }
    }
}
