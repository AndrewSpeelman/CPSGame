using Assets.Interfaces.Modules;
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


        public virtual bool Attack()
        {
            this._IsAttacked = true;
            return true;
        }

        public virtual bool Fix()
        {
            this._IsAttacked = false;
            return true;
        }

        /// <summary>
        /// When the user clicks on a module
        /// </summary>
        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.renderer.material.color = Color.yellow; // Turn color yellow while user is clicking on the object
                
                if (this.gameController.IsAttackersTurn())
                {
                    // TODO: Show attack menu
                }
            }
        }

        /// <summary>
        /// Restore color when the mouse is released
        /// </summary>
        private void OnMouseUp()
        {
            this.renderer.material.color = this.startingColor;
        }

    }
}
