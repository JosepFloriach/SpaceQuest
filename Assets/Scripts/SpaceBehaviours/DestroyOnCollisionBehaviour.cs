using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollisionBehaviour : InteractableBase
{
    public override void StartInteraction(Player player, IPhysicsBody body, Transform transform)
    {
        IDestructible destructible = transform.GetComponent<IDestructible>();
        if (destructible != null)
        {
            destructible.Destroy();
        }
    }
}
