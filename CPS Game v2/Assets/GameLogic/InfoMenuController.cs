﻿using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameLogic
{
    public class InfoMenuController
    {
        public static GameObject _popupPrefab = null;
        public static GameObject _popupInstance = null;
        public static GameObject popupInstance
        {
            get
            {
                if (InfoMenuController._popupInstance == null)
                {
                    var rootCanvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
                    InfoMenuController._popupInstance = UnityEngine.Object.Instantiate(_popupPrefab, _popupPrefab.transform.position, _popupPrefab.transform.rotation);
                    _popupInstance.transform.SetParent(rootCanvas.transform, false);

                   
                }

                return InfoMenuController._popupInstance;
            }
        }

        private GameController gameController;
        private GameControllerWrapper gameControllerWrapper;
        private Canvas rootCanvas;

        private Module module;

        //public GameObject popupPrefab;
        //private GameObject popupInstance;

        // private Button[] buttons;
        private Text[] texts;
        private Text textContent;


        public InfoMenuController(Module obj, GameObject popupPrefab)
        {
            this.module = obj;

            if (InfoMenuController._popupPrefab == null && popupPrefab != null)
                InfoMenuController._popupPrefab = popupPrefab;

            this.gameController = GameControllerWrapper.GetGameController();
            this.rootCanvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));

            this.texts = InfoMenuController.popupInstance.GetComponentsInChildren<Text>();
            this.textContent = this.texts[1];

            this.texts[0].text = "Info Menu";
            this.textContent.text = "";
            this.CloseMenu();


            // Hide the menu when turn changes
            GameEvents.OnTurnChange((turnChangeArgs) => {
                this.CloseMenu();
            });
        }

        /// <summary>
        /// Keeps information on the menu up to date
        /// </summary>
        public void UpdateMenu()
        {
            MenuToDisplay menu = this.module.GetInformation(new MenuBuilder());

            int start = 0;
            int end = menu.MenuChoices.Count;

            var items = menu.GetAllAsStrings();
            foreach (var item in items)
            {
                this.textContent.text += item.DisplayName + ": " + item.Value + "\n";
            }

            this.textContent.text = this.textContent.text.Substring(0, this.textContent.text.Length - 2); // Remove the last newline
        }

        /// <summary>
        /// Shows the menu
        /// </summary>
        /// <param name="position"></param>
        public void OpenMenu()
        {
            this.CloseMenu();
            this.UpdateMenu();

            // Move to position
            RectTransform uiTransform = InfoMenuController.popupInstance.GetComponent<RectTransform>();
            uiTransform.position = new Vector2((float)400, (float)900);

            InfoMenuController.popupInstance.SetActive(true);
        }

        /// <summary>
        /// Hides the menu
        /// </summary>
        public void CloseMenu()
        {
            
            InfoMenuController.popupInstance.SetActive(false);
        }
    }
}
