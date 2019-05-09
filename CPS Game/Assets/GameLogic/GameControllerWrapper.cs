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
            this.gameController = GameControllerWrapper.GetGameController();
        }

        /// <summary>
        /// A static function for retrieving the game controller so it can be used in 
        /// multiple places without needing to type this stuff all over again.
        /// </summary>
        /// <returns></returns>
        public static GameController GetGameController()
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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

            return this.gameController.GameState == GameState.DefenderTurn;
        }

        public bool IsAttackersAttacksZero()
        {
            if (!this.gameController)
                return false; 

            return this.gameController.NumAvailableAttacks == 0;
        }

        public int GetAttacks()
        {
            return this.gameController.NumAvailableAttacks;
        }
        
    }
}
