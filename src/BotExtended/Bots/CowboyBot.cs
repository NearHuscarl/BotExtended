using static BotExtended.Library.Mocks.MockObjects;

namespace BotExtended.Bots
{
    class CowboyBot : Bot
    {
        public float DisarmChance { get; set; }
        public float CritDisarmChance { get; set; }
        public float DestroyWeaponWhenDisarmChance { get; set; }
        public float DestroyWeaponWhenCritDisarmChance { get; set; }

        public CowboyBot()
        {
            if (Info.IsBoss)
            {
                DisarmChance = Game.IsEditorTest ? 1f : .3f;
                CritDisarmChance = Game.IsEditorTest ? 1f : .6f;
                DestroyWeaponWhenDisarmChance = .1f;
                DestroyWeaponWhenCritDisarmChance = .3f;
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
