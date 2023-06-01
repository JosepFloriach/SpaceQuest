using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarController : MonoBehaviour
{
    [SerializeField] private CockpitSetup setup;

    public List<GameObject> allowedShips;

    public event EventHandler<CockpitSetup> ShipUpdated;
    private int currentShipIdx;

    private void Start()
    {
        SendShipUpdatedEvent();
    }

    public void ChangeToNext()
    {
        setup.ShipPrefab = allowedShips[(++currentShipIdx) % allowedShips.Count];
        SendShipUpdatedEvent();
    }

    public void ChangeToPrevious()
    {
        setup.ShipPrefab = allowedShips[(--currentShipIdx) % allowedShips.Count];
        SendShipUpdatedEvent();
    }

    /*
    public void IncrementMaxSpeed(int amount)
    {                
        if (setup.AvailableMechanicalPoints - amount < 0)
            return;
        if (setup.MaxSpeed + amount > 10)
            return;

        setup.AvailableMechanicalPoints -= amount;
        setup.MaxSpeed += amount;
        SendParamsUpdatedEvent();
    }

    public void DecrementMaxSpeed(int amount)
    {
        if (setup.AvailableMechanicalPoints + amount > setup.TotalMechanicalPoints)
            return;
        if (setup.MaxSpeed - amount < 0)
            return;

        setup.AvailableMechanicalPoints += amount;
        setup.MaxSpeed -= amount;
        SendParamsUpdatedEvent();
    }

    public void IncrementMaxFuel(int amount)
    {
        if (setup.AvailableMechanicalPoints - amount < 0)
            return;
        if (setup.MaxFuelCapacity + amount > 10)
            return;

        setup.AvailableMechanicalPoints -= amount;
        setup.MaxFuelCapacity += amount;
        SendParamsUpdatedEvent();
    }

    public void DecrementMaxFuel(int amount)
    {
        if (setup.AvailableMechanicalPoints + amount > setup.TotalMechanicalPoints)
            return;
        if (setup.MaxFuelCapacity - amount < 0)
            return;

        setup.AvailableMechanicalPoints += amount;
        setup.MaxFuelCapacity -= amount;
        SendParamsUpdatedEvent();
    }

    public void IncrementQuantumDeposit(int amount)
    {
        if (setup.AvailableMechanicalPoints - amount < 0)
            return;
        if (setup.MaxQuantumEnergy + amount > 10)
            return;

        setup.AvailableMechanicalPoints -= amount;
        setup.MaxQuantumEnergy += amount;
        SendParamsUpdatedEvent();
    }

    public void DecrementQuantumDeposit(int amount)
    {
        if (setup.AvailableMechanicalPoints + amount > setup.TotalMechanicalPoints)
            return;
        if (setup.MaxQuantumEnergy - amount < 0)
            return;

        setup.AvailableMechanicalPoints += amount;
        setup.MaxQuantumEnergy -= amount;
        SendParamsUpdatedEvent();
    }

    public void IncrementManeuverability(int amount)
    {
        if (setup.AvailableMechanicalPoints - amount < 0)
            return;
        if (setup.RotationThrusterPower + amount > 10)
            return;

        setup.AvailableMechanicalPoints -= amount;
        setup.RotationThrusterPower += amount;
        SendParamsUpdatedEvent();
    }

    public void DecrementManeuverability(int amount)
    {
        if (setup.AvailableMechanicalPoints + amount > setup.TotalMechanicalPoints)
            return;
        if (setup.RotationThrusterPower - amount < 0)
            return;

        setup.AvailableMechanicalPoints += amount;
        setup.RotationThrusterPower -= amount;
        SendParamsUpdatedEvent();
    }

    public void IncrementVerticalThrusterPower(int amount)
    {
        if (setup.AvailableMechanicalPoints - amount < 0)
            return;
        if (setup.VerticalThrusterPower + amount > 10)
            return;

        setup.AvailableMechanicalPoints -= amount;
        setup.VerticalThrusterPower += amount;
        SendParamsUpdatedEvent();
    }

    public void DecrementVerticalThrusterPower(int amount)
    {
        if (setup.AvailableMechanicalPoints + amount > setup.TotalMechanicalPoints)
            return;
        if (setup.VerticalThrusterPower - amount < 0)
            return;

        setup.AvailableMechanicalPoints += amount;
        setup.VerticalThrusterPower -= amount;
        SendParamsUpdatedEvent();
    }
    */
    public void SendShipUpdatedEvent()
    {
        ShipUpdated?.Invoke(this, setup);
    }

}
