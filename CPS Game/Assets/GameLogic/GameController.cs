using Assets.GameLogic;
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

    public Reservoir Reservoir;

    public Text TurnCounter;
    public Text ReservoirCounter;

    public Image ScreenCover;
    public GameObject GameUI;
    public GameObject GameBoard;
    private Module[] GameBoardObjects;
    public Text TurnText;

    public int NumberOfAttacksPerTurn = 1;
    public int NumberOfOracles = 2;
    public int NumAvailableAttacks { get; set; }

    private int Turn = 0;
    private int Round = 1;
    private int RoundLimit = 1;

    public int ReservoirLimit = 10;
    public int TurnLimit = 5;

    public Text TurnTimer;
    private DateTime ActiveTurnTimer;
    private DateTime StartTurnTimer;
    public int TurnDuration = 15; // Seconds
    private bool ActiveTurn;


    public GameState GameState;

    private List<Oracle> oracles;

    /// <summary>
    /// Event listeners for when the turn changes
    /// </summary>
    private event EventHandler TurnChange;

    protected void Awake()
    {
        this.GameBoardObjects = GameObject.FindObjectsOfType<Module>();
        this.NumberOfAttacksPerTurn = Options.Attacks;
		this.Round = Options.Round;
        this.RoundLimit = Options.RoundLimit;
        this.NumberOfOracles = Options.Oracles;
        Results.ReservoirLimit = ReservoirLimit;
        this.oracles = new List<Oracle>();
        TurnText.gameObject.SetActive(true);
        ScreenCover.gameObject.SetActive(false);
        ScreenCover.fillCenter = true;
    }

    protected void Start()
    {
        for (int i = 0; i < this.NumberOfOracles; i++)
        {
            var newOracle = Instantiate(this.OraclePrefab, new Vector3(this.OracleSpawnPoint.x - (i * 2), this.OracleSpawnPoint.y, this.OracleSpawnPoint.z), this.OraclePrefab.transform.rotation);
            oracles.Add(newOracle.GetComponent<Oracle>());
        }

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
            TurnText.text = "Defender's Turn";
            TurnText.color = new Color(0, .5F, 1F);
        }
        else
        {
            this.GameState = GameState.AttackerTurn;
            this.NumAvailableAttacks = this.NumberOfAttacksPerTurn;

            this.AttackerUI.SetActive(true);
            
            foreach(Module m in this.GameBoardObjects)
            {
                m.HasInspectorAttached = false;
                m.HasFixerAttached = false;
                m.ResetColor();
            }
            
            foreach (Oracle o in this.oracles)
            {
                o.InputActive = false;
                o.InspectModule();
                o.FixModule();
            }

            for (int i = 0; i < 13; i++) {
                this.WaterFlowController.TickModules();
            }

            if (++Turn > TurnLimit)
            {
                Options.Round = ++Options.Round;
                if(Round >= RoundLimit)
                {
                    this.SceneLoader.LoadVictoryScene();
                }
                else
                {
                    this.SceneLoader.LoadGameScene();
                }
            }
            TurnCounter.text = "Round: " + Round + "/" + RoundLimit + " Turn: " + Turn + "/" + TurnLimit;
            TurnText.text = "Attacker's Turn";
            TurnText.color = new Color(1F, 0, 0);
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
            TurnTimer.text = "Time Left: " + SecondsRemaining.ToString();

            if (SecondsRemaining > 5)
            {
                TurnTimer.color = new Color(.79f, .82f, .16f);
            }
            else if (SecondsRemaining % 2 == 0)
            {
                TurnTimer.color = new Color(1f, .3f, .15f);
            }
            else
            {
                TurnTimer.color = new Color(1f, .2f, 0);
            }

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
