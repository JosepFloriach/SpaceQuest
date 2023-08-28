using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a force increasing continously in one axis. It can be provided in the positive axis or
/// in the negative axis. It will be continously applied from the moment AddPositive or AddNegative is
/// called, until the moment Reset is called.
/// </summary>
public interface IForce1D : IForce
{
    /// <summary>
    /// Applies a positive force to the provided IPhysicsBody.
    /// </summary>
    /// <param name="cockpit">Physics body where the force is applied</param>
    /// <param name="maxMagnitude">The maximum magnitude the force can reach. It will be clamped up to that maximum</param>
    /// <param name="diff">The differential between each frame. It determines how fast the force grows.</param>
    void AddPositive(IPhysicsBody body, float maxMagnitude, float diff);

    /// <summary>
    /// Adds a negative force
    /// </summary>
    /// <param name="cockpit">Physics body where the force is applied</param>
    /// <param name="maxMagnitude">The maximum magnitude the force can reach. It will be clamped up to that maximum</param>
    /// <param name="diff">The differential between each frame. It determines how fast the force grows.</param>
    void AddNegative(IPhysicsBody body, float maxMagnitude, float diff);
    
    /// <summary>
    /// It resets the force. After this is called no force will be applied anymore to the IPhysicsBody.
    /// </summary>
    /// <param name="body">Physics body from the force is reset/removed.</param>
    void Reset(IPhysicsBody body);

    /// <summary>
    /// Returns if any force is being applied. This will return true if any positive or negative force is applied.
    /// False otherwise.
    /// </summary>
    bool IsEnabled { get; }

    /// <summary>
    /// Returns if any positive force is being applied. This will return true just if any positive is applied.
    /// False otherwise.
    /// </summary>
    bool IsEnabledPositive { get; }

    /// <summary>
    /// Returns if any negative force is being applied. This will return true just if any negative is applied.
    /// False otherwise.
    /// </summary>
    bool IsEnabledNegative { get; }
}
