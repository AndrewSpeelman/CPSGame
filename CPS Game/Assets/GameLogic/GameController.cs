﻿using Assets;
using Assets.GameLogic;
using Assets.Modules;
using Assets.Modules.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls whose turn it is, the actions available to the players, and other game logic
/// </summary>
public class GameController : MonoBehaviour
{
    public WaterFlowController WaterFlowController;
    public SceneLoader SceneLoader;

    public GameObject OraclePrefab;
    public Vector3 OracleSpawnPoint;

    public GameObject AttackerUI;
    public GameObject DefenderUI;
    private AttackerUI AttackerUIObject;
    private DefenderUI DefenderUIObject;

    public Reservoir Reservoir;

    public Text TurnCounter;
    public Text ReservoirCounter;

    public Image ScreenCover;
    public Image AttackerUICover;
    public Image DefenderUICover;
    public GameObject GameUI;
    public ScoreController ScoreController;
    public GameObject GameBoard;
    private AttackableModule[] GameBoardObjects;
    public Text TurnText;

    public int NumberOfAttacksPerTurn = 1;
    public int NumberOfOracles = 2;
    public int NumAvailableAttacks { get; set; }

    private int Turn = 0;
    private int Round = Options.Round;
    private int RoundLimit = Options.RoundLimit;

    public int ReservoirLimit = 10;
    public int TurnLimit = 5;

    public Text TurnTimer;
    private DateTime ActiveTurnTimer;
    private DateTime StartTurnTimer;
    public int TurnDuration = 15; // Seconds
    private bool ActiveTurn;


    public GameState GameState;

    private List<Oracle> oracles;

    public List<WaterObject> WaterLeavingSystemOnLastTurnChange;

    /// <summary>
    /// Event listeners for when the turn changes
    /// </summary>
    private event EventHandler TurnChange;

    protected void Awake()
    {
        this.GameBoardObjects = GameObject.FindObjectsOfType<AttackableModule>();
        this.NumberOfAttacksPerTurn = Options.Attacks;
        this.NumberOfOracles = Options.Oracles;
        this.oracles = new List<Oracle>();
        this.AttackerUIObject = GameObject.FindGameObjectWithTag("Attacker").GetComponent<AttackerUI>();
        this.DefenderUIObject = GameObject.FindGameObjectWithTag("Defender").GetComponent<DefenderUI>();
        this.ScoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
        TurnText.gameObject.SetActive(true);
        ScreenCover.gameObject.SetActive(false);
        ScreenCover.fillCenter = true;
        AttackerUIObject.SetRoundText("Round: " + Round + "/" + RoundLimit);
        DefenderUIObject.SetRoundText("Round: " + Round + "/" + RoundLimit);
        this.WaterLeavingSystemOnLastTurnChange = new List<WaterObject>();
        AttackerUICover.gameObject.SetActive(false);
        DefenderUICover.gameObject.SetActive(false);
    }

    protected void Start()
    {
        for (int i = 0; i < this.NumberOfOracles; i++)
        {
            var newOracle = Instantiate(this.OraclePrefab, new Vector3(this.OracleSpawnPoint.x, this.OracleSpawnPoint.y - (i * 2), this.OracleSpawnPoint.z), this.OraclePrefab.transform.rotation);
            newOracle.transform.Rotate(0,0,90);
            oracles.Add(newOracle.GetComponent<Oracle>());
        }
        AttackerUIObject.SetTurnText(" Turn: " + Turn + "/" + TurnLimit);
        DefenderUIObject.SetTurnText(" Turn: " + Turn + "/" + TurnLimit);

        this.EndTurn();
        StartTurnTimer = DateTime.Now;
        ActiveTurn = true;
    }

