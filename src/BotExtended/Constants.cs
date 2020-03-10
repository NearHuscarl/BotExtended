using BotExtended.Library;
using System.Linq;

namespace BotExtended
{
    public static class Constants
    {
        internal const int BOSS_FACTION_START_INDEX = 200;
        internal const string CURRENT_VERSION = "3.5.5";
        internal const string STORAGE_KEY_PREFIX = "BE_";

        // default settings value
        internal static readonly string[] DEFAULT_FACTIONS = BotHelper.GetAvailableBotFactions()
                .Select((f) => SharpHelper.EnumToString(f))
                .ToArray();
        internal const int DEFAULT_MAX_BOT_COUNT = 5;
        internal const int DEFAULT_FACTION_ROTATION_INTERVAL = 3;

        // boolean value (1|0) for fields like InfiniteAmmo, CanBurn, MeleeStunImmunity...
        internal const int TOGGLE_ON = 1;
        internal const int TOGGLE_OFF = 0;
    }

    /// <summary>
    /// Health range: 1-9999
    /// <para/>
    /// 
    /// Hacker = 400
    /// UltraStrong = 300
    /// ExtremelyStrong = 250
    /// VeryStrong = 200
    /// Strong = 150
    /// <para/>
    /// 
    /// AboveNormal = 125
    /// Normal = 100
    /// BelowNormal = 80
    /// <para/>
    /// 
    /// Weak = 65
    /// VeryWeak = 50
    /// ExtremelyWeak = 35
    /// EmbarrassinglyWeak = 20
    /// BarelyAny = 5
    /// </summary>
    public static class Health
    {
        internal const int Hacker = 400;
        internal const int UltraStrong = 300;
        internal const int ExtremelyStrong = 250;
        internal const int VeryStrong = 200;
        internal const int Strong = 150;
        internal const int AboveNormal = 125;
        internal const int Normal = 100;
        internal const int BelowNormal = 80;
        internal const int Weak = 65;
        internal const int VeryWeak = 50;
        internal const int ExtremelyWeak = 35;
        internal const int EmbarrassinglyWeak = 20;
        internal const int BarelyAny = 5;
    }

    /// <summary>
    /// Energy range: 1-9999
    /// <para/>
    /// 
    /// Hacker = 400
    /// UltraHigh = 300
    /// ExtremelyHigh = 250
    /// VeryHigh = 200
    /// High = 150
    /// <para/>
    /// 
    /// AboveNormal = 125
    /// Normal = 100
    /// BelowNormal = 80
    /// <para/>
    /// 
    /// Low = 65
    /// VeryLow = 50
    /// ExtremelyLow = 35
    /// EmbarrassinglyLow = 20
    /// BarelyAny = 5
    /// </summary>
    public static class Stamina
    {
        internal const int Hacker = 400;
        internal const int UltraHigh = 300;
        internal const int ExtremelyHigh = 250;
        internal const int VeryHigh = 200;
        internal const int High = 150;
        internal const int AboveNormal = 125;
        internal const int Normal = 100;
        internal const int BelowNormal = 80;
        internal const int Low = 65;
        internal const int VeryLow = 50;
        internal const int ExtremelyLow = 35;
        internal const int EmbarrassinglyLow = 20;
        internal const int BarelyAny = 5;
    }

    /// <summary>
    /// Speed range: 0.5-2.0
    /// <para/>
    /// 
    /// Hacker = 2
    /// ExtremelyFast = 1.5
    /// VeryFast = 1.35
    /// Fast = 1.2
    /// <para/>
    /// 
    /// AboveNormal = 1.1
    /// Normal = 1
    /// BelowNormal = 0.9
    /// <para/>
    /// 
    /// Slow = 0.8
    /// VerySlow = 0.7
    /// ExtremelySlow = 0.6
    /// BarelyAny = 0.5
    /// </summary>
    public static class Speed
    {
        internal const float Hacker = 2f;
        internal const float ExtremelyFast = 1.5f;
        internal const float VeryFast = 1.35f;
        internal const float Fast = 1.2f;
        internal const float AboveNormal = 1.1f;
        internal const float Normal = 1f;
        internal const float BelowNormal = .9f;
        internal const float Slow = .8f;
        internal const float VerySlow = .7f;
        internal const float ExtremelySlow = .6f;
        internal const float BarelyAny = .5f;
    }

    /// <summary>
    /// Size range: 0.75-1.25
    /// <para/>
    /// 
    /// Chonky = 1.25
    /// ExtremelyBig = 1.2
    /// VeryBig = 1.15
    /// Big = 1.1
    /// <para/>
    /// 
    /// AboveNormal = 1.05
    /// Normal = 1
    /// BelowNormal = 0.95
    /// <para/>
    /// 
    /// Small = 0.9
    /// VerySmall = 0.85
    /// ExtremelySmall = 0.8
    /// Tiny = 0.75
    /// </summary>
    public static class Size
    {
        internal const float Chonky = 1.25f;
        internal const float ExtremelyBig = 1.2f;
        internal const float VeryBig = 1.15f;
        internal const float Big = 1.1f;
        internal const float AboveNormal = 1.05f;
        internal const float Normal = 1f;
        internal const float BelowNormal = .95f;
        internal const float Small = .9f;
        internal const float VerySmall = .85f;
        internal const float ExtremelySmall = .8f;
        internal const float Tiny = .75f;
    }

