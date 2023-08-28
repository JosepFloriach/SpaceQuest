using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cockpit))]
public class ShipPhysicsBodyBehaviour : PhysicsBodyBehaviour
{
    protected override IPhysicsBody CreatePhysicsBody()
    {
        return new ShipPhysics(GetComponent<Cockpit>(), this.transform); ;
    }
}
