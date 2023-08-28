using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class KeyboardControls : MonoBehaviour
{
    private Cockpit cockpit;

    public event Action PressedScape;
    
    [SerializeField] private InputActionAsset inputConfig;

    private bool enable = false;

    public bool Enable
    {
        get { return enable; }
        set { enable = value; }
    }

    private void Update()
    {
        if (cockpit == null)
        {
            cockpit = FindObjectOfType<Cockpit>();
        }

        if (!Enable || cockpit == null)
        {
            return;
        }   

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedScape?.Invoke();
        }
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
     
        if (Input.GetMouseButton(0) && !cockpit.ThrustingBackward)
        {
            cockpit.ThrustForward();
        }
        else if (Input.GetMouseButton(1) && !cockpit.ThrustingForward)
        {
            cockpit.ThrustBackward();
        }
        else
        {
            cockpit.ResetVerticalThruster();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cockpit.EnableWarpEngine();
        }
        else if(!Input.GetKey(KeyCode.Space))
        {
            cockpit.ResetWarpEngine();
        }
    }
}
