using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayController : MonoBehaviour {

    public GameObject InventorySystem;
    public Text WoodCount; 

	// Use this for initialization
	void Start () {
        int woodCount = InventorySystem.GetComponent<InventoryController>().woodCount;
        WoodCount.text = "Wood Count: " + woodCount.ToString(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
