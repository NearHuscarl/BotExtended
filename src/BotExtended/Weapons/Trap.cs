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
    public class Trap : Weapon
    {
        public override IEnumerable<IObject> Components { get; set; }
        public bool IsTriggered { get; private set; }
        private bool _ownerLeft = false;
        public Trap(IPlayer owner, string name, Vector2 offset) : base(owner)
        {
            var pos = Owner.GetWorldPosition();
            Instance = Game.CreateObject(name, pos + offset);
            Instance.SetBodyType(BodyType.Static);
            Instance.CustomID = name;
            Components = new List<IObject>() { Instance };
            IsTriggered = false;
            FriendlyTrigger = 0.15f;
        }

        public override void Update(float elapsed)
        {
            if (IsTriggered) OnUpdateAfterTrigger();
            else
            {
                if (!_ownerLeft && !Owner.GetAABB().Intersects(Instance.GetAABB()) || Owner.IsDead)
                    _ownerLeft = true;

                foreach (var p in Game.GetPlayers())
                {
                    if (p.IsDead || p.GetLinearVelocity() == Vector2.Zero) continue;
                    if (p.UniqueID == Owner.UniqueID && !_ownerLeft) continue;

                    if (Instance.GetAABB().Intersects(p.GetAABB()))
                    {
                        if (!ScriptHelper.SameTeam(p, Owner) || RandomHelper.Percentage(Game.IsEditorTest ? 1 : FriendlyTrigger))
                            IsTriggered = OnTrigger(p);
                    }
                }
            }
        }

        public float FriendlyTrigger { get; protected set; }
        protected virtual void OnUpdateAfterTrigger() { }
        protected virtual bool OnTrigger(IPlayer player) { return true; }
    }
}
