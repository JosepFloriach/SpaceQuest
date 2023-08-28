using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ForceInteractable : InteractableBase, IForce
{
    public abstract Vector3 Direction { get; protected set; }

    public abstract GameObject GetObject();
}
