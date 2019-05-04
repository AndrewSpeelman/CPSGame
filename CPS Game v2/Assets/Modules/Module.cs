using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Interfaces.Modules;
using Assets.Interfaces;
using Assets.Modules.Menu;
using Assets;
using Assets.GameLogic;
using System;

public abstract class Module : MonoBehaviour, IModule, IHaveFlow, IHoldWater, IDetectPurity
{
    // These are used to control the order of the system
    public Module NextModule;
    public Module PreviousModule;
	public bool Attacked = false;

    public WaterObject Water { get; protected set; }

    /// <summary>
    /// Represents if water is flowing through something
    /// </summary>
    public bool HasFlow { get { return this.Water != null; } }

    /// <summary>
    /// Will be true if an oracle inspector is attached to this module
    /// </summary>
    public bool HasInspectorAttached = false;

    /// <summary>
    /// Will be true if an oracle fixer is attached to this module
    /// </summary>
    public bool HasFixerAttached = false;

    public GameObject InfoPopupPrefab;
    private InfoMenuController infoMenuController;

    /// <summary>
    /// Expected Water Purity
    /// </summary>
    [SerializeField]
    private bool _ExpectedPurity1 = false;
    public bool ExpectedPurity1 { get { return _ExpectedPurity1; } set { _ExpectedPurity1 = value; } }

    [SerializeField]
    private bool _ExpectedPurity2 = false;
    public bool ExpectedPurity2 { get { return _ExpectedPurity2; } set { _ExpectedPurity2 = value; } }

    [SerializeField]
    private bool _ExpectedPurity3 = false;
    public bool ExpectedPurity3 { get { return _ExpectedPurity3; } set { _ExpectedPurity3 = value; } }


    /**
     * Unity Things 
     */
    protected new Renderer renderer;
    protected Color startingColor;
    protected GameControllerWrapper gameController;


    public Module()
    {
        this.Water = new WaterObject();
    }


    /// <summary>
    /// Don't override this one
    /// </summary>
    public void Start()
    {
        this.gameController = new GameControllerWrapper();
        this.renderer = GetComponent<Renderer>();
        this.startingColor = renderer.material.color;

        this.OnStart();
    }

    /// <summary>
    /// Override this method if you need it
    /// </summary>
    public virtual void OnStart()
    {

    }

    /// <summary>
    /// Don't override this
    /// </summary>
    private void Awake()
    {
        this.infoMenuController = new InfoMenuController(this, this.InfoPopupPrefab);
        this.OnAwake();
    }

    /// <summary>
    /// Override this if you need it
    /// </summary>
    public virtual void OnAwake()
    {

    }


    /// <summary>
    /// Don't override this
    /// </summary>
    public void Tick()
    {
        this.OnTick();
        this.UpdatePopups();
    }


    /// <summary>
    /// Override this one
    /// </summary>
    public virtual void OnTick()
    {

    }

    /// <summary>
    /// Override this specifically for updating popup displays
    /// </summary>
    public virtual void UpdatePopups()
    {

    }



    /**
     * Determines if the purity is as it should be 
     */
    public bool IsPurityAsExpected
    {
        get
        {
            if (this.Water == null)
                return true;

            if (this.Water.Purity1 != ExpectedPurity1)
                return false;
            if (this.Water.Purity2 != ExpectedPurity2)
                return false;
            if (this.Water.Purity3 != ExpectedPurity3)
                return false;

            return true;
        }
    }

    /// <summary>
    /// what to do when the module is no longer being attacked
    /// </summary>
    public virtual bool Fix()
    {
        if (this.Attacked)
        {
            this.Attacked = false;
            return true;
        }
        return false;
    }

    /**
     * Returns the water in the module 
     */
    public virtual WaterObject getWater()
    {
        return this.Water;
    }


    /**
     * Shift the water
     */
    public virtual WaterObject OnFlow(WaterObject inflow)
    {
        if (inflow == null)
        {
            this.Water = null;
            return null; 
        }

        if (this.Water == null)
        {
            this.Water = inflow.Copy();
            return null;
        }

        WaterObject cpy = this.Water.Copy();
        this.Water = inflow.Copy();

        return cpy;
    }


    /// <summary>
    /// Default menu has basic information
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public virtual MenuToDisplay GetInformation(MenuBuilder builder)
    {
        builder.AddBoolItem(Strings.HasFlow, this.HasFlow);
        builder.AddBoolItem(Strings.IsPurityAsExpected, this.IsPurityAsExpected);

        return builder.Build();
    }



