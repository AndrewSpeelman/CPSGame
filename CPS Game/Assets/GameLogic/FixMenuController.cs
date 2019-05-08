using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Assets.Interfaces.Modules;
using Assets.Modules.Menu;

namespace Assets.GameLogic
{
    public class FixMenuController
    {
        public static GameObject _popupPrefab = null;
        public static GameObject _popupInstance = null;
        public static GameObject popupInstance
        {
            get
            {
                if (FixMenuController._popupInstance == null)
                {
                    var rootCanvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
                    FixMenuController._popupInstance = UnityEngine.Object.Instantiate(_popupPrefab, _popupPrefab.transform.position, _popupPrefab.transform.rotation);
                    _popupInstance.transform.SetParent(rootCanvas.transform, false);

                    FixMenuController.buttons = popupInstance.GetComponentsInChildren<Button>();
                }

                return FixMenuController._popupInstance;
            }
        }
        public static Button[] buttons;

        private GameController gameController;
        private GameControllerWrapper gameControllerWrapper; 
        private Canvas rootCanvas;

        private ICanBeAttacked module;
        private Text[] texts;
        private Text textContent;


        public FixMenuController(ICanBeAttacked obj, GameObject popupPrefab)
        {
            this.module = obj;

            if (FixMenuController._popupPrefab == null && popupPrefab != null)
                FixMenuController._popupPrefab = popupPrefab;

            this.gameController = GameControllerWrapper.GetGameController();
            this.rootCanvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));

            this.texts = FixMenuController.popupInstance.GetComponentsInChildren<Text>();
            this.textContent = this.texts[0];

            textContent.text = "Fix Menu";
            this.CloseMenu();


            // Hide the menu if it changes to the defender's turn
            GameEvents.OnTurnChange((turnChangeArgs) => {
                    this.CloseMenu();
            });
        }
        
        /// <summary>
        /// Keeps information on the menu up to date
        /// </summary>
        public void UpdateMenu()
        {
 
        }

        /// <summary>
        /// Shows the menu
        /// </summary>
        /// <param name="position"></param>
        public void OpenMenu()
        {
            this.CloseMenu();
            MenuToDisplay menu = this.module.GetFixMenu(new MenuBuilder());
            
            int start = 0;
            int end = menu.MenuChoices.Count;

            if (menu.MenuChoices.Count > buttons.Count())
                end = buttons.Count();

            // Populate text for all buttons
            for (int i = start; i < end; i++)
            {
                var btnText = buttons[i].GetComponentInChildren<Text>();
                btnText.text = menu.MenuChoices[i].DisplayName;
                this.addListener(i);

                buttons[i].gameObject.SetActive(true);
            }

            // Move to position
            RectTransform uiTransform = FixMenuController.popupInstance.GetComponent<RectTransform>();
            uiTransform.position = new Vector2((float)1675, (float)210);
            uiTransform.rotation = Quaternion.Euler(0,0,90);

            FixMenuController.popupInstance.SetActive(true);
        }

        /// <summary>
        /// Hides the menu
        /// </summary>
        public void CloseMenu()
        {
            // Set all buttons as inactive 
            for (int i = 0; i < buttons.Count(); i++)
            {
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].GetComponentInChildren<Text>().text = "";
                buttons[i].gameObject.SetActive(false);
            }

            FixMenuController.popupInstance.SetActive(false);
        }


        /// <summary>
        /// Creates a click listener on a button that calls the Attack function 
        /// on the module this controller represents. it will passes in the 
        /// button text as a parameter Then closes the menu
        /// </summary>
        /// <param name="i"></param>
        private void addListener(int i)
        {
            buttons[i].onClick.AddListener(() => {
                var btnText = buttons[i].GetComponentInChildren<Text>();
                this.module.SetAttackToFix(btnText.text); // Attack the module
                this.CloseMenu();
            });
        }
    }
}
