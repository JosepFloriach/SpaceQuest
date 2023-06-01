using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeCommand : MonoBehaviour, ConsoleCommand
{
    [SerializeField] private string CommandName;

    private Cockpit cockpit;
    private Player player;

    private void Start()
    {
        cockpit = FindObjectOfType<Cockpit>();
        player = FindObjectOfType<Player>();
    }

    public string GetCommandName()
    {
        return CommandName;
    }

    public bool Execute(params string[] arguments)
    {
        cockpit.TriggerGodMode();
        player.TriggerInvencible();
        return true;
    }

    public string GetHelp()
    {
        return "GodMode: Enables god mode for the player. This allows some other commands that are just possible in this mode.";
    }
}
