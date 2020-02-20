// Decompiled with JetBrains decompiler
// Type: SFDGameScriptInterface.BotBehaviorSet

using System;

namespace SFDGameScriptInterface
{
    [Serializable]
    public class BotBehaviorSet : ICloneable, IEquatable<BotBehaviorSet>
    {
        private float m_meleeWaitTimeLimitMin = 1000f;
        private float m_meleeWaitTimeLimitMax = 1000f;
        private BotBehaviorNavigationMode m_navigationMode;
        private BotBehaviorMeleeMode m_meleeMode;
        private bool m_eliminateEnemies;
        private bool m_attackDeadEnemies;
        private bool m_searchForItems;
        private SearchItems m_searchItems;
        private float m_aggroRange;
        private float m_searchItemRange;
        private float m_guardRange;
        private float m_chaseRange;
        private bool m_teamLineUp;
        private ushort m_chokePointPlayerCountThreshold;
        private float m_chokePointValue;
        private float m_offensiveDiveLevel;
        private float m_offensiveEnrageLevel;
        private float m_offensiveClimbingLevel;
        private float m_counterOutOfRangeMeleeAttacksLevel;
        private float m_defensiveBlockLevel;
        private float m_defensiveAvoidProjectilesLevel;
        private float m_defensiveRollFireLevel;
        private float m_navigationRandomPausesLevel;
        private float m_offensiveSprintLevel;
        private bool m_powerupUsage;
        private bool m_meleeUsage;
        private bool m_meleeWeaponUsage;
        private bool m_meleeWeaponUseFullRange;
        public BotMeleeActions MeleeActions;
        public BotMeleeActions MeleeActionsWhenHit;
        public BotMeleeActions MeleeActionsWhenEnraged;
        public BotMeleeActions MeleeActionsWhenEnragedAndHit;
        private bool m_rangedWeaponLOSIgnoreTeammates;
        private bool m_rangedWeaponUsage;
        private BotBehaviorRangedWeaponMode m_rangedWeaponMode;
        private float m_rangedWeaponAccuracy;
        private float m_rangedWeaponHipFireAimShootDelayMin;
        private float m_rangedWeaponHipFireAimShootDelayMax;
        private float m_rangedWeaponAimShootDelayMin;
        private float m_rangedWeaponAimShootDelayMax;
        private float m_rangedWeaponBurstTimeMin;
        private float m_rangedWeaponBurstTimeMax;
        private float m_rangedWeaponBurstPauseMin;
        private float m_rangedWeaponBurstPauseMax;
        private float m_seekCoverWhileShooting;
        private float m_rangedWeaponPrecisionInterpolateTime;
        private float m_rangedWeaponPrecisionAccuracy;
        private float m_rangedWeaponPrecisionAimShootDelayMin;
        private float m_rangedWeaponPrecisionAimShootDelayMax;
        private float m_rangedWeaponPrecisionBurstTimeMin;
        private float m_rangedWeaponPrecisionBurstTimeMax;
        private float m_rangedWeaponPrecisionBurstPauseMin;
        private float m_rangedWeaponPrecisionBurstPauseMax;
        private float m_rocketRideProficiency;

        public BotBehaviorNavigationMode NavigationMode
        {
            get
            {
                return this.m_navigationMode;
            }
            set
            {
                this.m_navigationMode = value;
            }
        }

        public BotBehaviorMeleeMode MeleeMode
        {
            get
            {
                return this.m_meleeMode;
            }
            set
            {
                this.m_meleeMode = value;
            }
        }

        public bool EliminateEnemies
        {
            get
            {
                return this.m_eliminateEnemies;
            }
            set
            {
                this.m_eliminateEnemies = value;
            }
        }

        public bool AttackDeadEnemies
        {
            get
            {
                return this.m_attackDeadEnemies;
            }
            set
            {
                this.m_attackDeadEnemies = value;
            }
        }

        public bool SearchForItems
        {
            get
            {
                return this.m_searchForItems;
            }
            set
            {
                this.m_searchForItems = value;
            }
        }

        public SearchItems SearchItems
        {
            get
            {
                return this.m_searchItems;
            }
            set
            {
                this.m_searchItems = value;
            }
        }

        public float AggroRange
        {
            get
            {
                return this.m_aggroRange;
            }
            set
            {
                this.m_aggroRange = value;
            }
        }

        public float SearchItemRange
        {
            get
            {
                return this.m_searchItemRange;
            }
            set
            {
                this.m_searchItemRange = value;
            }
        }

        public float GuardRange
        {
            get
            {
                return this.m_guardRange;
            }
            set
            {
                this.m_guardRange = value;
                this.m_chaseRange = Math.Max(value, this.m_chaseRange);
            }
        }

        public float ChaseRange
        {
            get
            {
                return this.m_chaseRange;
            }
            set
            {
                this.m_chaseRange = value;
                this.m_guardRange = Math.Min(value, this.m_guardRange);
            }
        }

        public bool TeamLineUp
        {
            get
            {
                return this.m_teamLineUp;
            }
            set
            {
                this.m_teamLineUp = value;
            }
        }

        public ushort ChokePointPlayerCountThreshold
        {
            get
            {
                return this.m_chokePointPlayerCountThreshold;
            }
            set
            {
                this.m_chokePointPlayerCountThreshold = value;
            }
        }

        public float ChokePointValue
        {
            get
            {
                return this.m_chokePointValue;
            }
            set
            {
                this.m_chokePointValue = value;
            }
        }

