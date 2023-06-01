using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSpawner : MonoBehaviour
{
    private List<SatelliteSetup> satellites;
    private void Start()
    {
        foreach(SatelliteSetup satelliteSetup in satellites)
        {
            var satellite = GameObject.Instantiate(satelliteSetup.Prefab);
            satellite.AddComponent<Satellite>();
            satellite.transform.parent = gameObject.transform;
            satellite.transform.localPosition = Vector3.zero;
            satellite.transform.localScale = new Vector3(satelliteSetup.Size, satelliteSetup.Size, 1.0f);
            satellite.GetComponent<Satellite>().Setup(satelliteSetup);
        }
    }

    public void UpdateData(GameObject prototype, List<SatelliteSetup> satellites)
    {
        this.satellites = satellites;
    }

    private void OnDrawGizmos()
    {
        foreach(SatelliteSetup setup in satellites)
        {
            float angleRads = (2 * Mathf.PI * setup.InitialAngle) / 360.0f;
            float x = setup.PlanetRadiusOrbit * Mathf.Cos(angleRads);
            float y = setup.PlanetRadiusOrbit * Mathf.Sin(angleRads);
            Gizmos.color = Color.cyan;
            GizmosCustom.DrawCircle(transform.position + new Vector3(x, y, 0.0f), setup.Size /*(setup.Sprite.rect.width / setup.Sprite.pixelsPerUnit)*/, 5);
        }
    }
}
