using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ForceInteractable : InteractableBase, Force
{
    public abstract Vector3 Force { get; protected set; }
}
