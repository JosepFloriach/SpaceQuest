using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an entity that can be picked up. Usually when the player 
/// collides with it. But is not mandatory. It can be picked in some other
/// ways if the implementation allows it.
/// </summary>
public interface IPickup : IInteractable, IReseteable
{
    /// <summary>
    /// Pickups the item and applies any logic to the player or the ship.
    /// </summary>
    /// <param name="player">Player that triggered the pickup. If this was not triggered by a player, it should be null</param>
    /// <param name="cockpit">Ship that triggered the pickup. If this was not triggered by a ship, it should be null</param>
    void Pickup(Player player, Cockpit cockpit);
}
