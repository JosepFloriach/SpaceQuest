using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipDeposit
{
    float Current { get; }
    float MaxCapacity { get; }

    public void Consume(float thrustAmount);
    public void RefillAbsoluteAmount(float amount);
    public void RefillPercentageAmount(float amount);    
    public void SetPercentageAmount(float amount);
    public void SetAbsoluteAmount(float amount);
    public void RefillCompletely();
}
