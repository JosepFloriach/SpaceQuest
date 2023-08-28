using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelProgressController;

public class UnlockAllLevelsCommand : MonoBehaviour, IConsoleCommand
{
    private LevelProgressController levelProgressController;

    private void Awake()
    {
        levelProgressController = FindObjectOfType<LevelProgressController>();
    }

    public bool Execute(params string[] arguments)
    {
        for (int idx = 0; idx < 3; ++idx)
        {
            LevelCompletion level = new();
            level.LevelIdx = idx;
            level.LevelName = "";
            level.Completed = true;
            level.GemsCollected = new List<bool> { true, true, true };
            levelProgressController.UpdateLevelProgress(level);
        }
        return true;
    }

    public string GetCommandName()
    {
        return "UnlockAllLevels";
    }

    public string GetHelp()
    {
        return "UnlockAllLevels : Unlock automatically all levels of the game";
    }
}
