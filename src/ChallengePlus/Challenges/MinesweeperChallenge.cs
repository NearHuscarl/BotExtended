using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus.Challenges
{
    public class MinesweeperChallenge : Challenge
    {
        public MinesweeperChallenge(ChallengeName name) : base(name) { }

        public override string Description
        {
            get { return "Spawns mine randomly every 2 seconds."; }
        }

        private readonly Func<float, bool> _isElapsedSpawnMine = ScriptHelper.WithIsElapsed();
        private static readonly List<IObject> Mines = new List<IObject>();

        public override void OnUpdate(float e)
        {
            base.OnUpdate(e);

            if (Game.IsEditorTest) Game.DrawText(Mines.Count + "", Vector2.Zero);
            if (_isElapsedSpawnMine(2000)) SpawnMineRandomly();
        }

        private static void SpawnMineRandomly()
        {
            var groundPathNodes = Game.GetObjects<IObjectPathNode>()
                .Where(x => IsGroundPathNode(x) && Mines.All(m => !m.GetAABB().Intersects(x.GetAABB())))
                .ToList();
            if (groundPathNodes.Count == 0) return;
            
            var node = RandomHelper.GetItem(groundPathNodes);
            var mine = Game.CreateObject("WpnMineThrown", node.GetWorldPosition());

            Mines.Add(mine);
        }

        public override void OnObjectTerminated(IObject[] objs)
        {
            base.OnObjectTerminated(objs);
            
            foreach (var o in objs)
            {
                if (o.Name == "WpnMineThrown")
                    Mines.Remove(o);
            }
        }

        private static bool IsGroundPathNode(IObjectPathNode n) { return n.GetPathNodeType() == PathNodeType.Ground || n.GetPathNodeType() == PathNodeType.Platform; }
    }
}
