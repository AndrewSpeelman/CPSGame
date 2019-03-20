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
    public int rounds;
	public int oracles;
    public int attacks;
    public Text RoundsText;
	public Text OraclesText;
	public Text AttacksText;
	
	private Module firstModule; 
	
	private void Start()
    {
        this.rounds = 1;
        this.oracles = 2;
        this.attacks = 1;
    }
	
	private void Update()
	{
		RoundsText.text = "Rounds:\t\t\t\t\t\t\t\t\t\t\t\t" + this.rounds.ToString();
		OraclesText.text = "Oracles:\t\t\t\t\t\t\t\t\t\t\t\t" + this.oracles.ToString();
		AttacksText.text = "Attacks: \t\t\t\t\t\t\t\t\t\t\t\t" + this.attacks.ToString();
		PlayerPrefs.SetInt("Rounds",rounds);
		PlayerPrefs.SetInt("Oracles",oracles);
		PlayerPrefs.SetInt("Attacks",attacks);
	}
	
    public int getRounds()
	{
		return rounds;
	}
	
	
	public void incRounds()
	{
		rounds = rounds + 1;
	}
	
	public void decRounds()
	{
		rounds = rounds - 1;
	}
	
	public void incOracles()
	{
		oracles = oracles + 1;
	}
	
	public void decOracles()
	{
		oracles = oracles - 1;
	}
		
	public void incAttacks()
	{
		attacks = attacks + 1;
	}
	
	public void decAttacks()
	{
		attacks = attacks - 1;
	}
}