﻿using UnityEngine;
using UnityEngine.UI;
namespace Assets.GameLogic
{
    public class AttackerUI : MonoBehaviour
    {
        private GameControllerWrapper gameController;
        public Text attacksLeftText;
        public Text turnTimer;
        public Text turnCounter;
        public Text roundCounter;
        
        private void Start()
        {
            this.gameController = new GameControllerWrapper();
        }

        /// <summary>
        /// Displays how many attacks the attacker has left
        /// </summary>
        private void Update()
        {
            this.attacksLeftText.text = "Attacks: " + gameController.GetAttacks();
        }

        public void SetTurnText(string turnText)
        {
            this.turnCounter.text = turnText;
        }

        public void SetRoundText(string roundText)
        {
            this.roundCounter.text = roundText;
        }

        public void SetTurnTimer(string timerText)
        {
            this.turnTimer.text = timerText;
        }
    }
}