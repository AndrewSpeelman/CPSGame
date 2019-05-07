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

    private Vector3 screenPoint, offset;
    private Vector2 minScreen = new Vector2(0, 0);
    private Vector2 maxScreen = new Vector2(Screen.width, Screen.height);
    private Inspector firstInspector, secondInspector;
    private Fixer fixer;
    private Button modeButton;

    private void Awake()
    {
        var vals = this.GetComponentsInChildren<Inspector>();
        this.firstInspector = vals[0];
        this.secondInspector = vals[1];
        this.fixer = this.GetComponentInChildren<Fixer>();
		
    }

    private void Start()
    {
        this.MovementPlane = new Plane(Vector3.up, this.transform.position);
        this.fixer.gameObject.SetActive(false);
    }
    
    private void OnMouseDown()
    {
        if (InputActive)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseDrag()
    {
        if (InputActive)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
 
            Vector2 minPosition = Camera.main.ScreenToWorldPoint(minScreen);
            Vector2 maxPosition = Camera.main.ScreenToWorldPoint(maxScreen);
 
            //owl screen bounds
            if ( (cursorPosition.x) < minPosition.x)
            {
                cursorPosition.x = minPosition.x;
            }else if(cursorPosition.x > maxPosition.x)
            {
                cursorPosition.x = maxPosition.x;
            }
 
            if (cursorPosition.y < minPosition.y)
            {
                cursorPosition.y = minPosition.y;
            }else if(cursorPosition.y > maxPosition.y)
            {
                cursorPosition.y = maxPosition.y;
            }
 
            transform.position = cursorPosition;
            this.firstInspector.UpdateLine();
            this.secondInspector.UpdateLine();
            this.fixer.UpdateLine();
        }
    }
        // if (InputActive)
        // {
        //     //Shoot a raycast to the x-z plane that the owl resides to get the location to move to
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     float enter = 0.0f;

        //     if (this.MovementPlane.Raycast(ray, out enter))
        //     {
        //         //Get the point that is clicked
        //         Vector3 hitPoint = ray.GetPoint(enter);

        //         //Move your cube GameObject to the point where you clicked
        //         this.transform.position = hitPoint;
        //     }

        //     //Update the lines that come from the valuations
        //     this.firstInspector.UpdateLine();
        //     this.secondInspector.UpdateLine();
		// 	this.fixer.UpdateLine();
        // }
    /// <summary>
    /// Will let defender display the real information
    /// </summary>
    public void InspectModule()
    {
        if (this.firstInspector.CurrentSelection == null || this.secondInspector.CurrentSelection == null)
        {
            return;
        }
        this.firstInspector.CurrentSelection.GetComponent<Renderer>().material.color = new Color(0, 1F, 1F);
        this.secondInspector.CurrentSelection.GetComponent<Renderer>().material.color = new Color(0, 1F, 1F);
        this.firstInspector.CurrentSelection.HasInspectorAttached = true;
        this.secondInspector.CurrentSelection.HasInspectorAttached = true;
        return;
    }

    /// <summary>
    /// Fixes a module if fixer attached
    /// </summary>
    public void FixModule()
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
			this.firstInspector.ModeChange(false);
			this.secondInspector.ModeChange(false);
			this.fixer.ModeChange(true);
		}
		else
		{
            this.fixer.ModeChange(false);
			this.firstInspector.ModeChange(true);
            this.secondInspector.ModeChange(true);
		}
	}
  }
