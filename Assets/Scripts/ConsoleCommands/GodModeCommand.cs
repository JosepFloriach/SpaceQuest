using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeCommand : MonoBehaviour, IConsoleCommand
{
    private Cockpit cockpit;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public string GetCommandName()
    {
        return "GodMode";
    }

    public bool Execute(params string[] arguments)
    {        
        cockpit = FindObjectOfType<Cockpit>();
        if (cockpit != null)
        {
            cockpit.TriggerGodMode();
        }
        player.TriggerInvencible();
        return true;
    }

    public string GetHelp()
    {
        return "GodMode: Enables god mode for the player. This allows some other commands that are just possible in this mode, and makes invencible the player. He cannot die or explode.";
    }
}