    /// <summary>
    /// When the user clicks on a module
    /// </summary>
    public virtual void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            try
            {
                this.renderer.material.color = Color.yellow; // Turn color yellow while user is clicking on the object
            }
            catch (Exception e)
            {

            }

            if (this.gameController.IsAttackersTurn())
            {
                this.infoMenuController.OpenMenu();
            }
        }
    }

    /// <summary>
    /// Restore color when the mouse is released
    /// </summary>
    public virtual void OnMouseUp()
    {
        try
        {
            this.renderer.material.color = this.startingColor;
        }
        catch (Exception e)
        {

        }
    }
}

/// <summary>
/// Abstract class with implementation that is common to all modules.  Unless the virtual functions are overriden for custom functionality,
/// the modules will pass water through them iff their corresponding pump is on and they have the capacity to take in more water.
/// </summary>
//public abstract class Module2 : MonoBehaviour
//{
//    public GameObject popupPrefab;
//    public GameObject AttackedIndicator;

//    public Module PreviousModule;

//    public PumpOld InFlowingPump;

//    public WaterObject Water;

//    public virtual int Capacity
//    {
//        get { return 3; }
//    }

//    private GameController gameController;

//    protected List<string> displayFields;
//	private GameObject popupInstance;
//	private Text displayTextTitle;
//	private Text displayTextContent;

//    private Renderer renderer;
//    private Color startingColor;

//    protected Dropdown[] AttackDropdowns;

//    public bool HasFlow {
//        get {
//            return this.Water != null;
//        }
//    }
//    public bool Purity1 {
//        get {
//            if (this.Attacked && this.GetType() == typeof(Pipe))
//            {
//                return this.AttackDropdowns[1].value == 0;
//            }
//            else
//            {
//                if (this.Water != null)
//                    return this.Water.purity[0];
//                else
//                    return false;
//            }
//        }
//    }
//    public bool Purity2 {
//        get {
//            if (this.Attacked && this.GetType() == typeof(Pipe))
//            {
//                return this.AttackDropdowns[2].value == 0;
//            }
//            else
//            {
//                if (this.Water != null)
//                    return this.Water.purity[1];
//                else
//                    return false;
//            }
//        }
//    }
//    public bool Purity3 {
//        get {
//            if (this.Attacked && this.GetType() == typeof(Pipe))
//            {
//                return this.AttackDropdowns[3].value == 0;
//            }
//            else
//            {
//                if (this.Water != null)
//                    return this.Water.purity[2];
//                else
//                    return false;
//            }
//        }
//    }

//    private GameObject attackedIndicatorInstance;
//    private Canvas rootCanvas;

//    public virtual bool IsFilter()
//    {
//        return false;
//    }

//    public virtual bool IsPump()
//    {
//        return false;
//    }

//	private void Awake() {
//        this.displayFields = new List<string>
//        {
//            "Attacked",
//            "Capacity",
//            "HasFlow",
//            "Purity1",
//            "Purity2",
//            "Purity3"
//        };

//        rootCanvas = (Canvas)FindObjectOfType(typeof(Canvas));

//        //Instantiate the popup that displays the display fields
//        this.popupInstance = Instantiate (this.popupPrefab, this.popupPrefab.transform.position, this.popupPrefab.transform.rotation);
//		this.popupInstance.transform.SetParent(this.rootCanvas.transform, false);
//		var texts = this.popupInstance.GetComponentsInChildren<Text>();
//		if (texts.Length == 2) {
//			this.displayTextContent = texts[1];
//			this.displayTextTitle = texts[0];
//		}
//		this.displayTextTitle.text = this.gameObject.name;

//		this.CloseInfoPopup();

//        this.attackedIndicatorInstance = Instantiate(this.AttackedIndicator, 
//            Camera.main.WorldToScreenPoint(this.transform.position),
//            this.AttackedIndicator.transform.rotation);
//        this.attackedIndicatorInstance.transform.SetParent(GameObject.FindGameObjectWithTag("Attacker").transform);
//        this.attackedIndicatorInstance.SetActive(false);

//        this.AttackDropdowns = this.attackedIndicatorInstance.GetComponentsInChildren<Dropdown>();
//        var cancelAttackButton = this.attackedIndicatorInstance.GetComponentInChildren<Button>();
//        if (cancelAttackButton)
//        {
//            cancelAttackButton.onClick.AddListener(delegate { this.ReverseAttack(); });
//        }
//	}

