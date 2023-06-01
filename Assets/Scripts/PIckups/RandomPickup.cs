using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : BasePickup
{
    [SerializeField] private List<GameObject> pickupsList;

    public override string ItemId()
    {
        return "RandomPickup";
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        int randomElement = Random.Range(0, pickupsList.Count);
        pickupsList[randomElement].GetComponent<Pickup>().Pickup(player, cockpit);
    }
}
