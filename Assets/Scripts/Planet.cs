using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private PlanetSetup setup;
    
    private PlanetSurfaceBuilder surfaceBuilder;
    private SatelliteSpawner satelliteSpawner;
    private PlanetGravity gravityBehaviour;

    private void Awake()
    {
        UpdateData();
    }

    private void Start()
    {
        satelliteSpawner.SpawnSatellites();
    }

    private void OnValidate()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        surfaceBuilder = GetComponentInChildren<PlanetSurfaceBuilder>();
        satelliteSpawner = GetComponentInChildren<SatelliteSpawner>();
        gravityBehaviour = GetComponentInChildren<PlanetGravity>();

        if (surfaceBuilder)
            surfaceBuilder.UpdateData(
                setup.Radius, 
                setup.Sprite);
        if (satelliteSpawner)
            satelliteSpawner.UpdateData(
                setup.satellites);
        //if (gravityBehaviour)
            /*gravityBehaviour.UpdateData(
                setup.Radius, 
                setup.OrbitDivisions, 
                setup.MinimumOrbitSpeed, 
                setup.MaximumOrbitSpeed, 
                setup.MaximumDistance,
                setup.DivisionCurveSlope, 
                setup.EditCurves);*/
    }
}
