using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class KeyboardControls : MonoBehaviour
{
    private Cockpit cockpit;
    private Player player;

    [SerializeField] private InputActionAsset inputConfig;
    [SerializeField] private bool backwards;
    [SerializeField] private bool forward;
    private void Awake()
    {
        //cockpit = FindObjectOfType<Cockpit>();
        player = FindObjectOfType<Player>();
    }

    private void OnThrustBackward(CallbackContext context)
    {
        cockpit.ThrustBackward();
    }


    // Update is called once per frame
    private void Update()
    {
        if (cockpit == null)
            cockpit = FindObjectOfType<Cockpit>() ;

        /*bool rotating = false;
        foreach(var touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2.0f)
            {
                ProcessVerticalThruster(touch);
            }
            else
            {
                ProcessGyroscope(touch);
            }
        }*/

        if (Input.GetKey(KeyCode.A))
        {
            cockpit.RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cockpit.RotateRight();
        }
        else
        {
            cockpit.StopRotation();
        }
     
        if (Input.GetKey(KeyCode.W) || forward)
        {
            cockpit.ThrustForward();
        }
        else if (Input.GetKey(KeyCode.S) || backwards)
        {
            cockpit.ThrustBackward();
        }
        else
        {
            cockpit.ResetVerticalThruster();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            cockpit.EnableQuantumEngine();
        }
        else
        {
            cockpit.DisableQuantumEngine();
        }


        /*if (rotating)
        {
            cockpit.ResetHorizontalThruster();
            cockpit.ResetVerticalThruster();
            return;
        }
        if (Input.GetMouseButton(0))
        {
            // Get screen position
            var mousePixelCoordinates = Input.mousePosition;
            mousePixelCoordinates.z = Camera.main.nearClipPlane;
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixelCoordinates);

            // Get direction from position to ship
            var directionFromShipToMouse = mouseWorldPosition - cockpit.transform.position;

            // Compute angle between forward and direction
            var angle = Vector3.Angle(cockpit.transform.right, directionFromShipToMouse);

            // if angle is negative or greater than 90 (don't know
            if (angle > 90)
            {
                cockpit.RotateLeft();
            }
            if (angle < 90)
            {
                cockpit.RotateRight();
            }
        }*/











        /*
        if (Input.GetKey(KeyCode.W))
        {
            cockpit.ThrustForward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cockpit.ThrustBackward();
        }
        else
        {
            cockpit.ResetVerticalThruster();
        }*/
    }

    private void OnDrawGizmos()
    {
        /*cockpit = FindObjectOfType<Cockpit>();
        player = FindObjectOfType<Player>();
        var mousePixelCoordinates = Input.mousePosition;
        mousePixelCoordinates.z = Camera.main.nearClipPlane;
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixelCoordinates);

        // Get direction from position to ship
        var directionFromShipToMouse = mouseWorldPosition - cockpit.transform.position;

        // Compute angle between forward and direction
        var angle = Vector3.Angle(cockpit.transform.up, directionFromShipToMouse);

        Gizmos.DrawLine(cockpit.transform.up * 10.0f, directionFromShipToMouse.normalized * 10.0f);
        Gizmos.DrawCube(mouseWorldPosition, new Vector3(4, 4, 4));*/
    }
}
