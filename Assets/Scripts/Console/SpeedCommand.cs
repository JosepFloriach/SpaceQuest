using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCommand : MonoBehaviour, ConsoleCommand
{
    [SerializeField] private string CommandName;

    private Cockpit cockpit;
    
    void Start()
    {
        cockpit = GameObject.FindObjectOfType<Cockpit>();
    }

    public bool Execute(params string[] arguments)
    {
        float speed;
        if (!float.TryParse(arguments[1], out speed))
        {
            return false;
        }
        cockpit.SetSpeed(speed);
        return true;
    }

    public string GetCommandName()
    {
        return CommandName;
    }

    public string GetHelp()
    {
        return "SetSpeed [value]: Only in god mode. Sets the speed of the ship, keeping the same direction (by just modifying Velocity's magnitude).";
    }
}
