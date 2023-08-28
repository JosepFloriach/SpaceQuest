using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShipCommand : MonoBehaviour, IConsoleCommand
{
    private HangarController hangarController;

    private void Awake()
    {
        hangarController = FindObjectOfType<HangarController>();
    }

    public bool Execute(params string[] arguments)
    {
        int shipIndex = 0;
        if (!int.TryParse(arguments[1], out shipIndex))
        {
            return false;
        }
        hangarController.UnlockShip(shipIndex);
        return true;
    }

    public string GetCommandName()
    {
        return "UnlockShip";
    }

    public string GetHelp()
    {
        return "UnlockShip [index] : unlocks the ship corresponding to the index provided";
    }
}
