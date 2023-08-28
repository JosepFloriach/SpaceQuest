using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class FreezeCommand : MonoBehaviour, IConsoleCommand
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

    private void OnShortCutPressed(CallbackContext callback)
    {
        Freeze();
    }

    public string GetCommandName()
    {
        return "Freeze";
    }

    public bool Execute(params string[] arguments)
    {
        return Freeze();
    }

    private bool Freeze()
    {
        cockpit = FindObjectOfType<Cockpit>();
        if (cockpit == null)
        {
            return false;
        }
        cockpit.Freeze();
        return true;
    }

    public string GetHelp()
    {
        return "Freeze: Freezes the ship. No forces will affect.";
    }
}
