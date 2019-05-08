using Assets.GameLogic;
using Assets.Interfaces.Modules;
using Assets.Modules.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules
{
    public class AttackableModule : Module, ICanBeAttacked
    {
        [SerializeField]
        private bool _IsAttacked = false;
        public bool IsAttacked { get { return _IsAttacked; } set { _IsAttacked = value; } }
        public GameObject AttackPopupPrefab;
        public GameObject FixPopupPrefab;
        private AttackMenuController attackMenuController;
        private FixMenuController fixMenuController;
        

        /// <summary>
        /// Default Attack method
        /// </summary>
        /// <returns></returns>
        public virtual bool Attack(String AttackMenuOption)
        {
            this._IsAttacked = true;
            return true;
        }

        /// <summary>
        /// Method called by menus
        /// </summary>
        /// <returns></returns>
        public virtual void SetAttackToFix(String FixMenuOption)
        {

        }
        /// <summary>
        /// Default Fix method, oracles call this method
        /// </summary>
        /// <returns></returns>
        public override bool Fix()
        {
            this._IsAttacked = false;
            return true;
        }

        /// <summary>
        /// When created
        /// </summary>
        public override void OnAwake()
        {
            this.attackMenuController = new AttackMenuController(this, this.AttackPopupPrefab);
            this.fixMenuController = new FixMenuController(this, this.FixPopupPrefab);
            
        }

        /// <summary>
        /// Default attack menu
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual MenuToDisplay GetAttackMenu(MenuBuilder builder)
        {
            builder.SetTitle(this.GetType().Name.ToString() + " Attacks");

            return builder.Build();
        }

        /// <summary>
        /// Default Fix menu
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual MenuToDisplay GetFixMenu(MenuBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Update popup menus each tick
        /// </summary>
        public override void UpdatePopups()
        {
        }

        /// <summary>
        /// When the user clicks on a module
        /// </summary>
        public override void OnMouseDown()
        {
            base.OnMouseDown();

            if (Input.GetMouseButtonDown(0))
            {
                if (this.gameController.IsAttackersTurn() && !this.gameController.IsAttackersAttacksZero())
                {
                    this.attackMenuController.OpenMenu();
                }
            }
        }

        /// <summary>
        /// Open Fix Menu
        /// <summary>
        public void OpenFixMenu()
        {
            this.fixMenuController.OpenMenu();
        }

        /// <summary>
        /// Open Fix Menu
        /// <summary>
        public void CloseFixMenu()
        {
            this.fixMenuController.CloseMenu();
        }
    }
}
