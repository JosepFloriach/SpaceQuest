using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : BasePickup
{
    public override string ID
    {
        get
        {
            return "StarPickup";
        }
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        soundManager.PlaySound("ImportantPickup");
    }
}
