using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : BasePickup
{
    public override string ItemId()
    {
        return "StarPickup";
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        player.AddStar();
    }
}
