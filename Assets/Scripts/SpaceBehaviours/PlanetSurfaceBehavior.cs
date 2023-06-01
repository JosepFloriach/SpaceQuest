using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSurfaceBehavior : InteractableBase
{
    public override void StartInteraction(Player player, PhysicsBody cockpit, Transform transform)
    {
        player.Kill();
    }
}
