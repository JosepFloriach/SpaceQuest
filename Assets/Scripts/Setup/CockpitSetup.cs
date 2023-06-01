using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Planetoids/CockpitInstance")]
public class CockpitSetup : ScriptableObject
{

    [Tooltip("Amount of mechanical points the player has to upgrade the ship")]
    [Range(0, 50)]
    public int TotalMechanicalPoints;

    [Tooltip("Amount of mechanical points already spent upgrading the ship")]
    [Range(0, 50)]
    public int AvailableMechanicalPoints;

    [Tooltip("Initial direction in which the ship will be facing. In degrees.")]
    [Range(0, 360)]
    public float InitialDirection;

    [Tooltip("The maximum thrusting power the ship can provide.")]
    [Range(0, 10)]
    public int VerticalThrusterPower;

    [Tooltip("The rotation power applied when the player rotates the ship. It affects the maneuverability")]
    [Range(0, 10)]
    public int RotationThrusterPower;

    [Tooltip("The maximum amount of fuel the ship can carry.")]
    [Range(0, 10)]
    public int MaxFuelCapacity;

    [Tooltip("The maximum speed the ship can take. All forces will be clamped to this.")]
    [Range(0, 10)]
    public int MaxSpeed;

    [Tooltip("The maximum amount of quantum energy the ship can carry.")]
    [Range(0, 10)]
    public int MaxQuantumEnergy;

    public GameObject ShipPrefab;

    public float VerticalThrusterPowerFactor;
    public float MaxFuelCapacityFactor;
    public float RotationPowerFactor;
    public float MaxSpeedFactor;
    public float MaxQuantumEnergyFactor;
    public float QuantumRotationPowerFactor;

    /*
    [Tooltip("Initial direction in which the ship will be facing. In degrees.")]
    [Range(0, 360)]
    public float InitialDirection;

    [Tooltip("The maximum thrusting power the ship can provide.")]
    [Min(0)]
    public float VerticalThrusterPower;

    [Tooltip("The rotation power applied when the player rotates the ship. It affects the maneuverability")]
    [Min(0)]
    public float RotationThrusterPower;

    [Tooltip("The maximum amount of fuel the ship can carry.")]
    [Min(0)]
    public float MaxFuel;

    [Tooltip("The maximum speed the ship can take. All forces will be clamped to this.")]
    [Min(0)]
    public float MaxSpeed;

    [Tooltip("The maximum amount of quantum energy the ship can carry.")]
    [Min(0)]
    public float MaxQuantumEnergy;
    */
}
