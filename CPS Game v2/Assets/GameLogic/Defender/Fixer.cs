using System.Collections;
using System.Collections.Generic;
using Assets.Modules;
using UnityEngine;
using UnityEngine.UI;
using Assets.Modules.Menu;

[RequireComponent(typeof(LineRenderer))]
public class Fixer : MonoBehaviour
{
    /// <summary>
    /// The Module the valuation is currently pointed at
    /// </summary>
    public AttackableModule CurrentSelection {
        get {
            if (this.module) return this.module;
            return null;
        }
    }

    public Dropdown[] dropdowns;

    private GameObject popupInstance;

    private AttackableModule module;

    private LineRenderer lineRenderer;

    private Oracle parentOracle;

    public Text RuleIndicator;

    public void UpdateLine()
    {
        if (this.lineRenderer.enabled && this.module)
        {
            this.DrawLine(this.transform.position, this.module.transform.position);
        }
    }

    private void Awake()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.lineRenderer.startWidth = 0.1f;
        this.lineRenderer.endWidth = 0.1f;
        this.parentOracle = this.GetComponentInParent<Oracle>();
        this.popupInstance = Instantiate(this.parentOracle.OraclePopupPrefab);
        this.popupInstance.transform.SetParent(((Canvas)FindObjectOfType(typeof(Canvas))).transform);
        this.popupInstance.SetActive(false);
        this.dropdowns = this.popupInstance.GetComponentsInChildren<Dropdown>();
        var texts = this.popupInstance.GetComponentsInChildren<Text>();
        foreach (Text t in texts)
        {
            if (t.text == "RULE BROKEN")
            {
                this.RuleIndicator = t;
                this.RuleIndicator.gameObject.SetActive(false);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!this.parentOracle.InputActive)
            return;

        this.Deselect();
    }

    private void OnMouseUp()
    {
        if (!this.parentOracle.InputActive)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            AttackableModule mod = hit.transform.GetComponent<AttackableModule>();
            if (mod != null)
            {
                this.Select(mod);
            }
            else
            {
                this.Deselect();
            }
        }
        else
        {
            this.Deselect();
        }
    }

    private void OnMouseDrag()
    {
        if (!this.parentOracle.InputActive)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;

        if (this.parentOracle.MovementPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            this.DrawLine(this.transform.position, hitPoint);
        }
    }

    private void Update()
    {
        if (!this.parentOracle.InputActive)
        {
            // this.module.CloseFixMenu();
        }
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        this.lineRenderer.enabled = true;
        this.lineRenderer.SetPositions(new List<Vector3>()
        {
            new Vector3(start.x, start.y, start.z),
            new Vector3(end.x, end.y, end.z)
        }.ToArray());
    }

    private void Select(AttackableModule mod)
    {
        this.module = mod;
        this.module.OpenFixMenu();
        this.module.HasFixerAttached = true;
        this.DrawLine(this.transform.position, mod.transform.position);
    }

    private void Deselect()
    {
        if(this.module)
        {
            this.module.CloseFixMenu();
            this.module.HasFixerAttached = false;
            this.module = null;
        }
        this.lineRenderer.enabled = false;
    }

    public void ModeChange(bool isActive)
    {
        if(isActive)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.Deselect();
            this.gameObject.SetActive(false);
        }
    }
}
