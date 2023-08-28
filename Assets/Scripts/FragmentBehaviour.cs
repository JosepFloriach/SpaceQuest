using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentBehaviour : MonoBehaviour, IDestructible, IReseteable
{
    private enum Behaviour
    {
        NonSpreadable,
        Spline,
        Physics,
    }

    [SerializeField] private Behaviour behaviour;
    [SerializeField] private GameObject explosionPrototype;
    [SerializeField] private SplineNavigator splineNavigator;

    private bool pathForward = true;
    private PhysicsBodyBehaviour physicsBodyBehaviour;
    private SoundManager soundManager;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    private void Awake()
    {
        if (!ValidateSetup())
        {
            throw new MissingComponentException("Wrong setup on fragment " + gameObject.name + ". Check that all required components depending on behaviour type.");
        }
        physicsBodyBehaviour = GetComponent<PhysicsBodyBehaviour>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }

    private bool ValidateSetup()
    {
        switch (behaviour)
        {
            case Behaviour.NonSpreadable:
                return true;
            case Behaviour.Spline:
                return
                    splineNavigator != null;
            case Behaviour.Physics:
                return 
                    GetComponent<PhysicsBodyBehaviour>() != null &&
                    GetComponent<Rigidbody2D>() != null && 
                    GetComponent<ForceApplier>() != null;
        }
        return false;
    }

    public void OnSpread()
    {
        switch(behaviour)
        {
            case Behaviour.NonSpreadable:
                gameObject.SetActive(false);
                break;
            case Behaviour.Spline:
                SpreadWithSpline();
                break;
            case Behaviour.Physics:
                SpreadWithPhysics();
                break;
            default:
                throw new System.Exception("Unkonwn behaviour type " + behaviour.GetType().Name + " for Fragment " + gameObject.name);
        }
    }

    private void SpreadWithSpline()
    {
        //splineNavigator.SetTravelForward(pathForward);        
        splineNavigator.Navigate();
        pathForward = !pathForward;
    }

    private void SpreadWithPhysics()
    {
        GetComponent<ForceApplier>().ApplyForce();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (behaviour == Behaviour.Physics)
        {         
            var interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {                
                interactable.StartInteraction(null, physicsBodyBehaviour.PhysicsBody, transform);
            }
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (behaviour == Behaviour.Physics)
        {
            var interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {                
                interactable.ContinueInteraction(null, physicsBodyBehaviour.PhysicsBody, transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (behaviour == Behaviour.Physics)
        {
            var interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                interactable.EndInteraction(null, physicsBodyBehaviour.PhysicsBody, transform);
            }
        }
    }

    public void Destroy()
    {
        var explosion = GameObject.Instantiate(explosionPrototype, transform.parent);
        explosion.transform.position = transform.position;
        explosion.GetComponent<ParticleSystem>().Play();
        soundManager.PlaySound("Explosion");
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        if (physicsBodyBehaviour)
        {
            physicsBodyBehaviour.PhysicsBody.Freeze();
        }
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
        gameObject.transform.localScale = originalScale;
        gameObject.SetActive(true);
    }
}
