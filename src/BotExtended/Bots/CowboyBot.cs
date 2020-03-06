using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class CowboyBot : Bot
    {
        public float DisarmChance { get; set; }

        public CowboyBot()
        {
            DisarmChance = Game.IsEditorTest ? 1f : .15f;
        }
    }
}
