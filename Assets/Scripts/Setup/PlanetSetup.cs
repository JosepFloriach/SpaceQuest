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

[Serializable]
public class PlanetSetup
{
    [Header("General")]
    public float Radius;

    [Header("Visuals")]
    public Sprite Sprite;

    [Header("Gravity Physics")]
    public bool EditCurves = false;
    public float MinimumOrbitSpeed;
    public float MaximumOrbitSpeed;
    public float MaximumDistance;
    public int OrbitDivisions;
    public float DivisionCurveSlope;

    [Header("Satellites")]
    public List<SatelliteSetup> satellites;
}
