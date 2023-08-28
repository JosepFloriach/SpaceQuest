using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an entity that can be affected by physics. External objects add forces to the IPhysicsBody 
/// and this is updating rotation and position depending on this forces applied.
/// </summary>
public interface IPhysicsBody
{
    /// <summary>
    /// Returns the linear speed of the body. This is the magnitude of the LinearVelocity.
    /// </summary>
    float LinearSpeed
    {
        get;
    }

    /// <summary>
    /// Returns and sets the linear velocity of the body. 
    /// </summary>
    Vector3 LinearVelocity
    {
        get;
        set;
    }

    /// <summary>
    /// Returns and sets the angular velocity of the body. 
    /// </summary>
    Vector3 AngularVelocity
    {
        get;
        set;
    }

    /// <summary>
    /// Returns if the body is frozen.
    /// </summary>
    bool IsFrozen
    {
        get;
    }

    /// <summary>
    /// Returns the transform of the body.
    /// </summary>
    Transform Transform
    {
        get;
    }

    /// <summary>
    /// Add an instant linear force to this body.That means that it will be applied directly into the linear velocity without
    /// taking into account the rest of linear forces. Use it to simulate sudden forces like explosions.
    /// </summary>
    /// <param name="force">IForce reference. IForce contains the direction of the force and the object that is applying it.</param>
    public void AddInstantLinearForce(IForce force);

    /// <summary>
    /// Add an linear force to this body.
    /// </summary>
    /// <param name="force">IForce reference. IForce contains the direction of the force and the object that is applying it.</param>
    public void AddLinearForce(string id, IForce force);

    /// <summary>
    /// Add 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="force"></param>
    public void AddAngularForce(string id, IForce force);
    public void AddAngularMomentum(float force);
    public void RemoveAngularMomentum();
    public List<IForce> GetAllLinearForces();
    public List<IForce> GetAllAngularForces();
    public IForce GetLinearForce(string id);
    public IForce GetAngularForce(string id);
    public void RemoveLinearForce(string id);
    public void RemoveAngularForce(string id);
    public void ForceRotationToVelocity(bool force);
    public void ForceRotationToInverseVelocity(bool force);
    public void ForceRotationToVector3(bool force, Vector3 rotation);
    public void ResetRotation();
    public void Update(float diffTime);
    public void FixedUpdate(float diffTime);
    public void ClearAllForces();
    public void SetLinearSpeed(float speed);
    public void Freeze();
    public void UnFreeze();
}
