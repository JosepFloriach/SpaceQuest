using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ResetShipVelocityCommand : MonoBehaviour, IConsoleCommand
{    
    [SerializeField] private InputAction shortcut;

    private Cockpit cockpit;

    private void OnEnable()
    {
        shortcut.Enable();
        shortcut.performed += OnShortCutPressed;
    }

    private void OnDisable()
    {
        shortcut.Disable();
        shortcut.performed -= OnShortCutPressed;
    }

    private void OnShortCutPressed(CallbackContext context)
    {
        ExecuteClearAllForces();
    }

    public string GetCommandName()
    {
        return "ResetShip";
    }

    public bool Execute(params string[] arguments)
    {
        return ExecuteClearAllForces();
    }

    public string GetHelp()
    {
        return "ResetShip: resets to zero the velocity of the ship, making it to stop.";
    }

    private bool ExecuteClearAllForces()
    {
        cockpit = FindObjectOfType<Cockpit>();
        if (cockpit == null)
        {
            return false;
        }
        cockpit.PhysicsBody.ClearAllForces();
        return true;
    }
}
