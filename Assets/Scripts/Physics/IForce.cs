using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface <c>IForce</c> represents an entity that is producing a physical force.
/// </summary>
public interface IForce
{
    /// <summary>
    /// Gets the force direction.
    /// <returns> Vector3 that represents the direction of the force and its magnitude. </returns>
    /// </summary>
    Vector3 Direction
    {
        get;
    }

    /// <summary>
    /// Gets the object that is producing the force.
    /// <returns> GameObject that is producing the force.</returns>
    /// </summary>
    GameObject GetObject();
}
