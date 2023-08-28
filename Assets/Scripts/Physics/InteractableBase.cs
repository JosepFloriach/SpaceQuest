using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable, IReseteable
{
    private Transform originalTransform;

    public string ID
    {
        protected set;
        get;
    }

    protected virtual void Start()
    {
        ID = Guid.NewGuid().ToString();
        OnStart();
        originalTransform = transform;
    }

    protected virtual void OnStart()
    {
    }

    public virtual void StartInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
    }

    public virtual void ContinueInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
    }

    public virtual void EndInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        transform.position = originalTransform.position;
        transform.rotation = originalTransform.rotation;
        transform.localScale = originalTransform.localScale;
    }
}
