using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationNebulaBehaviour : NebulaBehaviour
{
    [SerializeField] float accelerationPercentage;

    public override void StartInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        base.StartInteraction(player, cockpit, transform);
        float prevLinearVelocity = cockpit.LinearVelocity.magnitude;
        cockpit.LinearVelocity += (cockpit.LinearVelocity.normalized * prevLinearVelocity * accelerationPercentage);
    }
}
