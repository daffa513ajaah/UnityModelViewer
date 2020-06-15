using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    private GameObject[] textList;
    // Start is called before the first frame update
    void Start()
    {
        textList = GameObject.FindGameObjectsWithTag("Instructions");
        this.gameObject.GetComponent<Button>().onClick.AddListener(ToggleHide);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleHide()
    {
        foreach(GameObject text in textList)
        {
            text.SetActive(!text.activeInHierarchy);
        }
    }
}
