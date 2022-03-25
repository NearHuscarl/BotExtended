using SFDGameScriptInterface;

namespace BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        public static BotMeleeActions HulkMeleeActions
        {
            get
            {
                return new BotMeleeActions()
                {
                    Attack = (ushort)10,
                    AttackCombo = (ushort)20,
                    Block = (ushort)1,
                    Kick = (ushort)1,
                    Jump = (ushort)1,
                    Wait = (ushort)10, // Hulk's original is 50
                    Grab = (ushort)6
                };
            }
        }

        public static BotBehaviorSet GetBehaviorSet(BotAI botAI)
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
                    botBehaviorSet = Rage(botBehaviorSet);
                    botBehaviorSet = VeryDefensive(botBehaviorSet);

                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.SearchItems = SearchItems.Melee | SearchItems.Throwable;

                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.OffensiveDiveLevel = 0.1f;
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

                #region Sniper == BotAI.RangeExpert + Defensive
                case BotAI.Hunter:
                case BotAI.Sniper:
                {
                    botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.RangedA);
                    botBehaviorSet.RangedWeaponMode = BotBehaviorRangedWeaponMode.ManualAim;
                    botBehaviorSet.RangedWeaponAccuracy = 0.85f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.95f;
                    if (botAI == BotAI.Hunter)
                        botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.35f;

                    botBehaviorSet = VeryDefensive(botBehaviorSet);
                    botBehaviorSet = VeryInoffensive(botBehaviorSet);
                    botBehaviorSet.TeamLineUp = false;
                    break;
                }
                #endregion

                #region Spacer - Slow shooter
                case BotAI.Spacer:
                case BotAI.SpacerExpert:
                {
                    if (botAI == BotAI.SpacerExpert)
                        botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.BotA);
                    else
                        botBehaviorSet = BotBehaviorSet.GetBotBehaviorPredefinedSet(PredefinedAIType.RangedA);
                    botBehaviorSet.RangedWeaponMode = BotBehaviorRangedWeaponMode.ManualAim;
                    botBehaviorSet.RangedWeaponAccuracy = 0.85f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 1000f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 3000f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.95f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 1500f;
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
                    botBehaviorSet.SetMeleeActionsAll(HulkMeleeActions);
                    break;
                }
                #endregion

                #region RagingHulk
                case BotAI.RagingHulk:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.Hulk);
                    botBehaviorSet = Rage(botBehaviorSet);
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)4,
                        AttackCombo = (ushort)20,
                        Block = (ushort)1,
                        Kick = (ushort)4,
                        Jump = (ushort)1,
                        Wait = (ushort)0,
                        Grab = (ushort)16,
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

                #region Assassin
                case BotAI.AssassinMelee:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.MeleeHard);
                    botBehaviorSet = Jogger(botBehaviorSet);
                    break;
                }
                case BotAI.AssassinRange:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.RangeHard);
                    botBehaviorSet = Jogger(botBehaviorSet);
                    break;
                }
                #endregion

                #region BabyBear
                case BotAI.Babybear:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.Easy);
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveClimbingLevel = 0.9f;
                    botBehaviorSet.OffensiveSprintLevel = 0.85f;
                    botBehaviorSet.GuardRange = 16;
                    botBehaviorSet.ChaseRange = 16;
                    break;
                }
                #endregion

                #region Cowboy
                case BotAI.Cowboy:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.Grunt);
                    botBehaviorSet = TriggerHappy(botBehaviorSet);
                    break;
                }
                #endregion

                #region Kingpin
                case BotAI.Kingpin:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.Hard);
                    botBehaviorSet.OffensiveSprintLevel = 0.8f;
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)16,
                        AttackCombo = (ushort)24,
                        Block = (ushort)1,
                        Kick = (ushort)1,
                        Jump = (ushort)1,
                        Wait = (ushort)10, // Hulk's original is 50
                        Grab = (ushort)8
                    });
                    break;
                }
                #endregion

                #region Sheriff
                case BotAI.Sheriff:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.Hard);
                    botBehaviorSet = TriggerHappy(botBehaviorSet);
                    break;
                }
                #endregion

                #region Soldier
                case BotAI.Soldier:
                {
                    botBehaviorSet = GetBehaviorSet(BotAI.Hard);
                    botBehaviorSet = Defensive(botBehaviorSet);
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
                    botBehaviorSet = GetBehaviorSet(BotAI.MeleeHard);
                    botBehaviorSet.AttackDeadEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.PowerupUsage = false;
                    botBehaviorSet.ChokePointValue = 32f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = 5;
                    botBehaviorSet.DefensiveRollFireLevel = 0.1f;
                    botBehaviorSet.OffensiveDiveLevel = 0f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0f;
                    botBehaviorSet.RocketRideProficiency = .8f;
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
            botBehaviorSet.SearchItems = SearchItems.None; // Disable SearchItems by setting to None

            return botBehaviorSet;
        }

        private static BotBehaviorSet Offensive(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.75f;

            botBehaviorSet.OffensiveEnrageLevel = 0.6f;
            botBehaviorSet.OffensiveClimbingLevel = 0.7f;
            botBehaviorSet.OffensiveSprintLevel = 0.7f;
            botBehaviorSet.OffensiveDiveLevel = 0.7f;

            return botBehaviorSet;
        }

        private static BotBehaviorSet VeryOffensive(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.9f;

            botBehaviorSet.OffensiveEnrageLevel = 0.7f;
            botBehaviorSet.OffensiveClimbingLevel = 0.9f;
            botBehaviorSet.OffensiveSprintLevel = 0.9f;
            botBehaviorSet.OffensiveDiveLevel = 0.8f;

            return botBehaviorSet;
        }

        private static BotBehaviorSet Rage(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet = VeryOffensive(botBehaviorSet);
            botBehaviorSet.MeleeWaitTimeLimitMin = 200f;
            botBehaviorSet.MeleeWaitTimeLimitMax = 400f;
            botBehaviorSet.TeamLineUp = false;
            botBehaviorSet.RangedWeaponLOSIgnoreTeammates = true;

            botBehaviorSet.OffensiveEnrageLevel = 0.8f;
            botBehaviorSet.NavigationRandomPausesLevel = 0.1f;

            return botBehaviorSet;
        }

        private static BotBehaviorSet VeryInoffensive(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet.OffensiveEnrageLevel = 0.2f;
            botBehaviorSet.OffensiveClimbingLevel = 0f;
            botBehaviorSet.OffensiveSprintLevel = 0f;
            botBehaviorSet.OffensiveDiveLevel = 0f;
            botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0f;

            return botBehaviorSet;
        }

        private static BotBehaviorSet Defensive(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet.DefensiveBlockLevel = 0f; // NOT YET IMPLEMENTED
            botBehaviorSet.DefensiveAvoidProjectilesLevel = .8f; // expert ref: .4f
            botBehaviorSet.DefensiveRollFireLevel = .85f; // .9f
            botBehaviorSet.SeekCoverWhileShooting = .85f; // .85f

            return botBehaviorSet;
        }

        private static BotBehaviorSet VeryDefensive(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet.DefensiveBlockLevel = 0f; // NOT YET IMPLEMENTED
            botBehaviorSet.DefensiveAvoidProjectilesLevel = .95f;
            botBehaviorSet.DefensiveRollFireLevel = .95f;
            botBehaviorSet.SeekCoverWhileShooting = .99f;

            return botBehaviorSet;
        }

        private static BotBehaviorSet Jogger(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet.OffensiveClimbingLevel = 0.9f;
            botBehaviorSet.OffensiveSprintLevel = 0.9f;

            return botBehaviorSet;
        }

        private static BotBehaviorSet TriggerHappy(BotBehaviorSet botBehaviorSet)
        {
            botBehaviorSet.RangedWeaponAimShootDelayMin = 0;
            botBehaviorSet.RangedWeaponAimShootDelayMax = 50;
            botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 0;
            botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 25;
            botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 50;

            return botBehaviorSet;
        }
    }
}
