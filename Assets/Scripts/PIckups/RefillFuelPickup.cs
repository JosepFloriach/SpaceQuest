using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillFuelPickup : BasePickup
{
    [SerializeField] float FuelAmount;

    public override string ItemId()
    {
        return "RefillFuelPickup";
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        cockpit.RefillFuelDeposit(FuelAmount);
    }

}
