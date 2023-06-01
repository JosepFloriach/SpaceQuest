using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    string ID { get; }

    void StartInteraction(Player player, PhysicsBody cockpit, Transform transform);
    void ContinueInteraction(Player player, PhysicsBody cockpit, Transform transform);
    void EndInteraction(Player player, PhysicsBody cockpit, Transform transform);
}
