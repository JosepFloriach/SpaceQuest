using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NebulaBehaviour : InteractableBase
{
    static public Action<GameObject> NebulaExit;

    public override void EndInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        NebulaExit?.Invoke(this.transform.parent.gameObject);
    }
}
