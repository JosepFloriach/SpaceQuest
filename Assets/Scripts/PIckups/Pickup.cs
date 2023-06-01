using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Pickup
{
    string ItemId();

    void Pickup(Player player, Cockpit cockpit);
}