        public float OffensiveDiveLevel
        {
            get
            {
                return this.m_offensiveDiveLevel;
            }
            set
            {
                this.m_offensiveDiveLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float OffensiveEnrageLevel
        {
            get
            {
                return this.m_offensiveEnrageLevel;
            }
            set
            {
                this.m_offensiveEnrageLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float OffensiveClimbingLevel
        {
            get
            {
                return this.m_offensiveClimbingLevel;
            }
            set
            {
                this.m_offensiveClimbingLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float CounterOutOfRangeMeleeAttacksLevel
        {
            get
            {
                return this.m_counterOutOfRangeMeleeAttacksLevel;
            }
            set
            {
                this.m_counterOutOfRangeMeleeAttacksLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float DefensiveBlockLevel
        {
            get
            {
                return this.m_defensiveBlockLevel;
            }
            set
            {
                this.m_defensiveBlockLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float DefensiveAvoidProjectilesLevel
        {
            get
            {
                return this.m_defensiveAvoidProjectilesLevel;
            }
            set
            {
                this.m_defensiveAvoidProjectilesLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float DefensiveRollFireLevel
        {
            get
            {
                return this.m_defensiveRollFireLevel;
            }
            set
            {
                this.m_defensiveRollFireLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float NavigationRandomPausesLevel
        {
            get
            {
                return this.m_navigationRandomPausesLevel;
            }
            set
            {
                this.m_navigationRandomPausesLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float OffensiveSprintLevel
        {
            get
            {
                return this.m_offensiveSprintLevel;
            }
            set
            {
                this.m_offensiveSprintLevel = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public bool PowerupUsage
        {
            get
            {
                return this.m_powerupUsage;
            }
            set
            {
                this.m_powerupUsage = value;
            }
        }

        public bool MeleeUsage
        {
            get
            {
                return this.m_meleeUsage;
            }
            set
            {
                this.m_meleeUsage = value;
            }
        }

        public bool MeleeWeaponUsage
        {
            get
            {
                return this.m_meleeWeaponUsage;
            }
            set
            {
                this.m_meleeWeaponUsage = value;
            }
        }

        public bool MeleeWeaponUseFullRange
        {
            get
            {
                return this.m_meleeWeaponUseFullRange;
            }
            set
            {
                this.m_meleeWeaponUseFullRange = value;
            }
        }

        public void SetMeleeActionsAll(BotMeleeActions value)
        {
            this.MeleeActions = value;
            this.MeleeActionsWhenHit = value;
            this.MeleeActionsWhenEnraged = value;
            this.MeleeActionsWhenEnragedAndHit = value;
        }

        public void SetMeleeActionsToEasy()
        {
            this.MeleeActions = BotMeleeActions.Easy;
            this.MeleeActionsWhenHit = BotMeleeActions.EasyWhenHit;
            this.MeleeActionsWhenEnraged = BotMeleeActions.EasyWhenEnraged;
            this.MeleeActionsWhenEnragedAndHit = BotMeleeActions.EasyWhenEnragedAndHit;
        }

        public void SetMeleeActionsToNormal()
        {
            this.MeleeActions = BotMeleeActions.Normal;
            this.MeleeActionsWhenHit = BotMeleeActions.NormalWhenHit;
            this.MeleeActionsWhenEnraged = BotMeleeActions.NormalWhenEnraged;
            this.MeleeActionsWhenEnragedAndHit = BotMeleeActions.NormalWhenEnragedAndHit;
        }

        public void SetMeleeActionsToHard()
        {
            this.MeleeActions = BotMeleeActions.Hard;
            this.MeleeActionsWhenHit = BotMeleeActions.HardWhenHit;
            this.MeleeActionsWhenEnraged = BotMeleeActions.HardWhenEnraged;
            this.MeleeActionsWhenEnragedAndHit = BotMeleeActions.HardWhenEnragedAndHit;
        }

        public void SetMeleeActionsToExpert()
        {
            this.MeleeActions = BotMeleeActions.Expert;
            this.MeleeActionsWhenHit = BotMeleeActions.ExpertWhenHit;
            this.MeleeActionsWhenEnraged = BotMeleeActions.ExpertWhenEnraged;
            this.MeleeActionsWhenEnragedAndHit = BotMeleeActions.ExpertWhenEnragedAndHit;
        }

        public void SetMeleeActionsToDefault()
        {
            this.MeleeActions = BotMeleeActions.Default;
            this.MeleeActionsWhenHit = BotMeleeActions.DefaultWhenHit;
            this.MeleeActionsWhenEnraged = BotMeleeActions.DefaultWhenEnraged;
            this.MeleeActionsWhenEnragedAndHit = BotMeleeActions.DefaultWhenEnragedAndHit;
        }

        public void MutateMeleeActions(float value, Random random)
        {
            this.MutateMeleeActions(ref this.MeleeActions, value, random);
            this.MutateMeleeActions(ref this.MeleeActionsWhenHit, value, random);
            this.MutateMeleeActions(ref this.MeleeActionsWhenEnraged, value, random);
            this.MutateMeleeActions(ref this.MeleeActionsWhenEnragedAndHit, value, random);
        }

        public void MutateMeleeActions(ref BotMeleeActions meleeActions, float value, Random random)
        {
            if ((double)value <= 0.0 || random == null)
                return;
            if ((double)value > 1.0)
                value = 1f;
            BotMeleeActions botMeleeActions = meleeActions;
            ushort minRegisteredValue = botMeleeActions.GetMinRegisteredValue(false);
            if ((double)minRegisteredValue < 100.0)
            {
                ushort maxRegisteredValue = botMeleeActions.GetMaxRegisteredValue();
                if (minRegisteredValue <= (ushort)10 && maxRegisteredValue < (ushort)655)
                    botMeleeActions = botMeleeActions.Multiply(100f);
                else if (minRegisteredValue < (ushort)100 && maxRegisteredValue < (ushort)6553)
                    botMeleeActions = botMeleeActions.Multiply(10f);
            }
            Func<float> func = (Func<float>)(() => (float)(1.0 + ((double)value * random.NextDouble() * 2.0 - (double)value)));
            botMeleeActions.Attack = BotMeleeActions.Multiply(botMeleeActions.Attack, func());
            botMeleeActions.AttackCombo = BotMeleeActions.Multiply(botMeleeActions.AttackCombo, func());
            botMeleeActions.Block = BotMeleeActions.Multiply(botMeleeActions.Block, func());
            botMeleeActions.Kick = BotMeleeActions.Multiply(botMeleeActions.Kick, func());
            botMeleeActions.Jump = BotMeleeActions.Multiply(botMeleeActions.Jump, func());
            botMeleeActions.Wait = BotMeleeActions.Multiply(botMeleeActions.Wait, func());
            botMeleeActions.Grab = BotMeleeActions.Multiply(botMeleeActions.Grab, func());
            meleeActions = botMeleeActions;
        }

        public float MeleeWaitTimeLimitMin
        {
            get
            {
                return this.m_meleeWaitTimeLimitMin;
            }
            set
            {
                this.m_meleeWaitTimeLimitMin = Math.Min(Math.Max(0.0f, value), 5000f);
                this.m_meleeWaitTimeLimitMax = Math.Max(this.m_meleeWaitTimeLimitMin, this.m_meleeWaitTimeLimitMax);
            }
        }

        public float MeleeWaitTimeLimitMax
        {
            get
            {
                return this.m_meleeWaitTimeLimitMax;
            }
            set
            {
                this.m_meleeWaitTimeLimitMax = Math.Min(Math.Max(0.0f, value), 5000f);
                this.m_meleeWaitTimeLimitMin = Math.Min(this.m_meleeWaitTimeLimitMin, this.m_meleeWaitTimeLimitMax);
            }
        }

        public bool RangedWeaponLOSIgnoreTeammates
        {
            get
            {
                return this.m_rangedWeaponLOSIgnoreTeammates;
            }
            set
            {
                this.m_rangedWeaponLOSIgnoreTeammates = value;
            }
        }

        public bool RangedWeaponUsage
        {
            get
            {
                return this.m_rangedWeaponUsage;
            }
            set
            {
                this.m_rangedWeaponUsage = value;
            }
        }

        public BotBehaviorRangedWeaponMode RangedWeaponMode
        {
            get
            {
                return this.m_rangedWeaponMode;
            }
            set
            {
                this.m_rangedWeaponMode = value;
            }
        }

        public float RangedWeaponAccuracy
        {
            get
            {
                return this.m_rangedWeaponAccuracy;
            }
            set
            {
                this.m_rangedWeaponAccuracy = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float RangedWeaponHipFireAimShootDelayMin
        {
            get
            {
                return this.m_rangedWeaponHipFireAimShootDelayMin;
            }
            set
            {
                this.m_rangedWeaponHipFireAimShootDelayMin = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponHipFireAimShootDelayMax >= (double)this.m_rangedWeaponHipFireAimShootDelayMin)
                    return;
                this.m_rangedWeaponHipFireAimShootDelayMax = this.m_rangedWeaponHipFireAimShootDelayMin;
            }
        }

        public float RangedWeaponHipFireAimShootDelayMax
        {
            get
            {
                return this.m_rangedWeaponHipFireAimShootDelayMax;
            }
            set
            {
                this.m_rangedWeaponHipFireAimShootDelayMax = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponHipFireAimShootDelayMin <= (double)this.m_rangedWeaponHipFireAimShootDelayMax)
                    return;
                this.m_rangedWeaponHipFireAimShootDelayMin = this.m_rangedWeaponHipFireAimShootDelayMax;
            }
        }

        public float RangedWeaponAimShootDelayMin
        {
            get
            {
                return this.m_rangedWeaponAimShootDelayMin;
            }
            set
            {
                this.m_rangedWeaponAimShootDelayMin = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponAimShootDelayMax >= (double)this.m_rangedWeaponAimShootDelayMin)
                    return;
                this.m_rangedWeaponAimShootDelayMax = this.m_rangedWeaponAimShootDelayMin;
            }
        }

        public float RangedWeaponAimShootDelayMax
        {
            get
            {
                return this.m_rangedWeaponAimShootDelayMax;
            }
            set
            {
                this.m_rangedWeaponAimShootDelayMax = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponAimShootDelayMin <= (double)this.m_rangedWeaponAimShootDelayMax)
                    return;
                this.m_rangedWeaponAimShootDelayMin = this.m_rangedWeaponAimShootDelayMax;
            }
        }

        public float RangedWeaponBurstTimeMin
        {
            get
            {
                return this.m_rangedWeaponBurstTimeMin;
            }
            set
            {
                this.m_rangedWeaponBurstTimeMin = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponBurstTimeMax >= (double)this.m_rangedWeaponBurstTimeMin)
                    return;
                this.m_rangedWeaponBurstTimeMax = this.m_rangedWeaponBurstTimeMin;
            }
        }

        public float RangedWeaponBurstTimeMax
        {
            get
            {
                return this.m_rangedWeaponBurstTimeMax;
            }
            set
            {
                this.m_rangedWeaponBurstTimeMax = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponBurstTimeMin <= (double)this.m_rangedWeaponBurstTimeMax)
                    return;
                this.m_rangedWeaponBurstTimeMin = this.m_rangedWeaponBurstTimeMax;
            }
        }

        public float RangedWeaponBurstPauseMin
        {
            get
            {
                return this.m_rangedWeaponBurstPauseMin;
            }
            set
            {
                this.m_rangedWeaponBurstPauseMin = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponBurstPauseMax >= (double)this.m_rangedWeaponBurstPauseMin)
                    return;
                this.m_rangedWeaponBurstPauseMax = this.m_rangedWeaponBurstPauseMin;
            }
        }

        public float RangedWeaponBurstPauseMax
        {
            get
            {
                return this.m_rangedWeaponBurstPauseMax;
            }
            set
            {
                this.m_rangedWeaponBurstPauseMax = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponBurstPauseMin <= (double)this.m_rangedWeaponBurstPauseMax)
                    return;
                this.m_rangedWeaponBurstPauseMin = this.m_rangedWeaponBurstPauseMax;
            }
        }

        public float SeekCoverWhileShooting
        {
            get
            {
                return this.m_seekCoverWhileShooting;
            }
            set
            {
                this.m_seekCoverWhileShooting = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float RangedWeaponPrecisionInterpolateTime
        {
            get
            {
                return this.m_rangedWeaponPrecisionInterpolateTime;
            }
            set
            {
                this.m_rangedWeaponPrecisionInterpolateTime = Math.Min(Math.Max(value, 0.0f), 10000f);
            }
        }

        public float RangedWeaponPrecisionAccuracy
        {
            get
            {
                return this.m_rangedWeaponPrecisionAccuracy;
            }
            set
            {
                this.m_rangedWeaponPrecisionAccuracy = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public float RangedWeaponPrecisionAimShootDelayMin
        {
            get
            {
                return this.m_rangedWeaponPrecisionAimShootDelayMin;
            }
            set
            {
                this.m_rangedWeaponPrecisionAimShootDelayMin = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponPrecisionAimShootDelayMax >= (double)this.m_rangedWeaponPrecisionAimShootDelayMin)
                    return;
                this.m_rangedWeaponPrecisionAimShootDelayMax = this.m_rangedWeaponPrecisionAimShootDelayMin;
            }
        }

        public float RangedWeaponPrecisionAimShootDelayMax
        {
            get
            {
                return this.m_rangedWeaponPrecisionAimShootDelayMax;
            }
            set
            {
                this.m_rangedWeaponPrecisionAimShootDelayMax = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponPrecisionAimShootDelayMin <= (double)this.m_rangedWeaponPrecisionAimShootDelayMax)
                    return;
                this.m_rangedWeaponPrecisionAimShootDelayMin = this.m_rangedWeaponPrecisionAimShootDelayMax;
            }
        }

        public float RangedWeaponPrecisionBurstTimeMin
        {
            get
            {
                return this.m_rangedWeaponPrecisionBurstTimeMin;
            }
            set
            {
                this.m_rangedWeaponPrecisionBurstTimeMin = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponPrecisionBurstTimeMax >= (double)this.m_rangedWeaponPrecisionBurstTimeMin)
                    return;
                this.m_rangedWeaponPrecisionBurstTimeMax = this.m_rangedWeaponPrecisionBurstTimeMin;
            }
        }

        public float RangedWeaponPrecisionBurstTimeMax
        {
            get
            {
                return this.m_rangedWeaponPrecisionBurstTimeMax;
            }
            set
            {
                this.m_rangedWeaponPrecisionBurstTimeMax = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponPrecisionBurstTimeMin <= (double)this.m_rangedWeaponPrecisionBurstTimeMax)
                    return;
                this.m_rangedWeaponPrecisionBurstTimeMin = this.m_rangedWeaponPrecisionBurstTimeMax;
            }
        }

        public float RangedWeaponPrecisionBurstPauseMin
        {
            get
            {
                return this.m_rangedWeaponPrecisionBurstPauseMin;
            }
            set
            {
                this.m_rangedWeaponPrecisionBurstPauseMin = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponPrecisionBurstPauseMax >= (double)this.m_rangedWeaponPrecisionBurstPauseMin)
                    return;
                this.m_rangedWeaponPrecisionBurstPauseMax = this.m_rangedWeaponPrecisionBurstPauseMin;
            }
        }

        public float RangedWeaponPrecisionBurstPauseMax
        {
            get
            {
                return this.m_rangedWeaponPrecisionBurstPauseMax;
            }
            set
            {
                this.m_rangedWeaponPrecisionBurstPauseMax = Math.Min(5000f, Math.Max(value, 0.0f));
                if ((double)this.m_rangedWeaponPrecisionBurstPauseMin <= (double)this.m_rangedWeaponPrecisionBurstPauseMax)
                    return;
                this.m_rangedWeaponPrecisionBurstPauseMin = this.m_rangedWeaponPrecisionBurstPauseMax;
            }
        }

        public float RocketRideProficiency
        {
            get
            {
                return this.m_rocketRideProficiency;
            }
            set
            {
                this.m_rocketRideProficiency = Math.Max(0.0f, Math.Min(1f, value));
            }
        }

        public BotBehaviorSet()
        {
            this.NavigationMode = BotBehaviorNavigationMode.PathFinding;
            this.MeleeMode = BotBehaviorMeleeMode.Default;
            this.EliminateEnemies = true;
            this.AttackDeadEnemies = false;
            this.SearchForItems = true;
            this.SearchItems = SearchItems.All;
            this.TeamLineUp = false;
            this.AggroRange = 0.0f;
            this.SearchItemRange = 250f;
            this.GuardRange = 80f;
            this.ChaseRange = 100f;
            this.OffensiveDiveLevel = 0.0f;
            this.OffensiveEnrageLevel = 0.0f;
            this.OffensiveClimbingLevel = 0.0f;
            this.OffensiveSprintLevel = 0.0f;
            this.DefensiveBlockLevel = 0.0f;
            this.DefensiveAvoidProjectilesLevel = 0.0f;
            this.DefensiveRollFireLevel = 0.0f;
            this.CounterOutOfRangeMeleeAttacksLevel = 0.0f;
            this.NavigationRandomPausesLevel = 0.0f;
            this.ChokePointValue = 150f;
            this.ChokePointPlayerCountThreshold = (ushort)1;
            this.MeleeUsage = true;
            this.MeleeWeaponUsage = true;
            this.MeleeWeaponUseFullRange = true;
            this.RangedWeaponUsage = true;
            this.RangedWeaponMode = BotBehaviorRangedWeaponMode.Both;
            this.MeleeActions = BotMeleeActions.Default;
            this.MeleeActionsWhenHit = BotMeleeActions.DefaultWhenHit;
            this.MeleeActionsWhenEnraged = BotMeleeActions.DefaultWhenEnraged;
            this.MeleeActionsWhenEnragedAndHit = BotMeleeActions.DefaultWhenEnragedAndHit;
            this.PowerupUsage = true;
            this.SeekCoverWhileShooting = 0.0f;
            this.RangedWeaponLOSIgnoreTeammates = false;
            this.RangedWeaponAccuracy = 0.7f;
            this.RangedWeaponAimShootDelayMin = 200f;
            this.RangedWeaponHipFireAimShootDelayMin = 200f;
            this.RangedWeaponBurstTimeMin = 400f;
            this.RangedWeaponBurstTimeMax = 800f;
            this.RangedWeaponBurstPauseMin = 400f;
            this.RangedWeaponBurstPauseMax = 800f;
            this.RangedWeaponPrecisionInterpolateTime = 4000f;
            this.RangedWeaponPrecisionAccuracy = 0.9f;
            this.RangedWeaponPrecisionAimShootDelayMin = 50f;
            this.RangedWeaponPrecisionBurstTimeMin = 1000f;
            this.RangedWeaponPrecisionBurstTimeMax = 2000f;
            this.RangedWeaponPrecisionBurstPauseMin = 200f;
            this.RangedWeaponPrecisionBurstPauseMax = 400f;
            this.RocketRideProficiency = 0.0f;
        }

        public static BotBehaviorSet GetBotBehaviorPredefinedSet(PredefinedAIType aiType)
        {
            BotBehaviorSet botBehaviorSet = new BotBehaviorSet();
            botBehaviorSet.MeleeActions = BotMeleeActions.Default;
            botBehaviorSet.MeleeActionsWhenHit = BotMeleeActions.DefaultWhenHit;
            botBehaviorSet.MeleeActionsWhenEnraged = BotMeleeActions.DefaultWhenEnraged;
            botBehaviorSet.MeleeActionsWhenEnragedAndHit = BotMeleeActions.DefaultWhenEnragedAndHit;
            botBehaviorSet.SeekCoverWhileShooting = 0.0f;
            switch (aiType)
            {
                case PredefinedAIType.None:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.None;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.None;
                    botBehaviorSet.EliminateEnemies = false;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    break;
                case PredefinedAIType.TutorialMeleeA:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PlainGround;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Zombie;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.ZombieA:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Zombie;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.AttackDeadEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.ChokePointValue = 32f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)5;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.5f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.1f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.0f;
                    botBehaviorSet.OffensiveDiveLevel = 0.0f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.0f;
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.ZombieB:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Zombie;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.AttackDeadEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.ChokePointValue = 32f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)5;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.0f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.0f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.6f;
                    botBehaviorSet.OffensiveSprintLevel = 1f;
                    botBehaviorSet.OffensiveDiveLevel = 1f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.0f;
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.BotA:
                case PredefinedAIType.CompanionA:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.OffensiveEnrageLevel = 0.6f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.2f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.4f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.3f;
                    botBehaviorSet.OffensiveDiveLevel = 0.4f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.75f;
                    botBehaviorSet.RocketRideProficiency = 1f;
                    if (aiType == PredefinedAIType.CompanionA)
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)0;
                    }
                    else
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                        botBehaviorSet.ChokePointValue = 150f;
                    }
                    botBehaviorSet.MeleeWaitTimeLimitMin = 800f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 1000f;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.SetMeleeActionsToExpert();
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.SeekCoverWhileShooting = 0.85f;
                    botBehaviorSet.RangedWeaponAccuracy = 0.75f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 200f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 200f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 200f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 1000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.9f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 50f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMax = 100f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 1000f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 200f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 400f;
                    break;
                case PredefinedAIType.DebugBot:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.None;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 0.0f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.0f;
                    botBehaviorSet.DefensiveRollFireLevel = 1f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.0f;
                    botBehaviorSet.OffensiveSprintLevel = 0.0f;
                    botBehaviorSet.OffensiveDiveLevel = 0.0f;
                    botBehaviorSet.MeleeUsage = false;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.RangedWeaponAccuracy = 0.0f;
                    break;
                case PredefinedAIType.MeleeA:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Zombie;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 0.0f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.3f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.8f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.05f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.0f;
                    botBehaviorSet.OffensiveSprintLevel = 0.0f;
                    botBehaviorSet.OffensiveDiveLevel = 0.0f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.TeamLineUp = true;
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.MeleeB:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 0.0f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.2f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.2f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.0f;
                    botBehaviorSet.OffensiveDiveLevel = 0.1f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.1f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.TeamLineUp = true;
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.RangedA:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.2f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.2f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.0f;
                    botBehaviorSet.OffensiveDiveLevel = 0.0f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.0f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.RangedWeaponAccuracy = 0.6f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 700f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 700f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 4000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.7f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 800f;
                    botBehaviorSet.TeamLineUp = true;
                    break;
                case PredefinedAIType.Punk:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.5f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.0f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 1f;
                    botBehaviorSet.OffensiveDiveLevel = 0.2f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.2f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.TeamLineUp = true;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)5,
                        AttackCombo = (ushort)0,
                        Block = (ushort)0,
                        Kick = (ushort)1,
                        Jump = (ushort)0,
                        Wait = (ushort)20,
                        Grab = (ushort)0
                    });
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.SeekCoverWhileShooting = 0.1f;
                    botBehaviorSet.RangedWeaponAccuracy = 0.3f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 1000f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 1500f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 1000f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 1500f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 1000f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 0.0f;
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.Grunt:
                case PredefinedAIType.GruntMelee:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.5f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.0f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.2f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.0f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.TeamLineUp = true;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)8,
                        AttackCombo = (ushort)1,
                        Block = (ushort)1,
                        Kick = (ushort)1,
                        Jump = (ushort)1,
                        Wait = (ushort)20,
                        Grab = (ushort)0
                    });
                    switch (aiType)
                    {
                        case PredefinedAIType.Grunt:
                            botBehaviorSet.RangedWeaponUsage = true;
                            botBehaviorSet.RangedWeaponAccuracy = 0.4f;
                            botBehaviorSet.RangedWeaponAimShootDelayMin = 1000f;
                            botBehaviorSet.RangedWeaponAimShootDelayMax = 1500f;
                            botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 1000f;
                            botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 1500f;
                            botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                            botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                            botBehaviorSet.RangedWeaponBurstPauseMin = 800f;
                            botBehaviorSet.RangedWeaponBurstPauseMax = 1000f;
                            botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 0.0f;
                            break;
                        case PredefinedAIType.GruntMelee:
                            botBehaviorSet.RangedWeaponUsage = false;
                            break;
                    }
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.Hulk:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.5f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.0f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.5f;
                    botBehaviorSet.OffensiveDiveLevel = 0.1f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.1f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.TeamLineUp = true;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)10,
                        AttackCombo = (ushort)20,
                        Block = (ushort)1,
                        Kick = (ushort)1,
                        Jump = (ushort)1,
                        Wait = (ushort)50,
                        Grab = (ushort)5
                    });
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.RangedWeaponAccuracy = 0.2f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 1000f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 1500f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 1000f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 1500f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 1000f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 0.0f;
                    botBehaviorSet.PowerupUsage = false;
                    break;
                case PredefinedAIType.CompanionB:
                case PredefinedAIType.BotB:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.OffensiveEnrageLevel = 0.4f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.2f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.35f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.2f;
                    botBehaviorSet.OffensiveDiveLevel = 0.3f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.6f;
                    botBehaviorSet.RocketRideProficiency = 0.5f;
                    if (aiType == PredefinedAIType.CompanionB)
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)0;
                    }
                    else
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                        botBehaviorSet.ChokePointValue = 150f;
                    }
                    botBehaviorSet.MeleeWaitTimeLimitMin = 800f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 1200f;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.SetMeleeActionsToHard();
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.SeekCoverWhileShooting = 0.6f;
                    botBehaviorSet.RangedWeaponAccuracy = 0.7f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 200f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 600f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 200f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 600f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.85f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 150f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMax = 400f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 800f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 1500f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 250f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 600f;
                    break;
                case PredefinedAIType.CompanionC:
                case PredefinedAIType.BotC:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.SearchItems &= ~SearchItems.Streetsweeper;
                    botBehaviorSet.OffensiveEnrageLevel = 0.2f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.3f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.8f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.3f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.3f;
                    botBehaviorSet.OffensiveSprintLevel = 0.2f;
                    botBehaviorSet.OffensiveDiveLevel = 0.2f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.4f;
                    if (aiType == PredefinedAIType.CompanionC)
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)0;
                    }
                    else
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                        botBehaviorSet.ChokePointValue = 150f;
                    }
                    botBehaviorSet.MeleeWaitTimeLimitMin = 1000f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 2000f;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.SetMeleeActionsToNormal();
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.SeekCoverWhileShooting = 0.4f;
                    botBehaviorSet.RangedWeaponAccuracy = 0.58f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 1500f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 1500f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 500f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 1000f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 500f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 1400f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 4000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.67f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 500f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMax = 1000f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 800f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 1000f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 1200f;
                    break;
                case PredefinedAIType.CompanionD:
                case PredefinedAIType.BotD:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.OffensiveEnrageLevel = 0.0f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.4f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.2f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.2f;
                    botBehaviorSet.OffensiveSprintLevel = 0.15f;
                    botBehaviorSet.OffensiveDiveLevel = 0.2f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.2f;
                    if (aiType == PredefinedAIType.CompanionD)
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)0;
                    }
                    else
                    {
                        botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                        botBehaviorSet.ChokePointValue = 150f;
                    }
                    botBehaviorSet.MeleeWaitTimeLimitMin = 1000f;
                    botBehaviorSet.MeleeWaitTimeLimitMax = 3000f;
                    botBehaviorSet.MeleeActions = BotMeleeActions.Easy;
                    botBehaviorSet.MeleeActionsWhenHit = BotMeleeActions.EasyWhenHit;
                    botBehaviorSet.MeleeActionsWhenEnraged = BotMeleeActions.EasyWhenEnraged;
                    botBehaviorSet.MeleeActionsWhenEnragedAndHit = BotMeleeActions.EasyWhenEnragedAndHit;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.SetMeleeActionsToEasy();
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.SearchItems &= ~SearchItems.Makeshift;
                    botBehaviorSet.SearchItems &= ~SearchItems.Streetsweeper;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.SeekCoverWhileShooting = 0.1f;
                    botBehaviorSet.RangedWeaponAccuracy = 0.55f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 900f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 2300f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 900f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMax = 2300f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 600f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 1800f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 4000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.6f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 900f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMax = 1500f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 600f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 1500f;
                    break;
                case PredefinedAIType.ChallengeA:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.2f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.5f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.2f;
                    botBehaviorSet.OffensiveDiveLevel = 0.4f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.RangedWeaponMode = BotBehaviorRangedWeaponMode.ManualAim;
                    botBehaviorSet.RangedWeaponAccuracy = 0.6f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 200f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 4000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.9f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 50f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 1000f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 2000f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 200f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 400f;
                    break;
                case PredefinedAIType.Meatgrinder:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = true;
                    botBehaviorSet.OffensiveEnrageLevel = 1f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.0f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.0f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.0f;
                    botBehaviorSet.OffensiveDiveLevel = 0.05f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.1f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.SetMeleeActionsAll(new BotMeleeActions()
                    {
                        Attack = (ushort)10,
                        AttackCombo = (ushort)15,
                        Block = (ushort)0,
                        Kick = (ushort)3,
                        Jump = (ushort)1,
                        Wait = (ushort)1,
                        Grab = (ushort)2
                    });
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = true;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.RangedWeaponAccuracy = 0.6f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 700f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 700f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 4000f;
                    botBehaviorSet.RangedWeaponPrecisionAccuracy = 0.7f;
                    botBehaviorSet.RangedWeaponPrecisionAimShootDelayMin = 600f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMin = 400f;
                    botBehaviorSet.RangedWeaponPrecisionBurstTimeMax = 800f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMin = 400f;
                    botBehaviorSet.RangedWeaponPrecisionBurstPauseMax = 800f;
                    botBehaviorSet.TeamLineUp = false;
                    break;
                case PredefinedAIType.Funnyman:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.EliminateEnemies = true;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.OffensiveEnrageLevel = 1f;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.0f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.2f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 0.0f;
                    botBehaviorSet.OffensiveDiveLevel = 0.1f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.0f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.MeleeUsage = true;
                    botBehaviorSet.MeleeWeaponUsage = false;
                    botBehaviorSet.MeleeWeaponUseFullRange = false;
                    botBehaviorSet.RangedWeaponUsage = true;
                    botBehaviorSet.RangedWeaponAccuracy = 0.5f;
                    botBehaviorSet.RangedWeaponAimShootDelayMin = 1000f;
                    botBehaviorSet.RangedWeaponAimShootDelayMax = 1000f;
                    botBehaviorSet.RangedWeaponHipFireAimShootDelayMin = 1000f;
                    botBehaviorSet.RangedWeaponBurstTimeMin = 3000f;
                    botBehaviorSet.RangedWeaponBurstTimeMax = 3000f;
                    botBehaviorSet.RangedWeaponBurstPauseMin = 800f;
                    botBehaviorSet.RangedWeaponBurstPauseMax = 1600f;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 0.0f;
                    botBehaviorSet.RangedWeaponLOSIgnoreTeammates = true;
                    botBehaviorSet.TeamLineUp = false;
                    break;
                case PredefinedAIType.FunnymanRunning:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.PathFinding;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.Default;
                    botBehaviorSet.OffensiveEnrageLevel = 0.5f;
                    botBehaviorSet.DefensiveRollFireLevel = 0.9f;
                    botBehaviorSet.DefensiveAvoidProjectilesLevel = 0.0f;
                    botBehaviorSet.OffensiveClimbingLevel = 0.4f;
                    botBehaviorSet.OffensiveSprintLevel = 1f;
                    botBehaviorSet.OffensiveDiveLevel = 0.2f;
                    botBehaviorSet.CounterOutOfRangeMeleeAttacksLevel = 0.2f;
                    botBehaviorSet.ChokePointValue = 150f;
                    botBehaviorSet.ChokePointPlayerCountThreshold = (ushort)1;
                    botBehaviorSet.NavigationRandomPausesLevel = 0.0f;
                    botBehaviorSet.TeamLineUp = false;
                    botBehaviorSet.EliminateEnemies = false;
                    botBehaviorSet.SearchForItems = false;
                    botBehaviorSet.MeleeUsage = false;
                    botBehaviorSet.RangedWeaponUsage = false;
                    botBehaviorSet.RangedWeaponPrecisionInterpolateTime = 0.0f;
                    botBehaviorSet.GuardRange = 1f;
                    botBehaviorSet.AggroRange = 1f;
                    botBehaviorSet.ChaseRange = 1f;
                    break;
                default:
                    botBehaviorSet.NavigationMode = BotBehaviorNavigationMode.None;
                    botBehaviorSet.MeleeMode = BotBehaviorMeleeMode.None;
                    botBehaviorSet.EliminateEnemies = false;
                    botBehaviorSet.SearchForItems = false;
                    break;
            }
            return botBehaviorSet;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Equals(BotBehaviorSet other)
        {
            if (other.m_navigationMode == this.m_navigationMode && other.m_eliminateEnemies == this.m_eliminateEnemies && (other.m_searchForItems == this.m_searchForItems && other.m_searchItems == this.m_searchItems) && ((int)other.m_chokePointPlayerCountThreshold == (int)this.m_chokePointPlayerCountThreshold && (double)other.m_chokePointValue == (double)this.m_chokePointValue && ((double)other.m_defensiveAvoidProjectilesLevel == (double)this.m_defensiveAvoidProjectilesLevel && (double)other.m_defensiveBlockLevel == (double)this.m_defensiveBlockLevel)) && ((double)other.m_counterOutOfRangeMeleeAttacksLevel == (double)this.m_counterOutOfRangeMeleeAttacksLevel && (double)other.m_defensiveRollFireLevel == (double)this.m_defensiveRollFireLevel && (other.m_meleeUsage == this.m_meleeUsage && other.m_meleeWeaponUsage == this.m_meleeWeaponUsage) && (other.m_meleeWeaponUseFullRange == this.m_meleeWeaponUseFullRange && other.m_rangedWeaponUsage == this.m_rangedWeaponUsage && ((double)other.m_seekCoverWhileShooting == (double)this.m_seekCoverWhileShooting && other.m_rangedWeaponMode == this.m_rangedWeaponMode))) && (other.m_rangedWeaponLOSIgnoreTeammates == this.m_rangedWeaponLOSIgnoreTeammates && other.m_powerupUsage == this.m_powerupUsage && ((double)other.m_navigationRandomPausesLevel == (double)this.m_navigationRandomPausesLevel && (double)other.m_aggroRange == (double)this.m_aggroRange) && ((double)other.m_searchItemRange == (double)this.m_searchItemRange && (double)other.m_guardRange == (double)this.m_guardRange && ((double)other.m_chaseRange == (double)this.m_chaseRange && (double)other.m_offensiveClimbingLevel == (double)this.m_offensiveClimbingLevel)) && ((double)other.m_offensiveDiveLevel == (double)this.m_offensiveDiveLevel && (double)other.m_offensiveEnrageLevel == (double)this.m_offensiveEnrageLevel && ((double)other.m_offensiveSprintLevel == (double)this.m_offensiveSprintLevel && (double)other.m_rangedWeaponAccuracy == (double)this.m_rangedWeaponAccuracy) && ((double)other.m_rangedWeaponAimShootDelayMax == (double)this.m_rangedWeaponAimShootDelayMax && (double)other.m_rangedWeaponAimShootDelayMin == (double)this.m_rangedWeaponAimShootDelayMin && ((double)other.m_rangedWeaponHipFireAimShootDelayMax == (double)this.m_rangedWeaponHipFireAimShootDelayMax && (double)other.m_rangedWeaponHipFireAimShootDelayMin == (double)this.m_rangedWeaponHipFireAimShootDelayMin)))) && ((double)other.m_rangedWeaponBurstPauseMax == (double)this.m_rangedWeaponBurstPauseMax && (double)other.m_rangedWeaponBurstPauseMin == (double)this.m_rangedWeaponBurstPauseMin && ((double)other.m_rangedWeaponBurstTimeMax == (double)this.m_rangedWeaponBurstTimeMax && (double)other.m_rangedWeaponBurstTimeMin == (double)this.m_rangedWeaponBurstTimeMin) && ((double)other.m_rangedWeaponPrecisionInterpolateTime == (double)this.m_rangedWeaponPrecisionInterpolateTime && (double)other.m_rangedWeaponPrecisionAccuracy == (double)this.m_rangedWeaponPrecisionAccuracy && ((double)other.m_rangedWeaponPrecisionAimShootDelayMax == (double)this.m_rangedWeaponPrecisionAimShootDelayMax && (double)other.m_rangedWeaponPrecisionAimShootDelayMin == (double)this.m_rangedWeaponPrecisionAimShootDelayMin)) && ((double)other.m_rangedWeaponPrecisionBurstPauseMax == (double)this.m_rangedWeaponPrecisionBurstPauseMax && (double)other.m_rangedWeaponPrecisionBurstPauseMin == (double)this.m_rangedWeaponPrecisionBurstPauseMin && ((double)other.m_rangedWeaponPrecisionBurstTimeMax == (double)this.m_rangedWeaponPrecisionBurstTimeMax && (double)other.m_rangedWeaponPrecisionBurstTimeMin == (double)this.m_rangedWeaponPrecisionBurstTimeMin) && ((double)other.m_meleeWaitTimeLimitMin == (double)this.m_meleeWaitTimeLimitMin && (double)other.m_meleeWaitTimeLimitMax == (double)this.m_meleeWaitTimeLimitMax && (other.MeleeActions == this.MeleeActions && other.MeleeActionsWhenHit == this.MeleeActionsWhenHit))) && (other.MeleeActionsWhenEnraged == this.MeleeActionsWhenEnraged && other.MeleeActionsWhenEnragedAndHit == this.MeleeActionsWhenEnragedAndHit)))
                return (double)other.m_rocketRideProficiency == (double)this.m_rocketRideProficiency;
            return false;
        }
    }
}
