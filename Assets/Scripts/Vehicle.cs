using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to all GameObjects tagged "Vehicle"
/// </summary>
public class Vehicle : MonoBehaviour
{
    //Variables
    private GameObject[] partList;
    private int numParts;
    private Vector3 baseRotation;
    private Vector3 basePosition;
    private VehicleManager vInst;

    void Awake()
    {
        vInst = VehicleManager.instance;
        numParts = this.gameObject.GetComponent<Transform>().childCount;
        partList = new GameObject[numParts];
        baseRotation = this.gameObject.transform.eulerAngles;
        basePosition = this.gameObject.transform.position;
        FillParts();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Only rotate this object if it's the selected vehicle and a part is not selected
        if (vInst.SelectedVehicle == this.gameObject && vInst.SelectedPart == null)
        {
            UpdateRotation();
        }
    }

    /// <summary>
    /// Add parts script and MeshColliders to all this vehicles gameobjects
    /// </summary>
    void FillParts()
    {
        int counter = 0;
        Transform[] tempArr = this.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform trans in tempArr)
        {
            if (trans.gameObject.GetComponent<Vehicle>() == null)
            {
                trans.gameObject.AddComponent<Part>();
                trans.gameObject.AddComponent<MeshCollider>();
                trans.gameObject.GetComponent<Part>().ParentVehicle = this.gameObject;
                partList[counter] = trans.gameObject;
                counter++;
            }
        }
    }

    /// <summary>
    /// Rotate this vehicle with WASD; E resets to base rotation
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
            this.gameObject.transform.eulerAngles = baseRotation;
        }
    }

    /// <summary>
    /// Reset this vehicle and all of it's parts transformations 
    /// </summary>
    /// <param name="raise">True if vehicle should be raised, false otherwise</param>
    public void ResetTransform(bool raise)
    {
        this.gameObject.transform.eulerAngles = baseRotation;
        this.gameObject.transform.position = basePosition;
        foreach (GameObject part in partList)
        {
            part.GetComponent<Part>().ResetRotation();
        }
        ResetMaterials(null);
        SwitchFocus(raise);
    }

    /// <summary>
    /// Raise this vehicle's height if newly selected, lower it otherwise
    /// </summary>
    /// <param name="raise">True if vehicle should be raised, false otherwise</param>
    void SwitchFocus(bool raise)
    {
        float delta = 1.0f;
        if (raise)
        {
            this.gameObject.transform.position += new Vector3(0, delta, 0);
        }
        else
        {
            this.gameObject.transform.position = basePosition;
        }
    }

    /// <summary>
    /// Set all parts on this vehicle to their base material besides the selected part
    /// </summary>
    /// <param name="selectedPart">The part to remain highlighted (can be null)</param>
    public void ResetMaterials(GameObject selectedPart)
    {
        foreach(GameObject part in partList)
        {
            if (part != selectedPart)
            {
                part.GetComponent<Part>().SetBaseMat();
            }
        }
    }
}
