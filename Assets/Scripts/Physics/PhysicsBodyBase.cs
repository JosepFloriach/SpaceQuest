using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsBodyBase : IPhysicsBody
{

    private Dictionary<string, IForce> linearForces = new();
    private Dictionary<string, IForce> angularForces = new();

    private bool freeze = false;
    private bool forceRotation = false;
    private bool forceInverseRotation = false;
    private bool forceRotationToVector3 = false;
    private Vector3 forcedRotation;

    public bool IsFrozen
    {
        get
        {
            return freeze;
        }
    }

    public PhysicsBodyBase(Transform transform)
    {
        Transform = transform;
    }

    public Transform Transform
    {
        get;
        protected set;
    }

    public float LinearSpeed
    {
        private set;
        get;
    }

    public Vector3 LinearVelocity
    {
        set;
        get;
    }

    public Vector3 AngularVelocity
    {
        set;
        get;
    }

    public float AngularMomentum
    {
        set;
        get;
    }

    public void Freeze()
    {
        freeze = true;
    }

    public void UnFreeze()
    {
        freeze = false;
    }

    public void AddInstantLinearForce(IForce force)
    {
        LinearVelocity = force.Direction;
    }

    public void AddLinearForce(string id, IForce force)
    {
        linearForces[id] = force;
    }

    public void AddAngularForce(string id, IForce force)
    {
        angularForces[id] = force;
    }

    public void AddAngularMomentum(float force)
    {
        AngularMomentum = force;
    }

    public void RemoveAngularMomentum()
    {
        AngularMomentum = 0.0f;
    }

    public void RemoveLinearForce(string id)
    {
        linearForces.Remove(id);
    }

    public void RemoveAngularForce(string id)
    {
        angularForces.Remove(id);
    }

    public void ResetLinearVelocity()
    {
        LinearVelocity = Vector3.zero;
    }

    public void ForceRotationToVelocity(bool force)
    {
        if (force)
        {
            forceRotation = true;
            forceInverseRotation = false;
        }
        else
        {
            forceRotation = false;
        }
    }
    public void ForceRotationToInverseVelocity(bool force)
    {
        if (force)
        {
            forceRotation = false;
            forceInverseRotation = true;
        }
        else
        {
            forceInverseRotation = false;
        }
    }

    public void ForceRotationToVector3(bool force, Vector3 rotation)
    {
        forceRotationToVector3 = force;
        forcedRotation = rotation;
    }


    public List<IForce> GetAllLinearForces()
    {
        return linearForces.Values.ToList();
    }

    public List<IForce> GetAllAngularForces()
    {
        return angularForces.Values.ToList();
    }

    public IForce GetLinearForce(string id)
    {
        if (linearForces.ContainsKey(id))
            return linearForces[id];
        return null;
    }

    public IForce GetAngularForce(string id)
    {
        if (angularForces.ContainsKey(id))
            return angularForces[id];
        return null;
    }
    
    public void ClearAllForces()
    {
        linearForces.Clear();
        angularForces.Clear();
        ResetLinearVelocity();
    }

    public void SetLinearSpeed(float speed)
    {
        // Set speed to 0 would mean to have a vector of magnitude 0, which is not defined.
        // If speed provided is 0, set it to a very low speed, and handle it by not moving 
        // the body if the speed is less than that threshold. This way, the client is allowed
        // to set the speed to 0 (which should be a normal use case).
        speed = Mathf.Clamp(speed, 0.0001f, speed);
        // When Linear velocity is zero no speed can be applied. So, set it up to the transform up, 
        // so the client can set the movement to some expected direction.
        if (LinearVelocity == Vector3.zero)
        {
            LinearVelocity = Transform.up;
        }
        LinearVelocity = LinearVelocity.normalized * speed;
    }

    public void ResetRotation()
    {
        Transform.up = Vector3.zero;
    }

    public void Update(float diffTime)
    {
        UpdatePosition(diffTime);
        UpdateRotation(diffTime);
    }

    public void FixedUpdate(float diffTime)
    {
        
        PreUpdate();
        ComputeLinearVelocity(diffTime);
        PreComputeAngularVelocity();
        ComputeAngularVelocity(diffTime);
        PreUpdatePosition();        
        //UpdatePosition(diffTime);
        PreUpdateRotation();
        //UpdateRotation(diffTime);
        PostUpdate();
    }

    protected virtual void PreUpdate()
    {
    }

    protected virtual void PreComputeAngularVelocity()
    {
    }

    protected virtual void PreUpdatePosition()
    {
    }

    protected virtual void PreUpdateRotation()
    {
    }

    protected virtual void PostUpdate()
    {
    }

    private void ComputeLinearVelocity(float diffTime)
    {
        if (freeze)
        {
            return;
        }

        Vector3 accLinearForces = Vector3.zero;
        foreach (var force in linearForces)
        {
            accLinearForces += force.Value.Direction;
        }
        LinearVelocity += (accLinearForces * diffTime);
        LinearSpeed = LinearVelocity.magnitude;
    }

    private void ComputeAngularVelocity(float diffTime)
    {
        if (freeze)
        {
            return;
        }

        Vector3 accAngularForces = Vector3.zero;
        foreach (var force in angularForces)
        {
            accAngularForces += force.Value.Direction;
        }
        AngularVelocity = (accAngularForces * diffTime);
    }

    private void UpdatePosition(float diffTime)
    {
        if (freeze)
        {
            return;
        }

        // Don't move the body for extremly slow velocities. This is 
        // to handle setting the speed at 0.
        if (LinearVelocity.magnitude < 0.0002f)
        {
            return;
        }            
        Transform.position += (LinearVelocity * diffTime);
    }

    private void UpdateRotation(float diffTime)
    {
        if (LinearSpeed < 0.1f || freeze)
        {
            return;
        }

        LinearVelocity = Quaternion.Euler(AngularVelocity) * LinearVelocity;
        LinearVelocity = Quaternion.Euler(new Vector3(0.0f, 0.0f, AngularMomentum) * diffTime) * LinearVelocity;        
        
        if (forceRotationToVector3)
        {
            Transform.up = forcedRotation;
        }
        else if (forceRotation)
        {
            Transform.up = LinearVelocity.normalized;
        }
        else if (forceInverseRotation)
        {
            Transform.up = -LinearVelocity.normalized;
        }
        else if (AngularMomentum < 0)
        {
            Transform.rotation = Quaternion.AngleAxis(AngularMomentum * Time.deltaTime, Transform.forward) * Transform.rotation;
        }
        else if (AngularMomentum > 0)
        {
            Transform.rotation = Quaternion.AngleAxis(AngularMomentum * Time.deltaTime, Transform.forward) * Transform.rotation;
        }
    }
}
