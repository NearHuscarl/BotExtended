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
                {
                    // https://profile-editor.vercel.app?p=eyJuYW1lIjoiQWdlbnQiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjMiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1aXRKYWNrZXRCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNoaXJ0V2l0aFRpZSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nRGFya0dyYXkiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQWdlbnRTdW5nbGFzc2VzIiwiY29sb3JzIjpbbnVsbCwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Agent", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown"), Accesory = new IProfileClothingItem("AgentSunglasses", "", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQWdlbnQiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1aXRKYWNrZXRCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNoaXJ0V2l0aFRpZSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nRGFya0dyYXkiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQWdlbnRTdW5nbGFzc2VzIiwiY29sb3JzIjpbbnVsbCwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Agent", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown"), Accesory = new IProfileClothingItem("AgentSunglasses", "", "ClothingLightGray"), });
                    break;
                }
                #endregion

                #region Agent79
                case BotType.Agent79:
                {
                    // https://profile-editor.vercel.app?p=M38yg367Z2H7Z0ieg0qeZ13ZZ01Zl10eZ0QeZ
                    profiles.Add(new IProfile() { Name = "Agent 79", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightBlue"), Head = new IProfileClothingItem("Flatcap", "ClothingGray"), ChestOver = new IProfileClothingItem("GrenadeBelt", ""), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightBlue"), Legs = new IProfileClothingItem("StripedPants", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("AgentSunglasses", "", "ClothingLightOrange"), });
                    break;
                }
                #endregion

                #region Amos
                case BotType.Amos:
                {
                    // https://profile-editor.vercel.app?p=M38xe363Z1K6Z0i7e0p7Z3j66YYY
                    profiles.Add(new IProfile() { Name = "Amos", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingGray"), ChestOver = new IProfileClothingItem("Vest", "ClothingDarkCyan", "ClothingDarkCyan"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingCyan"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingDarkCyan"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Assassin
                case BotType.AssassinMelee:
                case BotType.AssassinRange:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQXNzYXNzaW4iLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU3dlYXRlckJsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTWFzayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Assassin", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQXNzYXNzaW4iLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW40IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlN3ZWF0ZXJCbGFja19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50c0JsYWNrX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTWFzayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Assassin", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQXNzYXNzaW4iLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU3dlYXRlckJsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQmFsYWNsYXZhIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Assassin", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQXNzYXNzaW4iLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW40IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlN3ZWF0ZXJCbGFja19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50c0JsYWNrX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQmFsYWNsYXZhIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Assassin", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQXNzYXNzaW4iLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1aXRKYWNrZXRCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNoaXJ0V2l0aFRpZSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nRGFya0dyYXkiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IkJhbGFjbGF2YSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Assassin", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQXNzYXNzaW4iLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1aXRKYWNrZXRCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNoaXJ0V2l0aFRpZSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nRGFya0dyYXkiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik1hc2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Assassin", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkBlue"), });
                    break;
                }
                #endregion

                #region AssKicker
                case BotType.AssKicker:
                {
                    // https://profile-editor.vercel.app?p=F1Gxj2lpZ1NpZY2n7ZYYYY
                    profiles.Add(new IProfile() { Name = "Ass Kicker", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("Shirt_fem", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightYellow"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Balloonatic
                case BotType.Balloonatic:
                {
                    // https://profile-editor.vercel.app?p=M1FAj2jcp1KqZY2D971dqk0yoZY00nZ
                    profiles.Add(new IProfile() { Name = "Balloonatic", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), Head = new IProfileClothingItem("Afro", "ClothingLightPurple"), ChestOver = new IProfileClothingItem("Jacket", "ClothingOrange", "ClothingLightGreen"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingDarkRed", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants", "ClothingOrange"), Feet = new IProfileClothingItem("Sneakers", "ClothingDarkOrange", "ClothingDarkGray"), Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region Bandido
                case BotType.Bandido:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmFuZGlkbyIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImhlYWQiOnsiaWQiOiJTb21icmVybyIsImNvbG9ycyI6WyJDbG90aGluZ09yYW5nZSIsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlBvbmNobzIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrWWVsbG93IiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrT3JhbmdlIixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJTYXRjaGVsQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ09yYW5nZSIsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtSZWQiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiUmlkaW5nQm9vdHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCcm93biIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik1hc2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrUmVkIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Bandido", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Sombrero", "ClothingOrange"), ChestOver = new IProfileClothingItem("Poncho2", "ClothingDarkYellow", "ClothingLightYellow"), ChestUnder = new IProfileClothingItem("Shirt", "ClothingDarkOrange"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingOrange"), Legs = new IProfileClothingItem("Pants", "ClothingDarkRed"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmFuZGlkbyIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImhlYWQiOnsiaWQiOiJTb21icmVybyIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0QnJvd24iLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlVuYnV0dG9uZWRTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtPcmFuZ2UiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IkZpbmdlcmxlc3NHbG92ZXMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJBbW1vQmVsdFdhaXN0IiwiY29sb3JzIjpbIkNsb3RoaW5nT3JhbmdlIixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1JlZCIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU2NhcmYiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodE9yYW5nZSIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Bandido", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Sombrero", "ClothingLightBrown"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkYellow"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingOrange"), Legs = new IProfileClothingItem("Pants", "ClothingDarkRed"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), Accesory = new IProfileClothingItem("Scarf", "ClothingLightOrange"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmFuZGlkbyIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImhlYWQiOnsiaWQiOiJIZWFkYmFuZCIsImNvbG9ycyI6WyJDbG90aGluZ1JlZCIsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IkFtbW9CZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVW5idXR0b25lZFNoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya09yYW5nZSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ09yYW5nZSIsIkNsb3RoaW5nWWVsbG93IixudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiUmlkaW5nQm9vdHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCcm93biIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Bandido", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange"), Waist = new IProfileClothingItem("Belt", "ClothingOrange", "ClothingYellow"), Legs = new IProfileClothingItem("Pants", "ClothingDarkYellow"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmFuZGlkbyIsImdlbmRlciI6MSwic2tpbiI6eyJpZCI6Ik5vcm1hbF9mZW0iLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiU29tYnJlcm8yIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRPcmFuZ2UiLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJBbW1vQmVsdF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJVbmJ1dHRvbmVkU2hpcnRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya09yYW5nZSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiRmluZ2VybGVzc0dsb3ZlcyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkFtbW9CZWx0V2Fpc3RfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nT3JhbmdlIixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU2NhcmYiLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Bandido", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Sombrero2", "ClothingLightOrange"), ChestOver = new IProfileClothingItem("AmmoBelt_fem", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt_fem", "ClothingDarkOrange"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingGray"), Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingOrange"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightGray"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), Accesory = new IProfileClothingItem("Scarf", "ClothingOrange"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmFuZGlkbyIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImhlYWQiOnsiaWQiOiJTb21icmVybyIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0QnJvd24iLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlVuYnV0dG9uZWRTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtPcmFuZ2UiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IkZpbmdlcmxlc3NHbG92ZXMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJBbW1vQmVsdFdhaXN0IiwiY29sb3JzIjpbIkNsb3RoaW5nT3JhbmdlIixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1B1cnBsZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQ2lnYXIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Bandido", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Sombrero", "ClothingLightBrown"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkYellow"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingOrange"), Legs = new IProfileClothingItem("Pants", "ClothingDarkPurple"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmFuZGlkbyIsImdlbmRlciI6MSwic2tpbiI6eyJpZCI6Ik5vcm1hbF9mZW0iLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IlNvbWJyZXJvMiIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0T3JhbmdlIixudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiQW1tb0JlbHRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVHJhaW5pbmdTaGlydF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IlNhdGNoZWxCZWx0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ09yYW5nZSIsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50c19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQ2lnYXIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Bandido", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("Sombrero2", "ClothingLightOrange"), ChestOver = new IProfileClothingItem("AmmoBelt_fem", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("TrainingShirt_fem", "ClothingOrange"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingOrange"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightYellow"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmFuZGlkbyIsImdlbmRlciI6MSwic2tpbiI6eyJpZCI6Ik5vcm1hbF9mZW0iLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IlNvbWJyZXJvIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1BpbmsiLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJQb25jaG9fZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya09yYW5nZSIsIkNsb3RoaW5nRGFya1llbGxvdyIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2hpcnRXaXRoQm93dGllX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ09yYW5nZSIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQW1tb0JlbHRXYWlzdF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHNfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya09yYW5nZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTWFzayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtSZWQiLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Bandido", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("Sombrero", "ClothingDarkPink"), ChestOver = new IProfileClothingItem("Poncho_fem", "ClothingDarkOrange", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie_fem", "ClothingOrange", "ClothingLightGray"), Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingOrange"), Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkOrange"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed"), });
                    break;
                }
                #endregion

                #region BazookaJane
                case BotType.BazookaJane:
                {
                    // https://profile-editor.vercel.app?p=F1Gyj37tZ1MdZ2ddZ2neZ31ejY10eZ1adZ
                    profiles.Add(new IProfile() { Name = "Bazooka Jane", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("Helmet2", "ClothingDarkYellow"), ChestOver = new IProfileClothingItem("Suspenders_fem", "ClothingGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingRed"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkYellow"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Balista
                case BotType.Balista:
                {
                    // https://profile-editor.vercel.app?p=F1Gwj2zpZ1M6Z2B2j0q1Z1ydo2XZj0O7Z004Z
                    profiles.Add(new IProfile() { Name = "Balista", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("Afro", "ClothingDarkBlue"), ChestOver = new IProfileClothingItem("MilitaryJacket_fem", "ClothingDarkYellow", "ClothingLightRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightYellow"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray"), Waist = new IProfileClothingItem("SmallBelt_fem", "ClothingBrown", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    break;
                }
                #endregion

                #region Beast
                case BotType.Beast:
                {
                    // https://profile-editor.vercel.app?p=M1FwjY1K1Z2A6j0qsZ3j4411ZZ0PsZY
                    profiles.Add(new IProfile() { Name = "The Beast", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Vest", "ClothingDarkBlue", "ClothingDarkBlue"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingPurple"), Waist = new IProfileClothingItem("SmallBelt", "ClothingDarkCyan", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingPurple"), Accesory = new IProfileClothingItem("GoalieMask", ""), });
                    break;
                }
                #endregion

                #region Berserker
                case BotType.Berserker:
                {
                    // https://profile-editor.vercel.app?p=M3owiY1KeZY2neZY1WeZ0PoZ0geo
                    profiles.Add(new IProfile() { Name = "Berserker", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin1", "ClothingLightCyan"), Head = new IProfileClothingItem("BaseballCap", "ClothingGray", "ClothingLightRed"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightRed"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Biker
                case BotType.Biker:
                case BotType.BikerHulk:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW4xIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3R1ZGRlZEphY2tldF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIiwiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2xlZXZlbGVzc1NoaXJ0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ikdsb3Zlc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImxlZ3MiOnsiaWQiOiJUb3JuUGFudHNfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1B1cnBsZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IlN1bkdsYXNzZXMiLCJjb2xvcnMiOltudWxsLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingDarkYellow"), ChestOver = new IProfileClothingItem("StuddedJacket_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkYellow"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkPurple"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkhlYWRiYW5kIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3R1ZGRlZFZlc3QiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIiwiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2xlZXZlbGVzc1NoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1llbGxvdyIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiR2xvdmVzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJsZWdzIjp7ImlkIjoiVG9yblBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1B1cnBsZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IlN1bkdsYXNzZXMiLCJjb2xvcnMiOltudWxsLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightBlue"), Head = new IProfileClothingItem("Headband", "ClothingLightBlue"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkYellow"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingDarkPurple"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJBdmlhdG9ySGF0IiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJWZXN0QmxhY2tfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsIkNsb3RoaW5nRGFya0JsdWUiLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNsZWV2ZWxlc3NTaGlydF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrUGluayIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiRmluZ2VybGVzc0dsb3Zlc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50c0JsYWNrX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray"), ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkPink"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3R1ZGRlZEphY2tldF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIiwiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2xlZXZlbGVzc1NoaXJ0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtQaW5rIixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJHbG92ZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkJlbHRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHNCbGFja19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IlN1bkdsYXNzZXMiLCJjb2xvcnMiOltudWxsLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), ChestOver = new IProfileClothingItem("StuddedJacket_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkPink"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IkhlYWRiYW5kIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3R1ZGRlZFZlc3QiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIiwiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2xlZXZlbGVzc1NoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJGaW5nZXJsZXNzR2xvdmVzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("Headband", "ClothingLightBlue"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightBlue"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IkF2aWF0b3JIYXQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCcm93biIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN0dWRkZWRWZXN0IiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsIkNsb3RoaW5nRGFya0JsdWUiLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNsZWV2ZWxlc3NTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0Qmx1ZSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiRmluZ2VybGVzc0dsb3Zlc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightBlue"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJIZWFkYmFuZCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0Qmx1ZSIsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlZlc3RCbGFja19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIiwiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVFNoaXJ0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ikdsb3Zlc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImxlZ3MiOnsiaWQiOiJUb3JuUGFudHNfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU3VuR2xhc3NlcyIsImNvbG9ycyI6W251bGwsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX19
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("Headband", "ClothingLightBlue"), ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingDarkYellow"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0xpZ2h0Qmx1ZSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiSGVhZGJhbmQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdHVkZGVkVmVzdCIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLCJDbG90aGluZ0RhcmtCbHVlIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTbGVldmVsZXNzU2hpcnRCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJGaW5nZXJsZXNzR2xvdmVzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU3VuR2xhc3NlcyIsImNvbG9ycyI6W251bGwsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX19
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightBlue"), Head = new IProfileClothingItem("Headband", "ClothingLightBlue"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingDarkGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJBdmlhdG9ySGF0IiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdHVkZGVkVmVzdF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIiwiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2xlZXZlbGVzc1NoaXJ0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IkZpbmdlcmxlc3NHbG92ZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkJlbHRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHNfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray"), ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkYellow"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJNb3RvcmN5Y2xlSGVsbWV0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLCJDbG90aGluZ0xpZ2h0Qmx1ZSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdHVkZGVkSmFja2V0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLCJDbG90aGluZ0RhcmtCbHVlIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTbGVldmVsZXNzU2hpcnRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1llbGxvdyIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiR2xvdmVzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwibGVncyI6eyJpZCI6IlRvcm5QYW50c19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrUHVycGxlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU3VuR2xhc3NlcyIsImNvbG9ycyI6W251bGwsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX19
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkBlue", "ClothingLightBlue"), ChestOver = new IProfileClothingItem("StuddedJacket_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkYellow"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkPurple"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6Ik1vdG9yY3ljbGVIZWxtZXQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN0dWRkZWRWZXN0IiwiY29sb3JzIjpbIkNsb3RoaW5nQmx1ZSIsIkNsb3RoaW5nRGFya0JsdWUiLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNsZWV2ZWxlc3NTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtQaW5rIixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJGaW5nZXJsZXNzR2xvdmVzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkBlue", "ClothingLightBlue"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkPink"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQmlrZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkhlYWRiYW5kIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3R1ZGRlZEphY2tldCIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLCJDbG90aGluZ0RhcmtCbHVlIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTbGVldmVsZXNzU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrUGluayIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU3VuR2xhc3NlcyIsImNvbG9ycyI6W251bGwsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX19
                    profiles.Add(new IProfile() { Name = "Biker", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightBlue"), Head = new IProfileClothingItem("Headband", "ClothingLightBlue"), ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkPink"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray"), });
                    break;
                }
                #endregion

                #region Bobby
                case BotType.Bobby:
                {
                    // https://profile-editor.vercel.app?p=M3oxjY1KgZ0i7j0q1Z3k440HZZ0O7Z0r7p
                    profiles.Add(new IProfile() { Name = "Bobby", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("BucketHat", "ClothingDarkGray", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("VestBlack", "ClothingDarkBlue", "ClothingDarkBlue"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("DogTag",""), });
                    break;
                }
                #endregion

                #region Bodyguard
                case BotType.Bodyguard:
                case BotType.Bodyguard2:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQm9keWd1YXJkIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW40IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdWl0SmFja2V0IiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTd2VhdGVyQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiU2hvZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJBZ2VudFN1bmdsYXNzZXMiLCJjb2xvcnMiOltudWxsLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Bodyguard", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray"), ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("AgentSunglasses", "", "ClothingLightGray"), });
                    break;
                }
                #endregion

                #region Boffin
                case BotType.Boffin:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj1jjj1LeZY0q1Z0Ajj0Wfk25vZ1BjZ
                    profiles.Add(new IProfile() { Name = "Boffin", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("Mohawk", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Coat", "ClothingLightGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingLightGray", "ClothingLightGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingYellow"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Glasses", "ClothingGreen", "ClothingLightGreen"), });
                    break;
                }
                #endregion
                
                #region Chairman
                case BotType.Chairman:
                {
                    // https://profile-editor.vercel.app?p=M1Fyk2woZ2ooZ0igj0qgZY0x2ZY0a2j
                    profiles.Add(new IProfile() { Name = "Chairman", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"), Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightRed"), Waist = new IProfileClothingItem("Belt", "ClothingLightBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("Shorts", "ClothingLightRed"), Feet = new IProfileClothingItem("BootsBlack", "ClothingLightBlue"), Accesory = new IProfileClothingItem("Cigar", "ClothingBrown"), });
                    break;
                }
                #endregion
                
                #region Cindy
                case BotType.Cindy:
                {
                    // https://profile-editor.vercel.app?p=F1Gxj1R6Z1M6Z2d6Z0q5Z3l7q2XZg0PeZ0v6Z
                    profiles.Add(new IProfile() { Name = "Cindy", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Cap", "ClothingDarkCyan"), ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingDarkGray", "ClothingOrange"), ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkCyan"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkCyan"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightBlue"), });
                    break;
                }
                #endregion

                #region ClownBodyguard
                case BotType.ClownBodyguard:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ2xvd24gQm9keWd1YXJkIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiTm9ybWFsX2ZlbSIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRDeWFuIixudWxsXX0sImhlYWQiOnsiaWQiOiJCdWNrZXRIYXQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEN5YW4iLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdWl0SmFja2V0X2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0Q3lhbiIsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiSGlnaEhlZWxzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRDeWFuIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQ2xvd25NYWtldXBfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRSZWQiLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Clown Bodyguard", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightCyan"), Head = new IProfileClothingItem("BucketHat", "ClothingLightCyan", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingLightCyan"), Feet = new IProfileClothingItem("HighHeels", "ClothingLightCyan"), Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ2xvd24gQm9keWd1YXJkIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiTm9ybWFsX2ZlbSIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IkJ1Y2tldEhhdCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0WWVsbG93IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VpdEphY2tldF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiSGlnaEhlZWxzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJDbG93bk1ha2V1cF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodFJlZCIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Clown Bodyguard", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("BucketHat", "ClothingLightYellow", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingLightYellow"), Feet = new IProfileClothingItem("HighHeels", "ClothingLightYellow"), Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ2xvd24gQm9keWd1YXJkIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiTm9ybWFsX2ZlbSIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nUGluayIsbnVsbF19LCJoZWFkIjp7ImlkIjoiQnVja2V0SGF0IiwiY29sb3JzIjpbIkNsb3RoaW5nUGluayIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1aXRKYWNrZXRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nUGluayIsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiSGlnaEhlZWxzIiwiY29sb3JzIjpbIkNsb3RoaW5nUGluayIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IkNsb3duTWFrZXVwX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0UmVkIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Clown Bodyguard", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingPink"), Head = new IProfileClothingItem("BucketHat", "ClothingPink", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingPink"), Feet = new IProfileClothingItem("HighHeels", "ClothingPink"), Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ2xvd24gQm9keWd1YXJkIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiTm9ybWFsX2ZlbSIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRHcmVlbiIsbnVsbF19LCJoZWFkIjp7ImlkIjoiQnVja2V0SGF0IiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRHcmVlbiIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1aXRKYWNrZXRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRHcmVlbiIsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiSGlnaEhlZWxzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRHcmVlbiIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IkNsb3duTWFrZXVwX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0UmVkIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Clown Bodyguard", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGreen"), Head = new IProfileClothingItem("BucketHat", "ClothingLightGreen", "ClothingLightGray"), ChestOver = new IProfileClothingItem("SuitJacket_fem", "ClothingLightGreen"), Feet = new IProfileClothingItem("HighHeels", "ClothingLightGreen"), Accesory = new IProfileClothingItem("ClownMakeup_fem", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region ClownBoxer
                case BotType.ClownBoxer:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ2xvd24gQm94ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1c3BlbmRlcnMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrT3JhbmdlIiwiQ2xvdGhpbmdPcmFuZ2UiLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ikdsb3ZlcyIsImNvbG9ycyI6WyJDbG90aGluZ1JlZCIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsXX0sImxlZ3MiOnsiaWQiOiJTdHJpcGVkUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodE9yYW5nZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJDbG93bk1ha2V1cCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0UmVkIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Clown Boxer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingDarkOrange", "ClothingOrange"), Hands = new IProfileClothingItem("Gloves", "ClothingRed"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightYellow"), Legs = new IProfileClothingItem("StripedPants", "ClothingLightOrange"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBlue"), Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region ClownCowboy
                case BotType.ClownCowboy:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ2xvd24gQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkZlZG9yYTIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLCJDbG90aGluZ1B1cnBsZSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJQb25jaG8iLCJjb2xvcnMiOlsiQ2xvdGhpbmdQdXJwbGUiLCJDbG90aGluZ0dyZWVuIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTaGlydFdpdGhCb3d0aWUiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodFllbGxvdyIsIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQW1tb0JlbHRXYWlzdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IkNhbW9QYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCcm93biIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IkNsb3duTWFrZXVwIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRSZWQiLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Clown Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Fedora2", "ClothingOrange", "ClothingPurple"), ChestOver = new IProfileClothingItem("Poncho", "ClothingPurple", "ClothingGreen"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingLightGray", "ClothingLightGray"), Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingLightBrown"), Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region ClownGangster
                case BotType.ClownGangster:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ2xvd24gR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjMiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiU3R5bGlzaEhhdCIsImNvbG9ycyI6WyJDbG90aGluZ1B1cnBsZSIsIkNsb3RoaW5nTGlnaHRHcmVlbiIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdXNwZW5kZXJzIiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTaGlydFdpdGhUaWUiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLCJDbG90aGluZ0dyYXkiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsXX0sImxlZ3MiOnsiaWQiOiJTdHJpcGVkUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdQdXJwbGUiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiU2hvZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0QnJvd24iLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJDbG93bk1ha2V1cCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0UmVkIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Clown Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("StylishHat", "ClothingPurple", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingLightYellow"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingGray"), Waist = new IProfileClothingItem("Belt", "ClothingBrown", "ClothingLightYellow"), Legs = new IProfileClothingItem("StripedPants", "ClothingPurple"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingLightBrown"), Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region Cowboy
                case BotType.Cowboy:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkNvd2JveUhhdCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0QnJvd24iLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJWZXN0IiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0Jyb3duIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTaGlydFdpdGhCb3d0aWUiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLCJDbG90aGluZ0RhcmtHcmF5IixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCcm93biIsIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlJpZGluZ0Jvb3RzIiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("CowboyHat", "ClothingLightBrown", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Vest", "ClothingBrown", "ClothingBrown"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkZlZG9yYSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0QnJvd24iLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlVuYnV0dG9uZWRTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtPcmFuZ2UiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IkZpbmdlcmxlc3NHbG92ZXMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJBbW1vQmVsdFdhaXN0IiwiY29sb3JzIjpbIkNsb3RoaW5nT3JhbmdlIixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1JlZCIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU2NhcmYiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodE9yYW5nZSIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("Fedora", "ClothingLightBrown", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkOrange"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkYellow"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingOrange"), Legs = new IProfileClothingItem("Pants", "ClothingDarkRed"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), Accesory = new IProfileClothingItem("Scarf", "ClothingLightOrange"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkZlZG9yYTIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCcm93biIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVW5idXR0b25lZFNoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkJlbHQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrT3JhbmdlIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlJpZGluZ0Jvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrT3JhbmdlIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiU2NhcmYiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Fedora2", "ClothingBrown", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingLightYellow"), Waist = new IProfileClothingItem("Belt", "ClothingDarkOrange", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkOrange"), Accesory = new IProfileClothingItem("Scarf", "ClothingLightYellow"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkNvd2JveUhhdCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0QnJvd24iLCJDbG90aGluZ0xpZ2h0R3JlZW4iLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJMdW1iZXJqYWNrU2hpcnQyIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1BpbmsiLCJDbG90aGluZ0RhcmtQaW5rIixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCcm93biIsIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("CowboyHat", "ClothingLightBrown", "ClothingLightGreen"), ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingDarkPink", "ClothingDarkPink"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJWZXN0IiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0Jyb3duIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTaGlydFdpdGhCb3d0aWUiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLCJDbG90aGluZ0RhcmtHcmF5IixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQW1tb0JlbHRXYWlzdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCcm93biIsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0Qmx1ZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Vest", "ClothingBrown", "ClothingBrown"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkBrown"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkNvd2JveUhhdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCcm93biIsIkNsb3RoaW5nTGlnaHRCcm93biIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6Ikx1bWJlcmphY2tTaGlydDIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrUmVkIiwiQ2xvdGhpbmdEYXJrUmVkIixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQW1tb0JlbHRXYWlzdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCcm93biIsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("CowboyHat", "ClothingDarkBrown", "ClothingLightBrown"), ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingDarkRed", "ClothingDarkRed"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkBrown"), Legs = new IProfileClothingItem("Pants", "ClothingLightGray"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ293Ym95IiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkNvd2JveUhhdCIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiVmVzdCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IiwiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2hpcnRXaXRoQm93dGllIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRHcmF5IiwiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkJlbHQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQnJvd24iLCJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0Qmx1ZSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJSaWRpbmdCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Cowboy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("CowboyHat", "ClothingBrown", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Vest", "ClothingDarkGray", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingBrown"), });
                    break;
                }
                #endregion

                #region Cyborg
                case BotType.Cyborg:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ3lib3JnIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW41IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6Ikhhem1hdE1hc2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLCJDbG90aGluZ0xpZ2h0UGluayIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IkJvZHlBcm1vciIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiRmluZ2VybGVzc0dsb3Zlc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRPcmFuZ2UiLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IlNtYWxsQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0Q3lhbiIsIkNsb3RoaW5nTGlnaHRPcmFuZ2UiLG51bGxdfSwibGVncyI6eyJpZCI6IkNhbW9QYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IiwiQ2xvdGhpbmdHcmF5IixudWxsXX0sImZlZXQiOnsiaWQiOiJTbmVha2VycyIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0Q3lhbiIsIkNsb3RoaW5nRGFya0N5YW4iLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cyborg", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), Head = new IProfileClothingItem("HazmatMask", "ClothingLightGray", "ClothingLightPink"), ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightOrange"), Waist = new IProfileClothingItem("SmallBelt", "ClothingLightCyan", "ClothingLightOrange"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGray", "ClothingGray"), Feet = new IProfileClothingItem("Sneakers", "ClothingLightCyan", "ClothingDarkCyan"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ3lib3JnIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW41IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6Ikhhem1hdE1hc2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrUHVycGxlIiwiQ2xvdGhpbmdMaWdodE9yYW5nZSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IkJvZHlBcm1vciIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtQdXJwbGUiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IkZpbmdlcmxlc3NHbG92ZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ1B1cnBsZSIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiU2F0Y2hlbEJlbHQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrT3JhbmdlIixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IkNhbW9QYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtQdXJwbGUiLCJDbG90aGluZ0RhcmtQdXJwbGUiLG51bGxdfSwiZmVldCI6eyJpZCI6IlNuZWFrZXJzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1B1cnBsZSIsIkNsb3RoaW5nRGFya1B1cnBsZSIsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Cyborg", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), Head = new IProfileClothingItem("HazmatMask", "ClothingDarkPurple", "ClothingLightOrange"), ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingDarkPurple"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingPurple"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkOrange"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkPurple", "ClothingDarkPurple"), Feet = new IProfileClothingItem("Sneakers", "ClothingDarkPurple", "ClothingDarkPurple"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ3lib3JnIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiVGF0dG9vc19mZW0iLCJjb2xvcnMiOlsiU2tpbjUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiSGF6bWF0TWFzayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtDeWFuIiwiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJCb2R5QXJtb3JfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nQ3lhbiIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiRmluZ2VybGVzc0dsb3Zlc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRDeWFuIixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IkNhbW9QYW50c19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQ3lhbiIsIkNsb3RoaW5nQ3lhbiIsbnVsbF19LCJmZWV0Ijp7ImlkIjoiU25lYWtlcnMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdDeWFuIiwiQ2xvdGhpbmdMaWdodEN5YW4iLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiVml6b3IiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsIkNsb3RoaW5nTGlnaHRSZWQiLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Cyborg", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin5", "ClothingLightGray"), Head = new IProfileClothingItem("HazmatMask", "ClothingDarkCyan", "ClothingLightBlue"), ChestUnder = new IProfileClothingItem("BodyArmor_fem", "ClothingCyan"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightCyan"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkCyan", "ClothingCyan"), Feet = new IProfileClothingItem("Sneakers", "ClothingCyan", "ClothingLightCyan"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ3lib3JnIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiVGF0dG9vc19mZW0iLCJjb2xvcnMiOlsiU2tpbjUiLCJDbG90aGluZ0xpZ2h0Q3lhbiIsbnVsbF19LCJoZWFkIjp7ImlkIjoiSGF6bWF0TWFzayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IiwiQ2xvdGhpbmdMaWdodFJlZCIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IkJvZHlBcm1vcl9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJGaW5nZXJsZXNzR2xvdmVzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodFJlZCIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJDYW1vUGFudHNfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLCJDbG90aGluZ0dyYXkiLG51bGxdfSwiZmVldCI6eyJpZCI6IlNuZWFrZXJzIiwiY29sb3JzIjpbIkNsb3RoaW5nUmVkIiwiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJBcm1iYW5kX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ1JlZCIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cyborg", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin5", "ClothingLightCyan"), Head = new IProfileClothingItem("HazmatMask", "ClothingDarkGray", "ClothingLightRed"), ChestUnder = new IProfileClothingItem("BodyArmor_fem", "ClothingGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightRed"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkGray", "ClothingGray"), Feet = new IProfileClothingItem("Sneakers", "ClothingRed", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Armband_fem", "ClothingRed"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ3lib3JnIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW41IiwiQ2xvdGhpbmdMaWdodEN5YW4iLG51bGxdfSwiaGVhZCI6eyJpZCI6Ikhhem1hdE1hc2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IiwiQ2xvdGhpbmdMaWdodE9yYW5nZSIsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IkJvZHlBcm1vciIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IlNhZmV0eUdsb3ZlcyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiQ2Ftb1BhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLCJDbG90aGluZ0dyYXkiLG51bGxdfSwiZmVldCI6eyJpZCI6IlNuZWFrZXJzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsIkNsb3RoaW5nRGFya0dyYXkiLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cyborg", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightCyan"), Head = new IProfileClothingItem("HazmatMask", "ClothingGray", "ClothingLightOrange"), ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingGray"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGray", "ClothingGray"), Feet = new IProfileClothingItem("Sneakers", "ClothingGray", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiQ3lib3JnIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiVGF0dG9vc19mZW0iLCJjb2xvcnMiOlsiU2tpbjUiLCJDbG90aGluZ0xpZ2h0Q3lhbiIsbnVsbF19LCJoZWFkIjp7ImlkIjoiSGF6bWF0TWFzayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIiwiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJCb2R5QXJtb3JfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IkZpbmdlcmxlc3NHbG92ZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IkNhbW9QYW50c19mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsIkNsb3RoaW5nQmx1ZSIsbnVsbF19LCJmZWV0Ijp7ImlkIjoiU2hvZXMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrQmx1ZSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6IlZpem9yIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLCJDbG90aGluZ0xpZ2h0UmVkIixudWxsXX19
                    profiles.Add(new IProfile() { Name = "Cyborg", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin5", "ClothingLightCyan"), Head = new IProfileClothingItem("HazmatMask", "ClothingDarkBlue", "ClothingLightBlue"), ChestUnder = new IProfileClothingItem("BodyArmor_fem", "ClothingDarkBlue"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingLightYellow"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkBlue", "ClothingBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBlue"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region Demolitionist
                case BotType.Demolitionist:
                {
                    // https://profile-editor.vercel.app?p=M1FAj36eZ1L6Z0iej0qeZ13ZZ01Zj0XeZY
                    profiles.Add(new IProfile() { Name = "The Demolitionist", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), ChestOver = new IProfileClothingItem("GrenadeBelt",""), ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray"), Hands = new IProfileClothingItem("Gloves", "ClothingGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("AgentSunglasses", "", "ClothingLightGray"), });
                    break;
                }
                #endregion

                #region Demoman
                case BotType.Demoman:
                {
                    // https://profile-editor.vercel.app?p=M1Fwj2w7Z1KhZY0p7Z13ZZ087ZY3r7Z
                    profiles.Add(new IProfile() { Name = "Demoman", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("WoolCap", "ClothingDarkGray"), ChestOver = new IProfileClothingItem("GrenadeBelt", ""), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBrown"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Armband", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Elf
                case BotType.Elf:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRWxmIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiVGF0dG9vcyIsImNvbG9ycyI6WyJTa2luMyIsIkNsb3RoaW5nUGluayIsbnVsbF19LCJoZWFkIjp7ImlkIjoiU2FudGFIYXQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmVlbiIsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiTGVhdGhlckphY2tldCIsImNvbG9ycyI6WyJDbG90aGluZ0dyZWVuIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyZWVuIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JlZW4iLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Elf", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink"), Head = new IProfileClothingItem("SantaHat", "ClothingGreen"), ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingGreen", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGreen", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingGreen"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRWxmIiwiZ2VuZGVyIjoxLCJza2luIjp7ImlkIjoiVGF0dG9vc19mZW0iLCJjb2xvcnMiOlsiU2tpbjMiLCJDbG90aGluZ1BpbmsiLG51bGxdfSwiaGVhZCI6eyJpZCI6IlNhbnRhSGF0IiwiY29sb3JzIjpbIkNsb3RoaW5nR3JlZW4iLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IkxlYXRoZXJKYWNrZXRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JlZW4iLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkJlbHRfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyZWVuIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzX2ZlbSIsImNvbG9ycyI6WyJDbG90aGluZ0dyZWVuIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Elf", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin3", "ClothingPink"), Head = new IProfileClothingItem("SantaHat", "ClothingGreen"), ChestUnder = new IProfileClothingItem("LeatherJacket_fem", "ClothingGreen", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGreen", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingGreen"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Engineer
                case BotType.Engineer:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRW5naW5lZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiUGl0aEhlbG1ldCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IkphY2tldCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJUU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdSZWQiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6IlNhZmV0eUdsb3ZlcyIsImNvbG9ycyI6WyJDbG90aGluZ09yYW5nZSIsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQ29tYmF0QmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Engineer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("PithHelmet", "ClothingLightGray", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Jacket", "ClothingLightGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange"), Waist = new IProfileClothingItem("CombatBelt", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRW5naW5lZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiUGl0aEhlbG1ldCIsImNvbG9ycyI6WyJDbG90aGluZ1llbGxvdyIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IktldmxhclZlc3QiLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVFNoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nUmVkIixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJTYWZldHlHbG92ZXMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkNvbWJhdEJlbHQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiU2hvZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiR2xhc3NlcyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Engineer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("PithHelmet", "ClothingYellow", "ClothingLightGray"), ChestOver = new IProfileClothingItem("KevlarVest", "ClothingOrange"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange"), Waist = new IProfileClothingItem("CombatBelt", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Glasses", "ClothingDarkGray", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRW5naW5lZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiUGl0aEhlbG1ldCIsImNvbG9ycyI6WyJDbG90aGluZ1llbGxvdyIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlNob3VsZGVySG9sc3RlciIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlRTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ1JlZCIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiU2FmZXR5R2xvdmVzIiwiY29sb3JzIjpbIkNsb3RoaW5nT3JhbmdlIixudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJDb21iYXRCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Engineer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("PithHelmet", "ClothingYellow", "ClothingLightGray"), ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingDarkGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange"), Waist = new IProfileClothingItem("CombatBelt", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRW5naW5lZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjQiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiV2VsZGluZ0hlbG1ldCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU2hvdWxkZXJIb2xzdGVyIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVFNoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nUmVkIixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJTYWZldHlHbG92ZXMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6IkNvbWJhdEJlbHQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiU2hvZXNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Engineer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("WeldingHelmet", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingDarkGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingOrange"), Waist = new IProfileClothingItem("CombatBelt", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Farmer
                case BotType.Farmer:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRmFybWVyIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkZlZG9yYTIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdZZWxsb3ciLCJDbG90aGluZ0dyYXkiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VzcGVuZGVycyIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLCJDbG90aGluZ0RhcmtCbHVlIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJUU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdSZWQiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0N5YW4iLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Farmer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Fedora2", "ClothingYellow", "ClothingGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingRed"), Legs = new IProfileClothingItem("Pants", "ClothingBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkCyan"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRmFybWVyIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiVGF0dG9vcyIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nUGluayIsbnVsbF19LCJoZWFkIjp7ImlkIjoiU2VyZ2VhbnRIYXQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrWWVsbG93IiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1c3BlbmRlcnMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdDeWFuIiwiQ2xvdGhpbmdDeWFuIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTbGVldmVsZXNzU2hpcnRCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtDeWFuIixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nQ3lhbiIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lcyIsImNvbG9ycyI6WyJDbG90aGluZ0N5YW4iLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJDaWdhciIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Farmer", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingPink"), Head = new IProfileClothingItem("SergeantHat", "ClothingDarkYellow", "ClothingDarkYellow"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingCyan", "ClothingCyan"), ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingDarkCyan"), Legs = new IProfileClothingItem("Pants", "ClothingCyan"), Feet = new IProfileClothingItem("Shoes", "ClothingCyan"), Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiRmFybWVyIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IlJpY2VIYXQiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlVuYnV0dG9uZWRTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiU2hvcnRzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Farmer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("RiceHat", ""), ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingGray"), Feet = new IProfileClothingItem("Shoes", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Firebug
                case BotType.Firebug:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj2xtZ2pcZ0EjZ0q1Z303j0S3p253Z18qZ
                    profiles.Add(new IProfile() { Name = "Firebug", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingOrange"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingCyan", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingRed"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightGray"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkRed"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("GasMask", "ClothingCyan", "ClothingLightYellow"), });
                    break;
                }
                #endregion

                #region Fireman
                case BotType.Fireman:
                {
                    // https://profile-editor.vercel.app?p=M1Fyo1Q7Z1K7Z2Avv2ndZ1xev2XZp10eZ1Opv
                    profiles.Add(new IProfile() { Name = "Fireman", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightRed"), Head = new IProfileClothingItem("PithHelmet", "ClothingLightYellow", "ClothingYellow"), ChestOver = new IProfileClothingItem("MilitaryJacket", "ClothingGray", "ClothingYellow"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("SmallBelt", "ClothingYellow", "ClothingYellow"), Legs = new IProfileClothingItem("Pants", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkYellow"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightYellow"), });
                    break;
                }
                #endregion

                #region Fritzliebe
                case BotType.Fritzliebe:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj1jjj1LeZY0q1ZY08tZ251Z0Kjj
                    profiles.Add(new IProfile() { Name = "Dr. Fritzliebe", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("FLDisguise", "ClothingLightGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingLightGray", "ClothingLightGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    break;
                }
                #endregion

                #region Funnyman
                case BotType.Funnyman:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj2hvg2HgZY2npZ2JgZ0yoZ0XjZY
                    profiles.Add(new IProfile() { Name = "Funnyman", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestOver = new IProfileClothingItem("StripedSuitJacket", "ClothingLightBlue"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingYellow", "ClothingLightBlue"), Hands = new IProfileClothingItem("Gloves", "ClothingLightGray"), Legs = new IProfileClothingItem("StripedPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingLightYellow"), Accesory = new IProfileClothingItem("ClownMakeup", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region Jo
                case BotType.Jo:
                {
                    // https://profile-editor.vercel.app?p=F1Gwj2zjZ3cgZ2Bhj0q9ZY0x7ZYY
                    profiles.Add(new IProfile() { Name = "Jo", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray"), Waist = new IProfileClothingItem("SmallBelt_fem", "ClothingLightBrown", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkOrange"), Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Hacker
                case BotType.Hacker:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj36eZ1KeZY0pqZ1d7i128i24jZ0g7i
                    profiles.Add(new IProfile() { Name = "Hacker", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("BaseballCap", "ClothingDarkGray", "ClothingLightCyan"), ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkGray", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("Boots", "ClothingOrange"), Accesory = new IProfileClothingItem("Goggles", "ClothingDarkGreen", "ClothingLightCyan"), });
                    // https://profile-editor.vercel.app?p=M1Fyj36eZ1KeZY0pqZ1d7i3n7o24jZ0g7i
                    profiles.Add(new IProfile() { Name = "Hacker", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("BaseballCap", "ClothingDarkGray", "ClothingLightCyan"), ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkGray", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("Boots", "ClothingOrange"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region Hawkeye
                case BotType.Hawkeye:
                {
                    // https://profile-editor.vercel.app?p=M3ozi1z4g0te4031Z2m7Z027Z127l0XeZ0k4p
                    profiles.Add(new IProfile() { Name = "Hawkeye", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin4", "ClothingLightCyan"), Head = new IProfileClothingItem("Beret", "ClothingDarkBlue", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkBlue", "ClothingLightBlue"), Hands = new IProfileClothingItem("Gloves", "ClothingGray"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingBlue"), Legs = new IProfileClothingItem("CamoPants", "ClothingGray", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Goggles", "ClothingDarkGray", "ClothingLightOrange"), });
                    break;
                }
                #endregion

                #region HeavySoldier
                case BotType.HeavySoldier:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj1zdt0ted2cdZ0qeZ1heZ0TeiY1adZ
                    profiles.Add(new IProfile() { Name = "Heavy Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("Helmet2", "ClothingDarkYellow"), ChestOver = new IProfileClothingItem("KevlarVest", "ClothingGray"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingRed"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants", "ClothingGray", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("GasMask2", "ClothingGray", "ClothingLightCyan"), });
                    break;
                }
                #endregion

                #region Hitman
                case BotType.Hitman:
                {
                    // https://profile-editor.vercel.app?p=M1Fyk2jjo1L7Z2Aee2m7Z2U7ZY10eZY
                    profiles.Add(new IProfile() { Name = "Hitman", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingLightRed"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("SmallBelt", "ClothingGray", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M1Fyk2jjo1KjZY2neZ2TjZY0X7ZY
                    profiles.Add(new IProfile() { Name = "Hitman", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("SuitJacket", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingLightRed"), Hands = new IProfileClothingItem("Gloves", "ClothingDarkGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Huntsman
                case BotType.Huntsman:
                {
                    // https://profile-editor.vercel.app?p=M38ye36aZ1KeZY2n2Z3jej1r7Z0O9Z0Ghe
                    profiles.Add(new IProfile() { Name = "Huntsman", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingGray"), Head = new IProfileClothingItem("CowboyHat", "ClothingLightBrown", "ClothingGray"), ChestOver = new IProfileClothingItem("Vest", "ClothingGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkPink"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkOrange"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingBrown"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Gangster
                case BotType.Gangster:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjIiLCJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VzcGVuZGVycyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNoaXJ0V2l0aFRpZSIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsIkNsb3RoaW5nRGFya1BpbmsiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiRmxhdGNhcCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJCbGF6ZXJXaXRoU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lc0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("Flatcap", "ClothingGray"), ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkYellow"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW40IiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1aXRKYWNrZXQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNoaXJ0V2l0aFRpZSIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0RhcmtQaW5rIixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW40IiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiRmxhdGNhcCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJCbGF6ZXJXaXRoU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IiwiQ2xvdGhpbmdEYXJrUGluayIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow"), Head = new IProfileClothingItem("Flatcap", "ClothingGray"), ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiRmVkb3JhIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsIkNsb3RoaW5nRGFya1BpbmsiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiSmFja2V0IiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsIkNsb3RoaW5nR3JheSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2hpcnRXaXRoVGllIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya1llbGxvdyIsIkNsb3RoaW5nRGFya1BpbmsiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJCZWx0IiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink"), ChestOver = new IProfileClothingItem("Jacket", "ClothingGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingDarkYellow", "ClothingDarkPink"), Waist = new IProfileClothingItem("Belt", "ClothingBrown", "ClothingDarkYellow"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjMiLCJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsXX0sImhlYWQiOnsiaWQiOiJGbGF0Y2FwIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlN1c3BlbmRlcnMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJTaGlydFdpdGhUaWUiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLCJDbG90aGluZ0RhcmtQaW5rIixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("Flatcap", "ClothingGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4xIiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiU3R5bGlzaEhhdCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0RhcmtQaW5rIixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IkJsYXplcldpdGhTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow"), Head = new IProfileClothingItem("StylishHat", "ClothingGray", "ClothingDarkPink"), ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkYellow"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiRmVkb3JhIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsIkNsb3RoaW5nRGFya1BpbmsiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VpdEphY2tldCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2hpcnRXaXRoVGllIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsIkNsb3RoaW5nRGFya1BpbmsiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink"), ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4xIiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiRmxhdGNhcCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJKYWNrZXQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IiwiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiU2hpcnRXaXRoVGllIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRHcmF5IiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0xpZ2h0WWVsbG93IixudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow"), Head = new IProfileClothingItem("Flatcap", "ClothingGray"), ChestOver = new IProfileClothingItem("Jacket", "ClothingGray", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkYellow"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IkphY2tldCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0dyYXkiLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlNoaXJ0V2l0aFRpZSIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtZZWxsb3ciLCJDbG90aGluZ0RhcmtQaW5rIixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiQmVsdCIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("Jacket", "ClothingGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingDarkYellow", "ClothingDarkPink"), Waist = new IProfileClothingItem("Belt", "ClothingBrown", "ClothingDarkYellow"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJUYXR0b29zIiwiY29sb3JzIjpbIlNraW4yIiwiQ2xvdGhpbmdMaWdodFllbGxvdyIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IkJsYXplcldpdGhTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0RhcmtQaW5rIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJCb290c0JsYWNrIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJUYXR0b29zX2ZlbSIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IkZlZG9yYSIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0RhcmtQaW5rIixudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IkJsYXplcldpdGhTaGlydF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IiwiQ2xvdGhpbmdEYXJrUGluayIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJTa2lydF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkhpZ2hIZWVscyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtQaW5rIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Gangster", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink"), ChestOver = new IProfileClothingItem("BlazerWithShirt_fem", "ClothingGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Skirt_fem", "ClothingGray"), Feet = new IProfileClothingItem("HighHeels", "ClothingDarkPink"), });
                    break;
                }
                #endregion

                #region GangsterHulk
                case BotType.GangsterHulk:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIgSHVsayIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMyIsIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IkZsYXRjYXAiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VzcGVuZGVycyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJUU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Gangster Hulk", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("Flatcap", "ClothingGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIgSHVsayIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdXNwZW5kZXJzIiwiY29sb3JzIjpbIkNsb3RoaW5nQnJvd24iLCJDbG90aGluZ0RhcmtZZWxsb3ciLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlRTaGlydCIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJDaWdhciIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfX0-
                    profiles.Add(new IProfile() { Name = "Gangster Hulk", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Cigar", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FuZ3N0ZXIgSHVsayIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMyIsIkNsb3RoaW5nTGlnaHRZZWxsb3ciLG51bGxdfSwiaGVhZCI6eyJpZCI6IkJ1Y2tldEhhdCIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0dyYXkiLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VzcGVuZGVycyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIiwiQ2xvdGhpbmdEYXJrWWVsbG93IixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJUU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IkJvb3RzQmxhY2siLCJjb2xvcnMiOlsiQ2xvdGhpbmdHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Gangster Hulk", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("BucketHat", "ClothingGray", "ClothingGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingBrown", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Gardener
                case BotType.Gardener:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FyZGVuZXIiLCJnZW5kZXIiOjEsInNraW4iOnsiaWQiOiJOb3JtYWxfZmVtIiwiY29sb3JzIjpbIlNraW4zIiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJBcHJvbl9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdDeWFuIixudWxsLG51bGxdfSwiY2hlc3RVbmRlciI6eyJpZCI6IlRTaGlydF9mZW0iLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEJsdWUiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiUGFudHNfZmVtIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0JsdWUiLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Gardener", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Apron_fem", "ClothingCyan"), ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingLightBlue"), Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiR2FyZGVuZXIiLCJnZW5kZXIiOjAsInNraW4iOnsiaWQiOiJOb3JtYWwiLCJjb2xvcnMiOlsiU2tpbjEiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJoZWFkIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImNoZXN0T3ZlciI6eyJpZCI6IlNob3VsZGVySG9sc3RlciIsImNvbG9ycyI6WyJDbG90aGluZ0N5YW4iLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVFNoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nRGFya0N5YW4iLG51bGwsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHNCbGFjayIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtHcmF5IixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Gardener", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingCyan", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Handler
                case BotType.Handler:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj36jZ2o2Z0ih22DejY2XZkY0vjZ
                    profiles.Add(new IProfile() { Name = "Handler", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Cap", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingLightBrown", "ClothingBrown"), Legs = new IProfileClothingItem("Shorts", "ClothingBrown"), Feet = new IProfileClothingItem("Sneakers", "ClothingGray", "ClothingLightGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGreen"), });
                    break;
                }
                #endregion

                #region Hunter
                case BotType.Hunter:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiSHVudGVyIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW40IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkNhcCIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtDeWFuIixudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiSmFja2V0IiwiY29sb3JzIjpbIkNsb3RoaW5nQ3lhbiIsIkNsb3RoaW5nTGlnaHRDeWFuIixudWxsXX0sImNoZXN0VW5kZXIiOnsiaWQiOiJUU2hpcnQiLCJjb2xvcnMiOlsiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGwsbnVsbF19LCJoYW5kcyI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJ3YWlzdCI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19LCJsZWdzIjp7ImlkIjoiQ2Ftb1BhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRDeWFuIiwiQ2xvdGhpbmdEYXJrQ3lhbiIsbnVsbF19LCJmZWV0Ijp7ImlkIjoiQm9vdHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdCcm93biIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik5vbmUiLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Hunter", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("Cap", "ClothingDarkCyan"), ChestOver = new IProfileClothingItem("Jacket", "ClothingCyan", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingLightCyan", "ClothingDarkCyan"), Feet = new IProfileClothingItem("Boots", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiSHVudGVyIiwiZ2VuZGVyIjowLCJza2luIjp7ImlkIjoiTm9ybWFsIiwiY29sb3JzIjpbIlNraW40IiwiQ2xvdGhpbmdMaWdodEdyYXkiLG51bGxdfSwiaGVhZCI6eyJpZCI6IkhlYWRiYW5kIiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRPcmFuZ2UiLG51bGwsbnVsbF19LCJjaGVzdE92ZXIiOnsiaWQiOiJTdHVkZGVkVmVzdCIsImNvbG9ycyI6WyJDbG90aGluZ09yYW5nZSIsIkNsb3RoaW5nRGFya09yYW5nZSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiVFNoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJDYW1vUGFudHMiLCJjb2xvcnMiOlsiQ2xvdGhpbmdPcmFuZ2UiLCJDbG90aGluZ0xpZ2h0T3JhbmdlIixudWxsXX0sImZlZXQiOnsiaWQiOiJCb290cyIsImNvbG9ycyI6WyJDbG90aGluZ0Jyb3duIixudWxsLG51bGxdfSwiYWNjZXNzb3J5Ijp7ImlkIjoiQ2lnYXIiLCJjb2xvcnMiOlsiQ2xvdGhpbmdEYXJrR3JheSIsbnVsbCxudWxsXX19
                    profiles.Add(new IProfile() { Name = "Hunter", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingLightOrange"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingOrange", "ClothingDarkOrange"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingOrange", "ClothingLightOrange"), Feet = new IProfileClothingItem("Boots", "ClothingBrown"), Accesory = new IProfileClothingItem("Cigar", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Incinerator
                case BotType.Incinerator:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj1kdq2pdZ0idl0q9ZY0Sdl25dZ18qZ
                    profiles.Add(new IProfile() { Name = "The Incinerator", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingOrange"), ChestUnder = new IProfileClothingItem("LeatherJacketBlack", "ClothingDarkYellow", "ClothingOrange"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingDarkYellow"), Waist = new IProfileClothingItem("Belt", "ClothingDarkYellow", "ClothingLightOrange"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkOrange"), Accesory = new IProfileClothingItem("GasMask", "ClothingDarkYellow", "ClothingLightOrange"), });
                    break;
                }
                #endregion

                #region Ion
                case BotType.Ion:
                {
                    // https://profile-editor.vercel.app?p=M3owo2geZ1LtZY2neZY1W7Z0X7Z0M7e
                    profiles.Add(new IProfile() { Name = "Ion", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin1", "ClothingLightRed"), Head = new IProfileClothingItem("Fedora2", "ClothingDarkGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("Shirt", "ClothingGray"), Hands = new IProfileClothingItem("Gloves", "ClothingDarkGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingRed"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Kingpin
                case BotType.Kingpin:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj2hr72HeZY2neZ2JeZY0XjZ3a7r
                    profiles.Add(new IProfile() { Name = "Kingpin", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("TopHat", "ClothingDarkGray", "ClothingPink"), ChestOver = new IProfileClothingItem("StripedSuitJacket", "ClothingGray"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingPink", "ClothingDarkGray"), Hands = new IProfileClothingItem("Gloves", "ClothingLightGray"), Legs = new IProfileClothingItem("StripedPants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Kriegbär
                case BotType.Kriegbar:
                {
                    // https://profile-editor.vercel.app?p=M0RZgYYYYYYYY
                    profiles.Add(new IProfile() { Name = "Kriegbär #2", Gender = Gender.Male, Skin = new IProfileClothingItem("FrankenbearSkin", "", "ClothingLightBlue"), });
                    break;
                }
                #endregion

                #region LabAssistant
                case BotType.LabAssistant:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj363Z1K3ZY0qeZY1riZ243ZY
                    profiles.Add(new IProfile() { Name = "Lab Assistant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingCyan"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingCyan"), Legs = new IProfileClothingItem("Pants", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Mask", "ClothingLightCyan"), });
                    // https://profile-editor.vercel.app?p=F1Gzj373Z1N3ZY0qeZY3n3iYY
                    profiles.Add(new IProfile() { Name = "Lab Assistant", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingCyan"), Legs = new IProfileClothingItem("Pants_fem", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingCyan", "ClothingLightCyan"), });
                    break;
                }
                #endregion

                #region Lumberjack
                case BotType.Lumberjack:
                {
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiTHVtYmVyamFjayIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImhlYWQiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VzcGVuZGVycyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiTHVtYmVyamFja1NoaXJ0IiwiY29sb3JzIjpbIkNsb3RoaW5nTGlnaHRSZWQiLCJDbG90aGluZ0dyYXkiLG51bGxdfSwiaGFuZHMiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwid2Fpc3QiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwibGVncyI6eyJpZCI6IlBhbnRzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImZlZXQiOnsiaWQiOiJTaG9lcyIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19LCJhY2Nlc3NvcnkiOnsiaWQiOiJTYW50YU1hc2siLCJjb2xvcnMiOltudWxsLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Lumberjack", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("LumberjackShirt", "ClothingLightRed", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("Shoes", "ClothingGray"), Accesory = new IProfileClothingItem("SantaMask", ""), });
                    // https://profile-editor.vercel.app?tab=profile&p=eyJuYW1lIjoiTHVtYmVyamFjayIsImdlbmRlciI6MCwic2tpbiI6eyJpZCI6Ik5vcm1hbCIsImNvbG9ycyI6WyJTa2luMiIsIkNsb3RoaW5nTGlnaHRHcmF5IixudWxsXX0sImhlYWQiOnsiaWQiOiJOb25lIiwiY29sb3JzIjpbbnVsbCxudWxsLG51bGxdfSwiY2hlc3RPdmVyIjp7ImlkIjoiU3VzcGVuZGVycyIsImNvbG9ycyI6WyJDbG90aGluZ0JsdWUiLCJDbG90aGluZ0xpZ2h0R3JheSIsbnVsbF19LCJjaGVzdFVuZGVyIjp7ImlkIjoiTHVtYmVyamFja1NoaXJ0MiIsImNvbG9ycyI6WyJDbG90aGluZ0xpZ2h0UmVkIiwiQ2xvdGhpbmdHcmF5IixudWxsXX0sImhhbmRzIjp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sIndhaXN0Ijp7ImlkIjoiTm9uZSIsImNvbG9ycyI6W251bGwsbnVsbCxudWxsXX0sImxlZ3MiOnsiaWQiOiJQYW50cyIsImNvbG9ycyI6WyJDbG90aGluZ0RhcmtCbHVlIixudWxsLG51bGxdfSwiZmVldCI6eyJpZCI6IlNob2VzIiwiY29sb3JzIjpbIkNsb3RoaW5nR3JheSIsbnVsbCxudWxsXX0sImFjY2Vzc29yeSI6eyJpZCI6Ik1vdXN0YWNoZSIsImNvbG9ycyI6WyJDbG90aGluZ0dyYXkiLG51bGwsbnVsbF19fQ--
                    profiles.Add(new IProfile() { Name = "Lumberjack", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingBlue", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingLightRed", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingGray"), Accesory = new IProfileClothingItem("Moustache", "ClothingGray"), });
                    break;
                }
                #endregion

                #region LordPinkerton
                case BotType.LordPinkerton:
                {
                    // https://profile-editor.vercel.app?p=M1FAj2O1Z1KgZ2cqZ0q1Z0Aee2N1Z0O7ZY
                    profiles.Add(new IProfile() { Name = "Lord Pinkerton", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Coat", "ClothingGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("StuddedLeatherSuit", "ClothingBlue"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingOrange"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("StuddedLeatherMask", "ClothingBlue"), });
                    break;
                }
                #endregion

                #region Meatgrinder
                case BotType.Meatgrinder:
                {
                    // https://profile-editor.vercel.app?p=M38yrY2peZY0qeZ06mZ11ZZ25cZ0wjZ
                    profiles.Add(new IProfile() { Name = "The Meatgrinder", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink"), Head = new IProfileClothingItem("ChefHat", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Apron", "ClothingLightPink"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingDarkRed"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("GoalieMask", ""), });
                    break;
                }
                #endregion

                #region Mecha
                case BotType.Mecha:
                {
                    // https://profile-editor.vercel.app?p=M1sjoYYYYYYYY
                    profiles.Add(new IProfile() { Name = "Mecha Fritzliebe", Gender = Gender.Male, Skin = new IProfileClothingItem("MechSkin", "ClothingLightGray", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region MetroCop
                case BotType.MetroCop:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj2wfZ1LeZY0qeZ1ueeY25eZ1tek
                    profiles.Add(new IProfile() { Name = "MetroCop", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("MetroLawGasMask", "ClothingGray", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M1Fyo2wfZ1LeZY0qeZ1ueeY25eZ1wek
                    profiles.Add(new IProfile() { Name = "MetroCop", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightRed"), Head = new IProfileClothingItem("MetroLawMask", "ClothingGray", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38Aj0neZ1LeZ0EeZ0qeZYY25eZ1teo
                    profiles.Add(new IProfile() { Name = "MetroCop", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin5", "ClothingLightGray"), Head = new IProfileClothingItem("MetroLawGasMask", "ClothingGray", "ClothingLightRed"), ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("CombatBelt", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region MetroCop2
                case BotType.MetroCop2:
                {
                    // https://profile-editor.vercel.app?p=M1Fyo2wfZ1LeZY0qeZ1uee0JZZ25eZY
                    profiles.Add(new IProfile() { Name = "MetroCop Chief", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightRed"), ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Earpiece", ""), });
                    // https://profile-editor.vercel.app?p=M1Fyj0neZ1LeZ0EeZ0qeZY0JZZ25eZY
                    profiles.Add(new IProfile() { Name = "MetroCop Chief", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("CombatBelt", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Earpiece", ""), });
                    // https://profile-editor.vercel.app?p=M1Fyo2wfZ1LeZY0qeZ1ueeY25eZY
                    profiles.Add(new IProfile() { Name = "MetroCop Chief", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightRed"), ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGreen"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Mutant
                case BotType.BigMutant:
                case BotType.Mutant:
                {
                    // https://profile-editor.vercel.app?p=M3sZZ2x3Z2p6Z0EgZ0q6ZY1WiZ0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingCyan"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingLightCyan"), });
                    // https://profile-editor.vercel.app?p=M1Fzj2x3Z2p6Z0EgZ0q6ZY0S8k0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingCyan"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen"), });
                    // https://profile-editor.vercel.app?p=M1FxjY2p6Z0EgZ0q6ZY0S8k0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen"), });
                    // https://profile-editor.vercel.app?p=M1FAjY2p6Z0EgZ0q6ZY1W3Z0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingCyan"), });
                    // https://profile-editor.vercel.app?p=M1FwjY2p6Z0EgZ0q6ZY1WjZ0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?p=M1Fxj2x3Z2p6Z0EgZ0q6ZY1WiZ0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingCyan"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingLightCyan"), });
                    // https://profile-editor.vercel.app?p=M3sZZ3ddZ2p6Z0EgZ0q6ZY0S8k0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkYellow"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen"), });
                    // https://profile-editor.vercel.app?p=M1FAjY2p6Z0EgZ0q6ZY0S8k0P3ZY
                    profiles.Add(new IProfile() { Name = "Mutant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingCyan"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGreen", "ClothingLightGreen"), });
                    break;
                }
                #endregion

                #region Monk
                case BotType.Monk:
                {
                    // https://profile-editor.vercel.app?p=M1Fzl2wlZ2olZYY06lZ0HZZY1cqZ
                    profiles.Add(new IProfile() { Name = "Monk", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightOrange"), Head = new IProfileClothingItem("Hood", "ClothingOrange"), ChestOver = new IProfileClothingItem("Apron", "ClothingLightOrange"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightOrange"), Legs = new IProfileClothingItem("Shorts", "ClothingLightOrange"), Accesory = new IProfileClothingItem("CrossNecklace", ""), });
                    break;
                }
                #endregion

                #region Nadja
                case BotType.Nadja:
                {
                    // https://profile-editor.vercel.app?p=F1Gyk1Ad70udd2ddZ2neZYY10eZ0k7o
                    profiles.Add(new IProfile() { Name = "Nadja", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGreen"), Head = new IProfileClothingItem("Beret", "ClothingDarkGray", "ClothingLightRed"), ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingDarkGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Napoleon
                case BotType.Napoleon:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj1k1j2p1Z0EgZ0q1ZY0x2ZY2G1Z
                    profiles.Add(new IProfile() { Name = "Napoleon VII", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("SpikedHelmet", "ClothingBlue"), ChestUnder = new IProfileClothingItem("LeatherJacketBlack", "ClothingBlue", "ClothingLightGray"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Cigar", "ClothingBrown"), });
                    break;
                }
                #endregion

                #region NaziLabAssistant
                case BotType.NaziLabAssistant:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj363Z1K3ZY0q1ZY08tZYY
                    profiles.Add(new IProfile() { Name = "Nazi Lab Assistant", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingCyan"), Legs = new IProfileClothingItem("Pants", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    break;
                }
                #endregion

                #region NaziMuscleSoldier
                case BotType.NaziHulk:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj1zhj1KhZ0iej0q1ZY08tZYY
                    profiles.Add(new IProfile() { Name = "Nazi Muscle Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingLightBrown", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBrown"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    break;
                }
                #endregion

                #region NaziScientist
                case BotType.NaziScientist:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj1j3j1K3ZY0q1ZY08tZ251Z173k
                    profiles.Add(new IProfile() { Name = "Nazi Scientist", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("HazmatMask", "ClothingCyan", "ClothingLightGreen"), ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingCyan", "ClothingLightGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingBlue"), Legs = new IProfileClothingItem("Pants", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    // https://profile-editor.vercel.app?p=F1Gzj1m3j1N3ZY0q1ZY09tZ261Z173k
                    profiles.Add(new IProfile() { Name = "Nazi Scientist", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("HazmatMask", "ClothingCyan", "ClothingLightGreen"), ChestUnder = new IProfileClothingItem("LeatherJacket_fem", "ClothingCyan", "ClothingLightGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack_fem", "ClothingBlue"), Legs = new IProfileClothingItem("Pants_fem", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband_fem", "ClothingRed"), });
                    break;
                }
                #endregion

                #region NaziSoldier
                case BotType.NaziSoldier:
                {
                    // https://profile-editor.vercel.app?p=M1FzjY1LeZY0q1Z1uej08tZ101Z0VeZ
                    profiles.Add(new IProfile() { Name = "Nazi Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("GermanHelmet", "ClothingGray"), ChestOver = new IProfileClothingItem("MetroLawJacket", "ClothingGray", "ClothingLightGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    // https://profile-editor.vercel.app?p=M1Fzj1zhj1KhZ0iej0q1ZY08tZY0v2Z
                    profiles.Add(new IProfile() { Name = "Nazi Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("Cap", "ClothingBrown"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingLightBrown", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBrown"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    // https://profile-editor.vercel.app?p=M1Fzj1zhj1KhZ0iej0q1ZY08tZY0VeZ
                    profiles.Add(new IProfile() { Name = "Nazi Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("GermanHelmet", "ClothingGray"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingLightBrown", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBrown"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    break;
                }
                #endregion

                #region SSOfficer
                case BotType.SSOfficer:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj2jj71LeZY0q1Z1I7p08tZ101Z1H7Z
                    profiles.Add(new IProfile() { Name = "SS Officer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("OfficerHat", "ClothingDarkGray"), ChestOver = new IProfileClothingItem("OfficerJacket", "ClothingDarkGray", "ClothingLightYellow"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Armband", "ClothingRed"), });
                    break;
                }
                #endregion

                #region Ninja
                case BotType.Ninja:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj337Z1L7ZY2n7ZY0f7Z0PeZY
                    profiles.Add(new IProfile() { Name = "Ninja", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=F1Gyj347Z1M7ZY2n7ZY0f7Z0PeZY
                    profiles.Add(new IProfile() { Name = "Ninja", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=F1Gyj347Z1M7ZY2n7ZY1rcZ0PeZY
                    profiles.Add(new IProfile() { Name = "Ninja", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack_fem", "ClothingDarkGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed"), });
                    // https://profile-editor.vercel.app?p=M1Fyj337Z1L7ZY2n7ZY1rcZ0PeZY
                    profiles.Add(new IProfile() { Name = "Ninja", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed"), });
                    break;
                }
                #endregion

                #region Police
                case BotType.Police:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj1Q4Z1L4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Officer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M1Fyj1Q4Z1L4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Officer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M1Fxj1Q4Z1L4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Officer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M1Fwj1Q4Z1L4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Officer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=F1Gyj1R4Z1M4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Officer", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=F1Gxj1R4Z1M4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Officer", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=F1Gwj1R4Z1M4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Officer", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    break;
                }
                #endregion

                #region PoliceChief
                case BotType.PoliceChief:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj1Q4Z1L4Z0i7e2m5Z3j7vYY1P4Z
                    profiles.Add(new IProfile() { Name = "Police Chief", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestOver = new IProfileClothingItem("Vest", "ClothingDarkGray", "ClothingYellow"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    break;
                }
                #endregion

                #region PoliceSWAT
                case BotType.PoliceSWAT:
                {
                    // https://profile-editor.vercel.app?p=F1Gxj1R4Z1M4ZY2m5Z1ieZYY1a4Z
                    profiles.Add(new IProfile() { Name = "SWAT", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Helmet2", "ClothingDarkBlue"), ChestOver = new IProfileClothingItem("KevlarVest_fem", "ClothingGray"), ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M1Fxj1Q4Z1L4ZY2m5Z1heZYY1a4Z
                    profiles.Add(new IProfile() { Name = "SWAT", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Helmet2", "ClothingDarkBlue"), ChestOver = new IProfileClothingItem("KevlarVest", "ClothingGray"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    break;
                }
                #endregion

                #region President
                case BotType.President:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj2jjo1L4ZY2n7Z2U4ZY0P4Z0QvZ
                    profiles.Add(new IProfile() { Name = "Donald Trump", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Flatcap", "ClothingYellow"), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingLightRed"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Punk
                case BotType.Punk:
                {
                    // https://profile-editor.vercel.app?p=M3oxpY3bgZ0iej0qeZ2L11YY1Cpq
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingLightYellow", "ClothingOrange"), ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38wpY3bgZ0iej0qeZ2Q112XZjY0s7Z
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow"), Head = new IProfileClothingItem("Buzzcut", "ClothingDarkGray"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M38zp2wpZ3bgZ0iej0qeZY2XZjY1BoZ
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow"), Head = new IProfileClothingItem("Mohawk", "ClothingLightRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightYellow"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=F1Gxj2zpZ3cgZ04eZ0qeZ1f14YY0kdp
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("JacketBlack_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightYellow"), Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M3oxpY3bgZ0iej0qeZ2L11YY1BpZ
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("Mohawk", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38xdY3bgZ0iej0qeZ2L113n7oY1BnZ
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("Mohawk", "ClothingLightPurple"), ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?p=F1Gwj2zrZ3cgZ0jej0qeZ2R142XZjY0s7Z
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("Buzzcut", "ClothingDarkGray"), ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingPink"), Waist = new IProfileClothingItem("Belt_fem", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M3oxpY3bgZ0iej0qeZ2L11YY18pZ
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("Headband", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingBlue", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38ydY3bgZ0iej0qeZ3jpp2XZjY1BqZ
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingDarkYellow"), Head = new IProfileClothingItem("Mohawk", "ClothingOrange"), ChestOver = new IProfileClothingItem("Vest", "ClothingLightYellow", "ClothingLightYellow"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M38xvY3bgZ0iej0qeZ3jpp3n7nY18pZ
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingYellow"), Head = new IProfileClothingItem("Headband", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("Vest", "ClothingLightYellow", "ClothingLightYellow"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightPurple"), });
                    // https://profile-editor.vercel.app?p=M38wpY3bgZ0iej0qeZ2Q112XZjY0kdp
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow"), Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=F3pwj2zpZ3cgZ04eZ0qeZ0C14YY0kdp
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Female, Skin = new IProfileClothingItem("Warpaint_fem", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("CoatBlack_fem", "ClothingBlue", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightYellow"), Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38ypY3bgZ039Z0qeZ3j7lYY1C7l
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkGray", "ClothingLightOrange"), ChestOver = new IProfileClothingItem("Vest", "ClothingDarkGray", "ClothingLightOrange"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingDarkOrange"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M3owjY3bgZ0iej0qeZ2Q11YY1coZ
                    profiles.Add(new IProfile() { Name = "Punk", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("Hood", "ClothingLightRed"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region PunkHulk
                case BotType.PunkHulk:
                {
                    // https://profile-editor.vercel.app?p=M38wv2wjZ3bgZ0iej0qeZY2XZjYY
                    profiles.Add(new IProfile() { Name = "Punk Muscle", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingYellow"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M38yoY2p3Z0EgZ0qeZY127oYY
                    profiles.Add(new IProfile() { Name = "Punk Muscle", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightRed"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Goggles", "ClothingDarkGray", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?p=M38xoY2p3Z0EgZ0qeZY1WdZYY
                    profiles.Add(new IProfile() { Name = "Punk Muscle", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightRed"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingDarkYellow"), });
                    // https://profile-editor.vercel.app?p=M38zp2wjZ3bgZ0EgZ0qeZY2XZjY18qZ
                    profiles.Add(new IProfile() { Name = "Punk Muscle", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow"), Head = new IProfileClothingItem("Headband", "ClothingOrange"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("CombatBelt", "ClothingLightBlue"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M38wdY3bgZ0iej0qeZ3jpp2XZjYY
                    profiles.Add(new IProfile() { Name = "Punk Muscle", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingDarkYellow"), ChestOver = new IProfileClothingItem("Vest", "ClothingLightYellow", "ClothingLightYellow"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    break;
                }
                #endregion

                #region Pyromaniac
                case BotType.Pyromaniac:
                {
                    // https://profile-editor.vercel.app?p=M38yo32oZ1L7ZY0p7Z13ZZ0Seo25cZY
                    profiles.Add(new IProfile() { Name = "Pyromaniac", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightRed"), ChestOver = new IProfileClothingItem("GrenadeBelt", ""), ChestUnder = new IProfileClothingItem("Sweater", "ClothingLightRed"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingDarkRed"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("GasMask", "ClothingGray", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?p=F39yoY1M7ZY0qeZ057Z0Seo26cZY
                    profiles.Add(new IProfile() { Name = "Pyromaniac", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin3", "ClothingLightRed"), ChestOver = new IProfileClothingItem("AmmoBelt_fem", "ClothingDarkGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack_fem", "ClothingDarkRed"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("GasMask", "ClothingGray", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region Queen
                case BotType.Queen:
                {
                    // https://profile-editor.vercel.app?p=F1Gyj3ioZ2voZ2Blt1bcZYYY0ael
                    profiles.Add(new IProfile() { Name = "Mia", Gender = Gender.Female, Skin = new IProfileClothingItem("NormalBigTiddies_fem", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("AviatorHat", "ClothingGray", "ClothingLightOrange"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt_fem", "ClothingLightRed"), Waist = new IProfileClothingItem("SmallBelt_fem", "ClothingLightOrange", "ClothingRed"), Legs = new IProfileClothingItem("Skirt_fem", "ClothingLightRed"), Feet = new IProfileClothingItem("HighHeels", "ClothingDarkRed"), });
                    // https://profile-editor.vercel.app?p=F1GzjY3clZ2Blq1btZ3lqpYY0kdp
                    profiles.Add(new IProfile() { Name = "Lia", Gender = Gender.Female, Skin = new IProfileClothingItem("NormalBigTiddies_fem", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingOrange", "ClothingLightYellow"), Waist = new IProfileClothingItem("SmallBelt_fem", "ClothingLightOrange", "ClothingOrange"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingLightOrange"), Feet = new IProfileClothingItem("HighHeels", "ClothingRed"), });
                    break;
                }
                #endregion

                #region Quillhogg
                case BotType.Quillhogg:
                {
                    // https://profile-editor.vercel.app?p=M38xp2wtZ1KgZ2cqZ0q1Z02cZ3n7n0O7Z0kdo
                    profiles.Add(new IProfile() { Name = "Quillhogg", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("Beret", "ClothingDarkYellow", "ClothingLightRed"), ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingRed"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingOrange"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightPurple"), });
                    break;
                }
                #endregion

                #region Rambo
                case BotType.Rambo:
                {
                    // https://profile-editor.vercel.app?p=M1Fyk2weZ0tdd2Aee0qeZ027ZYY18oZ
                    profiles.Add(new IProfile() { Name = "Rambo", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"), Head = new IProfileClothingItem("Headband", "ClothingLightRed"), ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGray"), Waist = new IProfileClothingItem("SmallBelt", "ClothingGray", "ClothingGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Raze
                case BotType.Raze:
                {
                    // https://profile-editor.vercel.app?p=F1Gzg1ReZ1N7Z0jev0q9Z1J7v2XZg0PeZ0veZ
                    profiles.Add(new IProfile() { Name = "Raze", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightBlue"), Head = new IProfileClothingItem("Cap", "ClothingGray"), ChestOver = new IProfileClothingItem("OfficerJacket_fem", "ClothingDarkGray", "ClothingYellow"), ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("Belt_fem", "ClothingGray", "ClothingYellow"), Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkOrange"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightBlue", ""), });
                    break;
                }
                #endregion

                #region Reznor
                case BotType.Reznor:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj339Z1LfZ2Afk2n3Z2UfZY0PeZ1Cek
                    profiles.Add(new IProfile() { Name = "Reznor", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingGreen"), ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingDarkOrange"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("SmallBelt", "ClothingGreen", "ClothingLightGreen"), Legs = new IProfileClothingItem("PantsBlack", "ClothingGreen"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingCyan"), });
                    break;
                }
                #endregion

                #region Santa
                case BotType.Santa:
                {
                    // https://profile-editor.vercel.app?p=M38yr2wjZ1KtZ0icp0q2Z0Atj29ZZ25eZ28tZ
                    profiles.Add(new IProfile() { Name = "Bad Santa", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink"), Head = new IProfileClothingItem("SantaHat", "ClothingRed"), ChestOver = new IProfileClothingItem("Coat", "ClothingRed", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkRed", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants", "ClothingRed"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBrown"), Accesory = new IProfileClothingItem("SantaMask", ""), });
                    break;
                }
                #endregion

                #region Scientist
                case BotType.Scientist:
                {
                    // https://profile-editor.vercel.app?p=M1Fzj1j3j1K3ZY0q1ZYY251Z173k
                    profiles.Add(new IProfile() { Name = "Scientist", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("HazmatMask", "ClothingCyan", "ClothingLightGreen"), ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingCyan", "ClothingLightGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingBlue"), Legs = new IProfileClothingItem("Pants", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?p=M1Fzj333Z1K3ZY0qeZ1I331riZ0XiZ1c3Z
                    profiles.Add(new IProfile() { Name = "Scientist", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGray"), Head = new IProfileClothingItem("Hood", "ClothingCyan"), ChestOver = new IProfileClothingItem("OfficerJacket", "ClothingCyan", "ClothingCyan"), ChestUnder = new IProfileClothingItem("SweaterBlack", "ClothingCyan"), Hands = new IProfileClothingItem("Gloves", "ClothingLightCyan"), Legs = new IProfileClothingItem("Pants", "ClothingCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Mask", "ClothingLightCyan"), });
                    break;
                }
                #endregion

                #region Sheriff
                case BotType.Sheriff:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj1QhZ1K4Z0i5p215ZY1DeZY2fhp
                    profiles.Add(new IProfile() { Name = "Sheriff Sternwood", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("SergeantHat", "ClothingLightBrown", "ClothingLightYellow"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingLightBrown"), Waist = new IProfileClothingItem("Belt", "ClothingDarkBrown", "ClothingLightYellow"), Legs = new IProfileClothingItem("Pants", "ClothingDarkBlue"), Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkBrown"), Accesory = new IProfileClothingItem("Moustache", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Smoker
                case BotType.Smoker:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj1z3j1L6Z0E3Z0p7Z1h7Z0S7i24eZY
                    profiles.Add(new IProfile() { Name = "Smoker", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingCyan", "ClothingLightGray"), Hands = new IProfileClothingItem("SafetyGloves", "ClothingGray"), Waist = new IProfileClothingItem("CombatBelt", "ClothingCyan"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkCyan"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("GasMask", "ClothingDarkGray", "ClothingLightCyan"), });
                    break;
                }
                #endregion

                #region Sniper
                case BotType.Sniper:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj367Z0t8703eZ0q7Z027Z3n7o0XeZY
                    profiles.Add(new IProfile() { Name = "Sniper", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("Gloves", "ClothingGray"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?p=M1Fwj367Z0t8703eZ0q7Z027Z3n7o0XeZY
                    profiles.Add(new IProfile() { Name = "Sniper", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), ChestOver = new IProfileClothingItem("AmmoBelt", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("Gloves", "ClothingGray"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed"), });
                    break;
                }
                #endregion

                #region Soldier
                case BotType.Soldier:
                {
                    // https://profile-editor.vercel.app?p=M38zp1zdg0tdd2cdZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin4", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38yp1zdg0tdd2cdZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38xp1zdg0tdd2cdZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M38wp1zdg0tdd2cdZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=F39zp1Adg0udd2ddZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin4", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=F39yp1Adg0udd2ddZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=F39xp1Adg0udd2ddZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin2", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=F39wp1Adg0udd2ddZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Soldier", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin1", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("MilitaryShirt_fem", "ClothingDarkYellow", "ClothingLightBlue"), Waist = new IProfileClothingItem("SatchelBelt_fem", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Spacer
                case BotType.Spacer:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj0nqZ0tl7Y0p7ZYY0XlZ1Cei
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingOrange"), Hands = new IProfileClothingItem("Gloves", "ClothingLightOrange"), Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M1Fxj367Z0tl7Y0p7Z1d6i087Z25lZ1Cei
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan"), ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkCyan", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingLightOrange"), Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Armband", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M1Fxj2w7Z0tl7Y0p7ZY087Z25lZ1B7Z
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Mohawk", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingLightOrange"), Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Armband", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M1FAj36eZ1LqZY0p7Z1eqjY10eZY
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin5", "ClothingLightGray"), ChestOver = new IProfileClothingItem("JacketBlack", "ClothingOrange", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingOrange"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M1Fyj2w7Z0tq7Y0p7ZY3nei10iZY
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingLightCyan"), Legs = new IProfileClothingItem("CamoPants", "ClothingOrange", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingGray", "ClothingLightCyan"), });
                    // https://profile-editor.vercel.app?p=F1GxiY2IqZ0FlZ0qeZ077Z3n7oY1BnZ
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightCyan"), Head = new IProfileClothingItem("Mohawk", "ClothingLightPurple"), ChestOver = new IProfileClothingItem("Apron_fem", "ClothingDarkGray"), Waist = new IProfileClothingItem("CombatBelt_fem", "ClothingLightOrange"), Legs = new IProfileClothingItem("StripedPants_fem", "ClothingOrange"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?p=M1Fxj157i0ti7Y0p7Z1h7ZY0XdZ1Cei
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan"), ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("HawaiiShirt", "ClothingDarkGray", "ClothingLightCyan"), Hands = new IProfileClothingItem("Gloves", "ClothingDarkYellow"), Legs = new IProfileClothingItem("CamoPants", "ClothingLightCyan", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M1Fxj367Z0tl72ceZ0p7Z1eeiY10qZ1Cel
                    profiles.Add(new IProfile() { Name = "Spacer", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightOrange"), ChestOver = new IProfileClothingItem("JacketBlack", "ClothingGray", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingOrange"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingLightOrange", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region SpaceSniper
                case BotType.SpaceSniper:
                {
                    // https://profile-editor.vercel.app?p=M1Fxj0neZ0t78Y0p7ZY08qZ0X7Z1Cei
                    profiles.Add(new IProfile() { Name = "Sniper", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingGray"), Hands = new IProfileClothingItem("Gloves", "ClothingDarkGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGray", "ClothingDarkGreen"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Armband", "ClothingOrange"), });
                    // https://profile-editor.vercel.app?p=M1Fxj367Z0t87Y0p7Z1d8f08qZ25jZ1Cei
                    profiles.Add(new IProfile() { Name = "Sniper", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingGray", "ClothingLightCyan"), ChestOver = new IProfileClothingItem("Jacket", "ClothingDarkGreen", "ClothingGreen"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingLightGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Armband", "ClothingOrange"), });
                    // https://profile-editor.vercel.app?p=M1Fyj2w7Z0t87Y0p7ZY3n7i10iZ0g7i
                    profiles.Add(new IProfile() { Name = "Sniper", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("BaseballCap", "ClothingDarkGray", "ClothingLightCyan"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingLightCyan"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkGreen", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Vizor", "ClothingDarkGray", "ClothingLightCyan"), });
                    break;
                }
                #endregion

                #region Spy
                case BotType.Spy:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj2jj42H1ZY2n7Z2J1Z0f4Z0X7ZY
                    profiles.Add(new IProfile() { Name = "Spy", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestOver = new IProfileClothingItem("StripedSuitJacket", "ClothingBlue"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingLightGray", "ClothingDarkBlue"), Hands = new IProfileClothingItem("Gloves", "ClothingDarkGray"), Legs = new IProfileClothingItem("StripedPants", "ClothingBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkBlue"), });
                    break;
                }
                #endregion

                #region Stripper
                case BotType.Stripper:
                {
                    // https://profile-editor.vercel.app?p=F1Gx7YYY202ZYYYY
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkGray"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), });
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("NormalBigTiddies_fem", "Skin2", "ClothingDarkGray"), Feet = new IProfileClothingItem("RidingBoots", "ClothingBrown"), });
                    // https://profile-editor.vercel.app?p=M1FwjYYYYYYYY
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), });
                    // https://profile-editor.vercel.app?p=F1GzoYYY1boZYYYY
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingLightRed"), Feet = new IProfileClothingItem("HighHeels", "ClothingLightRed"), });
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("NormalBigTiddies_fem", "Skin4", "ClothingLightRed"), Feet = new IProfileClothingItem("HighHeels", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?p=F1Gxf3efZYYYYYYY
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingGreen"), ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingGreen"), });
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("NormalBigTiddies_fem", "Skin2", "ClothingGreen"), ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingGreen"), });
                    // https://profile-editor.vercel.app?p=F1GzsYYY217Z2tsjYYY
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin4", "ClothingPurple"), ChestOver = new IProfileClothingItem("ShoulderHolster_fem", "ClothingPurple", "ClothingLightGray"), Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkGray"), });
                    profiles.Add(new IProfile() { Name = "Stripper", Gender = Gender.Female, Skin = new IProfileClothingItem("NormalBigTiddies_fem", "Skin4", "ClothingPurple"), ChestOver = new IProfileClothingItem("ShoulderHolster_fem", "ClothingPurple", "ClothingLightGray"), Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkGray"), });
                    break;
                }
                #endregion
                
                #region SuicideDwarf
                case BotType.SuicideDwarf:
                {
                    // https://profile-editor.vercel.app?p=M38wp3ddZ3bdZ2cdZ0qeZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Dwarf", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingLightYellow"), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkYellow"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("TornPants", "ClothingDarkYellow"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region Survivalist
                case BotType.Survivalist:
                {
                    // https://profile-editor.vercel.app?p=M3oxd3hdZ0tdd2cqZ0p9ZYY0OhZ0Gde
                    profiles.Add(new IProfile() { Name = "The Survivalist", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("CowboyHat", "ClothingDarkYellow", "ClothingGray"), ChestUnder = new IProfileClothingItem("UnbuttonedShirt", "ClothingDarkYellow"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingLightBrown"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingOrange"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("Boots", "ClothingDarkOrange"), });
                    break;
                }
                #endregion

                #region Survivor
                case BotType.Survivor:
                {
                    // https://profile-editor.vercel.app?p=M3oAo15jo1KeZY0p7Z1e7eYY1Co7
                    profiles.Add(new IProfile() { Name = "Survivor", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingLightRed", "ClothingDarkGray"), ChestOver = new IProfileClothingItem("JacketBlack", "ClothingDarkGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("HawaiiShirt", "ClothingLightGray", "ClothingLightRed"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M3oAoY3beZ2c2ZYY2CoZY0v1Z
                    profiles.Add(new IProfile() { Name = "Survivor", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed"), Head = new IProfileClothingItem("Cap", "ClothingBlue"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingBrown"), Legs = new IProfileClothingItem("TornPants", "ClothingGray"), Accesory = new IProfileClothingItem("SmallMoustache", "ClothingLightRed"), });
                    // https://profile-editor.vercel.app?p=M3oAoY1KeZY2DeeY0HZZY0v7Z
                    profiles.Add(new IProfile() { Name = "Survivor", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed"), Head = new IProfileClothingItem("Cap", "ClothingDarkGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("Sneakers", "ClothingGray", "ClothingGray"), Accesory = new IProfileClothingItem("DogTag", ""), });
                    // https://profile-editor.vercel.app?p=M38Ao364Z1K4Z2c2Z0p7ZYYY0v7Z
                    profiles.Add(new IProfile() { Name = "Survivor", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin5", "ClothingLightRed"), Head = new IProfileClothingItem("Cap", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkBlue"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingBrown"), Legs = new IProfileClothingItem("Pants", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    // https://profile-editor.vercel.app?p=M3oAo36eZ1KeZY0peZ1e7eYYY
                    profiles.Add(new IProfile() { Name = "Survivor", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed"), ChestOver = new IProfileClothingItem("JacketBlack", "ClothingDarkGray", "ClothingGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("Boots", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M3oAo1o251KeZY0p7Z1h7ZYY1aeZ
                    profiles.Add(new IProfile() { Name = "Survivor", Gender = Gender.Male, Skin = new IProfileClothingItem("Warpaint", "Skin5", "ClothingLightRed"), Head = new IProfileClothingItem("Helmet2", "ClothingGray"), ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingBrown", "ClothingDarkBrown"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), });
                    break;
                }
                #endregion

                #region Tank
                case BotType.Tank:
                    // https://profile-editor.vercel.app?p=M1Fyj2OcZ0t272a7Z0p7Z1I7o0T7o10tZ0s7Z
                    profiles.Add(new IProfile() { Name = "Tank", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("Buzzcut", "ClothingDarkGray"), ChestOver = new IProfileClothingItem("OfficerJacket", "ClothingDarkGray", "ClothingLightRed"), ChestUnder = new IProfileClothingItem("StuddedLeatherSuit", "ClothingDarkRed"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingRed"), Waist = new IProfileClothingItem("Sash", "ClothingDarkGray"), Legs = new IProfileClothingItem("CamoPants", "ClothingBrown", "ClothingDarkGray"), Feet = new IProfileClothingItem("Boots", "ClothingDarkGray"), Accesory = new IProfileClothingItem("GasMask2", "ClothingDarkGray", "ClothingLightRed"), });
                    break;
                #endregion

                #region Bear
                case BotType.Teddybear:
                case BotType.Babybear:
                {
                    // https://profile-editor.vercel.app?p=M0hZZYYYYYYYY
                    profiles.Add(new IProfile() { Name = "Teddybear", Gender = Gender.Male, Skin = new IProfileClothingItem("BearSkin", ""), });
                    break;
                }
                #endregion

                #region Thug
                case BotType.Thug:
                {
                    // https://profile-editor.vercel.app?p=F1Gyj2zjZ1NgZ0j7j0qeZY2XZjY18tZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M1Fyj2wjZ1KgZ0i7j0qeZY2XZjY18tZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M1Fyj2wjZ1KgZ0i7j0qeZY2XZjY1C6p
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkCyan", "ClothingLightYellow"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=F1Gyj37jZ1NgZ0j7j0qeZY2XZjY1C6p
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkCyan", "ClothingLightYellow"), ChestUnder = new IProfileClothingItem("TShirt_fem", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M38yk2wjZ1KgZ032Z0q1Z2Q112XZj0P1ZY
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingBrown"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=F39yp2zjZ1NgZ0jej0q1Z2R11Y0P1ZY
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin3", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?p=M38xk2wjZ1KgZ032Z0q1Z2Q11Y0P1ZY
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin2", "ClothingLightGreen"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingBrown"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?p=F39xk2zjZ1NgZ0j2j0q1Z2R112XZj0P1Z18tZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin2", "ClothingLightGreen"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestOver = new IProfileClothingItem("StuddedVest_fem", "ClothingBlue", "ClothingBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingBrown", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=F39wl2zjZ1NgZ0j2j0q1Z3l11Y0P1Z1C8p
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Tattoos_fem", "Skin1", "ClothingLightOrange"), Head = new IProfileClothingItem("MotorcycleHelmet", "ClothingDarkGreen", "ClothingLightYellow"), ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingBrown", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), });
                    // https://profile-editor.vercel.app?p=F1Gxj2zjZ1NgZ0j7j0qeZY2XZjY18tZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M1Fxj2wjZ1KgZ0i7j0qeZY2XZjY18tZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M1Fwj2wjZ1KgZ0i7j0qeZ2Q112XZjY18tZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestOver = new IProfileClothingItem("StuddedVest", "ClothingBlue", "ClothingBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=F1Gwj2zjZ1NgZ0j7j0qeZ3l112XZjY0gto
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin1", "ClothingLightGray"), Head = new IProfileClothingItem("BaseballCap", "ClothingRed", "ClothingLightRed"), ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants_fem", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M38yj2w4Z1L4ZY0p5Z3jggYY18cZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightGray"), Head = new IProfileClothingItem("Headband", "ClothingDarkRed"), ChestOver = new IProfileClothingItem("Vest", "ClothingLightBlue", "ClothingLightBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M38yrY3bgZ0i7j0q1Z3k410HZZ0O7Z3roZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingPink"), Head = new IProfileClothingItem("WoolCap", "ClothingLightRed"), ChestOver = new IProfileClothingItem("VestBlack", "ClothingDarkBlue", "ClothingBlue"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("DogTag", ""), });
                    // https://profile-editor.vercel.app?p=M38yp2wjZ3bgZ0iej0qeZY2XZj0O2Z18oZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin3", "ClothingLightYellow"), Head = new IProfileClothingItem("Headband", "ClothingLightRed"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingBrown"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=F1GxdY3cbZ0j4j0q1Z3l142XZj101Z18oZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Normal_fem", "Skin2", "ClothingDarkYellow"), Head = new IProfileClothingItem("Headband", "ClothingLightRed"), ChestOver = new IProfileClothingItem("VestBlack_fem", "ClothingBlue", "ClothingDarkBlue"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingBlue"), Waist = new IProfileClothingItem("Belt_fem", "ClothingDarkBlue", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkPurple"), Feet = new IProfileClothingItem("BootsBlack", "ClothingBlue"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M38wd2w4Z1L4ZY0p5Z3jggYY18tZ
                    profiles.Add(new IProfile() { Name = "Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Tattoos", "Skin1", "ClothingDarkYellow"), Head = new IProfileClothingItem("Headband", "ClothingRed"), ChestOver = new IProfileClothingItem("Vest", "ClothingLightBlue", "ClothingLightBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown"), });
                    break;
                }
                #endregion

                #region ThugHulk
                case BotType.ThugHulk:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj36jZ1KgZ0iej0qeZY2XZjYY
                    profiles.Add(new IProfile() { Name = "Thug Hulk", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M1Fxj36jZ1KgZ0iej0qeZY2XZjYY
                    profiles.Add(new IProfile() { Name = "Thug Hulk", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin2", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    // https://profile-editor.vercel.app?p=M1Fwj36jZ1KgZ0iej0qeZY2XZjYY
                    profiles.Add(new IProfile() { Name = "Thug Hulk", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingLightGray"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("Belt", "ClothingGray", "ClothingLightGray"), Legs = new IProfileClothingItem("Pants", "ClothingLightBlue"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    break;
                }
                #endregion

                #region Translucent
                case BotType.Translucent:
                {
                    // https://profile-editor.vercel.app?p=M1Fyj2geZ1KeZY2n2Z30tc1WtZ10eZY
                    profiles.Add(new IProfile() { Name = "Translucent", Gender = Gender.Male, Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingRed", "ClothingDarkRed"), ChestUnder = new IProfileClothingItem("Shirt", "ClothingGray"), Hands = new IProfileClothingItem("GlovesBlack", "ClothingGray"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingBrown"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingRed"), });
                    break;
                }
                #endregion

                #region Zombie
                case BotType.Zombie:
                {
                    // https://profile-editor.vercel.app?p=F3tZZ3e4Z3c4ZYYYYYY
                    profiles.Add(new IProfile() { Name = "Zombie", Gender = Gender.Female, Skin = new IProfileClothingItem("Zombie_fem", ""), ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkBlue"), });
                    // https://profile-editor.vercel.app?p=M3sZZ3d4Z3b4ZYYYYYY
                    profiles.Add(new IProfile() { Name = "Zombie", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("TornPants", "ClothingDarkBlue"), });
                    break;
                }
                #endregion

                #region ZombieAgent
                case BotType.ZombieAgent:
                {
                    // https://profile-editor.vercel.app?p=M3sZZ2je71L7ZY2n5Z2U7Z2XZjYY
                    profiles.Add(new IProfile() { Name = "Zombie Agent", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown"), Accesory = new IProfileClothingItem("SunGlasses", "", "ClothingLightGray", ""), });
                    break;
                }
                #endregion

                #region ZombieBruiser
                case BotType.ZombieBruiser:
                {
                    // https://profile-editor.vercel.app?p=M3sZZY3bbZYY3k141WeZYY
                    profiles.Add(new IProfile() { Name = "Zombie Bruiser", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestOver = new IProfileClothingItem("VestBlack", "ClothingBlue", "ClothingDarkBlue"), Legs = new IProfileClothingItem("TornPants", "ClothingDarkPurple"), Accesory = new IProfileClothingItem("RestraintMask", "ClothingGray"), });
                    break;
                }
                #endregion

                #region ZombieChild
                case BotType.ZombieChild:
                {
                    // https://profile-editor.vercel.app?p=F3tZZ3esZ3c4ZYYYYYY
                    profiles.Add(new IProfile() { Name = "Zombie Child", Gender = Gender.Female, Skin = new IProfileClothingItem("Zombie_fem", ""), ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingPurple"), Legs = new IProfileClothingItem("TornPants_fem", "ClothingDarkBlue"), });
                    // https://profile-editor.vercel.app?p=M3sZZ3dsZ3b4ZYYYYYY
                    profiles.Add(new IProfile() { Name = "Zombie Child", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestUnder = new IProfileClothingItem("TornShirt", "ClothingPurple"), Legs = new IProfileClothingItem("TornPants", "ClothingDarkBlue"), });
                    break;
                }
                #endregion

                #region ZombieFat
                case BotType.ZombieFat:
                {
                    // https://profile-editor.vercel.app?p=M3sZZY2oeZYY2stjYYY
                    profiles.Add(new IProfile() { Name = "Fat Zombie", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingRed", "ClothingLightGray"), Legs = new IProfileClothingItem("Shorts", "ClothingGray"), });
                    break;
                }
                #endregion

                #region ZombieFighter
                case BotType.ZombieFighter:
                {
                    // https://profile-editor.vercel.app?p=M3sZZ32fZ1L7Z0i7j0q5ZYY0O7ZY
                    profiles.Add(new IProfile() { Name = "Dead Cop", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestUnder = new IProfileClothingItem("Sweater", "ClothingGreen"), Hands = new IProfileClothingItem("FingerlessGloves", "ClothingDarkGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M3sZZ36jZ1L7Z2c2Z0q5Z1d2hYY0a2j
                    profiles.Add(new IProfile() { Name = "Dead Merc", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("AviatorHat", "ClothingBrown", "ClothingLightGray"), ChestOver = new IProfileClothingItem("Jacket", "ClothingBrown", "ClothingLightBrown"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingBrown"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M3sZZ36dZ1L7ZY0q5ZYYY0vdZ
                    profiles.Add(new IProfile() { Name = "Dead Vigilante", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("Cap", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("TShirt", "ClothingDarkYellow"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M3sZZ2hj71L7ZY2n5Z2U7ZYYY
                    profiles.Add(new IProfile() { Name = "Dead Spy", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestOver = new IProfileClothingItem("SuitJacketBlack", "ClothingDarkGray"), ChestUnder = new IProfileClothingItem("ShirtWithBowtie", "ClothingLightGray", "ClothingDarkGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M3sZZ2wjZ1L7Z2A7j2n5Z2s55YYY
                    profiles.Add(new IProfile() { Name = "Dead Pilot", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestOver = new IProfileClothingItem("ShoulderHolster", "ClothingDarkBrown", "ClothingDarkBrown"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingLightGray"), Waist = new IProfileClothingItem("SmallBelt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M3sZZ2weZ1L7Z0i7j0q5Z1d22YYY
                    profiles.Add(new IProfile() { Name = "Dead Driver", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestOver = new IProfileClothingItem("Jacket", "ClothingBrown", "ClothingBrown"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingGray"), Waist = new IProfileClothingItem("Belt", "ClothingDarkGray", "ClothingLightGray"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkBrown"), });
                    break;
                }
                #endregion

                #region ZombieFlamer
                case BotType.ZombieFlamer:
                {
                    // https://profile-editor.vercel.app?p=M3sZZ2xeZ2p7ZYYY0WppYY
                    profiles.Add(new IProfile() { Name = "Zombie Flamer", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestUnder = new IProfileClothingItem("SleevelessShirtBlack", "ClothingGray"), Legs = new IProfileClothingItem("ShortsBlack", "ClothingDarkGray"), Accesory = new IProfileClothingItem("Glasses", "ClothingLightYellow", "ClothingLightYellow"), });
                    break;
                }
                #endregion

                #region ZombieGangster
                case BotType.ZombieGangster:
                {
                    // https://profile-editor.vercel.app?p=M3sZZY3beZY2neZ0legYY2Ser
                    profiles.Add(new IProfile() { Name = "Zombie Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("StylishHat", "ClothingGray", "ClothingPink"), ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingLightBlue"), Legs = new IProfileClothingItem("TornPants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M3sZZ2jra3beZY2neZ30edYY0QeZ
                    profiles.Add(new IProfile() { Name = "Zombie Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("Flatcap", "ClothingGray"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingGray", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingPink", "ClothingDarkPink"), Legs = new IProfileClothingItem("TornPants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M3sZZY3beZY2neZ2TeZYY2Sed
                    profiles.Add(new IProfile() { Name = "Zombie Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("StylishHat", "ClothingGray", "ClothingDarkYellow"), ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray"), Legs = new IProfileClothingItem("TornPants", "ClothingGray"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M3sZZ2jea1KeZY0qeZ2TeZYY0Lea
                    profiles.Add(new IProfile() { Name = "Zombie Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("Fedora", "ClothingGray", "ClothingDarkPink"), ChestOver = new IProfileClothingItem("SuitJacket", "ClothingGray"), ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    // https://profile-editor.vercel.app?p=M3sZZY1KeZY0qeZ0leaYYY
                    profiles.Add(new IProfile() { Name = "Zombie Gangster", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), ChestOver = new IProfileClothingItem("BlazerWithShirt", "ClothingGray", "ClothingDarkPink"), Legs = new IProfileClothingItem("Pants", "ClothingGray"), Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"), });
                    break;
                }
                #endregion

                #region ZombieNinja
                case BotType.ZombieNinja:
                {
                    // https://profile-editor.vercel.app?p=F3tZZ3g4Z1N4Z2bcZ2n4ZY1rcZ0P4ZY
                    profiles.Add(new IProfile() { Name = "Zombie Ninja", Gender = Gender.Female, Skin = new IProfileClothingItem("Zombie_fem", ""), ChestUnder = new IProfileClothingItem("TrainingShirt_fem", "ClothingDarkBlue"), Hands = new IProfileClothingItem("FingerlessGlovesBlack", "ClothingDarkBlue"), Waist = new IProfileClothingItem("Sash_fem", "ClothingDarkRed"), Legs = new IProfileClothingItem("Pants_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBlue"), Accesory = new IProfileClothingItem("Mask", "ClothingDarkRed"), });
                    break;
                }
                #endregion

                #region ZombiePolice
                case BotType.ZombiePolice:
                {
                    // https://profile-editor.vercel.app?p=F3tZZ1R4Z1M4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Zombie Police", Gender = Gender.Female, Skin = new IProfileClothingItem("Zombie_fem", ""), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M3sZZ1Q4Z1L4ZY2m5ZYYY1P4Z
                    profiles.Add(new IProfile() { Name = "Zombie Police", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("PoliceHat", "ClothingDarkBlue"), ChestUnder = new IProfileClothingItem("PoliceShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Shoes", "ClothingDarkBrown"), });
                    break;
                }
                #endregion

                #region ZombiePrussian
                case BotType.ZombiePrussian:
                case BotType.ZombieEater:
                {
                    // https://profile-editor.vercel.app?p=M3sZZ1k3j3b6ZY0q6ZYYY2G3Z
                    profiles.Add(new IProfile() { Name = "Zombie Prussian", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("SpikedHelmet", "ClothingCyan"), ChestUnder = new IProfileClothingItem("LeatherJacketBlack", "ClothingCyan", "ClothingLightGray"), Legs = new IProfileClothingItem("TornPants", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), });
                    // https://profile-editor.vercel.app?p=M3sZZ3d6Z3b6ZY0q6ZY0S3kY2G3Z
                    profiles.Add(new IProfile() { Name = "Zombie Prussian", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("SpikedHelmet", "ClothingCyan"), ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkCyan"), Legs = new IProfileClothingItem("TornPants", "ClothingDarkCyan"), Feet = new IProfileClothingItem("BootsBlack", "ClothingDarkCyan"), Accesory = new IProfileClothingItem("GasMask", "ClothingCyan", "ClothingLightGreen"), });
                    break;
                }
                #endregion

                #region ZombieSoldier
                case BotType.ZombieSoldier:
                {
                    // https://profile-editor.vercel.app?p=F3tZZ3edZ0udd042Z0pcZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Zombie Soldier", Gender = Gender.Female, Skin = new IProfileClothingItem("Zombie_fem", ""), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("TornShirt_fem", "ClothingDarkYellow"), Waist = new IProfileClothingItem("AmmoBeltWaist_fem", "ClothingBrown"), Legs = new IProfileClothingItem("CamoPants_fem", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("Boots", "ClothingDarkRed"), });
                    // https://profile-editor.vercel.app?p=M3sZZ3ddZ0tdd032Z0pcZYYY19dZ
                    profiles.Add(new IProfile() { Name = "Zombie Soldier", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("Helmet", "ClothingDarkYellow"), ChestUnder = new IProfileClothingItem("TornShirt", "ClothingDarkYellow"), Waist = new IProfileClothingItem("AmmoBeltWaist", "ClothingBrown"), Legs = new IProfileClothingItem("CamoPants", "ClothingDarkYellow", "ClothingDarkYellow"), Feet = new IProfileClothingItem("Boots", "ClothingDarkRed"), });
                    break;
                }
                #endregion

                #region ZombieThug
                case BotType.ZombieThug:
                {
                    // https://profile-editor.vercel.app?p=F3tZZ2z4Z1M4ZY0p5Z3mggYY18cZ
                    profiles.Add(new IProfile() { Name = "Zombie Thug", Gender = Gender.Female, Skin = new IProfileClothingItem("Zombie_fem", ""), Head = new IProfileClothingItem("Headband", "ClothingDarkRed"), ChestOver = new IProfileClothingItem("Vest_fem", "ClothingLightBlue", "ClothingLightBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt_fem", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack_fem", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown"), });
                    // https://profile-editor.vercel.app?p=M3sZZ2w4Z1L4ZY0p5Z3jggYY18cZ
                    profiles.Add(new IProfile() { Name = "Zombie Thug", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("Headband", "ClothingDarkRed"), ChestOver = new IProfileClothingItem("Vest", "ClothingLightBlue", "ClothingLightBlue"), ChestUnder = new IProfileClothingItem("SleevelessShirt", "ClothingDarkBlue"), Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkBlue"), Feet = new IProfileClothingItem("Boots", "ClothingDarkBrown"), });
                    break;
                }
                #endregion

                #region ZombieWorker
                case BotType.ZombieWorker:
                {
                    // https://profile-editor.vercel.app?p=M3sZZ3dqZ3bqZ2cqZ2n5Z30qlYY0vvZ
                    profiles.Add(new IProfile() { Name = "Zombie Worker", Gender = Gender.Male, Skin = new IProfileClothingItem("Zombie", ""), Head = new IProfileClothingItem("Cap", "ClothingYellow"), ChestOver = new IProfileClothingItem("Suspenders", "ClothingOrange", "ClothingLightOrange"), ChestUnder = new IProfileClothingItem("TornShirt", "ClothingOrange"), Waist = new IProfileClothingItem("SatchelBelt", "ClothingOrange"), Legs = new IProfileClothingItem("TornPants", "ClothingOrange"), Feet = new IProfileClothingItem("ShoesBlack", "ClothingDarkBrown"), });
                    break;
                }
                #endregion
            }

            return profiles;
        }
    }
}
