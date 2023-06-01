using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDecreasePickup : BasePickup
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float PercentageAmount;

    public override string ItemId()
    {
        return "SpeedDecreasePickup";
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        float currentSpeed = cockpit.PhysicsBody.LinearVelocity.magnitude;
        float boostedSpeed = currentSpeed - (currentSpeed * PercentageAmount);

        cockpit.PhysicsBody.LinearVelocity = cockpit.PhysicsBody.LinearVelocity.normalized * boostedSpeed;
    }
}
