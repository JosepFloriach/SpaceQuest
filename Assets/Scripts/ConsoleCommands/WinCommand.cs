using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCommand : MonoBehaviour, IConsoleCommand
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public string GetCommandName()
    {
        return "Win";
    }

    public bool Execute(params string[] arguments)
    {
        player.Win();
        return true;
    }

    public string GetHelp()
    {
        return "Win: Automatically wins the level.";
    }
}
