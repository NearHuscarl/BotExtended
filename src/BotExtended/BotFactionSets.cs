﻿using SFDGameScriptInterface;
using BotExtended.Factions;
using System.Collections.Generic;
using BotExtended.Library;

namespace BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        private static BotType[] CommonZombieTypes = new BotType[]
        {
            BotType.Zombie,
            BotType.ZombieAgent,
            BotType.ZombieFlamer,
            BotType.ZombieGangster,
            BotType.ZombieNinja,
            BotType.ZombiePolice,
            BotType.ZombieSoldier,
            BotType.ZombieThug,
            BotType.ZombieWorker,
        };
        private static BotType[] MutatedZombieTypes = new BotType[]
        {
            BotType.ZombieBruiser,
            BotType.ZombieChild,
            BotType.ZombieFat,
            BotType.ZombieFlamer,
        };

        public static FactionSet GetFactionSet(BotFaction botFaction)
        {
            var factionSet = new FactionSet(botFaction);
            var mainBoss = BotType.None;

            switch (botFaction)
            {
                #region Assassin
                case BotFaction.Assassin:
                {
                    factionSet.AddFaction(new SubFaction(BotType.AssassinMelee, 1f));
                    factionSet.AddFaction(new SubFaction(BotType.AssassinRange, 1f));
                    break;
                }
                #endregion

                #region Agent
                case BotFaction.Agent:
                {
                    factionSet.AddFaction(new SubFaction(BotType.Agent, 1f));
                    break;
                }
                #endregion

                #region Bandido
                case BotFaction.Bandido:
                {
                    // TODO: add bandido boss
                    factionSet.AddFaction(new SubFaction(BotType.Bandido, 1f));
                    break;
                }
                #endregion

                #region Biker
                case BotFaction.Boss_Jo:
                case BotFaction.Biker:
                {
                    if (botFaction == BotFaction.Boss_Jo) mainBoss = BotType.Jo;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Biker, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Biker, 0.5f),
                        new SubFaction(BotType.Thug, 0.5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Biker, 0.6f),
                        new SubFaction(BotType.BikerHulk, 0.4f),
                    });
                    break;
                }
                #endregion

                #region Clown
                case BotFaction.Boss_Funnyman:
                case BotFaction.Boss_Balloonatic:
                case BotFaction.Clown:
                {
                    if (botFaction == BotFaction.Boss_Funnyman) mainBoss = BotType.Funnyman;
                    if (botFaction == BotFaction.Boss_Balloonatic) mainBoss = BotType.Balloonatic;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.ClownCowboy, 0.5f),
                        new SubFaction(BotType.ClownGangster, 0.25f),
                        new SubFaction(BotType.ClownBoxer, 0.25f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.ClownCowboy, 0.6f),
                        new SubFaction(BotType.ClownGangster, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.ClownBoxer, 0.7f),
                        new SubFaction(BotType.ClownGangster, 0.3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.ClownBodyguard, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(new BotType[]
                        {
                            BotType.ClownBoxer,
                            BotType.ClownCowboy,
                            BotType.ClownGangster,
                        }, 1f),
                    });
                    break;
                }
                #endregion

                #region Cowboy
                case BotFaction.Boss_Sheriff:
                case BotFaction.Cowboy:
                {
                    if (botFaction == BotFaction.Boss_Sheriff) mainBoss = BotType.Sheriff;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Cowboy, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Cowboy, .7f),
                        new SubFaction(BotType.Bandido, .3f),
                    });
                    break;
                }
                #endregion

                #region Engineer
                case BotFaction.Engineer:
                {
                    factionSet.AddFaction(new SubFaction(BotType.Engineer, 1f));
                    break;
                }
                #endregion

                #region Farmer
                case BotFaction.Boss_Handler:
                case BotFaction.Farmer:
                {
                    if (botFaction == BotFaction.Boss_Handler) mainBoss = BotType.Handler;
                    var nonFarmer = new BotType[] { BotType.Gardener, BotType.Lumberjack, BotType.Hunter, };

                    if (botFaction == BotFaction.Farmer)
                        factionSet.AddFaction(new SubFaction(BotType.Farmer, 1f));
                    else
                    {
                        factionSet.AddFaction(new List<SubFaction>()
                        {
                            new SubFaction(mainBoss),
                            new SubFaction(BotType.Farmer, 0.5f),
                            new SubFaction(nonFarmer, .5f),
                        });
                        factionSet.AddFaction(new List<SubFaction>()
                        {
                            new SubFaction(mainBoss),
                            new SubFaction(BotType.Farmer, 0.3f),
                            new SubFaction(nonFarmer, .7f),
                        });
                    }
                    break;
                }
                #endregion

                #region Gangster
                case BotFaction.Gangster:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Gangster, 0.8f),
                        new SubFaction(BotType.GangsterHulk, 0.2f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Gangster, 0.7f),
                        new SubFaction(BotType.ThugHulk, 0.3f),
                    });
                    break;
                }
                #endregion

                #region Hunter
                case BotFaction.Hunter:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Hunter, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Hunter, .7f),
                        new SubFaction(BotType.Farmer, .3f),
                    });
                    break;
                }
                #endregion

                #region MetroCop
                case BotFaction.Boss_MetroCop:
                case BotFaction.MetroCop:
                {
                    if (botFaction == BotFaction.Boss_MetroCop) mainBoss = BotType.MetroCop2;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.MetroCop, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.MetroCop, 0.7f),
                        new SubFaction(BotType.Agent, 0.3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.MetroCop, 0.5f),
                        new SubFaction(BotType.Agent, 0.5f),
                    });
                    break;
                }
                #endregion

                #region Nazi
                case BotFaction.Boss_MadScientist:
                case BotFaction.Nazi:
                {
                    var bosses = new List<SubFaction>();
                    if (botFaction == BotFaction.Boss_MadScientist)
                    {
                        bosses.Add(new SubFaction(BotType.Kriegbar));
                        bosses.Add(new SubFaction(BotType.Fritzliebe));
                    }
                    factionSet.AddFaction(new List<SubFaction>(bosses));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.NaziSoldier, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.NaziSoldier, 0.6f),
                        new SubFaction(BotType.NaziMuscleSoldier, 0.4f),
                    });
                    break;
                }
                #endregion

                #region Police
                case BotFaction.Boss_Cindy:
                case BotFaction.Boss_PoliceChief:
                case BotFaction.Police:
                {
                    if (botFaction == BotFaction.Boss_Cindy) mainBoss = BotType.Cindy;
                    if (botFaction == BotFaction.Boss_PoliceChief) mainBoss = BotType.PoliceChief;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Police, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Police, 0.7f),
                        new SubFaction(BotType.PoliceSWAT, 0.3f),
                    });
                    break;
                }
                #endregion

                #region PoliceSWAT
                case BotFaction.Boss_Raze:
                case BotFaction.Boss_Smoker:
                case BotFaction.PoliceSWAT:
                {
                    if (botFaction == BotFaction.Boss_Raze) mainBoss = BotType.Raze;
                    if (botFaction == BotFaction.Boss_Smoker) mainBoss = BotType.Smoker;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.PoliceSWAT, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.PoliceSWAT, 0.8f),
                        new SubFaction(BotType.Police, 0.2f),
                    });
                    break;
                }
                #endregion

                #region Punk
                case BotFaction.Boss_Balista:
                case BotFaction.Punk:
                {
                    if (botFaction == BotFaction.Boss_Balista) mainBoss = BotType.Balista;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Punk, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Punk, 0.5f),
                        new SubFaction(BotType.Biker, 0.5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Punk, 0.6f),
                        new SubFaction(BotType.PunkHulk, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Punk, .75f),
                        new SubFaction(BotType.PunkHulk, .25f),
                    });
                    break;
                }
                #endregion

                #region Robot
                case BotFaction.Robot:
                {
                    factionSet.AddFaction(new SubFaction(BotType.Cyborg, 1f));
                    break;
                }
                #endregion

                #region Sniper
                case BotFaction.Sniper:
                {
                    factionSet.AddFaction(new SubFaction(BotType.Sniper, 1f));
                    break;
                }
                #endregion

                #region Spacer
                case BotFaction.Boss_Amos:
                case BotFaction.Boss_Reznor:
                case BotFaction.Spacer:
                {
                    if (botFaction == BotFaction.Boss_Amos) mainBoss = BotType.Amos;
                    if (botFaction == BotFaction.Boss_Reznor) mainBoss = BotType.Reznor;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Spacer, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Spacer, .7f),
                        new SubFaction(BotType.SpaceSniper, .3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Spacer, .9f),
                        new SubFaction(BotType.SpaceSniper, .1f),
                    });
                    break;
                }
                #endregion

                #region SpaceSniper
                case BotFaction.SpaceSniper:
                {
                    factionSet.AddFaction(new SubFaction(BotType.SpaceSniper, 1f));
                    break;
                }
                #endregion

                #region Stripper
                case BotFaction.Stripper:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.Bodyguard, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.BikerHulk, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.GangsterHulk, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.PunkHulk, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.ThugHulk, .5f),
                    });
                    break;
                }
                #endregion

                #region Soldier
                case BotFaction.Soldier:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Soldier, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Soldier, 0.7f),
                        new SubFaction(BotType.Sniper, 0.3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Soldier, 0.9f),
                        new SubFaction(BotType.Soldier2, 0.1f),
                    });
                    break;
                }
                #endregion

                #region Survivor
                case BotFaction.Survivor:
                {
                    factionSet.AddFaction(new SubFaction(new BotType[]
                    {
                        BotType.SurvivorBiker,
                        BotType.SurvivorCrazy,
                        BotType.SurvivorNaked,
                        BotType.SurvivorRifleman,
                        BotType.SurvivorRobber,
                        BotType.SurvivorTough,
                    }, 1f));
                    break;
                }
                #endregion

                #region Thug
                case BotFaction.Boss_Bobby:
                case BotFaction.Thug:
                {
                    if (botFaction == BotFaction.Boss_Bobby) mainBoss = BotType.Bobby;
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Thug, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Thug, 0.5f),
                        new SubFaction(BotType.Biker, 0.5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(mainBoss),
                        new SubFaction(BotType.Thug, 0.6f),
                        new SubFaction(BotType.ThugHulk, 0.4f),
                    });
                    break;
                }
                #endregion

                #region Zombie
                case BotFaction.Zombie:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Zombie, 0.4f),
                        new SubFaction(CommonZombieTypes, 0.6f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.8f),
                        new SubFaction(BotType.ZombieBruiser, 0.2f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.6f),
                        new SubFaction(BotType.ZombieBruiser, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.8f),
                        new SubFaction(BotType.ZombieChild, 0.2f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.6f),
                        new SubFaction(BotType.ZombieChild, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.8f),
                        new SubFaction(BotType.ZombieFat, 0.2f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.6f),
                        new SubFaction(BotType.ZombieFat, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.8f),
                        new SubFaction(BotType.ZombieFlamer, 0.2f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.6f),
                        new SubFaction(BotType.ZombieFlamer, 0.4f),
                    });
                    break;
                }
                #endregion

                #region ZombieMutated
                case BotFaction.ZombieMutated:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(MutatedZombieTypes, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.2f),
                        new SubFaction(MutatedZombieTypes, 0.8f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.4f),
                        new SubFaction(MutatedZombieTypes, 0.6f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(CommonZombieTypes, 0.7f),
                        new SubFaction(MutatedZombieTypes, 0.3f),
                    });
                    break;
                }
                #endregion

                #region Boss_Standalone

                case BotFaction.Boss_Demolitionist:
                    factionSet.AddFaction(new SubFaction(BotType.Demolitionist));
                    break;
                case BotFaction.Boss_Hitman:
                    factionSet.AddFaction(new SubFaction(BotType.Hitman));
                    break;
                case BotFaction.Boss_Incinerator:
                    factionSet.AddFaction(new SubFaction(BotType.Incinerator));
                    break;
                case BotFaction.Boss_Meatgrinder:
                    factionSet.AddFaction(new SubFaction(BotType.Meatgrinder));
                    break;
                case BotFaction.Boss_Mecha:
                    factionSet.AddFaction(new SubFaction(BotType.Mecha));
                    break;
                case BotFaction.Boss_MirrorMan:
                    factionSet.AddFaction(new SubFaction(BotType.MirrorMan));
                    break;
                case BotFaction.Boss_Ninja:
                    factionSet.AddFaction(new SubFaction(BotType.Ninja));
                    break;
                case BotFaction.Boss_Rambo:
                    factionSet.AddFaction(new SubFaction(BotType.Rambo));
                    break;

                #endregion

                #region Boss_Boffin
                case BotFaction.Boss_Boffin:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Boffin),
                        new SubFaction(BotType.Scientist, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Boffin),
                        new SubFaction(BotType.LabAssistant, .5f),
                        new SubFaction(BotType.Scientist, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Boffin),
                        new SubFaction(BotType.Scientist, .75f),
                        new SubFaction(BotType.LabAssistant, .25f),
                    });
                    break;
                }
                #endregion

                #region Boss_Hacker
                case BotFaction.Boss_Hacker:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Hacker),
                        new SubFaction(BotType.Hacker),
                    });
                    break;
                }
                #endregion

                #region Boss_Kingpin
                case BotFaction.Boss_Kingpin:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Kingpin),
                        new SubFaction(BotType.Bodyguard, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Kingpin),
                        new SubFaction(BotType.GangsterHulk, 0.55f),
                        new SubFaction(BotType.Bodyguard2, 0.45f),
                    });
                    break;
                }
                #endregion
                
                #region Boss_Santa
                case BotFaction.Boss_Santa:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Santa),
                        new SubFaction(BotType.Elf, 1f),
                    });
                    break;
                }
                #endregion

                #region Boss_Teddybear
                case BotFaction.Boss_Teddybear:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Teddybear),
                        new SubFaction(BotType.Babybear),
                        new SubFaction(BotType.Babybear),
                    });
                    break;
                }
                #endregion

                #region Boss_Zombie
                case BotFaction.Boss_Zombie:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.ZombieFighter),
                        new SubFaction(CommonZombieTypes, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.ZombieFighter),
                        new SubFaction(CommonZombieTypes, 0.7f),
                        new SubFaction(MutatedZombieTypes, 0.3f),
                    });
                    break;
                }
                #endregion
            }

            return factionSet;
        }
    }
}
