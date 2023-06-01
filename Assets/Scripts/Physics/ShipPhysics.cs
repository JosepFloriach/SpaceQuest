using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPhysics : PhysicsBodyBase
{
    private Cockpit cockpit;
    private float prevSpeed;

    public ShipPhysics(Cockpit cockpit, Transform transform)
        : base(transform)
    {
        this.cockpit = cockpit;
    }

    protected override void PreUpdateRotation()
    {
        if (cockpit.ThrustingBackward)
        {
            if (GetLinearForce("VerticalThruster").magnitude > LinearVelocity.magnitude)
            {
                float dotProduct = Vector2.Dot(GetLinearForce("VerticalThruster").normalized, LinearVelocity.normalized);
                bool sameDirection = dotProduct > 0;
                if (sameDirection)
                {
                    ForceRotationToInverseVelocity(true);
                }
                else
                {
                    ForceRotationToVelocity(true);
                }
            }
            else
            {
                if (Vector2.Dot(GetLinearForce("VerticalThruster").normalized, LinearVelocity.normalized) > 0)
                {
                    ForceRotationToInverseVelocity(true);
                }
                else
                {
                    ForceRotationToVelocity(true);
                }
            }
        }
        else
        {
            if (Vector2.Dot(LinearVelocity.normalized, cockpit.transform.up) > 0)
            {
                ForceRotationToVelocity(true);
            }
            else
            {
                ForceRotationToInverseVelocity(true);
            }
        }

    }
}
