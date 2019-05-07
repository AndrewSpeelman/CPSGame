using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameLogic;

public class ModeButton : MonoBehaviour
{
	public bool isFixMode = false;
	private Oracle parentOracle;
	private GameControllerWrapper gameController;
	
	private void Awake()
    {
        this.parentOracle = this.GetComponentInParent<Oracle>();
		this.gameController = new GameControllerWrapper();
	}
	
	private void OnMouseDown()
	{
		if (this.gameController.IsDefendersTurn())
		{
			isFixMode = (isFixMode) ? false : true;
			parentOracle.SwapMode(isFixMode);
		}
	}
}
