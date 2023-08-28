using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Interface <c>IInteractable</c> represents an entity that can trigger an interaction by an external
/// object colliding with it. 
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Provides a unique ID for this force, so it can be retrived from other subsystems.
    /// </summary>
    string ID { get; }

    /// <summary>
    /// It will be called when the external object is starting to collide with the current IInteractable.
    /// It's called just at the first frame where the collision happens.
    /// </summary>
    /// <param name="player">Player that triggered the interaction. Null if it was not triggered by a player</param>
    /// <param name="cockpit">PhysicsBody that triggered the interaction. Null if it was not triggered by any PhysicsBody </param>
    /// <param name="transform">Transform of the gameObjec that triggered the interaction. It cannot be null.</param>
    void StartInteraction(Player player, IPhysicsBody body, Transform transform);

    /// <summary>
    /// It will be called when the external object is keep colliding with the current IInteractable.
    /// It will be called every frame while the collision is happening. 
    /// </summary>
    /// <param name="player">Player that triggered the interaction. Null if it was not triggered by a player</param>
    /// <param name="cockpit">PhysicsBody that triggered the interaction. Null if it was not triggered by any PhysicsBody </param>
    /// <param name="transform">Transform of the gameObjec that triggered the interaction. It cannot be null.</param>
    void ContinueInteraction(Player player, IPhysicsBody cockpit, Transform transform);

    /// <summary>
    /// It will be called when the external object ends the collision with the current IInteractable.
    /// It's called just at the first frame when the collision stops happening.
    /// </summary>
    /// <param name="player">Player that triggered the interaction. Null if it was not triggered by a player</param>
    /// <param name="cockpit">PhysicsBody that triggered the interaction. Null if it was not triggered by any PhysicsBody </param>
    /// <param name="transform">Transform of the gameObjec that triggered the interaction. It cannot be null.</param>
    void EndInteraction(Player player, IPhysicsBody cockpit, Transform transform);
}
