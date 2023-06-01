using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class FreezeCommand : MonoBehaviour, ConsoleCommand
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

    void Start()
    {
        cockpit = FindObjectOfType<Cockpit>();
        shortcut.performed += OnShortCutPressed;
    }

    private void OnShortCutPressed(CallbackContext context)
    {
        //cockpit.TriggerFreeze();
    }

    public string GetCommandName()
    {
        return CommandName;
    }

    public bool Execute(params string[] arguments)
    {
        bool freezeExternalForces = false;
        if (!bool.TryParse(arguments[1], out freezeExternalForces))
        {
            return false;
        }
        //cockpit.TriggerFreeze();
        return true;
    }

    public string GetHelp()
    {
        return "Freeze [true,false]: Only in god mode. Freeze the ship. If the first argument is true, external forces will be freezed as well (i.e gravity forces)";
    }
}
