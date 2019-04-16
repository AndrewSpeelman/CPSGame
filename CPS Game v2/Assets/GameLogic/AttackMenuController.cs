using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameLogic
{
    public class AttackMenuController
    {
        private GameController gameController;
        private Canvas rootCanvas;

        private Module module;

        public GameObject popupPrefab;
        private GameObject popupInstance; 


        public AttackMenuController(Module obj, GameObject popupPrefab)
        {
            this.module = obj;

            this.gameController = GameControllerWrapper.GetGameController();
            this.rootCanvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
            //this.popupPrefab = (GameObject)Resources.Load("Prefabs/ModulePopup", typeof(GameObject));
            
            this.popupPrefab = popupPrefab;
            this.popupInstance = UnityEngine.Object.Instantiate(this.popupPrefab, this.popupPrefab.transform.position, this.popupPrefab.transform.rotation);

            this.popupInstance.transform.SetParent(this.rootCanvas.transform, false);
            this.CloseMenu();

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
        public void OpenMenu(Vector2 position)
        {
            this.CloseMenu();
            this.UpdateMenu();

            // Move to position
            RectTransform uiTransform = this.popupInstance.GetComponent<RectTransform>(); 
            uiTransform.position = position + new Vector2((uiTransform.rect.width / 2), (uiTransform.rect.height / 2));

            this.popupInstance.SetActive(true);
        }

        /// <summary>
        /// Hides the menu
        /// </summary>
        public void CloseMenu()
        {
            this.popupInstance.SetActive(false);
        }
    }
}
