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

    // Start is called before the first frame update
    void Start()
    {
        MaxDeposit = setup.MaxFuelCapacity * setup.MaxFuelCapacityFactor;
        RefillCompletely();
    }

    public void Consume(float thrustAmount)
    {
        CurrentFuel = Math.Clamp(CurrentFuel - thrustAmount, 0.0f, MaxDeposit);
    }

    public void RefillAmount(float amount)
    {
        CurrentFuel = Mathf.Clamp(CurrentFuel + amount, 0.0f, MaxDeposit);
    }

    public void RefillCompletely()
    {
        CurrentFuel = MaxDeposit;
    }
}
