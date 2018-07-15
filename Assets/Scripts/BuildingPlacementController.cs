using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementController : MonoBehaviour {

    public GameObject Map;
    public GameObject Building;

	void Start () {
		
	}
	
	void Update () {

        if(Input.GetKeyDown(KeyCode.F))
        {
            Vector3 debugPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Current Mouse Position: " + debugPos.x + ", " + debugPos.y + ", " + debugPos.z);
        }
		// Poll for key event that requests a building placement.
        if( Input.GetMouseButtonDown(0))
        {
            Vector3 requestedPosition = Input.mousePosition;
            Vector3 newPosition;

            //Round the mouse position to the lower left corner of square we're currently in
            Ray ray = Camera.main.ScreenPointToRay(requestedPosition);
            RaycastHit output; 


            //Get the output of the raycast using physics model 
            if(Physics.Raycast(ray, out output, 1000f))
            {
                newPosition = getCellPosition(output.point);
            }
            else
            {
                newPosition = getCellPosition(Camera.main.ScreenToWorldPoint(requestedPosition));
            }
            Debug.Log("Place a building at: " + newPosition.x + ", " + newPosition.y + ", " + newPosition.z);

            //Offset the building so it sets inside the cell space
            newPosition.x += 5;
            newPosition.y += 5;
            newPosition.z += 5; 

            GameObject newBuilding = (GameObject)Instantiate(Building);
            newBuilding.transform.parent = Map.transform;
            newBuilding.transform.position = newPosition;
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
