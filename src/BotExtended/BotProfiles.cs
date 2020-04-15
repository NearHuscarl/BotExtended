﻿using System.Collections.Generic;
using SFDGameScriptInterface;

namespace BotExtended
{
    public partial class GameScript : GameScriptInterface
    {
        public static List<IProfile> GetProfiles(BotType botType)
        {
            var profiles = new List<IProfile>();

            switch (botType)
            {
                #region Agent
                case BotType.Agent:
                case BotType.Agent2:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Agent",
                        Accesory = new IProfileClothingItem("AgentSunglasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Agent",
                        Accesory = new IProfileClothingItem("AgentSunglasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Amos
                case BotType.Amos:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Amos",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingDarkCyan", "ClothingDarkCyan", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingGray", ""),
                    });
                    break;
                }
                #endregion

                #region Assassin
                case BotType.AssassinMelee:
                case BotType.AssassinRange:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Assassin",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Assassin",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Assassin",
                        Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Assassin",
                        Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Assassin",
                        Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Assassin",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Balloonatic
                case BotType.Balloonatic:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Balloonatic",
                        Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingOrange", "ClothingLightGreen", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingDarkRed", "ClothingLightYellow", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingDarkOrange", "ClothingDarkGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Afro", "ClothingLightPurple", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingOrange", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Bandido
                case BotType.Bandido:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Bandido",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("Poncho2", "ClothingDarkYellow", "ClothingLightYellow"),
                        ChestUnder = new IProfileClothingItem("Shirt", "ClothingDarkOrange", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Sombrero", "ClothingOrange", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkRed", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingOrange", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Bandido",
                        Accesory = new IProfileClothingItem("Scarf", "ClothingLightOrange", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkYellow", "ClothingLightGray"),
                        Head = new IProfileClothingItem("Sombrero", "ClothingLightBrown", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkRed", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingOrange", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Bandido",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray", "ClothingLightGray"),
                        ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkYellow", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingOrange", "ClothingYellow"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Bandido",
                        Accesory = new IProfileClothingItem("Scarf", "ClothingOrange", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("AmmoBelt_fem", "ClothingDarkGray", "ClothingLightGray"),
                        ChestUnder = new IProfileClothingItem("UnbuttonedShirt_fem", "ClothingDarkOrange", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingGray", "ClothingLightGray"),
                        Head = new IProfileClothingItem("Sombrero2", "ClothingLightOrange", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingOrange", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Bandido",
                        Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkYellow", "ClothingLightGray"),
                        Head = new IProfileClothingItem("Sombrero", "ClothingLightBrown", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkPurple", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingOrange", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Bandido",
                        Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("AmmoBelt_fem", "ClothingDarkGray", "ClothingLightGray"),
                        ChestUnder = new IProfileClothingItem("TrainingShirt_fem", "ClothingOrange", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Sombrero2", "ClothingLightOrange", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightYellow", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"),
                        Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingOrange", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Bandido",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("Poncho_fem", "ClothingDarkOrange", "ClothingDarkYellow"),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie_fem", "ClothingOrange", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Sombrero", "ClothingDarkPink", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkOrange", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"),
                        Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingOrange", "ClothingLightGray"),
                    });
                    break;
                }
                #endregion

                #region Balista
                case BotType.Balista:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Balista",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("MilitaryJacket_fem", "ClothingDarkYellow", "ClothingLightRed", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Afro", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SmallBelt_fem", "ClothingBrown", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Biker
                case BotType.Biker:
                case BotType.BikerHulk:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedJacket_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkPurple", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Headband", "ClothingLightBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingDarkPurple", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightBlue", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkPink", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedJacket_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkPink", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Headband", "ClothingLightBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Headband", "ClothingLightBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Headband", "ClothingLightBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightBlue", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedJacket_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkBlue", "ClothingLightBlue", ""),
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkPurple", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkPink", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkBlue", "ClothingLightBlue", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Biker",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkPink", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingLightBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightBlue", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Bobby
                case BotType.Bobby:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Bobby",
                        Accesory = new IProfileClothingItem("DogTag", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("VestBlack", "ClothingDarkBlue", "ClothingDarkBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("BucketHat", "ClothingDarkGray", "ClothingLightYellow", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Bodyguard
                case BotType.Bodyguard:
                case BotType.Bodyguard2:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Bodyguard",
                        Accesory = new IProfileClothingItem("AgentSunglasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Boffin
                case BotType.Boffin:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Boffin",
                        Accesory = new IProfileClothingItem("Glasses", "ClothingGreen", "ClothingLightGreen", ""),
                        ChestOver = new IProfileClothingItem("Coat", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingYellow", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Mohawk", "ClothingLightGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Cindy
                case BotType.Cindy:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Cindy",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightBlue", ""),
                        ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingDarkGray", "ClothingOrange", ""),
                        ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Cap", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkCyan", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region ClownBodyguard
                case BotType.ClownBodyguard:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Clown Bodyguard",
                        Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingLightCyan", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("HighHeels", "ClothingLightCyan", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("BucketHat", "ClothingLightCyan", "ClothingLightGray", ""),
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightCyan", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Clown Bodyguard",
                        Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingLightYellow", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("HighHeels", "ClothingLightYellow", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("BucketHat", "ClothingLightYellow", "ClothingLightGray", ""),
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Clown Bodyguard",
                        Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingPink", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("HighHeels", "ClothingPink", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("BucketHat", "ClothingPink", "ClothingLightGray", ""),
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingPink", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Clown Bodyguard",
                        Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingLightGreen", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("HighHeels", "ClothingLightGreen", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("BucketHat", "ClothingLightGreen", "ClothingLightGray", ""),
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGreen", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ClownBoxer
                case BotType.ClownBoxer:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Clown Boxer",
                        Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingDarkOrange", "ClothingOrange", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingRed", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("StripedPants", "ClothingLightOrange", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightYellow", ""),
                    });
                    break;
                }
                #endregion

                #region ClownCowboy
                case BotType.ClownCowboy:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Clown Cowboy",
                        Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Poncho", "ClothingPurple", "ClothingGreen", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingLightBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Fedora2", "ClothingOrange", "ClothingPurple", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region ClownGangster
                case BotType.ClownGangster:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Clown Gangster",
                        Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingLightYellow", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingLightBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("StylishHat", "ClothingPurple", "ClothingLightGreen", ""),
                        Legs = new IProfileClothingItem("StripedPants", "ClothingPurple", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingBrown", "ClothingLightYellow", ""),
                    });
                    break;
                }
                #endregion

                #region Cowboy
                case BotType.Cowboy:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Cowboy",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingBrown", "ClothingBrown", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("CowboyHat", "ClothingLightBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cowboy",
                        Accesory = new IProfileClothingItem("Scarf", "ClothingLightOrange", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Fedora", "ClothingLightBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkRed", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingOrange", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cowboy",
                        Accesory = new IProfileClothingItem("Scarf", "ClothingLightYellow", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingLightYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkOrange", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Fedora2", "ClothingBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkOrange", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cowboy",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingDarkPink", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("CowboyHat", "ClothingLightBrown", "ClothingLightGreen", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cowboy",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingBrown", "ClothingBrown", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkBrown", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cowboy",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingDarkRed", "ClothingDarkRed", ""),
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("CowboyHat", "ClothingDarkBrown", "ClothingLightBrown", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkBrown", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cowboy",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingDarkGray", "ClothingDarkGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("CowboyHat", "ClothingBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow", ""),
                    });
                    break;
                }
                #endregion

                #region Cyborg
                case BotType.Cyborg:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Cyborg",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("OfficerJacket", "ClothingLightGray", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingLightCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cyborg",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("OfficerJacket", "ClothingLightGray", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingLightCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cyborg",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("OfficerJacket_fem", "ClothingLightGray", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("BodyArmor_fem", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingLightCyan", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingLightGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin5", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cyborg",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkPurple", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingLightCyan", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingLightGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin5", "ClothingLightCyan", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cyborg",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingLightCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingLightGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightCyan", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Cyborg",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingLightCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingLightCyan", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingLightGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin5", "ClothingLightCyan", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Demolitionist
                case BotType.Demolitionist:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "The Demolitionist",
                        Accesory = new IProfileClothingItem("AgentSunglasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("GrenadeBelt", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Elf
                case BotType.Elf:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Elf",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingGreen", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("SantaHat", "ClothingGreen", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGreen", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGreen", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Elf",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacket_fem", "ClothingGreen", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("SantaHat", "ClothingGreen", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingGreen", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin3", "ClothingPink", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGreen", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Engineer
                case BotType.Engineer:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Engineer",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("PithHelmet", "ClothingLightGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Engineer",
                        Accesory = new IProfileClothingItem("Glasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("KevlarVest", "ClothingOrange", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("PithHelmet", "ClothingYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Engineer",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("PithHelmet", "ClothingYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Engineer",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("WeldingHelmet", "ClothingLightYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Farmer
                case BotType.Farmer:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Farmer",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Fedora2", "ClothingYellow", "ClothingGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Farmer",
                        Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingCyan", "ClothingCyan", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("SergeantHat", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingPink", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Farmer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("RiceHat", "ClothingLightGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Fritzliebe
                case BotType.Fritzliebe:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Dr. Fritzliebe",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("FLDisguise", "ClothingLightGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Funnyman
                case BotType.Funnyman:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Funnyman",
                        Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StripedSuitJacket", "ClothingLightBlue", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingLightYellow", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingLightGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("StripedPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Jo
                case BotType.Jo:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Jo",
                        Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkOrange", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SmallBelt_fem", "ClothingLightBrown", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Hacker
                case BotType.Hacker:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Hacker",
                        Accesory = new IProfileClothingItem("Goggles", "ClothingDarkGreen", "ClothingLightCyan", ""),
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkGray", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingOrange", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingLightGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("BaseballCap", "ClothingDarkGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Hacker",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkGray", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingOrange", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingLightGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("BaseballCap", "ClothingDarkGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Gangster
                case BotType.Gangster:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingDarkYellow", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkYellow", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Flatcap", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkPink", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Flatcap", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingGray", "ClothingGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingDarkYellow", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingBrown", "ClothingDarkYellow", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingDarkYellow", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Flatcap", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkYellow", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("StylishHat", "ClothingGray", "ClothingDarkPink", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingGray", "ClothingDarkGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkYellow", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Flatcap", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightYellow", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingGray", "ClothingGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingDarkYellow", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingBrown", "ClothingDarkYellow", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkPink", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("BlazerWithShirt_fem", "ClothingGray", "ClothingDarkPink", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("HighHeels", "ClothingDarkPink", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink", ""),
                        Legs = new IProfileClothingItem("Skirt_fem", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin2", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region GangsterHulk
                case BotType.GangsterHulk:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster Hulk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingDarkYellow", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Flatcap", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster Hulk",
                        Accesory = new IProfileClothingItem("Cigar", "ClothingBrown", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingDarkYellow", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gangster Hulk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingDarkYellow", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("BucketHat", "ClothingGray", "ClothingGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightYellow", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Gardener
                case BotType.Gardener:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Gardener",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Apron_fem", "ClothingCyan", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingLightBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Gardener",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingCyan", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Handler
                case BotType.Handler:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Handler",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGreen", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Sneakers", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingLightGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Shorts", "ClothingBrown", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingLightBrown", "ClothingBrown", ""),
                    });
                    break;
                }
                #endregion

                #region Hunter
                case BotType.Hunter:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Hunter",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingCyan", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightCyan", "ClothingDarkCyan", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Hunter",
                        Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingOrange", "ClothingDarkOrange", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingLightOrange", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingOrange", "ClothingLightOrange", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Incinerator
                case BotType.Incinerator:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "The Incinerator",
                        Accesory = new IProfileClothingItem("GasMask", "ClothingDarkYellow", "ClothingLightOrange", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacketBlack", "ClothingDarkYellow", "ClothingOrange", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkOrange", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Headband", "ClothingOrange", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkYellow", "ClothingLightOrange", ""),
                    });
                    break;
                }
                #endregion

                #region Kingpin
                case BotType.Kingpin:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Kingpin",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StripedSuitJacket", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingPink", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingLightGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("TopHat", "ClothingDarkGray", "ClothingPink", ""),
                        Legs = new IProfileClothingItem("StripedPants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Kriegbär
                case BotType.Kriegbär:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Kriegbär #2",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("FrankenbearSkin", "ClothingDarkGray", "ClothingLightBlue", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region LabAssistant
                case BotType.LabAssistant:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Lab Assistant",
                        Accesory = new IProfileClothingItem("Mask", "ClothingLightCyan", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Lab Assistant",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingCyan", "ClothingLightCyan", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Lumberjack
                case BotType.Lumberjack:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Lumberjack",
                        Accesory = new IProfileClothingItem("SantaMask", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("LumberjackShirt", "ClothingLightRed", "ClothingGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Lumberjack",
                        Accesory = new IProfileClothingItem("Moustache", "ClothingGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingBlue", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingLightRed", "ClothingGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Meatgrinder
                case BotType.Meatgrinder:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "The Meatgrinder",
                        Accesory = new IProfileClothingItem("GoalieMask", "ClothingLightGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("Apron", "ClothingLightPink", "ClothingLightGray"),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingDarkRed", "ClothingLightGray"),
                        Head = new IProfileClothingItem("ChefHat", "ClothingLightGray", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink"),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Mecha
                case BotType.Mecha:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Mecha Fritzliebe",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("MechSkin", "ClothingLightGray", "ClothingLightRed", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region MetroCop
                case BotType.MetroCop:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "MetroCop",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray"),
                        Head = new IProfileClothingItem("MetroLawGasMask", "ClothingGray", "ClothingLightGreen"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "MetroCop",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray"),
                        Head = new IProfileClothingItem("MetroLawMask", "ClothingGray", "ClothingLightGreen"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightRed"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "MetroCop",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray"),
                        Head = new IProfileClothingItem("MetroLawGasMask", "ClothingGray", "ClothingLightRed"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin5", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingGray", "ClothingLightGray"),
                    });
                    break;
                }
                #endregion

                #region MetroCop2
                case BotType.MetroCop2:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "MetroCop",
                        Accesory = new IProfileClothingItem("Earpiece", "ClothingLightGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray"),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightRed"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "MetroCop",
                        Accesory = new IProfileClothingItem("Earpiece", "ClothingLightGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray"),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "MetroCop",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray"),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightRed"),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region MirrorMan
                case BotType.MirrorMan:
                    profiles.Add(new IProfile()
                    {
                        Name = "MirrorMan",
                        Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("Shirt", "ClothingLightBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Hood", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Sash", "ClothingDarkBlue", "ClothingLightGray", ""),
                    });
                    break;
                #endregion

                #region Mutant
                case BotType.Mutant:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("RestraintMask", "ClothingLightCyan", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("RestraintMask", "ClothingCyan", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("RestraintMask", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("RestraintMask", "ClothingLightCyan", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Mutant",
                        Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region NaziLabAssistant
                case BotType.NaziLabAssistant:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Nazi Lab Assistant",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region NaziMuscleSoldier
                case BotType.NaziMuscleSoldier:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Nazi Soldier",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingLightBrown", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBrown", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region NaziScientist
                case BotType.NaziScientist:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Nazi Scientist",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("HazmatMask", "ClothingCyan", "ClothingLightGreen", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Nazi Scientist",
                        Accesory = new IProfileClothingItem("Armband_fem", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacket_fem", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack_fem", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("HazmatMask", "ClothingCyan", "ClothingLightGreen", ""),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region NaziSoldier
                case BotType.NaziSoldier:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Nazi Soldier",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("GermanHelmet", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Nazi Soldier",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingLightBrown", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBrown", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Nazi Soldier",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingLightBrown", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("GermanHelmet", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBrown", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region SSOfficer
                case BotType.SSOfficer:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "SS Officer",
                        Accesory = new IProfileClothingItem("Armband", "ClothingRed", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("OfficerJacket", "ClothingDarkGray", "ClothingLightYellow", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("OfficerHat", "ClothingDarkGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Ninja
                case BotType.Ninja:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Ninja",
                        Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Ninja",
                        Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Ninja",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Ninja",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Police
                case BotType.Police:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Officer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Officer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Officer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Officer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Officer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Officer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Officer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray"),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region PoliceChief
                case BotType.PoliceChief:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Police Chief",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingDarkGray", "ClothingYellow", ""),
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingGray", ""),
                    });
                    break;
                }
                #endregion

                #region PoliceSWAT
                case BotType.PoliceSWAT:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "SWAT",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("KevlarVest_fem", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet2", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "SWAT",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("KevlarVest", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet2", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Punk
                case BotType.Punk:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingLightYellow", "ClothingOrange", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Buzzcut", "ClothingDarkGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Mohawk", "ClothingLightRed", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("JacketBlack_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightYellow", ""),
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Mohawk", "ClothingLightYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Mohawk", "ClothingLightPurple", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingPink", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Buzzcut", "ClothingDarkGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingLightYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Vest", "ClothingLightYellow", "ClothingLightYellow", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Mohawk", "ClothingOrange", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightPurple", ""),
                        ChestOver = new IProfileClothingItem("Vest", "ClothingLightYellow", "ClothingLightYellow", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingLightYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightYellow", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("CoatBlack_fem", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightYellow", ""),
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingDarkGray", "ClothingLightOrange", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkGray", "ClothingLightOrange", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkOrange", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Hood", "ClothingLightRed", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region PunkHulk
                case BotType.PunkHulk:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk Muscle",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk Muscle",
                        Accesory = new IProfileClothingItem("Goggles", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightRed", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk Muscle",
                        Accesory = new IProfileClothingItem("RestraintMask", "ClothingDarkYellow", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightRed", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk Muscle",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingOrange", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Punk Muscle",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Vest", "ClothingLightYellow", "ClothingLightYellow", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingDarkYellow", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Raze
                case BotType.Raze:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Raze",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightBlue", ""),
                        ChestOver = new IProfileClothingItem("OfficerJacket_fem", "ClothingDarkGray", "ClothingYellow", ""),
                        ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkOrange", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Cap", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightBlue", ""),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingGray", "ClothingYellow", ""),
                    });
                    break;
                }
                #endregion

                #region Reznor
                case BotType.Reznor:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Reznor",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingGreen", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkOrange", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightGreen", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingGreen", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SmallBelt", "ClothingGreen", "ClothingLightGreen", ""),
                    });
                    break;
                }
                #endregion

                #region Santa
                case BotType.Santa:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Bad Santa",
                        Accesory = new IProfileClothingItem("SantaMask", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Coat", "ClothingRed", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("SantaHat", "ClothingRed", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingRed", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkRed", "ClothingLightYellow", ""),
                    });
                    break;
                }
                #endregion

                #region Scientist
                case BotType.Scientist:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Scientist",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingBlue", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("HazmatMask", "ClothingCyan", "ClothingLightGreen", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Scientist",
                        Accesory = new IProfileClothingItem("Mask", "ClothingLightCyan", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("OfficerJacket", "ClothingCyan", "ClothingCyan", ""),
                        ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Hood", "ClothingCyan", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Sheriff
                case BotType.Sheriff:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Sheriff Sternwood",
                        Accesory = new IProfileClothingItem("Moustache", "ClothingGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingLightBrown", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("SergeantHat", "ClothingLightBrown", "ClothingLightYellow", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow", ""),
                    });
                    break;
                }
                #endregion

                #region Smoker
                case BotType.Smoker:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Smoker",
                        Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGray", "ClothingLightCyan", ""),
                        ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGloves", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("CombatBelt", "ClothingCyan", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Sniper
                case BotType.Sniper:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Sniper",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Sniper",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Soldier
                case BotType.Soldier:
                case BotType.Soldier2:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin4", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin3", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin2", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin1", "ClothingLightYellow", ""),
                        Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region Spacer
                case BotType.Spacer:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingOrange", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingLightOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = new IProfileClothingItem("Armband", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkCyan", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingLightOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = new IProfileClothingItem("Armband", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingLightOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("Mohawk", "ClothingDarkGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("JacketBlack", "ClothingOrange", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingOrange", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingGray", "ClothingLightCyan", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("CamoPants", "ClothingOrange", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed", ""),
                        ChestOver = new IProfileClothingItem("Apron_fem", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Mohawk", "ClothingLightPurple", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("StripedPants_fem", "ClothingOrange", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightCyan", ""),
                        Waist = new IProfileClothingItem("CombatBelt_fem", "ClothingLightOrange", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("HawaiiShirt", "ClothingDarkGray", "ClothingLightCyan", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightCyan", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Spacer",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("JacketBlack", "ClothingGray", "ClothingLightCyan", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingOrange", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightOrange", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region SpaceSniper
                case BotType.SpaceSniper:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Sniper",
                        Accesory = new IProfileClothingItem("Armband", "ClothingOrange", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("Gloves", "ClothingDarkGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGray", "ClothingDarkGreen", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Sniper",
                        Accesory = new IProfileClothingItem("Armband", "ClothingOrange", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkGreen", "ClothingGreen", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingLightGray", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Sniper",
                        Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightCyan", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingLightCyan", "ClothingLightGray", ""),
                        Head = new IProfileClothingItem("BaseballCap", "ClothingDarkGray", "ClothingLightCyan", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray", ""),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Stripper
                case BotType.Stripper:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Stripper",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Stripper",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Stripper",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("HighHeels", "ClothingLightRed", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightRed", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Stripper",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingGreen", "ClothingLightGray", ""),
                        Feet = null,
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingGreen", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Stripper",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("ShoulderHolster_fem", "ClothingPurple", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingPurple", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region SurvivorBiker
                case BotType.SurvivorBiker:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Survivor",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("JacketBlack", "ClothingDarkGray", "ClothingGray", ""),
                        ChestUnder = new IProfileClothingItem("HawaiiShirt", "ClothingLightGray", "ClothingLightRed", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingLightRed", "ClothingDarkGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion
                #region SurvivorCrazy
                case BotType.SurvivorCrazy:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Survivor",
                        Accesory = new IProfileClothingItem("SmallMoustache", "ClothingLightRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingBrown", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion
                #region SurvivorNaked
                case BotType.SurvivorNaked:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Survivor",
                        Accesory = new IProfileClothingItem("DogTag", "ClothingLightGray", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("Sneakers", "ClothingGray", "ClothingGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingDarkGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion
                #region SurvivorRifleman
                case BotType.SurvivorRifleman:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Survivor",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingDarkGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Tattoos", "Skin5", "ClothingLightRed", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingBrown", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion
                #region SurvivorRobber
                case BotType.SurvivorRobber:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Survivor",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("JacketBlack", "ClothingDarkGray", "ClothingGray", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion
                #region SurvivorTough
                case BotType.SurvivorTough:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Survivor",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingBrown", "ClothingDarkBrown", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet2", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Bear
                case BotType.Teddybear:
                case BotType.Babybear:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Teddybear",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = null,
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = null,
                        Skin = new IProfileClothingItem("BearSkin", "Skin1", "ClothingLightGray"),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region Thug
                case BotType.Thug:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkCyan", "ClothingLightYellow"),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkCyan", "ClothingLightYellow"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray"),
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightGreen"),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingBrown", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray"),
                        Head = null,
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin3", "ClothingLightYellow"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray"),
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightGreen"),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingBrown", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray"),
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin2", "ClothingLightGreen"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingBrown", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue", "ClothingLightGray"),
                        Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkGreen", "ClothingLightYellow"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos_fem", "Skin1", "ClothingLightOrange"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingBrown", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("BaseballCap", "ClothingRed", "ClothingLightRed"),
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingLightBlue", "ClothingLightBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingDarkRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightGray"),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("DogTag", "ClothingLightGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("VestBlack", "ClothingDarkBlue", "ClothingBlue"),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray", "ClothingLightGray"),
                        Head = new IProfileClothingItem("WoolCap", "ClothingLightRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink"),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingBrown", "ClothingLightGray"),
                        Head = new IProfileClothingItem("Headband", "ClothingLightRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow"),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingDarkBlue"),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue", "ClothingLightGray"),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue", "ClothingLightGray"),
                        Head = new IProfileClothingItem("Headband", "ClothingLightRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkPurple", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"),
                        Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingLightBlue", "ClothingLightBlue"),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkBlue", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingRed", "ClothingLightGray"),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingDarkYellow"),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ThugHulk
                case BotType.ThugHulk:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug Hulk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug Hulk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Thug Hulk",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray"),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray"),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray"),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingLightBlue", "ClothingLightGray"),
                        Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"),
                        Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"),
                    });
                    break;
                }
                #endregion

                #region Zombie
                case BotType.Zombie:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = null,
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieAgent
                case BotType.ZombieAgent:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Agent",
                        Accesory = new IProfileClothingItem("SunGlasses", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieBruiser
                case BotType.ZombieBruiser:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Bruiser",
                        Accesory = new IProfileClothingItem("RestraintMask", "ClothingGray", "ClothingLightGray", ""),
                        ChestOver = new IProfileClothingItem("VestBlack", "ClothingBlue", "ClothingDarkBlue", ""),
                        ChestUnder = null,
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants", "ClothingDarkPurple", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieChild
                case BotType.ZombieChild:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Child",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingPurple", "ClothingLightGray", ""),
                        Feet = null,
                        Gender = Gender.Female,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Child",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt", "ClothingPurple", "ClothingLightGray", ""),
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("TornPants", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieFat
                case BotType.ZombieFat:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Fat Zombie",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingRed", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Shorts", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieFighter
                case BotType.ZombieFighter:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Dead Cop",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("Sweater", "ClothingGreen", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Dead Merc",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingBrown", "ClothingLightBrown", ""),
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingBrown", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Dead Vigilante",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Dead Spy",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Dead Pilot",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingDarkBrown", "ClothingDarkBrown", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SmallBelt", "ClothingDarkGray", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Dead Driver",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Jacket", "ClothingBrown", "ClothingBrown", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGray", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region ZombieFlamer
                case BotType.ZombieFlamer:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Flamer",
                        Accesory = new IProfileClothingItem("Glasses", "ClothingLightYellow", "ClothingLightYellow", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingGray", "ClothingLightGray", ""),
                        Feet = null,
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieGangster
                case BotType.ZombieGangster:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingLightBlue", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("StylishHat", "ClothingGray", "ClothingPink", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingDarkYellow", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingPink", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Flatcap", "ClothingGray", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("StylishHat", "ClothingGray", "ClothingDarkYellow", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray", "ClothingLightGray", ""),
                        ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkPink", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink", ""),
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Gangster",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkPink", ""),
                        ChestUnder = null,
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingGray", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = null,
                        Legs = new IProfileClothingItem("Pants", "ClothingGray", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieNinja
                case BotType.ZombieNinja:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Ninja",
                        Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed", "ClothingLightGray", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TrainingShirt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Head = null,
                        Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("Sash_fem", "ClothingDarkRed", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region ZombiePolice
                case BotType.ZombiePolice:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Police",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Police",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombiePrussian
                case BotType.ZombiePrussian:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Prussian",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("LeatherJacketBlack", "ClothingCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("SpikedHelmet", "ClothingCyan", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Prussian",
                        Accesory = new IProfileClothingItem("GasMask", "ClothingCyan", "ClothingLightGreen", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("SpikedHelmet", "ClothingCyan", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region BaronVonHauptstein
                case BotType.BaronVonHauptstein:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "BaronVonHauptstein", // TODO
                        Accesory = new IProfileClothingItem("GasMask", "ClothingCyan", "ClothingLightGreen", ""),
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("SpikedHelmet", "ClothingCyan", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingDarkCyan", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieSoldier
                case BotType.ZombieSoldier:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkRed", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingBrown", "ClothingLightGray", ""),
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Soldier",
                        Accesory = null,
                        ChestOver = null,
                        ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkRed", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingBrown", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion

                #region ZombieThug
                case BotType.ZombieThug:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Thug",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest_fem", "ClothingLightBlue", "ClothingLightBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Female,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingDarkRed", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie_fem", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Thug",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Vest", "ClothingLightBlue", "ClothingLightBlue", ""),
                        ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Headband", "ClothingDarkRed", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = null,
                    });
                    break;
                }
                #endregion

                #region ZombieWorker
                case BotType.ZombieWorker:
                {
                    profiles.Add(new IProfile()
                    {
                        Name = "Zombie Worker",
                        Accesory = null,
                        ChestOver = new IProfileClothingItem("Suspenders", "ClothingOrange", "ClothingLightOrange", ""),
                        ChestUnder = new IProfileClothingItem("TornShirt", "ClothingOrange", "ClothingLightGray", ""),
                        Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown", "ClothingLightGray", ""),
                        Gender = Gender.Male,
                        Hands = null,
                        Head = new IProfileClothingItem("Cap", "ClothingYellow", "ClothingLightGray", ""),
                        Legs = new IProfileClothingItem("TornPants", "ClothingOrange", "ClothingLightGray", ""),
                        Skin = new IProfileClothingItem("Zombie", "Skin1", "ClothingLightGray", ""),
                        Waist = new IProfileClothingItem("SatchelBelt", "ClothingOrange", "ClothingLightGray", ""),
                    });
                    break;
                }
                #endregion
            }

            return profiles;
        }
    }
}
