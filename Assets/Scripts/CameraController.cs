using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float MovementSensitivity;
    public float RotationalSensitivity;
    public float ScrollSensitivity; 

    // Camera angles
    private float heading;
    private float pitch; 

	void Start ()
    {
        heading = Camera.main.transform.rotation.eulerAngles.y;
        pitch = Camera.main.transform.rotation.eulerAngles.x; 
	}
	
	void Update ()
    {

        //Pivot the camera based on horizontal click-drag input 
        //if( Input.GetMouseButton(0) )
        //{
        //    heading += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        //    Debug.Log("Mouse X input value: " + Input.GetAxis("Mouse X").ToString());
        //    Debug.Log("Delta Time" + Time.deltaTime.ToString());
        //    //transform.rotation = Quaternion.Euler(0, heading, 0);
        //    //transform.Rotate(new Vector3(0, heading));
        //    transform.RotateAround(Vector3.zero, new Vector3(0, heading), angle * Time.deltaTime);
        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
        //}
        updateCameraMovement();
        updateCameraRotation();
        updateCameraZoom(); 
    }

    private void updateCameraMovement()
    {
        // Get the current player camera inputs from WASD or arrow keys
        Vector2 userInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        userInput = Vector2.ClampMagnitude(userInput, 1);

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        // Clear out the y inputs, we don't want to modify those 
        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

        transform.position += ((camForward * userInput.y) + (camRight * userInput.x)) * MovementSensitivity * Time.deltaTime;
    }

    private void updateCameraRotation()
    {
        // Rotate Clockwise 
        if(Input.GetKey(KeyCode.Q))
        {
            heading -= RotationalSensitivity * Time.deltaTime; 
            Camera.main.transform.rotation = Quaternion.Euler(pitch, heading, 0);
        }
        // Rotate Counterclockwise
        else if(Input.GetKey(KeyCode.E))
        {
            heading += RotationalSensitivity * Time.deltaTime;
            Camera.main.transform.rotation = Quaternion.Euler(pitch, heading, 0);
        }
    }

    private void updateCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 camForward = Camera.main.transform.forward;

        // Zoom in 
        if ( scroll > 0.0f )
        {
        camForward = camForward.normalized;
        transform.position += camForward * ScrollSensitivity * Time.deltaTime;
        }
        // Zoom out
        else if ( scroll < 0.0f )
        {
        camForward = camForward.normalized;
        transform.position -= camForward * ScrollSensitivity * Time.deltaTime;
        }
        
    }
}
