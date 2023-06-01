using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCommand : MonoBehaviour, ConsoleCommand
{
    [SerializeField] private string commandName;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public string GetCommandName()
    {
        return commandName;
    }

    public bool Execute(params string[] arguments)
    {
        player.Kill();
        return true;
    }

    public string GetHelp()
    {
        return "Kill: Automatically kills the player.";
    }
}
