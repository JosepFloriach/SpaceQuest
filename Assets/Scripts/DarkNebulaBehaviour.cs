using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNebulaBehaviour : NebulaBehaviour
{
    public override void StartInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        base.StartInteraction(player, cockpit, transform);
        player.Kill();
    }
}
