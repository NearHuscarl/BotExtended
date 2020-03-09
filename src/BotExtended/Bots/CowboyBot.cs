using BotExtended.Factions;
using SFDGameScriptInterface;
using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class CowboyBot : Bot
    {
        public float DisarmChance { get; set; }
        public float CritDisarmChance { get; set; }
        public float DestroyWeaponWhenDisarmChance { get; set; }
        public float DestroyWeaponWhenCritDisarmChance { get; set; }

        public CowboyBot(BotArgs args) : base(args)
        {
            if (Info.IsBoss)
            {
                DisarmChance = Game.IsEditorTest ? 1f : .35f;
                CritDisarmChance = Game.IsEditorTest ? 1f : .65f;
                DestroyWeaponWhenDisarmChance = Game.IsEditorTest ? 0f : .15f;
                DestroyWeaponWhenCritDisarmChance = Game.IsEditorTest ? 0f : .35f;
            }
            else
            {
                DisarmChance = Game.IsEditorTest ? 1f : .15f;
                CritDisarmChance = Game.IsEditorTest ? 1f : .15f;
                DestroyWeaponWhenDisarmChance = Game.IsEditorTest ? 1f : 0f;
                DestroyWeaponWhenCritDisarmChance = .01f;
            }
        }
    }
}
