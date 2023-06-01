using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSetup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float KillRadius;
    [SerializeField] private float TotalRadius;

    [Header("Visuals")]
    [SerializeField] private Color CenterColor;
    [SerializeField] private float CenterRadius;

    [Header("Gravity Physics")]
    [SerializeField] private bool EditCurves = false;
    [SerializeField] private float MinimumOrbitSpeed;
    [SerializeField] private float MaximumOrbitSpeed;
    [SerializeField] private float MaximumDistance;
    [SerializeField] private int OrbitDivisions;
    [SerializeField] private float DivisionCurveSlope;

    [Header("References")]
    private PlanetSurfaceBuilder surfaceBuilder;
    private SatelliteSpawner satelliteSpawner;
    private PlanetGravity gravityBehaviour;
}
