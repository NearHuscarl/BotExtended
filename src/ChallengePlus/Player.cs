using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public class Player
    {
        public static readonly Player None;
        public static readonly string NoneCustomID = "BeNonePlayer";

        public IPlayer Instance { get; private set; }
        public bool IsBot { get { return Instance.IsBot; } }
        public int UniqueID { get { return Instance.UniqueID; } }
        public int Direction { get { return Instance.FacingDirection; } }
        public Area Hitbox { get { return Instance.GetAABB(); } }

        public Vector2 Position
        {
            get { return Instance.GetWorldPosition(); }
            set { Instance.SetWorldPosition(value); }
        }
        public Vector2 Velocity
        {
            get { return Instance.GetLinearVelocity(); }
            set { Instance.SetLinearVelocity(value); }
        }

        public void DealDamage(float damage) { Instance.DealDamage(damage); }
        public void DealDamage(float damage, int sourceID) { Instance.DealDamage(damage, sourceID); }

        static Player()
        {
            var nonePlayer = Game.CreatePlayer(Vector2.Zero);
            nonePlayer.Remove();
            nonePlayer.CustomID = NoneCustomID;
            None = new Player(nonePlayer);
        }

        public Player(IPlayer player)
        {
            Instance = player;
        }
    }
}