    /// <summary>
    /// Melee force range: 0-10
    /// <para/>
    /// 
    /// UltraStrong = 3
    /// ExtremelyStrong = 2
    /// VeryStrong = 1.75
    /// Strong = 1.5
    /// <para/>
    /// 
    /// AboveNormal = 1.25
    /// Normal = 1
    /// Weak = 0.5
    /// None = 0
    /// </summary>
    public static class MeleeForce
    {
        internal const float UltraStrong = 3f;
        internal const float ExtremelyStrong = 2f;
        internal const float VeryStrong = 1.75f;
        internal const float Strong = 1.5f;
        internal const float AboveNormal = 1.25f;
        internal const float Normal = 1f;
        internal const float Weak = 0.5f;
        internal const float None = 0f;
    }

    /// <summary>
    /// EnergyRecharge range: 0-100
    /// <para/>
    /// 
    /// Quick = 1.5
    /// Normal = 1
    /// Slow = 0.5
    /// </summary>
    public static class EnergyRecharge
    {
        internal const float Quick = 1.5f;
        internal const float Normal = 1f;
        internal const float Slow = .5f;
    }

    /// <summary>
    /// Damage range: 0-100. Apply to all fields related to damage dealt like
    /// ProjectileDamageDealtModifier, MeleeDamageDealtModifier...
    /// <para/>
    /// 
    /// OnePunch = 10
    /// UltraHigh = 5
    /// ExtremelyHigh = 2
    /// VeryHigh = 1.5
    /// High = 1.25
    /// FairlyHigh = 1.2
    /// AboveNormal = 1.1
    /// <para/>
    /// 
    /// Normal = 1
    /// <para/>
    /// 
    /// BelowNormal = 0.9
    /// FairlyLow = 0.8
    /// Low = 0.75
    /// VeryLow = 0.5
    /// ExtremelyLow = 0.25
    /// UltraLow = 0.1
    /// None = 0
    /// </summary>
    public static class DamageDealt
    {
        internal const float OnePunch = 10f;
        internal const float UltraHigh = 5f;
        internal const float ExtremelyHigh = 2f;
        internal const float VeryHigh = 1.5f;
        internal const float High = 1.25f;
        internal const float FairlyHigh = 1.2f;
        internal const float AboveNormal = 1.1f;
        internal const float Normal = 1f;
        internal const float BelowNormal = .9f;
        internal const float FairlyLow = .8f;
        internal const float Low = .75f;
        internal const float VeryLow = .5f;
        internal const float ExtremelyLow = .25f;
        internal const float UltraLow = .1f;
        internal const float None = 0f;
    }

    /// <summary>
    /// Damage range: 0-100. Apply to all fields related to damage taken like
    /// ExplosionDamageTakenModifier, ProjectileCritChanceTakenModifier...
    /// <para/>
    /// 
    /// Defenseless = 10
    /// UltraVulnerable = 5
    /// ExtremelyVulnerable = 2
    /// VeryVulnerable = 1.5
    /// Vulnerable = 1.25
    /// FairlyVulnerable = 1.2
    /// SlightlyVulnerable = 1.1
    /// <para/>
    /// 
    /// Normal = 1
    /// <para/>
    /// 
    /// SlightlyResistant = 0.9
    /// FairlyResistant = 0.8
    /// Resistant = 0.75
    /// VeryResistant = 0.5
    /// ExtremelyResistant = 0.25
    /// UltraResistant = 0.1
    /// Unbeatable = 0
    /// </summary>
    public static class DamageTaken
    {
        internal const float Defenseless = 10f;
        internal const float UltraVulnerable = 5f;
        internal const float ExtremelyVulnerable = 2f;
        internal const float VeryVulnerable = 1.5f;
        internal const float Vulnerable = 1.25f;
        internal const float FairlyVulnerable = 1.2f;
        internal const float SlightlyVulnerable = 1.1f;
        internal const float Normal = 1f;
        internal const float SlightlyResistant = .9f;
        internal const float FairlyResistant = .8f;
        internal const float Resistant = .75f;
        internal const float VeryResistant = .5f;
        internal const float ExtremelyResistant = .25f;
        internal const float UltraResistant = .1f;
        internal const float Unbeatable = 0f;
    }

    public static class ItemDropMode
    {
        internal const int Normal = 0;
        internal const int Break = 1;
        internal const int Remove = 2;
    }
}
