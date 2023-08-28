using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBodyBehaviour : MonoBehaviour
{
    public IPhysicsBody PhysicsBody;

    private void Awake()
    {
        PhysicsBody = CreatePhysicsBody();
    }

    protected virtual IPhysicsBody CreatePhysicsBody()
    {
        return new PhysicsBodyBase(this.transform);
    }

    private void OnEnable()
    {
        //PhysicsBody.ClearAllForces();
        PhysicsBody.SetLinearSpeed(0.0f);
    }

    private void Update()
    {
        PhysicsBody.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        PhysicsBody.FixedUpdate(Time.fixedDeltaTime);  
    }

    private void OnDrawGizmos()
    {
        if (PhysicsBody != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + PhysicsBody.LinearVelocity);

            Gizmos.color = Color.blue;
            foreach (IForce force in PhysicsBody.GetAllLinearForces())
            {
                Gizmos.DrawLine(transform.position, transform.position + force.Direction);
            }
        }
    }

}
