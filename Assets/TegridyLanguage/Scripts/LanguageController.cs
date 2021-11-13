/////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2021 Tegridy Ltd                                          //
// Author: Darren Braviner                                                 //
// Contact: db@tegridygames.co.uk                                          //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// This program is free software; you can redistribute it and/or modify    //
// it under the terms of the GNU General Public License as published by    //
// the Free Software Foundation; either version 2 of the License, or       //
// (at your option) any later version.                                     //
//                                                                         //
// This program is distributed in the hope that it will be useful,         //
// but WITHOUT ANY WARRANTY.                                               //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// You should have received a copy of the GNU General Public License       //
// along with this program; if not, write to the Free Software             //
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,              //
// MA 02110-1301 USA                                                       //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.IO;
using System;
namespace Tegridy.LanguageControl
{
    public static class LanguageMenu
    {
        public static string gameName;
        public static string gameDescription;
        public static string start;
        public static string exit;
        public static string notesOpen;
        public static string notesClose;
        public static string notesTitle;
        public static string notesDescription;
    }
    public class LanguageController : MonoBehaviour
    {
        //Note to readers, sorry.
        readonly string mainPath = Application.streamingAssetsPath + "/Languages/languages.txt";

        public int selectedLanguage = 0;
        public string[] languageNames;
        string[] languageImages;
        string[] languagePaths;

        public string creditsText;
        public void GetLanguagePacks()
        {
            string[] data;
            data = File.ReadAllLines(mainPath);
            Array.Resize(ref languageNames, data.Length);
            Array.Resize(ref languageImages, data.Length);
            Array.Resize(ref languagePaths, data.Length);

            //get our split and load the images
            for (int i = 0; i < data.Length; i++)
            {
                string value = data[i];
                string[] split = value.Split(',');
                languageNames[i] = split[0];
                languageImages[i] = Application.streamingAssetsPath + "/Languages" + (split[1]);
                languagePaths[i] = Application.streamingAssetsPath + "/Languages" + (split[2]);
            }
        }
        public void LoadSelectedLanguage()
        {
            /*
            //check if we have a matching language file for the selected language, if not use the default language for that part of the ui
            //load and format game credits
            string[] creditData;
            if (File.Exists(languagePaths[selectedLanguage] + "/Credits.txt")) creditData = File.ReadAllLines(languagePaths[selectedLanguage] + "/Credits.txt");
            else creditData = File.ReadAllLines(languagePaths[0] + "/Credits.txt");
            for (int i = 0; i < creditData.Length; i++)
            {
                creditsText += "<br>" + creditData[i];
            }
            */

            //load the strings used in the main menu // either add more variables here or add your own class and load for a different file. its up to you.
            string[] mainMenu;
            if (File.Exists(languagePaths[selectedLanguage] + "/MainMenu.txt")) mainMenu = File.ReadAllLines(languagePaths[selectedLanguage] + "/MainMenu.txt");
            else mainMenu = File.ReadAllLines(languagePaths[0] + "/MainMenu.txt");
            LanguageMenu.gameName = mainMenu[0];
            LanguageMenu.gameDescription = mainMenu[1];
            LanguageMenu.start = mainMenu[2];
            LanguageMenu.exit = mainMenu[3];
            LanguageMenu.notesOpen = mainMenu[4];
            LanguageMenu.notesClose = mainMenu[5];
            LanguageMenu.notesTitle = mainMenu[6];
            LanguageMenu.notesDescription = mainMenu[7];
    }
    }
}