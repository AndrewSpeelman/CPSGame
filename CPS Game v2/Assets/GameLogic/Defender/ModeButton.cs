using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		isFixMode = (isFixMode) ? false : true;
		parentOracle.SwapMode(isFixMode);
	}
}
