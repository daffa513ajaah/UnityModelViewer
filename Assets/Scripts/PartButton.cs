using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attached to all Buttons on the left-hand panel
/// </summary>
public class PartButton : MonoBehaviour
{
    //Variables
    private GameObject assignedPart;
    private VehicleManager vInst;

    void Awake()
    {
        vInst = VehicleManager.instance;
        this.gameObject.GetComponent<Button>().onClick.AddListener(FocusPart);
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
    /// Save this button's assigned part
    /// </summary>
    /// <param name="obj">The part to be assigned</param>
    public void AssignPart(GameObject obj)
    {
        assignedPart = obj;
    }

    /// <summary>
    /// On click, will switch the selected part to this button's assigned part
    /// </summary>
    void FocusPart()
    {
        vInst.SwitchPart(assignedPart);
        assignedPart.GetComponent<Part>().SetHighlightMat();
    }
}
