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
public abstract class BasePickup : MonoBehaviour, IPickup
{
    public static event EventHandler<PickupEventArgs> OnPickup;
    [SerializeField] protected float PickupEffectDelay = 0.0f;

    protected bool consumed = false;
    protected SoundManager soundManager;
    private Animator animator;
    private float pickupAnimationTime;

    protected Transform originalTransform;

    public abstract string ID { get; }

    protected virtual void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        animator = GetComponent<Animator>();
        ReferenceValidator.NotNull(soundManager, animator);
    }

    protected virtual void Start()
    {
        originalTransform = transform;
    }

    public virtual void StartInteraction(Player player, IPhysicsBody body, Transform transform)
    {
        if (consumed)
        {
            return;
        }

        Cockpit cockpit = body.Transform.GetComponentInParent<Cockpit>();
        if (cockpit == null)
        {
            return;
        }

        Pickup(player, cockpit);
        ConsumePickup();
        SendEventPickup(player, cockpit);
    }

    public virtual void ContinueInteraction(Player player, IPhysicsBody body, Transform transform)
    {
    }

    public virtual void EndInteraction(Player player, IPhysicsBody body, Transform transform)
    {
    }

    public virtual void Reset()
    {
        gameObject.SetActive(true);
        transform.position = originalTransform.position;
        transform.rotation = originalTransform.rotation;
        transform.localScale = originalTransform.localScale;
    }

    public virtual void Pickup(Player player, Cockpit cockpit)
    {
        soundManager.PlaySound("Pickup");
    }

    private void Update()
    {
        if (consumed)
        {
            animator.SetFloat("PickEffectDelay", pickupAnimationTime);
            pickupAnimationTime += Time.deltaTime;
        }
    }

    protected void ConsumePickup()
    {
        consumed = true;
        pickupAnimationTime = PickupEffectDelay;
        animator.SetTrigger("Despawn");
        Invoke("DisableObject", 1.0f);
    }

    private void DisableObject()
    {
        consumed = false;
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
