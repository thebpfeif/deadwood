using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacementController : MonoBehaviour {

    public GameObject Map;
    //public GameObject House;
    //public GameObject Church;
    //public GameObject Saloon;

    private bool buildingPlacementActive;
    private GameObject buildingType; 

	void Start()
    {
        buildingPlacementActive = false; 
	}
	
	void Update()
    {

        if(Input.GetKeyDown(KeyCode.F))
        {
            Vector3 debugPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Current Mouse Position: " + debugPos.x + ", " + debugPos.y + ", " + debugPos.z);
        }
	
	}

    public void PlaceBuilding()
    {
        // Poll for key event that requests a building placement.
        if (buildingType != null)
        {
            Vector3 requestedPosition = Input.mousePosition;
            Vector3 newPosition;

            //Round the mouse position to the lower left corner of square we're currently in
            Ray ray = Camera.main.ScreenPointToRay(requestedPosition);
            RaycastHit output;

            //Get the output of the raycast using physics model 
            if (Physics.Raycast(ray, out output, 1000f))
            {
                newPosition = getCellPosition(output.point);
            }
            else
            {
                newPosition = getCellPosition(Camera.main.ScreenToWorldPoint(requestedPosition));
            }
            Debug.Log("Place a building at: " + newPosition.x + ", " + newPosition.y + ", " + newPosition.z);

            //Offset the building so it sets inside the cell space
            Vector3 scale = buildingType.GetComponent<Transform>().localScale;
            Vector3 offset = new Vector3(scale.x / 2, scale.y / 2, scale.z / 2);
            //newPosition += offset; 
            
            GameObject newBuilding = (GameObject)Instantiate(buildingType);
            newBuilding.transform.parent = Map.transform;
            newBuilding.transform.position = newPosition;
        }
    }

    public void ToggleBuildingPlacementActive(GameObject building)
    {

        // If the same building type is being requested, toggle building mode off. 
        if( buildingType == building)
        {
            buildingType = null; 
        }
        else
        {
            buildingType = building;
        }

    }

    //BRP TODO: Move this to a utility file 
    private Vector3 getCellPosition(Vector3 mousePosition)
    {
        // Get the position of the nearest cell in 2D space. 
        float x_rounded = (float)(Mathf.FloorToInt(mousePosition.x / 10) * 10);
        float z_rounded = (float)(Mathf.FloorToInt(mousePosition.z / 10) * 10);

        return new Vector3(x_rounded, 0.0f, z_rounded);
    }
}
