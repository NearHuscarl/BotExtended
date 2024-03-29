﻿using System.Collections.Generic;
using BotExtended.Powerups;
using SFDGameScriptInterface;

namespace BotExtended
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
                        SecondaryPowerup = RangedWeaponPowerup.Poison,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Poison,
                        UseLazer = true,
                    });
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
                        SecondaryPowerup = RangedWeaponPowerup.Poison,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                        Secondary = WeaponItem.UZI,
                        SecondaryPowerup = RangedWeaponPowerup.Poison,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.DARK_SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Poison,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region Agent79
                case BotType.Agent79:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.BAZOOKA,
                        PrimaryPowerup = RangedWeaponPowerup.Object,
                        Secondary = WeaponItem.PISTOL,
                        Throwable = WeaponItem.C4,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Amos
                case BotType.Amos:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.DARK_SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Gauss,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Gauss,
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

                #region AssKicker
                case BotType.AssKicker:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                    });
                    break;
                }
                #endregion

                #region Balista
                case BotType.Balista:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAIN,
                        Primary = WeaponItem.ASSAULT,
                        Secondary = WeaponItem.UZI,
                    });
                    break;
                }
                #endregion

                #region Balloonatic
                case BotType.Balloonatic:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.SMG,
                        PrimaryPowerup = RangedWeaponPowerup.Helium,
                        Secondary = WeaponItem.UZI,
                        SecondaryPowerup = RangedWeaponPowerup.Helium,
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

                #region BazookaJane
                case BotType.BazookaJane:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.BAZOOKA,
                        PrimaryPowerup = RangedWeaponPowerup.SuicideFighter,
                        Secondary = WeaponItem.PISTOL,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Beast
                case BotType.Beast:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.MACHETE,
                        MeleePowerup = MeleeWeaponPowerup.Earthquake,
                        Throwable = WeaponItem.C4,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Berserker
                case BotType.Berserker:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                        MeleePowerup = MeleeWeaponPowerup.Gib,
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
                    weapons.Add(new WeaponSet()
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
                    });
                    break;
                }
                #endregion

                #region Bobby
                case BotType.Bobby:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Scattershot,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.SAWED_OFF,
                        PrimaryPowerup = RangedWeaponPowerup.Scattershot,
                    });
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

                #region Boffin
                case BotType.Boffin:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.Shrinking,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Chairman
                case BotType.Chairman:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAIR,
                        MeleeHandPowerup = MeleeWeaponPowerup.Pushback,
                        Throwable = WeaponItem.C4,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region Cindy
                case BotType.Cindy:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Stun,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                        Primary = WeaponItem.ASSAULT,
                        PrimaryPowerup = RangedWeaponPowerup.Stun,
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
                        SecondaryPowerup = RangedWeaponPowerup.Blast,
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
                        PrimaryPowerup = RangedWeaponPowerup.Blast,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Blast,
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

                #region Cyborg
                case BotType.Cyborg:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MACHINE_PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Homing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Homing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL45,
                        SecondaryPowerup = RangedWeaponPowerup.Homing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.DARK_SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Homing,
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

                #region Demoman
                case BotType.Demoman:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.Mine,
                        Melee = WeaponItem.MACHETE,
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

                #region Engineer
                case BotType.Engineer:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
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
                        Melee = WeaponItem.LEAD_PIPE,
                        Primary = WeaponItem.SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.PIPE,
                        Primary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region Farmer
                case BotType.Farmer:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SAWED_OFF,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                    });
                    break;
                }
                #endregion

                #region Firebug
                case BotType.Firebug:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.PIPE,
                        Secondary = WeaponItem.MACHINE_PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Fire,
                    });
                    break;
                }
                #endregion

                #region Fireman
                case BotType.Fireman:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                        MeleePowerup = MeleeWeaponPowerup.FireTrail,
                        Throwable = WeaponItem.MOLOTOVS,
                    });
                    break;
                }
                #endregion

                #region Fritzliebe
                case BotType.Fritzliebe:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MAGNUM,
                        SecondaryPowerup = RangedWeaponPowerup.Lightning,
                    });
                    break;
                }
                #endregion

                #region Funnyman
                case BotType.Funnyman:
                {
                    weapons.Add(new WeaponSet()
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.Megaton,
                        Primary = WeaponItem.TOMMYGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.Megaton,
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
                        MeleeHandPowerup = MeleeWeaponPowerup.Hurling,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
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
                    weapons.Add(new WeaponSet()
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
                    });
                    break;
                }
                #endregion

                #region Gardener
                case BotType.Gardener:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.TEAPOT,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
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

                #region Handler
                case BotType.Handler:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.WHIP,
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.SuicideDove,
                    });
                    break;
                }
                #endregion

                #region Hawkeye
                case BotType.Hawkeye:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SNIPER,
                        PrimaryPowerup = RangedWeaponPowerup.Penetration,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region Hunter
                case BotType.Hunter:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.BOW,
                        PrimaryPowerup = RangedWeaponPowerup.Bow,
                        //PrimaryPowerup = TODO: add Bleeding powerup
                    });
                    break;
                }
                #endregion

                #region Hitman
                case BotType.Hitman:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.ASSAULT,
                        PrimaryPowerup = RangedWeaponPowerup.Precision,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.DARK_SHOTGUN,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.SILENCEDPISTOL,
                        PrimaryPowerup = RangedWeaponPowerup.Precision,
                    });
                    break;
                }
                #endregion

                #region Huntsman
                case BotType.Huntsman:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SNIPER,
                        PrimaryPowerup = RangedWeaponPowerup.Trigger,
                        Secondary = WeaponItem.MACHINE_PISTOL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MAGNUM,
                        SecondaryPowerup = RangedWeaponPowerup.Trigger,
                    });
                    break;
                }
                #endregion

                #region Incinerator
                case BotType.Incinerator:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.Molotov,
                        Throwable = WeaponItem.MOLOTOVS,
                    });
                    break;
                }
                #endregion

                #region Ion
                case BotType.Ion:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SNIPER,
                        PrimaryPowerup = RangedWeaponPowerup.BouncingLaser,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.MAGNUM,
                        PrimaryPowerup = RangedWeaponPowerup.BouncingLaser,
                    });
                    break;
                }
                #endregion

                #region Kingpin
                case BotType.Kingpin:
                {
                    weapons.Add(new WeaponSet
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.GroundSlam,
                    });
                    break;
                }
                #endregion

                #region Kriegbär
                case BotType.Kriegbar:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Powerup = WeaponItem.SLOWMO_10,
                    });
                    break;
                }
                #endregion

                #region LabAssistant
                case BotType.LabAssistant:
                {
                    weapons.Add(WeaponSet.Empty);
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.PreciseBouncing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.PreciseBouncing,
                        Powerup = WeaponItem.STRENGTHBOOST,
                    });
                    break;
                }
                #endregion

                #region Lumberjack
                case BotType.Lumberjack:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CHAINSAW,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                        MeleePowerup = MeleeWeaponPowerup.Gib,
                        Powerup = WeaponItem.STRENGTHBOOST,
                    });
                    break;
                }
                #endregion

                #region LordPinkerton
                case BotType.LordPinkerton:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.BAZOOKA,
                        PrimaryPowerup = RangedWeaponPowerup.Riding,
                        Secondary = WeaponItem.SILENCEDUZI,
                        Throwable = WeaponItem.C4,
                        Powerup = WeaponItem.STRENGTHBOOST,
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
                        Primary = WeaponItem.DARK_SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Grapeshot,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region Monk
                case BotType.Monk:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.CUESTICK,
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

                #region BigMutant
                case BotType.BigMutant:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.AXE,
                        MeleePowerup = MeleeWeaponPowerup.Splitting,
                    });
                    break;
                }
                #endregion

                #region Nadja
                case BotType.Nadja:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Secondary = WeaponItem.PISTOL,
                        Powerup = WeaponItem.SLOWMO_10,
                        Throwable = WeaponItem.MINES,
                    });
                    break;
                }
                #endregion

                #region Napoleon
                case BotType.Napoleon:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.Shrapnel,
                        Secondary = WeaponItem.UZI,
                        SecondaryPowerup = RangedWeaponPowerup.Minigun,
                        Melee = WeaponItem.BATON,
                        Throwable = WeaponItem.GRENADES,
                        Powerup = WeaponItem.SLOWMO_10,
                    });
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

                #region NaziHulk
                case BotType.NaziHulk:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
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
                        SecondaryPowerup = RangedWeaponPowerup.Taser,
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
                        SecondaryPowerup = RangedWeaponPowerup.Taser,
                    });
                    break;
                }
                #endregion

                #region PoliceChief
                case BotType.PoliceChief:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BATON,
                        Primary = WeaponItem.SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Fatigue,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Fatigue,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                        Primary = WeaponItem.SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Fatigue,
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
                        SecondaryPowerup = RangedWeaponPowerup.Termite,
                        Throwable = WeaponItem.C4,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Secondary = WeaponItem.MACHINE_PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Termite,
                        Throwable = WeaponItem.GRENADES,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.ASSAULT,
                        PrimaryPowerup = RangedWeaponPowerup.Termite,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.SMG,
                        PrimaryPowerup = RangedWeaponPowerup.Termite,
                    });
                    break;
                }
                #endregion

                #region President
                case BotType.President:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.FLAGPOLE,
                    });
                    break;
                }
                #endregion

                #region Punk
                case BotType.Punk:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BAT,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Knockback,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Secondary = WeaponItem.UZI,
                        SecondaryPowerup = RangedWeaponPowerup.Knockback,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.BASEBALL,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL45,
                        SecondaryPowerup = RangedWeaponPowerup.Knockback,
                    });
                    break;
                }
                #endregion

                #region PunkHulk
                case BotType.PunkHulk:
                {
                    weapons.Add(new WeaponSet()
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
                    });
                    break;
                }
                #endregion

                #region Pyromaniac
                case BotType.Pyromaniac:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.FLAMETHROWER,
                        Throwable = WeaponItem.MOLOTOVS,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.FLAREGUN,
                        SecondaryPowerup = RangedWeaponPowerup.InfiniteBouncing,
                        Throwable = WeaponItem.MOLOTOVS,
                    });
                    break;
                }
                #endregion

                #region Queen
                case BotType.Queen:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.PILLOW,
                    });
                    break;
                }
                #endregion

                #region Quillhogg
                case BotType.Quillhogg:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.Dormant,
                        Throwable = WeaponItem.GRENADES,
                    });
                    break;
                }
                #endregion

                #region Raze
                case BotType.Raze:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.StickyBomb,
                        Secondary = WeaponItem.PISTOL45,
                        SecondaryPowerup = RangedWeaponPowerup.Termite,
                        Throwable = WeaponItem.C4,
                    });
                    break;
                }
                #endregion
                
                #region Rambo
                case BotType.Rambo:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.M60,
                        PrimaryPowerup = RangedWeaponPowerup.Minigun,
                    });
                    break;
                }
                #endregion

                #region Reznor
                case BotType.Reznor:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.BAZOOKA,
                        PrimaryPowerup = RangedWeaponPowerup.Blackhole,
                        Secondary = WeaponItem.PISTOL45,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.Blackhole,
                        Secondary = WeaponItem.PISTOL45,
                        SecondaryPowerup = RangedWeaponPowerup.Blackhole,
                        UseLazer = true,
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
                        PrimaryPowerup = RangedWeaponPowerup.Present,
                        Secondary = WeaponItem.UZI,
                    });
                    break;
                }
                #endregion

                #region Scientist
                case BotType.Scientist:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                        SecondaryPowerup = RangedWeaponPowerup.PreciseBouncing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.PreciseBouncing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL45,
                        SecondaryPowerup = RangedWeaponPowerup.PreciseBouncing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    break;
                }
                #endregion

                #region Sheriff
                case BotType.Sheriff:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MAGNUM,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.SHOTGUN,
                    });
                    break;
                }
                #endregion

                #region Smoker
                case BotType.Smoker:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.GRENADE_LAUNCHER,
                        PrimaryPowerup = RangedWeaponPowerup.Smoke,
                        Secondary = WeaponItem.MACHINE_PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Fatigue,
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
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Penetration,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SHOTGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Penetration,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Penetration,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.ASSAULT,
                        PrimaryPowerup = RangedWeaponPowerup.Penetration,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Penetration,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SMG,
                        PrimaryPowerup = RangedWeaponPowerup.Penetration,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Penetration,
                    });
                    break;
                }
                #endregion

                #region HeavySoldier
                case BotType.HeavySoldier:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.M60,
                        PrimaryPowerup = RangedWeaponPowerup.DoublePenetration,
                        Secondary = WeaponItem.PISTOL,
                    });
                    break;
                }
                #endregion

                #region Spacer
                case BotType.Spacer:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.SHOCK_BATON,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.ASSAULT,
                        PrimaryPowerup = RangedWeaponPowerup.Gauss,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Gauss,
                        UseLazer = true,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MACHINE_PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Gauss,
                        UseLazer = true,
                    });
                    break;
                }
                #endregion

                #region SpaceSniper
                case BotType.SpaceSniper:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SNIPER,
                        PrimaryPowerup = RangedWeaponPowerup.Gauss,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SNIPER,
                        PrimaryPowerup = RangedWeaponPowerup.Gauss,
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Gauss,
                    });
                    break;
                }
                #endregion

                #region Spy
                case BotType.Spy:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.KNIFE,
                        Secondary = WeaponItem.REVOLVER,
                    });
                    break;
                }
                #endregion

                #region Stripper
                case BotType.Stripper:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.MACHINE_PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Tearing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                        SecondaryPowerup = RangedWeaponPowerup.Tearing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.PISTOL,
                        SecondaryPowerup = RangedWeaponPowerup.Tearing,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SAWED_OFF,
                        PrimaryPowerup = RangedWeaponPowerup.Tearing,
                    });
                    break;
                }
                #endregion
                
                #region SuicideDwarf
                case BotType.SuicideDwarf:
                {
                    weapons.Add(WeaponSet.Empty);
                    break;
                }
                #endregion

                #region Survivalist
                case BotType.Survivalist:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                        Primary = WeaponItem.FLAREGUN,
                        PrimaryPowerup = RangedWeaponPowerup.Steak,
                    });
                    break;
                }
                #endregion

                #region Survivor
                case BotType.Survivor:
                {
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SMG,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.KNIFE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.MACHETE,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Primary = WeaponItem.SAWED_OFF,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Secondary = WeaponItem.REVOLVER,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                    });
                    break;
                }
                #endregion

                #region Tank
                case BotType.Tank:
                {
                    weapons.Add(new WeaponSet()
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.Slide,
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
                    weapons.Add(new WeaponSet()
                    {
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.LEAD_PIPE,
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
                    });
                    weapons.Add(new WeaponSet()
                    {
                        Melee = WeaponItem.PIPE,
                        MeleeHandPowerup = MeleeWeaponPowerup.Breaking,
                    });
                    break;
                }
                #endregion

                #region Translucent
                case BotType.Translucent:
                {
                    weapons.Add(WeaponSet.Empty);
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
                        MeleeHandPowerup = MeleeWeaponPowerup.Serious,
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

                #region ZombieEater
                case BotType.ZombieEater:
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
