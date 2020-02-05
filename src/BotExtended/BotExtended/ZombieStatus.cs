namespace SFDScript.BotExtended
{
    public enum ZombieStatus
    {
        // Not infected by zombie. Do not turn into zombie when dying
        Human,

        // Infected by zombie or other infected. Start turning into zombie when dying
        Infected,

        // Most zombies dont turn again after dying
        Zombie,
    }
}
