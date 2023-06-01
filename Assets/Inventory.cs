using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<string, int> items = new();  

    public void Start()
    {
        BasePickup.OnPickup += OnPickup;
    }

    public void DropEverything()
    {
        items.Clear();
    }

    public int GetItemCount(string itemId)
    {
        if (!HasItem(itemId))
        {
            return 0;
        }
        return items[itemId];
    }

    public bool HasItem(string itemId)
    {
        return items.ContainsKey(itemId);
    }

    public bool ThrowItem(string itemId)
    {
        if (!HasItem(itemId))
        {
            return false;
        }
        items[itemId]--;
        if (items[itemId] == 0)
        {
            items.Remove(itemId);
        }
        return true;
    }

    private void OnPickup(object sender, PickupEventArgs args)
    {
        string itemId = args.pickUp.ItemId();
        if (!items.ContainsKey(itemId))
        {
            items.Add(itemId, 0);
        }
        items[itemId]++;
    }
}
