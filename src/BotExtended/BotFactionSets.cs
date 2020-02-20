﻿using SFDGameScriptInterface;
using BotExtended.Factions;
using System.Collections.Generic;

namespace BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        private static List<BotType> CommonZombieTypes = new List<BotType>()
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
        private static List<BotType> MutatedZombieTypes = new List<BotType>()
        {
            BotType.ZombieBruiser,
            BotType.ZombieChild,
            BotType.ZombieFat,
            BotType.ZombieFlamer,
        };

        public static FactionSet GetFactionSet(BotFaction botFaction)
        {
            var factionSet = new FactionSet(botFaction);

            switch (botFaction)
            {
                #region Assassin
                case BotFaction.Assassin:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.AssassinMelee, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.AssassinRange, 1f),
                    });
                    break;
                }
                #endregion

                #region Agent
                case BotFaction.Agent:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Agent, 1f),
                    });
                    break;
                }
                #endregion

                #region Bandido
                case BotFaction.Bandido:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Bandido, 1f),
                    });
                    break;
                }
                #endregion

                #region Biker
                case BotFaction.Biker:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Biker, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Biker, 0.5f),
                        new SubFaction(BotType.Thug, 0.5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Biker, 0.6f),
                        new SubFaction(BotType.BikerHulk, 0.4f),
                    });
                    break;
                }
                #endregion

                #region Clown
                case BotFaction.Clown:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.ClownCowboy, 0.5f),
                        new SubFaction(BotType.ClownGangster, 0.25f),
                        new SubFaction(BotType.ClownBoxer, 0.25f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.ClownCowboy, 0.6f),
                        new SubFaction(BotType.ClownGangster, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.ClownBoxer, 0.7f),
                        new SubFaction(BotType.ClownGangster, 0.3f),
                    });
                    break;
                }
                #endregion

                #region Cowboy
                case BotFaction.Cowboy:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Cowboy, 1f),
                    });
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

                #region Marauder
                case BotFaction.Marauder:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(new List<BotType>()
                        {
                            BotType.MarauderBiker,
                            BotType.MarauderCrazy,
                            BotType.MarauderNaked,
                            BotType.MarauderRifleman,
                            BotType.MarauderRobber,
                            BotType.MarauderTough,
                        }, 1f),
                    });
                    break;
                }
                #endregion

                #region MetroCop
                case BotFaction.MetroCop:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.MetroCop, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.MetroCop, 0.7f),
                        new SubFaction(BotType.Agent2, 0.3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.MetroCop, 0.5f),
                        new SubFaction(BotType.Agent2, 0.5f),
                    });
                    break;
                }
                #endregion

                #region Police
                case BotFaction.Police:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Police, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Police, 0.7f),
                        new SubFaction(BotType.PoliceSWAT, 0.3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.PoliceSWAT, 0.8f),
                        new SubFaction(BotType.Police, 0.2f),
                    });
                    break;
                }
                #endregion

                #region PoliceSWAT
                case BotFaction.PoliceSWAT:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.PoliceSWAT, 1f),
                    });
                    break;
                }
                #endregion

                #region Sniper
                case BotFaction.Sniper:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Sniper, 1f),
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

                #region Thug
                case BotFaction.Thug:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Thug, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Thug, 0.5f),
                        new SubFaction(BotType.Biker, 0.5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
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

                #region ZombieHard
                case BotFaction.ZombieHard:
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

                #region Boss_Demolitionist
                case BotFaction.Boss_Demolitionist:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Demolitionist),
                    });
                    break;
                }
                #endregion

                #region Boss_Funnyman
                case BotFaction.Boss_Funnyman:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Funnyman),
                        new SubFaction(BotType.ClownBodyguard, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Funnyman),
                        new SubFaction(new List<BotType>()
                        {
                            BotType.ClownBoxer,
                            BotType.ClownCowboy,
                            BotType.ClownGangster,
                        }, 1f),
                    });
                    break;
                }
                #endregion

                #region Boss_Jo
                case BotFaction.Boss_Jo:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Jo),
                        new SubFaction(BotType.Biker, 0.6f),
                        new SubFaction(BotType.BikerHulk, 0.4f),
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

                #region Boss_Incinerator
                case BotFaction.Boss_Incinerator:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Incinerator),
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

                #region Boss_MadScientist
                case BotFaction.Boss_MadScientist:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Kriegbär),
                        new SubFaction(BotType.Fritzliebe),
                    });
                    break;
                }
                #endregion

                #region Boss_Meatgrinder
                case BotFaction.Boss_Meatgrinder:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Meatgrinder),
                    });
                    break;
                }
                #endregion

                #region Boss_Mecha
                case BotFaction.Boss_Mecha:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Mecha),
                    });
                    break;
                }
                #endregion

                #region Boss_MetroCop
                case BotFaction.Boss_MetroCop:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.MetroCop2),
                        new SubFaction(BotType.MetroCop, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.MetroCop2),
                        new SubFaction(BotType.MetroCop, 0.7f),
                        new SubFaction(BotType.Agent2, 0.3f),
                    });
                    break;
                }
                #endregion

                #region Boss_MirrorMan
                case BotFaction.Boss_MirrorMan:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.MirrorMan),
                    });
                    break;
                }
                #endregion

                #region Boss_Ninja
                case BotFaction.Boss_Ninja:
                {
                    factionSet.AddFaction(new List<SubFaction>()
                    {
                        new SubFaction(BotType.Ninja),
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
                    // TODO: uncomment
                    //factionSet.AddFaction(new List<SubFaction>()
                    //{
                    //    new SubFaction(BotType.Teddybear),
                    //});
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