    public void EndTurn()
    {
        ActiveTurn = false;

        if (this.GameState == GameState.AttackerTurn)
        {
            this.GameState = GameState.DefenderTurn;
            this.AttackerUI.SetActive(false);
            this.DefenderUI.SetActive(true);
            TurnText.text = "Defender's\nTurn";
            TurnText.color = new Color(0, .5F, 1F);

            this.WaterLeavingSystemOnLastTurnChange = this.WaterFlowController.TickModules();
            foreach(WaterObject w in this.WaterLeavingSystemOnLastTurnChange)
            {
                ScoreController.AddDefenderScore(w.GetWaterScore());
            }
            TurnText.transform.Rotate(0,0,180);
            ScoreController.RotateScoreText(180);
            AttackerUICover.gameObject.SetActive(true);
            DefenderUICover.gameObject.SetActive(false);
        }
        else
        {
            this.GameState = GameState.AttackerTurn;
            this.NumAvailableAttacks = this.NumberOfAttacksPerTurn;

            AttackerUICover.gameObject.SetActive(false);
            DefenderUICover.gameObject.SetActive(true);
            ScoreController.RotateScoreText(180);
            this.AttackerUI.SetActive(true);
            this.DefenderUI.SetActive(false);
            
            // Reset modules attachments
            foreach(AttackableModule m in this.GameBoardObjects)
            {
                m.HasInspectorAttached = false;
                m.HasFixerAttached = false;
                m.ResetColor();
            }

            // Oracles Get their values and fixes done
            foreach (Oracle o in this.oracles)
            {
                o.InputActive = false;
                o.InspectModule();
                o.FixModule();
            }

            // Score points for non-fixed modules
            foreach(AttackableModule m in this.GameBoardObjects)
            {
                if(m.IsAttacked)
                {
                    m.SetAttackDuration(m.GetAttackDuration()+1);
                    ScoreController.AddAttackerScore(m.GetAttackDuration() * Ints.Score.Defender.Fix);
                }
            }

            if (++Turn > TurnLimit)
            {
                Options.Round = ++Options.Round;
                Results.AttackerVictory = ScoreController.GetWinner();
                this.SceneLoader.LoadVictoryScene();
            }
            AttackerUIObject.SetTurnText(" Turn: " + Turn + "/" + TurnLimit);
            DefenderUIObject.SetTurnText(" Turn: " + Turn + "/" + TurnLimit);
            TurnText.text = "Attacker's\n Turn";
            TurnText.color = new Color(1F, 0, 0);
            TurnText.transform.Rotate(0,0,180);
        }

        ScreenCover.gameObject.GetComponentsInChildren<Text>()[0].text = TurnText.text;
        ScreenCover.gameObject.GetComponentsInChildren<Text>()[0].color = TurnText.color;
        
        StartCoroutine(WaitForClick());

        GameEvents.DispatchTurnChangeEvent(this.GameState);
    }

    protected void Update()
    {
        if (ActiveTurn)
        {
            ActiveTurnTimer = DateTime.Now;
            int SecondsRemaining = (TurnDuration - (ActiveTurnTimer - StartTurnTimer).Seconds);
            AttackerUIObject.SetTurnTimer("Time: " + SecondsRemaining.ToString());
            DefenderUIObject.SetTurnTimer("Time: " + SecondsRemaining.ToString());

            if (ActiveTurnTimer > StartTurnTimer.AddSeconds(TurnDuration))
            {
                EndTurn();
            }
        }
    }

    protected IEnumerator WaitForClick()
    {
        ScreenCover.gameObject.SetActive(true);
        GameUI.SetActive(false);
        GameBoard.SetActive(false);
        TurnTimer.gameObject.SetActive(false);

        yield return new WaitWhile(() => !Input.GetMouseButtonDown(0));

        ScreenCover.gameObject.SetActive(false);
        TurnTimer.gameObject.SetActive(true);
        GameUI.SetActive(true);
        GameBoard.SetActive(true);

        ActiveTurn = true;
        StartTurnTimer = DateTime.Now;
        if (this.GameState == GameState.DefenderTurn)
        {
            this.oracles.ForEach(o => o.InputActive = true);
        }
    }
}
