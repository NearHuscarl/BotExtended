using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended
{
    public class RopeObject
    {
        private IObject _rope;
        private IObject _ropeTip;
        public static readonly int TILE_SIZE = 8;

        public RopeObject(string name, Vector2 position)
        {
            _rope = Game.CreateObject(name, position);
            _ropeTip = Game.CreateObject(name, position);
        }

        public float GetAngle()
        {
            return _rope.GetAngle();
        }

        public void SetAngle(float value)
        {
            _rope.SetAngle(value);
            _ropeTip.SetAngle(value);
        }

        public void SetStartPosition(Vector2 position)
        {
            _rope.SetWorldPosition(position);
        }

        public void SetEndPosition(float newAngle, float distance)
        {
            _rope.SetAngle(newAngle);
            _ropeTip.SetAngle(newAngle);

            var sizeFactor = distance / TILE_SIZE + 1;
            var tipOffset = sizeFactor - (int)sizeFactor;
            var updatedDir = ScriptHelper.GetDirection(newAngle);
            var end = StartPosition + ScriptHelper.GetDirection(newAngle) * distance;
            var tipPos = end - updatedDir * 1 /* avoid the gap */ + updatedDir * tipOffset;

            _ropeTip.SetWorldPosition(tipPos);
            if (_rope.GetSizeFactor().X != sizeFactor) _rope.SetSizeFactor(new Point((int)sizeFactor, 0));
        }

        public void SetEndPosition(Vector2 position)
        {
            var direction = Vector2.Normalize(position - StartPosition);
            var angle = ScriptHelper.GetAngle(direction);

            _rope.SetAngle(angle);
            _ropeTip.SetAngle(angle);

            var ropeLength = Vector2.Distance(StartPosition, position);
            var sizeFactor = ropeLength / TILE_SIZE + 1;
            var tipOffset = sizeFactor - (int)sizeFactor;
            var tipPos = position - direction * 1 /* avoid the gap */ + direction * tipOffset;

            _ropeTip.SetWorldPosition(tipPos);
            if (_rope.GetSizeFactor().X != sizeFactor)
                _rope.SetSizeFactor(new Point((int)sizeFactor, 0));
        }

        public void Remove()
        {
            if (_rope == null) return;
            _rope.Remove();
            _ropeTip.Remove();
        }

        public bool IsRemoved { get { return _rope.IsRemoved; } }
        public Vector2 StartPosition { get { return _rope.GetWorldPosition(); } }
    }
}
