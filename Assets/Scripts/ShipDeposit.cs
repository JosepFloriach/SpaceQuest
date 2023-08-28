using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDeposit : IShipDeposit
{
    public ShipDeposit(float maxCapacity)
    {
        MaxCapacity = maxCapacity;
        RefillCompletely();
    }

    public float Current
    {
        get;
        private set;
    }

    public float MaxCapacity
    {
        get;
        private set;
    }

    public void Consume(float amount)
    {
        Current = Mathf.Clamp(Current - amount, 0.0f, MaxCapacity);
    }
    public void RefillCompletely()
    {
        Current = MaxCapacity;
    }

    public void RefillAbsoluteAmount(float amount)
    {
        Current = Mathf.Clamp(Current + amount, 0.0f, MaxCapacity);
    }

    public void RefillPercentageAmount(float amount)
    {
        amount = Mathf.Clamp(amount, 0, 1);
        float percentage = amount * MaxCapacity;
        Current = Mathf.Clamp(Current + percentage, 0.0f, MaxCapacity);
    }

    public void SetAbsoluteAmount(float amount)
    {
        Current = Mathf.Clamp(amount, 0.0f, MaxCapacity);
    }

    public void SetPercentageAmount(float amount)
    {
        Current = Mathf.Clamp(amount, 0.0f, 1.0f) * MaxCapacity;
    }
}
