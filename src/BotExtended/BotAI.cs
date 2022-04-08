namespace BotExtended
{
    public enum BotAI
    {
        Debug,
        None, // Normal SF (not extended bot)

        God,
        Hacker,
        Expert,
        Hard,
        Normal,
        Easy,

        MeleeExpert, // == BotAI.Hacker but with range weapons disabled
        MeleeHard, // == BotAI.Expert but with range weapons disabled
        RangeExpert, // == BotAI.Hacker but with melee weapons disabled
        RangeHard, // == BotAI.Expert but with melee weapons disabled

        Grunt,
        GruntMelee,
        Hulk,
        RagingHulk,

        // Jogger + hard melee/shooting
        AssassinMelee,
        AssassinRange,

        // Trigger-happy Grunt
        Cowboy,
        // Trigger-happy hard
        Sheriff,
        Spacer,
        SpacerExpert,

        Babybear,
        Kingpin,
        Meatgrinder,
        Ninja,
        Hunter,
        Sniper,
        Soldier,

        ZombieSlow,
        ZombieFast,
        ZombieHulk,
        ZombieFighter,
    }
}
