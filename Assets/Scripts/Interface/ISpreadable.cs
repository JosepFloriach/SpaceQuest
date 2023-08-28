using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an entity that can be spread in several subparts.
/// </summary>
public interface ISpreadable
{
    /// <summary>
    /// Spreads each of the parts of the parent (the ISpreadable) entity.
    /// </summary>
    void Spread();
}
