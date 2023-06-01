using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, Interactable
{

    public string ID
    {
        protected set;
        get;
    }

    protected virtual void Start()
    {
        ID = Guid.NewGuid().ToString();
        OnStart();
    }

    protected virtual void OnStart()
    {
    }

    public virtual void StartInteraction(Player player, PhysicsBody cockpit, Transform transform)
    {
    }

    public virtual void ContinueInteraction(Player player, PhysicsBody cockpit, Transform transform)
    {
    }

    public virtual void EndInteraction(Player player, PhysicsBody cockpit, Transform transform)
    {
    }
}
