using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsBodyBase : PhysicsBody
{

    public Dictionary<string, Vector3> linearForces = new Dictionary<string, Vector3>();
    public Dictionary<string, Vector3> angularForces = new Dictionary<string, Vector3>();

    protected bool freeze = false;
    protected bool forceRotation = false;
    protected bool forceInverseRotation = false;
    protected bool forceRotationToVector3 = false;
    protected Vector3 forcedRotation;

    public PhysicsBodyBase(Transform transform)
    {
        Transform = transform;
    }

    public Transform Transform
    {
        get;
        set;
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

    public void AddLinearForce(string id, Vector3 force)
    {
        linearForces[id] = force;
    }

    public void AddAngularForce(string id, Vector3 force)
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


    public List<Vector3> GetAllLinearForces()
    {
        return linearForces.Values.ToList();
    }

    public List<Vector3> GetAllAngularForces()
    {
        return angularForces.Values.ToList();
    }

    public Vector3 GetLinearForce(string id)
    {
        if (linearForces.ContainsKey(id))
            return linearForces[id];
        return Vector3.zero;
    }

    public Vector3 GetAngularForce(string id)
    {
        if (angularForces.ContainsKey(id))
            return angularForces[id];
        return Vector3.zero;
    }
    
    public void ClearAllForces()
    {
        linearForces.Clear();
        angularForces.Clear();
        ResetLinearVelocity();
    }

    public void ResetRotation()
    {
        Transform.up = Vector3.zero;
    }

    public void Update(float diffTime)
    {
        if (freeze)
            return;

        PreUpdate();
        ComputeLinearVelocity(diffTime);
        PreComputeAngularVelocity();
        ComputeAngularVelocity(diffTime);
        PreUpdatePosition();
        UpdatePosition(diffTime);
        PreUpdateRotation();
        UpdateRotation(diffTime);
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
        Vector3 accLinearForces = Vector3.zero;
        foreach (var force in linearForces)
        {
            accLinearForces += force.Value;
        }
        LinearVelocity += (accLinearForces * diffTime);
    }

    private void ComputeAngularVelocity(float diffTime)
    {
        Vector3 accAngularForces = Vector3.zero;
        foreach (var force in angularForces)
        {
            accAngularForces += force.Value;
        }
        AngularVelocity = (accAngularForces * diffTime);
    }

    private void UpdatePosition(float diffTime)
    {
        Transform.position += (LinearVelocity * diffTime);
    }

    private void UpdateRotation(float diffTime)
    {
        LinearVelocity = Quaternion.Euler(AngularVelocity) * LinearVelocity;
        LinearVelocity = Quaternion.Euler(new Vector3(0.0f,0.0f, AngularMomentum) * diffTime) * LinearVelocity;
        
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
        /*else if (AngularMomentum < 0)
        {
            Transform.rotation = Quaternion.AngleAxis(AngularMomentum * Time.deltaTime, Transform.forward) * Transform.rotation;
        }
        else if (AngularMomentum > 0)
        {
            Transform.rotation = Quaternion.AngleAxis(AngularMomentum * Time.deltaTime, Transform.forward) * Transform.rotation;
        }*/
    }
}
