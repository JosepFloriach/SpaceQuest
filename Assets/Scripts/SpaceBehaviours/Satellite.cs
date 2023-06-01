using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    private float currAngle = 0.0f;
    SatelliteSetup setup;

    public void Setup(SatelliteSetup setup)
    {
        this.setup = setup;
        this.currAngle = (2 * Mathf.PI * setup.InitialAngle) / 360.0f;
    }

    public void Update()
    {
        float x = setup.PlanetRadiusOrbit * Mathf.Cos(currAngle);
        float y = setup.PlanetRadiusOrbit * Mathf.Sin(currAngle);

        transform.localPosition = new Vector3(x, y, 1.0f);
        currAngle = (currAngle + (setup.Speed * Time.deltaTime)) % (2 * Mathf.PI);
        transform.localRotation = transform.localRotation * Quaternion.AngleAxis(setup.RotationSpeed * Time.deltaTime, new Vector3(0.0f, 0.0f, 1.0f));
    }

}
