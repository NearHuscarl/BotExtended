﻿using SFDGameScriptInterface;

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
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        SizeModifier = Size.BelowNormal,
                    };
                    if (botType == BotType.Agent)
                    {
                        botInfo.SearchItems = SearchItems.Secondary;
                        botInfo.AIType = BotAI.Hard;
                    }
                    if (botType == BotType.Soldier)
                    {
                        botInfo.SearchItems = SearchItems.Primary;
                        botInfo.AIType = BotAI.Soldier;
                        // nerf because heavy weapons + penetration powerup + advanced AI
                        botInfo.Modifiers.ProjectileDamageDealtModifier = DamageDealt.BelowNormal;
                    }
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
                case BotType.PunkHulk:
                case BotType.Lumberjack:
                case BotType.NaziHulk:
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
                case BotType.Scientist:
                case BotType.Thug:
                case BotType.Punk:
                {
                    botInfo.AIType = BotAI.Grunt;
                    botInfo.EquipWeaponChance = 0.6f;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.BelowNormal,
                        CurrentHealth = Health.BelowNormal,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        MeleeDamageDealtModifier = DamageDealt.BelowNormal,
                        SizeModifier = Size.BelowNormal,
                    };

                    if (botType == BotType.Thug)
                    {
                        botInfo.SearchItems = SearchItems.Primary | SearchItems.Secondary | SearchItems.Melee;
                        botInfo.SearchRange = WpnSearchRange.Nearby;
                    }
                    if (botType == BotType.Punk)
                    {
                        botInfo.EquipWeaponChance = 1;
                        botInfo.Modifiers.InfiniteAmmo = Constants.TOGGLE_ON;
                    }
                    break;
                }
                #endregion

                #region Grunt with weapon
                case BotType.Bandido:
                case BotType.Bodyguard:
                case BotType.Bodyguard2:
                case BotType.ClownBodyguard:
                case BotType.ClownGangster:
                case BotType.Cyborg:
                case BotType.Elf:
                case BotType.Engineer:
                case BotType.Farmer:
                case BotType.Gangster:
                case BotType.Gardener:
                case BotType.LabAssistant:
                case BotType.MetroCop:
                case BotType.NaziSoldier:
                case BotType.Police:
                case BotType.PoliceSWAT:
                case BotType.Spacer:
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
                    botInfo.SpawnLineChance = .01f;
                    botInfo.DeathLineChance = .01f;

                    if (botType == BotType.Bandido)
                    {
                        botInfo.AIType = BotAI.Cowboy;
                        botInfo.Modifiers.InfiniteAmmo = 1;
                    }
                    if (botType == BotType.Cyborg)
                        botInfo.ImmuneToInfect = true;
                    if (botType == BotType.Engineer)
                        botInfo.Modifiers.SizeModifier = Size.Normal;
                    if (botType == BotType.Gardener)
                        botInfo.SpawnLine = "It's almost harvesting season";
                    if (botType == BotType.Farmer)
                        botInfo.SpawnLine = "Look at all those chickens";
                    break;
                }
                #endregion

                #region Survivor
                case BotType.Survivor:
                {
                    botInfo.AIType = BotAI.Normal;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Weak,
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
                case BotType.Hunter:
                case BotType.Sniper:
                case BotType.SpaceSniper:
                {
                    botInfo.AIType = BotAI.Sniper;
                    botInfo.SearchItems = SearchItems.Primary;
                    botInfo.SearchRange = WpnSearchRange.Nearby;
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

                    if (botType == BotType.SpaceSniper)
                    {
                        botInfo.Modifiers.ProjectileDamageDealtModifier = DamageDealt.Normal;
                        botInfo.Modifiers.ProjectileCritChanceDealtModifier = DamageDealt.Normal;
                    }
                    if (botType == BotType.Hunter)
                    {
                        botInfo.SearchItems = SearchItems.Health;
                        botInfo.AIType = BotAI.Hunter;
                        botInfo.Modifiers.InfiniteAmmo = Constants.TOGGLE_ON;
                        botInfo.SpawnLine = "You can run, but you cant hide";
                    }
                    break;
                }
                #endregion

                #region Stripper
                case BotType.Stripper:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Makeshift | SearchItems.Health;
                    botInfo.SearchRange = WpnSearchRange.InSight;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Weak,
                        CurrentHealth = Health.Weak,
                        ProjectileDamageDealtModifier = DamageDealt.FairlyHigh,
                        ProjectileCritChanceDealtModifier = DamageDealt.FairlyHigh,
                        MeleeDamageDealtModifier = DamageDealt.FairlyLow,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        FireDamageTakenModifier = DamageTaken.Vulnerable,
                        SizeModifier = Size.BelowNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    break;
                }
                #endregion

                #region SuicideDwarf
                case BotType.SuicideDwarf:
                {
                    botInfo.AIType = BotAI.Easy;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyWeak,
                        CurrentHealth = Health.ExtremelyWeak,
                        SizeModifier = Size.ExtremelySmall,
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

                #region Boss Agent79
                case BotType.Agent79:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Amos
                case BotType.Amos:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Primary;
                    botInfo.SearchRange = WpnSearchRange.InSight;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeDamageDealtModifier = DamageDealt.High,
                        ImpactDamageTakenModifier = DamageTaken.FairlyResistant,
                        RunSpeedModifier = Speed.BelowNormal,
                        SprintSpeedModifier = Speed.BelowNormal,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        SizeModifier = Size.Big,
                    };
                    break;
                }
                #endregion

                #region Boss AssKicker
                case BotType.AssKicker:
                {
                    botInfo.AIType = BotAI.AssKicker;
                    botInfo.SearchItems = SearchItems.Melee;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        MeleeDamageDealtModifier = DamageDealt.AboveNormal,
                        RunSpeedModifier = Speed.ExtremelyFast,
                        SprintSpeedModifier = Speed.ExtremelyFast,
                        SizeModifier = Size.BelowNormal,
                        EnergyRechargeModifier = EnergyRecharge.Quick,
                        MeleeForceModifier = MeleeForce.OnePunch,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Balista
                case BotType.Balista:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.All;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        RunSpeedModifier = Speed.Fast,
                        SprintSpeedModifier = Speed.Fast,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        MeleeDamageDealtModifier = DamageDealt.FairlyLow,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        SizeModifier = Size.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Balloonatic
                case BotType.Balloonatic:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Primary | SearchItems.Health;
                    botInfo.SearchRange = WpnSearchRange.InSight;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        ImpactDamageTakenModifier = DamageTaken.VeryResistant,
                        SizeModifier = Size.VeryBig,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss BazookaJane
                case BotType.BazookaJane:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Melee | SearchItems.Powerups | SearchItems.Secondary;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        SizeModifier = Size.AboveNormal,
                        RunSpeedModifier = Speed.Slow,
                        SprintSpeedModifier = Speed.Slow,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Beast
                case BotType.Beast:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
                        EnergyConsumptionModifier = 0,
                        SizeModifier = Size.ExtremelyBig,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Berserker
                case BotType.Berserker:
                {
                    botInfo.AIType = BotAI.God;
                    botInfo.SearchItems = SearchItems.Melee;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        MeleeDamageDealtModifier = DamageDealt.UltraHigh,
                        MeleeForceModifier = MeleeForce.UltraStrong,
                        SprintSpeedModifier = Speed.Hacker,
                        RunSpeedModifier = Speed.Hacker,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Bobby
                case BotType.Bobby:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        SizeModifier = Size.AboveNormal,
                        ProjectileDamageTakenModifier = DamageTaken.Resistant,
                        ProjectileDamageDealtModifier = DamageDealt.FairlyHigh,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Boffin
                case BotType.Boffin:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.SpecificSearchItems.Add(WeaponItem.GRENADE_LAUNCHER);
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Chairman
                case BotType.Chairman:
                {
                    botInfo.AIType = BotAI.MeleeExpert;
                    botInfo.SearchItems = SearchItems.Makeshift | SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeForceModifier = MeleeForce.Strong,
                        RunSpeedModifier = Speed.Hacker,
                        SprintSpeedModifier = Speed.Fast,
                        SizeModifier = Size.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Cindy
                case BotType.Cindy:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchRange = WpnSearchRange.InSight;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Streetsweeper | SearchItems.Powerups | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        MaxEnergy = Stamina.High,
                        CurrentEnergy = Stamina.High,
                        RunSpeedModifier = Speed.Fast,
                        SprintSpeedModifier = Speed.Fast,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Demolitionist
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

                #region Boss Demoman
                case BotType.Demoman:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Makeshift | SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        RunSpeedModifier = Speed.BelowNormal,
                        SprintSpeedModifier = Speed.BelowNormal,
                        ExplosionDamageTakenModifier = DamageTaken.ExtremelyResistant,
                        MeleeDamageDealtModifier = DamageDealt.High,
                        MeleeForceModifier = MeleeForce.VeryStrong,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Fritzliebe
                case BotType.Fritzliebe:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Primary | SearchItems.Health;
                    botInfo.SearchRange = WpnSearchRange.Nearby;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        SizeModifier = Size.BelowNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Funnyman
                case BotType.Funnyman:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Powerups;
                    botInfo.SearchRange = WpnSearchRange.InSight;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        MeleeDamageTakenModifier = DamageTaken.Resistant,
                        SizeModifier = Size.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Hacker
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

                #region Boss Handler
                case BotType.Handler:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Powerups | SearchItems.Primary | SearchItems.Secondary;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        MaxEnergy = Stamina.VeryHigh,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Hawkeye
                case BotType.Hawkeye:
                {
                    botInfo.AIType = BotAI.RangeExpert;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Powerups | SearchItems.Streetsweeper;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        ProjectileCritChanceDealtModifier = DamageDealt.VeryHigh,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss HeavySoldier
                case BotType.HeavySoldier:
                {
                    botInfo.AIType = BotAI.Soldier;
                    botInfo.SearchItems = SearchItems.Primary;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.AboveNormal,
                        RunSpeedModifier = Speed.Slow,
                        SprintSpeedModifier = Speed.Slow,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Hitman
                case BotType.Hitman:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.All;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        RunSpeedModifier = Speed.BelowNormal,
                        SprintSpeedModifier = Speed.Fast,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Huntsman
                case BotType.Huntsman:
                {
                    botInfo.AIType = BotAI.RangeExpert;
                    botInfo.SearchItems = SearchItems.Makeshift | SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        ProjectileCritChanceDealtModifier = DamageDealt.High,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Incinerator
                case BotType.Incinerator:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Powerups;
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

                #region Boss Ion
                case BotType.Ion:
                {
                    botInfo.AIType = BotAI.Cowboy;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        RunSpeedModifier = Speed.VerySlow,
                        SprintSpeedModifier = Speed.Slow,
                        ProjectileCritChanceTakenModifier = DamageTaken.Unbeatable,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.Big,
                    };
                    botInfo.ImmuneToInfect = true; // robot cannot be infected
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Pyromaniac
                case BotType.Pyromaniac:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        FireDamageTakenModifier = DamageTaken.VeryResistant,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Firebug
                case BotType.Firebug:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Melee | SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        FireDamageTakenModifier = DamageTaken.Unbeatable,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Fireman
                case BotType.Fireman:
                {
                    botInfo.AIType = BotAI.AssassinMelee;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        FireDamageTakenModifier = DamageTaken.ExtremelyResistant,
                        MeleeDamageTakenModifier = DamageTaken.Resistant,
                        RunSpeedModifier = Speed.Fast,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Jo
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

                #region Boss Kingpin
                case BotType.Kingpin:
                {
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Health | SearchItems.Streetsweeper;
                    botInfo.AIType = BotAI.Kingpin;
                    botInfo.SearchRange = WpnSearchRange.Nearby; // encourage this bot to fight in melee to crush enemies
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        EnergyConsumptionModifier = .1f,
                        SizeModifier = Size.Big,
                        MeleeForceModifier = MeleeForce.Strong,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Kriegbär
                case BotType.Kriegbar:
                {
                    botInfo.AIType = BotAI.RagingHulk;
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

                #region Boss LordPinkerton
                case BotType.LordPinkerton:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.All;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        ImpactDamageTakenModifier = DamageTaken.VeryResistant,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Meatgrinder
                case BotType.Meatgrinder:
                {
                    botInfo.AIType = BotAI.Meatgrinder;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
                        MaxEnergy = Stamina.ExtremelyHigh,
                        CurrentEnergy = Stamina.ExtremelyHigh,
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

                #region Boss Mecha
                case BotType.Mecha:
                {
                    botInfo.AIType = BotAI.Hulk;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.ExtremelyStrong,
                        CurrentHealth = Health.ExtremelyStrong,
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

                #region Boss MetroCop2
                case BotType.MetroCop2:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Streetsweeper | SearchItems.Powerups | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        SizeModifier = Size.BelowNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Monk
                case BotType.Monk:
                {
                    botInfo.AIType = BotAI.MeleeHard;
                    botInfo.SearchItems = SearchItems.Melee | SearchItems.Makeshift | SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeDamageDealtModifier = DamageDealt.High,
                        RunSpeedModifier = Speed.Fast,
                        SprintSpeedModifier = Speed.Fast,
                        SizeModifier = Size.BelowNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss BigMutant
                case BotType.Mutant:
                {
                    botInfo.AIType = BotAI.Grunt;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        ImpactDamageTakenModifier = DamageTaken.ExtremelyVulnerable,
                        SizeModifier = Size.Big,
                    };
                    break;
                }
                case BotType.BigMutant:
                {
                    botInfo.AIType = BotAI.MeleeHard;
                    botInfo.Name = "Big Mutant";
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.UltraStrong,
                        CurrentHealth = Health.UltraStrong,
                        ImpactDamageTakenModifier = DamageTaken.ExtremelyVulnerable,
                        SizeModifier = Size.VeryBig,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Nadja
                case BotType.Nadja:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Napoleon
                case BotType.Napoleon:
                {
                    botInfo.AIType = BotAI.Soldier;
                    botInfo.SearchItems = SearchItems.Primary;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        EnergyConsumptionModifier = 0,
                        ProjectileDamageTakenModifier = DamageTaken.ExtremelyResistant,
                        SizeModifier = Size.Tiny,
                        RunSpeedModifier = Speed.VeryFast,
                        SprintSpeedModifier = Speed.VeryFast,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Ninja
                case BotType.Ninja:
                {
                    botInfo.AIType = BotAI.Ninja;
                    botInfo.SearchItems = SearchItems.Melee;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeDamageDealtModifier = DamageDealt.FairlyHigh,
                        RunSpeedModifier = Speed.UltraFast,
                        SprintSpeedModifier = Speed.UltraFast,
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

                #region Boss PoliceChief
                case BotType.PoliceChief:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        EnergyRechargeModifier = EnergyRecharge.Quick,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss President
                case BotType.President:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        EnergyRechargeModifier = EnergyRecharge.Slow,
                        ImpactDamageTakenModifier = DamageTaken.ExtremelyResistant,
                        SizeModifier = Size.Chonky,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Queen
                case BotType.Queen:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Melee | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        MeleeDamageDealtModifier = DamageDealt.Low,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.Fast,
                        SizeModifier = Size.BelowNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Quillhogg
                case BotType.Quillhogg:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Powerups | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Tank
                case BotType.Tank:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Powerups;
                    botInfo.ImmuneToInfect = true; // robot cannot be infected
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MaxEnergy = Stamina.VeryHigh,
                        CurrentEnergy = Stamina.VeryHigh,
                        RunSpeedModifier = Speed.Slow,
                        SprintSpeedModifier = Speed.Fast,
                        ProjectileDamageTakenModifier = DamageTaken.ExtremelyResistant,
                        ProjectileCritChanceTakenModifier = DamageTaken.Unbeatable,
                        SizeModifier = Size.ExtremelyBig,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Teddybear
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

                #region Boss Babybear
                case BotType.Babybear:
                {
                    botInfo.AIType = BotAI.Babybear;
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
                
                #region Boss Rambo
                case BotType.Rambo:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        ExplosionDamageTakenModifier = DamageTaken.VeryResistant,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.Big,
                        RunSpeedModifier = Speed.BelowNormal,
                        SprintSpeedModifier = Speed.BelowNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Raze
                case BotType.Raze:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Primary | SearchItems.Secondary | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        ExplosionDamageTakenModifier = DamageTaken.VeryResistant,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Reznor
                case BotType.Reznor:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Primary | SearchItems.Health | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        RunSpeedModifier = Speed.Slow,
                        SprintSpeedModifier = Speed.Slow,
                        MeleeForceModifier = MeleeForce.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.Big,
                    };
                    botInfo.IsBoss = true; // set IsBoss to spawn once
                    break;
                }
                #endregion

                #region Boss Santa
                case BotType.Santa:
                {
                    botInfo.AIType = BotAI.Hard; // ChallengeA
                    botInfo.SearchRange = WpnSearchRange.InSight;
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

                #region Boss Sheriff
                case BotType.Sheriff:
                {
                    botInfo.AIType = BotAI.Sheriff;
                    botInfo.SearchItems = SearchItems.Secondary | SearchItems.Powerups | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        MaxEnergy = Stamina.AboveNormal,
                        CurrentEnergy = Stamina.AboveNormal,
                        ProjectileDamageTakenModifier = DamageTaken.FairlyResistant,
                        SizeModifier = Size.AboveNormal,
                        ItemDropMode = ItemDropMode.Break,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    botInfo.SpawnLine = "I wanted to break your jaw";
                    botInfo.SpawnLineChance = .1f;
                    break;
                }
                #endregion

                #region Boss Smoker
                case BotType.Smoker:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchRange = WpnSearchRange.InSight;
                    botInfo.SearchItems = SearchItems.Primary | SearchItems.Powerups | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        FireDamageTakenModifier = DamageTaken.SlightlyResistant,
                        ProjectileDamageDealtModifier = DamageDealt.BelowNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Spy
                case BotType.Spy:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Health | SearchItems.Streetsweeper | SearchItems.Powerups;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                        SizeModifier = Size.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Survivalist
                case BotType.Survivalist:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.SearchItems = SearchItems.Melee | SearchItems.Makeshift | SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.Strong,
                        CurrentHealth = Health.Strong,
                        ProjectileDamageDealtModifier = DamageDealt.VeryHigh,
                        InfiniteAmmo = Constants.TOGGLE_ON,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss Translucent
                case BotType.Translucent:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.SearchItems = SearchItems.Health;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        SizeModifier = Size.AboveNormal,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Boss ZombieEater
                case BotType.ZombieEater:
                {
                    botInfo.AIType = BotAI.ZombieFighter;
                    botInfo.Name = "Gluttonous Zombie";
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.AboveNormal,
                        CurrentHealth = Health.AboveNormal,
                        RunSpeedModifier = Speed.VeryFast,
                        SprintSpeedModifier = Speed.VeryFast,
                        SizeModifier = Size.Small,
                    };
                    botInfo.IsBoss = true;
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Boss ZombieFighter
                case BotType.ZombieFighter:
                {
                    botInfo.AIType = BotAI.ZombieFighter;
                    botInfo.Modifiers = new PlayerModifiers(true)
                    {
                        MaxHealth = Health.VeryStrong,
                        CurrentHealth = Health.VeryStrong,
                        MeleeDamageDealtModifier = DamageDealt.AboveNormal,
                        RunSpeedModifier = Speed.AboveNormal,
                        SprintSpeedModifier = Speed.AboveNormal,
                        SizeModifier = Size.Big,
                        MeleeStunImmunity = Constants.TOGGLE_ON,
                        InfiniteAmmo = Constants.TOGGLE_ON,
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
