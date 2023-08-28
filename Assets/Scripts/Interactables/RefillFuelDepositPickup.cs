using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillFuelDepositPickup : BasePickup
{
    [Tooltip("If enabled, the deposit will be filled based on a percentage of the total capacity of it. Otherwise it will an absolute value.")]
    [SerializeField] bool PercentageRefill;
    [Tooltip("Amount of fuel to refill. If PercentageRefill is enabled it will be clamped from 0 to 1. Otherwise it will be an absolute value.")]
    [SerializeField] float Amount;
    public override string ID
    {
        get
        {
            return "RefillFuelDepositPickup";
        }        
    }
    public override void Pickup(Player player, Cockpit cockpit)
    {
        base.Pickup(player, cockpit);
        if (PercentageRefill)
        {
            if (Amount < 0 || Amount > 1)
            {
                Debug.LogError("PercentageRefill is enabled but the amount set up is not normalized");
            }
            cockpit.RefillFuelDepositPercentageAmount(Mathf.Clamp(Amount, 0, 1));
        }
        else
        {
            cockpit.RefillFuelDepositAbsoluteAmount(Amount);
        }
    }

}
