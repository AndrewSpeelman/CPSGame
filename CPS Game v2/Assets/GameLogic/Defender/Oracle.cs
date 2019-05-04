using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// The Oracle has two valuations that, each can point at a module.  
/// </summary>
public class Oracle : MonoBehaviour
{
    public bool InputActive = false;
    public string messageText = "Stopped an attack!";

    public Plane MovementPlane;

    public GameObject OraclePopupPrefab;

    private Valuation firstValuation, secondValuation;
    private Fixer fixer;
    private Button modeButton;
    private void Awake()
    {
        var vals = this.GetComponentsInChildren<Valuation>();
        this.firstValuation = vals[0];
        this.secondValuation = vals[1];
        this.fixer = this.GetComponentInChildren<Fixer>();
		
    }

    private void Start()
    {
        this.MovementPlane = new Plane(Vector3.up, this.transform.position);
        this.fixer.gameObject.SetActive(false);
    }
    
    private void OnMouseDrag()
    {
        if (InputActive)
        {
            //Shoot a raycast to the x-z plane that the owl resides to get the location to move to
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;

            if (this.MovementPlane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                Vector3 hitPoint = ray.GetPoint(enter);

                //Move your cube GameObject to the point where you clicked
                this.transform.position = hitPoint;
            }

            //Update the lines that come from the valuations
            this.firstValuation.UpdateLine();
            this.secondValuation.UpdateLine();
			this.fixer.UpdateLine();
        }
    }

    /// <summary>
    /// Applies a rule between the two valuations, if successfull, it will fix the modules between the valuations.
    /// </summary>
    public void ApplyRule()
    {
        if (this.firstValuation.CurrentSelection == null || this.secondValuation.CurrentSelection == null)
        {
            return;
        }

        //used to decide which to fix on
        bool firstVal = false; //false = first  true = second
        
        Module moduleOne = this.firstValuation.CurrentSelection;
        Module moduleTwo = this.secondValuation.CurrentSelection;

        var currVal = firstVal ? secondValuation : firstValuation;

        if (moduleOne.Attacked)
        {
            currVal.RuleIndicator.gameObject.SetActive(true);
			moduleOne.GetComponent<Renderer>().material.color = new Color(1f, .3f, .15f);
        }
        else
        {
            currVal.RuleIndicator.gameObject.SetActive(false);
        }

        currVal = firstVal ? firstValuation : secondValuation;
        if (moduleTwo.Attacked)
        {
            currVal.RuleIndicator.gameObject.SetActive(true);
            moduleTwo.GetComponent<Renderer>().material.color = new Color(1f, .3f, .15f);
        }
        else
        {
            currVal.RuleIndicator.gameObject.SetActive(false);        
        }
    }

    /// <summary>
    /// Fixes a module if rules have caught an error. Only fixes if in span of 3 modules.
    /// </summary>
    public void FixRule()
    {
		if (this.fixer.CurrentSelection == null)
        {
            return;
        }

		Module ToFix = this.fixer.CurrentSelection;
		ToFix.Fix();
    }
	
	public void SwapMode(bool mode)
	{
		if(mode)
		{
			this.firstValuation.ModeChange(false);
			this.secondValuation.ModeChange(false);
			this.fixer.ModeChange(true);
		}
		else
		{
            this.fixer.ModeChange(false);
			this.firstValuation.ModeChange(true);
            this.secondValuation.ModeChange(true);
		}
	}
  }
