using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAllShipsCommand : MonoBehaviour, IConsoleCommand
{
    private HangarController hangarController;

    private void Awake()
    {
        hangarController = FindObjectOfType<HangarController>();
    }

    public bool Execute(params string[] arguments)
    {
        for (int idx = 0; idx < hangarController.Ships.Count; ++idx)
        {
            hangarController.UnlockShip(idx);
        }
        return true;
    }

    public string GetCommandName()
    {
        return "UnlockAllShips";
    }

    public string GetHelp()
    {
        return "UnlockAllShips: Unlocks all available ships in the game";
    }

}
