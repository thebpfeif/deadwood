using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRenderer : MonoBehaviour {

    public GameObject Map;
    public GameObject GridSquare;

    private GameObject gameGrid;

    // Use this for initialization
    void Start () {
        gameGrid = new GameObject("Grid");
        createCanvas();
        generateGrid(); 
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void createCanvas()
    {
        gameGrid.AddComponent<Canvas>();
        RectTransform transform = gameGrid.GetComponent<RectTransform>();

        gameGrid.transform.parent = Map.transform;
        gameGrid.transform.position = Map.transform.position;
        transform.sizeDelta = new Vector2(500, 500);
        transform.anchorMin = new Vector2(0.5f, 0);
        transform.anchorMax = new Vector2(0.5f, 0);
        transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        transform.position = new Vector3(250, 0, 250);

        Debug.Log("Attached to parent object" + Map.name);
    }

    private void generateGrid()
    {
        //Get canvas info 
        Canvas canvas = gameGrid.GetComponent<Canvas>();
        RectTransform transform = gameGrid.GetComponent<RectTransform>();

        Vector2 size = transform.sizeDelta;

        for(int x = 0; x < size.x; x+=10)
        {
            for(int z = 0; z < size.y; z+=10)
            {
                GameObject newCell = (GameObject)Instantiate(GridSquare);
                newCell.transform.parent = Map.transform; 
                Transform cellPos = newCell.GetComponent<Transform>();
                cellPos.transform.position = new Vector3(x, 0, z);
            }
        }

    }

}
