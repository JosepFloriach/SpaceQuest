using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePickup : BasePickup
{
    public override string ID
    {
        get
        {
            return "BouncePickup";
        }
    }
    public override void Pickup(Player player, Cockpit cockpit)
    {
        base.Pickup(player, cockpit);

        Vector3 prevForce = cockpit.PhysicsBody.LinearVelocity;
        cockpit.PhysicsBody.ClearAllForces();
        cockpit.PhysicsBody.LinearVelocity = prevForce * -1;
    }
}
