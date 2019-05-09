using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour {
    public Text VictorTextbox;
    public Button RestartButton;
    public Button ExitButton;

    // Use this for initialization
    void Start()
    {

		if (!Results.AttackerVictory)
        {
            VictorTextbox.color = new Color(0.45f, 0.75f, 1f);
            VictorTextbox.text = "Defender Wins!";
        }
        if(Options.Round != Options.RoundLimit)
        {
            RestartButton.gameObject.SetActive(false);
        }
        else
        {
            ExitButton.gameObject.SetActive(false);
        }
    } 
}
