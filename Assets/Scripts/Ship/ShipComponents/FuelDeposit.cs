using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelDeposit : MonoBehaviour
{
    public float CurrentFuel { get; private set; }
    public float MaxDeposit { get; private set; }

    private CockpitSetup setup;

    private void Awake()
    {
        setup = FindObjectOfType<Cockpit>().cockpitSetup;
    }

    private void Start()
    {
        MaxDeposit = setup.MaxFuelCapacity * setup.MaxFuelCapacityFactor;
        RefillCompletely();
    }

    public void Consume(float thrustAmount)
    {
        CurrentFuel = Math.Clamp(CurrentFuel - thrustAmount, 0.0f, MaxDeposit);
    }

    public void RefillAbsoluteAmount(float amount)
    {
        CurrentFuel = Mathf.Clamp(CurrentFuel + amount, 0.0f, MaxDeposit);
    }

    public void RefillPercentageAmount(float amount)
    {
        amount = Mathf.Clamp(amount, 0, 1);
        float percentage = amount * MaxDeposit;
        CurrentFuel = Mathf.Clamp(CurrentFuel + percentage, 0.0f, MaxDeposit);
    }
    public void RefillCompletely()
    {
        CurrentFuel = MaxDeposit;
    }

    public void SetPercentageAmount(float amount)
    {
        CurrentFuel = Mathf.Clamp(amount, 0.0f, 1.0f) * MaxDeposit;
    }
}
