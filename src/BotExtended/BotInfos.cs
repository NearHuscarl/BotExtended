using SFDGameScriptInterface;

namespace BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        public static BotInfo GetInfo(BotType botType)
        {
            var botInfo = new BotInfo();

            switch (botType)
            {
                #region Agent, Soldier
                case BotType.Agent:
                case BotType.Soldier:
                {
                    if (botType == BotType.Agent)
                    {
                        botInfo.SearchItems = SearchItems.Secondary;
                        botInfo.AIType = BotAI.Hard;
                    }
                    if (botType == BotType.Soldier || botType == BotType.Soldier2)
                    {
                        botInfo.SearchItems = SearchItems.Primary;
                        botInfo.AIType = BotAI.Soldier;
                    }
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        SizeModifier = Size.BelowNormal,
                    };
                    break;
                }
                #endregion

                #region Assassin
                case BotType.AssassinMelee:
                {
                    botInfo.AIType = BotAI.AssassinMelee;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        RunSpeedModifier = Speed.VeryFast,
                        SprintSpeedModifier = Speed.VeryFast,
                        SizeModifier = Size.BelowNormal,
                    };
                    break;
                }
                case BotType.AssassinRange:
                {
                    botInfo.AIType = BotAI.AssassinRange;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        RunSpeedModifier = Speed.VeryFast,
                        SprintSpeedModifier = Speed.VeryFast,
                        SizeModifier = Size.BelowNormal,
                    };
                    break;
                }
                #endregion

                #region Boxer
                case BotType.ClownBoxer:
                {
                    botInfo.AIType = BotAI.Hulk;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        ProjectileDamageDealtModifier = DamageDealt.VeryLow,
                        MeleeDamageDealtModifier = DamageDealt.AboveNormal,
                        MeleeForceModifier = MeleeForce.Strong,
                        SizeModifier = Size.VeryBig,
                    };
                    break;
                }
                #endregion

                #region Cowboy (faster grunt)
                case BotType.ClownCowboy:
                case BotType.Cowboy:
                {
                    botInfo.AIType = BotAI.Cowboy;
                    botInfo.EquipWeaponChance = 1f;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.AboveNormal,
                        MeleeDamageDealtModifier = DamageDealt.FairlyLow,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        SizeModifier = Size.Small,
                    };
                    botInfo.SpawnLine = "Move 'em on, head 'em up...";
                    botInfo.SpawnLineChance = 0.05f;
                    botInfo.DeathLine = "Count 'em in, ride 'em... oof!";
                    botInfo.DeathLineChance = 0.05f;
                    break;
                }
                #endregion

                #region Hulk
                case BotType.BikerHulk:
                case BotType.GangsterHulk:
                case BotType.ThugHulk:
                {
                    botInfo.AIType = BotAI.Hulk;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        ProjectileDamageDealtModifier = DamageDealt.VeryLow,
                        MeleeDamageDealtModifier = DamageDealt.AboveNormal,
                        MeleeForceModifier = MeleeForce.Strong,
                        RunSpeedModifier = Speed.Slow,
                        SprintSpeedModifier = Speed.Slow,
                        SizeModifier = Size.VeryBig,
                    };
                    break;
                }
                #endregion

                #region Grunt
                case BotType.Biker:
                case BotType.NaziScientist:
                case BotType.Thug:
                {
                    botInfo.AIType = BotAI.Grunt;
                    botInfo.EquipWeaponChance = 0.5f;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        SizeModifier = Size.BelowNormal,
                    };
                    break;
                }
                #endregion

                #region Grunt with weapon
                case BotType.Agent2:
                case BotType.Bandido:
                case BotType.Bodyguard:
                case BotType.Bodyguard2:
                case BotType.ClownBodyguard:
                case BotType.ClownGangster:
                case BotType.Elf:
                case BotType.Engineer:
                case BotType.Gangster:
                case BotType.MetroCop:
                case BotType.Police:
                case BotType.PoliceSWAT:
                {
                    botInfo.AIType = BotAI.Grunt;
                    botInfo.EquipWeaponChance = 1f;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        SizeModifier = Size.BelowNormal,
                    };

                    if (botType == BotType.Engineer)
                        botInfo.Modifiers.SizeModifier = Size.Normal;
                    break;
                }
                #endregion

                #region Marauder
                case BotType.MarauderBiker:
                case BotType.MarauderCrazy:
                case BotType.MarauderNaked:
                case BotType.MarauderRifleman:
                case BotType.MarauderRobber:
                case BotType.MarauderTough:
                {
                    botInfo.AIType = BotAI.Grunt;
                    botInfo.EquipWeaponChance = 1f;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Weak * 100, // Fake blood on the face to make it look like the infected
                        CurrentHealth = Health.Weak,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        SizeModifier = Size.BelowNormal,
                    };
                    botInfo.ZombieStatus = ZombieStatus.Infected;
                    break;
                }
                #endregion

                #region Sniper
                case BotType.Sniper:
                {
                    botInfo.AIType = BotAI.Sniper;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Weak,
                        CurrentHealth = Health.Weak,
                        ProjectileDamageDealtModifier = DamageDealt.FairlyHigh,
                        ProjectileCritChanceDealtModifier = DamageDealt.FairlyHigh,
                        MeleeDamageDealtModifier = DamageDealt.FairlyLow,
                        RunSpeedModifier = Speed.Slow,
                        SprintSpeedModifier = Speed.Slow,
                        SizeModifier = Size.BelowNormal,
                    };
                    break;
                }
                #endregion

                #region Zombie
                case BotType.Zombie:
                case BotType.ZombieAgent:
                case BotType.ZombieGangster:
                case BotType.ZombieNinja:
                case BotType.ZombiePolice:
                case BotType.ZombiePrussian:
                case BotType.ZombieSoldier:
                case BotType.ZombieThug:
                case BotType.ZombieWorker:
                {
                    botInfo.AIType = BotAI.ZombieSlow;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Weak,
                        CurrentHealth = Health.Weak,
                        MeleeDamageDealtModifier = DamageDealt.Low,
                        RunSpeedModifier = Speed.Slow,
                        SizeModifier = Size.BelowNormal,
                    };
                    botInfo.SpawnLine = "Brainzz";
                    botInfo.SpawnLineChance = 0.1f;
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Zombie fast
                case BotType.ZombieChild:
                {
                    botInfo.AIType = BotAI.ZombieFast;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyWeak,
                        CurrentHealth = Health.ExtremelyWeak,
                        MeleeDamageDealtModifier = DamageDealt.Low,
                        RunSpeedModifier = Speed.Fast,
                        SprintSpeedModifier = Speed.Fast,
                        MeleeForceModifier = MeleeForce.Weak,
                        SizeModifier = Size.VerySmall,
                    };
                    botInfo.SpawnLine = "Brainzz";
                    botInfo.SpawnLineChance = 0.1f;
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Zombie fat
                case BotType.ZombieFat:
                {
                    botInfo.AIType = BotAI.ZombieSlow;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.EmbarrassinglyWeak,
                        CurrentHealth = Health.EmbarrassinglyWeak,
                        MeleeDamageDealtModifier = DamageDealt.FairlyHigh,
                        RunSpeedModifier = Speed.BarelyAny,
                        SprintSpeedModifier = Speed.BarelyAny,
                        SizeModifier = Size.Chonky,
                    };
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Zombie flamer
                case BotType.ZombieFlamer:
                {
                    botInfo.AIType = BotAI.ZombieFast;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyWeak,
                        CurrentHealth = Health.ExtremelyWeak,
                        FireDamageTakenModifier = DamageTaken.UltraResistant,
                        MeleeDamageDealtModifier = DamageDealt.VeryLow,
                        RunSpeedModifier = Speed.Fast,
                        SprintSpeedModifier = Speed.Fast,
                        SizeModifier = Size.BelowNormal,
                    };
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Zombie hulk
                case BotType.ZombieBruiser:
                {
                    botInfo.AIType = BotAI.ZombieHulk;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        MeleeDamageDealtModifier = DamageDealt.AboveNormal,
                        MeleeForceModifier = MeleeForce.Strong,
                        RunSpeedModifier = Speed.Slow,
                        SprintSpeedModifier = Speed.Slow,
                        SizeModifier = Size.ExtremelyBig,
                    };
                    botInfo.SpawnLine = "Brainzz";
                    botInfo.SpawnLineChance = 0.1f;
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Bosses Demolitionist
                case BotType.Demolitionist:
                {
                    botInfo.AIType = BotAI.RangeHard;
                    botInfo.SearchItems = SearchItems.Primary | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        ProjectileDamageDealtModifier = DamageDealt.UltraHigh,
                        ProjectileCritChanceDealtModifier = DamageDealt.UltraHigh,
                        MeleeDamageDealtModifier = DamageDealt.VeryHigh,
                        RunSpeedModifier = Speed.BarelyAny,
                        SprintSpeedModifier = Speed.BarelyAny,
                        SizeModifier = Size.BelowNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Fritzliebe
                case BotType.Fritzliebe:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        SizeModifier = Size.BelowNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Funnyman
                case BotType.Funnyman:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
                        SizeModifier = Size.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Hacker
                case BotType.Hacker:
                {
                    botInfo.AIType = BotAI.Hacker;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        EnergyConsumptionModifier = Constants.TOGGLE_OFF,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Incinerator
                case BotType.Incinerator:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
                        FireDamageTakenModifier = DamageTaken.ExtremelyResistant,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Jo
                case BotType.Jo:
                {
                    botInfo.AIType = BotAI.MeleeExpert;
                    botInfo.SearchItems = SearchItems.Makeshift | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
                        MeleeForceModifier = MeleeForce.Strong,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Kingpin
                case BotType.Kingpin:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
                        SizeModifier = Size.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Kriegbär
                case BotType.Kriegbär:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.UltraStrong,
                        CurrentHealth = Health.UltraStrong,
                        MaxEnergy = Stamina.UltraHigh,
                        CurrentEnergy = Stamina.UltraHigh,
                        FireDamageTakenModifier = DamageTaken.VeryVulnerable,
                        MeleeForceModifier = MeleeForce.VeryStrong,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        SizeModifier = Size.Chonky,
                    };
                    botInfo.IsBoss = true;
                    botInfo.SpawnLine = "HNNNARRRRRRRHHH!";
                    break;
                }
                #endregion

                #region Bosses Meatgrinder
                case BotType.Meatgrinder:
                {
                    botInfo.AIType = BotAI.Meatgrinder;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
                        MaxEnergy = Stamina.ExtremelyHigh,
                        CurrentEnergy = Stamina.ExtremelyHigh,
                        ProjectileDamageDealtModifier = DamageDealt.VeryHigh,
                        MeleeDamageDealtModifier = DamageDealt.VeryHigh,
                        MeleeForceModifier = MeleeForce.Strong,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.Fast,
                        SizeModifier = Size.Big,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Mecha
                case BotType.Mecha:
                {
                    botInfo.AIType = BotAI.Hulk;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.UltraStrong,
                        CurrentHealth = Health.UltraStrong,
                        ExplosionDamageTakenModifier = DamageTaken.ExtremelyResistant, // 1200 (300 / 0.25)
                        ProjectileDamageTakenModifier = DamageTaken.VeryResistant, // 600
                        ImpactDamageTakenModifier = DamageTaken.Unbeatable,
                        MeleeForceModifier = MeleeForce.UltraStrong,
                        MeleeStunImmunity = Constants.TOGGLE_ON,
                        CanBurn = Constants.TOGGLE_OFF,
                        RunSpeedModifier = Speed.BelowNormal,
                        SprintSpeedModifier = Speed.BelowNormal,
                        SizeModifier = Size.ExtremelyBig,
                    };
                    botInfo.IsBoss = true;
                    botInfo.ImmuneToInfect = true;
                    break;
                }
                #endregion

                #region Bosses MetroCop2
                case BotType.MetroCop2:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        SizeModifier = Size.BelowNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses MirrorMan
                case BotType.MirrorMan:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MaxEnergy = Stamina.VeryHigh,
                        CurrentEnergy = Stamina.VeryHigh,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        ProjectileDamageTakenModifier = DamageTaken.ExtremelyResistant,
                        ProjectileCritChanceTakenModifier = DamageTaken.ExtremelyResistant,
                        SizeModifier = Size.Small,
                    };
                    botInfo.ImmuneToInfect = true; // robot cannot be infected
                    botInfo.SpawnLine = "BRING IT ON!!!";
                    botInfo.SpawnLineChance = .1f;
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Ninja
                case BotType.Ninja:
                {
                    botInfo.AIType = BotAI.Ninja;
                    botInfo.SearchItems = SearchItems.Melee;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeDamageDealtModifier = DamageDealt.FairlyHigh,
                        RunSpeedModifier = Speed.ExtremelyFast,
                        SprintSpeedModifier = Speed.ExtremelyFast,
                        SizeModifier = Size.Small,
                        EnergyRechargeModifier = EnergyRecharge.Quick,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    botInfo.SpawnLine = "Tatakai...";
                    botInfo.DeathLine = "H-h-haji...";
                    break;
                }
                #endregion

                #region Bosses Teddybear
                case BotType.Teddybear:
                {
                    botInfo.AIType = BotAI.Hulk;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.UltraStrong,
                        CurrentHealth = Health.UltraStrong,
                        MaxEnergy = Stamina.UltraHigh,
                        CurrentEnergy = Stamina.UltraHigh,
                        MeleeDamageDealtModifier = DamageDealt.High,
                        MeleeForceModifier = MeleeForce.VeryStrong,
                        RunSpeedModifier = Speed.BelowNormal,
                        SprintSpeedModifier = Speed.BelowNormal,
                        SizeModifier = Size.Chonky,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Babybear
                case BotType.Babybear:
                {
                    botInfo.AIType = BotAI.Easy;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryWeak,
                        CurrentHealth = Health.VeryWeak,
                        RunSpeedModifier = Speed.VeryFast,
                        SprintSpeedModifier = Speed.VeryFast,
                        MeleeForceModifier = MeleeForce.Weak,
                        SizeModifier = Size.Tiny,
                    };
                    botInfo.IsBoss = true; // set IsBoss to spawn once
                    break;
                }
                #endregion

                #region Bosses Santa
                case BotType.Santa:
                {
                    botInfo.AIType = BotAI.Hard; // ChallengeA
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        ExplosionDamageTakenModifier = DamageTaken.VeryResistant,
                        MeleeForceModifier = MeleeForce.Strong,
                        SizeModifier = Size.Big,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    botInfo.SpawnLine = "Ho ho ho!";
                    botInfo.DeathLine = "Ho ohhhh...";
                    break;
                }
                #endregion

                #region Bosses ZombieFighter
                case BotType.ZombieFighter:
                {
                    botInfo.AIType = BotAI.ZombieFighter;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeDamageDealtModifier = DamageDealt.AboveNormal,
                        RunSpeedModifier = Speed.BelowNormal,
                        SprintSpeedModifier = Speed.BelowNormal,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion
            }

            return botInfo;
        }
    }
}
