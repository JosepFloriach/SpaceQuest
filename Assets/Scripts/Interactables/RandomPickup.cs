using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : BasePickup
{
    [SerializeField] private List<GameObject> pickupsList;

    public override string ID
    {
        get
        {
            return "RandomPickup";
        }
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        base.Pickup(player, cockpit);
        int randomElement = Random.Range(0, pickupsList.Count);
        pickupsList[randomElement].GetComponent<IPickup>().Pickup(player, cockpit);
    }
}
