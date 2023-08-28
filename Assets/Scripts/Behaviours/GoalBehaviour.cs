using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : InteractableBase
{
    [Serializable]
    private class InventoryCondition
    {
        public BasePickup Pickup;
        public int MinimumCount;
    }

    [SerializeField] private List<InventoryCondition> objectsToPickup;
    [SerializeField] private DialogBehaviour dialog;

    private Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        ReferenceValidator.NotNull(inventory);
    }

    public override void StartInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        DialogController.GetInstance().CloseDialog();
        foreach (var item in objectsToPickup)
        {
            if (inventory.GetItemCount(item.Pickup.ID) < item.MinimumCount)
            {
                if (dialog != null)
                {
                    DialogController.GetInstance().OpenDialogRequest(dialog.Setup);
                }
                return;
            }
        }
        player.Win();
    }
}
