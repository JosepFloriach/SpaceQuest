using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : BasePickup
{
    public override string ID
    {
        get
        {
            return "ArtifactPickup";
        }
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        soundManager.PlaySound("ImportantPickup");
    }
}
