using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameLogic
{
    public class ScoreControllerWrapper
    {
        private ScoreController scoreController;

        public ScoreControllerWrapper()
        {
            this.scoreController = ScoreControllerWrapper.GetScoreController();
        }

        /// <summary>
        /// A static function for retrieving the score controller 
        /// </summary>
        /// <returns></returns>
        public static ScoreController GetScoreController()
        {
            return GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
        }
    }
}