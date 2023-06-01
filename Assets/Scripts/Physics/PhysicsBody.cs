using System.Collections.Generic;
using UnityEngine;

public interface PhysicsBody
{
    Vector3 LinearVelocity
    {
        get;
        set;
    }

    Vector3 AngularVelocity
    {
        get;
        set;
    }

    Transform Transform
    {
        get;
        set;
    }

    public void AddLinearForce(string id, Vector3 force);
    public void AddAngularForce(string id, Vector3 force);
    public void AddAngularMomentum(float force);
    public void RemoveAngularMomentum();
    public List<Vector3> GetAllLinearForces();
    public List<Vector3> GetAllAngularForces();
    public Vector3 GetLinearForce(string id);
    public Vector3 GetAngularForce(string id);
    public void RemoveLinearForce(string id);
    public void RemoveAngularForce(string id);
    public void ForceRotationToVelocity(bool force);
    public void ForceRotationToInverseVelocity(bool force);
    public void ForceRotationToVector3(bool force, Vector3 rotation);
    public void ResetRotation();
    public void Update(float diffTime);
    public void ClearAllForces();
}
