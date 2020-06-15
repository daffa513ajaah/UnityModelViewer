using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton attached to Managers GameObject
/// </summary>
public class VehicleManager : MonoBehaviour
{
    //Variables
    public static VehicleManager instance = null;
    public Camera mainCam;
    //array of all vehicles in scene
    private List<GameObject> garage;
    private GameObject selectedVehicle;
    private GameObject selectedPart;
    private UIManager uiInst;

    //Properties
    public GameObject SelectedVehicle
    {
        get { return selectedVehicle; }
    }

    public GameObject SelectedPart
    {
        get { return selectedPart; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiInst = UIManager.instance;
        garage = new List<GameObject>();
        FillGarage();
        //Give the scene a default vehicle to display info for
        if (garage.Count > 0)
        {
            selectedVehicle = garage[0];
        }
        selectedVehicle.GetComponent<Vehicle>().ResetTransform(true);
        mainCam.GetComponent<CameraMovement>().SwitchFocus(selectedVehicle);
        selectedPart = null;
        uiInst.SwitchVehicle();
    }

    // Update is called once per frame
    void Update()
    {
        //Switch the currently selected vehicle if more than 1 exists
        if (Input.GetKeyDown(KeyCode.Return) && garage.Count > 1)
        {
            SwitchVehicle();
        }
        //De-highlight any selected part to cease its rotation
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SwitchPart(null);
            selectedVehicle.GetComponent<Vehicle>().ResetMaterials(null);
        }
        //Reset rotation and position of currently selected vehicle and its parts
        if (Input.GetKeyDown(KeyCode.R))
        {
            selectedVehicle.GetComponent<Vehicle>().ResetTransform(true);
            SwitchPart(null);
        }
    }

    /// <summary>
    /// Fills list with all vehicles in the scene
    /// </summary>
    void FillGarage()
    {
        GameObject[] tempArr = GameObject.FindGameObjectsWithTag("Vehicle");
        foreach(GameObject vehicle in tempArr)
        {
            vehicle.AddComponent<Vehicle>();
            garage.Add(vehicle);
        }
    }

    /// <summary>
    /// Switches focus to the next vehicle in the garage array
    /// </summary>
    void SwitchVehicle()
    {
        SwitchPart(null);
        int index = garage.IndexOf(selectedVehicle) + 1;
        if (index >= garage.Count)
        {
            index = 0;
        }
        selectedVehicle.GetComponent<Vehicle>().ResetTransform(false);
        selectedVehicle = garage[index];
        selectedVehicle.GetComponent<Vehicle>().ResetTransform(true);
        mainCam.GetComponent<CameraMovement>().SwitchFocus(selectedVehicle);
        uiInst.SwitchVehicle();
    }

    /// <summary>
    /// Switches the currently highlighted part
    /// </summary>
    /// <param name="part">The part to highlight (can be null)</param>
    public void SwitchPart(GameObject part)
    {
        bool reveal = false;
        if (selectedPart != null)
        {
            selectedPart.GetComponent<Part>().SetBaseMat();
        }
        selectedPart = part;
        if (part != null)
        {
            reveal = true;
            part.GetComponent<Part>().SaveRotation();
        }
        uiInst.SwitchLabel(reveal);
    }
}
