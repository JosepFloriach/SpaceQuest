using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShipComponent : Force
{
    /// <summary>
    /// Adds a positive force 
    /// <returns> Vector3 that represents the direction of the differential of the force and its magnitude. </returns>
    /// </summary>
    void AddPositive(PhysicsBody cockpit, float maxMagnitude, float diff);

    void AddNegative(PhysicsBody cockpit, float maxMagnitude, float diff);
    
    void Reset(PhysicsBody cockpit);

    bool IsEnabled { get; }

    bool IsEnabledPositive{ get; }

    bool IsEnabledNegative { get; }
}
