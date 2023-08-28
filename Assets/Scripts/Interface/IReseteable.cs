using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides an interface to an entity for which its state can be reset to a default one.
/// </summary>
public interface IReseteable
{
    void Reset();
}
