using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Planetoids/CockpitInstance")]
public class CockpitSetup : ScriptableObject
{
    [Tooltip("Unique identifier for this ship.")]
    public string Id;

    [Tooltip("Low resolution sprite.")]
    public Sprite LowResolutionSprite;

    [Tooltip("High resolution sprite.")]
    public Sprite HighResolutionSprite;

    [Tooltip("Cost in gems to buy this ship.")]
    public int GemsCost;

    [Tooltip("The maximum thrusting power the ship can provide.")]
    [Range(0, 10)]
    public int VerticalThrusterPower;

    [Tooltip("The rotation power applied when the player rotates the ship. It affects the maneuverability")]
    [Range(0, 10)]
    public int RotationThrusterPower;

    [Tooltip("The maximum speed the ship can take. All forces will be clamped to this.")]
    [Range(0, 10)]
    public int MaxSpeed;

    [Tooltip("The maximum capacity the of the fuel deposit.")]
    [Range(0, 10)]
    public int MaxFuelCapacity;

    [Tooltip("The maximum capacity of the quantum deposit.")]
    [Range(0, 10)]
    public int MaxQuantumEnergy;

    public float VerticalThrusterPowerFactor;
    public float MaxFuelCapacityFactor;
    public float RotationPowerFactor;
    public float MaxSpeedFactor;
    public float MaxQuantumEnergyFactor;
    public float QuantumRotationPowerFactor;
}