//    protected void Start()
//    {
//        this.gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
//        this.renderer = GetComponent<Renderer>();
//        this.startingColor = renderer.material.color;
//    }

//    /// <summary>
//    /// Moves water through system if specified pump is on. Then calls Tick for previous module.
//    /// </summary>
//    public virtual void Tick()
//    {
//        if (this.InFlowingPump.On)
//        {
//            this.OnFlow();
//        }

//        /*if (Water.Amount > this.Capacity)
//        {
//            this.OnOverflow();
//        }*/

//        this.UpdatePopupDisplay();
//        if (this.PreviousModule)
//            this.PreviousModule.Tick();
//    }

//    /// <summary>
//    /// Only called when the pump is on.  Brings as much water as it can from the previous module into this one.
//    /// Override for custom functionality in modules!
//    /// </summary>
//    protected virtual void OnFlow()
//    {
//        if (this.PreviousModule && this.Water == null)
//        {
//            this.Water = this.PreviousModule.Water;
//            this.PreviousModule.Water = null;
//        }
//    }

//    /// <summary>
//    /// Override to specify how the module behaves when fill exceeds capacity.  (Can only occur if OnFlow is overritten)
//    /// </summary>
//    protected virtual void OnOverflow()
//    {

//    }

//    /// <summary>
//    /// What to do when the attacker attacks the module
//    /// </summary>
//    public virtual void Attack()
//    {
//        this.Attacked = true;
//        this.attackedIndicatorInstance.SetActive(true);
//        this.gameController.NumAvailableAttacks--;
//    }

//    /// <summary>
//    /// what to do when the module is no longer being attacked
//    /// </summary>
//    public void Fix()
//    {
//        if (this.Attacked)
//        {
//            this.Attacked = false;
//            this.attackedIndicatorInstance.SetActive(false);
//        }
//    }

//    /// <summary>
//    /// Adds back an attack and fixes attacked module.
//    /// </summary>
//    public void ReverseAttack()
//    {
//        if (this.Attacked)
//        {
//            this.gameController.NumAvailableAttacks++;
//            this.Fix();
//        }
//    }

//    /// <summary>
//    /// Defines interaction with mouse
//    /// </summary>
//    private void OnMouseOver()
//    {
        
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (this.gameController && this.gameController.GameState == GameState.AttackerTurn)
//            {
//                if (!this.Attacked && this.gameController.NumAvailableAttacks > 0)
//                {
//                    this.Attack();
//                }
//            }
//        }
//        if (Input.GetMouseButtonDown(1))
//        {
//            if (this.popupInstance.activeSelf)
//            {
//                this.CloseInfoPopup();
//            }
//            else
//            {
//                this.OpenInfoPopup(Input.mousePosition);
//            }
//        }
//    }


//    /// <summary>
//    /// Updates the popup display by getting the values of the fields and changing the popup text to display
//    /// the current values of the fields
//    /// </summary>
//    public void UpdatePopupDisplay() {
//		var bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

//		var fields = new List<FieldInfo>();
//        var props = new List<PropertyInfo>();
//		foreach (string fieldName in displayFields) {
//            var info = this.GetType().GetField(fieldName, bindings);
//            if (info != null)
//            {
//                fields.Add(this.GetType().GetField(fieldName, bindings));
//            }
//            else
//            {
//                props.Add(this.GetType().GetProperty(fieldName, bindings));
//            }
//		}

//		var displayStrings = new List<string>();
//		foreach(FieldInfo field in fields) {
//			displayStrings.Add(field.Name + ": " + field.GetValue(this));
//		}
//        foreach(PropertyInfo prop in props)
//        {
//            displayStrings.Add(prop.Name + ": " + prop.GetValue(this, null));
//        }

//		this.displayTextContent.text = string.Join("\n", displayStrings.ToArray());
//	}

//	/// <summary>
//	/// Opens the info popup at the given location
//	/// </summary>
//	/// <param name="position">The position to place the popup at.</param>
//	protected void OpenInfoPopup(Vector2 position) {
//		this.CloseInfoPopup();
//		this.UpdatePopupDisplay();
//		RectTransform UITransform = this.popupInstance.GetComponent<RectTransform>();
//		UITransform.position = position + new Vector2((UITransform.rect.width / 2), (UITransform.rect.height / 2));
//		this.popupInstance.SetActive(true);
//	}

//    /// <summary>
//    /// Closes the info popup
//    /// </summary>
//	protected void CloseInfoPopup() {
//		this.popupInstance.SetActive(false);
//	}


//}

