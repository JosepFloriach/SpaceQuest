using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an entity that can be destroyed.
/// </summary>
public interface IDestructible
{
    /// <summary>
    /// Destroys the IDestructible entity.
    /// </summary>
    void Destroy();
}
