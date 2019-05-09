using UnityEngine;
using UnityEngine.UI;
namespace Assets.GameLogic
{
    public class DefenderUI : MonoBehaviour
    {
        private GameControllerWrapper gameController;
        public Text turnTimer;
        public Text turnCounter;
        public Text roundCounter;
        
        private void Start()
        {
            this.gameController = new GameControllerWrapper();
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