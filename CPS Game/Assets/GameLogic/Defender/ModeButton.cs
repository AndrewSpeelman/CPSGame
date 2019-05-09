using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameLogic;

public class ModeButton : MonoBehaviour
{
	public bool isFixMode = false;
	private Oracle parentOracle;
	
	private void Awake()
    {
        this.parentOracle = this.GetComponentInParent<Oracle>();
	}
	
	private void OnMouseDown()
	{
        if (!this.parentOracle.InputActive)
            return;
		isFixMode = !isFixMode;
		parentOracle.SwapMode(isFixMode);
	}
}
