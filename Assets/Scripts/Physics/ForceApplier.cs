using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(PhysicsBodyBehaviour))]
public class ForceApplier : MonoBehaviour, IForce
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private bool applyOnStart;

    private PhysicsBodyBehaviour physicsBodyBehaviour;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
        private set
        {
            direction = value;
        }
    }

    private void Awake()
    {
        physicsBodyBehaviour = GetComponent<PhysicsBodyBehaviour>();
    }

    private void Start()
    {
        if (applyOnStart)
        {
            ApplyForce();
        }
        else
        {
            physicsBodyBehaviour.PhysicsBody.Freeze();
        }
    }

    public void ApplyForce()
    {
        physicsBodyBehaviour.PhysicsBody.UnFreeze();
        physicsBodyBehaviour.PhysicsBody.AddInstantLinearForce(this);
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawLine(transform.position, transform.position + direction);
#endif
    }
}
