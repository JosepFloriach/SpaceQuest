using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ResetShipVelocityCommand : MonoBehaviour, ConsoleCommand
{
    [SerializeField] private string CommandName;
    [SerializeField] private InputAction shortcut;

    private Cockpit cockpit;

    private void OnEnable()
    {
        shortcut.Enable();
    }

    private void OnDisable()
    {
        shortcut.Disable();
    }

    private void Start()
    {
        cockpit = FindObjectOfType<Cockpit>();
        shortcut.performed += OnShortCutPressed;
    }

    private void OnShortCutPressed(CallbackContext context)
    {
        cockpit.PhysicsBody.ClearAllForces();
    }

    public string GetCommandName()
    {
        return CommandName;
    }

    public bool Execute(params string[] arguments)
    {
        cockpit.PhysicsBody.ClearAllForces();        
        return true;
    }

    public string GetHelp()
    {
        return "ResetShip: resets to zero the velocity of the ship, making it to stop.";
    }
}
