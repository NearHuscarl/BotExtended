using System.Collections.Generic;
using SFDGameScriptInterface;

namespace SFDScript.BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        public static List<WeaponSet> GetWeapons(BotType botType)
        {
            var weapons = new List<WeaponSet>();

            switch (botType)
            {
                #region Agent
                case BotType.Agent:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region Agent2
                case BotType.Agent2:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MAGNUM,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                        Secondary = WeaponItem.UZI,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.DARK_SHOTGUN,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region AssassinMelee
                case BotType.AssassinMelee:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KATANA,
                    });
                    break;
                }
                #endregion

                #region AssassinRange
                case BotType.AssassinRange:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.UZI,
                    });
                    break;
                }
                #endregion

                #region Bandido
                case BotType.Bandido:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.MACHETE,
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.CARBINE,
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.SHOTGUN,
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region Biker
                case BotType.Biker:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAIN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                    });
                    break;
                }
                #endregion

                #region BikerHulk
                case BotType.BikerHulk:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region Bodyguard
                case BotType.Bodyguard:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region Bodyguard2
                case BotType.Bodyguard2:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.TOMMYGUN,
                    });
                    break;
                }
                #endregion

                #region ClownBodyguard
                case BotType.ClownBodyguard:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KATANA,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BAT,
                    });
                    break;
                }
                #endregion

                #region ClownBoxer
                case BotType.ClownBoxer:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region ClownCowboy
                case BotType.ClownCowboy:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    break;
                }
                #endregion

                #region ClownGangster
                case BotType.ClownGangster:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.TOMMYGUN,
                    });
                    break;
                }
                #endregion

                #region Cowboy
                case BotType.Cowboy:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SAWED_OFF,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MAGNUM,
                    });
                    break;
                }
                #endregion

                #region Demolitionist
                case BotType.Demolitionist:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SNIPER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                    });
                    break;
                }
                #endregion

                #region Elf
                case BotType.Elf:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAIN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.MP50,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.FLAMETHROWER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.UZI,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.FLAREGUN,
                    });
                    break;
                }
                #endregion

                #region Fritzliebe
                case BotType.Fritzliebe:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region Funnyman
                case BotType.Funnyman:
                {
                    weapons.Add(WeaponSet.Empty);
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.TOMMYGUN,
                    });
                    break;
                }
                #endregion

                #region Jo
                case BotType.Jo:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BOTTLE,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Hacker
                case BotType.Hacker:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region Gangster
                case BotType.Gangster:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BAT,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BOTTLE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.UZI,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SAWED_OFF,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.MP50,
                    });
                    break;
                }
                #endregion

                #region GangsterHulk
                case BotType.GangsterHulk:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region Incinerator
                case BotType.Incinerator:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                        Primary = WeaponItem.FLAMETHROWER,
                        Secondary = WeaponItem.FLAREGUN,
                        Throwable = WeaponItem.MOLOTOVS,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Kingpin
                case BotType.Kingpin:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.TOMMYGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MAGNUM,
                    });
                    break;
                }
                #endregion

                #region Kriegbär
                case BotType.Kriegbär:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region MarauderBiker
                case BotType.MarauderBiker:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SMG,
                    });
                    break;
                }
                #endregion

                #region MarauderCrazy
                case BotType.MarauderCrazy:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                    });
                    break;
                }
                #endregion

                #region MarauderNaked
                case BotType.MarauderNaked:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.MACHETE,
                    });
                    break;
                }
                #endregion

                #region MarauderRifleman
                case BotType.MarauderRifleman:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SAWED_OFF,
                    });
                    break;
                }
                #endregion

                #region MarauderRobber
                case BotType.MarauderRobber:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    break;
                }
                #endregion

                #region MarauderTough
                case BotType.MarauderTough:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    break;
                }
                #endregion

                #region Meatgrinder
                case BotType.Meatgrinder:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAINSAW,
                        Throwable = WeaponItem.MOLOTOVS,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Mecha
                case BotType.Mecha:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region MetroCop
                case BotType.MetroCop:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                        Primary = WeaponItem.SMG,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                        Primary = WeaponItem.DARK_SHOTGUN,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.ASSAULT,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.DARK_SHOTGUN,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SMG,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOCK_BATON,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region MetroCop2
                case BotType.MetroCop2:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                        Secondary = WeaponItem.PISTOL,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region Mutant
                case BotType.Mutant:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region NaziLabAssistant
                case BotType.NaziLabAssistant:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Powerup = WeaponItem.STRENGTHBOOST,
                    });
                    break;
                }
                #endregion

                #region NaziMuscleSoldier
                case BotType.NaziMuscleSoldier:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region NaziScientist
                case BotType.NaziScientist:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAIR,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BOTTLE,
                    });
                    break;
                }
                #endregion

                #region NaziSoldier
                case BotType.NaziSoldier:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.MP50,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.MP50,
                        Throwable = WeaponItem.GRENADES,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.MP50,
                        Throwable = WeaponItem.GRENADES,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.CARBINE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.CARBINE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.CARBINE,
                        Throwable = WeaponItem.GRENADES,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region SSOfficer
                case BotType.SSOfficer:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.MP50,
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region Ninja
                case BotType.Ninja:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KATANA,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Police
                case BotType.Police:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                        Primary = WeaponItem.SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                        Secondary = WeaponItem.REVOLVER,
                    });
                    break;
                }
                #endregion

                #region PoliceSWAT
                case BotType.PoliceSWAT:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Secondary = WeaponItem.PISTOL45,
                        Throwable = WeaponItem.C4,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Secondary = WeaponItem.MACHINE_PISTOL,
                        Throwable = WeaponItem.GRENADES,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.ASSAULT,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.SMG,
                    });
                    break;
                }
                #endregion

                #region Santa
                case BotType.Santa:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.M60,
                        Secondary = WeaponItem.UZI,
                    });
                    break;
                }
                #endregion

                #region Sniper
                case BotType.Sniper:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.SNIPER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SNIPER,
                        Secondary = WeaponItem.SILENCEDPISTOL,
                    });
                    break;
                }
                #endregion

                #region Soldier
                case BotType.Soldier:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.ASSAULT,
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SMG,
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region Soldier2
                case BotType.Soldier2:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region Teddybear
                case BotType.Teddybear:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Throwable = WeaponItem.GRENADES,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Babybear
                case BotType.Babybear:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region Thug
                case BotType.Thug:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BAT,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.HAMMER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAIN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MACHINE_PISTOL,
                    });
                    break;
                }
                #endregion

                #region ThugHulk
                case BotType.ThugHulk:
                {
                    weapons.Add(WeaponSet.Empty);
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.PIPE,
                    });
                    break;
                }
                #endregion

                #region Zombies
                case BotType.Zombie:
                case BotType.ZombieBruiser:
                case BotType.ZombieChild:
                case BotType.ZombieFat:
                case BotType.ZombieFlamer:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region ZombieAgent
                case BotType.ZombieAgent:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.SILENCEDPISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.SILENCEDUZI,
                    });
                    break;
                }
                #endregion

                #region ZombieFighter
                case BotType.ZombieFighter:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region ZombieGangster
                case BotType.ZombieGangster:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.TOMMYGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region ZombieNinja
                case BotType.ZombieNinja:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KATANA,
                    });
                    break;
                }
                #endregion

                #region ZombiePolice
                case BotType.ZombiePolice:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.REVOLVER,
                    });
                    break;
                }
                #endregion

                #region ZombiePrussian
                case BotType.ZombiePrussian:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.CARBINE,
                        Throwable = WeaponItem.GRENADES,
                    });
                    break;
                }
                #endregion

                #region BaronVonHauptstein
                case BotType.BaronVonHauptstein:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Secondary = WeaponItem.REVOLVER,
                        Throwable = WeaponItem.GRENADES,
                    });
                    break;
                }
                #endregion

                #region ZombieSoldier
                case BotType.ZombieSoldier:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SMG,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.ASSAULT,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Throwable = WeaponItem.GRENADES,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Throwable = WeaponItem.MINES,
                    });
                    break;
                }
                #endregion

                #region ZombieThug
                case BotType.ZombieThug:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BAT,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Throwable = WeaponItem.MOLOTOVS,
                    });
                    break;
                }
                #endregion

                #region ZombieWorker
                case BotType.ZombieWorker:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.PIPE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.HAMMER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAINSAW,
                    });
                    break;
                }
                #endregion
            }

            return weapons;
        }
    }
}
