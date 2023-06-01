using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private Cockpit cockpit;

    private bool thrustingForward = false;
    private bool thrustingBackward = false;
    private bool rotatingLeft = false;
    private bool rotatingRight = false;

    private void Awake()
    {
        cockpit = FindObjectOfType<Cockpit>();
        if (cockpit == null)
        {
            throw new MissingReferenceException("Cockpit not found in the scene");
        }
    }

    private void Update()
    {    
        if (thrustingForward)
        {
            cockpit.ThrustForward();
        }
        else if (thrustingBackward)
        {
            cockpit.ThrustBackward();
        }
        else
        {
            cockpit.ResetVerticalThruster();
        }

        if (rotatingLeft)
        {
            cockpit.RotateLeft();
        }
        else if (rotatingRight)
        {
            cockpit.RotateRight();
        }
        else
        {
            cockpit.StopRotation();
        }
    }

    public void OnTopLeftPressed()
    {
        thrustingForward = true;
    }

    public void OnTopLeftUnpressed()
    {
        thrustingForward = false;
    }

    public void OnBottomLeftPressed()
    {
        thrustingBackward = true;
    }

    public void OnBottomLeftUnpressed()
    {
        thrustingBackward = false;
    }

    public void OnRightLeftPressed()
    {
        rotatingLeft = true;
    }

    public void OnRightLeftUnpressed()
    {
        rotatingLeft = false;
    }

    public void OnRightRightPressed()
    {
        rotatingRight = true;
    }
    public void OnRightRightUnpressed()
    {
        rotatingRight = false;
    }
}
