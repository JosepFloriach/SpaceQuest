using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPhysics : PhysicsBodyBase
{
    private Cockpit cockpit;

    public ShipPhysics(Cockpit cockpit, Transform transform)
        : base(transform)
    {
        this.cockpit = cockpit;
    }

    protected override void PreUpdateRotation()
    {
        if (!IsFrozen)
        {
            /*if (GetLinearForce("PlanetGravity") == null)
            {
                ForceRotationOutOfGravity();
            }
            else
            {
                ForceRotationOnGravityField();
            }*/
            ForceRotationOutOfGravity();
        }
    }

    private void ForceRotationOnGravityField()
    {
        ForceRotationToVelocity(false);
        ForceRotationToInverseVelocity(false);
    }

    private void ForceRotationOutOfGravity()
    {
        if (cockpit.ThrustingBackward)
        {
            if (GetLinearForce("VerticalThruster").Direction.magnitude > LinearVelocity.magnitude)
            {
                float dotProduct = Vector2.Dot(GetLinearForce("VerticalThruster").Direction.normalized, LinearVelocity.normalized);
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
                if (Vector2.Dot(GetLinearForce("VerticalThruster").Direction.normalized, LinearVelocity.normalized) > 0)
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
            if (Vector2.Dot(LinearVelocity.normalized, cockpit.transform.up) >= 0)
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
