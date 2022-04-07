using SFDGameScriptInterface;
using BotExtended.Factions;
using System.Collections.Generic;
using BotExtended.Library;

namespace BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        public static BotType[] CommonZombieTypes = new BotType[]
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
        public static BotType[] MutatedZombieTypes = new BotType[]
        {
            BotType.ZombieBruiser,
            BotType.ZombieChild,
            BotType.ZombieFat,
            BotType.ZombieFlamer,
        };

        public static FactionSet GetFactionSet(BotFaction botFaction)
        {
            var factionSet = new FactionSet(botFaction);
            var bosses = new List<SubFaction>();

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
                case BotFaction.Boss_Agent79:
                case BotFaction.Boss_President:
                case BotFaction.Agent:
                {
                    if (botFaction == BotFaction.Boss_Agent79) bosses.Add(new SubFaction(BotType.Agent79));
                    if (botFaction == BotFaction.Boss_President) bosses.Add(new SubFaction(BotType.President));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Agent, 1f),
                    });
                    break;
                }
                #endregion

                #region Bandido
                case BotFaction.Bandido:
                {
                    factionSet.AddFaction(new SubFaction(BotType.Bandido, 1f));
                    break;
                }
                #endregion

                #region Biker
                case BotFaction.Boss_Jo:
                case BotFaction.Biker:
                {
                    if (botFaction == BotFaction.Boss_Jo) bosses.Add(new SubFaction(BotType.Jo));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Biker, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Biker, 0.7f),
                        new SubFaction(BotType.BikerHulk, 0.3f),
                    });
                    break;
                }
                #endregion

                #region Clown
                case BotFaction.Boss_Funnyman:
                case BotFaction.Boss_Balloonatic:
                case BotFaction.Clown:
                {
                    if (botFaction == BotFaction.Boss_Funnyman) bosses.Add(new SubFaction(BotType.Funnyman));
                    if (botFaction == BotFaction.Boss_Balloonatic) bosses.Add(new SubFaction(BotType.Balloonatic));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.ClownCowboy, 0.5f),
                        new SubFaction(BotType.ClownGangster, 0.25f),
                        new SubFaction(BotType.ClownBoxer, 0.25f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.ClownCowboy, 0.6f),
                        new SubFaction(BotType.ClownGangster, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.ClownBoxer, 0.7f),
                        new SubFaction(BotType.ClownGangster, 0.3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.ClownBodyguard, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
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
                    if (botFaction == BotFaction.Boss_Sheriff) bosses.Add(new SubFaction(BotType.Sheriff));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Cowboy, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
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
                    if (botFaction == BotFaction.Boss_Handler) bosses.Add(new SubFaction(BotType.Handler));
                    var nonFarmer = new BotType[] { BotType.Gardener, BotType.Lumberjack, BotType.Hunter, };

                    if (botFaction == BotFaction.Farmer)
                        factionSet.AddFaction(new SubFaction(BotType.Farmer, 1f));
                    else
                    {
                        factionSet.AddFaction(new List<SubFaction>(bosses)
                        {
                            new SubFaction(BotType.Farmer, 0.5f),
                            new SubFaction(nonFarmer, .5f),
                        });
                        factionSet.AddFaction(new List<SubFaction>(bosses)
                        {
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
                    if (botFaction == BotFaction.Boss_MetroCop) bosses.Add(new SubFaction(BotType.MetroCop2));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.MetroCop, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.MetroCop, 0.7f),
                        new SubFaction(BotType.Agent, 0.3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.MetroCop, 0.5f),
                        new SubFaction(BotType.Agent, 0.5f),
                    });
                    break;
                }
                #endregion

                #region Mutant
                case BotFaction.Boss_BigMutant:
                case BotFaction.Mutant:
                {
                    if (botFaction == BotFaction.Boss_BigMutant) bosses.Add(new SubFaction(BotType.BigMutant));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Mutant, 1f),
                    });
                    break;
                }
                #endregion

                #region Nazi
                case BotFaction.Boss_MadScientist:
                case BotFaction.Nazi:
                {
                    if (botFaction == BotFaction.Boss_MadScientist) bosses.Add(new SubFaction(BotType.Fritzliebe));
                    // TODO: add SSOfficer
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.NaziSoldier, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.NaziSoldier, 0.6f),
                        new SubFaction(BotType.NaziHulk, 0.4f),
                    });
                    break;
                }
                #endregion

                #region Police
                case BotFaction.Boss_Cindy:
                case BotFaction.Boss_PoliceChief:
                case BotFaction.Police:
                {
                    if (botFaction == BotFaction.Boss_Cindy) bosses.Add(new SubFaction(BotType.Cindy));
                    if (botFaction == BotFaction.Boss_PoliceChief) bosses.Add(new SubFaction(BotType.PoliceChief));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Police, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
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
                    if (botFaction == BotFaction.Boss_Raze) bosses.Add(new SubFaction(BotType.Raze));
                    if (botFaction == BotFaction.Boss_Smoker) bosses.Add(new SubFaction(BotType.Smoker));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.PoliceSWAT, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.PoliceSWAT, 0.8f),
                        new SubFaction(BotType.Police, 0.2f),
                    });
                    break;
                }
                #endregion

                #region Punk
                case BotFaction.Boss_Balista:
                case BotFaction.Boss_Firebug:
                case BotFaction.Boss_Quillhogg:
                case BotFaction.Punk:
                {
                    if (botFaction == BotFaction.Boss_Balista) bosses.Add(new SubFaction(BotType.Balista));
                    if (botFaction == BotFaction.Boss_Firebug) bosses.Add(new SubFaction(BotType.Firebug));
                    if (botFaction == BotFaction.Boss_Quillhogg) bosses.Add(new SubFaction(BotType.Quillhogg));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Punk, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Punk, 0.5f),
                        new SubFaction(BotType.Biker, 0.5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Punk, 0.6f),
                        new SubFaction(BotType.PunkHulk, 0.4f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Punk, .75f),
                        new SubFaction(BotType.PunkHulk, .25f),
                    });
                    break;
                }
                #endregion

                #region Pyromaniac
                case BotFaction.Boss_Incinerator:
                case BotFaction.Boss_Fireman:
                case BotFaction.Pyromaniac:
                {
                    if (botFaction == BotFaction.Pyromaniac)
                    {
                        factionSet.AddFaction(new List<SubFaction>()
                        {
                            new SubFaction(BotType.Pyromaniac),
                            new SubFaction(BotType.Pyromaniac),
                            new SubFaction(BotType.Pyromaniac),
                        });
                        break;
                    }

                    if (botFaction == BotFaction.Boss_Incinerator) bosses.Add(new SubFaction(BotType.Incinerator));
                    if (botFaction == BotFaction.Boss_Fireman) bosses.Add(new SubFaction(BotType.Fireman));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Pyromaniac),
                        new SubFaction(BotType.Pyromaniac),
                    });
                    break;
                }
                #endregion

                #region Robot
                case BotFaction.Robot:
                {
                    // TODO: add custom profiles
                    factionSet.AddFaction(new SubFaction(BotType.Cyborg, 1f));
                    break;
                }
                #endregion

                #region Scientist
                case BotFaction.Boss_Boffin:
                case BotFaction.Boss_Kriegbar:
                case BotFaction.Scientist:
                {
                    if (botFaction == BotFaction.Boss_Boffin) bosses.Add(new SubFaction(BotType.Boffin));
                    if (botFaction == BotFaction.Boss_Kriegbar) bosses.Add(new SubFaction(BotType.Kriegbar));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Scientist, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.LabAssistant, .5f),
                        new SubFaction(BotType.Scientist, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Scientist, .75f),
                        new SubFaction(BotType.LabAssistant, .25f),
                    });
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
                    if (botFaction == BotFaction.Boss_Amos) bosses.Add(new SubFaction(BotType.Amos));
                    if (botFaction == BotFaction.Boss_Reznor) bosses.Add(new SubFaction(BotType.Reznor));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Spacer, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Spacer, .7f),
                        new SubFaction(BotType.SpaceSniper, .3f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
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
                case BotFaction.Boss_Chairman:
                case BotFaction.Stripper:
                {
                    if (botFaction == BotFaction.Boss_Chairman) bosses.Add(new SubFaction(BotType.Chairman));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.Bodyguard, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.BikerHulk, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.GangsterHulk, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.PunkHulk, .5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Stripper, .5f),
                        new SubFaction(BotType.ThugHulk, .5f),
                    });
                    break;
                }
                #endregion

                #region Soldier
                case BotFaction.Boss_Nadja:
                case BotFaction.Boss_BazookaJane:
                case BotFaction.Boss_HeavySoldier:
                case BotFaction.Soldier:
                {
                    if (botFaction == BotFaction.Boss_BazookaJane) bosses.Add(new SubFaction(BotType.BazookaJane));
                    if (botFaction == BotFaction.Boss_Nadja) bosses.Add(new SubFaction(BotType.Nadja));
                    if (botFaction == BotFaction.Boss_HeavySoldier) bosses.Add(new SubFaction(BotType.HeavySoldier));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Soldier, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Soldier, 0.7f),
                        new SubFaction(BotType.Sniper, 0.3f),
                    });
                    break;
                }
                #endregion

                #region Survivor
                case BotFaction.Survivor:
                {
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Survivor, 1f),
                    });
                    break;
                }
                #endregion

                #region Thug
                case BotFaction.Boss_Beast:
                case BotFaction.Boss_Bobby:
                case BotFaction.Thug:
                {
                    if (botFaction == BotFaction.Boss_Beast) bosses.Add(new SubFaction(BotType.Beast));
                    if (botFaction == BotFaction.Boss_Bobby) bosses.Add(new SubFaction(BotType.Bobby));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Thug, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(BotType.Thug, 0.5f),
                        new SubFaction(BotType.Biker, 0.5f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
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
                case BotFaction.Boss_Survivalist:
                    factionSet.AddFaction(new SubFaction(BotType.Survivalist));
                    break;
                case BotFaction.Boss_Napoleon:
                    factionSet.AddFaction(new SubFaction(BotType.Napoleon));
                    break;

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
                case BotFaction.Boss_ZombieEater:
                case BotFaction.Boss_ZombieFighter:
                {
                    if (botFaction == BotFaction.Boss_ZombieEater) bosses.Add(new SubFaction(BotType.ZombieEater));
                    if (botFaction == BotFaction.Boss_ZombieFighter) bosses.Add(new SubFaction(BotType.ZombieFighter));
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
                        new SubFaction(CommonZombieTypes, 1f),
                    });
                    factionSet.AddFaction(new List<SubFaction>(bosses)
                    {
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
