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
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Tegridy.Tools;
using UnityEngine.SceneManagement;
namespace Tegridy.LanguageControl
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Main Menu Objects")]
        public Image mainMenu; //main menu holder
        public TextMeshProUGUI title; //game name
        public TextMeshProUGUI description; 
        public Button start;
        public Button exit;
        public Button notesView;

        [Header("Info Screen")]
        public Image notes;
        public Button notesBack;
        public TextMeshProUGUI notesTitle;
        public TextMeshProUGUI notesDescription;

        [Header("Language Screen")]
        public Image languageScreen;
        public Button languagePrefab;
        public float vSpacing;
        public float hSpacing;
        public GameObject[] langButt;

        LanguageController language;

        void Awake()
        {
            //setup the buttons for the gui
            start.onClick.AddListener(() => StartGame());
            notesView.onClick.AddListener(() => OpenNotes(true));
            notesBack.onClick.AddListener(() => OpenNotes(false));
            exit.onClick.AddListener(() => ExitGame());

            //make sure the ui wasn't left open in the editor
            mainMenu.SetActive(false);
            notes.SetActive(false);

            //open the language screen and load the selected language
            language = FindObjectOfType<LanguageController>();
            language.GetLanguagePacks();
            OpenLanguageSelect();
        }
        void OpenMainMenu()
        {
            UITools.SetButtonText(start, LanguageMenu.start);
            UITools.SetButtonText(exit, LanguageMenu.exit);
            UITools.SetButtonText(notesView, LanguageMenu.notesOpen);
            UITools.SetButtonText(notesBack, LanguageMenu.notesClose);
            title.text = LanguageMenu.gameName;
            description.text = LanguageMenu.gameDescription;
            notesTitle.text = LanguageMenu.notesTitle;
            notesDescription.text = LanguageMenu.notesDescription;
            mainMenu.SetActive(true);
        }
        void StartGame()
        {
            Debug.Log("Put your own start code here");
            ResetScene();
        }
        void ExitGame()
        {
            Application.Quit();
        }
        void OpenNotes(bool open)
        {
            mainMenu.SetActive(!open);
            notes.SetActive(open);
        }
        private void OpenLanguageSelect()
        {
            //create the buttons
            UITools.DestoryOld(langButt);
            langButt = UITools.DrawTiled(languagePrefab.gameObject, language.languageNames.Length, hSpacing, vSpacing, 2);

            //add the listeners
            for (int i = 0; i < langButt.Length; i++)
            {
                int selection = i;
                langButt[i].GetComponentInChildren<TextMeshProUGUI>().text = language.languageNames[i];
                langButt[i].GetComponent<Button>().onClick.AddListener(() => CloseLanguageSelect(selection));
            }

            //enable the ui
            languageScreen.SetActive(true);
        }
        private void CloseLanguageSelect(int i)
        {
            UITools.DestoryOld(langButt);
            language.selectedLanguage = i;
            language.LoadSelectedLanguage();
            languageScreen.SetActive(false);
            OpenMainMenu();
        }
        public void ResetScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}