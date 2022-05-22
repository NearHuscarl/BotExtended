using SFDGameScriptInterface;
using System.Linq;

namespace ChallengePlus
{
    public static class Constants
    {
        internal const float CORPSE_MAX_HEALTH = 150;

        internal const float MAX_WALK_SPEED = 2.5f;
        internal const float MAX_SPRINT_SPEED = 3.5f;
        internal const string SCRIPT_NAME = "ChallengePlus";

        internal const string STORAGE_KEY_PREFIX = "CP_";
        internal const string CURRENT_VERSION = "0.0.0";

        // normal explosion radius: bazooka rockets, grenades, mines, explosive barrels, propane tank
        internal const float ExplosionRadius = 38.5f;

        // default settings value
        internal const int DEFAULT_CHALLENGE_ROTATION_INTERVAL = 3;
        internal static readonly string[] DEFAULT_ENABLED_CHALLENGES = new string[] { "All" };

        // boolean value (1|0) for fields like InfiniteAmmo, CanBurn, MeleeStunImmunity...
        internal const int TOGGLE_ON = 1;
        internal const int TOGGLE_OFF = 0;
        internal static readonly WeaponItemType[] WeaponItemTypes = new WeaponItemType[]
        {
            WeaponItemType.Melee, WeaponItemType.Rifle, WeaponItemType.Handgun, WeaponItemType.Thrown, WeaponItemType.Powerup, WeaponItemType.InstantPickup,
        };
        internal static readonly PlayerTeam[] Teams = new PlayerTeam[] { PlayerTeam.Team1, PlayerTeam.Team2, PlayerTeam.Team3, PlayerTeam.Team4, };

        internal static readonly string[] Giblets = new string[]
        {
            "Giblet00",
            "Giblet01",
            "Giblet02",
            "Giblet03",
            "Giblet04",
        };

        internal static readonly string[] WeaponNames = new string[]
        {
            "WpnPistol",
            "WpnPistol45",
            "WpnSilencedPistol",
            "WpnMachinePistol",
            "WpnMagnum",
            "WpnRevolver",
            "WpnPumpShotgun",
            "WpnDarkShotgun",
            "WpnTommygun",
            "WpnSMG",
            "WpnM60",
            "WpnPipeWrench",
            "WpnChain",
            "WpnWhip",
            "WpnHammer",
            "WpnKatana",
            "WpnMachete",
            "WpnChainsaw",
            "WpnKnife",
            "WpnSawedoff",
            "WpnBat",
            "WpnBaton",
            "WpnShockBaton",
            "WpnLeadPipe",
            "WpnUzi",
            "WpnSilencedUzi",
            "WpnBazooka",
            "WpnAxe",
            "WpnAssaultRifle",
            "WpnMP50",
            "WpnSniperRifle",
            "WpnCarbine",
            "WpnFlamethrower",
            "ItemPills",
            "ItemMedkit",
            "ItemSlomo5",
            "ItemSlomo10",
            "ItemStrengthBoost",
            "ItemSpeedBoost",
            "ItemLaserSight",
            "ItemBouncingAmmo",
            "ItemFireAmmo",
            "WpnGrenades",
            "WpnMolotovs",
            "WpnMines",
            "WpnShuriken",
            "WpnBow",
            "WpnFlareGun",
            "WpnGrenadeLauncher",
        };

        internal static CollisionFilter NoCollision
        {
            get
            {
                return new CollisionFilter()
                {
                    AboveBits = 0,
                    CategoryBits = 0,
                    MaskBits = 0,
                    AbsorbProjectile = false,
                    BlockExplosions = false,
                    BlockFire = false,
                    BlockMelee = false,
                    ProjectileHit = false,
                };
            }
        }
    }

    public struct ScriptColors
    {
        public static readonly Color Team1 = new Color(64, 64, 128);
        public static readonly Color Team2 = new Color(128, 40, 40);
        public static readonly Color Team3 = new Color(0, 112, 0);
        public static readonly Color Team4 = new Color(112, 112, 0);

        public static readonly Color Red = new Color(128, 32, 32);
        public static readonly Color Orange = new Color(255, 128, 24);

        public static readonly Color MESSAGE_COLOR = new Color(24, 238, 200);
        public static readonly Color ERROR_COLOR = new Color(244, 77, 77);
        public static readonly Color WARNING_COLOR = new Color(249, 191, 11);
    }

    // PlayerCommandFaceDirection is too long
    public enum FaceDirection { None, Left, Right, }

    public enum ClothingType
    {
        Accesory,
        ChestOver,
        ChestUnder,
        Feet,
        Hands,
        Head,
        Legs,
        Waist,
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
    /// UltraFast = 1.75
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
        internal const float UltraFast = 1.75f;
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
        internal const float OnePunch = 5f;
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

    public static class CategoryBits
    {
        internal const ushort None = 0x0000;

        /// <summary>
        /// Static impassable objects (wall, ground, plate...)
        /// </summary>
        internal const ushort StaticGround = 0x0001;
        internal const ushort DynamicPlatform = 0x0002;
        internal const ushort Player = 0x0004;
        /// <summary>
        /// Dynamic objects that can collide with player without setting IObject.TrackAsMissle(true)
        /// Example: table, chair, couch, crate...
        /// </summary>
        internal const ushort DynamicG1 = 0x0008;
        /// <summary>
        /// Dynamic objects that cannot collide with player but can collide with other dynamic objects
        /// Set IObject.TrackAsMissle(true) to make them collide with players
        /// Example: glass, cup, bottle, weapons on map...
        /// </summary>
        internal const ushort DynamicG2 = 0x0010;
        /// <summary>
        /// Objects that are both DynamicG1 and DynamicG2.
        /// Example: The above of Table is DynamicG1 while the below is DynamicG2 
        /// </summary>
        internal const ushort Dynamic = DynamicG1 + DynamicG2;

        internal const ushort Items = 0x0020;
        internal const ushort Debris = 0x0010;
        internal const ushort DynamicsThrown = 0x8000;
    }

    public static class WpnSearchRange
    {
        public const float Infinite = 0f;
        public const float InSight = 80f;
        public const float Nearby = 20f;
    }
}
