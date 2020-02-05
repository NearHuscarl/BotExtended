using SFDGameScriptInterface;

namespace SFDScript.BotExtended
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
                    botInfo.AIType = BotAI.Hard;
                    if (botType == BotType.Agent)
                        botInfo.SearchItems = SearchItems.Secondary;
                    if (botType == BotType.Soldier || botType == BotType.Soldier2)
                        botInfo.SearchItems = SearchItems.Primary;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 70,
                        CurrentHealth = 70,
                        ProjectileDamageDealtModifier = 0.9f,
                        MeleeDamageDealtModifier = 0.9f,
                        SizeModifier = 0.95f,
                    };
                    break;
                }
                #endregion

                #region Assassin
                case BotType.AssassinMelee:
                {
                    botInfo.AIType = BotAI.MeleeHard;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 70,
                        CurrentHealth = 70,
                        ProjectileDamageDealtModifier = 0.9f,
                        MeleeDamageDealtModifier = 0.95f,
                        RunSpeedModifier = 1.25f,
                        SprintSpeedModifier = 1.4f,
                        SizeModifier = 0.95f,
                    };
                    break;
                }
                case BotType.AssassinRange:
                {
                    botInfo.AIType = BotAI.RangeHard;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 70,
                        CurrentHealth = 70,
                        ProjectileDamageDealtModifier = 0.9f,
                        MeleeDamageDealtModifier = 0.95f,
                        RunSpeedModifier = 1.25f,
                        SprintSpeedModifier = 1.4f,
                        SizeModifier = 0.95f,
                    };
                    break;
                }
                #endregion

                #region Boxer
                case BotType.ClownBoxer:
                {
                    botInfo.AIType = BotAI.Hulk;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 110,
                        CurrentHealth = 110,
                        ProjectileDamageDealtModifier = 0.5f,
                        MeleeDamageDealtModifier = 1.1f,
                        MeleeForceModifier = 1.5f,
                        SizeModifier = 1.15f,
                    };
                    break;
                }
                #endregion

                #region Cowboy (faster grunt)
                case BotType.ClownCowboy:
                case BotType.Cowboy:
                {
                    botInfo.AIType = BotAI.Grunt;
                    botInfo.EquipWeaponChance = 1f;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 70,
                        CurrentHealth = 70,
                        ProjectileDamageDealtModifier = 1.1f,
                        MeleeDamageDealtModifier = 0.85f,
                        RunSpeedModifier = 1.1f,
                        SprintSpeedModifier = 1.1f,
                        SizeModifier = 0.9f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 150,
                        CurrentHealth = 150,
                        ProjectileDamageDealtModifier = 0.5f,
                        MeleeDamageDealtModifier = 1.1f,
                        MeleeForceModifier = 1.5f,
                        RunSpeedModifier = 0.75f,
                        SprintSpeedModifier = 0.75f,
                        SizeModifier = 1.15f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 70,
                        CurrentHealth = 70,
                        ProjectileDamageDealtModifier = 0.9f,
                        MeleeDamageDealtModifier = 0.95f,
                        SizeModifier = 0.95f,
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
                case BotType.Gangster:
                case BotType.MetroCop:
                case BotType.Police:
                case BotType.PoliceSWAT:
                {
                    botInfo.AIType = BotAI.Grunt;
                    botInfo.EquipWeaponChance = 1f;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 70,
                        CurrentHealth = 70,
                        ProjectileDamageDealtModifier = 0.9f,
                        MeleeDamageDealtModifier = 0.95f,
                        SizeModifier = 0.95f,
                    };
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 1000,
                        CurrentHealth = 70, // Fake blood on the face to make it look like the infected
                        ProjectileDamageDealtModifier = 0.9f,
                        MeleeDamageDealtModifier = 0.95f,
                        SizeModifier = 0.95f,
                    };
                    botInfo.ZombieStatus = ZombieStatus.Infected;
                    break;
                }
                #endregion

                #region Sniper
                case BotType.Sniper:
                {
                    botInfo.AIType = BotAI.Sniper;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 60,
                        CurrentHealth = 60,
                        ProjectileDamageDealtModifier = 1.15f,
                        ProjectileCritChanceDealtModifier = 1.15f,
                        MeleeDamageDealtModifier = 0.85f,
                        RunSpeedModifier = 0.8f,
                        SprintSpeedModifier = 0.8f,
                        SizeModifier = 0.95f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 60,
                        CurrentHealth = 60,
                        MeleeDamageDealtModifier = 0.75f,
                        RunSpeedModifier = 0.75f,
                        SizeModifier = 0.95f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 35,
                        CurrentHealth = 35,
                        MeleeDamageDealtModifier = 0.75f,
                        RunSpeedModifier = 1.15f,
                        SprintSpeedModifier = 1.15f,
                        SizeModifier = 0.85f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 20,
                        CurrentHealth = 20,
                        MeleeDamageDealtModifier = 1.2f,
                        RunSpeedModifier = 0.5f,
                        SprintSpeedModifier = 0.5f,
                        SizeModifier = 1.25f,
                    };
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Zombie flamer
                case BotType.ZombieFlamer:
                {
                    botInfo.AIType = BotAI.ZombieFast;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 35,
                        CurrentHealth = 35,
                        FireDamageTakenModifier = 0.01f,
                        MeleeDamageDealtModifier = 0.5f,
                        RunSpeedModifier = 1.15f,
                        SprintSpeedModifier = 1.15f,
                        SizeModifier = 0.95f,
                    };
                    botInfo.ZombieStatus = ZombieStatus.Zombie;
                    break;
                }
                #endregion

                #region Zombie hulk
                case BotType.ZombieBruiser:
                {
                    botInfo.AIType = BotAI.ZombieHulk;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 125,
                        CurrentHealth = 125,
                        MeleeDamageDealtModifier = 1.1f,
                        MeleeForceModifier = 1.4f,
                        RunSpeedModifier = 0.75f,
                        SprintSpeedModifier = 0.75f,
                        SizeModifier = 1.2f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 150,
                        CurrentHealth = 150,
                        ProjectileDamageDealtModifier = 5.0f,
                        ProjectileCritChanceDealtModifier = 5.0f,
                        MeleeDamageDealtModifier = 1.5f,
                        RunSpeedModifier = 0.5f,
                        SprintSpeedModifier = 0.5f,
                        SizeModifier = 0.95f,
                        InfiniteAmmo = 1,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Fritzliebe
                case BotType.Fritzliebe:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 200,
                        CurrentHealth = 200,
                        SizeModifier = 0.95f,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Funnyman
                case BotType.Funnyman:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 250,
                        CurrentHealth = 250,
                        SizeModifier = 1.05f,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Hacker
                case BotType.Hacker:
                {
                    botInfo.AIType = BotAI.Hacker;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 125,
                        CurrentHealth = 125,
                        EnergyConsumptionModifier = 0f,
                        RunSpeedModifier = 1.1f,
                        SprintSpeedModifier = 1.1f,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Incinerator
                case BotType.Incinerator:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 250,
                        CurrentHealth = 250,
                        FireDamageTakenModifier = 0.25f,
                        InfiniteAmmo = 1,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 250,
                        CurrentHealth = 250,
                        SizeModifier = 1.1f,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Kingpin
                case BotType.Kingpin:
                {
                    botInfo.AIType = BotAI.Hard;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 250,
                        CurrentHealth = 250,
                        SizeModifier = 1.05f,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Kriegbär
                case BotType.Kriegbär:
                {
                    botInfo.AIType = BotAI.Expert;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 350,
                        CurrentHealth = 350,
                        MaxEnergy = 350,
                        CurrentEnergy = 350,
                        FireDamageTakenModifier = 1.5f,
                        MeleeForceModifier = 1.75f,
                        RunSpeedModifier = 1.1f,
                        SprintSpeedModifier = 1.1f,
                        SizeModifier = 1.25f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 250,
                        CurrentHealth = 250,
                        MaxEnergy = 250,
                        CurrentEnergy = 250,
                        ProjectileDamageDealtModifier = 1.5f,
                        MeleeDamageDealtModifier = 1.5f,
                        MeleeForceModifier = 1.5f,
                        RunSpeedModifier = 1.15f,
                        SprintSpeedModifier = 1.15f,
                        SizeModifier = 1.1f,
                        InfiniteAmmo = 1,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Mecha
                case BotType.Mecha:
                {
                    botInfo.AIType = BotAI.Hulk;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 400,
                        CurrentHealth = 400,
                        ExplosionDamageTakenModifier = 0.2f, // 2000 (400 / 0.2)
                        ProjectileDamageTakenModifier = 0.5f, // 800
                        ImpactDamageTakenModifier = 0f,
                        MeleeForceModifier = 3f,
                        MeleeStunImmunity = 1,
                        CanBurn = 0,
                        RunSpeedModifier = 0.85f,
                        SprintSpeedModifier = 0.85f,
                        SizeModifier = 1.2f,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 110,
                        CurrentHealth = 110,
                        RunSpeedModifier = 1.1f,
                        SprintSpeedModifier = 1.1f,
                        SizeModifier = 0.95f,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Ninja
                case BotType.Ninja:
                {
                    botInfo.AIType = BotAI.Ninja;
                    botInfo.SearchItems = SearchItems.Melee;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 200,
                        CurrentHealth = 200,
                        MeleeDamageDealtModifier = 1.2f,
                        RunSpeedModifier = 1.5f,
                        SprintSpeedModifier = 1.5f,
                        SizeModifier = 0.9f,
                        EnergyRechargeModifier = 0.85f,
                        InfiniteAmmo = 1,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 400,
                        CurrentHealth = 400,
                        MaxEnergy = 400,
                        CurrentEnergy = 400,
                        MeleeDamageDealtModifier = 1.25f,
                        MeleeForceModifier = 2.0f,
                        RunSpeedModifier = 0.9f,
                        SprintSpeedModifier = 0.9f,
                        SizeModifier = 1.25f,
                    };
                    botInfo.IsBoss = true;
                    break;
                }
                #endregion

                #region Bosses Babybear
                case BotType.Babybear:
                {
                    botInfo.AIType = BotAI.Easy;
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 50,
                        CurrentHealth = 50,
                        RunSpeedModifier = 1.25f,
                        SprintSpeedModifier = 1.25f,
                        SizeModifier = 0.75f,
                    };
                    botInfo.IsBoss = true; // set IsBoss to spawn once
                    break;
                }
                #endregion

                #region Bosses Santa
                case BotType.Santa:
                {
                    botInfo.AIType = BotAI.Hard; // ChallengeA
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 200,
                        CurrentHealth = 200,
                        ExplosionDamageTakenModifier = 0.5f,
                        MeleeForceModifier = 1.5f,
                        SizeModifier = 1.1f,
                        InfiniteAmmo = 1,
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
                    botInfo.Modifiers = new PlayerModifiers()
                    {
                        MaxHealth = 200,
                        CurrentHealth = 200,
                        MeleeDamageDealtModifier = 1.05f,
                        RunSpeedModifier = 0.95f,
                        SprintSpeedModifier = 0.95f,
                        SizeModifier = 1.1f,
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
