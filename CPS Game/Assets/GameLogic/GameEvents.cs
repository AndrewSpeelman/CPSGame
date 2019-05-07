using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameLogic
{
	public class TurnChangeArgs : EventArgs
    {
        public GameState State;
    }


    public static class GameEvents
    {
		/// <summary>
        /// Turn Change Event
        /// </summary>
        public static event EventHandler TurnChange;
		public static void OnTurnChange(Action<TurnChangeArgs> callback)
        {
            TurnChange += new EventHandler((s, e) => {
                callback((TurnChangeArgs)e);
            });
        }
		public static void DispatchTurnChangeEvent(GameState state)
        {
            EventHandler handler = TurnChange;
            if (handler != null)
            {
                handler(null, new TurnChangeArgs() { State = state });
            }
        }


    }
}
