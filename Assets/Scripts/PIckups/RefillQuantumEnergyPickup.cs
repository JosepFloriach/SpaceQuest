using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillQuantumEnergyPickup : BasePickup
{
    [SerializeField] float QuantumEnergyAmount;

    public override string ItemId()
    {
        return "RefillQuantumEnergyPickup";
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        cockpit.RefillQuantumEnergy(QuantumEnergyAmount);
    }


}
