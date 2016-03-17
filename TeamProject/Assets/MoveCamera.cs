using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    //
    // VARIABLES
    //

	void Start () {
	
	}
    public int x=0;
	public int y=30;
    public int z = -1;

    public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;		// Speed of the camera when being panned
    public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth

    public float cameraDistanceMax = 20f;
    public float cameraDistanceMin = 5f;
    public float cameraDistance = 10f;
    public float scrollSpeed = 0.5f;

    private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
    private bool isPanning;		// Is the camera being panned?
    private bool isRotating;	// Is the camera being rotated?
    private bool isZooming;		// Is the camera zooming?

    //
    // UPDATE
    //

    void Update()
    {
        Vector3 tmp = (Camera.main.ScreenToViewportPoint(Input.mousePosition));
	    
        if(tmp.x>0.95)
            transform.Translate(new Vector3(1, 0, 0), Space.Self);
	    else
	    if(tmp.x<0.05)
            transform.Translate(new Vector3(-1, 0, 0), Space.Self);
	    if(tmp.y>0.95)
            transform.Translate(new Vector3(0, 1, 0), Space.Self);
	    else
	    if(tmp.y<0.05)
            transform.Translate(new Vector3(0, -1, 0), Space.Self);
        
        // Get the left mouse button + LeftCTRL
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl)) 
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        // Get the right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        // Get the middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(0)) isRotating = false;
        if (!Input.GetMouseButton(1)) isPanning = false;
        if (!Input.GetMouseButton(2)) isZooming = false;

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

        // Move the camera on it's XY plane
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
        }

        // Move the camera linearly along Z axis (scrollwhell)
        //cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        //cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
        //transform.Translate(0, 0, cameraDistance);

        //middlebutton
        if (isZooming)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }
    }
}
