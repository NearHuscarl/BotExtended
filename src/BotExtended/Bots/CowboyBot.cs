using BotExtended.Factions;
using SFDGameScriptInterface;
using static BotExtended.Library.SFD;

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
                DisarmChance = Game.IsEditorTest ? 1 : .45f;
                CritDisarmChance = Game.IsEditorTest ? 1 : .45f;
                DestroyWeaponWhenDisarmChance = .35f;
                DestroyWeaponWhenCritDisarmChance = .35f;
            }
            else
            {
                DisarmChance = .15f;
                CritDisarmChance = .15f;
                DestroyWeaponWhenDisarmChance = 0f;
                DestroyWeaponWhenCritDisarmChance = .01f;
            }
        }
    }
}
