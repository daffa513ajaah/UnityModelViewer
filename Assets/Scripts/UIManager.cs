using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton attached to Managers GameObject
/// </summary>
public class UIManager : MonoBehaviour
{
    //Variables
    public static UIManager instance;
    public GameObject buttonPrefab;
    //Displays selected vehicle's name
    private Text header;
    private List<GameObject> buttonList;
    public GameObject partLabel;
    private VehicleManager vInst;

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
        vInst = VehicleManager.instance;
        header = GameObject.Find("PartListHead").GetComponent<Text>();
        buttonList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Changes header name text and adjusts buttons for the scene
    /// </summary>
    public void SwitchVehicle()
    {
        int counter = 0;
        foreach (GameObject part in buttonList)
        {
            part.SetActive(false);
        }
        buttonList.Clear();
        header.text = vInst.SelectedVehicle.name;
        Transform[] tempArr = vInst.SelectedVehicle.GetComponentsInChildren<Transform>();
        foreach (Transform trans in tempArr)
        {
            //Instantiate a new button prefab and attach it to the canvas panel, hooking up part to button and vice-versa
            if (trans.gameObject.GetComponent<Vehicle>() == null)
            {
                buttonList.Add(Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity));
                buttonList[counter].GetComponentInChildren<Text>().text = trans.gameObject.name;
                buttonList[counter].transform.SetParent(GameObject.Find("Options").transform);
                trans.gameObject.GetComponent<Part>().AssignButton(buttonList[counter]);
                buttonList[counter].GetComponent<PartButton>().AssignPart(trans.gameObject);
                counter++;
            }
        }
    }

    /// <summary>
    /// Reveals or hides the part label and changes it's text based on the currently selected part
    /// </summary>
    /// <param name="reveal">True if the label should be seen, false otherwise</param>
    public void SwitchLabel(bool reveal)
    {
        partLabel.SetActive(reveal);
        if (vInst.SelectedPart != null)
        {
            partLabel.GetComponentInChildren<Text>().text = vInst.SelectedPart.name;
        }
    }
}
