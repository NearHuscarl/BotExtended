using BotExtended.Library;
using BotExtended.Powerups;
using SFDGameScriptInterface;
using System.Linq;
using static BotExtended.GameScript;
using static BotExtended.Library.SFD;

namespace BotExtended.Bots
{
    class BoffinBot : Bot
    {
        private float _initialSize = 0f;
        public BoffinBot(BotArgs args) : base(args)
        {
            _initialSize = Player.GetModifiers().SizeModifier;
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            var mod = Player.GetModifiers();
            if (mod.SizeModifier < _initialSize)
            {
                mod.SizeModifier = _initialSize;
                Player.SetModifiers(mod);
            }
        }
    }
}
