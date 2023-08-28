using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCommand : MonoBehaviour, IConsoleCommand
{
    private Cockpit cockpit;
    
    public bool Execute(params string[] arguments)
    {
        cockpit = GameObject.FindObjectOfType<Cockpit>();
        if (cockpit == null)
        {
            return false;
        }

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
        return "SetSpeed";
    }

    public string GetHelp()
    {
        return "SetSpeed [value]: Sets the speed of the ship, keeping the same direction (by just modifying Velocity's magnitude).";
    }
}
