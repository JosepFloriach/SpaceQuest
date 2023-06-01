using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface <c>Force</c> represents a physical force that can be applied to any <c>PhysicsBody</c>. 
/// This force can be applied in one axis, but in both directions. That means, that force can be positive
/// or negative.
/// </summary>
public interface Force
{
    /// <summary>
    /// Gets the current force.
    /// <returns> Vector3 that represents the direction of the force and its magnitude. </returns>
    /// </summary>
    Vector3 Force
    {
        get;
    }

    /// <summary>
    /// Gets differential between the current force and the last time it was a different one. Even if it
    /// has been the same force for several frames in a row, this should take into account the previous 
    /// force that was different than the current one. That means that DiffForce should be always greater 
    /// than zero vector.
    /// <returns> Vector3 that represents the direction of the differential of the force and its magnitude. </returns>
    /// </summary>
    /*Vector3 DiffForce
    {
        get;
    }*/

    /// <summary>
    /// Gets previous force that was different than the current one. Even if it has been the same force for 
    /// several frames in a row, this should take into account the previous force that was different. 
    /// That means that PreviousForce will be always different than Force (the current one)
    /// <returns> Vector3 that represents the direction of the differential of the force and its magnitude. </returns>
    /// </summary>
    /*Vector3 PrevForce
    {
        get;
    }*/

   
    //void AddPositive(PhysicsBody cockpit, float maxMagnitude, float diff);

    //void AddNegative(PhysicsBody cockpit, float maxMagnitude, float diff);

    //void Reset(PhysicsBody cockpit);
}
