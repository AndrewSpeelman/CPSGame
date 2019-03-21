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
    private int Rounds;
    private int Oracles;
    private int Attacks;
    public Text RoundsText;
    public Text OraclesText;
    public Text AttacksText;
    private Module firstModule; 
	
    private void Start()
    {
        this.Rounds = 1;
        this.Oracles = 2;
        this.Attacks = 1;
    }

    private void Update()
    {
        RoundsText.text = "Rounds:\t\t\t\t\t\t\t\t\t\t\t\t" + this.Rounds.ToString();
        OraclesText.text = "Oracles:\t\t\t\t\t\t\t\t\t\t\t\t" + this.Oracles.ToString();
        AttacksText.text = "Attacks: \t\t\t\t\t\t\t\t\t\t\t\t" + this.Attacks.ToString();
        PlayerPrefs.SetInt("Rounds",Rounds);
        PlayerPrefs.SetInt("Oracles",Oracles);
        PlayerPrefs.SetInt("Attacks",Attacks);
	}

    public int getRounds()
    {
        return Rounds;
    }
	
    public void incRounds()
    {
        Rounds = Rounds + 1;
    }

    public void decRounds()
    {
        Rounds = Rounds - 1;
    }

    public void incOracles()
    {
        Oracles = Oracles + 1;
    }

    public void decOracles()
    {
        Oracles = Oracles - 1;
    }
	
    public void incAttacks()
    {
        Attacks = Attacks + 1;
    }

    public void decAttacks()
    {
        Attacks = Attacks - 1;
    }
}