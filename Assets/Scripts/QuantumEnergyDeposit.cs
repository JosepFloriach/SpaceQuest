using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumEnergyDeposit : MonoBehaviour
{
    public float CurrentEnergy { get; private set; }
    public float MaxDeposit { get; private set; }

    private CockpitSetup setup;

    private void Awake()
    {
        setup = FindObjectOfType<Cockpit>().cockpitSetup;
    }

    // Start is called before the first frame update
    void Start()
    {
        MaxDeposit = setup.MaxQuantumEnergy * setup.MaxQuantumEnergyFactor;
        RefillCompletely();
    }

    public void Consume(float amount)
    {
        CurrentEnergy = Math.Clamp(CurrentEnergy - amount, 0.0f, MaxDeposit);
    }

    public void RefillAmount(float amount)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + amount, 0.0f, MaxDeposit);
    }

    public void RefillCompletely()
    {
        CurrentEnergy = MaxDeposit;
    }
}
