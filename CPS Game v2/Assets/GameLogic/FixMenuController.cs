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

        //public GameObject popupPrefab;
        //private GameObject popupInstance;

       // private Button[] buttons;
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

            //this.buttons = FixMenuController.popupInstance.GetComponentsInChildren<Button>();

            textContent.text = "Fix Menu";
            this.CloseMenu();


            // Hide the menu if it changes to the defender's turn
            GameEvents.OnTurnChange((turnChangeArgs) => {
                if (turnChangeArgs.State == GameState.DefenderTurn)
                    this.CloseMenu();
            });
        }
        
        /// <summary>
        /// Keeps information on the menu up to date
        /// </summary>
        public void UpdateMenu()
        {
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
                //this.buttons[i].transform.position = new Vector3(100, this.buttons[i].transform.position.y, this.buttons[i].transform.position.z);
            }
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
            RectTransform uiTransform = FixMenuController.popupInstance.GetComponent<RectTransform>();
            uiTransform.position = new Vector2((float)400, (float)500);

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
                //this.buttons[i].transform.position = new Vector3(1000000, this.buttons[i].transform.position.y, this.buttons[i].transform.position.z);
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
                this.gameController.NumAvailableAttacks--; // Decrease the number of attacks left
                var btnText = buttons[i].GetComponentInChildren<Text>();
                this.module.Attack(btnText.text); // Attack the module
                this.CloseMenu();
            });
        }
    }
}
