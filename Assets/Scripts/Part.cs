using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to all children of Vehicles
/// </summary>
public class Part : MonoBehaviour
{
    //Variables
    private Material baseMat;
    private Material highlightMat;
    private GameObject parentVehicle;
    private GameObject assignedButton;
    private Vector3 baseRotation;
    //Rotation saved after being selected
    private Vector3 savedRotation;
    private VehicleManager vInst;

    //Properties
    public GameObject ParentVehicle
    {
        get { return parentVehicle; }
        set { parentVehicle = value; }
    }

    void Awake()
    {
        vInst = VehicleManager.instance;
        baseMat = this.gameObject.GetComponent<MeshRenderer>().material;
        highlightMat = Resources.Load("Materials/highlight") as Material;
        baseRotation = this.gameObject.transform.eulerAngles;
        savedRotation = this.gameObject.transform.eulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Only rotate if this part is selected
        if (vInst.SelectedPart == this.gameObject)
        {
            UpdateRotation();
        }
    }

    /// <summary>
    /// Highlight this part if not selected and if its part of the selected vehicle
    /// </summary>
    void OnMouseOver()
    {
        if (vInst.SelectedPart != this.gameObject && vInst.SelectedVehicle == parentVehicle)
        {
            SetHighlightMat();
        }
    }

    /// <summary>
    /// Return this part to its original material if not selected
    /// </summary>
    void OnMouseExit()
    {
        if (vInst.SelectedPart != this.gameObject && vInst.SelectedVehicle == parentVehicle)
        {
            SetBaseMat();
        }
    }

    /// <summary>
    /// Save a button to this part
    /// </summary>
    /// <param name="obj">The button to be assigned</param>
    public void AssignButton(GameObject obj)
    {
        assignedButton = obj;
    }

    /// <summary>
    /// Have this part's vehicle reset all materials except for this part (highlighted)
    /// </summary>
    void OnMouseDown()
    {
        if (parentVehicle == vInst.SelectedVehicle)
        {
            if (vInst.SelectedPart == this.gameObject)
            {
                vInst.SwitchPart(null);
                vInst.SelectedVehicle.GetComponent<Vehicle>().ResetMaterials(null);
            }
            else
            {
                vInst.SwitchPart(this.gameObject);
                vInst.SelectedVehicle.GetComponent<Vehicle>().ResetMaterials(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Set this part to it's starting material
    /// </summary>
    public void SetBaseMat()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = baseMat;
    }

    /// <summary>
    /// Set this part to it's highlighted material
    /// </summary>
    public void SetHighlightMat()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = highlightMat;
    }

    /// <summary>
    /// Rotate this part with WASD; E resets to base rotation
    /// </summary>
    void UpdateRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.eulerAngles += new Vector3(0, 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.eulerAngles += new Vector3(0, -0.5f, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.eulerAngles += new Vector3(0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.eulerAngles += new Vector3(-0.5f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.transform.eulerAngles = savedRotation;
        }
    }

    /// <summary>
    /// Set this part's rotation to its starting value
    /// </summary>
    public void ResetRotation()
    {
        this.gameObject.transform.eulerAngles = baseRotation;
    }

    /// <summary>
    /// Save this part's current rotation
    /// </summary>
    public void SaveRotation()
    {
        savedRotation = this.gameObject.transform.eulerAngles;
    }
}
