using Assets.Modules.Menu;
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

        private GameControllerWrapper gameController;
        private Canvas rootCanvas;

        private Module module;
        private Text[] texts;
        private Text textContent;


        public InfoMenuController(Module obj, GameObject popupPrefab)
        {
            this.module = obj;

            if (InfoMenuController._popupPrefab == null && popupPrefab != null)
                InfoMenuController._popupPrefab = popupPrefab;

            this.gameController = new GameControllerWrapper();
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

            if (!String.IsNullOrEmpty(menu.Title))
                this.texts[0].text = menu.Title; 

            int start = 0;
            int end = menu.MenuChoices.Count;

            var items = menu.GetAllAsStrings();
            foreach (var item in items)
            {
                this.textContent.text += item.DisplayName + ": " + item.Value + "\n";
            }

            this.textContent.text = this.textContent.text.Substring(0, this.textContent.text.Length - 1); // Remove the last newline
        }

        /// <summary>
        /// Shows the menu
        /// </summary>
        /// <param name="position"></param>
        public void OpenMenu()
        {
            this.CloseMenu();
            this.UpdateMenu();

            RectTransform uiTransform = InfoMenuController.popupInstance.GetComponent<RectTransform>();
            // Move to position dependent on player turn
            if(this.gameController.IsAttackersTurn())
            {
                uiTransform.position = new Vector2((float)300, (float)565);
                uiTransform.rotation = Quaternion.Euler(0,0,270);
            }
            else
            {
                uiTransform.position = new Vector2((float)1800, (float)545);
                uiTransform.rotation = Quaternion.Euler(0,0,90);
            }

        
            
            InfoMenuController.popupInstance.SetActive(true);
        }

        /// <summary>
        /// Hides the menu
        /// </summary>
        public void CloseMenu()
        {
            this.textContent.text = "";
            InfoMenuController.popupInstance.SetActive(false);
        }
    }
}
