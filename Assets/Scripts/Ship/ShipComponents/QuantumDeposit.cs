using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumDeposit : MonoBehaviour
{
    public float CurrentEnergy { get; private set; }
    public float MaxDeposit { get; private set; }

    private CockpitSetup setup;

    private void Awake()
    {
        setup = FindObjectOfType<Cockpit>().cockpitSetup;
    }

    private void Start()
    {
        MaxDeposit = setup.MaxQuantumEnergy * setup.MaxQuantumEnergyFactor;
        RefillCompletely();
    }

    public void Consume(float amount)
    {
        CurrentEnergy = Math.Clamp(CurrentEnergy - amount, 0.0f, MaxDeposit);
    }

    public void RefillAbsoluteAmount(float amount)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + amount, 0.0f, MaxDeposit);
    }
    public void RefillPercentageAmount(float amount)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + amount, 0.0f, MaxDeposit);
    }
    public void RefillCompletely()
    {
        CurrentEnergy = MaxDeposit;
    }

    public void SetPercentageAmount(float amount)
    {
        CurrentEnergy = Mathf.Clamp(amount, 0.0f, 1.0f) * MaxDeposit;
    }
}
