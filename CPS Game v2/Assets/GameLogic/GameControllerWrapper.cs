using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameLogic
{
    public class GameControllerWrapper
    {
        private GameController gameController;

        public GameControllerWrapper()
        {
            this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }


        public bool IsAttackersTurn()
        {
            if (!this.gameController)
                return false; 

            return this.gameController.GameState == GameState.AttackerTurn;
        }

        public bool IsDefendersTurn()
        {
            if (!this.gameController)
                return false; 

            return this.gameController.GameState == GameState.AttackerTurn;
        }
    }
}
