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
using Assets.Modules;
using Assets.Modules.Scripts;

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
    private ExpectedValuesMenuController expectedValuesMenuController;

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


    public ExpectedValuesClass ExpectedValues { get; set; }

    /**
     * Unity Things 
     */
    protected new Renderer renderer;
    protected Color startingColor;
    protected GameControllerWrapper gameController;


    public Module()
    {
        this.Water = new WaterObject();
        this.ExpectedValues = new ExpectedValuesClass();
    }


    /// <summary>
    /// Don't override this one
    /// </summary>
    public void Start()
    {
        this.gameController = new GameControllerWrapper();
        this.renderer = GetComponent<Renderer>();
        this.startingColor = renderer.material.color;

        // Set default expected values
        this.ExpectedValues.ExpectedPurity1 = this.ExpectedPurity1;
        this.ExpectedValues.ExpectedPurity2 = this.ExpectedPurity2;
        this.ExpectedValues.ExpectedPurity3 = this.ExpectedPurity3;

        this.OnStart();
    }

    /// <summary>
    /// Override this method if you need it
    /// </summary>
    public virtual void OnStart()
    {
        // Nothing here, this is for child classes to override
    }

    /// <summary>
    /// Don't override this
    /// </summary>
    private void Awake()
    {
        this.infoMenuController = new InfoMenuController(this, this.InfoPopupPrefab);
        this.expectedValuesMenuController = new ExpectedValuesMenuController(this, this.InfoPopupPrefab);
        this.OnAwake();
    }

    /// <summary>
    /// Override this if you need it
    /// </summary>
    public virtual void OnAwake()
    {
        // Nothing here, this is for child classes to override
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
        // Nothing here, this is for child classes to override
    }

    /// <summary>
    /// Override this specifically for updating popup displays
    /// </summary>
    public virtual void UpdatePopups()
    {
        // Nothing here, this is for child classes to override
    }



    /*
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

    /*
     * Returns the water in the module 
     */
    public virtual WaterObject getWater()
    {
        return this.Water;
    }


    /*
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
        return builder.SetTitle(this.GetType().Name.ToString() + " Info").Build();
    }

    /// <summary>
    /// Gets what to display for the expected values popup
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public MenuToDisplay GetExpectedValuesPopup(MenuBuilder builder)
    {
        builder.SetTitle(this.GetType().Name.ToString() + " Expected");

        builder.AddBoolItem(Strings.HasFlow, this.ExpectedValues.HasFlow);
        builder.AddBoolItem(Strings.IsPurityAsExpected, this.ExpectedValues.IsPurityAsExpected);

        if (this.GetType() == typeof(Pump))
            builder.AddBoolItem(Strings.IsPumping, this.ExpectedValues.IsPumping);

        if (this.GetType() == typeof(Reservoir))
        {
            builder.AddBoolItem(Strings.IsFull, this.ExpectedValues.IsFull);
            builder.AddBoolItem(Strings.IsEmpty, this.ExpectedValues.IsEmpty);
        }
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
                if(this.renderer.material.color == this.startingColor)
                    this.renderer.material.color = Color.yellow; // Turn color yellow while user is clicking on the object if it's not inspected
            }
            catch (Exception e)
            {

            }
            if (this.gameController.IsDefendersTurn())
            {
                if(this.HasInspectorAttached)
                {
                    this.infoMenuController.OpenMenu();
                }
                else
                {
                    this.infoMenuController.CloseMenu();
                }
                this.expectedValuesMenuController.OpenMenu();
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
        if(this.renderer.material.color == Color.yellow || this.renderer.material.color == Color.red)
            this.ResetColor();

    }

    public void ResetColor()
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