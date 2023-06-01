using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : GoalBehaviourBase
{
    [Serializable]
    private class InventoryCondition
    {
        public BasePickup Pickup;
        public int MinimumCount;
    }

    [SerializeField] private List<InventoryCondition> objectsToPickup;
    [SerializeField] private DialogSetup dialogBeforeAllItemsPickup;

    private DialogController dialogController;
    private Inventory inventory;

    private void Awake()
    {
        dialogController = FindObjectOfType<DialogController>();
        inventory = FindObjectOfType<Inventory>();
    }

    public override void StartInteraction(Player player, PhysicsBody cockpit, Transform transform)
    {
        foreach(var item in objectsToPickup)
        {
            if (inventory.GetItemCount(item.Pickup.ItemId()) < item.MinimumCount)
            {
                dialogController.OpenDialog(dialogBeforeAllItemsPickup);
                return;
            }
        }
        player.Win();
    }
}
