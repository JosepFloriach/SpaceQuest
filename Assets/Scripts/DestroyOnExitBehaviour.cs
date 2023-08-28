using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExitBehaviour : InteractableBase
{
    public override void EndInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        IDestructible destructible = transform.GetComponent<IDestructible>();
        if (destructible != null)
        {
            destructible.Destroy();
        }
    }
}