using SFDGameScriptInterface;

namespace SFDScript.BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        public static BotBehaviorSet GetBehaviorSet(BotAI botAI, SearchItems searchItems = SearchItems.None)
        {
            var botBehaviorSet = new BotBehaviorSet()
            {
                MeleeActions = BotMeleeActions.Default,
                MeleeActionsWhenHit = BotMeleeActions.DefaultWhenHit,
                MeleeActionsWhenEnraged = BotMeleeActions.DefaultWhenEnraged,
                MeleeActionsWhenEnragedAndHit = BotMeleeActions.DefaultWhenEnragedAndHit,
                ChaseRange = 44f,
                GuardRange = 40f,
            };

            switch (botAI)
            {
                #region Debug
                case BotAI.Debug:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotD);
                    botBehaviorSet.RangedWeaponBurstTimeMin = 5000;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 5000;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 0;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 0;
                    break;
                }
                #endregion

                #region OffensiveMelee
                case BotAI.OffensiveMelee:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.MeleeB);
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.9f;
                    botBehaviorSet.MeleeWaitTimeLimitMin = 600f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 800f;

                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.1f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.95f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.9f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.9f;
                    botBehaviorSet.OffensiveSprintLevel = 0.9f;
                    botBehaviorSet.OffensiveDiveLevel = 0.1f; // 0.7f
                    break;
                }
                #endregion

                #region Easy
                case BotAI.Easy:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotD);
                    break;
                }
                #endregion

                #region Normal
                case BotAI.Normal:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotC);
                    break;
                }
                #endregion

                #region Hard
                case BotAI.Hard:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotB);
                    break;
                }
                #endregion

                #region Expert
                case BotAI.Expert:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotA);
                    break;
                }
                #endregion

                #region Hacker
                case BotAI.Hacker:
                {
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.OffensiveEnrageLevel = 0.8f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.1f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.95f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.7f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.7f;
                    botBehaviorSet.OffensiveSprintLevel = 0.6f;
                    botBehaviorSet.OffensiveDiveLevel = 0.6f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.9f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = 1;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.MeleeWaitTimeLimitMin = 100f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 200f;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.SetMeleeActionsToExpert();
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.RangedWeaponAccuracy = 0.85f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 50f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 200f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 50f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 50f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 800f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.95f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 25f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMax = 50f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 800f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 1600f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 100f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 200f;
                    break;
                }
                #endregion

                #region MeleeExpert
                case BotAI.MeleeExpert:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.MeleeB);
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.9f;
                    botBehaviorSet.MeleeWaitTimeLimitMin = 600f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 800f;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = true;
                    break;
                }
                #endregion

                #region MeleeHard
                case BotAI.MeleeHard:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.MeleeB);
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.75f;
                    botBehaviorSet.MeleeWaitTimeLimitMin = 800f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 1000f;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    break;
                }
                #endregion

                #region Ninja == BotAI.MeleeExpert + more offensive melee tactic
                case BotAI.Ninja:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.OffensiveMelee);

                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = true;

                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.SearchItems = SearchItems.Melee;
                    break;
                }
                #endregion

                #region RangeExpert
                case BotAI.RangeExpert:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.RangedA);
                    botBehaviorSet.RangedWeaponAccuracy = 0.85f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.95f;
                    break;
                }
                #endregion

                #region RangeHard
                case BotAI.RangeHard:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.RangedA);
                    botBehaviorSet.RangedWeaponAccuracy = 0.75f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.9f;
                    break;
                }
                #endregion

                #region Sniper == BotAI.RangeExpert + more defensive melee tactic
                case BotAI.Sniper:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.RangedA);
                    botBehaviorSet.RangedWeaponMode = BotBehaviorRangedWeaponMode.ManualAim;
                    botBehaviorSet.RangedWeaponAccuracy = 0.85f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.95f;

                    botBehaviorSet.DefensiveRollFireLevel = 0.95f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.6f;
                    botBehaviorSet.OffensiveEnrageLevel = 0.2f;
                    botBehaviorSet.OffensiveClimbingLevel = 0f;
                    botBehaviorSet.OffensiveSprintLevel = 0f;
                    botBehaviorSet.OffensiveDiveLevel = 0f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0f;
                    botBehaviorSet.TeamLineUp = false;
                    break;
                }
                #endregion

                #region Grunt
                case BotAI.Grunt:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.Grunt);

                    // Taken from PredefinedAIType.BotB, PredefinedAIType.Grunt is too slow in shooting
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 200f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 600f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 200f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 600f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 800f;
                    break;
                }
                #endregion

                #region Hulk
                case BotAI.Hulk:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.Hulk);
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)10,
                        AttackCombo = (ushort)20,
                        Block = (ushort)1,
                        Kick = (ushort)1,
                        Jump = (ushort)1,
                        Wait = (ushort)20, // Hulk's original is 50
                        Grab = (ushort)6
                    });
                    break;
                }
                #endregion

                #region RagingHulk
                case BotAI.RagingHulk:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.OffensiveMelee);
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)4,
                        AttackCombo = (ushort)20,
                        Block = (ushort)1,
                        Kick = (ushort)4,
                        Jump = (ushort)1,
                        Wait = (ushort)10,
                        Grab = (ushort)8
                    });
                    break;
                }
                #endregion

                #region Meatgrinder
                case BotAI.Meatgrinder:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.Meatgrinder);
                    break;
                }
                #endregion

                #region ZombieSlow
                case BotAI.ZombieSlow:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.ZombieA);
                    break;
                }
                #endregion

                #region ZombieFast
                case BotAI.ZombieFast:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.ZombieB);
                    break;
                }
                #endregion

                #region ZombieHulk
                case BotAI.ZombieHulk:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.Hulk);
                    botBehaviorSet.AttackDeadEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.PowerupUsage = false;
                    botBehaviorSet.ChokePointValue = 32f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = 5;
                    botBehaviorSet.DefensiveRollFireLevel = 0.1f;
                    botBehaviorSet.OffensiveDiveLevel = 0f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0f;
                    break;
                }
                #endregion

                #region ZombieFighter
                case BotAI.ZombieFighter:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.MeleeB);
                    botBehaviorSet.AttackDeadEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.PowerupUsage = false;
                    botBehaviorSet.ChokePointValue = 32f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = 5;
                    botBehaviorSet.DefensiveRollFireLevel = 0.1f;
                    botBehaviorSet.OffensiveDiveLevel = 0f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0f;
                    break;
                }
                #endregion

                #region default
                default:
                {
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.None;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.None;
                    botBehaviorSet.EliminateEnemies = false;
                    botBehaviorSet.SearchForItems = false;
                    break;
                }
                #endregion
            }

            botBehaviorSet.SearchForItems = true;
            botBehaviorSet.SearchItems = searchItems; // Disable SearchItems by setting to None

            return botBehaviorSet;
        }
    }
}
