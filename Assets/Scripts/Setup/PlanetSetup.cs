using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SatelliteSetup
{
    public float PlanetRadiusOrbit;
    [Range(0, 360)]
    public float InitialAngle;
    public float Speed;
    public float Size;
    public float RotationSpeed;
    public GameObject Prefab;
}

public class PlanetSetup : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float Radius;

    [Header("Visuals")]
    [SerializeField] private Sprite Sprite;

    [Header("Gravity Physics")]
    [SerializeField] private bool EditCurves = false;
    [SerializeField] private float MinimumOrbitSpeed;
    [SerializeField] private float MaximumOrbitSpeed;
    [SerializeField] private float MaximumDistance;
    [SerializeField] private int OrbitDivisions;
    [SerializeField] private float DivisionCurveSlope;


    [Header("Satellites")]
    [SerializeField] private GameObject prototype;
    [SerializeField] private List<SatelliteSetup> satellites;

    [Header("References")]
    private PlanetSurfaceBuilder surfaceBuilder;
    private SatelliteSpawner satelliteSpawner;
    private PlanetGravity gravityBehaviour;

    private void OnValidate()
    {
        surfaceBuilder = GetComponentInChildren<PlanetSurfaceBuilder>();
        satelliteSpawner = GetComponentInChildren<SatelliteSpawner>();
        gravityBehaviour = GetComponentInChildren<PlanetGravity>();

        if (surfaceBuilder)
            surfaceBuilder.UpdateData(Radius, Sprite);
        if (satelliteSpawner)
            satelliteSpawner.UpdateData(prototype, satellites);
        if (gravityBehaviour)
            gravityBehaviour.UpdateData(Radius, OrbitDivisions, MinimumOrbitSpeed, MaximumOrbitSpeed, MaximumDistance, DivisionCurveSlope, EditCurves);
    }
}
