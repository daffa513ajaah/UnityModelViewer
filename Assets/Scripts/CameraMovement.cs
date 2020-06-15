using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to main camera
/// </summary>
public class CameraMovement : MonoBehaviour
{
    //Variables
    private GameObject focusedObject;
    private float zoomSpeed;

    void Awake()
    {
        zoomSpeed = 2.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Mouse wheel zooms in and out of target
        this.gameObject.transform.position += this.gameObject.transform.forward * zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
    }

    /// <summary>
    /// Camera focuses on selected vehicle by changing position
    /// </summary>
    /// <param name="obj">The vehicle to focus on</param>
    public void SwitchFocus(GameObject obj)
    {
        focusedObject = obj;
        Vector3 pos = focusedObject.transform.position;
        this.gameObject.transform.position = new Vector3(pos.x + 3, pos.y + 3, pos.z + 5);
    }
}
