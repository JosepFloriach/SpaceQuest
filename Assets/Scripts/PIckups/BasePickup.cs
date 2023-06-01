using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PickupEventArgs
{
    public BasePickup pickUp;
    public Player player;
    public Cockpit cockpit;
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class BasePickup : MonoBehaviour, Pickup
{
    public static event EventHandler<PickupEventArgs> OnPickup; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = FindObjectOfType<Player>();
        Cockpit cockpit = other.GetComponent<Cockpit>();

        if (cockpit == null)
            return;

        Pickup(player, cockpit);
        ConsumePickup();
        SendEventPickup(player, cockpit);
    }

    public abstract void Pickup(Player player, Cockpit cockpit);

    public abstract string ItemId();

    protected void ConsumePickup()
    {
        GetComponent<Animator>().SetTrigger("Despawn");
        Invoke("DisableObject", 2.0f);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    protected void SendEventPickup(Player player, Cockpit cockpit)
    {
        PickupEventArgs args = new();
        args.pickUp = this;
        args.player = player;
        args.cockpit = cockpit;

        OnPickup?.Invoke(this, args);
    }
}
