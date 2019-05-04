using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controls default settings of game
/// </summary>
public class OptionConfiguration : MonoBehaviour
{
    public Text RoundsText;
    public Text OraclesText;
    public Text AttacksText;
    private Module firstModule; 
	
    private void Update()
    {
        RoundsText.text = "Rounds:\t\t\t\t\t\t\t\t\t\t\t\t" + Options.RoundLimit.ToString();
        OraclesText.text = "Oracles:\t\t\t\t\t\t\t\t\t\t\t\t" + Options.Oracles.ToString();
        AttacksText.text = "Attacks: \t\t\t\t\t\t\t\t\t\t\t\t" + Options.Attacks.ToString();
	}
	
    public void incRounds()
    {
        Options.RoundLimit = ++Options.RoundLimit;
    }

    public void decRounds()
    {
        Options.RoundLimit = --Options.RoundLimit;
    }

    public void incOracles()
    {
        Options.Oracles = ++Options.Oracles;
    }

    public void decOracles()
    {
        Options.Oracles = --Options.Oracles;
    }
	
    public void incAttacks()
    {
        Options.Attacks = ++Options.Attacks;
    }

    public void decAttacks()
    {
        Options.Attacks = --Options.Attacks;
    }
}
