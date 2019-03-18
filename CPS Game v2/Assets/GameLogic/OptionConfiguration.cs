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
    private int rounds;
	private int owls;
    private int attacks;
    public Text RoundText;
	public Text OwlsText;
	public Text AttacksText;
	
	private Module firstModule; 
	
	private void Start()
    {
        this.rounds = 1;
        this.owls = 1;
        this.attacks = 1;
    }
	
	private void Update()
	{
		RoundText.text = "Rounds:\t\t\t\t\t\t\t\t\t\t\t\t" + this.rounds.ToString();
		OwlsText.text = "Owls:\t\t\t\t\t\t\t\t\t\t\t\t\t" + this.owls.ToString();
		AttacksText.text = "Attacks: \t\t\t\t\t\t\t\t\t\t\t\t" + this.attacks.ToString();
	}
	
    public int getRounds()
	{
		return rounds;
	}
	
	public void setRounds(int value)
	{
		rounds = value;
	}
	
	public void incRounds()
	{
		rounds = rounds + 1;
	}
	
	public void decRounds()
	{
		rounds = rounds - 1;
	}
	
	public void incOwls()
	{
		owls = owls + 1;
	}
	
	public void decOwls()
	{
		owls = owls - 1;
	}
	
	public void incAttacks()
	{
		attacks = attacks + 1;
	}
	
	public void decAttacks()
	{
		attacks = attacks - 1;
	}
	
	public int getOwls()
	{
		return owls;
	}
	
	public void setOwls(int value)
	{
		owls = value;
	}
	
	public int getAttacks()
	{
		return attacks;
	}
	
	public void setAttacks(int value)
	{
		attacks = value;
	}
}